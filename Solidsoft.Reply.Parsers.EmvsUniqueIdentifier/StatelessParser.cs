// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatelessParser.cs" company="Solidsoft Reply Ltd">
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
// The stateless pack identifier parser.
// Supports a stateless model suitable for client/server scenarios where no session
// state is maintained across multiple calls into the server.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using BarcodeScanner.Calibration;
using Preprocessor = HighCapacityAidc.Preprocessor;

using Packs;

/// <summary>
/// The stateless pack identifier parser.
/// </summary>
/// <remarks>
///   Supports a stateless model suitable for client/server scenarios where no session
///   state is maintained across multiple calls into the server.
/// </remarks>
public class StatelessParser {
    /// <summary>
    ///   Initializes a new instance of the <see cref="StatelessParser" /> class.
    /// </summary>
    /// <param name="data">The calibration data.</param>
    public StatelessParser(
        Data data) {
        Calibrator = new StatelessCalibrator(data);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatelessParser" /> class.
    /// </summary>
    /// <param name="data">The calibration data.</param>
    /// <param name="assumption">The assumption made concerning the use of calibration in client systems.</param>
    public StatelessParser(
        Data? data = null,
        Assumption assumption = Assumption.Calibration) {
        Calibrator = new StatelessCalibrator(data, assumption);
    }

    /// <summary>
    ///   Gets the keyboard calibrator for this instance of the parser.
    /// </summary>
    public StatelessCalibrator Calibrator { get; }

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <param name="preProcessedData">The pre-processed barcode data.</param>
    /// <param name="preProcessors">The pre-processor functions, provided as a delegate.</param>
    /// <returns>A pack identifier.</returns>
    public IPackIdentifier Parse(string? data, out string preProcessedData, Preprocessor? preProcessors = null) {
        var calibrationProcessor = Calibrator.IsProcessingRequired
            ? Calibrator.ProcessInput
            : default(Preprocessor);

        return BaseParser.Parse(data, out preProcessedData, calibrationProcessor, preProcessors);
    }

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <returns>A pack identifier.</returns>
    // ReSharper disable once UnusedMember.Global
    public IPackIdentifier Parse(string data) {
        return Parse(data, out _);
    }
}