// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Utility functions for views.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Views;

using System.Diagnostics.CodeAnalysis;
using System.IO;

using Properties;

using static System.Console;

/// <summary>
/// Utility functions for views.
/// </summary>
public static class Utilities {
    /// <summary>
    /// Clear the console safely.
    /// </summary>
    public static void ClearConsole() {
        try {
            Clear();
        }
        catch (IOException) {
            WriteLine(Resources.ClearConsoleBuffer);
        }
    }

    /// <summary>
    /// Display the default colours and clear the console.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void ClearConsoleToDefault() {
        ResetColor();
        ClearConsole();
    }
}