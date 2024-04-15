// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComponent.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents a component.  Each component displays output on the screen.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

/// <summary>
/// Represents a component.  Each component displays output on the screen.
/// </summary>
public interface IComponent {
    /// <summary>
    /// Render the component on the screen.
    /// </summary>
    public void Render();
}