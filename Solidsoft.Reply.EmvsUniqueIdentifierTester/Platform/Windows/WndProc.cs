// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WndProc.cs" company="Solidsoft Reply Ltd.">
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