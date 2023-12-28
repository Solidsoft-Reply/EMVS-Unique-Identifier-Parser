// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModalInputHandler.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <license>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </license>
// <summary>
// A modal console input handler.  The handler assumes that control is managed by a mode handler,
// and is not specified by commands e.g., entered at a prompt.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.ConsoleMvc;

using System;
using System.Text;
using System.Threading;
using Platform; 

using static System.Console;

/// <summary>
/// A modal console input handler.  The handler assumes that control is managed by a mode
/// handler, and is not specified by commands e.g., entered at a prompt.
/// </summary>
public class ModalInputHandler : IInputHandler
{
    /// <summary>
    /// A lock object for the current line.
    /// </summary>
    private readonly object _currentLineLock = new();

    /// <summary>
    /// The mode manager used in association with the modal input manager.
    /// </summary>
    private readonly IModeManager _modeManager;

    /// <summary>
    /// The input buffer.
    /// </summary>
    private readonly TextInputControl _inputControl = new();

    /// <summary>
    /// Indicates whether to automate line entry for lines that are not terminated by a carriage return.
    /// </summary>
    private static bool _automaticLineEntry;

    /// <summary>
    /// A copy of the current line used to test for new input in the specified interval for automated entry.
    /// </summary>
    private static string _currentLineBuffer = string.Empty;

    /// <summary>
    /// Used to detect redundant disposal calls.
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Signal that the message pump should close.
    /// </summary>
    private bool _terminateMessagePump;

    /// <summary>
    /// A timer used to submit keyboard input if the scanner does not transmit a carriage return.
    /// </summary>
    private Timer _timer;

    /// <summary>
    /// Indicates that the current return character was sent on a timer.
    /// </summary>
    private bool _timedReturn;

    /// <summary>
    /// A value indicating whether the console input is visible.  When visible, the
    /// input is echoed on the terminal screen. 
    /// </summary>
    private Visibility _visibility = Visibility.Visible;

    /// <summary>
    /// Represents a key press event.
    /// </summary>
    public event EventHandler<ConsoleKeyEventArgs> KeyPress;

    /// <summary>
    /// Represents entering of a line.
    /// </summary>
    public event EventHandler<ConsoleLineEntryArgs> LineEntry;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModalInputHandler"/> class
    /// </summary>
    /// <param name="modeManager">
    /// The mode manager used in association with the modal input manager.
    /// </param>
    /// <param name="promptOffset">
    /// The offset required for a command prompt at the start of the command line.
    /// It is not the responsibility of the input handler to create this prompt.
    /// </param>
    /// <param name="mustSupportVt">
    /// Indicates that the input handler must support VT escape sequences.  This is false
    /// by default because older versions of Windows don't support VT and implementation vary
    /// across other platforms and terminals.</param>
    public ModalInputHandler(IModeManager modeManager, int promptOffset = 0, bool mustSupportVt = false)
    {
        PromptOffset = promptOffset;
        _modeManager = modeManager;

        if (mustSupportVt && Platform.OperatingSystem.IsWindows)
        {
            try
            {
                var result = WindowsVt.Enable();

                if (!result)
                {
                    throw new VtUnsupportedException();
                }
            }
            catch (Exception)
            {
                throw new VtUnsupportedException();
            }
        }

        // Start the 'message pump'
        var messagePump = new Thread(ProcessReadKey);
        messagePump.Start();
    }

    /// <summary>
    /// Finalizes the instance of the <see cref="ModalInputHandler"/> class
    /// </summary>
    ~ModalInputHandler() => Dispose(true);

    /// <summary>
    /// The offset required for a command prompt at the start of the command line.
    /// It is not the responsibility of the input handler to create this prompt.
    /// </summary>
    public int PromptOffset { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lines of text that are not terminated by a
    /// carriage return should be entered automatically.
    /// </summary>
    public bool AutomaticEntry
    {
        get => _automaticLineEntry;

        set
        {
#pragma warning disable S2696
            _automaticLineEntry = value;
#pragma warning restore S2696

            if (value)
            {
                StartTimer();
            }
            else
            {
                StopTimer();
            }
        }
    }

    /// <summary>
    /// Gets or sets the time interval between checks to see if there is data ready to be automatically entered, in milliseconds.
    /// </summary>
    public int AutomaticEntryPeriod { get; set; } = 500;

    /// <summary>
    /// Gets or sets a value indicating whether the console input is visible.  When visible, the
    /// input is echoed on the terminal screen. 
    /// </summary>
    public Visibility Visibility {
        get => _visibility;

        set
        {
            _visibility = value;
            _inputControl.Visibility = _visibility;
        }
    }

    /// <summary>
    /// Dispose of resources for the <see cref="ModalInputHandler"/> class
    /// </summary>
    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Send a key to the input for the given character.  This simulates a key press.
    /// </summary>
    /// <param name="keyChar">The character.  Note that this is case-insensitive.
    /// You must provide a modifier to shift the character. </param>
    /// <param name="modifiers">The modifier flags.</param>
    public void Send(char keyChar, ConsoleModifiers modifiers = 0)
    {
        SendKey(
            new ConsoleKeyInfo(
                keyChar,
                ResolveKey(keyChar),
                (modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift,
                (modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt,
                (modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control));
    }

    /// <summary>
    /// Send a key to the input.  This simulates a key press.
    /// </summary>
    /// <param name="keyInfo">The key information.</param>
    public void SendKey(ConsoleKeyInfo keyInfo)
    {
        lock (_currentLineLock)
        {
            // Write character to input buffer
            WriteToBuffer(keyInfo);
        }
    }

    /// <summary>
    /// Resets the input handler.
    /// </summary>
    void IInputHandler.Reset() {
        Reset();
    }

    /// <summary>
    /// Resets the input handler.
    /// </summary>
    /// <param name="promptOffset">The offset required for a command prompt at the start of the command line.</param>
    public void Reset(int promptOffset = 0)
    {
        PromptOffset = promptOffset;
        StateReset();
    }

    /// <summary>
    /// Dispose of resources for the <see cref="ModalInputHandler"/> class
    /// </summary>
    /// <param name="disposing">Used to detect redundant disposal calls.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // Dispose managed state.
            _terminateMessagePump = true;
        }

        _disposed = true;
    }

    /// <summary>
    /// Resets the Console handler state.
    /// </summary>
    private void StateReset()
    {
        lock (_currentLineLock)
        {
            _inputControl.Reset();
            _timedReturn = false;
            _currentLineBuffer = string.Empty;
        }
    }

    /// <summary>
    /// Process each key press. This is the message pump.
    /// </summary>
    private void ProcessReadKey()
    {
        while (true)
        {
            if (_terminateMessagePump)
            {
                return;
            }

            ConsoleKeyInfo input;

            try
            {

                input = ReadKey(true);

                // An ASCII 04 (end of transmission) in a barcode may be represented as DC3 (IBM stop transmission) by a scanner
                // When a dead key literal occurs before an ASCII 04, e.g., in a barcode, it may be reported after the DC3 - i.e., the
                // DC3 is reported before the dead key literal character.  We will detect this and reverse the characters.  We will
                // retain the DC3 for fidelity to the scanner behaviour.
                if (input.KeyChar == 19 && input.Modifiers == 0)
                {
                    int nextChar;

                    if ((nextChar = Read()) > -1)
                    {
                        // There is a character immediately after the DC3
                        Send((char)nextChar);
                    }
                }
            }
            catch (EncoderFallbackException)
            {
                // If the character is not a recognised Unicode character, the encoder (which is always UTF 8 in .NET Core)
                // issues this fallback exception.  The fallback is to emit a space.
                Send(' ');
                continue;
            }

            lock (_currentLineLock)
            {
                // Add the character to input
                WriteToBuffer(input);
            }
        }
    }

    /// <summary>
    /// Start the timer.
    /// </summary>
    private void StartTimer()
    {
        if (_timer == null && _automaticLineEntry)
        {
            _timer = new Timer(OnTimedEvent, null, 0, AutomaticEntryPeriod);
        }
    }

    /// <summary>
    /// Stop the timer.
    /// </summary>
    private void StopTimer()
    {
        if (_timer == null)
        {
            return;
        }

        _timer.Dispose();
        _timer = null;
    }

    /// <summary>
    /// Fires on a timer to check buffers and 'fire' processing if there is
    /// data and no data has been received in the last timer interval.
    /// </summary>
    /// <param name="source">The source.</param>
    private void OnTimedEvent(object source)
    {
        lock (_currentLineLock)
        {
            var currentLine = _inputControl.Input;

            // Test to make sure we have had no more input for the timer interval period
            if (currentLine.Length > 0 && _currentLineBuffer == currentLine)
            {
                // If no further input detected, trigger the parser.
                _timedReturn = true;
                Send('\r');
            }
            else
            {
                _currentLineBuffer = currentLine;
            }
        }
    }

    /// <summary>
    /// Resolve a character to a given console key.
    /// </summary>
    /// <param name="keyChar">The character to resolve</param>
    /// <returns>The console key for the given character.</returns>
    private static ConsoleKey ResolveKey(char keyChar)
    {
        _ = Enum.TryParse<ConsoleKey>(keyChar.ToString().ToUpper(), out var consoleKey);
        return consoleKey;
    }

    /// <summary>
    /// Write the input to the buffer, handling various keystrokes appropriately.
    /// </summary>
    /// <param name="input">The last key press.</param>
    private void WriteToBuffer(ConsoleKeyInfo input) {
        var switchHighlightingOff = _inputControl.IsTextHighlighted;
        var currentInput = _inputControl.Input;

        switch (input.KeyChar)
        {
            case '\r':
            case '\n':
                if (switchHighlightingOff)
                {
                    // Pressing Enter will copy the highlighted text to the clipboard
                    if (Platform.OperatingSystem.IsWindows)
                    {
                        WindowsClipboard.SetText(currentInput);
                    }

                    if (Platform.OperatingSystem.IsMacOs)
                    {
                        OsxClipboard.SetText(currentInput);
                    }

                    if (Platform.OperatingSystem.IsLinux)
                    {
                        LinuxClipboard.SetText(currentInput);
                    }

                    _inputControl.ToggleLineHighlight();

                    return;
                }
                
                _inputControl.Terminate();

                // Read the newInput and raise an event
                var readLine = currentInput;

                lock (_currentLineLock)
                {
                    if (!_timedReturn && input.KeyChar is '\r' or '\n')
                    {
                        readLine += input.KeyChar;
                    }
                    else
                    {
                        _timedReturn = false;
                    }
                }

                StateReset();
                WriteLine();
                LineEntry?.Invoke(this, new ConsoleLineEntryArgs(_modeManager.ModeName, readLine));
                _inputControl.Reset();
                break;
            case '\b':
                _inputControl.Backspace();
                break;
            default:
                _inputControl.ProcessKey(input);

                if (switchHighlightingOff)
                {
                    _inputControl.ToggleLineHighlight();
                }

                break;
        }

        var consoleKeyEventArgs = new ConsoleKeyEventArgs(_modeManager.ModeName, input);
        KeyPress?.Invoke(this, consoleKeyEventArgs);
    }
}