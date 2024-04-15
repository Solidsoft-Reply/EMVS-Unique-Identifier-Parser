// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArguments.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents the command line arguments.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the command line arguments.
/// </summary>
public static class CommandLineArguments {
    /// <summary>
    /// Gets the code page name.
    /// </summary>
    public static string CodePageName { get; private set; }

    /// <summary>
    /// Gets the code page.
    /// </summary>
    public static int CodePage { get; private set; }

    /// <summary>
    /// Initialises the command line arguments.
    /// </summary>
    /// <param name="args">The command line argument.</param>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void Initialise(string[] args) {
        if (args == null || args.Length == 0) {
            return;
        }

        for (var idx = 0; idx < args.Length; idx++) {
            var arg = args[idx];
            var kvp = arg.Split(':');

            if (string.IsNullOrWhiteSpace(kvp[0])) {
                continue;
            }

            switch (kvp[0].ToLower()) {
                case "-cp":
                case "/cp":
                case "-codepage":
                case "/codepage":
                    var value = kvp.Length > 1 ? kvp[1] : null;
#pragma warning disable S127 // "for" loop stop conditions should be invariant
                    var param = string.IsNullOrWhiteSpace(value) ? args[++idx] : kvp[1];

                    if ((param.StartsWith('-') || param.StartsWith('/')) && string.IsNullOrWhiteSpace(kvp[1])) {
                        --idx;
#pragma warning restore S127 // "for" loop stop conditions should be invariant
                        continue;
                    }

                    if (int.TryParse(param, out var output)) {
                        CodePage = output;
                    }
                    else {
                        CodePageName = param;
                    }

                    break;
            }
        }
    }
}