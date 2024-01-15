// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Calibrate.cs" company="Solidsoft Reply Ltd.">
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
// Controls calibration mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Controllers;

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Platform.Windows;
using Model;
using Views;
using Components;
using BarcodeScanner.Calibration;
using Parsers.EmvsUniqueIdentifier;
using ConsoleMvc;
using System.Threading;
using static System.Console;
using System.Linq;

/// <summary>
/// Controls calibration mode.
/// </summary>
public class Calibrate : IController
{
    /// <summary>
    /// Singleton instance of the MessagePump
    /// </summary>
    private static readonly MessagePump MessagePump = new();

    /// <summary>
    /// The input handler.
    /// </summary>
    private readonly ModalInputHandler _inputHandler;

    /// <summary>
    /// The mode manager for the application.
    /// </summary>
    private readonly ModeManager _modeManager;

    /// <summary>
    /// A list of reported calibration information.
    /// </summary>
    private readonly IList<CalibrationInformation> _calibrationInformation = new List<CalibrationInformation>();

    /// <summary>
    /// The view for calibrating scanners.
    /// </summary>
    private IView _calibrationView;

    /// <summary>
    /// A timer used to draw the bitmap reliable on the screen.  This is necessary to resolve a race
    /// condition between Console.Clear and the bitmap drawing routine.
    /// </summary>
    private Timer _timerBitmap;

    /// <summary>
    /// The action for posting messages to the calibration barcode popup window.
    /// </summary>
    private Action<uint> _postMessage;

    /// <summary>
    /// An instance of the pack parser.
    /// </summary>
#if STATELESS_MODEL
    private StatelessParser _parser;
#else
    private Parser _parser;
#endif

#if STATELESS_MODEL
    /// <summary>
    /// The current calibration token.
    /// </summary>
    private CalibrationToken _currentCalibrationToken;
#else
    /// <summary>
    /// The current calibration token enumerator.
    /// </summary>
    private IEnumerator<CalibrationToken> _calibrationTokens;
#endif

    /// <summary>
    /// The number of remaining calibration tokens.  -1 indicates 'unknown'.
    /// </summary>
    private int _remainingCalibrationTokens = -1;

    /// <summary>
    /// Set of name-value pair properties.
    /// </summary>
    private Tuple<string, object>[] _preambleProperties = Array.Empty<Tuple<string, object>>();

    /// <summary>
    /// Indicates whether the timer controlling bitmap display has been activated.
    /// </summary>
    private bool _viewingBitmap;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calibrate"/> class.
    /// </summary>
    /// <param name="modeManager">The mode manager.</param>
    /// <param name="inputHandler">The input handler.</param>
    public Calibrate(ModeManager modeManager, ModalInputHandler inputHandler)
    {
        _modeManager = modeManager;
        _inputHandler = inputHandler;
        _inputHandler.KeyPress += OnKeyPress;
        _inputHandler.LineEntry += OnLineEntry;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include calibration
    /// tests for Format 0n syntax in accordance with ISO/IEC 15434:2019.
    /// </summary>
    public bool IncludeFormatnnTests { get; set; }


    /// <summary>
    /// Initialise the application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private void Initialise()
    {
        // Initialise the input handler
        _inputHandler.Visibility = Visibility.Hidden;
        _inputHandler.Reset();
        _inputHandler.PromptOffset = 0;
        CancelKeyPress += OnCancel;

        _calibrationView = new CalibrationView();

        ResetForCalibration();

        // Set preamble properties
        foreach (var (item1, item2) in _preambleProperties)
        {
            _calibrationView.SetValue(item1, item2);
        }

        _calibrationView.Render(nameof(CalibrationOptions));

        _calibrationInformation.Clear();

#if STATELESS_MODEL
        _parser = new StatelessParser();
#else
        _parser = new Parser();
#endif

        StartTimerBitmap();
        return;

        // Start the bitmap timer.
        void StartTimerBitmap()
        {
            _timerBitmap = new Timer(OnTimedEventBitmap, null, 100, 100);
        }
    }

    /// <summary>
    /// Event handler.  Handles the key press event raised by the input handler.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="consoleKeyEventArgs">The console key event arguments.</param>
    private void OnKeyPress(object sender, ConsoleKeyEventArgs consoleKeyEventArgs)
    {
        // Guard on mode.  This controller only handles events in calibration mode.
        if (consoleKeyEventArgs.Mode != Mode.Calibrating.ToString())
        {
            return;
        }

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (consoleKeyEventArgs.Key)
        {
            case ConsoleKey.F11:
                if ((consoleKeyEventArgs.Modifiers & ConsoleModifiers.Control)
                 == ConsoleModifiers.Control)
                {
                    IncludeFormatnnTests = (consoleKeyEventArgs.Modifiers & ConsoleModifiers.Shift)
                                   != ConsoleModifiers.Shift;

                    ResetCalibration();

                    // Re-activate the Calibrate controller
                    _modeManager.Mode = Mode.Calibrating;
                }

                break;
            case ConsoleKey.F12:
                if (consoleKeyEventArgs.Modifiers == ConsoleModifiers.Control)
                {
                    ResetCalibration();
                    CancelKeyPress -= OnCancel;

                    // Activate the Scan controller
                    _modeManager.Mode = Mode.Scanning;
                }

                break;
        }
    }

    /// <summary>
    /// Event handler.  Handles the key press event raised by the input handler.
    /// </summary>
    private static void OnCancel (object sender, ConsoleCancelEventArgs args)
    {
        BackgroundColor = ConsoleColor.Black;
        ForegroundColor = ConsoleColor.White;

        Environment.Exit(-1);
    }

    /// <summary>
    /// Event handler. Handles the line entry event raised by the input handler.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="consoleLineEntryArgs">The console line entry arguments.</param>
    private void OnLineEntry(object sender, ConsoleLineEntryArgs consoleLineEntryArgs)
    {
        // Guard on mode.  This controller only handles events generated in calibration mode.
        if (consoleLineEntryArgs.Mode != Mode.Calibrating.ToString())
        {
            return;
        }

        var input = consoleLineEntryArgs.Line;

        if (input is "\r" or "\n" or "\r\n")
        {
            return;
        }

#if STATELESS_MODEL
        if (_currentCalibrationToken != default)
        {
            var token = _parser.Calibrator.Calibrate(input, _currentCalibrationToken);
            _currentCalibrationToken = token;
#else
        if (_calibrationTokens != null)
        {
            var token = _parser.Calibrator.Calibrate(input, _calibrationTokens.Current);
#endif

#if SHORTCIRCUIT_CALIBRATION
            if (token.Errors.Any())
            {
                ResetCalibration();
                _calibrationInformation.Clear();

                // Collect calibration information
                foreach (var error in token.Errors)
                {
                    _calibrationInformation.Add(error);
                }

                AddInfoAndWarnings(token);

                _modeManager.Mode = _modeManager.LastMode;
                _modeManager.ScanController.CalibrationInformation = _calibrationInformation;
                _modeManager.ScanController.SystemCapabilities = _parser.Calibrator.SystemCapabilities();
                _modeManager.ScanController.CalibrationFailedMessage();
                _modeManager.ScanController.DisplayCalibrationReport();
                _modeManager.ScanController.DisplayAdviceReport();
                _modeManager.ScanController.DisplayCommandPrompt();
                _viewingBitmap = false;

                return;
            }
#endif

            _calibrationInformation.Clear();

            AddInfoAndWarnings(token);
            WriteLine();
        }

        // Get the next token
#if STATELESS_MODEL
#if SMALL_BARCODES
        _currentCalibrationToken = _parser.Calibrator.NextCalibrationToken(_currentCalibrationToken, 18F, DataMatrixSize.Dm24X24);
#else
        this.currentCalibrationToken = this.parser.Calibrator.NextCalibrationToken(this.currentCalibrationToken, 18F);
#endif
        if (_currentCalibrationToken != default)
        {
            _calibrationView.SetValue("CurrentCalibrationToken", _currentCalibrationToken);
#else
        if (_calibrationTokens?.MoveNext() ?? false)
        {
#endif
        // Destroy the popup calibration barcode window
        _postMessage((uint)WM.USER + 1);

            _calibrationView.Render(nameof(KeepScanningMessage));
        }
        else
        {
            if (!SaveCalibration(_parser.Calibrator.CalibrationData?.ToString(), out var saveErrorMessage))
            {
                _calibrationView.SetValue("SaveErrorMessage", saveErrorMessage);
                _calibrationView.Render(nameof(CalibrationSaveError));
            }

            ResetCalibration();
            Utilities.ClearConsole();

            _modeManager.Mode = _modeManager.LastMode;

            if (_calibrationInformation.Count > 0)
            {
                _modeManager.ScanController.IncludeFormat06 = IncludeFormatnnTests;
                _modeManager.ScanController.CalibrationInformation = _calibrationInformation;
                _modeManager.ScanController.SystemCapabilities = _parser.Calibrator.SystemCapabilities();
                _modeManager.ScanController.DisplayCalibrationReport();
                _modeManager.ScanController.DisplayAdviceReport();
                _modeManager.ScanController.DisplayCommandPrompt();
            }

            if (_remainingCalibrationTokens >= 0)
            {
                _inputHandler.SendKey(new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false));
            }
        }

        return;

        void AddInfoAndWarnings(CalibrationToken token)
        {
            foreach (var warning in token.Warnings)
            {
                _calibrationInformation.Add(warning);
            }

            foreach (var information in token.Information)
            {
                _calibrationInformation.Add(information);
            }
        }
    }

    /// <summary>
    /// Write calibration to the keyboard calibration file.
    /// </summary>
    /// <param name="calibration">The calibration to be saved.</param>
    /// <param name="errorMessage">The error message, if any.</param>
    /// <returns>True, if the calibration was saved; otherwise false.</returns>
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
    private static bool SaveCalibration(string calibration, out string errorMessage)
    {
        errorMessage = string.Empty;

        try
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Keyboard.Calibration";

            using var file = new StreamWriter(File.Create(filePath));
            file.Write(calibration);
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = $"[{ex.GetType().Name}] - {ex.Message}";
            return false;
        }
    }

    /// <summary>
    /// Reset state for calibration.
    /// </summary>
    private void ResetForCalibration()
    {
        ResetCalibration();
        _calibrationInformation.Clear();
    }

    /// <summary>
    /// Reset calibration.
    /// </summary>
    private void ResetCalibration()
    {
#if STATELESS_MODEL
        _currentCalibrationToken = default;
        _calibrationView.SetValue("CurrentCalibrationToken", default(CalibrationToken));
#else
        _calibrationTokens = null;
        _calibrationView.SetValue("CalibrationTokens", null);
#endif
        _remainingCalibrationTokens = -1;

        // Destroy the popup calibration barcode window
        _postMessage?.Invoke((uint)WM.USER + 1);
    }

    /// <summary>
    /// Fires on a timer to draw the next calibration barcode.
    /// </summary>
    /// <param name="source">The source.</param>
    private void OnTimedEventBitmap(object source)
    {
        if (_viewingBitmap)
        {
            return;
        }

        _viewingBitmap = true;

#if STATELESS_MODEL
        if (_currentCalibrationToken == default)
#else
        if (_calibrationTokens == null)
#endif
        {
            _parser.Calibrator.AssessFormatnnSupport = IncludeFormatnnTests;

#if STATELESS_MODEL
#if SMALL_BARCODES
            _currentCalibrationToken = _parser.Calibrator.NextCalibrationToken(_currentCalibrationToken, 18F, DataMatrixSize.Dm24X24);
#else
            _currentCalibrationToken = _parser.Calibrator.NextCalibrationToken(this.currentCalibrationToken, 18F);
#endif
            _calibrationView.SetValue("CurrentCalibrationToken", _currentCalibrationToken);
#else
#if SMALL_BARCODES
            _calibrationTokens = _parser.Calibrator.CalibrationTokens(18F, DataMatrixSize.Dm24X24).GetEnumerator();
#else
            _calibrationTokens = _parser.Calibrator.CalibrationTokens(18F).GetEnumerator();
#endif

            _calibrationView.SetValue("CalibrationTokens", _calibrationTokens);
            _calibrationTokens.MoveNext();
#endif
        }

#if STATELESS_MODEL
        if (_currentCalibrationToken.BitmapStream is { CanRead: true })
#else
        if (_calibrationTokens.Current.BitmapStream is { CanRead: true })
#endif
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                MessagePump.CreateMessagePump(
#if STATELESS_MODEL
                    _currentCalibrationToken.BitmapStream,
#else
                    _calibrationTokens.Current.BitmapStream,
#endif
                    pm => _postMessage = pm,
                    _inputHandler.SendKey);
            }

#if STATELESS_MODEL
            if (_currentCalibrationToken != default)
            {
                _remainingCalibrationTokens = _currentCalibrationToken.Remaining;
            }
#else
#pragma warning disable S2589
            if (_calibrationTokens is not null)
#pragma warning restore S2589
            {
                _remainingCalibrationTokens = _calibrationTokens.Current.Remaining;
            }
#endif
        }

#if STATELESS_MODEL
        if (_currentCalibrationToken.BitmapStream == default)
#else
#pragma warning disable S2589
        if (_calibrationTokens?.Current.BitmapStream == null)
#pragma warning restore S2589
#endif
        {
            StopTimerBitmap();
        }
        else
        {
            _viewingBitmap = false;
            DisplayNextCalibrationBitmap();
        }

        return;

        // Stop the timer.
        // ReSharper disable once ImplicitlyCapturedClosure
        void StopTimerBitmap()
        {
            if (_timerBitmap == null)
            {
                return;
            }

            _timerBitmap.Dispose();
            _timerBitmap = null;
            _viewingBitmap = false;
        }
    }

    /// <inheritdoc />
    public Action<string> Activate(params Tuple<string, object>[] properties)
    {
        _preambleProperties = properties;
        Initialise();
        return _calibrationView.Render;
    }

    /// <summary>
    /// Display the next calibration bitmap.
    /// </summary>
    private void DisplayNextCalibrationBitmap() {
        // Start the bitmap timer.
        _timerBitmap ??= new Timer(OnTimedEventBitmap, null, 100, 100);
    }
}