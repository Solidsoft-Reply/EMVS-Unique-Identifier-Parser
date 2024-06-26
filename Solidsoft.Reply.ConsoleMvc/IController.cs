﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IController.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
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
public interface IController {
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