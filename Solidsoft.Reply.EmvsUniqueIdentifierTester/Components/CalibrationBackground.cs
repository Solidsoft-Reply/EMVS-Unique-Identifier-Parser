// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationBackground.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
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