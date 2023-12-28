// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinApi.cs" company="Solidsoft Reply Ltd.">
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
//  Windows API bindings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Windows API bindings.
/// </summary>
[SuppressMessage(category: "Microsoft.StyleCop.CSharp.NamingRules", checkId: "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Code follows Windows API naming conventions.")]
public static class WinApi
{
    // Message pump with C# and p/invoke  
    // please check out   
    //   http://msdn.microsoft.com/en-us/library/windows/desktop/ms644928(v=vs.85).aspx  
    //  
    // General Reference Page:  
    //    pinvoke.net : http://www.pinvoke.net  
    //    Howto: Marshal Structures Using PInvoke : http://msdn.microsoft.com/en-us/library/ef4c3t39(v=vs.80).aspx  
    //    using P/Invoke to call Unmanaged APIs from your Managed Classes: http://msdn.microsoft.com/en-us/library/aa719104(v=vs.10).aspx  
    [DllImport("user32.dll", EntryPoint = "DispatchMessageW")]
    internal static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

    [DllImport("user32.dll")]
    internal static extern bool TranslateMessage([In] ref MSG lpMsg);

    [DllImport("user32.dll", EntryPoint = "GetMessageW")]
    internal static extern sbyte GetMessage(
        out MSG lpMsg, 
        IntPtr hWnd, 
        uint wMsgFilterMin, 
        uint wMsgFilterMax);

    // Create a window, but accept an atom value.  
    [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowExW")]
    internal static extern IntPtr CreateWindowEx(
        WindowStylesEx dwExStyle,
        ushort lpClassName,
        string lpWindowName,
        WindowStyles dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
    internal static extern IntPtr DefWindowProc(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    internal static extern void PostQuitMessage(int nExitCode);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

    [DllImport("gdi32.dll")]
    internal static extern IntPtr GetStockObject(StockObjects fnObject);

    [DllImport("user32.dll")]
    internal static extern bool UpdateWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    internal static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassExW")]
    internal static extern ushort RegisterClassEx([In] ref WNDCLASSEX lpwcx);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "PostMessageW")]
    internal static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// <para>The DestroyWindow function destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).</para>
    /// <para>If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.</para>
    /// <para>DestroyWindow also destroys modeless dialog boxes created by the CreateDialog function.</para>
    /// </summary>
    /// <param name="hwnd">Handle to the window to be destroyed.</param>
    /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DestroyWindow(IntPtr hwnd);

    /// <summary>
    /// Retrieves a handle to the foreground window (the window with which the user is
    /// currently working). The system assigns a slightly higher priority to the thread
    /// that creates the foreground window than it does to other threads.
    /// </summary>
    /// <returns>
    /// The return value is a handle to the foreground window. The foreground window
    /// can be NULL in certain circumstances, such as when a window is losing activation.
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    /// <summary>
    ///     The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the
    ///     position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative
    ///     to the upper-left corner of the parent window's client area.
    ///     <para>
    ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534%28v=vs.85%29.aspx for more
    ///     information
    ///     </para>
    /// </summary>
    /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br /> Handle to the window.</param>
    /// <param name="x">C++ ( X [in]. Type: int )<br />Specifies the new position of the left side of the window.</param>
    /// <param name="y">C++ ( Y [in]. Type: int )<br /> Specifies the new position of the top of the window.</param>
    /// <param name="nWidth">C++ ( nWidth [in]. Type: int )<br />Specifies the new width of the window.</param>
    /// <param name="nHeight">C++ ( nHeight [in]. Type: int )<br />Specifies the new height of the window.</param>
    /// <param name="bRepaint">
    ///     C++ ( bRepaint [in]. Type: bool )<br />Specifies whether the window is to be repainted. If this
    ///     parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This
    ///     applies to the client area, the non-client area (including the title bar and scroll bars), and any part of the
    ///     parent window uncovered as a result of moving a child window.
    /// </param>
    /// <returns>
    ///     If the function succeeds, the return value is nonzero.<br /> If the function fails, the return value is zero.
    ///     <br />To get extended error information, call GetLastError.
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    internal static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);
}