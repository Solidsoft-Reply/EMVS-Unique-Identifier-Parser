// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WNDCLASSEX.cs" company="Solidsoft Reply Ltd.">
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
// Contains window class information. It is used with the RegisterClassEx
// and GetClassInfoEx functions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InconsistentNaming

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Contains window class information. It is used with the RegisterClassEx
/// and GetClassInfoEx functions.
/// </summary>
// ReSharper disable once InconsistentNaming
[SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Code follows Windows API naming conventions.")]
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Code follows Windows API naming conventions.")]
public struct WNDCLASSEX
{
    /// <summary>
    /// The size, in bytes, of this structure. Set this member to sizeof(WNDCLASSEX).
    /// Be sure to set this member before calling the GetClassInfoEx function.
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    public int cbSize;

    /// <summary>
    /// The class style(s). This member can be any combination of the Class Styles.
    /// </summary>
    [MarshalAs(UnmanagedType.U4)]
    public int style;

    /// <summary>
    /// A pointer to the window procedure. You must use the CallWindowProc function
    /// to call the window procedure. For more information, see WindowProc.
    /// </summary>
    public IntPtr lpfnWndProc; // not WndProc -- careful

    /// <summary>
    /// The number of extra bytes to allocate following the window-class structure.
    /// The system initializes the bytes to zero.
    /// </summary>
    public int cbClsExtra;

    /// <summary>
    /// The number of extra bytes to allocate following the window instance. The
    /// system initializes the bytes to zero. If an application uses WNDCLASSEX
    /// to register a dialog box created by using the CLASS directive in the
    /// resource file, it must set this member to DLGWINDOWEXTRA.
    /// </summary>
    public int cbWndExtra;

    /// <summary>
    /// A handle to the instance that contains the window procedure for the class.
    /// </summary>
    public IntPtr hInstance;

    /// <summary>
    /// A handle to the class icon. This member must be a handle to an icon resource.
    /// If this member is NULL, the system provides a default icon.
    /// </summary>
    public IntPtr hIcon;

    /// <summary>
    /// A handle to the class cursor. This member must be a handle to a cursor
    /// resource. If this member is NULL, an application must explicitly set
    /// the cursor shape whenever the mouse moves into the application's window.
    /// </summary>
    public IntPtr hCursor;

    /// <summary>
    /// <p>A handle to the class background brush. This member can be a handle to the
    /// brush to be used for painting the background, or it can be a color value.
    /// <p>A color value must be one of the following standard system colors (the
    /// value 1 must be added to the chosen color). If a color value is given, you
    /// must convert it to an HBRUSH type.</p>
    /// The system automatically deletes class background brushes when the class
    /// is unregistered by using UnregisterClass. An application should not delete
    /// these brushes.</p>
    /// <p>When this member is NULL, an application must paint its own background
    /// whenever it is requested to paint in its client area.To determine whether
    /// the background must be painted, an application can either process the
    /// WM_ERASEBKGND message or test the fErase member of the PAINTSTRUCT
    /// structure filled by the BeginPaint function.</p>
    /// </summary>
    public IntPtr hbrBackground;

    /// <summary>
    /// Pointer to a null-terminated character string that specifies the resource
    /// name of the class menu, as the name appears in the resource file. If you
    /// use an integer to identify the menu, use the MAKEINTRESOURCE macro. If this
    /// member is NULL, windows belonging to this class have no default menu.
    /// </summary>
    public string lpszMenuName;

    /// <summary>
    /// <p>A pointer to a null-terminated string or is an atom. If this parameter is
    /// an atom, it must be a class atom created by a previous call to the
    /// RegisterClass or RegisterClassEx function. The atom must be in the low-order
    /// word of lpszClassName; the high-order word must be zero.</p>
    /// <p>If lpszClassName is a string, it specifies the window class name. The class
    /// name can be any name registered with RegisterClass or RegisterClassEx, or
    /// any of the predefined control-class names.</p>
    /// <p>The maximum length for lpszClassName is 256. If lpszClassName is greater
    /// than the maximum length, the RegisterClassEx function will fail.</p>
    /// </summary>
    // ReSharper disable once StyleCop.SA1650
    public string lpszClassName;

    /// <summary>
    /// A handle to a small icon that is associated with the window class. If this
    /// member is NULL, the system searches the icon resource specified by the hIcon
    /// member for an icon of the appropriate size to use as the small icon.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
#pragma warning disable S1104
    public IntPtr hIconSm;
#pragma warning restore S1104
}