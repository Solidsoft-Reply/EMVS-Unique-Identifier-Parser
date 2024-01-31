// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage(
        "Naming",
        "CA1717:Only FlagsAttribute enums should have plural names",
        Justification = "<Pending>",
        Scope = "type",
        Target = "~T:Solidsoft.Reply.Parsers.HighCapacityAidc.ParseStatus")]

[assembly:
    SuppressMessage(
        "Build",
        "CA1801:Parameter backgroundColor of method CreateBarcode is never used. Remove the parameter or use it in the method body.",
        Justification = "<Pending>",
        Scope = "member",
        Target =
            "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.CreateBarcode(System.String,System.Drawing.Color,System.Drawing.Imaging.ImageFormat)~System.IO.Stream")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.Gs1Ai.Extensions.ResolveToGs1UpcACompatible(System.String)~Solidsoft.Reply.Parser.Gs1AiParser.Gs1CompanyPrefix")]
[assembly: SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.BarcodeParser.Parse(System.String,System.String@,Solidsoft.Reply.Parsers.HighCapacityAidc.Preprocessor)~Solidsoft.Reply.Parsers.HighCapacityAidc.IBarcode")]
[assembly: SuppressMessage("Major Code Smell", "S2234:Parameters should be passed in the correct order", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator.ResolveKeyboardScript(System.Collections.Generic.IReadOnlyList{System.String},System.Boolean)~System.String")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator.BarcodeProvenance(System.String,System.Boolean)~Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationBarcodeProvenance")]
[assembly: SuppressMessage("Minor Code Smell", "S1643:Strings should not be concatenated using '+' in a loop", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator.ConvertToSegments(Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationToken,System.String,System.Collections.Generic.IReadOnlyList{System.Collections.Generic.List{System.String}},System.Collections.Generic.List{System.Collections.Generic.List{System.String}}@)~Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationToken")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator.ProcessResolvedDataSequences(Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationToken,System.Collections.Generic.List{System.Collections.Generic.List{System.String}},System.Collections.Generic.IReadOnlyList{System.Collections.Generic.List{System.String}},Solidsoft.Reply.BarcodeScanner.Calibration.CaseConversionCharacteristics)")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator.FixUpExpectedReportedPrefix(System.Collections.Generic.List{System.String},System.Char)")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434Analyzer.Analyze(System.String,System.Int32,System.Boolean@)~System.Collections.Generic.IList{Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IFormat}")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.HighCapacityAidc.PreProcessors.Dom3KeyboardEventCodes.ConvertCodesToString(System.String)~System.String")]
[assembly: SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.AssignGs1PackIdentifierFields(Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IRecord,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.PackIdentifier,System.Collections.Generic.IReadOnlyDictionary{System.String,System.Collections.Generic.List{System.String}})~System.Boolean")]
[assembly: SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.AssignIfaPackIdentifierFields(Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IRecord,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.PackIdentifier,System.Collections.Generic.IReadOnlyDictionary{System.String,System.Collections.Generic.List{System.String}})~System.Boolean")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.HighCapacityAidc.PreProcessors.IsoIec15434Envelope.DoFixMissingControlCharacters(System.String,System.Text.RegularExpressions.Regex)~System.String")]
[assembly: SuppressMessage("Minor Code Smell", "S1643:Strings should not be concatenated using '+' in a loop", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationBarcodeData.ParseData(System.String)~System.Collections.Generic.IEnumerable{System.String}")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationBarcodeData.Segments~System.Collections.Generic.IEnumerable{System.String}")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.UnicodeBlocks.ResolveScript(System.Collections.Generic.IEnumerable{System.String},System.Collections.Generic.IEnumerable{System.String})~System.String")]
[assembly: SuppressMessage("Naming", "CA1700:Do not name enum values 'Reserved'", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.Parser.Gs1AiParser.Gs1CompanyPrefix")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Calibration.UnicodeBlocks")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.Parser.Gs1AiParser.Gs1CompanyPrefix")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.CreateBarcode(System.String)~System.IO.Stream")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~P:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.ElementXDimension")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.CreateBarcode(System.String,System.Drawing.Color)~System.IO.Stream")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.CreateBarcode(System.String,System.Drawing.Imaging.ImageFormat)~System.IO.Stream")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.Dispose")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.IBarcode.IsRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.SomeNonInvariantCharactersUnrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.SomeNonInvariantCharacterCombinationsUnrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.Format0506NotReliablyRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.AimNotRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.UnrecognisedData")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.SomeDeadKeyCombinationsUnrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.CreateBarcode(System.String,System.Drawing.Color,System.Drawing.Imaging.ImageFormat)~System.IO.Stream")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.DataMatrix.DataMatrixBarcode.Dispose(System.Boolean)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.Format0506NotRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.Format06GroupSeparatorMapping")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType.SomeCharactersUnrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Symbology.IDetector.Detect(System.String)~Solidsoft.Reply.BarcodeScanner.Symbology.ISymbologyId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.IDetector")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.Parsers.AnsiMhDi.AscEntityResolver")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.Barcode.IsRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.BarcodeParser.Parse(System.String,System.String@,Solidsoft.Reply.Parsers.HighCapacityAidc.Preprocessor)~Solidsoft.Reply.Parsers.HighCapacityAidc.IBarcode")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~P:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenExtendedData.UnrecognisedKeys")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenExtendedData.Equals(Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenExtendedData)~System.Boolean")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenExtendedData.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Calibration.KeyboardCalibrator")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.Unrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.LengthOrFirstDimensionMetresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.WidthDiameterOrSecondDimensionMetresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.DepthThicknessHeightOrThirdDimensionMetresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.AreaSquareMetresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.NetVolumeLitresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.NetVolumeCubicMetresVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.NetWeightPoundsVariableMeasureTradeItem")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.LengthOrFirstDimensionMetres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.WidthDiameterOrSecondDimensionMetres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.DepthThicknessHeightOrThirdDimensionMetres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.AreaSquareMetres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.LogisticVolumeLitres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.LogisticVolumeCubicMetres")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.KilogramPerSquareMetre")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.LogisticWeightPounds")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.GlobalServiceRelationNumberToIdentifyTheRelationshipBetweenAnOrganisationOfferingServicesAndTheProviderOfServices")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.Gs1Ai.Gs1ApplicationIdentifier.GlobalServiceRelationNumberToIdentifyTheRelationshipBetweenAnOrganisationOfferingServicesAndTheRecipientOfServices")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.Parsers.Gs1Ai.Gs1Parser")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.AimDetector")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.AimId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Telepen")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Codabar")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Codablock")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Pharmacode")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Other")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.ISymbologyId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.SymbologyIdScheme")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Transportation")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.AscX12")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.UnEdifact")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Cii")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Binary")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities.CiiDataEntity.#ctor(Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator,System.String,System.String,System.String,System.String,System.String,System.String,System.Int32,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities.CiiDataEntity.Organisation")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenExtendedData.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationToken.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.AnsiMhDi.AscDataIdentifier.Unrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.ParseStatus.Unrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.Advice.#ctor(Solidsoft.Reply.BarcodeScanner.Calibration.SystemCapabilities,System.Boolean)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.SystemCapabilities.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.SystemCapabilities.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationToken.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenData.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationData.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformation.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.AdviceItemToken.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.AdviceItem.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformationType")]
[assembly: SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.IsPpnValid(System.String,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IPackIdentifier,System.String,System.String,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.IsBatchIdentifierValid(System.String,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IPackIdentifier,System.String,System.String,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.IsGtinValid(System.String,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IPackIdentifier,System.String,System.String,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Parser.IsSerialNumberValid(System.String,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IPackIdentifier,System.String,System.String,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.AdviceItem.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.AdviceItemToken.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationData.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationInformation.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.CalibrationTokenData.OnError(System.Runtime.Serialization.StreamingContext,Newtonsoft.Json.Serialization.ErrorContext)")]
