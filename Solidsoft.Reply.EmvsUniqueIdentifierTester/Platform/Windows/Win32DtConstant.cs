// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32_DT_Constant.cs" company="Solidsoft Reply Ltd.">
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
// DrawText formats.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// DrawText formats.
/// </summary>
[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Code follows Windows API naming conventions.")]
public static class Win32DtConstant
{
    public const int DT_TOP = 0x00000000;

    public const int DT_LEFT = 0x00000000;

    public const int DT_CENTER = 0x00000001;

    public const int DT_RIGHT = 0x00000002;

    public const int DT_VCENTER = 0x00000004;

    public const int DT_BOTTOM = 0x00000008;

    public const int DT_WORDBREAK = 0x00000010;

    public const int DT_SINGLELINE = 0x00000020;

    public const int DT_EXPANDTABS = 0x00000040;

    public const int DT_TABSTOP = 0x00000080;

    public const int DT_NOCLIP = 0x00000100;

    public const int DT_EXTERNALLEADING = 0x00000200;

    public const int DT_CALCRECT = 0x00000400;

    public const int DT_NOPREFIX = 0x00000800;

    public const int DT_INTERNAL = 0x00001000;
}