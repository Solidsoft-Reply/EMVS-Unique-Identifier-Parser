// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitialisationError.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Displays an initialisation error.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// Displays an initialisation error.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="InitialisationError"/> class.
/// </remarks>
/// <param name="message">The error message</param>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
public class InitialisationError(string message)
    : IComponent {
    /// <summary>
    /// Gets the initialisation error message.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public string Message { get; } = message;

    /// <inheritdoc />
    public void Render() {
        ForegroundColor = ConsoleColor.Red;
        WriteLine();
        WriteLine();
        WriteLine(Message);
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine();
        WriteLine(Resources.Initialise_1);
        WriteLine();
        ReadKey();
    }
}