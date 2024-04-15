// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputHandler.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents an input handler for console applications. 
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable UnusedMemberInSuper.Global
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// Represents an input handler for console applications. 
/// </summary>
public interface IInputHandler : IDisposable {
    /// <summary>
    /// Represents a key press event.
    /// </summary>
    public event EventHandler<ConsoleKeyEventArgs> KeyPress;

    /// <summary>
    /// Represents entering of a line.
    /// </summary>
    public event EventHandler<ConsoleLineEntryEventArgs> LineEntry;

    /// <summary>
    /// Gets or sets a value indicating whether lines of text that are not terminated by a
    /// carriage return should be entered automatically.
    /// </summary>
    public bool AutomaticEntry { get; set; }

    /// <summary>
    /// Gets or sets the time interval between checks to see if there is data ready to be automatically entered, in milliseconds.
    /// </summary>
    public int AutomaticEntryPeriod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the console input is visible.  When visible, the
    /// input is echoed on the terminal screen. 
    /// </summary>
    public Visibility Visibility { get; set; }

    /// <summary>
    /// Send a key to the input for the given character.  This simulates a key press.
    /// </summary>
    /// <param name="keyChar">The character.  Note that this is case-insensitive.
    /// You must provide a modifier to shift the character. </param>
    /// <param name="modifiers">The modifier flags.</param>
    public void Send(char keyChar, ConsoleModifiers modifiers = 0);

    /// <summary>
    /// Send a key to the input.  This simulates a key press.
    /// </summary>
    /// <param name="keyInfo">The key information.</param>
    public void SendKey(ConsoleKeyInfo keyInfo);

    /// <summary>
    /// Resets the input handler.
    /// </summary>
    public void Reset();
}