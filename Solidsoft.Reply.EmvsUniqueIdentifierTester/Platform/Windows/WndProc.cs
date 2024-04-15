// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WndProc.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// An application-defined function that processes messages sent to a window.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;

/// <summary>
/// An application-defined function that processes messages sent to a window.
/// </summary>
/// <param name="hWnd">A handle to the window.</param>
/// <param name="msg">The message.</param>
/// <param name="wParam">
/// Additional message information. The contents of this parameter depend on
/// the value of the msg parameter.
/// </param>
/// <param name="lParam">
/// Second additional message information. The contents of this parameter depend on
/// the value of the msg parameter.
/// </param>
/// <returns>
/// The return value is the result of the message processing and depends on
/// the message sent.
/// </returns>
public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);