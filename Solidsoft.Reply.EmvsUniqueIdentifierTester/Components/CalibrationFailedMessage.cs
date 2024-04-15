// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationFailedMessage.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A component displaying an error message if calibration fails.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// A component displaying an error message if calibration fails.
/// </summary>
internal class CalibrationFailedMessage : IComponent {

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine(Resources.DisplayCalibrationFailedMessage_1);
        WriteLine(Resources.DisplayCalibrationFailedMessage_2);
        WriteLine(Resources.DisplayCalibrationFailedMessage_3);
        WriteLine();
    }
}