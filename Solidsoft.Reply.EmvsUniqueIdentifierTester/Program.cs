// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Solidsoft Reply Ltd.">
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
// The program.  Contains the application entry point and initialisation code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester;

using Model;

using System.Diagnostics.CodeAnalysis;

using Properties;

using static System.Console;
using System.Globalization;
using System.Threading;

/// <summary>
/// The program.  Contains the application entry point and initialisation code.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
public static class Program
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    // ReSharper disable once UnusedParameter.Local
    private static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
        Title = Resources.ConsoleTitle;
        CommandLineArguments.Initialise(args);
        Initialise();
    }

    /// <summary>
    /// Initialise the program.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private static void Initialise()
    {
        // Activate the Scan controller
        new ModeManager(Mode.Scanning).Mode = Mode.Scanning;
    }
}