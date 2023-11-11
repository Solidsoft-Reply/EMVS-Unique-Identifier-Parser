// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <license>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </license>
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