// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationReport.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
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
    public IList<Information> CalibrationInformation { get; set; }

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
                InformationLevel.Error => ConsoleColor.Red,
                InformationLevel.Warning => ConsoleColor.DarkYellow,
                InformationLevel.Information => ConsoleColor.Green,
                _ => ForegroundColor
            };

            var levelName = information.Level switch {
                InformationLevel.Warning => Resources.Warning,
                InformationLevel.Error => Resources.Error,
                _ => Resources.Information,
            };

            WriteLine($@" {levelName}: {information.Description}");
        }

        var testInformation = IncludeFormat06 ? Resources.IncludePpn : Resources.DidNotIncludePpn;
        ForegroundColor = ConsoleColor.Green;
        WriteLine(" " + Resources.InformationForPpn, Resources.Information, testInformation);
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine();
    }
}