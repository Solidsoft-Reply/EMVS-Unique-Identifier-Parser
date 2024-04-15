// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSG.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// The MSG structure contains message information from a thread's message queue.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// The MSG structure contains message information from a thread's message queue.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 8)]
[SuppressMessage(
    "Microsoft.StyleCop.CSharp.NamingRules",
    "SA1305:FieldNamesMustNotUseHungarianNotation",
    Justification = "Code follows Windows API naming conventions.")]
[SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
    Justification = "Code follows Windows API naming conventions.")]
// ReSharper disable once InconsistentNaming
public struct Msg {
    public IntPtr hwnd;
    public uint message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public Point pt;
}