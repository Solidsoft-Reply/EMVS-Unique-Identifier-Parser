// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32_IDC_Constants.cs" company="Solidsoft Reply Ltd.">
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
// Cursor enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Cursor enumeration.
/// </summary>
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Code follows Windows API naming conventions.")]
public static class Win32IdcConstants
{
    /// <summary>
    /// The cursor resource identifier in the low-order word and zero in the high-order word.
    /// The MAKEINTRESOURCE macro can also be used to create this value.
    /// </summary>
    public const int
        IDC_ARROW = 32512,       // Standard arrow
        IDC_IBEAM = 32513,       // I-Beam
        IDC_WAIT = 32514,        // Hourglass
        IDC_CROSS = 32515,       // Crosshair
        IDC_UPARROW = 32516,     // Vertical arrow
        IDC_SIZE = 32640,        // Obsolete for applications marked version 4.0 or later. Use IDC_SIZEALL.
        IDC_ICON = 32641,        // Obsolete for applications marked version 4.0 or later.
        IDC_SIZENWSE = 32642,    // Double-pointed arrow pointing northwest and southeast.
        IDC_SIZENESW = 32643,    // Double-pointed arrow pointing northeast and southwest.
        IDC_SIZEWE = 32644,      // Double-pointed arrow pointing west and east.
        IDC_SIZENS = 32645,      // Double-pointed arrow pointing north and south.
        IDC_SIZEALL = 32646,     // Four-pointed arrow pointing north, south, east, and west.
        IDC_NO = 32648,          // Slashed circle
        IDC_HAND = 32649,        // Hand
        IDC_APPSTARTING = 32650, // Standard arrow and small hourglass
        IDC_HELP = 32651;        // Arrow and question mark
}