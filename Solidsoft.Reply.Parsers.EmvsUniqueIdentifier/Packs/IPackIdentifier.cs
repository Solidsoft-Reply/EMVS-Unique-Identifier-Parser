// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackIdentifier.cs" company="Solidsoft Reply Ltd">
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
// Represents an EMVS pack identifier.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable UnusedMemberInSuper.Global
namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System.Collections.Generic;

/// <summary>
///   Represents an EMVS pack identifier.
/// </summary>
public interface IPackIdentifier {
    /// <summary>
    ///   Gets the batch identifier.
    /// </summary>
    string BatchIdentifier { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    IEnumerable<PackIdentifierException> Exceptions { get; }

    /// <summary>
    ///   Gets the expiry date.
    /// </summary>
    string Expiry { get; }

    /// <summary>
    ///   Gets the indicator digit for trade item groupings. This value is between 1 and 8, or 9
    ///   for a variable measure trade item.
    /// </summary>
    int Indicator { get; }

    /// <summary>
    ///   Gets a value indicating whether the product code represents a trade item grouping.
    /// </summary>
    bool IsTradeItemGrouping { get; }

    /// <summary>
    ///   Gets a value indicating whether the pack identifier is valid.
    /// </summary>
    /// <remarks>
    ///   Validity indicates if the barcode was parsed successfully without exceptions and
    ///   contains at least all four required data elements of a unique identifier.
    /// </remarks>
    bool IsValid { get; }

    /// <summary>
    ///   Gets a value indicating whether the product code represents a variable measure trade item.
    /// </summary>
    bool IsVariableMeasureTradeItem { get; }

    /// <summary>
    ///   Gets the GS1-recognised NHRN national numbers.
    /// </summary>
    IReadOnlyDictionary<NhrnMarket, string> NationalNumbers { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    IEnumerable<ParseException> ParseExceptions { get; }

    /// <summary>
    ///   Gets the product code.
    /// </summary>
    string ProductCode { get; }

    /// <summary>
    ///   Gets a collection of records in the barcode.
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    IEnumerable<IRecord> Records { get; }

    /// <summary>
    ///   Gets the encoding scheme (GS1 or IFA).
    /// </summary>
    Scheme Scheme { get; }

    /// <summary>
    ///   Gets the serial number.
    /// </summary>
    string SerialNumber { get; }

    /// <summary>
    ///   Gets a value indicating whether the pack identifier is suitable for submission to a national
    ///   medicines verification system for verification, supply, decommission or reactivation.
    /// </summary>
    bool Submit { get; }

    /// <summary>
    ///   Gets the legal validity of the use of the symbology to encode a unique pack identifier.
    /// </summary>
    /// <remarks>
    ///   By law, unique identifiers must be encoded as Data Matrix barcodes with error
    ///   detection and correction equivalent to or higher than those of the Data Matrix
    ///   ECC200.
    /// </remarks>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    SymbologyValidity ValidSymbology { get; }

    /// <summary>
    ///   Gets the raw data containing the pack identifier.
    /// </summary>
    /// <remarks>
    ///   The raw data is provided, even if the parser cannot
    ///   resolve the data into a pack identifier.
    /// </remarks>
    string RawData { get; }

    /// <summary>
    ///   Adds a pack identifier exception to the exception collection.
    /// </summary>
    /// <param name="exception">The exception to be added.</param>
    void AddException(PackIdentifierException exception);

    /// <summary>
    ///   Adds a GS1-recognised NHRN national number to the national number collection.
    /// </summary>
    /// <remarks>Any other national numbers will be found in the Elements collection of a record.</remarks>
    /// <param name="market">The market that defines the national number.</param>
    /// <param name="nationalNumber">The NHRN national number to be added.</param>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    void AddNationalNumber(NhrnMarket market, string nationalNumber);

    /// <summary>
    ///   Adds a barcode record to the record collection.
    /// </summary>
    /// <param name="record">The barcode record to be added.</param>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    void AddRecord(IRecord record);

    /// <summary>
    ///   Resets the pack identifier fields, but leaves the records collection unchanged.
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    void ResetIdentifier();
}