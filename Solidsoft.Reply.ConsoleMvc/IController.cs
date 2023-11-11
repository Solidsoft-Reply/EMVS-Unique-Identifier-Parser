// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IController.cs" company="Solidsoft Reply Ltd.">
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
// Represents a controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable UnusedMemberInSuper.Global
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// Represents a controller.
/// </summary>
public interface IController
{
    /// <summary>
    /// Call when the controller is activated.  Controllers are activated
    /// by switching the mode using the mode service.
    /// </summary>
    /// <param name="properties">An optional set to name-value pair view properties.</param>
    /// <returns>
    /// An action for rendering content.  Use this, for example, to set pre-amble content
    /// when activating the controller.
    /// </returns>
    public Action<string> Activate(params Tuple<string, object>[] properties);
}