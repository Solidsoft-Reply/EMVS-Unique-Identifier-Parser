// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationBackground.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
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
// A component displaying the background for calibration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using ConsoleMvc;

using Views;

using static System.Console;

/// <summary>
/// A component displaying the background for calibration.
/// </summary>
internal class CalibrationBackground : IComponent {
    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Render() {
        BackgroundColor = ConsoleColor.White;
        Utilities.ClearConsole();
    }
}