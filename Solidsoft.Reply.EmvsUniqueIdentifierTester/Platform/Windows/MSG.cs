// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSG.cs" company="Solidsoft Reply Ltd.">
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
public struct MSG
{
    public IntPtr hwnd;
    public uint message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public POINT pt;
}