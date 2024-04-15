// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandPrompt.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A component displaying the command prompt.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// A component displaying the command prompt.
/// </summary>
internal class CommandPrompt : IComponent {
    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        // Write the prompt or content.
        ResetColor();

        var originalForegroundColour = ForegroundColor;
        ForegroundColor = ConsoleColor.Cyan;
        Write(Resources.CommandPrompt);
        ForegroundColor = originalForegroundColour;
    }
}