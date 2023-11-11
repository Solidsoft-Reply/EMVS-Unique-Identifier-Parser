// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Menu.cs" company="Solidsoft Reply Ltd.">
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
// Renders the main menu on the screen.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable CA1416
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using ConsoleMvc;
using Views;

using static System.Console;

/// <summary>
/// Renders the main menu on the screen.
/// </summary>
public class Menu : IComponent
{
    /// <inheritdoc />
    public void Render()
    {
        Utilities.ClearConsoleToDefault();
        // ReSharper disable once LocalizableElement
        Write("\u001b[2r");

        ResetColor();
        BackgroundColor = ConsoleColor.Cyan;
        ForegroundColor = ConsoleColor.DarkBlue;

        var menuTextLength = 0;
        const string menuItemAutoSubmit = "\u001b[106m\u001b[35m[^F6]\u001b[34m\u001b[107m Auto-submit |";
        menuTextLength += 20;
        const string menuItemCalibrateGs1Only = "\u001b[106m\u001b[35m[^F11]\u001b[34m\u001b[107m Calibrate (GS1) |";
        menuTextLength += 25;
        const string menuItemCalibrate = "\u001b[106m\u001b[35m[Shift+^F11]\u001b[34m\u001b[107m Calibrate (full) |";
        menuTextLength += 32;
        const string menuItemClearScreen = "\u001b[106m\u001b[35m[^F12]\u001b[34m\u001b[107m Clear Screen |";
        menuTextLength += 22;
        const string menuItemExit = "\u001b[106m\u001b[35m[^X]\u001b[34m\u001b[107m Exit";
        menuTextLength += 12;

        if (menuTextLength >= BufferWidth)
        {
            BufferWidth = menuTextLength + 1;
        }

        var padding = new string(' ', BufferWidth - menuTextLength);
        var menuText =
            $"\u001b[107m {menuItemAutoSubmit} {menuItemCalibrateGs1Only} {menuItemCalibrate} {menuItemClearScreen} {menuItemExit}{padding}";

        WriteLine(menuText);
        ResetColor();
        WriteLine();
        CursorLeft = 0;
    }
}