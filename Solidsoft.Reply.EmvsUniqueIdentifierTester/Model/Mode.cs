// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mode.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.
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
// The current application mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Model;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// The current application mode.
/// </summary>
public enum Mode
{
    /// <summary>
    /// Initialising the application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    Initialising,

    /// <summary>
    /// Scanning barcodes.
    /// </summary>
    Scanning,

    /// <summary>
    /// Calibrating the keyboard layouts.
    /// </summary>
    Calibrating,

    /// <summary>
    /// Exiting the application.
    /// </summary>
    Exiting
}