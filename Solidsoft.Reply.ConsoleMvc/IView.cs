// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents a view.  Views are a kind of visual component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.ConsoleMvc;

/// <summary>
/// Represents a view.  Views are a kind of visual component.
/// </summary>
public interface IView : IComponent {
    /// <summary>
    /// Render a component within the view.
    /// </summary>
    /// <param name="component">The name or identifier of the component to run.</param>
    public void Render(string component);

    /// <summary>
    /// Set the value of a view property.
    /// </summary>
    /// <param name="propertyName">The name of the View property.</param>
    /// <param name="value">The value of the View property.</param>
    public void SetValue(string propertyName, object value);
}