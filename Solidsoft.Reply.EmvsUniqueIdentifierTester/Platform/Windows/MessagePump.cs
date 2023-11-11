// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagePump.cs" company="Solidsoft Reply Ltd.">
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
// The Windows message pump.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable S3358
#pragma warning disable CA1416
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// The Windows message pump.
/// </summary>
public class MessagePump : IDisposable
{
    /// <summary>
    ///     The class name.
    /// </summary>
    private const string IdcCalibrationBarcodeWinClassName = "CalibrationBarcodeWin";

    /// <summary>
    ///     Value indicates that the WM_UNICHAR message was sent with no character.
    /// </summary>
    private const int IdcUnicodeNochar = 0xFFFF;

    /// <summary>
    ///     CTRL key.
    /// </summary>
    private const int IdcVkControl = 0x11;

    /// <summary>
    ///     Left CTRL key.
    /// </summary>
    private const int IdcVkLcontrol = 0xA2;

    /// <summary>
    ///     ALT key.
    /// </summary>
    private const int IdcVkLmenu = 0xA4;

    /// <summary>
    ///     Left SHIFT key.
    /// </summary>
    private const int IdcVkLshift = 0xA0;

    /// <summary>
    ///     ALT key.
    /// </summary>
    private const int IdcVkMenu = 0x12;

    /// <summary>
    ///     Right CTRL key.
    /// </summary>
    private const int IdcVkRcontrol = 0xA3;

    /// <summary>
    ///     Right SHIFT key.
    /// </summary>
    private const int IdcVkRshift = 0xA1;

    /// <summary>
    ///     SHIFT key.
    /// </summary>
    private const int IdcVkShift = 0x10;

    /// <summary>
    ///     The registration atom for the CalibrationBarcodeWin class.
    /// </summary>
    private static ushort _classAtom;

    /// <summary>
    ///     The console window.  We obtain this as the foreground window in Windows.  There is an
    ///     assumption here that the conhost window is the foreground window.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private static IntPtr _consoleForegroundWindow;

    /// <summary>
    ///     The current bitmap.
    /// </summary>
    private static System.Drawing.Bitmap _currentBitmap;

    /// <summary>
    ///     The current handle for the pop-up window.
    /// </summary>
    private static IntPtr _currentPopupHwnd = IntPtr.Zero;

    /// <summary>
    ///     Garbage collection handle for the managed window procedure.
    /// </summary>
    private static GCHandle _gchManagedWndProc;

    /// <summary>
    ///     The instance handle for the current process.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "Code follows Windows API naming conventions.")]
    private static IntPtr _hInstance;

    /// <summary>
    ///     The pop-up window class information.
    /// </summary>
    private static WNDCLASSEX _wndClass;

    /// <summary>
    ///     A queue of input characters.  Strings are used to support Unicode.
    /// </summary>
    private readonly Queue<string> _charQueue = new();

    /// <summary>
    ///     Indicates whether the Alt key is currently being held down.
    /// </summary>
    private bool _isAlt;

    /// <summary>
    ///     Indicates whether the Control key is currently being held down.
    /// </summary>
    private bool _isControl;

    /// <summary>
    ///     Indicates whether the Shift key is currently being held down.
    /// </summary>
    private bool _isShift;

    /// <summary>
    ///     A count of the number of 'nested' KeyDown events.
    /// </summary>
    private int _κeyDownEnqueuedCharCount;

    /// <summary>
    ///     Finalizes an instance of the <see cref="MessagePump" /> class.
    /// </summary>
    ~MessagePump()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>
    ///     Create the message pump for the pop-up calibration barcode window.
    /// </summary>
    /// <param name="bitmapStream">The stream holding the calibration barcode bitmap to be displayed.</param>
    /// <param name="registerPostMessage">
    ///     An action that, when invoked, registers a local action used to post messages to the pop-up window.
    ///     This is used to post user-defined messages to tell the window to destroy itself..
    /// </param>
    /// <param name="sendKey">An action used to simulate keyboard input to the input handler.</param>
    public void CreateMessagePump(
        Stream bitmapStream,
        Action<Action<uint>> registerPostMessage,
        Action<ConsoleKeyInfo> sendKey)
    {
        _hInstance = Process.GetCurrentProcess().Handle;
        const int marginPercent = 10;
        const double scaleFactor = 8D;

        if (_wndClass.hInstance == IntPtr.Zero)
        {
            _wndClass = new WNDCLASSEX
                       {
                           cbSize = Marshal.SizeOf(typeof(WNDCLASSEX)),
                           style = (int)(ClassStyles.HorizontalRedraw | ClassStyles.VerticalRedraw
                                                                      | ClassStyles.DropShadow)
                       };

            // ReSharper disable once IdentifierTypo
            IDisplayCalibrationBarcode barcodeDisplayer = new DisplayCalibrationBarcodeOnWindows();

            WndProc managedWndProc = (hWnd, message, wParam, lParam) =>
            {
                void ProcessCharacterMessage(char character)
                {
                    ProcessStringMessage(Convert.ToString(character));
                }

                // We pass the character as a string in order to handle UNICHAR messages.
                void ProcessStringMessage(string character)
                {
                    if (_κeyDownEnqueuedCharCount > 0)
                    {
                        void RemoveLastΝull()
                        {
                            if (_charQueue.Count == 0)
                            {
                                return;
                            }

                            var recycleCount = _charQueue.Count - 1;

                            for (var idx = 0; idx < recycleCount; idx++)
                            {
                                _charQueue.Enqueue(_charQueue.Dequeue());
                            }

                            if (_charQueue.Peek() == "\0")
                            {
                                _charQueue.Dequeue();
                            }
                            else
                            {
                                _charQueue.Enqueue(_charQueue.Dequeue());
                            }
                        }

                        RemoveLastΝull();

                        _charQueue.Enqueue(character);
                    }
                    else
                    {
                        SendInput(character);
                    }
                }

                void SendInput(string input)
                {
                    foreach (var c in input)
                    {
                        sendKey(
                            new ConsoleKeyInfo(
                                c,
                                input.Length == 1 ? ResolveKey(_isControl ? (char)(c + 64) : c) : 0,
                                _isShift,
                                _isAlt,
                                _isControl));
                    }
                }

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch ((WM)message)
                {
                    case WM.PAINT:
                        WinApi.GetWindowRect(_consoleForegroundWindow, out var lpRect);
                        WinApi.GetClientRect(_consoleForegroundWindow, out var rect);

                        WinApi.MoveWindow(
                            _currentPopupHwnd,
                            lpRect.Left + (rect.Width - Convert.ToInt32(_currentBitmap.Width / scaleFactor)) / 2,
                            lpRect.Top + (rect.Height - Convert.ToInt32(_currentBitmap.Height / scaleFactor)) / 2,
                            Convert.ToInt32(_currentBitmap.Width * (1 + marginPercent / 100D) / scaleFactor),
                            Convert.ToInt32(_currentBitmap.Height * (1 + marginPercent / 100D) / scaleFactor),
                            false);
                        barcodeDisplayer.Display(_currentPopupHwnd, _currentBitmap);
                        return IntPtr.Zero;
                    case WM.USER + 1:
                        WinApi.DestroyWindow(_currentPopupHwnd);
                        return IntPtr.Zero;
                    case WM.DESTROY:
                        WinApi.PostQuitMessage(0);
                        return IntPtr.Zero;
                    case WM.QUIT:
                        _currentBitmap?.Dispose();
                        return
                            1; // The message loop has already been terminated, so this return value is not used.
                    case WM.CHAR:
                    case WM.SYSCHAR:
                        ProcessCharacterMessage((char)(int)wParam);
                        return IntPtr.Zero;
                    case WM.UNICHAR:
                        if (wParam == IdcUnicodeNochar)
                        {
                            return 1;
                        }

                        ProcessStringMessage(char.ConvertFromUtf32((int)wParam));
                        return IntPtr.Zero;
                    case WM.DEADCHAR:
                    case WM.SYSDEADCHAR:
                        return IntPtr.Zero;
                    case WM.KEYDOWN:
                    case WM.SYSKEYDOWN:
                        switch ((int)wParam)
                        {
                            case IdcVkShift:
                            case IdcVkLshift:
                            case IdcVkRshift:
                                _isShift = true;
                                break;
                            case IdcVkControl:
                            case IdcVkLcontrol:
                            case IdcVkRcontrol:
                                _isControl = true;
                                break;
                            case IdcVkMenu:
                            case IdcVkLmenu:
                                _isAlt = true;
                                break;
                            default:
                                // Post a NULL character to the queue as the default
                                _κeyDownEnqueuedCharCount++;
                                _charQueue.Enqueue(Convert.ToString((char)0));
                                break;
                        }

                        return IntPtr.Zero;
                    case WM.KEYUP:
                    case WM.SYSKEYUP:
                        switch ((int)wParam)
                        {
                            case IdcVkShift:
                            case IdcVkLshift:
                            case IdcVkRshift:
                                _isShift = false;
                                break;
                            case IdcVkControl:
                            case IdcVkLcontrol:
                            case IdcVkRcontrol:
                                _isControl = false;
                                break;
                            case IdcVkMenu:
                            case IdcVkLmenu:
                                _isAlt = false;
                                break;
                            default:
                                _κeyDownEnqueuedCharCount--;

                                if (_κeyDownEnqueuedCharCount == 0)
                                {
                                    while (_charQueue.Count > 0)
                                    {
                                        SendInput(_charQueue.Dequeue());
                                    }
                                }

                                break;
                        }

                        return IntPtr.Zero;
                    default:
                        return WinApi.DefWindowProc(hWnd, (WM)message, wParam, lParam);
                }
            };

            _gchManagedWndProc = GCHandle.Alloc(managedWndProc);
            _wndClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(managedWndProc);
            _wndClass.cbClsExtra = 0;
            _wndClass.cbWndExtra = 0;
            _wndClass.hInstance = _hInstance;
            _wndClass.hIcon = WinApi.LoadIcon(IntPtr.Zero, SystemIcons.Application.ToString());
            _wndClass.hCursor = WinApi.LoadCursor(IntPtr.Zero, Win32IdcConstants.IDC_ARROW);
            _wndClass.hbrBackground = WinApi.GetStockObject(StockObjects.WHITE_BRUSH);
            _wndClass.lpszMenuName = null;
            _wndClass.lpszClassName = IdcCalibrationBarcodeWinClassName;
            _classAtom = WinApi.RegisterClassEx(ref _wndClass);
        }

        // Get the bitmap
        using (bitmapStream)
        {
            try
            {
                _currentBitmap = new System.Drawing.Bitmap(bitmapStream);
            }
            catch
            {
                return;
            }
        }

        // Get the client rectangle for the foreground window
        _consoleForegroundWindow = WinApi.GetForegroundWindow();
        WinApi.GetWindowRect(_consoleForegroundWindow, out var windowRect);
        WinApi.GetClientRect(_consoleForegroundWindow, out var clientRect);

        _currentPopupHwnd = WinApi.CreateWindowEx(
            WindowStylesEx.WS_EX_TOPMOST,
            _classAtom,
            "Calibration Barcode",
            WindowStyles.WS_POPUP,
            windowRect.Left + (clientRect.Width - Convert.ToInt32(_currentBitmap.Width / scaleFactor)) / 2,
            windowRect.Top + (clientRect.Height - Convert.ToInt32(_currentBitmap.Height / scaleFactor)) / 2,
            Convert.ToInt32(_currentBitmap.Width * (1 + marginPercent / 100D) / scaleFactor),
            Convert.ToInt32(_currentBitmap.Height * (1 + marginPercent / 100D) / scaleFactor),
            IntPtr.Zero,
            IntPtr.Zero,
            _hInstance,
            IntPtr.Zero);

        if (_currentPopupHwnd == IntPtr.Zero)
        {
            var lastError = Marshal.GetLastWin32Error();
            Trace.WriteLine($"Current popup handle is 0: {new Win32Exception(lastError).Message}");
        }

        registerPostMessage(m => WinApi.PostMessage(_currentPopupHwnd, m, IntPtr.Zero, IntPtr.Zero));

        WinApi.ShowWindow(_currentPopupHwnd, ShowWindowCommands.Normal);
        WinApi.UpdateWindow(_currentPopupHwnd);

        while (WinApi.GetMessage(out var msg, IntPtr.Zero, 0, 0) != 0)
        {
            WinApi.TranslateMessage(ref msg);
            WinApi.DispatchMessage(ref msg);
        }
    }

    /// <summary>
    ///     Disposes the instance of the <see cref="MessagePump" /> class.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Release managed resources.
    /// </summary>
    private static void ReleaseUnmanagedResources()
    {
        if (!WinApi.UnregisterClass(IdcCalibrationBarcodeWinClassName, _hInstance))
        {
            return;
        }

        if (_gchManagedWndProc.IsAllocated)
        {
            _gchManagedWndProc.Free();
        }
    }

    /// <summary>
    ///     Resolve a character to a given console key.
    /// </summary>
    /// <param name="keyChar">The character to resolve</param>
    /// <returns>The console key for the given character.</returns>
    private static ConsoleKey ResolveKey(char keyChar)
    {
        _ = Enum.TryParse<ConsoleKey>(keyChar.ToString().ToUpper(), out var consoleKey);
        return consoleKey;
    }
}