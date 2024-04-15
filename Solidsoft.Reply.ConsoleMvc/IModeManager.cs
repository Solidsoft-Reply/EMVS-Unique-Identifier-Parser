// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModeManager.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents a mode manager used in association with the modal input handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

/// <summary>
/// Represents a mode manager used in association with the modal input handler.
/// </summary>
/// <remarks>
/// A mode manager manages different application modes.  An implementation will
/// generally select a different controller to handle each mode.
/// </remarks>
public interface IModeManager {
    /// <summary>
    /// Gets the name of the current mode.
    /// </summary>
    public string ModeName { get; }
}