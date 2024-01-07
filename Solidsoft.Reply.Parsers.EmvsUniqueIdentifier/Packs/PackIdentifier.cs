// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackIdentifier.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd. All rights reserved.
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
// An EMVS pack identifier.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using Gs1Ai;

using Properties;

/// <summary>
///   An EMVS pack identifier.
/// </summary>
public class PackIdentifier : IPackIdentifier
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifier" /> class.
    /// </summary>
    public PackIdentifier()
    {
        Exceptions = new List<PackIdentifierException>();
        ParseExceptions = new List<ParseException>();
        NationalNumbers = new Dictionary<NhrnMarket, string>();
        Records = new List<IRecord>();
    }

    /// <summary>
    ///   Gets or sets the batch identifier.
    /// </summary>
    public string BatchIdentifier { get; set; } = string.Empty;

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<PackIdentifierException> Exceptions { get; }

    /// <summary>
    ///   Gets or sets the expiry date.
    /// </summary>
    public string Expiry { get; set; } = string.Empty;

    /// <summary>
    ///   Gets the indicator digit for trade item groupings. This value is between 1 and 8, or 9
    ///   for a variable measure trade item.
    /// </summary>
    public int Indicator =>
        Scheme == Scheme.Gs1 && ProductCode.Length == 14 && char.IsDigit(ProductCode[0])
            ? ProductCode[0].ToInt()
            : 0;

    /// <summary>
    ///   Gets the GS1 country associated with the product code. This is the country in which the company
    ///   or national identifier was assigned by an issuing agency, not necessarily the country of origin
    ///   of the pharmaceutical product.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public CountryCode IssuingAgencyCountryCode 
    {
        get
        {
            return Scheme == Scheme.Ifa
                       ? CountryCode.Germany
                       : ResolveGs1Country();

            CountryCode ResolveGs1Country() =>
                string.IsNullOrWhiteSpace(ProductCode)
                    ? CountryCode.Unknown
                    : ProductCode.ResolveGtinNtinToGs1Country();
        }
    }

    /// <summary>
    ///   Get a value indicating whether the product code represents a trade item grouping.
    /// </summary>
    public bool IsTradeItemGrouping =>
        Scheme == Scheme.Gs1 && ProductCode.Length == 14 && char.IsDigit(ProductCode[0])
     && ProductCode[0].ToInt() > 0 && ProductCode[0].ToInt() < 9;

    /// <summary>
    ///   Gets a value indicating whether the pack identifier is valid.
    /// </summary>
    /// <remarks>
    ///   Validity indicates if the barcode was parsed successfully without exceptions and
    ///   contains at least all four required data elements of a unique identifier.
    /// </remarks>
    public bool IsValid
    {
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        get
        {
            var parseExceptions = (List<ParseException>)ParseExceptions;
            var exceptions = (List<PackIdentifierException>)Exceptions;

            // Does the identifier contain fatal parse exceptions?
            if (parseExceptions.Exists(
                    ex => ex.IsFatal))
            {
                return false;
            }

            // Does the identifier contain a product code?
            if (string.IsNullOrEmpty(ProductCode) && 
                !Exceptions.Any(ex => ex.ErrorNumber is >= 10 and < 30))
            {
                var multipleProductCodes = Exceptions.Any(
                    ex => ex.ErrorNumber is 10 or 20 or 6);

                int GetIfaProductCodeErrorNumber() => Scheme == Scheme.Ifa ? 20 : 6;
                string GetIfaProductCodeElementId() => Scheme == Scheme.Ifa ? "9N" : string.Empty;
                string GetIfaProductCodeElementTitle() => Scheme == Scheme.Ifa ? "PPN" : string.Empty;

                AddException(
                    new PackIdentifierFieldException(
                        Scheme == Scheme.Gs1 ? 10 : GetIfaProductCodeErrorNumber(),
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Packs_Error_001,
                            multipleProductCodes ? Resources.SubstituteUnique : string.Empty),
                        Scheme == Scheme.Gs1 ? "01" : GetIfaProductCodeElementId(),
                        Scheme == Scheme.Gs1 ? "GTIN" : GetIfaProductCodeElementTitle(),
                        0));
            }

            // Did the parser fail to detect any batch identifier data elements?
            if (string.IsNullOrEmpty(BatchIdentifier) && 
                !Exceptions.Any(ex => ex.ErrorNumber is >= 30 and < 40))
            {
                var multipleBatchIdentifiers =
                    Exceptions.Any(ex => ex.ErrorNumber is 7 or 30);

                string GetIfaBatchElementId() => Scheme == Scheme.Ifa ? "1T" : string.Empty;
                string GetIfaBatchElementTitle() => Scheme == Scheme.Ifa 
                                                        ? "TRACEABILITY NUMBER ASSIGNED BY THE SUPPLIER" 
                                                        : string.Empty;

                AddException(
                    new PackIdentifierFieldException(
                        Scheme == Scheme.Unknown ? 7 : 30,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Packs_Error_002,
                            multipleBatchIdentifiers ? string.Empty : Resources.SubstituteUnique),
                        Scheme == Scheme.Gs1 ? "10" : GetIfaBatchElementId(),
                        Scheme == Scheme.Gs1 ? "BATCH/LOT" : GetIfaBatchElementTitle(),
                        0));
            }

            // Did the parser fail to detect any expiry date data elements?
            if (string.IsNullOrEmpty(Expiry) && 
                !Exceptions.Any(ex => ex.ErrorNumber is >= 40 and < 50))
            {
                var multipleExpiryDate = Exceptions.Any(ex => ex.ErrorNumber is 8 or 40);

                string GetIfaExpiryElementId() => Scheme == Scheme.Ifa ? "D" : string.Empty;
                string GetIfaExpiryElementTitle() => Scheme == Scheme.Ifa ? "DATE" : string.Empty;

                AddException(
                    new PackIdentifierFieldException(
                        Scheme == Scheme.Unknown ? 8 : 40,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Packs_Error_003,
                            multipleExpiryDate ? string.Empty : Resources.SubstituteUnique),
                        Scheme == Scheme.Gs1 ? "17" : GetIfaExpiryElementId(),
                        Scheme == Scheme.Gs1 ? "USE BY OR EXPIRY" : GetIfaExpiryElementTitle(),
                        0));
            }

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                return exceptions.Count <= 0;
            }

            // Did the parser fail to detect any serial number identifier data elements?
            if (Exceptions.Any(ex => ex.ErrorNumber is >= 50 and < 60))
            {
                return exceptions.Count <= 0;
            }

            var multipleSerialNumber = Exceptions.Any(ex => ex.ErrorNumber is 9 or 50);

            AddException(
                new PackIdentifierFieldException(
                    Scheme == Scheme.Unknown ? 9 : 50,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Packs_Error_004,
                        multipleSerialNumber ? string.Empty : Resources.SubstituteUnique),
                    Scheme == Scheme.Gs1 ? "21" : GetIfaSerialElementId(),
                    Scheme == Scheme.Gs1 ? "SERIAL" : GetIfaSerialElementTitle(),
                    0));

            // Does the identifier contain pack identifier exceptions?
            return exceptions.Count <= 0;

            string GetIfaSerialElementId() => Scheme == Scheme.Ifa ? "S" : string.Empty;

            string GetIfaSerialElementTitle() => Scheme == Scheme.Ifa ? "SERIAL NUMBER" : string.Empty;
        }
    }

    /// <summary>
    ///   Gets a value indicating whether the product code represents a variable measure trade item.
    /// </summary>
    public bool IsVariableMeasureTradeItem =>
        Scheme == Scheme.Gs1 && ProductCode.Length == 14 && char.IsDigit(ProductCode[0])
     && ProductCode[0].ToInt() == 9;

    /// <summary>
    ///   Gets the GS1-recognised NHRN national numbers.
    /// </summary>
    public IReadOnlyDictionary<NhrnMarket, string> NationalNumbers { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<ParseException> ParseExceptions { get; }

    /// <summary>
    ///   Gets or sets the product code.
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    ///   Gets a collection of records in the barcode.
    /// </summary>
    public IEnumerable<IRecord> Records { get; }

    /// <summary>
    ///   Gets or sets the scheme.
    /// </summary>
    public Scheme Scheme { get; set; }

    /// <summary>
    ///   Gets or sets the serial number.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    ///   Gets a value indicating whether the pack identifier is suitable for submission to a national
    ///   medicines verification system for verification, supply, decommission or reactivation.
    /// </summary>
    public bool Submit
    {
        get
        {
            // If the pack identifier is valid, it is suitable for submission
            if (IsValid)
            {
                return true;
            }

            // If the symbology is known to be an invalid carrier of the unique identifier
            // then any unique identifier it may carry is not a unique identifier in
            // terms of the law, and the data should not be submitted to the National
            // System.
            if (ValidSymbology == SymbologyValidity.False)
            {
                return false;
            }

            // If there is no product code, or there is no serial number, there is no merit in
            // submitting the pack to the national system as it does not contain sufficient
            // information to allow the National System to perform a lookup of its records.
            return !string.IsNullOrWhiteSpace(ProductCode) && !string.IsNullOrWhiteSpace(SerialNumber);

            // If there is a product code and a serial number, the pack should only be submitted.
            // There is sufficient information for the National System to perform a lookup of the
            // pack identifier, even if other data elements of the identifier (Batch identifier and
            // expiry date) are not present. The pack may prove to be suspicious when validated by
            // the national system, so should be submitted, even if it is invalid. If a client
            // application fails to submit the pack, it may prevent effective detection of
            // falsified and counterfeit medicines.
        }
    }

    /// <summary>
    ///   Gets or sets the legal validity of the use of the symbology to encode a unique pack identifier.
    /// </summary>
    /// <remarks>
    ///   By law, unique identifiers must be encoded as Data Matrix barcodes with error
    ///   detection and correction equivalent to or higher than those of the Data Matrix
    ///   ECC200.
    /// </remarks>
    public SymbologyValidity ValidSymbology { get; set; }

    /// <summary>
    ///   Adds a pack identifier exception to the exception collection.
    /// </summary>
    /// <param name="exception">The exception to be added.</param>
    public void AddException(PackIdentifierException exception)
    {
        if (exception is ParseException parseException)
        {
            ((List<ParseException>)ParseExceptions).Add(parseException);
        }
        else
        {
            if (Exceptions.All(ex => ex.ErrorNumber != exception.ErrorNumber))
            {
                ((List<PackIdentifierException>)Exceptions).Add(exception);
            }
        }
    }

    /// <summary>
    ///   Adds a GS1-recognised NHRN national number to the national number collection.
    /// </summary>
    /// <remarks>Any other national numbers will be found in the Elements collection of a record.</remarks>
    /// <param name="market">The market that assigned the NHRN.</param>
    /// <param name="nationalNumber">The NHRN national number to be added.</param>
    public void AddNationalNumber(NhrnMarket market, string nationalNumber)
    {
        ((Dictionary<NhrnMarket, string>)NationalNumbers).Add(market, nationalNumber);
    }

    /// <summary>
    ///   Adds or replaces a pack identifier exception to the exception collection.
    /// </summary>
    /// <param name="exception">The exception to be added.</param>
    public void AddOrReplaceException(PackIdentifierException exception)
    {
        if (exception is ParseException parseException)
        {
            var parseExceptions = (List<ParseException>)ParseExceptions;

            var sameNumberExceptions = from pe in parseExceptions
                                       where pe.ErrorNumber == exception.ErrorNumber
                                       select pe;

            var numberExceptions = sameNumberExceptions as ParseException[] ?? sameNumberExceptions.ToArray();

            if (numberExceptions.Any())
            {
                foreach (var sameNumberException in numberExceptions.ToList())
                {
                    parseExceptions.Remove(sameNumberException);
                }
            }

            AddException(parseException);
        }
        else
        {
            var exceptions = (List<PackIdentifierException>)Exceptions;

            var sameNumberExceptions = from e in exceptions where e.ErrorNumber == exception.ErrorNumber select e;

            var packIdentifierExceptions =
                sameNumberExceptions as PackIdentifierException[] ?? sameNumberExceptions.ToArray();

            if (packIdentifierExceptions.Any())
            {
                foreach (var sameNumberException in packIdentifierExceptions.ToList())
                {
                    exceptions.Remove(sameNumberException);
                }
            }

            AddException(exception);
        }
    }

    /// <summary>
    ///   Adds a barcode record to the record collection.
    /// </summary>
    /// <param name="record">The barcode record to be added.</param>
    public void AddRecord(IRecord record)
    {
        ((List<IRecord>)Records).Add(record);
    }

    /// <summary>
    ///   Resets the pack identifier fields, but leaves the records collection unchanged.
    /// </summary>
    public void ResetIdentifier()
    {
        Scheme = Scheme.Unknown;
        ProductCode = string.Empty;
        BatchIdentifier = string.Empty;
        Expiry = string.Empty;
        SerialNumber = string.Empty;
        ((Dictionary<NhrnMarket, string>)NationalNumbers).Clear();
    }
}