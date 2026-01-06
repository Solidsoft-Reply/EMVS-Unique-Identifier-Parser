// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Solidsoft Reply Ltd">
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
        Justification = "<Approved>",
        Scope = "type",
        Target = "~T:Solidsoft.Reply.Parsers.HighCapacityAidc.ParseStatus")]

[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434Analyzer.Analyze(System.String,System.Int32,System.Boolean@)~System.Collections.Generic.IList{Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IFormat}")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.UnicodeBlocks.ResolveScript(System.Collections.Generic.IEnumerable{System.String},System.Collections.Generic.IEnumerable{System.String})~System.String")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Calibration.UnicodeBlocks")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.IBarcode.IsRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Symbology.IDetector.Detect(System.String)~Solidsoft.Reply.BarcodeScanner.Symbology.ISymbologyId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.IDetector")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.Barcode.IsRecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.AimDetector")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.AimId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Telepen")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Codabar")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Codablock")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Pharmacode")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.BarcodeScanner.Symbology.BarcodeType.Other")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.ISymbologyId")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "type", Target = "~T:Solidsoft.Reply.BarcodeScanner.Symbology.SymbologyIdScheme")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Transportation")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.AscX12")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.UnEdifact")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Cii")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator.Binary")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities.CiiDataEntity.#ctor(Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.FormatIndicator,System.String,System.String,System.String,System.String,System.String,System.String,System.Int32,System.Int32)")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~P:Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities.CiiDataEntity.Organisation")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~F:Solidsoft.Reply.Parsers.HighCapacityAidc.ParseStatus.Unrecognised")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.SystemCapabilities.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.SystemCapabilities.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.BarcodeScanner.Calibration.AdviceItem.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.AdviceItem.GetHashCode~System.Int32")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.BaseParser.AssignIfaPackIdentifierFields(Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.IRecord,Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs.PackIdentifier,System.Collections.Generic.IReadOnlyDictionary{System.String,System.Collections.Generic.List{System.String}})~System.Boolean")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Approved>", Scope = "member", Target = "~M:Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Extensions.ToInt(System.Char)~System.Int32")]
