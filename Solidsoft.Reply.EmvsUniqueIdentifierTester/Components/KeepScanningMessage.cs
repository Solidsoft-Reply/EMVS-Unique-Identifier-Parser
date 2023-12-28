// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeepScanningMessage.cs" company="Solidsoft Reply Ltd.">
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
// A component displaying the Keep Scanning message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using ConsoleMvc;

using Properties;
using Views;

using static System.Console;


/// <summary>
/// A component displaying the Keep Scanning message.
/// </summary>
internal class KeepScanningMessage : IComponent {

    /// <summary>
    /// Gets or sets the number of remaining calibration barcodes to display.
    /// </summary>
    public int RemainingBarcodeCount { get; set; } = -1;

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ForegroundColor = ConsoleColor.Black;
        Utilities.ClearConsole();
        WriteLine();
        ForegroundColor = ConsoleColor.DarkBlue;
        WriteLine(Resources.DisplayKeepScanning_1);
        ForegroundColor = ConsoleColor.DarkRed;
        WriteLine();

        if (RemainingBarcodeCount < 0) {
            return;
        }

        var remaining = ++RemainingBarcodeCount;
        var barcodeText = remaining == 1 ? Resources.BarcodeStillToScan : Resources.BarcodesStillToScan;

        WriteLine($@" {remaining} {barcodeText}");
    }
}