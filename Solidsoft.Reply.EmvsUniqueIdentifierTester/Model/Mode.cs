// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mode.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// The current application mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Model;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// The current application mode.
/// </summary>
public enum Mode {
    /// <summary>
    /// Initialising the application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    Initialising,

    /// <summary>
    /// Scanning barcodes.
    /// </summary>
    Scanning,

    /// <summary>
    /// Calibrating the keyboard layouts.
    /// </summary>
    Calibrating,

    /// <summary>
    /// Exiting the application.
    /// </summary>
    Exiting
}