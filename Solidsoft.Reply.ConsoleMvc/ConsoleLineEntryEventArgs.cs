// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLineEntryArgs.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Event arguments for the Console line entry event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// Event arguments for the Console line entry event.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ConsoleLineEntryEventArgs"/> class.
/// </remarks>
/// <param name="mode">The mode name.</param>
/// <param name="line">The line of text entered into the terminal.</param>
public class ConsoleLineEntryEventArgs(string mode, string line) : EventArgs {

    /// <summary>
    /// Gets the line of text entered into the terminal.
    /// </summary>
    public string Mode { get; } = mode;

    /// <summary>
    /// Gets the line of text entered into the terminal.
    /// </summary>
    public string Line { get; } = line;
}