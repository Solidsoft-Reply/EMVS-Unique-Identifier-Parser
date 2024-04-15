// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsClipboard.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Copies text to the Windows clipboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

/// <summary>
/// Copies text to the Windows clipboard.
/// </summary>
[SuppressMessage(category: "Microsoft.StyleCop.CSharp.NamingRules", checkId: "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Code follows Windows API naming conventions.")]
public static class WindowsClipboard {
    /// <summary>
    /// Data format used for text.
    /// </summary>
    private const uint CfUnicodeText = 13;

    /// <summary>
    /// Copies text to the Windows clipboard.
    /// </summary>
    /// <param name="text">The text to be copied.</param>
    public static void SetText(string text) {
        OpenClipboard();

        EmptyClipboard();
        IntPtr hGlobal = default;

        try {
            var bytes = (text.Length + 1) * 2;
            hGlobal = Marshal.AllocHGlobal(bytes);

            if (hGlobal == default) {
                ThrowWin32();
            }

            var target = GlobalLock(hGlobal);

            if (target == default) {
                ThrowWin32();
            }

            try {
                Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
            }
            finally {
                GlobalUnlock(target);
            }

            if (SetClipboardData(CfUnicodeText, hGlobal) == default) {
                ThrowWin32();
            }

            hGlobal = default;
        }
        finally {
            if (hGlobal != default) {
                Marshal.FreeHGlobal(hGlobal);
            }

            CloseClipboard();
        }
    }

    /// <summary>
    /// Opens the clipboard for examination and prevents other applications from modifying the clipboard content.
    /// </summary>
    public static void OpenClipboard() {
        var num = 10;

        while (true) {
            if (OpenClipboard(default)) {
                break;
            }

#pragma warning disable S2583
            if (--num == 0)
#pragma warning restore S2583
            {
                ThrowWin32();
            }

            Thread.Sleep(100);
        }
    }

    /// <summary>
    /// Throws a Win32 exception.
    /// </summary>
    private static void ThrowWin32() {
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    /// <summary>
    /// Locks a global memory object and returns a pointer to the first byte of the object's memory block.
    /// </summary>
    /// <param name="hMem">
    /// A handle to the global memory object. This handle is returned by either the GlobalAlloc or
    /// GlobalReAlloc function.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is a pointer to the first byte of the memory block.</p>
    /// <p>If the function fails, the return value is NULL.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalLock(IntPtr hMem);

    /// <summary>
    /// Decrements the lock count associated with a memory object that was allocated with GMEM_MOVEABLE.
    /// This function has no effect on memory objects allocated with GMEM_FIXED.
    /// </summary>
    /// <param name="hMem">
    /// A handle to the global memory object. This handle is returned by either the GlobalAlloc or
    /// GlobalReAlloc function.
    /// </param>
    /// <returns>
    /// <p>If the memory object is still locked after decrementing the lock count, the return value is a
    /// nonzero value. If the memory object is unlocked after decrementing the lock count, the function
    /// returns zero and GetLastError returns NO_ERROR.</p>
    /// <p>If the function fails, the return value is zero and GetLastError returns a value other than NO_ERROR.</p>
    /// </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalUnlock(IntPtr hMem);

    /// <summary>
    /// Opens the clipboard for examination and prevents other applications from modifying the clipboard content.
    /// </summary>
    /// <param name="hWndNewOwner">
    /// A handle to the window to be associated with the open clipboard. If this parameter is NULL, the open
    /// clipboard is associated with the current task.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    /// <summary>
    /// Closes the clipboard.
    /// </summary>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseClipboard();

    /// <summary>
    /// Places data on the clipboard in a specified clipboard format. The window must be the current clipboard
    /// owner, and the application must have called the OpenClipboard function. (When responding to the
    /// WM_RENDERFORMAT and WM_RENDERALLFORMATS messages, the clipboard owner must not call OpenClipboard
    /// before calling SetClipboardData.)
    /// </summary>
    /// <param name="uFormat">
    /// The clipboard format. This parameter can be a registered format or any of the standard clipboard
    /// formats. For more information, see Standard Clipboard Formats and Registered Clipboard Formats.
    /// </param>
    /// <param name="data">
    /// <p>A handle to the data in the specified format. This parameter can be NULL, indicating that the window
    /// provides data in the specified clipboard format (renders the format) upon request. If a window delays
    /// rendering, it must process the WM_RENDERFORMAT and WM_RENDERALLFORMATS messages.</p>
    /// <p>If SetClipboardData succeeds, the system owns the object identified by the hMem parameter.The
    /// application may not write to or free the data once ownership has been transferred to the system, but it
    /// can lock and read from the data until the CloseClipboard function is called. (The memory must be
    /// unlocked before the Clipboard is closed.) If the hMem parameter identifies a memory object, the object
    /// must have been allocated using the function with the GMEM_MOVEABLE flag.</p>
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is the handle to the data.</p>
    /// <p>If the function fails, the return value is NULL.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

    /// <summary>
    /// Empties the clipboard and frees handles to data in the clipboard. The function then assigns ownership
    /// of the clipboard to the window that currently has the clipboard open.
    /// </summary>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("user32.dll")]
    private static extern bool EmptyClipboard();
}