// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Displays the full list of options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// Displays the full list of options.
/// </summary>
/// <remarks>
/// This is included for legacy purposes and is only used on older
/// versions of Windows that do not support VT sequences.
/// </remarks>
public class Options : IComponent {
    /// <inheritdoc />
    public void Render() {
        ResetColor();
        ForegroundColor = ConsoleColor.Yellow;

        WriteLine($@" {Resources.Options}" + new string(' ', BufferWidth - 9));
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine(Resources.DisplayMainOptions_1);
        WriteLine(Resources.DisplayMainOptions_2);
        WriteLine(Resources.DisplayMainOptions_3);
        WriteLine(Resources.DisplayMainOptions_4);
        WriteLine(Resources.DisplayMainOptions_5);
        WriteLine(Resources.DisplayMainOptions_6);
        WriteLine(Resources.DisplayMainOptions_7);
        WriteLine(Resources.DisplayMainOptions_8);
        ResetColor();
        WriteLine();
    }
}