// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// The program.  Contains the application entry point and initialisation code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester;

using Model;

using System.Diagnostics.CodeAnalysis;

using Properties;

using static System.Console;

/// <summary>
/// The program.  Contains the application entry point and initialisation code.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
public static class Program {
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    // ReSharper disable once UnusedParameter.Local
    private static void Main(string[] args) {
        Title = Resources.ConsoleTitle;
        CommandLineArguments.Initialise(args);
        Initialise();
    }

    /// <summary>
    /// Initialise the program.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private static void Initialise() {
        // Activate the Scan controller
        new ModeManager(Mode.Scanning).Mode = Mode.Scanning;
    }
}