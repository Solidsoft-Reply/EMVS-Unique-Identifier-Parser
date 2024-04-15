// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserResults.cs" company="Solidsoft Reply Ltd.">
//   (c) 2022 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A component displaying the results of parsing a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable S3358
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Components;

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleMvc;

using Properties;
using Parsers.EmvsUniqueIdentifier;
using Parsers.EmvsUniqueIdentifier.Packs;
using Parsers.Gs1Ai;
using static System.Console;

/// <summary>
/// A component displaying the results of parsing a barcode.
/// </summary>
internal class ParserResults : IComponent {

    /// <summary>
    /// Gets or sets the pre-processed input from the scanner.
    /// </summary>
    public string PreprocessedInput { get; set; }

    /// <summary>
    /// Gets or sets the pack identifier represented by the processed input from the scanner.
    /// </summary>
    public PackIdentifier PackIdentifier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a pack identifier was found.
    /// </summary>
    public bool PackIdentifierFound { get; set; }

    /// <summary>
    /// Get or sets the width of the parsed data.
    /// </summary>
    public int DataWidth { get; set; }

    /// <summary>
    /// Render the component in the console window.
    /// </summary>
    public void Render() {
        ResetColor();
        WriteLine(new string('\u2550', BufferWidth - 1));

        // Print the ruler.
        for (var count = 0; count <= (BufferWidth - 1) / 10; count++) {
            Write(count % 10);

            // ReSharper disable once ArrangeRedundantParentheses
            if (BufferWidth - 1 - (count * 10) <= 10) {
                // ReSharper disable once ArrangeRedundantParentheses
                Write(new string(' ', BufferWidth - 2 - (count * 10)));
                break;
            }

            Write(new string(' ', 9));
        }

        WriteLine();
        var inRecord = false;

        // Print the parsed and colourised buffer line.
        var bufferLine = PreprocessedInput;
        var characterCount = 0;
        var isFormatIdentifier = false;
        var isAimIdentifier = false;
        var isMessageHeader = false;

        for (var charPos = 0; charPos < bufferLine.Length; charPos++) {
            var character = bufferLine[charPos];

            switch (character) {
                case ']':
                    isAimIdentifier = charPos == 0;

                    if (isAimIdentifier) {
                        BackgroundColor = ConsoleColor.DarkBlue;
                        ForegroundColor = ConsoleColor.Yellow;
                    }

                    break;
                case '[':
                    isMessageHeader = charPos is 0 or 3;

                    if (isMessageHeader) {
                        ResetColor();
                        ForegroundColor = ConsoleColor.Gray;
                        isAimIdentifier = false;
                        characterCount = 0;
                    }

                    break;
                case (char)28:
                    character = '\u241c';
                    BackgroundColor = ConsoleColor.Cyan;
                    ForegroundColor = ConsoleColor.DarkRed;
                    inRecord = true;
                    characterCount = 0;
                    break;
                case (char)29:
                    character = '\u241d';
                    BackgroundColor = ConsoleColor.Cyan;
                    ForegroundColor = ConsoleColor.DarkRed;
                    inRecord = true;
                    characterCount = 0;
                    break;
                case (char)31:
                    character = '\u241f';
                    BackgroundColor = ConsoleColor.Cyan;
                    ForegroundColor = ConsoleColor.DarkRed;
                    inRecord = true;
                    characterCount = 0;
                    break;
                case (char)30:
                    character = '\u241e';
                    BackgroundColor = ConsoleColor.Magenta;
                    ForegroundColor = ConsoleColor.DarkGreen;
                    inRecord = false;
                    isFormatIdentifier = true;
                    break;
                case (char)4:
                    character = '\u2404';
                    BackgroundColor = ConsoleColor.Yellow;
                    ForegroundColor = ConsoleColor.DarkBlue;
                    inRecord = false;
                    break;
                default:
                    // Use Unicode Control Pictures for any other control character
                    if (character >= 0 && character <= 31) {
                        character = (char)(9216 + character);
                    }

                    var isPosFound = false;

                    if (inRecord) {
                        BackgroundColor = ConsoleColor.DarkGray;
                        ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        if (isAimIdentifier && ++characterCount <= 2) {
                            BackgroundColor = ConsoleColor.DarkBlue;
                            ForegroundColor = ConsoleColor.Yellow;
                            break;
                        }

                        isAimIdentifier = false;

                        if (isMessageHeader) {
                            if (++characterCount <= 2) {
                                ResetColor();
                                ForegroundColor = ConsoleColor.Gray;
                                break;
                            }

                            isMessageHeader = false;
                            characterCount = 0;
                        }

                        if (isFormatIdentifier && ++characterCount <= 2) {
                            ResetColor();
                            ForegroundColor = ConsoleColor.Gray;
                        }
                        else {
                            characterCount = 0;
                            isFormatIdentifier = false;
                            BackgroundColor = ConsoleColor.DarkGray;
                            ForegroundColor = ConsoleColor.White;
                        }
                    }

                    if (PackIdentifier != null) {
                        foreach (var record in PackIdentifier.Records) {
                            if (isPosFound) {
                                break;
                            }

                            if (!record.Elements.Any(
                                    element => charPos >= element.Position
                                               - element.Identifier.Length
                                               && charPos < element.Position)) {
                                continue;
                            }

                            BackgroundColor = ConsoleColor.Blue;
                            ForegroundColor = ConsoleColor.Cyan;
                            isPosFound = true;
                            inRecord = true;
                        }
                    }

                    break;
            }

            try {
                Write(character);
            }
            catch (EncoderFallbackException) {
                // If the characters are not recognised Unicode characters, the encoder (which is always UTF 8 in .NET Core)
                // will issue a fallback exception.  We have to reset the standard output to recover from this exception.
                // If this is not done, any further attempt to write to the console will fail.
                var standardOutput = new StreamWriter(OpenStandardOutput()) {
                    AutoFlush = true
                };

                SetOut(standardOutput);
                OutputEncoding = System.Text.Encoding.UTF8;

                ResetColor();
                WriteLine();
                WriteLine();
                BackgroundColor = ConsoleColor.Red;
                ForegroundColor = ConsoleColor.White;
                Write(Resources.DisplayParserResults_1);
                ResetColor();
                WriteLine();
                WriteLine();
                return;
            }
        }

        var identifier = PackIdentifier;

        ResetColor();
        WriteLine();
        WriteLine(new string('\u2550', BufferWidth - 1));

        if (PackIdentifierFound) {
            // Get the date as a date time
            var expiryDate = PackIdentifier.Expiry;

            try {
                if (PackIdentifier.Expiry.Length == 6 && int.TryParse(PackIdentifier.Expiry, out _)) {
                    if (!int.TryParse(PackIdentifier.Expiry[..2], out var year)) {
                        year = 0;
                    }

                    if (!int.TryParse(PackIdentifier.Expiry[2..4], out var month)) {
                        month = 0;
                    }

                    if (!int.TryParse(PackIdentifier.Expiry[4..6], out var day)) {
                        day = 0;
                    }

                    // Adhere to GS1 guidance for establishing the century - see GS1 General Specs 7.12
                    // Start by subtracting current year from reported year
                    var yearMinusThisYear = year - Convert.ToInt32(DateTime.Now.Year.ToInvariantString("0000")[2..], CultureInfo.InvariantCulture);
                    var century = Convert.ToInt32(
                        DateTime.Now.Year.ToInvariantString("0000")[..2] + "00",
                        CultureInfo.InvariantCulture);

                    century = yearMinusThisYear switch {
                        > 50 and < 100 => century - 100,
                        <= -50 and > -100 => century + 100,
                        _ => century
                    };

                    if (day == 0) {
                        day = DateTime.DaysInMonth(year + century, month);
                    }

                    var date = new DateTime(year + century, month, day, 0, 0, 0, DateTimeKind.Local);

                    expiryDate = PackIdentifier.Scheme == Scheme.Ifa
                        ? date.ToInvariantString(
                            day == DateTime.DaysInMonth(year + century, month)
                                // ReSharper disable once StringLiteralTypo
                                ? "MM.yyyy"
                                // ReSharper disable once StringLiteralTypo
                                : "dd.MM.yyyy")
                        : date.ToInvariantString(
                            day == DateTime.DaysInMonth(year + century, month)
                                // ReSharper disable once StringLiteralTypo
                                ? "MM/yyyy"
                                // ReSharper disable once StringLiteralTypo
                                : "dd/MM/yyyy");
                    DataWidth += expiryDate.Length - 6;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch {
                // Do nothing here
            }

            void WriteBarcodePanelDataLine(string label, string content) {
                Write(@" ");
                BackgroundColor = ConsoleColor.White;
                ForegroundColor = ConsoleColor.Blue;
                Write($@" {label}:{new string(' ', 9 - label.Length)}");
                ForegroundColor = ConsoleColor.Black;
                Write($@" {new string(' ', DataWidth - content.Length)}{content} ");
                ResetColor();
                WriteInvalidCharacterErrorMessage(content);
                WriteLine();
            }

            WriteLine();
            ResetColor();
            WriteBarcodePanelDataLine("PC", PackIdentifier.ProductCode);
            WriteBarcodePanelDataLine("SN", PackIdentifier.SerialNumber);
            WriteBarcodePanelDataLine(PackIdentifier.Scheme == Scheme.Gs1 ? "Lot" : "Ch-B", PackIdentifier.BatchIdentifier);
            // ReSharper disable once StringLiteralTypo
            WriteBarcodePanelDataLine(PackIdentifier.Scheme == Scheme.Gs1 ? "EXP" : "verw.bis", expiryDate);
            ResetColor();
            Write(@" " + PackIdentifier.Scheme.ToString().ToUpperInvariant());
            var padding = DataWidth - PackIdentifier.Scheme.ToString().Length;
            Write(new string(' ', PackIdentifier.IsValid ? padding + 5 : padding + 3));
        }
        else {
            WriteLine();
            Write(@" ");
        }

        var isValid = PackIdentifier.IsValid;
        BackgroundColor = isValid ? ConsoleColor.DarkGreen : ConsoleColor.Red;
        ForegroundColor = ConsoleColor.White;
        Write(isValid ? $" {Resources.Valid} " : $" {Resources.Invalid} ");
        ResetColor();
        Write(@"  ");
        BackgroundColor = PackIdentifier.Submit ? ConsoleColor.DarkGreen : ConsoleColor.Red;
        ForegroundColor = ConsoleColor.White;
        Write(
            PackIdentifier.Submit ? $" {Resources.SubmitToNationalSystem} " : $" {Resources.DoNotSubmitToNationalSystem} ");
        ResetColor();
        WriteLine();

        if (identifier.Exceptions.Any()) {
            WriteLine();

            foreach (var exception in identifier.Exceptions.OrderBy(exception => exception.ErrorNumber)) {
                ForegroundColor = ConsoleColor.Red;
                Write(Resources.DisplayParserResults_3);
                ResetColor();

                WriteLine($@"[{exception.ErrorNumber:00}] {exception.Message}");
            }
        }

        ResetColor();
        WriteLine(new string('\u2500', BufferWidth - 1));

        if (PackIdentifierFound) {
            if (identifier != null) {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (identifier.IssuingAgencyCountryCode) {
                    case CountryCode.UpcACompatibleGtin8:
                    case CountryCode.UpcACompatibleUnitedStatesAndCanada:
                    case CountryCode.UpcACompatibleRestrictedCirculation:
                    case CountryCode.UpcACompatibleUnitedStatesDrugs:
                    case CountryCode.UpcACompatibleUnitedStatesReserved:
                    case CountryCode.RestrictedCirculation:
                    case CountryCode.GlobalOffice:
                    case CountryCode.GeneralManagerNumber:
                    case CountryCode.UnitedKingdomOfficeGtin8Allocation:
                    case CountryCode.GlobalOfficeGtin8Allocation:
                    case CountryCode.SerialPublicationIssn:
                    case CountryCode.RefundReceipt:
                    case CountryCode.CouponIdentificationForCommonCurrencyArea:
                    case CountryCode.CouponIdentification:
                    case CountryCode.BooklandIsbn:
                    case CountryCode.BooklandIsbnIsmn:
                    case CountryCode.Unknown:
                        WriteLine(
                            $@" {identifier.IssuingAgencyCountryCode.GetCompanyPrefixDescription()}.");

                        break;
                    default:
                        var issuingAgencyMessage = string.Format(" " + Resources.IssuingAgencyCountry,
                            identifier.IssuingAgencyCountryCode.GetCompanyPrefixDescription());
                        WriteLine(
                            $@" {issuingAgencyMessage}");
                        break;
                }
            }

            foreach (var dataEntity in PackIdentifier.NationalNumbers) {
                Write($@" {dataEntity.Key} = {dataEntity.Value}");
                WriteLine();
            }

            WriteLine(new string('\u2500', BufferWidth - 1));
        }

        if (identifier.Records.Any()) {
            foreach (var record in identifier.Records) {
                foreach (var element in record.Elements) {
                    var elementIdentifier = string.IsNullOrWhiteSpace(element.Identifier)
                        ? string.Empty
                        : $" ({element.Identifier})";
                    var title = string.IsNullOrEmpty(element.Title) ? $"<{Resources.Unknown}>" : element.Title;
                    var data = string.IsNullOrEmpty(element.Data) ? $"<{Resources.Nothing}>" : element.Data;
                    Write($@"{elementIdentifier} {title} = {data}");
                    WriteLine();
                }
            }

            WriteLine(new string('\u2500', BufferWidth - 1));
        }

        if (identifier.ParseExceptions.Any()) {
            foreach (var exception in identifier.ParseExceptions) {
                ForegroundColor = ConsoleColor.Red;
                Write(Resources.DisplayParserResults_2);
                ResetColor();

                Write(
                    $@"[{exception.ErrorNumber:0000} @{exception.CharacterPosition:0000}] {exception.Message}");
                WriteLine();
            }

            WriteLine(new string('\u2500', BufferWidth - 1));
        }

        WriteLine();
        return;

        static void WriteInvalidCharacterErrorMessage(string content) {
            if (!content.Contains('\u25a1', StringComparison.InvariantCulture)) return;

            // ReSharper disable once LocalizableElement
            Write(" ");
            BackgroundColor = ConsoleColor.DarkBlue;
            ForegroundColor = ConsoleColor.White;
            Write(Resources.InvalidCharacterPlaceHolderMessage);
            ResetColor();
        }
    }
}