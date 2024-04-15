// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomaticEntryStatus.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A component displaying the automatic entry status of the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Solidsoft.Reply.EmvsUniqueIdentifierTester.Properties;

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using ConsoleMvc;

using static System.Console;


/// <summary>
/// A component displaying the automatic entry status of the application.
/// </summary>
internal class AutomaticEntryStatus : IComponent {

    /// <summary>
    /// Gets or sets a value indicating whether data will be input automatically
    /// without the need to press the RETURN button.
    /// </summary>
    public bool AutomaticEntry { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        WriteLine();
        var text1 = AutomaticEntry ? Resources.AutomaticEntryInstructions1 : Resources.AutomaticEntryInstructions1Not;
        var text2 = AutomaticEntry ? string.Empty : Resources.AutomaticEntryInstructions2;
        ResetColor();
        WriteLine($@"{text1}{text2}");
    }
}