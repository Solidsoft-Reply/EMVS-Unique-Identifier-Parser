// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseParser.cs" company="Solidsoft Reply Ltd">
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
// The base pack identifier parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CommentTypo
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

#pragma warning disable S907
[assembly: CLSCompliant(true)]

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using HighCapacityAidc;
using HighCapacityAidc.PreProcessors;

using AnsiMhDi;
using Gs1Ai;

using Packs;

using BarcodeScanner.Symbology;

using Properties;

/// <summary>
///   The pack identifier parser.
/// </summary>
internal static partial class BaseParser {
#if NET8_0_OR_GREATER
    /// <summary>
    /// Composite format for Packs_Error_001.
    /// </summary>
    private static readonly CompositeFormat PacksError001 = CompositeFormat.Parse(Resources.Packs_Error_001);

    /// <summary>
    /// Composite format for Packs_Error_002.
    /// </summary>
    private static readonly CompositeFormat PacksError002 = CompositeFormat.Parse(Resources.Packs_Error_002);

    /// <summary>
    /// Composite format for Packs_Error_003.
    /// </summary>
    private static readonly CompositeFormat PacksError003 = CompositeFormat.Parse(Resources.Packs_Error_003);

    /// <summary>
    /// Composite format for Packs_Error_004.
    /// </summary>
    private static readonly CompositeFormat PacksError004 = CompositeFormat.Parse(Resources.Packs_Error_004);

    /// <summary>
    /// Composite format for Parser_Error_005.
    /// </summary>
    private static readonly CompositeFormat ParserError005 = CompositeFormat.Parse(Resources.Parser_Error_005);
#endif

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <param name="preProcessedData">The pre-processed barcode data.</param>
    /// <param name="calibrationProcessor">Pre-processor for calibration data.</param>
    /// <param name="preProcessors">Additional pre-processor functions, provided as a delegate.</param>
    /// <returns>A pack identifier.</returns>
    public static IPackIdentifier Parse(string? data, out string preProcessedData, Preprocessor? calibrationProcessor, Preprocessor? preProcessors = null) {
        // Remove any trailing CR or LF
        while (true) {
            if (!string.IsNullOrEmpty(data) &&
                (data.EndsWith('\r') || data.EndsWith('\n'))) {
                data = data[..^1];
                continue;
            }

            break;
        }

        // We need to convert the AIM identifier if it exists and there is a conversion.
        if (calibrationProcessor != default) {
            // Add the calibration processor to the list of pre-processors
            if (preProcessors is null) {
                preProcessors = calibrationProcessor;
            }
            else {
                if (!Array.Exists(preProcessors.GetInvocationList(), d => d.Equals(calibrationProcessor))) {
                    preProcessors += calibrationProcessor;
                }
            }
        }

        var packIdentifier = new PackIdentifier();
        preProcessedData = data ?? string.Empty;

        // Is any data present?
        if (string.IsNullOrWhiteSpace(data)) {
            packIdentifier.AddException(new PackIdentifierException(0, Resources.Parser_Error_001));
            return packIdentifier;
        }

        // Initialize the required pre-processors.
        if (preProcessors is null) {
            preProcessors = IsoIec15434Envelope.FixMissingControlCharacters;
        }
        else {
            var missingControlCharacters = new Preprocessor(IsoIec15434Envelope.FixMissingControlCharacters);
            if (!Array.Exists(preProcessors.GetInvocationList(), d => d.Equals(missingControlCharacters))) {
                preProcessors += missingControlCharacters;
            }
        }

        var replaceAngleBracketRepresentation =
            new Preprocessor(AsciiControlCharacters.ReplaceAngleBracketRepresentation);
        if (!Array.Exists(preProcessors.GetInvocationList(), d => d.Equals(replaceAngleBracketRepresentation))) {
            preProcessors += replaceAngleBracketRepresentation;
        }

        var barcode = Parsers.HighCapacityAidc.Parser.Parse(data, out var preProcessedDataOut, preProcessors);

        preProcessedData = preProcessedDataOut;
        packIdentifier.RawData = preProcessedDataOut;

        packIdentifier.ValidSymbology = ValidSymbology(barcode);

        if (packIdentifier.ValidSymbology == SymbologyValidity.False) {
            packIdentifier.AddException(new PackIdentifierException(3, Resources.Parser_Error_002));
        }

        if (barcode.IsRecognised) {
            // Add any exceptions to the pack identifier.
            foreach (var exception in barcode.Exceptions) {
                if (exception is DataElementException elementException) {
                    packIdentifier.AddException(
                        new ParseException(
                            elementException.ErrorNumber,
                            elementException.Message,
                            elementException.Title,
                            elementException.CharacterPosition,
                            elementException.IsFatal));
                }
                else {
                    packIdentifier.AddException(
                        new ParseException(exception.ErrorNumber, exception.Message, string.Empty, 0, false));
                }
            }

            // Reconstitute the records in the barcode.
            var records = from element in barcode.DataElements
                          group element by element.Record
                          into rec
                          let firstOrDefault = rec.FirstOrDefault()
                          where firstOrDefault is not null
                          select new Record(firstOrDefault.Format, rec.ToList());

            // Add each record to the record collection.
            var enumerable = records as IRecord[] ?? records.ToArray();

            foreach (var record in enumerable) {
                packIdentifier.AddRecord(record);
            }

            // Collect identifier information accross multiple records to check for
            // ambiguities both within and between records.
            var candidateIdentifiers =
                new List<(Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers)>();

            // Process the pack identifier fields in each record, building a unique pack
            // identifier if possible.
            foreach (var record in enumerable) {
                // Add a new record to the cross-record identities
                var crossRecordIdentifier = new Dictionary<string, List<string>>
                                            {
                                                { "scheme", new List<string>() },
                                                { "productCode", new List<string>() },
                                                { "batchIdentifier", new List<string>() },
                                                { "expiry", new List<string>() },
                                                { "serialNumber", new List<string>() },
                                            };

                var isUnique = true;

                // Determine the type of record
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (record.Encoding) {
                    // Assign GS1 data to the pack identifier.
                    case Encoding.Gs1:
                        isUnique = AssignGs1PackIdentifierFields(record, packIdentifier, crossRecordIdentifier);
                        break;

                    // Assign IFA data to the pack identifier.
                    case Encoding.AscMh10:
                        isUnique = AssignIfaPackIdentifierFields(record, packIdentifier, crossRecordIdentifier);
                        break;

                    // ReSharper disable once RedundantEmptySwitchSection
                    default:
                        break;
                }

                // If the record level only contains unique identifier elements, continue processing.
                if (isUnique) {
                    candidateIdentifiers.Add(
                        (scheme: packIdentifier.Scheme,
                         productCode: packIdentifier.ProductCode,
                         serialNumber: packIdentifier.SerialNumber,
                         batchIdentifier: packIdentifier.BatchIdentifier,
                         expiry: packIdentifier.Expiry,
                         nationalNumbers: new ReadOnlyDictionary<NhrnMarket, string>(packIdentifier.NationalNumbers.ToImmutableDictionary())));

                    packIdentifier.ResetIdentifier();
                    continue;
                }

                // The record is not unique, there is no point in continuing to parse the records, as there is
                // no pissibility of retrieving a unique identifier unambiguously from the barcode.
                packIdentifier.AddException(new PackIdentifierException(4, Resources.Parser_Error_003));
                return PostProcessIdentifier(packIdentifier);
            }

            switch (candidateIdentifiers.Count) {
                case > 1: {
                        var distinctCandidates = candidateIdentifiers.Distinct();

                        // If a single distinct candidate exists, we will return it.
                        var valueTuples =
                            distinctCandidates as (
                                Scheme scheme,
                                string productCode,
                                string serialNumber,
                                string
                                batchIdentifier,
                                string expiry,
                                IReadOnlyDictionary<NhrnMarket, string> nationalNumbers)[] ?? distinctCandidates.ToArray();

                        if (valueTuples.Length == 1) {
                            SetIdentifier(valueTuples[0]);
                            return PostProcessIdentifier(packIdentifier);
                        }

                        // If there are two or more distinct records with both a product code and a serial number, we
                        // have an ambiguity.
                        var distinctRecordsAll = (from distinctCandidate in valueTuples
                                                  where !string.IsNullOrEmpty(distinctCandidate.productCode)
                                                        && !string.IsNullOrEmpty(distinctCandidate.serialNumber)
                                                  select distinctCandidate).ToList();
                        var distinctRecords = distinctRecordsAll.Distinct(new DistinctRecordComparer());

                        if (distinctRecords.Count() > 1) {
                            packIdentifier.ResetIdentifier();

                            var distinctProductCodes = (from distinctRecord in distinctRecordsAll
                                                        where !string.IsNullOrEmpty(distinctRecord.productCode)
                                                        select distinctRecord.productCode).Distinct().ToList();

                            var distinctSerialNumbers = (from distinctRecord in distinctRecordsAll
                                                         where !string.IsNullOrEmpty(distinctRecord.serialNumber)
                                                         select distinctRecord.serialNumber).Distinct().ToList();

                            var distinctBatchIdentifiers = (from distinctRecord in distinctRecordsAll
                                                            where !string.IsNullOrEmpty(distinctRecord.batchIdentifier)
                                                            select distinctRecord.batchIdentifier).Distinct().ToList();

                            var distinctExpiryDates = (from distinctRecord in distinctRecordsAll
                                                       where !string.IsNullOrEmpty(distinctRecord.expiry)
                                                       select distinctRecord.expiry).Distinct().ToList();

                            switch (distinctProductCodes.Count) {
                                // Replace errors
                                case > 1:
                                    packIdentifier.AddOrReplaceException(
                                        new PackIdentifierException(
                                            10,
                                            string.Format(
                                                CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                                PacksError001,
#else
                                                Resources.Packs_Error_001,
#endif
                                                Resources.SubstituteUnique)));
                                    break;
                                case 1:
                                    packIdentifier.ProductCode = distinctProductCodes[0];
                                    break;
                            }

                            switch (distinctSerialNumbers.Count) {
                                case > 1:
                                    packIdentifier.AddOrReplaceException(
                                        new PackIdentifierException(
                                            9,
                                            string.Format(
                                                CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                                PacksError004,
#else
                                                Resources.Packs_Error_004,
#endif
                                                Resources.SubstituteUnique)));
                                    break;
                                case 1:
                                    packIdentifier.SerialNumber = distinctSerialNumbers[0];
                                    break;
                            }

                            switch (distinctBatchIdentifiers.Count) {
                                case > 1:
                                    packIdentifier.AddOrReplaceException(
                                        new PackIdentifierException(
                                            7,
                                            string.Format(
                                                CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                                PacksError002,
#else
                                                Resources.Packs_Error_002,
#endif
                                                Resources.SubstituteUnique)));
                                    break;
                                case 1:
                                    packIdentifier.BatchIdentifier = distinctBatchIdentifiers[0];
                                    break;
                            }

                            switch (distinctExpiryDates.Count) {
                                case > 1:
                                    packIdentifier.AddOrReplaceException(
                                        new PackIdentifierException(
                                            8,
                                            string.Format(
                                                CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                                PacksError003,
#else
                                                Resources.Packs_Error_003,
#endif
                                                Resources.SubstituteUnique)));
                                    break;
                                case 1:
                                    packIdentifier.Expiry = distinctExpiryDates[0];
                                    break;
                            }

                            packIdentifier.AddException(new PackIdentifierException(4, Resources.Parser_Error_003));
                            return PostProcessIdentifier(packIdentifier);
                        }

                        // If there is a single distinct record with all four elements, we will favour this.
                        var favouredCandidates = (from distinctCandidate in valueTuples
                                                  where !string.IsNullOrEmpty(distinctCandidate.productCode)
                                                        && !string.IsNullOrEmpty(distinctCandidate.serialNumber)
                                                        && !string.IsNullOrEmpty(distinctCandidate.batchIdentifier)
                                                        && !string.IsNullOrEmpty(distinctCandidate.expiry)
                                                  select distinctCandidate).ToList();

                        if (favouredCandidates.Count == 1) {
                            SetIdentifier(favouredCandidates[0]);
                            return PostProcessIdentifier(packIdentifier);
                        }

                        // Can we construct an umabiguous unique identifier from the contents of multiple
                        // distinct records?
                        var schemeValues =
                            (from distinctCandidate in valueTuples select distinctCandidate.scheme).Distinct();
                        var productCodeValues = (from distinctCandidate in valueTuples
                                                 where !string.IsNullOrEmpty(distinctCandidate.productCode)
                                                 select distinctCandidate.productCode).Distinct().ToList();
                        var serialNumberValues = (from distinctCandidate in valueTuples
                                                  where !string.IsNullOrEmpty(distinctCandidate.serialNumber)
                                                  select distinctCandidate.serialNumber).Distinct().ToList();
                        var batchIdentifierValues = (from distinctCandidate in valueTuples
                                                     where !string.IsNullOrEmpty(distinctCandidate.batchIdentifier)
                                                     select distinctCandidate.batchIdentifier).Distinct().ToList();
                        var expiryValues = (from distinctCandidate in valueTuples
                                            where !string.IsNullOrEmpty(distinctCandidate.expiry)
                                            select distinctCandidate.expiry).Distinct().ToList();

                        packIdentifier.ResetIdentifier();
                        var schemes = schemeValues as Scheme[] ?? schemeValues.ToArray();

                        packIdentifier.Scheme = schemes.Length == 1 ? schemes[0] : Scheme.Unknown;

                        switch (productCodeValues.Count) {
                            // Replace errors
                            case > 1:
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(4, Resources.Parser_Error_003));
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(
                                        10,
                                        string.Format(
                                            CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                            PacksError001,
#else
                                            Resources.Packs_Error_001,
#endif
                                            Resources.SubstituteUnique)));
                                break;
                            case 1:
                                packIdentifier.ProductCode = productCodeValues[0];
                                break;
                        }

                        switch (serialNumberValues.Count) {
                            case > 1:
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(4, Resources.Parser_Error_003));
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(
                                        9,
                                        string.Format(
                                            CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                            PacksError004,
#else
                                            Resources.Packs_Error_004,
#endif
                                            Resources.SubstituteUnique)));
                                break;
                            case 1:
                                packIdentifier.SerialNumber = serialNumberValues[0];
                                break;
                        }

                        switch (batchIdentifierValues.Count) {
                            case > 1:
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(
                                        7,
                                        string.Format(
                                            CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                            PacksError002,
#else
                                            Resources.Packs_Error_002,
#endif
                                            Resources.SubstituteUnique)));
                                break;
                            case 1:
                                packIdentifier.BatchIdentifier = batchIdentifierValues[0];
                                break;
                        }

                        switch (expiryValues.Count) {
                            case > 1:
                                packIdentifier.AddOrReplaceException(
                                    new PackIdentifierException(
                                        8,
                                        string.Format(
                                            CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                            PacksError003,
#else
                                            Resources.Packs_Error_003,
#endif
                                            Resources.SubstituteUnique)));
                                break;
                            case 1:
                                packIdentifier.Expiry = expiryValues[0];
                                break;
                        }

                        // Resolve the scheme if necessary.
                        packIdentifier.Scheme =
                            packIdentifier.Scheme == Scheme.Unknown
                                ? TestByProductCodeLength()
                                : Scheme.Unknown;

                        return PostProcessIdentifier(packIdentifier);

                        // Test for a German PPN.
                        Scheme TestForPpnProductCode() =>
                            packIdentifier.ProductCode.Length == 12
                                ? Scheme.Ifa
                                : Scheme.Unknown;

                        // Test for a GS1 GTIN-14/NTIN.
                        Scheme TestByProductCodeLength() =>
                            packIdentifier.ProductCode.Length == 14
                                ? Scheme.Gs1
                                : TestForPpnProductCode();
                    }

                case > 0:
                    SetIdentifier(candidateIdentifiers[0]);
                    break;
            }

            return PostProcessIdentifier(packIdentifier);

            void SetIdentifier(
                (Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers) candidate) {
                packIdentifier.ResetIdentifier();
                packIdentifier.Scheme = candidate.scheme;
                packIdentifier.ProductCode = candidate.productCode;
                packIdentifier.SerialNumber = candidate.serialNumber;
                packIdentifier.BatchIdentifier = candidate.batchIdentifier;
                packIdentifier.Expiry = candidate.expiry;
                foreach (var nationalNumber in candidate.nationalNumbers) {
                    packIdentifier.AddNationalNumber(nationalNumber.Key, nationalNumber.Value);
                }
            }
        }

        packIdentifier.ResetIdentifier();
        packIdentifier.AddException(new PackIdentifierException(4, Resources.Parser_Error_003));
        return packIdentifier;
    }

#if !NET7_0_OR_GREATER
    /// <summary>
    ///   Returns a regular expression to test that an IFA product code is composed of digits only.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    private static readonly Regex WellFormedIfaProductCodeRegex = new(@"^\d{12}$", RegexOptions.None);

    /// <summary>
    ///   Returns a regular expression to test that a GTIN/NTIN product code is composed of digits only.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    private static readonly Regex WellFormedGtinOrNtinRegex = new(@"^\d{14}$", RegexOptions.None);

    /// <summary>
    ///   Returns a regular expression for six-digit date representation - YYMMDD.
    ///   If it is not necessary to specify the day, the day field can be filled with two zeros.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    private static readonly Regex DatePatternYyMmDdZerosRegex = new(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[13456789]|1[012])(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))", RegexOptions.None);

    /// <summary>
    ///   Returns a regular expression for Character Set 82 character strings of variable length.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    private static readonly Regex CharacterSet82Regex = new(@"^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,20}$", RegexOptions.None);
#endif

#if NET7_0_OR_GREATER
    /// <summary>
    ///   Returns a regular expression to test that an IFA product code is composed of digits only.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    [GeneratedRegex(@"^\d{12}$", RegexOptions.None, "en-US")]
    private static partial Regex WellFormedIfaProductCodeRegex();

    /// <summary>
    ///   Returns a regular expression to test that a GTIN/NTIN product code is composed of digits only.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    [GeneratedRegex(@"^\d{14}$", RegexOptions.None, "en-US")]
    private static partial Regex WellFormedGtinOrNtinRegex();

    /// <summary>
    ///   Returns a regular expression for six-digit date representation - YYMMDD.
    ///   If it is not necessary to specify the day, the day field can be filled with two zeros.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    [GeneratedRegex(@"(((\d{2})(0[13578]|1[02])(0[0-9]|[12]\d|3[01]))|((\d{2})(0[13456789]|1[012])(0[0-9]|[12]\d|30))|((\d{2})02(0[0-9]|1\d|2[0-8]))|(((0[048]|[2468][048]|[13579][26]))0229))", RegexOptions.None, "en-US")]
    private static partial Regex DatePatternYyMmDdZerosRegex();

    /// <summary>
    ///   Returns a regular expression for Character Set 82 character strings of variable length.
    /// </summary>
    /// <returns>A regualar expression.</returns>
    [GeneratedRegex(@"^[-!""%&'()*+,./0-9:;<=>?A-Z_a-z]{1,20}$", RegexOptions.None, "en-US")]
    private static partial Regex CharacterSet82Regex();
#endif

    /// <summary>
    ///   Processes a record to assign pack identifier fields.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="crossRecordIdentifier">A candidate collection of data elements that may be taken as a pack identifier.</param>
    /// <returns>
    ///   True, if the pack identifier fields are currently unique or unassigned for all records
    ///   processed to this point; otherwise false if non-uniqueness is detected.
    /// </returns>
    private static bool AssignGs1PackIdentifierFields(
        IRecord record,
        PackIdentifier packIdentifier,
        IReadOnlyDictionary<string, List<string>> crossRecordIdentifier) {
        // Select GS1 pack identifier fields. We will allow scenarios where the same field is duplicated
        // with the same value, as this does not compromise the uniqueness of the pack identifier.
        var nonUniquenessDetected = false;
        var scheme = Scheme.Unknown;
        var productCode = string.Empty;
        var batchIdentifier = string.Empty;
        var expiry = string.Empty;
        var serialNumber = string.Empty;
        var nhrnNationalNumbers = new Dictionary<NhrnMarket, string>();
        var elementIndex = 0;

        foreach (var element in record.Elements) {
            // Get the GS1 AI
            if (!Enum.TryParse(element.Identifier, out ApplicationIdentifier gs1Ai)) {
                gs1Ai = ApplicationIdentifier.Unrecognised;
            }

            var nhrnMarket = NhrnMarket.Portugal;
            elementIndex++;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (gs1Ai) {
                case ApplicationIdentifier.GlobalTradeItemNumber:

                    // Validate the GTIN/NTIN.
                    if (!IsGtinValid(element.Data, packIdentifier, element.Identifier, element.Title, elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    scheme = Scheme.Gs1;

                    if (!string.IsNullOrEmpty(productCode)) {
                        if (productCode.Trim() != element.Data.Trim()) {
                            crossRecordIdentifier[nameof(productCode)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    10,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError001,
#else
                                        Resources.Packs_Error_001,
#endif
                                        Resources.SubstituteUnique),
                                    "01",
                                    "GTIN",
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    productCode = element.Data;
                    crossRecordIdentifier[nameof(scheme)].Add(scheme.ToString());
                    crossRecordIdentifier[nameof(productCode)].Add(element.Data);
                    break;
                case ApplicationIdentifier.BatchOrLotNumber:

                    // Validate the batch identifier.
                    if (!IsBatchIdentifierValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) && packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(batchIdentifier)) {
                        if (!string.Equals(
                                batchIdentifier.Trim(),
                                element.Data.Trim(),
                                StringComparison.Ordinal)) {
                            crossRecordIdentifier[nameof(batchIdentifier)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 7 : 30,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError002,
#else
                                        Resources.Packs_Error_002,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Gs1 ? "10" : string.Empty,
                                    scheme == Scheme.Gs1 ? "BATCH/LOT" : string.Empty,
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    batchIdentifier = element.Data;
                    crossRecordIdentifier[nameof(batchIdentifier)].Add(element.Data);
                    break;
                case ApplicationIdentifier.ExpirationDate:

                    // Validate the expiry date.
                    if (!IsExpiryDateValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) && packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(expiry)) {
                        if (expiry.Trim() != element.Data.Trim()) {
                            crossRecordIdentifier[nameof(expiry)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 8 : 40,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError003,
#else
                                        Resources.Packs_Error_003,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Gs1 ? "17" : string.Empty,
                                    scheme == Scheme.Gs1 ? "USE BY OR EXPIRY" : string.Empty,
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    expiry = element.Data;
                    crossRecordIdentifier[nameof(expiry)].Add(element.Data);
                    break;
                case ApplicationIdentifier.SerialNumber:

                    // Validate the serial number.
                    if (!IsSerialNumberValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(serialNumber)) {
                        if (!string.Equals(
                                serialNumber.Trim(),
                                element.Data.Trim(),
                                StringComparison.Ordinal)) {
                            crossRecordIdentifier[nameof(serialNumber)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 9 : 50,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError004,
#else
                                        Resources.Packs_Error_004,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Gs1 ? "21" : string.Empty,
                                    scheme == Scheme.Gs1 ? "SERIAL" : string.Empty,
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    serialNumber = element.Data;
                    crossRecordIdentifier[nameof(serialNumber)].Add(element.Data);
                    break;
                case ApplicationIdentifier.NationalHealthcareReimbursementNumberGermanyPzn:
                    nhrnMarket = NhrnMarket.Germany;
                    goto case ApplicationIdentifier.NationalHealthcareReimbursementNumberPortugalAim;
                case ApplicationIdentifier.NationalHealthcareReimbursementNumberFranceCip:
                    nhrnMarket = NhrnMarket.France;
                    goto case ApplicationIdentifier.NationalHealthcareReimbursementNumberPortugalAim;
                case ApplicationIdentifier.NationalHealthcareReimbursementNumberSpainCn:
                    nhrnMarket = NhrnMarket.Spain;
                    goto case ApplicationIdentifier.NationalHealthcareReimbursementNumberPortugalAim;
                case ApplicationIdentifier.NationalHealthcareReimbursementNumberPortugalAim:
                    if (packIdentifier.NationalNumbers.ContainsKey(nhrnMarket)) {
                        if (packIdentifier.ParseExceptions.All(ex => ex.ErrorNumber != 2)) {
                            packIdentifier.AddException(
                                new PackIdentifierException(
                                    2,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        ParserError005,
#else
                                        Resources.Parser_Error_005,
#endif
                                        nhrnMarket)));
                        }

                        break;
                    }

                    if (nhrnNationalNumbers.ContainsKey(nhrnMarket)) {
                        if (packIdentifier.ParseExceptions.All(ex => ex.ErrorNumber != 2)) {
                            packIdentifier.AddException(
                                new PackIdentifierException(
                                    2,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        ParserError005,
#else
                                        Resources.Parser_Error_005,
#endif
                                        nhrnMarket)));
                        }

                        break;
                    }

                    nhrnNationalNumbers.Add(nhrnMarket, element.Data);
                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        if (nonUniquenessDetected) {
            // Populate the pack identifier with each unambiguous element.
            var uniqueElements = crossRecordIdentifier.Where(elementList => elementList.Value.Count == 1);
            packIdentifier.ResetIdentifier();
            packIdentifier.Scheme = scheme;

            foreach (var element in uniqueElements) {
                switch (element.Key) {
                    case nameof(productCode):
                        packIdentifier.ProductCode = productCode;
                        break;
                    case nameof(batchIdentifier):
                        packIdentifier.BatchIdentifier = batchIdentifier;
                        break;
                    case nameof(expiry):
                        packIdentifier.Expiry = expiry;
                        break;
                    case nameof(serialNumber):
                        packIdentifier.SerialNumber = serialNumber;
                        break;
                }

                foreach (var nationalNumber in nhrnNationalNumbers) {
                    packIdentifier.AddNationalNumber(nationalNumber.Key, nationalNumber.Value);
                }
            }

            return !nonUniquenessDetected;
        }

        // Test to see if we have found all fields of a unique identifier
        var numberOfFieldsFound = (string.IsNullOrEmpty(productCode) ? 0 : 1)
                                + (string.IsNullOrEmpty(batchIdentifier) ? 0 : 1)
                                + (string.IsNullOrEmpty(expiry) ? 0 : 1)
                                + (string.IsNullOrEmpty(serialNumber) ? 0 : 1);

        var numberOfPreviousFieldsFound = (string.IsNullOrEmpty(packIdentifier.ProductCode) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.BatchIdentifier) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.Expiry) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.SerialNumber) ? 0 : 1);

        switch (numberOfFieldsFound) {
            // Resolve the candidate record for the pack identifier.
            // If we have already found a complete pack identifier in a previous record,
            // the pack identifier is ambiguous.
            case 4 when numberOfPreviousFieldsFound == 4:
                nonUniquenessDetected = true;
                break;
            case 4:
                // This record is now the candidate for the pack identifier
                PopulatePackIdentifierFields();
                break;
            default:
                if (numberOfPreviousFieldsFound >= 4) {
                    return !nonUniquenessDetected;
                }

                // We have two partial candidates. If one has more fields than the other, it will
                // be taken as the candidate
                if (numberOfFieldsFound > numberOfPreviousFieldsFound) {
                    // This record is now the candidate for the pack identifier
                    PopulatePackIdentifierFields();
                }
                else if (numberOfFieldsFound == numberOfPreviousFieldsFound &&
                         packIdentifier.Scheme == Scheme.Ifa) {
                    // GS1 is favoured over IFA and earlier records are favoured.
                    // This record is now the candidate for the pack identifier
                    PopulatePackIdentifierFields();
                }

                break;
        }

        return !nonUniquenessDetected;

        void PopulatePackIdentifierFields() {
            // This record is now the candidate for the pack identifier
            packIdentifier.Scheme = Scheme.Gs1;
            packIdentifier.ProductCode = productCode;
            packIdentifier.BatchIdentifier = batchIdentifier;
            packIdentifier.Expiry = expiry;
            packIdentifier.SerialNumber = serialNumber;

            foreach (var nationalNumber in nhrnNationalNumbers) {
                packIdentifier.AddNationalNumber(nationalNumber.Key, nationalNumber.Value);
            }
        }
    }

    /// <summary>
    ///   Processes a record to assign pack identifier fields.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="crossRecordIdentifier">A candidate collection of data elements that may be taken as a pack identifier.</param>
    /// <returns>
    ///   True, if the pack identifier fields are currently unique or unassigned for all records
    ///   processed to this point; otherwise false if non-uniqueness is detected.
    /// </returns>
    private static bool AssignIfaPackIdentifierFields(
        IRecord record,
        PackIdentifier packIdentifier,
        IReadOnlyDictionary<string, List<string>> crossRecordIdentifier) {
        // Select IFA pack identifier fields. We will allow scenarios where the same field is duplicated
        // with the same value, as this does not compromise the uniqueness of the pack identifier.
        var nonUniquenessDetected = false;
        var scheme = Scheme.Unknown;
        var productCode = string.Empty;
        var batchIdentifier = string.Empty;
        var expiry = string.Empty;
        var serialNumber = string.Empty;
        var nhrnNationalNumbers = new Dictionary<NhrnMarket, string>();
        var elementIndex = 0;

        foreach (var element in record.Elements) {
            // Get the ASC MH10.8.2 DI
            var nhrnMarket = NhrnMarket.Portugal;
            elementIndex++;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch ((DataIdentifier)element.Data.Resolve(element.Identifier, 0).Entity) {
                case DataIdentifier.Ppn:
                    // For EMVS, PPNs are treated as fixed width numeric data in which the first two digits represent an issuing
                    // authority, the last two digits are a checksum based on the ASCII characters of the rest of the PPN and the
                    // remaining 8 digits are a PZN (whose last digit is a checksum). The ASC MH10 parser does not validate
                    // according to this restrictive format, and so won't detect issues with a pack identifier PPN. Hence,
                    // EMVS-specific validation is invoked here.

                    // Validate the PPN as a German medicinal product code.
                    if (!IsPpnValid(element.Data, packIdentifier, element.Identifier, element.Title, elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    scheme = Scheme.Ifa;

                    if (!string.IsNullOrEmpty(productCode)) {
                        if (productCode.Trim() != element.Data.Trim()) {
                            crossRecordIdentifier[nameof(productCode)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    20,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError001,
#else
                                        Resources.Packs_Error_001,
#endif
                                        Resources.SubstituteUnique),
                                    "9N",
                                    "PPN",
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    productCode = element.Data;
                    crossRecordIdentifier[nameof(scheme)].Add(scheme.ToString());
                    crossRecordIdentifier[nameof(productCode)].Add(element.Data);
                    break;
                case DataIdentifier.TraceabilityNumberAssignedByTheSupplier:

                    // Validate the batch identifier.
                    if (!IsBatchIdentifierValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(batchIdentifier)) {
                        if (!string.Equals(
                                batchIdentifier.Trim(),
                                element.Data.Trim(),
                                StringComparison.Ordinal)) {
                            crossRecordIdentifier[nameof(batchIdentifier)].Add(element.Data);
#pragma warning disable SA1118 // Parameter should not span multiple lines
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 7 : 30,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError002,
#else
                                        Resources.Packs_Error_002,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Ifa ? "1T" : string.Empty,
                                    scheme == Scheme.Ifa
                                        ? "TRACEABILITY NUMBER ASSIGNED BY THE SUPPLIER"
                                        : string.Empty,
                                    0));
#pragma warning restore SA1118 // Parameter should not span multiple lines

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    batchIdentifier = element.Data;
                    crossRecordIdentifier[nameof(batchIdentifier)].Add(element.Data);
                    break;
                case DataIdentifier.DateYyMmDd:

                    // Validate the expiry date.
                    if (!IsExpiryDateValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(expiry)) {
                        if (expiry.Trim() != element.Data.Trim()) {
                            crossRecordIdentifier[nameof(expiry)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 8 : 40,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError003,
#else
                                        Resources.Packs_Error_003,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Ifa ? "D" : string.Empty,
                                    scheme == Scheme.Ifa ? "DATE" : string.Empty,
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    expiry = element.Data;
                    crossRecordIdentifier[nameof(expiry)].Add(element.Data);
                    break;
                case DataIdentifier.SerialNumber:

                    // Validate the serial number.
                    if (!IsSerialNumberValid(
                            element.Data,
                            packIdentifier,
                            element.Identifier,
                            element.Title,
                            elementIndex) &&
                        packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 1)) {
                        packIdentifier.AddException(new PackIdentifierException(1, Resources.Parser_Error_004));
                    }

                    if (!string.IsNullOrEmpty(serialNumber)) {
                        if (!string.Equals(
                                serialNumber.Trim(),
                                element.Data.Trim(),
                                StringComparison.Ordinal)) {
                            crossRecordIdentifier[nameof(serialNumber)].Add(element.Data);
                            packIdentifier.AddException(
                                new PackIdentifierFieldException(
                                    scheme == Scheme.Unknown ? 9 : 50,
                                    string.Format(
                                        CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                        PacksError004,
#else
                                        Resources.Packs_Error_004,
#endif
                                        Resources.SubstituteUnique),
                                    scheme == Scheme.Ifa ? "S" : string.Empty,
                                    scheme == Scheme.Ifa ? "SERIAL NUMBER" : string.Empty,
                                    0));

                            nonUniquenessDetected = true;
                        }

                        break;
                    }

                    serialNumber = element.Data;
                    crossRecordIdentifier[nameof(serialNumber)].Add(element.Data);
                    break;
                case DataIdentifier.Gs1Gtin14:
                    var country = element.Data.ResolveGtinNtinToGs1Country();

                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (country) {
                        case CountryCode.Germany:
                            nhrnMarket = NhrnMarket.Germany;
                            goto case CountryCode.Portugal;
                        case CountryCode.FranceAndMonaco:
                            nhrnMarket = NhrnMarket.France;
                            goto case CountryCode.Portugal;
                        case CountryCode.SpainAndAndorra:
                            nhrnMarket = NhrnMarket.Spain;
                            goto case CountryCode.Portugal;
                        case CountryCode.Portugal:
                            if (packIdentifier.NationalNumbers.ContainsKey(nhrnMarket) &&
                                packIdentifier.ParseExceptions.All(ex => ex.ErrorNumber != 2)) {
                                packIdentifier.AddException(
                                    new PackIdentifierException(
                                        2,
                                        string.Format(
                                            CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                            ParserError005,
#else
                                            Resources.Parser_Error_005,
#endif
                                            nhrnMarket)));
                                break;
                            }

                            if (nhrnNationalNumbers.ContainsKey(nhrnMarket)) {
                                if (packIdentifier.ParseExceptions.All(ex => ex.ErrorNumber != 2)) {
                                    packIdentifier.AddException(
                                        new PackIdentifierException(
                                            2,
                                            string.Format(
                                                CultureInfo.CurrentCulture,
#if NET8_0_OR_GREATER
                                                ParserError005,
#else
                                                Resources.Parser_Error_005,
#endif
                                                nhrnMarket)));
                                }

                                break;
                            }

                            nhrnNationalNumbers.Add(nhrnMarket, element.Data);
                            break;

                        // ReSharper disable once RedundantEmptySwitchSection
                        default:
                            break;
                    }

                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        if (nonUniquenessDetected) {
            // Populate the pack identifier with each unambiguous element.
            var uniqueElements = crossRecordIdentifier.Where(elementList => elementList.Value.Count == 1);
            packIdentifier.ResetIdentifier();
            packIdentifier.Scheme = scheme;

            foreach (var (uniqueElementKey, _) in uniqueElements) {
                switch (uniqueElementKey) {
                    case nameof(productCode):
                        packIdentifier.ProductCode = productCode;
                        break;
                    case nameof(batchIdentifier):
                        packIdentifier.BatchIdentifier = batchIdentifier;
                        break;
                    case nameof(expiry):
                        packIdentifier.Expiry = expiry;
                        break;
                    case nameof(serialNumber):
                        packIdentifier.SerialNumber = serialNumber;
                        break;
                }

                foreach (var (key, value) in nhrnNationalNumbers) {
                    packIdentifier.AddNationalNumber(key, value);
                }
            }

            return !nonUniquenessDetected;
        }

        // Test to see if we have found all fields of a unique identifier
        var numberOfFieldsFound = (string.IsNullOrEmpty(productCode) ? 0 : 1)
                                + (string.IsNullOrEmpty(batchIdentifier) ? 0 : 1)
                                + (string.IsNullOrEmpty(expiry) ? 0 : 1)
                                + (string.IsNullOrEmpty(serialNumber) ? 0 : 1);

        var numberOfPreviousFieldsFound = (string.IsNullOrEmpty(packIdentifier.ProductCode) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.BatchIdentifier) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.Expiry) ? 0 : 1)
                                        + (string.IsNullOrEmpty(packIdentifier.SerialNumber) ? 0 : 1);

        switch (numberOfFieldsFound) {
            // Resolve the candidate record for the pack identifier.
            // If we have already found a complete pack identifier in a previous record,
            // the pack identifier is ambiguous.
            case 4 when numberOfPreviousFieldsFound == 4:
                nonUniquenessDetected = true;
                break;
            case 4:
                // This record is now the candidate for the pack identifier
                PopulatePackIdentifierFields();
                break;
            default:
                if (numberOfPreviousFieldsFound == 4) {
                    // The previously detected record remains the candidate for the pack identifier
                }
                else {
                    // We have two partial candidates. If one has more fields than the other, it will
                    // be taken as the candidate
                    if (numberOfFieldsFound > numberOfPreviousFieldsFound) {
                        // This record is now the candidate for the pack identifier
                        PopulatePackIdentifierFields();
                    }

                    // GS1 is favoured over IFA and earlier records are favoured, so do nothing here.
                }

                break;
        }

        return !nonUniquenessDetected;

        void PopulatePackIdentifierFields() {
            // This record is now the candidate for the pack identifier
            packIdentifier.Scheme = Scheme.Ifa;
            packIdentifier.ProductCode = productCode;
            packIdentifier.BatchIdentifier = batchIdentifier;
            packIdentifier.Expiry = expiry;
            packIdentifier.SerialNumber = serialNumber;

            foreach (var nationalNumber in nhrnNationalNumbers) {
                packIdentifier.AddNationalNumber(nationalNumber.Key, nationalNumber.Value);
            }
        }
    }

    /// <summary>
    ///   Check the validity of a batch identifier.
    /// </summary>
    /// <param name="batchIdentifier">The batch identifier.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="identifier">The data element identifier.</param>
    /// <param name="title">The data element title.</param>
    /// <param name="elementIndex">
    ///   The element index of the batch identifier in the data transmitted by the scanner.
    /// </param>
    /// <returns>True, if the batch identifier is valid; otherwise false.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static bool IsBatchIdentifierValid(
        string batchIdentifier,
        PackIdentifier packIdentifier,
        string identifier,
        string title,
        int elementIndex) {
        // SecurPharm and IFA define a batch identifier (lot number) for medicinal packs of prescription
        // medicine in the same way as the GS1 General Specifications. It is a string of between 1 and 20
        // characters containing the invariant characters defined in ISO 646. GS1 calls this 'Character Set 82'.
        // NB. These rules conflict with the MH10.8.2 standard for DI 1T (Traceability Number Assigned By The
        // Supplier) which effectively constrains this field to upper case Latin letters and digits.
        var isValid = true;

        // Rule 1: The batch identifier must contain data.
        if (string.IsNullOrEmpty(batchIdentifier)) {
            packIdentifier.AddException(new PackIdentifierException(31, Resources.Parser_Error_014));
            isValid = false;
        }

        // Rule 2: The batch identifier must be between 1 and 20 characters in length.
        if (batchIdentifier is { Length: > 20 }) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    32,
                    $"{Resources.Parser_Error_015} {Resources.Parser_Error_016}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 3: The batch identifier must be composed of invariant characters only.
#if NET7_0_OR_GREATER
        if (CharacterSet82Regex().Match(batchIdentifier).Success) {
#else
        if (CharacterSet82Regex.Match(batchIdentifier).Success) {
#endif
            return isValid;
        }

        packIdentifier.AddException(
            new PackIdentifierFieldException(
                33,
                $"{Resources.Parser_Error_015} {Resources.Parser_Error_017}",
                identifier,
                title,
                elementIndex));

        return false;
    }

    /// <summary>
    ///   Check the validity of an expiry date.
    /// </summary>
    /// <param name="expiryDate">The expiry date.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="identifier">The data element identifier.</param>
    /// <param name="title">The data element title.</param>
    /// <param name="elementIndex">
    ///   The element index of the expiry date in the data transmitted by the scanner.
    /// </param>
    /// <returns>True, if the expiry date is valid; otherwise false.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static bool IsExpiryDateValid(
        string expiryDate,
        PackIdentifier packIdentifier,
        string identifier,
        string title,
        int elementIndex) {
        // SecurPharm and IFA define an expiry date for medicinal packs of prescription medicine in
        // the same way as the GS1 General Specifications. It is a string containing exactly 6 digits
        // representing the date in YYMMDD format. The DD digits can be set to "00" for expiry by
        // month.
        var isValid = true;

        // Rule 1: The batch identifier must contain data.
        if (string.IsNullOrEmpty(expiryDate)) {
            packIdentifier.AddException(new PackIdentifierException(41, Resources.Parser_Error_018));
            isValid = false;
        }

        // Rule 2: The expiry date must be correctly formatted as YYMMDD allowing DD to be 00.
#if NET7_0_OR_GREATER
        if (DatePatternYyMmDdZerosRegex().Match(expiryDate).Success) {
#else
        if (DatePatternYyMmDdZerosRegex.Match(expiryDate).Success) {
#endif
            return isValid;
        }

        packIdentifier.AddException(
            new PackIdentifierFieldException(42, Resources.Parser_Error_019, identifier, title, elementIndex));

        return false;
    }

    /// <summary>
    ///   Validates a GTIN/NTIN by testing that its checksum is correct according to the GS1 specification.
    /// </summary>
    /// <param name="gtin">The PPN.</param>
    /// <returns>True, if the checksum is correct; otherwise false.</returns>
    private static bool IsGs1ChecksumValid(string gtin) {
        /* Each GTIN requires a check digit for additional data security. To calculate the check digit, we
         * the sequence of digits in the GTIN. Each digit in an even position in the reversed sequence is
         * mutiplied by three and cummulatively summed with all digits in odd positions. The summed total
         * should be exactly divisible by 10. Otherwise, the chwck digit (the last digit in the GTIN) is
         * invalid.
         * */
        if (string.IsNullOrWhiteSpace(gtin)) {
            return false;
        }

        // Ensure that the string contains only integer values.
        if (gtin.Any(c => (int)char.GetNumericValue(c) == -1)) {
            return false;
        }

        // Test the checksum.
        var sum = gtin.Reverse().Select((c, i) => (int)char.GetNumericValue(c) * (i % 2 == 0 ? 1 : 3)).Sum();

        // ReSharper disable once ArrangeRedundantParentheses
        return (10 - (sum % 10)) % 10 == 0;
    }

    /// <summary>
    ///   Check the validity of a GTIN-14 or NTIN-14 according to the GS1 General Specifications.
    /// </summary>
    /// <param name="gtin">The GTIN/NTIN-14 value.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="identifier">The data element identifier.</param>
    /// <param name="title">The data element title.</param>
    /// <param name="elementIndex">The element index of the GTIN/NTIN in the data transmitted by the scanner.</param>
    /// <returns>True, if the GTIN/NTIN is valid; otherwise false.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static bool IsGtinValid(
        string? gtin,
        PackIdentifier packIdentifier,
        string identifier,
        string title,
        int elementIndex) {
        // GS1 defines a GTIN 14 or NTIN-14 for medicinal packs of prescription medicine as a fixed
        // sequence of 14 digits. The initial digits represent the country of the assigning GS1 office
        // and, for GTINs, a company or organisations (for NTINs, this is a nationally-assigned set of digits)
        // The last digit represents a checksum calculated according to a GS1-mandated algorithm.
        var isValid = true;

        // Rule 1: The GTIN/NTIN must contain data.
        if (string.IsNullOrEmpty(gtin)) {
            packIdentifier.AddException(new PackIdentifierException(11, Resources.Parser_Error_006));
            isValid = false;
        }

        // Rule 2: The TIN/NTIN must be 14 characters in length.
        if (gtin is not null && gtin.Length != 14) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    12,
                    $"{Resources.Parser_Error_007} {Resources.Parser_Error_008}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 3: The GTIN/NTIN product code must be composed of digits only.
#if NET7_0_OR_GREATER
        if (!WellFormedGtinOrNtinRegex().Match(gtin ?? string.Empty).Success) {
#else
        if (!WellFormedGtinOrNtinRegex.Match(gtin ?? string.Empty).Success) {
#endif
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    13,
                    $"{Resources.Parser_Error_007} {Resources.Parser_Error_009}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 4: The GTIN/NTIN checksum obeys GS1 rules
        if (IsGs1ChecksumValid(gtin ?? string.Empty)) {
            return isValid;
        }

        packIdentifier.AddException(
            new PackIdentifierFieldException(
                14,
                $"{Resources.Parser_Error_007} {Resources.Parser_Error_010}",
                identifier,
                title,
                elementIndex));

        return false;
    }

    /// <summary>
    ///   Checks if a PPN checksum is valid.
    /// </summary>
    /// <param name="ppn">
    ///   The PPN.
    /// </param>
    /// <returns>
    ///   True, if valid; otherwise false.
    /// </returns>
    private static bool IsPpnChecksumValid(string ppn) {
        /* Each PPN requires a Modulo 97 Check Sum for additional data security. To calculate the check sum
         * the ASCII value of the alphanumeric characters are used. Each of the characters is converted into
         * the ASCII value and multiplied with the incrementing weight factor beginning with the most
         * significant character to the left and with weight factor 2. The results of each multiplication
         * are summed and divided by 97 and the remainder is the check number. If the remainder is only one
         * digit then a leading zero is added. This 2-character string is appended to the PPN string as the
         * check sum.
         * */

        // Get the checksum and convert to an integer
        var checkCharacters = ppn[^2..];

        if (!int.TryParse(checkCharacters, out var checksum)) {
            return false;
        }

        // Get the alphanumeric characters as a character array
        var ppnAlphaNumericChars = ppn.Take(ppn.Length - 2).ToArray();

        var sum = 0;

        for (var weightFactor = 2; weightFactor <= ppnAlphaNumericChars.Length + 1; weightFactor++) {
            sum += weightFactor * ppnAlphaNumericChars[weightFactor - 2];
        }

        return checksum == sum % 97;
    }

    /// <summary>
    ///   Check the validity of a PPN according to the IFA specification for PPNs based on PZNs.
    /// </summary>
    /// <param name="ppn">The PPN value.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="identifier">The data element identifier.</param>
    /// <param name="title">The data element title.</param>
    /// <param name="elementIndex">The element index of the PPN in the data transmitted by the scanner.</param>
    /// <returns>True, if the PPN is valid; otherwise false.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static bool IsPpnValid(
        string? ppn,
        PackIdentifier packIdentifier,
        string identifier,
        string title,
        int elementIndex) {
        // IFA and SecurPharm define a PPN for medicinal packs of prescription medicine as a fixed
        // sequence of 12 digits. The first two digits represent the Product Registration Agency Code
        // (PRA Code or PRAC) abd are always "11" for PZNs. the next eight digits represent a PZN8.
        // The last two digits represent a checksum calculated according to an IFA-mandated algorithm.
        var isValid = true;

        // Rule 1: The PPN must contain data.
        if (string.IsNullOrEmpty(ppn)) {
            packIdentifier.AddException(new PackIdentifierException(21, Resources.Parser_Error_006));
            isValid = false;
        }

        // Rule 2: The PPN must be 12 characters in length.
        if (ppn is not null && ppn.Length != 12) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    22,
                    $"{Resources.Parser_Error_007} {Resources.Parser_Error_011}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 3: The IFA product code must be composed of digits only.
#if NET7_0_OR_GREATER
        if (!WellFormedIfaProductCodeRegex().Match(ppn ?? string.Empty).Success) {
#else
        if (!WellFormedIfaProductCodeRegex.Match(ppn ?? string.Empty).Success) {
#endif
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    23,
                    $"{Resources.Parser_Error_007} {Resources.Parser_Error_009}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 4: The first two characters of the PPN are "11", representing PZN.
        if (ppn is { Length: > 2 } && ppn[..2] != "11") {
            packIdentifier.AddException(
                new PackIdentifierFieldException(24, Resources.Parser_Error_012, identifier, title, elementIndex));
            isValid = false;
        }

        // Rule 5: The PZN checksum obeys IFA rules
        if (!IsPznChecksumValid(ppn ?? string.Empty)) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(25, Resources.Parser_Error_013, identifier, title, elementIndex));

            isValid = false;
        }

        // Rule 6: The IFA product code must contain a correct checksum value.
        if (IsPpnChecksumValid(ppn ?? string.Empty)) {
            return isValid;
        }

        packIdentifier.AddException(
            new PackIdentifierFieldException(
                26,
                $"{Resources.Parser_Error_007} {Resources.Parser_Error_010}",
                identifier,
                title,
                elementIndex));

        return false;
    }

    /// <summary>
    ///   Check the PZN checksum digit.
    /// </summary>
    /// <param name="ppn">The PPN value.</param>
    /// <returns>True, if the checksum is correct; otherwise false.</returns>
    private static bool IsPznChecksumValid(string ppn) {
        /*  1.  Multiply the 1st digit by 1,
                         the 2nd digit by 2,
                         the 3rd digit by 3,
                         the 4th digit by 4,
                         the 5th digit by 5,
                         the 6th digit by 6,
                         the 7th digit by 7.
            2.  Calculate the sum of these products.
            3.  Divide this sum by 11.
            4.  The remaining integer remainder* corresponds to the check digit.

            * If this yields remainder 10, the number sequence is not used as a PZN!
         * */

        // Get the checksum and convert to an integer
        var checksumCharacters = ppn.Length >= 3 ? ppn[^3].ToString(CultureInfo.InvariantCulture) : string.Empty;

        if (!int.TryParse(checksumCharacters, out var checksum)) {
            return false;
        }

        // Get the pzn
        var pzn = ppn.Length > 2 ? ppn[2..^2] : string.Empty;

        // Check that the PZN is numeric
        if (!long.TryParse(pzn, out _)) {
            return false;
        }

        // Get the numeric characters as an integer array.
        var pznDigits = pzn.Select(Convert.ToInt32).ToArray();
        var sum = 0;

        for (var multiplier = 1; multiplier < pznDigits.Length; multiplier++) {
            sum += multiplier * int.Parse(
                       Convert.ToChar(pznDigits[multiplier - 1], CultureInfo.InvariantCulture)
                              .ToInvariantString(),
                       CultureInfo.InvariantCulture);
        }

        return checksum == sum % 11;
    }

    /// <summary>
    ///   Check the validity of a serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number.</param>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <param name="identifier">The data element identifier.</param>
    /// <param name="title">The data element title.</param>
    /// <param name="elementIndex">
    ///   The element index of the serial number in the data transmitted by the scanner.
    /// </param>
    /// <returns>True, if the serial number is valid; otherwise false.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static bool IsSerialNumberValid(
        string serialNumber,
        PackIdentifier packIdentifier,
        string identifier,
        string title,
        int elementIndex) {
        // SecurPharm and IFA define a serial number for medicinal packs of prescription medicine in the same
        // way as the GS1 General Specifications. It is a string of between 1 and 20 characters containing
        // the invariant characters defined in ISO 646. GS1 calls this 'Character Set 82". NB. These rules
        // conflict with the MH10.8.2 standard for DI S (Serial Number or Code Assigned by the Supplier to an
        // Entity for its Lifetime) which effectively constrains this field to upper case Latin letters and
        // digits.
        var isValid = true;

        // Rule 1: The serial number must contain data.
        if (string.IsNullOrEmpty(serialNumber)) {
            packIdentifier.AddException(new PackIdentifierException(51, Resources.Parser_Error_020));
            isValid = false;
        }

        // Rule 2: The serial number must be between 1 and 20 characters in length.
        if (serialNumber is { Length: > 20 }) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    52,
                    $"{Resources.Parser_Error_021} {Resources.Parser_Error_016}",
                    identifier,
                    title,
                    elementIndex));

            isValid = false;
        }

        // Rule 3: The serial number must be composed of invariant characters only.
#if NET7_0_OR_GREATER
        if (CharacterSet82Regex().Match(serialNumber).Success) {
#else
        if (CharacterSet82Regex.Match(serialNumber).Success) {
#endif
            return isValid;
        }

        if (packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 53)) {
            packIdentifier.AddException(
                new PackIdentifierFieldException(
                    53,
                    $"{Resources.Parser_Error_021} {Resources.Parser_Error_017}",
                    identifier,
                    title,
                    elementIndex));
        }

        return false;
    }

    /// <summary>
    ///   Perform post-processing on the pack identifier.
    /// </summary>
    /// <param name="packIdentifier">The pack identifier.</param>
    /// <returns>The post-processed pack identifier.</returns>
    private static IPackIdentifier PostProcessIdentifier(IPackIdentifier packIdentifier) {
        if (packIdentifier.Scheme != Scheme.Ifa) {
            return packIdentifier;
        }

        /* Remove any parser exceptions that specify the use of an invalid data set
         * according to MH10.8.2 standard for serial numbers and batch identifiers.
         * The IFA specification does not adhere to the standard in this respect, but
         * effectively overrides the standard. Hence, including these parser
         * exceptions simply highlights potential confusion based on this discrepency
         * between the two specifications.
         */
        if (packIdentifier.ParseExceptions is not List<ParseException> parseExceptionsList) {
            return packIdentifier;
        }

        const string batchIdTitle = "TRACEABILITY NUMBER ASSIGNED BY THE SUPPLIER";
        const string serialNumberTitle = "SERIAL NUMBER";

        // If there are no batch identifier errors at the pack level, remove any at the parser level.
        if (packIdentifier.Exceptions.All(ex => ex.ErrorNumber != 33) &&
            parseExceptionsList.RemoveAll(
                ex => ex.ErrorNumber is 3005 or 3100 &&
                      ex.DataElementTitle == batchIdTitle) >= 1 && parseExceptionsList.Count == 1) {
            // If the pack has any batch identifier parser errors, remove them
            parseExceptionsList.RemoveAll(ex => ex.ErrorNumber == 1003);
        }

        // If there are no batch identifier errors at the pack level, remove any at the parser level.
        if (packIdentifier.Exceptions.Any(ex => ex.ErrorNumber == 52)) {
            return packIdentifier;
        }

        // If the pack has any serial number parser errors, remove them
        if (parseExceptionsList.RemoveAll(
                ex => ex.ErrorNumber is 3005 or 3100
                   && ex.DataElementTitle == serialNumberTitle) < 1) {
            return packIdentifier;
        }

        if (parseExceptionsList.Count == 1) {
            parseExceptionsList.RemoveAll(ex => ex.ErrorNumber == 1003);
        }

        return packIdentifier;
    }

    /// <summary>
    ///   Gets the legal validity of the use of the symbology to encode a unique pack identifier.
    /// </summary>
    /// <remarks>
    ///   By law, unique identifiers must be encoded as Data Matrix barcodes with error
    ///   detection and correction equivalent to or higher than those of the Data Matrix
    ///   ECC200.
    /// </remarks>
    private static SymbologyValidity ValidSymbology(IBarcode barcode) =>
        barcode.BarcodeType switch {
            BarcodeType.NoIdentifier => SymbologyValidity.Unknown,
            BarcodeType.DataMatrix => barcode.BarcodeTypeModifier > 0
                                          ? SymbologyValidity.True
                                          : SymbologyValidity.False,
            _ => SymbologyValidity.False
        };

    // Comparer class for distint records used only in this contect
    private sealed class DistinctRecordComparer : IEqualityComparer<(Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers)> {
        public bool Equals(
            (Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers) x,
            (Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers) y) {
            return x.productCode == y.productCode && x.serialNumber == y.serialNumber;
        }

        public int GetHashCode(
            (Scheme scheme, string productCode, string serialNumber, string batchIdentifier, string expiry, IReadOnlyDictionary<NhrnMarket, string> nationalNumbers) obj) {
            return obj.productCode.GetHashCode(StringComparison.Ordinal)
                 ^ obj.serialNumber.GetHashCode(StringComparison.Ordinal);
        }
    }
}