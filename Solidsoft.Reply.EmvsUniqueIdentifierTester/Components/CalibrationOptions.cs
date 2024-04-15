// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationOptions.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A component displaying calibration options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// A component displaying calibration options.
/// </summary>
internal class CalibrationOptions : IComponent {
    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ForegroundColor = ConsoleColor.DarkBlue;
        WriteLine(@$" {Resources.Options}" + new string(' ', BufferWidth - 9));
        ForegroundColor = ConsoleColor.DarkRed;
        WriteLine(Resources.DisplayCalibrationOptions_1);
        WriteLine(Resources.DisplayCalibrationOptions_2);
    }
}