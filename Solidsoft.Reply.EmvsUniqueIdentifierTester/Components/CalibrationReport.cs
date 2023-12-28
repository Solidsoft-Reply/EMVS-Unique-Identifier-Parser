// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationReport.cs" company="Solidsoft Reply Ltd.">
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
// A component displaying the calibration report.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleMvc;

using Properties;
using BarcodeScanner.Calibration;

using static System.Console;


/// <summary>
/// A component displaying the calibration report.
/// </summary>
internal class CalibrationReport : IComponent {
    /// <summary>
    /// Gets a value indicating whether to include calibration
    /// tests for Format 06, and by implication, Format 05.
    /// </summary>
    public bool IncludeFormat06 { get; set; }

    /// <summary>
    /// Gets a list of reported calibration information.
    /// </summary>
    public IList<CalibrationInformation> CalibrationInformation { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ForegroundColor = ConsoleColor.Yellow;
        CursorLeft = 0;
        WriteLine(Resources.DisplayCalibrationReport_1);
        WriteLine();
        foreach (var information in CalibrationInformation.OrderByDescending(i => i.Level)) {
            ForegroundColor = information.Level switch {
                                  CalibrationInformationLevel.Error       => ConsoleColor.Red,
                                  CalibrationInformationLevel.Warning     => ConsoleColor.DarkYellow,
                                  CalibrationInformationLevel.Information => ConsoleColor.Green,
                                  _                                       => ForegroundColor
                              };

            var levelName = information.Level switch
            {
                CalibrationInformationLevel.Warning => Resources.Warning,
                CalibrationInformationLevel.Error => Resources.Error,
                _ => Resources.Information,
            };
            
            WriteLine($@" {levelName}: {information.Description}");
        }

        var testInformation = IncludeFormat06 ? Resources.IncludePpn : Resources.DidNotIncludePpn;
        ForegroundColor = ConsoleColor.Green;
        WriteLine(Resources.InformationForPpn, Resources.Information, testInformation);
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine();
    }
}