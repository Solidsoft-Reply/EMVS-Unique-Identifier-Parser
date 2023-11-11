// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsVt.cs" company="Solidsoft Reply Ltd.">
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
// Represents the ability of the Windows console to handle VT sequences.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Represents the ability of the Windows console to handle VT sequences.
/// </summary>
public static class WindowsVt
{
    /// <summary>
    /// Handle for standard output.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private const int STD_OUTPUT_HANDLE = -11;

    /// <summary>
    /// When writing with WriteFile or WriteConsole, characters are parsed for VT100 and similar
    /// control character sequences that control cursor movement, color/font mode, and other
    /// operations that can also be performed via the existing Console APIs. For more information,
    /// see Console Virtual Terminal Sequences.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    /// <summary>
    /// <p>When writing with WriteFile or WriteConsole, this adds an additional state to end-of-line
    /// wrapping that can delay the cursor move and buffer scroll operations.</p>
    /// <p>Normally when ENABLE_WRAP_AT_EOL_OUTPUT is set and text reaches the end of the line, the
    /// cursor will immediately move to the next line and the contents of the buffer will scroll up
    /// by one line.In contrast with this flag set, the scroll operation and cursor move is delayed
    /// until the next character arrives. The written character will be printed in the final
    /// position on the line and the cursor will remain above this character as if
    /// ENABLE_WRAP_AT_EOL_OUTPUT was off, but the next printable character will be printed as if
    /// ENABLE_WRAP_AT_EOL_OUTPUT is on.No overwrite will occur. Specifically, the cursor quickly
    /// advances down to the following line, a scroll is performed if necessary, the character is
    /// printed, and the cursor advances one more position.</p>
    /// <p>The typical usage of this flag is intended in conjunction with setting
    /// ENABLE_VIRTUAL_TERMINAL_PROCESSING to better emulate a terminal emulator where writing the
    /// final character on the screen (in the bottom right corner) without triggering an immediate
    /// scroll is the desired behavior.</p>
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

    /// <summary>
    /// Enable VT processing in the Windows console.
    /// </summary>
    /// <returns></returns>
    public static bool Enable()
    {
        var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);

        if (!GetConsoleMode(iStdOut, out var outConsoleMode))
        {
            return false;
        }

        outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;

        return SetConsoleMode(iStdOut, outConsoleMode);
    }

    /// <summary>
    /// Retrieves the current input mode of a console's input buffer or the current output mode of a console screen buffer.
    /// </summary>
    /// <param name="hConsoleHandle">
    /// A handle to the console input buffer or the console screen buffer. The handle must have the GENERIC_READ access
    /// right. For more information, see Console Buffer Security and Access Rights.
    /// </param>
    /// <param name="lpMode">
    /// A pointer to a variable that receives the current mode of the specified buffer.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    /// <summary>
    /// Sets the input mode of a console's input buffer or the output mode of a console screen buffer.
    /// </summary>
    /// <param name="hConsoleHandle">
    /// A handle to the console input buffer or a console screen buffer. The handle must have the
    /// GENERIC_READ access right. For more information, see Console Buffer Security and Access Rights.
    /// </param>
    /// <param name="dwMode">
    /// The input or output mode to be set. If the hConsoleHandle parameter is an input handle, the mode can
    /// be one or more of the following values. When a console is created, all input modes except
    /// ENABLE_WINDOW_INPUT are enabled by default.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    /// <summary>
    /// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
    /// </summary>
    /// <param name="nStdHandle">
    /// The standard device. This parameter can be one of the following values.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is a handle to the specified device, or a redirected handle
    /// set by a previous call to SetStdHandle. The handle has GENERIC_READ and GENERIC_WRITE access rights,
    /// unless the application has used SetStdHandle to set a standard handle with lesser access.</p>
    /// <p>If the function fails, the return value is INVALID_HANDLE_VALUE.To get extended error information,
    /// call GetLastError.</p>
    /// <p>If an application does not have associated standard handles, such as a service running on an interactive
    /// desktop, and has not redirected them, the return value is NULL.</p>
    /// </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);
}