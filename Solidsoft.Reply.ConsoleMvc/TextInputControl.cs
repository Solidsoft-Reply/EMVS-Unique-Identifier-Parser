// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextInputControl.cs" company="Solidsoft Reply Ltd.">
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
// Manages the keyboard input to the console for a text input line. This handler 
// emulates the classic Windows (DOS-like) command line.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
#pragma warning disable S3358

namespace Solidsoft.Reply.ConsoleMvc;

using static Console;

/// <summary>
/// Handles the keyboard input to the console for a text input line.  This handler 
/// emulates the classic Windows (DOS-like) command line.
/// </summary>
public class TextInputControl
{
    /// <summary>
    /// The character used to escape ASCII control characters.
    /// </summary>
    private const char IdcEscapeChar = '\u241B';

    /// <summary>
    /// The character used to represent ASCII NULL characters.
    /// </summary>
    private const char IdcNullChar = '\u2400';

    /// <summary>
    /// Regular expression to replace escaped ASCII control characters.
    /// </summary>
    private static readonly Regex EscapedAsciiRegEx = new($@"{IdcEscapeChar}(?=[\u0001-\u001F])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Regular expression to replace ASCII NULL control characters.
    /// </summary>
    private static readonly Regex NullAsciiRegEx = new($@"{IdcNullChar}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// The data required to determine the absolute (buffer index) start of the current input.
    /// </summary>
    private (bool init, int width, int top, int promptOffset) _startPosition = (false, -1, -1, -1);

    /// <summary>
    /// The input string.  This string may contain 'escaped' ASCII control characters.
    /// </summary>
    private string _input = string.Empty;

    /// <summary>
    /// A history of the last 50 (by default) previous inputs.
    /// </summary>
    private List<string> _storedInputs = new(50);

    /// <summary>
    /// The current position in the stored input list.
    /// </summary>
    private int _storedInputCurrentPos;

    /// <summary>
    /// Indicates whether to insert or overwrite characters.
    /// </summary>
    private bool _insert = true;

    /// <summary>
    /// The current cursor index.
    /// </summary>
    private int _currentCursorIndex = -1;

    /// <summary>
    /// Gets or sets the current visibility for the input control. 
    /// </summary>
    public Visibility Visibility { get; set; }

    /// <summary>
    /// Indicates that text has been highlighted.  NB.  This is only relevant
    /// if Ctrl key shortcuts are disabled in the terminal.
    /// </summary>
    public bool IsTextHighlighted { get; set; }

    /// <summary>
    /// Gets the input.
    /// </summary>
    /// <remarks>The input property contains non-escaped ASCII control characters.</remarks>
    public string Input => NullAsciiRegEx.Replace(EscapedAsciiRegEx.Replace(_input, string.Empty), "\u0000");

    /// <summary>
    /// Gets or sets the maximum number of command lines stored in memory. Defaults to 50.
    /// </summary>
    public int InputStoreSize { get; set; } = 50;

    /// <summary>
    /// Gets a flag that indicates whether the cursor is positioned in the input.
    /// </summary>
    public bool IsCursorInInput {
        get {
            var (startTop, startLeft) = MapAbsoluteToPos(StartAbsolute);
            var (endTop, endLeft) = MapAbsoluteToPos(StartAbsolute + _input.Length);

            return (top: CursorTop, left: CursorLeft) switch {
#pragma warning disable S2589
                (_, _) when StartAbsolute < 0 => false,
                (top: var t, left: var l) when t == startTop && l >= startLeft - 1 => true,
                (top: var t, left: var l) when t == endTop && l <= endLeft => true,
                (top: var t, _) when t > startTop && t < endTop => true,
                _ => false
#pragma warning restore S2589
            };
        }
    }

    /// <summary>
    /// Gets a flag that indicates whether the cursor is positioned at the start of the input.
    /// </summary>
    public bool IsCursorAtStartOfInput {
        get {
            var (startTop, startLeft) = MapAbsoluteToPos(StartAbsolute);

            return (top: CursorTop, left: CursorLeft) switch {
#pragma warning disable S2589
                (_, _) when StartAbsolute < 0 => false,
                (top: var t, left: var l) when t == startTop && l == startLeft => true,
                _ => false
#pragma warning restore S2589
            };
        }
    }

    /// <summary>
    /// Gets a flag that indicates whether the cursor is positioned at the end of the input.
    /// </summary>
    public bool IsCursorAtEndOfInput {
        get {
            var (endTop, endLeft) = MapAbsoluteToPos(StartAbsolute + _input.Length - 1);

            return (top: CursorTop, left: CursorLeft) switch {
#pragma warning disable S2589
                (_, _) when StartAbsolute < 0 => false,
                (top: var t, left: var l) when t == endTop && l == endLeft + 1 => true,
                _ => false
#pragma warning restore S2589
            };
        }
    }

    /// <summary>
    /// Process information for a given keystroke.
    /// </summary>
    /// <param name="keyInfo">The information about the keystroke.</param>
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
    public void ProcessKey(ConsoleKeyInfo keyInfo) {
        switch (keyInfo.Key) {
            case ConsoleKey.UpArrow:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrievePreviousStoredInput();

                        break;
                }

                break;
            case ConsoleKey.DownArrow:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveNextStoredInput();

                        break;
                }

                break;
            case ConsoleKey.PageUp:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveFirstStoredInput();

                        break;
                }

                break;
            case ConsoleKey.PageDown:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveLastStoredInput();

                        break;
                }

                break;
            case ConsoleKey.LeftArrow:
                switch (keyInfo.Modifiers) {
                    case ConsoleModifiers.Control:
                        // Move left to next space.
                        MoveCursorLeftToNextSpace();

                        break;
                    case ConsoleModifiers.Alt:
                    case ConsoleModifiers.Shift:
                        // Do nothing.
                        break;
                    default:
                        MoveCursorLeft();

                        break;
                }

                break;
            case ConsoleKey.RightArrow:
                switch (keyInfo.Modifiers) {
                    case ConsoleModifiers.Control:
                        // Move right to next space.
                        MoveCursorRightToNextSpace();
                        break;
                    case ConsoleModifiers.Alt:
                    case ConsoleModifiers.Shift:
                        // Do nothing.
                        break;
                    default:
                        MoveCursorRight();
                        break;
                }

                break;
            case ConsoleKey.Home:
                switch (keyInfo.Modifiers) {
                    case 0:
                        MoveCursorToHome();

                        break;
                    case ConsoleModifiers.Control:
                        // Erase the current input to the left of the cursor.
                        EraseInputToLeft();

                        break;
                }

                break;
            case ConsoleKey.End:
                switch (keyInfo.Modifiers) {
                    case 0:
                        MoveCursorToEnd();

                        break;
                    case ConsoleModifiers.Control:
                        // Erase the current input to the right of the cursor.
                        EraseInputToRight();

                        break;
                }

                break;
            case ConsoleKey.Delete:
                switch (keyInfo.Modifiers) {
                    case 0:
                        Delete();

                        break;
                }

                break;
            case ConsoleKey.Escape:
                Clear();

                break;
            case ConsoleKey.F1:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveNextCharacterOfCurrentStoredInput();

                        break;
                }

                break;
            case ConsoleKey.F2:
                break;
            case ConsoleKey.F3:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveCurrentStoredInput();

                        break;
                }

                break;
            case ConsoleKey.F4:
                break;
            case ConsoleKey.F5:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrievePreviousStoredInput();

                        break;
                }

                break;
            case ConsoleKey.F7:
                switch (keyInfo.Modifiers) {
                    case ConsoleModifiers.Control | ConsoleModifiers.Alt:
                        ClearStoredInputs();

                        break;
                    case 0:
                        break;
                }

                break;
            case ConsoleKey.F8:
                switch (keyInfo.Modifiers) {
                    case 0:
                        RetrieveLastMatchingStoredInput();

                        break;
                }

                break;
            case ConsoleKey.F9:

                break;
            case ConsoleKey.Insert:
                _insert = !_insert;

                break;
            case var _ when keyInfo.KeyChar == '\u0012':
            case var _ when keyInfo.KeyChar == '\u0013':

                break;
            default:

                if (keyInfo.KeyChar > 0 && keyInfo is { Key: ConsoleKey.A, Modifiers: ConsoleModifiers.Control })
                {
                    ToggleLineHighlight();
                    return;
                }

                Insert(keyInfo.KeyChar);

                break;
        }
    }

    /// <summary>
    /// Insert or append characters to the input.
    /// </summary>
    /// <param name="line">The line of text to be input.</param>
    public void InsertLine(string line) {
        foreach (var c in line) {
            Insert(c);
        }
    }

    /// <summary>
    /// Insert or append a character to the input.
    /// </summary>
    /// <param name="keyChar">The character to be inserted into the input.</param>
    public void Insert(char keyChar) {
        InitTextLineBuffer();
        if (!IsCursorInInput) return;

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (Visibility) {
            case Visibility.Visible:
                var insertPos = BufferCursorIndex;
                var remainder = _input[(insertPos + (_insert ? 0 : 1))..];
                var asciiCtrl = keyChar < 32;
                Write((asciiCtrl && keyChar != 0 ? "^" + (char)(keyChar + 64) : keyChar == 0 ? string.Empty : keyChar) + remainder);
                _input = _input[..insertPos] + (asciiCtrl && keyChar != 0 ? IdcEscapeChar + keyChar.ToString() : keyChar == 0 ? IdcNullChar : keyChar.ToString()) + remainder;
                var (top, left) = MapAbsoluteToPos(insertPos + (asciiCtrl && keyChar != 0  ? 2 : 1) + StartAbsolute);
                CursorTop = top;
                CursorLeft = left - (left >= BufferWidth ? BufferWidth : 0);
                break;
            case Visibility.Hidden:
                _input += keyChar;
                break;
        }
    }

    /// <summary>
    /// Delete the character at the given index
    /// </summary>
    public void Delete() {
        if (StartAbsolute < 0 || !IsCursorInInput) return;

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (Visibility) {
            case Visibility.Visible:
                var inSpan = TryDetectSpan(out var deletePos);
                var remainder = _input.Length > 0 && deletePos < _input.Length 
                    ? _input[(deletePos + (inSpan ? 2 : 1))..] 
                    : string.Empty;
                Write(remainder);
                Write(new string(' ', inSpan ? 2 : 1));
                _input = _input[..deletePos] + remainder;
                var (top, left) = MapAbsoluteToPos(deletePos + StartAbsolute);
                CursorTop = top;
                CursorLeft = left;
                break;
            case Visibility.Hidden:
                _input = _input[..^1];
                break;
        }
    }

    /// <summary>
    /// Delete the character before the character at the given index
    /// </summary>
    public void Backspace() {
        if (StartAbsolute < 0 || !IsCursorInInput) return;

        MoveCursorLeft();
        Delete();
    }

    /// <summary>
    /// Toggle line highlighting on or off.
    /// </summary>
    /// <remarks>This only works if Ctrl key shortcuts are disabled in the terminal.</remarks>
    public void ToggleLineHighlight() {
        var currentCursorPos = CursorLeft;
        var currentBackgroundColour = BackgroundColor;
        var currentForegroundColour = ForegroundColor;

        CursorLeft = 0;

        if (!IsTextHighlighted) {
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
        }

        if (Visibility == Visibility.Visible) {
            Write(Input);
        }

        CursorLeft = currentCursorPos;
        ForegroundColor = currentForegroundColour;
        BackgroundColor = currentBackgroundColour;
        IsTextHighlighted = !IsTextHighlighted;
    }

    /// <summary>
    /// Move the cursor one position to the left in the input
    /// </summary>
    public void MoveCursorLeft() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtStartOfInput) return;

        int spanStartIndex;

        switch (CursorLeft) {
            case 0:
                CursorTop -= 1;
                CursorLeft = BufferWidth - 1;
                CursorLeft -= TryDetectSpan(out spanStartIndex) ? BufferCursorIndex - spanStartIndex : 0;
                break;
            default:
                CursorLeft -= 1;
                var inSpan = TryDetectSpan(out spanStartIndex);
                var inSpanAtLeft = inSpan && CursorLeft == 0;
                CursorLeft -= inSpanAtLeft 
                    ? -(BufferWidth - (BufferCursorIndex - spanStartIndex)) 
                    : inSpan 
                        ? BufferCursorIndex - spanStartIndex
                        :0;
                CursorTop -= inSpanAtLeft ? 1 : 0;
                break;
        }
    }

    /// <summary>
    /// Move the cursor one position to the right in the input
    /// </summary>
    public void MoveCursorRight() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtEndOfInput) return;

        switch (CursorLeft) {
#pragma warning disable S2589
            case var l when l == BufferWidth - 1:
                CursorLeft = TryDetectSpan(out var spanStartIndex) ? BufferCursorIndex - spanStartIndex + 1 : 0;
                CursorTop += 1;
                break;
            default:
                var inSpan = TryDetectSpan(out _);
                CursorLeft += inSpan ? 2 : 1;
                break;
#pragma warning restore S2589
        }
    }

    /// <summary>
    /// Move the cursor to the next space to the left in the input
    /// </summary>
    public void MoveCursorLeftToNextSpace() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtStartOfInput) return;

        var cursorIndex = BufferCursorIndex;
#pragma warning disable S108
        while (cursorIndex > 0 && _input[--cursorIndex] == ' ') { }
#pragma warning restore S108
        var (top, left) = MapAbsoluteToPos(_input[..cursorIndex].LastIndexOf(' ') + StartAbsolute + 1);
        CursorTop = top;
        CursorLeft = left;
    }

    /// <summary>
    /// Move the cursor to the next space to the right in the input
    /// </summary>
    public void MoveCursorRightToNextSpace() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtEndOfInput) return;

        var cursorIndex = BufferCursorIndex;
        var nextSpaceIndex = _input[cursorIndex..].IndexOf(' ');
        cursorIndex = nextSpaceIndex == -1 ? _input.Length - 1 : cursorIndex + nextSpaceIndex - 1;
        while (cursorIndex < _input.Length) {
            if (cursorIndex++ == _input.Length - 1) break;
            if (_input[cursorIndex] != ' ') break;
        }
        var (top, left) = MapAbsoluteToPos(cursorIndex + StartAbsolute);
        CursorTop = top;
        CursorLeft = left;
    }

    /// <summary>
    /// Move the cursor to the start of the input.
    /// </summary>
    public void MoveCursorToHome() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtStartOfInput) return;

        var (top, left) = MapAbsoluteToPos(StartAbsolute);
        CursorTop = top;
        CursorLeft = left;
    }

    /// <summary>
    /// Move the cursor to the end of the input.
    /// </summary>
    public void MoveCursorToEnd() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtEndOfInput) return;

        var (top, left) = MapAbsoluteToPos(StartAbsolute + _input.Length);
        CursorTop = top;
        CursorLeft = left;
    }

    /// <summary>
    /// Erase all characters in the current input to the left of the cursor.
    /// </summary>
    public void EraseInputToLeft() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput) return;

        var newInput = _input[BufferCursorIndex..];

        Clear();
        InsertLine(newInput);
    }

    /// <summary>
    /// Erase all characters in the current input to the right of the cursor.
    /// </summary>
    public void EraseInputToRight() {
        if (Visibility == Visibility.Hidden || !IsCursorInInput || IsCursorAtEndOfInput) return;

        var cursorTop = CursorTop;
        var cursorLeft = CursorLeft;

        Write(new string(' ', _input.Length - BufferCursorIndex));
        CursorTop = cursorTop;
        CursorLeft = cursorLeft;
        _input = _input[..BufferCursorIndex];
    }

    /// <summary>
    /// Retrieves and displays the previous stored input.
    /// </summary>
    public void RetrievePreviousStoredInput() {
        if (_storedInputs.Count == 0 || _storedInputCurrentPos <= 0) return;

        _storedInputCurrentPos = _storedInputCurrentPos >= _storedInputs.Count - 1
            ? _storedInputs.Count - (_input == string.Empty ? 1 : 2)
            : --_storedInputCurrentPos;

        DisplayHistoricInput();
    }

    /// <summary>
    /// Retrieves and displays the next stored input.
    /// </summary>
    public void RetrieveNextStoredInput() {
        if (_storedInputs.Count <= 0 || _storedInputCurrentPos >= _storedInputs.Count - 1) return;

        var lastIndex = _storedInputs.Count - 1;

        _storedInputCurrentPos = _storedInputCurrentPos >= lastIndex
            ? AssignSortedInputCurrentPos()
            : ++_storedInputCurrentPos;

        DisplayHistoricInput();

        int AssignSortedInputCurrentPos() => _storedInputCurrentPos = lastIndex;
    }

    /// <summary>
    /// Retrieves and displays the first stored text input.
    /// </summary>
    public void RetrieveFirstStoredInput(bool visible = true) {
        if (_storedInputs.Count == 0 || _storedInputCurrentPos <= 0) return;

        _storedInputCurrentPos = 0;
        DisplayHistoricInput();
    }

    /// <summary>
    /// Retrieves and displays the last stored input.
    /// </summary>
    public void RetrieveLastStoredInput(bool visible = true) {
        if (_storedInputs.Count <= 0 || _storedInputCurrentPos >= _storedInputs.Count - 1) return;

        _storedInputCurrentPos = _storedInputs.Count - 1;
        DisplayHistoricInput();
    }

    /// <summary>
    /// Retrieves and appends a character from the current stored input to the current input.
    /// </summary>
    public void RetrieveNextCharacterOfCurrentStoredInput() {
        if (Visibility != Visibility.Visible ||
            _storedInputs.Count <= 0 ||
            _storedInputCurrentPos >= _storedInputs.Count) return;

        var cursorIndex = StartAbsolute >= 0 ? BufferCursorIndex : 0;
        var lastInput = _storedInputs[_storedInputCurrentPos];
        if (cursorIndex >= lastInput.Length) return;
        Insert(lastInput[cursorIndex]);
    }

    /// <summary>
    /// Retrieves and displays the current stored text input.
    /// </summary>
    public void RetrieveCurrentStoredInput() {
        if (_storedInputs.Count <= 0 || _storedInputCurrentPos > _storedInputs.Count - 1) return;

        DisplayHistoricInput();
    }

    /// <summary>
    /// Retrieves and displays the last stored input that matches the current displayed input.
    /// </summary>
    public void RetrieveLastMatchingStoredInput() {
        if (_storedInputs.Count <= 0 || _storedInputCurrentPos > _storedInputs.Count - 1) return;

        var startSearchPos = _storedInputCurrentPos - (_input == _storedInputs[_storedInputCurrentPos] ? 1 : 0);
        var previousMatchPos = _storedInputs.FindLastIndex(startSearchPos, startSearchPos + 1, s => s == _input);
        _storedInputCurrentPos = previousMatchPos >= 0 ? previousMatchPos : _storedInputCurrentPos;
        DisplayHistoricInput();
    }

    /// <summary>
    /// Clears the stored inputs.
    /// </summary>
    public void ClearStoredInputs() {
        _storedInputs = new List<string>(InputStoreSize);
    }

    /// <summary>
    /// Registers the termination of the current text input line. This method must be called whenever teh current text line is
    /// submitted or abandoned.
    /// </summary>
    public void Terminate() {
        if (_input.Length == 0)
        {
            Clear();
            return;
        }

        if (_storedInputs.Count >= InputStoreSize)
            _storedInputs = _storedInputs.GetRange(_storedInputs.Count - InputStoreSize + 1, InputStoreSize - 1);
        if (_storedInputs.Count == 0 || _storedInputs[^1] != _input) _storedInputs.Add(_input);
        _storedInputCurrentPos = _storedInputs.Count;
    }

    /// <summary>
    /// Clear the current input.
    /// </summary>
    public void Clear() {
        if (Visibility == Visibility.Visible) {
            var startTop = CursorTop;
            var startLeft = CursorLeft;

            if (StartAbsolute >= 0) {
                (startTop, startLeft) = MapAbsoluteToPos(StartAbsolute);
                CursorTop = startTop;
                CursorLeft = startLeft;
            }

            Write(new string(' ', _input.Length));
            CursorTop = startTop;
            CursorLeft = startLeft;
        }

        Reset();
    }

    /// <summary>
    /// Reset the input.
    /// </summary>
    public void Reset() {
        _input =string.Empty;
        _startPosition = (false, -1, -1, -1);
    }

    /// <summary>
    /// Detect if the current index of the cursor in the screen buffer is in a multi-character span
    /// that represents a non-printing character.
    /// </summary>
    /// <param name="spanStartIndex">
    /// The screen buffer index at which the span starts, or the current
    /// index if no span is detected.
    /// </param>
    /// <returns>True, if a span is detected; otherwise false.</returns>
    private bool TryDetectSpan(out int spanStartIndex) {
        var cursorIndex = spanStartIndex = BufferCursorIndex;

        var state = cursorIndex switch {
            <= 1 => 0,
            _ when cursorIndex >= _input.Length => 0, 
            _ when _input[cursorIndex] == IdcEscapeChar => 1,
            _ when _input[cursorIndex - 1] == IdcEscapeChar => 2,
            _ => 0
        };

        spanStartIndex = state switch {
            0 => cursorIndex,
            1 => cursorIndex,
            _ => cursorIndex - 1
        };

        return state != 0;
    }

    /// <summary>
    /// Displays a historic input line as the current input.
    /// </summary>
    private void DisplayHistoricInput()
    {
        Clear();
        InsertLine(RemoveControlEscapes(_storedInputs[_storedInputCurrentPos]));
    }

    /// <summary>
    /// Converts an absolute screen buffer position to screen buffer coordinates.
    /// </summary>
    /// <param name="absolutePosition">An absolute screen buffer position</param>
    /// <returns>Screen buffer coordinates</returns>
    private static (int top, int left) MapAbsoluteToPos(int absolutePosition) => absolutePosition switch {
            < 0 => (0, 0),
            _ => (absolutePosition / BufferWidth, absolutePosition % BufferWidth)
    };

    /// <summary>
    /// Converts any escaped ASCII control characters to literal ASCII characters by removing the escape character.
    /// </summary>
    /// <param name="value">The string to be converted.</param>
    /// <returns>The converted string.</returns>
    private static string RemoveControlEscapes(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;

        var convertedChars = new char[value.Length];
        var charIdx = 0;


        for (var idx = 0; idx < value.Length; idx++)
        {
            convertedChars[charIdx] = value[idx] == IdcEscapeChar &&
                                      idx < value.Length - 1 &&
                                      value[idx + 1] switch
                                      {
                                          < (char)32 => true,
                                          _ => false
                                      }
                ? ConvertToNextCharacter()
                : ConvertToCharacter();
            continue;

            char ConvertToNextCharacter() => convertedChars[charIdx++] = value[++idx];

            char ConvertToCharacter() => convertedChars[charIdx++] = value[idx];
        }

        return new string(convertedChars);
    }

    /// <summary>
    /// Get and internally memoise the current cursor index.
    /// </summary>
    /// <param name="start">The position of the start of the input.</param>
    /// <returns></returns>
    private int GetCurrentCursorIndex((int top, int left) start) => _currentCursorIndex =
        (CursorTop - start.top) * BufferWidth - start.left + CursorLeft;

    /// <summary>
    /// Gets the index in the input for the current cursor position.
    /// </summary>
    private int BufferCursorIndex => GetCurrentCursorIndex(MapAbsoluteToPos(StartAbsolute));

    /// <summary>
    /// Get and internally memoise the start position of the current input.
    /// </summary>
    private (bool init, int width, int top, int promptOffset) StartPosition => _startPosition =
        (_startPosition.width != BufferWidth) switch {
            true => (_startPosition.init, 
                     BufferWidth,
                     GetCursorPosition().Top - (_currentCursorIndex + _startPosition.promptOffset) / BufferWidth,
                     _startPosition.promptOffset),
            false => _startPosition
        };

    /// <summary>
    /// Gets the absolute (buffer) index of the start position of the current input.
    /// </summary>
    private int StartAbsolute => StartPosition.init switch {
        false => -1,
        _ => BufferWidth * StartPosition.top + StartPosition.promptOffset
    };

    /// <summary>
    /// Initialises the input.
    /// </summary>
    private void InitTextLineBuffer() {
        // If the text line buffer is already initialized, then return, 
        if (_startPosition.init) return;

        // If startAbsolute is not yet set (less than 0) then set it.
        _startPosition = (true, BufferWidth, CursorTop, CursorLeft);

        // Set the cursor position.
        if (Visibility == Visibility.Hidden) return;
        var (top, left) = MapAbsoluteToPos(StartAbsolute);
        CursorTop = top;
        CursorLeft = left;
    }
}