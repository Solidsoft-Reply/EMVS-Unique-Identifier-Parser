// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Solidsoft Reply Ltd.">
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
// Displays the full list of options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// Displays the full list of options.
/// </summary>
/// <remarks>
/// This is included for legacy purposes and is only used on older
/// versions of Windows that do not support VT sequences.
/// </remarks>
public class Options : IComponent
{
    /// <inheritdoc />
    public void Render()
    {
        ResetColor();
        ForegroundColor = ConsoleColor.Yellow;

        WriteLine($@" {Resources.Options}" + new string(' ', BufferWidth - 9));
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine(Resources.DisplayMainOptions_1);
        WriteLine(Resources.DisplayMainOptions_2);
        WriteLine(Resources.DisplayMainOptions_3);
        WriteLine(Resources.DisplayMainOptions_4);
        WriteLine(Resources.DisplayMainOptions_5);
        WriteLine(Resources.DisplayMainOptions_6);
        WriteLine(Resources.DisplayMainOptions_7);
        WriteLine(Resources.DisplayMainOptions_8);
        ResetColor();
        WriteLine();
    }
}