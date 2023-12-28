// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomaticEntryStatus.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
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
// A component displaying the automatic entry status of the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Solidsoft.Reply.EmvsUniqueIdentifierTester.Properties;

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using ConsoleMvc;
using static System.Console;


/// <summary>
/// A component displaying the automatic entry status of the application.
/// </summary>
internal class AutomaticEntryStatus : IComponent {

    /// <summary>
    /// Gets or sets a value indicating whether data will be input automatically
    /// without the need to press the RETURN button.
    /// </summary>
    public bool AutomaticEntry { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        WriteLine();
        var text1 = AutomaticEntry ? Resources.AutomaticEntryInstructions1 : Resources.AutomaticEntryInstructions1Not;
        var text2 = AutomaticEntry ? string.Empty : Resources.AutomaticEntryInstructions2;
        ResetColor();
        WriteLine($@"{text1}{text2}");
    }
}