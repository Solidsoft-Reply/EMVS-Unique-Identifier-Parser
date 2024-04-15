// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleKeyEventArgs.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Event arguments for the Console key event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// Event arguments for the Console key event.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ConsoleKeyEventArgs"/> class.
/// </remarks>
/// <param name="mode">The mode name.</param>
/// <param name="info"></param>
public class ConsoleKeyEventArgs(string mode, ConsoleKeyInfo info) : EventArgs {

    /// <summary>
    /// Gets the line of text entered into the terminal.
    /// </summary>
    public string Mode { get; } = mode;

    /// <summary>
    /// Gets the console key information.
    /// </summary>
    public ConsoleKeyInfo ConsoleKeyInfo { get; } = info;

    /// <summary>
    /// Gets the key character.
    /// </summary>
    public char KeyChar { get; } = info.KeyChar;

    /// <summary>
    /// Gets the console key.
    /// </summary>
    public ConsoleKey Key { get; } = info.Key;

    /// <summary>
    /// Gets the console key modifiers (shift, alt and control).
    /// </summary>
    public ConsoleModifiers Modifiers { get; } = info.Modifiers;
}