// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Solidsoft Reply Ltd">
// Copyright (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// </copyright>
// <summary>
// The pack identifier parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using Solidsoft.Reply.BarcodeScanner.Calibration;

using Packs;

using Preprocessor = HighCapacityAidc.Preprocessor;

/// <summary>
///   EMVS Unique Identifier parser.
/// </summary>
public class Parser
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="Parser" /> class.
    /// </summary>
    /// <param name="data">The calibration data.</param>
    public Parser(
        Data data)
    {
        Calibrator = new Calibrator(data);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Parser" /> class.
    /// </summary>
    /// <param name="data">The calibration data.</param>
    /// <param name="assumption">The assumption made concerning the use of calibration in client systems.</param>
    public Parser(
        Data? data = null,
        Assumption assumption = Assumption.Calibration)
    {
        Calibrator = new Calibrator(data, assumption);
    }

    /// <summary>
    ///   Gets the keyboard calibrator for this instance of the parser.
    /// </summary>
    public Calibrator Calibrator { get; }

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <param name="preProcessedData">The pre-processed barcode data.</param>
    /// <param name="preProcessors">The pre-processor functions, provided as a delegate.</param>
    /// <param name="trace">Indicates whether the parser should trace the data it receives. This supports debugging.</param>
    /// <returns>A pack identifier.</returns>
    public IPackIdentifier Parse(string? data, out string preProcessedData, Preprocessor? preProcessors = null, bool trace = false)
    {
        ////data =
        ////    "[{\"code\":\"Digit9\",\"modifiers\":54},{\"code\":\"KeyD\",\"modifiers\":49},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit5\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit7\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit3\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit5\",\"modifiers\":48},{\"code\":\"Digit5\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit6\",\"modifiers\":48},{\"code\":\"Digit4\",\"modifiers\":48},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit7\",\"modifiers\":48},{\"code\":\"Digit7\",\"modifiers\":48},{\"code\":\"Digit3\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"KeyA\",\"modifiers\":48},{\"code\":\"KeyK\",\"modifiers\":48},{\"code\":\"KeyV\",\"modifiers\":48},{\"code\":\"KeyA\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit7\",\"modifiers\":48},{\"code\":\"BracketRight\",\"modifiers\":50},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit7\",\"modifiers\":48},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit5\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit9\",\"modifiers\":48},{\"code\":\"Digit3\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit1\",\"modifiers\":48},{\"code\":\"Digit0\",\"modifiers\":48},{\"code\":\"Digit2\",\"modifiers\":48},{\"code\":\"Digit4\",\"modifiers\":48},{\"code\":\"Digit5\",\"modifiers\":48},{\"code\":\"Digit4\",\"modifiers\":48},{\"code\":\"Digit8\",\"modifiers\":48},{\"code\":\"Enter\",\"modifiers\":48}]";

        ////preProcessors = Dom3KeyboardEventCodes.ConvertCodesToString;
        if (trace) {
            try {
                Console.WriteLine(data?.ToControlPictures());
            }
            catch {
                // Do nothing here
            }

            try {
                Trace.TraceInformation(data?.ToControlPictures());
            }
            catch {
                // Do nothing here
            }
        }

        var calibrationProcessor = Calibrator.IsProcessingRequired
            ? Calibrator.ProcessInput
            : default(Preprocessor);

        var packIdentifier =  BaseParser.Parse(data, out preProcessedData, calibrationProcessor, preProcessors);

        if (!trace
            || string.IsNullOrEmpty(preProcessedData)
            || string.IsNullOrEmpty(data)
            || preProcessedData == data) return packIdentifier;

        try {
            Console.WriteLine(preProcessedData.ToControlPictures());
        }
        catch {
            // Do nothing here
        }

        try {
            Trace.TraceInformation(preProcessedData.ToControlPictures());
        }
        catch {
            // Do nothing here
        }

        return packIdentifier;
    }

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <param name="trace">Indicates whether the parser should trace the data it receives. This supports debugging.</param>
    /// <returns>A pack identifier.</returns>
    // ReSharper disable once UnusedMember.Global
    public IPackIdentifier Parse(string data, bool trace = false)
    {
        return Parse(data, out _, trace: trace);
    }
}