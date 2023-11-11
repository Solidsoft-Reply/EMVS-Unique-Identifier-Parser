// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleKeyEventArgs.cs" company="Solidsoft Reply Ltd.">
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
// Event arguments for the Console key event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// Event arguments for the Console key event.
/// </summary>
public class ConsoleKeyEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleKeyEventArgs"/> class.
    /// </summary>
    /// <param name="mode">The mode name.</param>
    /// <param name="info"></param>
    public ConsoleKeyEventArgs(string mode, ConsoleKeyInfo info)
    {
        Mode = mode;
        ConsoleKeyInfo = info;
        KeyChar = info.KeyChar;
        Key = info.Key;
        Modifiers = info.Modifiers;
    }

    /// <summary>
    /// Gets the line of text entered in the terminal.
    /// </summary>
    public string Mode { get; }

    /// <summary>
    /// Gets the console key information.
    /// </summary>
    public ConsoleKeyInfo ConsoleKeyInfo { get; }

    /// <summary>
    /// Gets the key character.
    /// </summary>
    public char KeyChar { get; }

    /// <summary>
    /// Gets the console key.
    /// </summary>
    public ConsoleKey Key { get; }

    /// <summary>
    /// Gets the console key modifiers (shift, alt and control).
    /// </summary>
    public ConsoleModifiers Modifiers { get; }
}