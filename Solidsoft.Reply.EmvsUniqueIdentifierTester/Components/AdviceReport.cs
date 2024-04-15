// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdviceReport.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
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