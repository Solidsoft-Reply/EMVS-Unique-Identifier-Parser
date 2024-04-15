// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationSaveError.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
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