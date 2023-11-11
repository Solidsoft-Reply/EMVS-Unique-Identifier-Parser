// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArguments.cs" company="Solidsoft Reply Ltd.">
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
// Represents the command line arguments.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the command line arguments.
/// </summary>
public static class CommandLineArguments
{
    /// <summary>
    /// Gets the code page name.
    /// </summary>
    public static string CodePageName { get; private set; }

    /// <summary>
    /// Gets the code page.
    /// </summary>
    public static int CodePage { get; private set; }

    /// <summary>
    /// Initialises the command line arguments.
    /// </summary>
    /// <param name="args">The command line argument.</param>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static void Initialise(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            return;
        }

        for (var idx = 0; idx < args.Length; idx++)
        {
            var arg = args[idx];
            var kvp = arg.Split(':');

            if (string.IsNullOrWhiteSpace(kvp[0]))
            {
                continue;
            }

            switch (kvp[0].ToLower())
            {
                case "-cp":
                case "/cp":
                case "-codepage":
                case "/codepage":
                    var param = string.IsNullOrWhiteSpace(kvp.Length > 1 ? kvp[1] : null) ? args[++idx] : kvp[1];

                    if ((param.StartsWith('-') || param.StartsWith('/')) && string.IsNullOrWhiteSpace(kvp[1]))
                    {
                        --idx;
                        continue;
                    }

                    if (int.TryParse(param, out var output))
                    {
                        CodePage = output;
                    }
                    else
                    {
                        CodePageName = param;
                    }

                    break;
            }
        }
    }
}