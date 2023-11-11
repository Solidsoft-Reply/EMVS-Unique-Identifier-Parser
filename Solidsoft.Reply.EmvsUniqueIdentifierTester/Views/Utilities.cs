// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Solidsoft Reply Ltd.">
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
// Utility functions for views.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Views;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Properties;

using static System.Console;

/// <summary>
/// Utility functions for views.
/// </summary>
public static class Utilities
{
    /// <summary>
    /// Clear the console safely.
    /// </summary>
    public static void ClearConsole()
    {
        try
        {
            Clear();
        }
        catch (IOException)
        {
            WriteLine(Resources.ClearConsoleBuffer);
        }
    }

    /// <summary>
    /// Display the default colours and clear the console.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void ClearConsoleToDefault()
    {
        ResetColor();
        ClearConsole();
    }
}