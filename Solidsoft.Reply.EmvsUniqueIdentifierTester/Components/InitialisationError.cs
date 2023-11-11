// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitialisationError.cs" company="Solidsoft Reply Ltd.">
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
// Displays an initialisation error.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// Displays an initialisation error.
/// </summary>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
public class InitialisationError : IComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InitialisationError"/> class.
    /// </summary>
    /// <param name="message">The error message</param>
    public InitialisationError(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Gets the initialisation error message.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public string Message { get; }

    /// <inheritdoc />
    public void Render()
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine();
        WriteLine();
        WriteLine(Message);
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine();
        WriteLine(Resources.Initialise_1);
        WriteLine();
        ReadKey();
    }
}