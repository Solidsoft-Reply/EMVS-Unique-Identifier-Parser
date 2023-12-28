// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationSaveError.cs" company="Solidsoft Reply Ltd.">
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
// A component displaying an error when saving the calibration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components; 

using System;
using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// A component displaying an error when saving the calibration.
/// </summary>
internal class CalibrationSaveError : IComponent {

    /// <summary>
    /// Gets or sets the error message when an error is encountered while saving calibration data.
    /// </summary>
    public string SaveErrorMessage { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ForegroundColor = ConsoleColor.Red;
        WriteLine();
        WriteLine();
        var saveErrorMessage = string.IsNullOrWhiteSpace(SaveErrorMessage) ? "." : ": " + SaveErrorMessage;
        WriteLine(Resources.SaveError, saveErrorMessage);
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine();
        WriteLine(Resources.DisplayCalibrationSaveError_1);
        WriteLine();
        ReadKey();
    }
}