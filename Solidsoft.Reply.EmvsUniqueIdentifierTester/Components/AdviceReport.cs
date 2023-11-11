// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdviceReport.cs" company="Solidsoft Reply Ltd.">
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
// A component displaying the calibration advice report.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

//////#define statelessTest

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using ConsoleMvc;

using BarcodeScanner.Calibration;
using Properties;
using static System.Console;


/// <summary>
/// A component displaying the calibration advice report.
/// </summary>
internal class AdviceReport : IComponent {
    /// <summary>
    /// An instance of the pack parser.
    /// </summary>
    public SystemCapabilities SystemCapabilities { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        var foregroundColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(Resources.DisplayAdvice_1);
        WriteLine();
#if statelessTest

            var adviceItemToken = systemCapabilities.NextAdviceItem(CalibrationAssumption.Calibration);

            while (adviceItemToken.AdviceItemsRemaining >= 0)
            {
                ForegroundColor = adviceItemToken.AdviceItem.Severity switch
                {
                    ConditionSeverity.High => ConsoleColor.Red,
                    ConditionSeverity.Medium => ConsoleColor.DarkYellow,
                    ConditionSeverity.Low => ConsoleColor.Green,
                    _ => ForegroundColor
                };

                Console.WriteLine($" {adviceItemToken.AdviceItem.Condition}");

                ForegroundColor = foregroundColor;
                if (!string.IsNullOrWhiteSpace(adviceItemToken.AdviceItem.Advice))
                {
                    Console.WriteLine($" {adviceItemToken.AdviceItem.Advice}");
                }
                Console.WriteLine();
                systemCapabilities = SystemCapabilities.CreateSystemCapabilities(adviceItemToken);
                adviceItemToken = systemCapabilities.NextAdviceItem(adviceItemToken);
            }
#else
        var adviceItems = Parsers.EmvsUniqueIdentifier.Advice.CreateAdvice(SystemCapabilities).Items;

        foreach (var adviceItem in adviceItems) {
            ForegroundColor = adviceItem.Severity switch {
                ConditionSeverity.High => ConsoleColor.Red,
                ConditionSeverity.Medium => ConsoleColor.DarkYellow,
                ConditionSeverity.Low => ConsoleColor.Green,
                _ => ForegroundColor
            };

            // ReSharper disable once LocalizableElement
            WriteLine($" {adviceItem.Condition}");

            ForegroundColor = foregroundColor;

            foreach (var advice in adviceItem.Advice) {
                if (!string.IsNullOrWhiteSpace(advice)) {
                    // ReSharper disable once LocalizableElement
                    WriteLine($" {advice}");
                }
            }

            WriteLine();
        }
#endif
    }
}