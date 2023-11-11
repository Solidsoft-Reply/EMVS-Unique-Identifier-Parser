// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModeManager.cs" company="Solidsoft Reply Ltd.">
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
public interface IModeManager
{
    /// <summary>
    /// Gets the name of the current mode.
    /// </summary>
    public string ModeName { get; }
}