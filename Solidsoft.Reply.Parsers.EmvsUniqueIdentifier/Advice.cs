// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Advice.cs" company="Solidsoft Reply Ltd">
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
// Provides an ordered sequence of advice items.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using BarcodeScanner.Calibration;

using Properties;

/// <summary>
///   Provides an ordered sequence of advice items.
/// </summary>
public class Advice : IAdvice<AdviceItem, AdviceType> {
    /// <summary>
    ///   An ordered list of advice items.
    /// </summary>
    private readonly List<AdviceItem> _adviceItems = new();

    /// <summary>
    ///   Initializes a new instance of the <see cref="Advice"/> class.
    /// </summary>
    /// <param name="systemCapabilities">The capabilities of the barcode scanner/computer combination.</param>
    /// <param name="adviceTerritory">Territory for which advice will be provided.</param>
    private Advice(
        SystemCapabilities? systemCapabilities,
        Territory adviceTerritory = Territory.Europe) {
        var lowSeverity = new List<AdviceItem>();
        var mediumSeverity = new List<AdviceItem>();
        var highSeverity = new List<AdviceItem>();

        if (systemCapabilities is null) {
            ArgumentNullException.ThrowIfNull(systemCapabilities);
        }

        var testGs1Only = !systemCapabilities.FormatSupportAssessed;

        AddAdviceItemToList(
            systemCapabilities.ScannerKeyboardPerformance switch {
                ScannerKeyboardPerformance.Low => ReportThatTheDataInputPerformanceIsVeryPoor(),
                ScannerKeyboardPerformance.Medium => ReportThatTheDataInputPerformanceIsSlowerThanExpected(),
                _ => null
            });

        // Get boolean values
        var unexpectedError = systemCapabilities.UnexpectedError;
        var testsSucceeded = systemCapabilities.TestsSucceeded;
        var dataReported = systemCapabilities.DataReported;
        var correctSequenceReported = systemCapabilities.CorrectSequenceReported;
        var completeDataReported = systemCapabilities.CompleteDataReported;
        var keyboardLayoutsCorrespondForInvariantCharacters = systemCapabilities.KeyboardLayoutsCorrespondForInvariants;
        var keyboardLayoutsCorrespondForNonInvariantCharacters = systemCapabilities.KeyboardLayoutsCorrespondForNonInvariantCharacters;
        var keyboardLayoutsCanRepresentGroupSeparatorWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentGroupSeparatorWithoutMapping;
        var keyboardLayoutsCanRepresentRecordSeparatorWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentRecordSeparatorWithoutMapping;
        var keyboardLayoutsCanRepresentFileSeparatorWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentFileSeparatorWithoutMapping;
        var keyboardLayoutsCanRepresentUnitSeparatorWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentUnitSeparatorWithoutMapping;
        var keyboardLayoutsCanRepresentEotWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentEotWithoutMapping;
        var keyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping = systemCapabilities.KeyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping;
        var keyboardLayoutsCorrespondForAimIdentifier = systemCapabilities.KeyboardLayoutsCorrespondForAimIdentifier;
        var canReadInvariantCharactersReliably = systemCapabilities.CanReadInvariantsReliably;
        var canReadFormat05AndFormat06Reliably = systemCapabilities.CanReadFormat05AndFormat06Reliably;
        var canReadEdiReliably = systemCapabilities.CanReadEdiReliably;
        var canReadAscii28Reliably = systemCapabilities.CanReadFileSeparatorsReliably;
        var canReadAscii31Reliably = systemCapabilities.CanReadUnitSeparatorsReliably;
        var canReadAscii04Reliably = systemCapabilities.CanReadEotReliably;
        var canReadAimIdentifiersReliably = systemCapabilities.CanReadAimIdentifiersReliably;
        var canReadNonInvariantCharactersReliably = systemCapabilities.CanReadAdditionalAsciiCharactersReliably;
        var scannerTransmitsAimIdentifiers = systemCapabilities.ScannerTransmitsAimIdentifiers;
        var scannerTransmitsEndOfLineSequence = systemCapabilities.ScannerTransmitsEndOfLineSequence;
        var scannerTransmitsAdditionalPrefix = systemCapabilities.ScannerTransmitsAdditionalPrefix;
        var scannerTransmitsAdditionalSuffix = systemCapabilities.ScannerTransmitsAdditionalSuffix;
        var scannerMayConvertToUpperCase = systemCapabilities.ScannerMayConvertToUpperCase.GetValueOrDefault();
        var scannerMayConvertToLowerCase = systemCapabilities.ScannerMayConvertToLowerCase.GetValueOrDefault();
        var scannerMayInvertCase = systemCapabilities.ScannerMayInvertCase.GetValueOrDefault();
        var scannerMayCompensateForCapsLock = systemCapabilities.ScannerMayCompensateForCapsLock.GetValueOrDefault();
        var keyboardScriptDoesNotSupportCase = systemCapabilities.KeyboardScriptDoesNotSupportCase;
        var aimIdentifierUncertain = systemCapabilities.AimIdentifierUncertain;
        var calibrationAssumption = systemCapabilities.Assumption;
        var deadKeys = systemCapabilities.DeadKeys;
        var platform = systemCapabilities.Platform;

        /* To facilitate reasoning over this code, I've used verbosely named methods to represent boolean expressions and
           I have nested ternary operators quite deeply in some places. "The code is the documentation".
        */

        // AdviceTypes: 100, 105, 115 (Calibration)
        AddAdviceItemToList(
            IfTheTestSucceeded()
            && IfWeCanReadUniqueIdentifiersReliably()
            && IfWeAssumeCalibration()
                ? IfAdviceIsNotProvidedSpecificallyForGermany()
                    ? IfWeOmittedThePpnTest()
                        ? ReportThatUniqueIdentifiersAreReadReliablyButThePpnTestWasOmitted()                               // 105
                        : IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                            ? IfWeCanReadFormat05AndFormat06Reliably()
                                ? ReportThatInvariantCharactersAreReadReliably()                                            // 100
                                : ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesAreNotReadReliably()              // 115
                            : null
                    : IfWeCanReadFormat05AndFormat06Reliably()
                        ? ReportThatInvariantCharactersAreReadReliably()                                                    // 100
                        : null
                : null);

        // AdviceTypes: 100, 105, 110, 115 (Not Calibration)
        AddAdviceItemToList(
            IfTheTestSucceeded()
            && IfWeCanReadUniqueIdentifiersReliably()
            && IfTheKeyboardLayoutsCorrespondForUniqueIdentifiers()
            && IfTheKeyboardLayoutsCanRepresentGroupSeparatorsWithoutMapping()
            && IfWeDoNotAssumeCalibration()
                ? IfAdviceIsNotProvidedSpecificallyForGermany()
                  && IfWeOmittedThePpnTest()
                    ? ReportThatUniqueIdentifiersAreReadReliablyButThePpnTestWasOmitted()                                   // 105
                    : IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                        ? IfWeCanReadFormat05AndFormat06Reliably()
                            ? IfTheKeyboardLayoutsCanRepresentRecordSeparatorsWithoutMapping()
                                ? ReportThatInvariantCharactersAreReadReliably()                                            // 100
                                : IfWeAssumeAgnosticism()
                                    ? IfAdviceIsNotProvidedSpecificallyForGermany()
                                        ? ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesMayNotBeReadReliably()    // 110
                                        : null
                                    : IfAdviceIsNotProvidedSpecificallyForGermany()
                                        ? ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesAreNotReadReliably()      // 115
                                        : null
                            : IfAdviceIsNotProvidedSpecificallyForGermany()
                                ? ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesAreNotReadReliably()              // 115
                                : null
                        : ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesMayNotBeReadReliably()                    // 110
                : null);

        // AdviceType: 200
        AddAdviceItemToList(
            IfWeDoNotAscertainThatTheScannerTransmitsAimIdentifiers()
                ? ReportThatTheBarcodeScannerDoesNotTransmitAimIdentifiers()                                                // 200
                : null);

        // AdviceType: 205, 206
        AddAdviceItemToList(
            IfCapsLockIsOn()
            && IfWeDoNotAscertainThatTheKeyboardScriptDoesNotSupportCase()
                ? IfTheCurrentPlatformIsMacintosh()
                    ? ReportThatCapsLockIsSwitchedOnOnMacOsButCaseIsPreserved()                                             // 206
                    : IfScannerMayCompensateForCapsLock()
                        ? ReportThatCapsLockIsSwitchedOnButCaseIsReportedCorrectly()                                        // 205
                        : null
                : null);

        // AdviceType: 210
        AddAdviceItemToList(
            IfCapsLockIsOn()
            && IfTheKeyboardScriptDoesNotSupportCase()
                ? ReportThatCapsLockIsSwitchedOnButScriptDoesNotSupportCase()                                               // 210
                : null);

        // AdviceType: 215
        AddAdviceItemToList(
            IfWeDoNotAscertainThatTheScannerTransmitsAnEndOfLineSequence()
                ? ReportThatTheScannerDoesNotTransmitAnEndOfLineSequence()                                                  // 215
                : null);

        // AdviceType: 220
        AddAdviceItemToList(
            IfScannerTransmitsAnAdditionalPrefix()
                ? ReportThatTheScannerTransmitsAPrefix()                                                                    // 220
                : null);

        // AdviceType: 225
        AddAdviceItemToList(
            IfScannerTransmitsAnAdditionalSuffix()
                ? ReportThatTheScannerTransmitsASuffix()                                                                    // 225
                : null);

        // AdviceTypes: 230, 231, 235
        AddAdviceItemToList(
            IfWeDoNotAscertainThatTheKeyboardLayoutsCorrespondForAimIdentifierFlagCharacter()
                ? IfWeCannotReadAimIdentifiersReliably()
                    ? ReportThatWeCannotReadAimIdentifiers()                                                                // 235
                    : IfWeAssumeAgnosticism()
                            ? ReportThatWeMayNotReadAimIdentifiersAssumingAgnosticism()                                     // 230
                            : IfWeAssumeNoCalibration()
                                ? ReportThatWeMayNotReadAimIdentifiersAssumingNoCalibration()                               // 231
                                : null
                : null);

        // AdviceTypes: 232
        AddAdviceItemToList(
            IfWeDoNotAscertainThatTheKeyboardLayoutsCorrespondForAimIdentifierFlagCharacter()
            && IfThereIsUncertaintyAboutTheDetectedAimIdentifier()
                ? ReportThatTheBarcodeScannerMayNotTransmitAimIdentifiers()                                                 // 232
                : null);

        // AdviceTypes: 240, 241, 245
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                ? IfWeCanReadFormat05AndFormat06Reliably()
                    ? (IfWeKnowIfTheKeyboardLayoutsCanRepresentRecordSeparatorsWithoutMapping() || IfWeKnowIfWeCanReadUniqueIdentifiersReliably())
                      && IfTheKeyboardLayoutsCannotRepresentRecordSeparatorsWithoutMapping()
                      && IfWeCanReadUniqueIdentifiersReliably()
                        ? IfWeAssumeAgnosticism()
                            ? ReportThatFormat05OrFormat06MayNotBeReadReliablyAssumingAgnosticism()                         // 240
                            : IfWeAssumeNoCalibration()
                                ? ReportThatFormat05OrFormat06MayNotBeReadReliablyAssumingNoCalibration()                   // 241
                                : null
                        : null
                    : ReportThatFormat05OrFormat06AreNotReadReliably()                                                      // 245
                : null);

        // AdviceType: 250
        AddAdviceItemToList(
            IfWeOmittedThePpnTest()
                ? ReportThatWeDidNotTestForIsoIec15434()                                                                    // 250
                : null);

        // AdviceType: 260, 261, 265
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeDoNotAscertainThatTheKeyboardLayoutsCorrespondsForAdditionalAsciiCharacters()
                ? IfWeCannotReadAdditionalAsciiCharactersReliably()
                    ? ReportThatTheSystemCannotReadAdditionalDataReliably()                                                 // 265
                    : IfWeAssumeAgnosticism()
                        ? ReportThatNonAdditionalDataMayNotBeReadReliablyAssumingAgnosticism()                              // 260
                        : IfWeAssumeNoCalibration()
                            ? ReportThatNonAdditionalDataMayNotBeReadReliablyAssumingNoCalibration()                        // 261
                            : null
                : null);

        // AdviceType: 270, 271, 275
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeDoNotAscertainThatTheKeyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping()
                ? IfWeCannotReadEdiCharactersReliably()
                    ? ReportThatTheSystemCannotReadEdiCharactersReliably()                                                  // 275
                    : IfWeAssumeAgnosticism()
                        ? ReportThatEdiCharactersMayNotBeReadReliablyAssumingAgnosticism()                                  // 270
                        : IfWeAssumeNoCalibration()
                            ? IfTheKeyboardLayoutsCannotRepresentEdiSeparatorsWithoutMapping()
                                ? ReportThatTheSystemCannotReadEdiCharactersReliably()                                      // 275
                                : ReportThatEdiCharactersMayNotBeReadReliablyAssumingNoCalibration()                        // 271
                            : null
                : IfWeCannotReadEdiCharactersReliably()
                    ? ReportThatTheSystemCannotReadEdiCharactersReliably()                                                  // 275
                    : null);

        // AdviceType: 276, 277
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeCannotReadAscii28CharactersReliably()
                ? ReportThatTheSystemCannotReadAscii28CharactersReliably()                                                  // 277
                : IfTheKeyboardLayoutsCannotRepresentFileSeparatorsWithoutMapping()
                    ? IfWeAssumeAgnosticism()
                        ? ReportThatTheSystemMayNotReadAscii28CharactersReliably()                                          // 276
                        : IfWeAssumeNoCalibration()
                            ? ReportThatTheSystemCannotReadAscii28CharactersReliably()                                      // 277
                            : null
                    : null);

        // AdviceType: 278, 279
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeCannotReadAscii31CharactersReliably()
                ? ReportThatTheSystemCannotReadAscii31CharactersReliably()                                                  // 279
                : IfTheKeyboardLayoutsCannotRepresentUnitSeparatorsWithoutMapping()
                    ? IfWeAssumeAgnosticism()
                        ? ReportThatTheSystemMayNotReadAscii31CharactersReliably()                                          // 278
                        : IfWeAssumeNoCalibration()
                            ? ReportThatTheSystemCannotReadAscii31CharactersReliably()                                      // 279
                            : null
                    : null);

        // AdviceType: 280, 281
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeCannotReadAscii04CharactersReliably()
                ? ReportThatTheSystemCannotReadAscii04CharactersReliably()                                                  // 281
                : IfTheKeyboardLayoutsCannotRepresentEotWithoutMapping()
                    ? IfWeAssumeNoCalibration()
                        ? ReportThatTheSystemCannotReadAscii04CharactersReliably()                                          // 281
                        : IfWeAssumeAgnosticism()
                            ? ReportThatTheSystemMayNotReadAscii04CharactersReliably()                                      // 280
                            : null
                    : null);

        // AdviceType: 300
        AddAdviceItemToList(
            IfNoUnexpectedErrorOccurred()
            && IfTestDidNotSucceed()
            && IfDataWasReported()
                ? ReportThatTheTestFailed()                                                                                 // 300
                : null);

        // AdviceType: 301, 304
        AddAdviceItemToList(
            IfNoDataWasReported()
                ? IfDeadKeyBarcodesWereGeneratedDuringCalibration()
                    ? ReportThatNoScannedDataWasReportedForDeadKeyBarcodes()                                                // 304
                    : ReportThatNoScannedDataWasReportedForBaseLineBarcode()                                                // 301
                : null);

        // AdviceType: 303, 306
        AddAdviceItemToList(
            IfNoUnexpectedErrorOccurred()
            && IfDataWasOnlyPartiallyReported()
                ? IfDeadKeyBarcodesWereGeneratedDuringCalibration()
                    ? ReportThatScannedDataWasPartiallyReportedForDeadKeyBarcodes()                                         // 306
                    : ReportThatScannedDataWasPartiallyReportedForBaselineBarcode()                                         // 303
                : null);

        // AdviceType: 305
        AddAdviceItemToList(
            IfBarcodesWereScannedInAnIncorrectSequence()
                ? ReportThatUserScannedADeadKeyBarcodeOutOfSequence()                                                       // 305
                : null);

        // AdviceTypes: 307, 308
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfTheKeyboardLayoutsDoNotCorrespondForUniqueIdentifiers()
                ? IfWeAssumeAgnosticism()
                    ? IfWeKnowIfWeCanReadUniqueIdentifiersReliably()
                      || IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                        ? IfWeCanReadUniqueIdentifiersReliably()
                          && IfWeCanReadFormat05AndFormat06Reliably()
                            ? ReportThatLayoutsDoNotMatch()                                                                 // 307
                            : IfWeAssumeNoCalibration()
                                ? ReportThatLayoutsDoNotMatchForNoCalibrationAssumption()                                   // 308
                                : null
                        : null
                    : IfWeAssumeNoCalibration()
                        ? ReportThatLayoutsDoNotMatchForNoCalibrationAssumption()                                           // 308
                        : null
                : null);

        // AdviceTypes: 309, 310
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfAdviceIsNotProvidedSpecificallyForGermany()
            && IfTheKeyboardLayoutsCorrespondForUniqueIdentifiers()
            && IfTheKeyboardLayoutsCannotRepresentGroupSeparatorsWithoutMapping()
                ? IfWeAssumeAgnosticism()
                    ? IfWeKnowIfWeCanReadUniqueIdentifiersReliably()
                      || IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                        ? IfWeCanReadUniqueIdentifiersReliably()
                          && IfWeCanReadFormat05AndFormat06Reliably()
                            ? ReportThatHiddenCharactersAreNotRepresentedCorrectly()                                        // 309  // CHECK!
                            : null
                        : null
                    : IfWeAssumeNoCalibration()
                        ? ReportThatHiddenCharactersAreNotRepresentedCorrectlyAssumingNoCalibration()                       // 310
                        : null
                : null);

        // AdviceTypes: 311, 312
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfAdviceIsProvidedSpecificallyForGermany()
            && IfTheKeyboardLayoutsCorrespondForUniqueIdentifiers()
            && (IfTheKeyboardLayoutsCannotRepresentGroupSeparatorsWithoutMapping() || IfTheKeyboardLayoutsCannotRepresentRecordSeparatorsWithoutMapping())
                ? IfWeAssumeAgnosticism()
                    ? IfWeKnowIfWeCanReadUniqueIdentifiersReliably() || IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                        ? IfWeCanReadUniqueIdentifiersReliably()
                          && IfWeCanReadFormat05AndFormat06Reliably()
                            ? ReportThatHiddenCharactersAreNotRepresentedCorrectlyForGermany()                              // 311
                            : null
                        : null
                    : IfWeAssumeNoCalibration()
                        ? ReportThatHiddenCharactersAreNotRepresentedCorrectlyAssumingNoCalibrationForGermany()             // 312
                        : null
                : null);

        // AdviceType: 315, 316
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeAssumeAgnosticism()
            && IfWeCanReadUniqueIdentifiersReliably()
            && IfWeCannotReadFormat05AndFormat06Reliably()
                ? IfWeKnowIfKeyboardLayoutsCorrespondForUniqueIdentifiers()
                    ? IfTheKeyboardLayoutsCorrespondForUniqueIdentifiers()
                        ? IfTheKeyboardLayoutsCannotRepresentGroupSeparatorsWithoutMapping()
                            ? ReportThatHiddenCharactersAreNotReportedCorrectlyAndPpnBarcodesCannotBeReadReliably()         // 316
                            : null
                        : ReportThatLayoutsDoNotMatchAndPpnBarcodesCannotBeReadReliably()                                   // 315
                    : IfTheKeyboardLayoutsCannotRepresentGroupSeparatorsWithoutMapping()
                        ? ReportThatHiddenCharactersAreNotReportedCorrectlyAndPpnBarcodesCannotBeReadReliably()             // 316
                        : null
                : null);

        // Advice Type: 320
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeDoNotAscertainThatWeCanReadUniqueIdentifiersReliably()
                ? ReportThatSystemCannotReadUniqueIdentifiersReliably()                                                     // 320
                : null);

        // AdviceType: 325, 326, 327, 328
        AddAdviceItemToList(
            IfCapsLockIsOn()
            && IfWeDoNotAscertainThatTheKeyboardScriptDoesNotSupportCase()
                ? IfScannerMayConvertToUpperCase()
                    ? ReportThatCapsLockIsOnAndSystemConvertsToUpperCases()                                                 // 327
                    : IfScannerMayConvertToLowerCase()
                        ? ReportThatCapsLockIsOnAndSystemConvertsToLowerCases()                                             // 328
                        : IfTheCurrentPlatformIsMacintosh()
                            ? ReportThatCapsLockIsSwitchedOnForMacintosh()                                                  // 326
                            : ReportThatCapsLockIsSwitchedOn()                                                              // 325
                : null);

        // AdviceType: 330, 331, 332
        AddAdviceItemToList(
            IfCapsLockIsOff()
            && IfWeDoNotAscertainThatTheKeyboardScriptDoesNotSupportCase()
                ? IfScannerMayInvertCase()
                    ? ReportThatSystemConvertsUpperAndLowerCases()                                                          // 330
                    : IfScannerMayConvertToUpperCase()
                        ? ReportThatSystemConvertsToUpperCase()                                                             // 331
                        : IfScannerMayConvertToLowerCase()
                            ? ReportThatSystemConvertsToLowerCase()                                                         // 332
                            : null
                : null);

        // Advice Type 335
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfWeCannotReadUniqueIdentifiersReliably()
            && IfTheKeyboardScriptDoesNotSupportCase()
                ? ReportBarcodesCannotBeReadReliablyForKeyboardScriptThatDoesNotSupportCase()                               // 335
                : null);

        // AdviceType: 350, 351, 355
        AddAdviceItemToList(
            IfDataWasFullyReported()
            && IfAdviceIsProvidedSpecificallyForGermany()
            && IfWeKnowIfWeCanReadFormat05AndFormat06Reliably()
                ? IfWeCanReadFormat05AndFormat06Reliably()
                    ? IfTheKeyboardLayoutsCannotRepresentRecordSeparatorsWithoutMapping() &&
                      IfWeCanReadUniqueIdentifiersReliably()
                        ? IfWeAssumeAgnosticism()
                            ? ReportIncorrectRepresentationOfPpnRecordSeparators()                                          // 350
                            : IfWeAssumeNoCalibration()
                                ? ReportIncorrectRepresentationOfPpnRecordSeparatorsAssumingNoCalibration()                 // 351
                                : null
                        : null
                    : ReportThatPpnBarcodesCannotBeReadReliably()                                                           // 355
                : null);

        // AdviceType: 390
        AddAdviceItemToList(
            IfUnexpectedErrorOccurred()
                ? ReportThatAnUnexpectedErrorWasReported()
                : null);

        // General fix-up for other issues
        // Even if German PPN barcode tests are not selected, it is possible to detect incompatibility
        // with PPN barcodes - e.g., if [ is detected as an ambiguous character. If we report a PPN
        // issue, it feels redundant and confusing to an end user to warn them that they didn't run the
        // PPN tests. So, we will remove the warning if this occurs.
        if ((from ppnTestWarning in mediumSeverity
             let isPpnError = (from ppnError in highSeverity
                               where ppnError.AdviceType is AdviceType.RecordSeparatorIncorrectlyRepresentedGermany
                                                         or AdviceType.RecordSeparatorIncorrectlyRepresentedNoCalibrationGermany
                                                         or AdviceType.CannotReadPpnReliablyGermany
                                                         or AdviceType.LayoutsDoNotMatchNoPpn
                                                         or AdviceType.HiddenCharactersNotRepresentedCorrectlyNoPpn
                               select ppnError).Any()
             let isPpnWarning = (from ppnWarning in mediumSeverity
                                 where ppnWarning.AdviceType is AdviceType.MayNotReadPpn
                                                             or AdviceType.MayNotReadPpnNoCalibration
                                                             or AdviceType.CannotReadPpnReliably
                                 select ppnWarning).Any()
             let isPpnInfo = (from ppnInfo in lowSeverity
                              where ppnInfo.AdviceType is AdviceType.ReadsUniqueIdentifiersReliablyNoPpnTest
                                                       or AdviceType.ReadsUniqueIdentifiersReliablyMayNotReadPpn
                                                       or AdviceType.ReadsUniqueIdentifiersReliablyExceptPpn
                              select ppnInfo).Any()
             where (isPpnError || isPpnWarning || isPpnInfo) && ppnTestWarning.AdviceType == AdviceType.NoPpnTest
             select ppnTestWarning).Any()) {
            mediumSeverity.RemoveAll(item => item.AdviceType == AdviceType.NoPpnTest);
        }

        // Some PPN-related warnings duplicate others. This redundancy should be removed.
        var layoutsDoNotMatchNoPpn = highSeverity.Find(a => a.AdviceType == AdviceType.LayoutsDoNotMatchNoPpn);
        var hiddenCharactersNotRepresentedCorrectlyNoPpn = highSeverity.Find(a => a.AdviceType == AdviceType.HiddenCharactersNotRepresentedCorrectlyNoPpn);

        if (layoutsDoNotMatchNoPpn is not null || hiddenCharactersNotRepresentedCorrectlyNoPpn is not null) {
            var cannotReadPpnReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliably);
            var cannotReadPpnReliablyGermany = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliablyGermany);

            if (cannotReadPpnReliably is not null) {
                mediumSeverity.Remove(cannotReadPpnReliably);
            }

            if (cannotReadPpnReliablyGermany is not null) {
                mediumSeverity.Remove(cannotReadPpnReliablyGermany);
            }
        }

        // If the advice territory is Germany, there is no need to include 355 if 320 is already included.
        if (adviceTerritory == Territory.Germany) {
            var cannotReadPpnReliablyGermany = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliablyGermany);
            var cannotReadUniqueIdentifiersReliably = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadUniqueIdentifiersReliably);

            if (cannotReadPpnReliablyGermany is not null && cannotReadUniqueIdentifiersReliably is not null) {
                highSeverity.Remove(cannotReadPpnReliablyGermany);
            }
        }

        // Fix up the situation where the system reports that the system is changing case (upper to lower, lower to upper)
        // and also that the system is compensating. Also, remove any other advice about unreliable reads.
        var convertsToUpperCase = highSeverity.Find(a => a.AdviceType == AdviceType.ConvertsToUpperCase);
        var convertsToLowerCase = highSeverity.Find(a => a.AdviceType == AdviceType.ConvertsToLowerCase);
        var capsLockOnConvertsToUpperCase = highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOnConvertsToUpperCase);
        var capsLockOnConvertsToLowerCase = highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOnConvertsToLowerCase);

        if (convertsToUpperCase is not null || convertsToLowerCase is not null || capsLockOnConvertsToUpperCase is not null || capsLockOnConvertsToLowerCase is not null) {
            var capsLockCompensation = mediumSeverity.Find(a => a.AdviceType == AdviceType.CapsLockCompensation);

            if (capsLockCompensation is not null) {
                mediumSeverity.Remove(capsLockCompensation);
            }

            var cannotReadUniqueIdentifiersReliably = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadUniqueIdentifiersReliably);
            if (cannotReadUniqueIdentifiersReliably is not null) {
                highSeverity.Remove(cannotReadUniqueIdentifiersReliably);
            }

            var recordSeparatorIncorrectlyRepresentedGermany = highSeverity.Find(a => a.AdviceType == AdviceType.RecordSeparatorIncorrectlyRepresentedGermany);
            if (recordSeparatorIncorrectlyRepresentedGermany is not null) {
                highSeverity.Remove(recordSeparatorIncorrectlyRepresentedGermany);
            }

            var mayNotReadAim = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAim);
            if (mayNotReadAim is not null) {
                mediumSeverity.Remove(mayNotReadAim);
            }

            var cannotReadAimNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAimNoCalibration);
            if (cannotReadAimNoCalibration is not null) {
                mediumSeverity.Remove(cannotReadAimNoCalibration);
            }

            var cannotReadAim = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAim);
            if (cannotReadAim is not null) {
                mediumSeverity.Remove(cannotReadAim);
            }

            var mayNotReadPpn = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadPpn);
            if (mayNotReadPpn is not null) {
                mediumSeverity.Remove(mayNotReadPpn);
            }

            var mayNotReadPpnNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadPpnNoCalibration);
            if (mayNotReadPpnNoCalibration is not null) {
                mediumSeverity.Remove(mayNotReadPpnNoCalibration);
            }

            var cannotReadPpnReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliably);
            if (cannotReadPpnReliably is not null) {
                mediumSeverity.Remove(cannotReadPpnReliably);
            }

            var mayNotReadAdditionalDataReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAdditionalDataReliably);
            if (mayNotReadAdditionalDataReliably is not null) {
                mediumSeverity.Remove(mayNotReadAdditionalDataReliably);
            }

            var mayNotReadAdditionalDataNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAdditionalDataNoCalibration);
            if (mayNotReadAdditionalDataNoCalibration is not null) {
                mediumSeverity.Remove(mayNotReadAdditionalDataNoCalibration);
            }

            var cannotReadAdditionalData = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAdditionalData);
            if (cannotReadAdditionalData is not null) {
                mediumSeverity.Remove(cannotReadAdditionalData);
            }
        }

        // Fix up the issue with CAPS LOCK where, if CAPS LOCK is reported as being on, but the system also
        // determines that te scanner appears to be automatically compensating for this, we don't need to
        // report the CAPS LOCK being on, as this is subsumed into the information about compensation.
        var capsLockOn = highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOn);
        if (capsLockOn is not null) {
            var capsLockCompensation = mediumSeverity.Find(a => a.AdviceType == AdviceType.CapsLockCompensation);
            if (capsLockCompensation is not null) {
                highSeverity.Remove(capsLockOn);
            }
        }

        // Fix up test failures. If a 301, 303, 304, 305, 306, 320, 327, 328, 331 or 332 error occurs,
        // remove the 300 error, as this is redundant.
        var noDataReported = highSeverity.Find(a => a.AdviceType == AdviceType.NoDataReported);
        var partialDataReported = highSeverity.Find(a => a.AdviceType == AdviceType.PartialDataReported);

        if (noDataReported is not null ||
            partialDataReported is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.NoDataReportedDeadKeys) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.IncorrectSequenceDeadKeys) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.PartialDataReportedDeadKeys) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadUniqueIdentifiersReliably) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOnConvertsToUpperCase) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOnConvertsToLowerCase) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.ConvertsToUpperCase) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.ConvertsToLowerCase) is not null) {
            var testsFailed = highSeverity.Find(a => a.AdviceType == AdviceType.TestFailed);

            if (testsFailed is not null) {
                highSeverity.Remove(testsFailed);
            }

            if (noDataReported is not null && partialDataReported is not null) {
                highSeverity.Remove(partialDataReported);
            }

            if (noDataReported is not null || partialDataReported is not null) {
                // Remove any report about AIM identifiers, additional characters or control characters.
            }
        }

        // If the calibrator determines that your system cannot read AIM identifier characters, then it
        // should not report that your barcode scanner does not transmit AIM identifiers, as it cannot
        // determine this for certain.
        var mayNotReadAim2 = highSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAim);
        var cannotReadAimNoCalibration2 = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAimNoCalibration);
        var notTransmittingAim = highSeverity.Find(a => a.AdviceType == AdviceType.NotTransmittingAim);

        if ((mayNotReadAim2 is not null || cannotReadAimNoCalibration2 is not null) &&
            notTransmittingAim is not null) {
            highSeverity.Remove(notTransmittingAim);
        }

        if (highSeverity.Count > 0) {
            // Do not report low-severity, as these are used to represent 'green' conditions.
            // Given that there are high-severity problems, it can be very confusing if the
            // list also contains low-priority entries ("there is a significant problem...but all is well").
            _adviceItems.AddRange(highSeverity.OrderBy(ai => (int)ai.AdviceType));
            _adviceItems.AddRange(mediumSeverity.OrderBy(ai => (int)ai.AdviceType));
        }
        else {
            // Fix-up for 'green' messages.
            if (mediumSeverity.Count > 0) {
                // Add a hint that there are other issues to 'green' messages. If a UI shows one
                // advice message at a time, this will improve the UX.
                for (var idx = 0; idx < lowSeverity.Count; idx++) {
#pragma warning disable SA1118 // Parameter should not span multiple lines
                    lowSeverity[idx] = new AdviceItem(
                        lowSeverity[idx].AdviceType,
                        lowSeverity[idx].Condition,
                        lowSeverity[idx].Description +
                            lowSeverity[idx].Advice +
                            (mediumSeverity.Count > 1
                                ? "There are also some additional issues:"
                                : "There is also an additional issue."),
                        lowSeverity[idx].Severity);
#pragma warning restore SA1118 // Parameter should not span multiple lines
                }
            }

            _adviceItems.AddRange(lowSeverity.OrderBy(ai => (int)ai.AdviceType));
            _adviceItems.AddRange(mediumSeverity.OrderBy(ai => (int)ai.AdviceType));
        }

        return;

        void AddAdviceItemToList(AdviceItem? adviceItem) {
            switch (adviceItem?.Severity) {
                case ConditionSeverity.Low:
                    lowSeverity.Add(adviceItem);
                    break;
                case ConditionSeverity.Medium:
                    mediumSeverity.Add(adviceItem);
                    break;
                case ConditionSeverity.High:
                    highSeverity.Add(adviceItem);
                    break;
                case ConditionSeverity.None:
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(adviceItem),
                        adviceItem.Severity,
                        Resources.CalibrationInvalidAdviceItem);
            }
        }

        bool IfTheTestSucceeded() => testsSucceeded;
        bool IfTestDidNotSucceed() => !testsSucceeded;
        bool IfWeOmittedThePpnTest() => testGs1Only;
        bool IfWeKnowIfWeCanReadFormat05AndFormat06Reliably() => canReadFormat05AndFormat06Reliably is not null;
        bool IfWeCanReadFormat05AndFormat06Reliably() => canReadFormat05AndFormat06Reliably ?? false;
        bool IfWeCannotReadFormat05AndFormat06Reliably() => !canReadFormat05AndFormat06Reliably ?? false;
        bool IfWeKnowIfWeCanReadUniqueIdentifiersReliably() => canReadInvariantCharactersReliably is not null;
        bool IfWeCanReadUniqueIdentifiersReliably() => canReadInvariantCharactersReliably ?? false;
        bool IfWeCannotReadUniqueIdentifiersReliably() => !canReadInvariantCharactersReliably ?? false;
        bool IfWeDoNotAscertainThatWeCanReadUniqueIdentifiersReliably() => !(canReadInvariantCharactersReliably ?? false);
        bool IfTheKeyboardLayoutsCorrespondForUniqueIdentifiers() => keyboardLayoutsCorrespondForInvariantCharacters ?? false;
        bool IfTheKeyboardLayoutsDoNotCorrespondForUniqueIdentifiers() => !keyboardLayoutsCorrespondForInvariantCharacters ?? false;
        bool IfWeKnowIfKeyboardLayoutsCorrespondForUniqueIdentifiers() => keyboardLayoutsCorrespondForInvariantCharacters is not null;
        bool IfWeAssumeAgnosticism() => calibrationAssumption == Assumption.Agnostic;
        bool IfWeAssumeCalibration() => calibrationAssumption == Assumption.Calibration;
        bool IfWeDoNotAssumeCalibration() => calibrationAssumption != Assumption.Calibration;
        bool IfWeAssumeNoCalibration() => calibrationAssumption == Assumption.NoCalibration;
        bool IfTheCurrentPlatformIsMacintosh() => platform == SupportedPlatform.Macintosh;
        bool IfTheKeyboardLayoutsCanRepresentRecordSeparatorsWithoutMapping() => keyboardLayoutsCanRepresentRecordSeparatorWithoutMapping ?? false;
        bool IfTheKeyboardLayoutsCannotRepresentRecordSeparatorsWithoutMapping() => !keyboardLayoutsCanRepresentRecordSeparatorWithoutMapping ?? false;
        bool IfWeKnowIfTheKeyboardLayoutsCanRepresentRecordSeparatorsWithoutMapping() => keyboardLayoutsCanRepresentRecordSeparatorWithoutMapping is not null;
        bool IfTheKeyboardLayoutsCanRepresentGroupSeparatorsWithoutMapping() => keyboardLayoutsCanRepresentGroupSeparatorWithoutMapping ?? false;
        bool IfTheKeyboardLayoutsCannotRepresentGroupSeparatorsWithoutMapping() => !keyboardLayoutsCanRepresentGroupSeparatorWithoutMapping ?? false;
        bool IfTheKeyboardLayoutsCannotRepresentFileSeparatorsWithoutMapping() => !keyboardLayoutsCanRepresentFileSeparatorWithoutMapping ?? false;
        bool IfTheKeyboardLayoutsCannotRepresentUnitSeparatorsWithoutMapping() => !keyboardLayoutsCanRepresentUnitSeparatorWithoutMapping ?? false;
        bool IfTheKeyboardLayoutsCannotRepresentEotWithoutMapping() => !keyboardLayoutsCanRepresentEotWithoutMapping ?? false;
        bool IfWeDoNotAscertainThatTheKeyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping() => !(keyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping ?? false);
        bool IfTheKeyboardLayoutsCannotRepresentEdiSeparatorsWithoutMapping() => keyboardLayoutsCanRepresentEdiSeparatorsWithoutMapping is false;
        bool IfCapsLockIsOn() => systemCapabilities.CapsLock;
        bool IfCapsLockIsOff() => !systemCapabilities.CapsLock;
        bool IfWeDoNotAscertainThatTheKeyboardScriptDoesNotSupportCase() => !(keyboardScriptDoesNotSupportCase ?? false);
        bool IfTheKeyboardScriptDoesNotSupportCase() => keyboardScriptDoesNotSupportCase ?? false;
        bool IfScannerMayCompensateForCapsLock() => scannerMayCompensateForCapsLock;
        bool IfWeDoNotAscertainThatTheScannerTransmitsAimIdentifiers() => !(scannerTransmitsAimIdentifiers ?? false);
        bool IfWeDoNotAscertainThatTheScannerTransmitsAnEndOfLineSequence() => !(scannerTransmitsEndOfLineSequence ?? false);
        bool IfScannerTransmitsAnAdditionalPrefix() => scannerTransmitsAdditionalPrefix;
        bool IfScannerTransmitsAnAdditionalSuffix() => scannerTransmitsAdditionalSuffix;
        bool IfWeDoNotAscertainThatTheKeyboardLayoutsCorrespondForAimIdentifierFlagCharacter() => !(keyboardLayoutsCorrespondForAimIdentifier ?? false);
        bool IfWeCannotReadAimIdentifiersReliably() => !canReadAimIdentifiersReliably ?? false;
        bool IfThereIsUncertaintyAboutTheDetectedAimIdentifier() => aimIdentifierUncertain;
        bool IfWeDoNotAscertainThatTheKeyboardLayoutsCorrespondsForAdditionalAsciiCharacters() => !(keyboardLayoutsCorrespondForNonInvariantCharacters ?? false);
        bool IfWeCannotReadAdditionalAsciiCharactersReliably() => !canReadNonInvariantCharactersReliably ?? false;
        bool IfWeCannotReadEdiCharactersReliably() => !canReadEdiReliably ?? false;
        bool IfWeCannotReadAscii28CharactersReliably() => !canReadAscii28Reliably ?? false;
        bool IfWeCannotReadAscii31CharactersReliably() => !canReadAscii31Reliably ?? false;
        bool IfWeCannotReadAscii04CharactersReliably() => !canReadAscii04Reliably ?? false;
        bool IfUnexpectedErrorOccurred() => unexpectedError;
        bool IfNoUnexpectedErrorOccurred() => !unexpectedError;
        bool IfDataWasReported() => dataReported;
        bool IfNoDataWasReported() => !dataReported;
        bool IfDataWasFullyReported() => completeDataReported;
        bool IfDataWasOnlyPartiallyReported() => !completeDataReported;
        bool IfDeadKeyBarcodesWereGeneratedDuringCalibration() => deadKeys;
        bool IfBarcodesWereScannedInAnIncorrectSequence() => !correctSequenceReported;
        bool IfScannerMayConvertToUpperCase() => scannerMayConvertToUpperCase;
        bool IfScannerMayConvertToLowerCase() => scannerMayConvertToLowerCase;
        bool IfScannerMayInvertCase() => scannerMayInvertCase;
        bool IfAdviceIsProvidedSpecificallyForGermany() => adviceTerritory == Territory.Germany;
        bool IfAdviceIsNotProvidedSpecificallyForGermany() => adviceTerritory != Territory.Germany;

        // 100
        AdviceItem ReportThatInvariantCharactersAreReadReliably() =>
            new(AdviceType.ReadsUniqueIdentifiersReliably);

        // 105
        AdviceItem ReportThatUniqueIdentifiersAreReadReliablyButThePpnTestWasOmitted() =>
            new(AdviceType.ReadsUniqueIdentifiersReliablyNoPpnTest);

        // 110
        AdviceItem ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesMayNotBeReadReliably() =>
            new(AdviceType.ReadsUniqueIdentifiersReliablyMayNotReadPpn);

        // 115
        AdviceItem ReportThatUniqueIdentifiersAreReadReliablyButPpnBarcodesAreNotReadReliably() =>
            new(AdviceType.ReadsUniqueIdentifiersReliablyExceptPpn);

        // 200
        AdviceItem ReportThatTheBarcodeScannerDoesNotTransmitAimIdentifiers() =>
            new(AdviceType.NotTransmittingAim);

        // 205
        AdviceItem ReportThatCapsLockIsSwitchedOnButCaseIsReportedCorrectly() =>
            new(AdviceType.CapsLockCompensation);

        // 206
        AdviceItem ReportThatCapsLockIsSwitchedOnOnMacOsButCaseIsPreserved() =>
            new(AdviceType.CapsLockOnPreservationMacintosh);

        // 210
        AdviceItem ReportThatCapsLockIsSwitchedOnButScriptDoesNotSupportCase() =>
            new(AdviceType.CapsLockOnNoCase);

        // 215
        AdviceItem ReportThatTheScannerDoesNotTransmitAnEndOfLineSequence() =>
            new(AdviceType.NotTransmittingEndOfLine);

        // 220
        AdviceItem ReportThatTheScannerTransmitsAPrefix() =>
            new(AdviceType.TransmittingPrefix);

        // 225
        AdviceItem ReportThatTheScannerTransmitsASuffix() =>
            new(AdviceType.TransmittingSuffix);

        // 230
        AdviceItem ReportThatWeMayNotReadAimIdentifiersAssumingAgnosticism() =>
            new(AdviceType.MayNotReadAim);

        // 231
        AdviceItem ReportThatWeMayNotReadAimIdentifiersAssumingNoCalibration() =>
            new(AdviceType.CannotReadAimNoCalibration);

        // 232
        AdviceItem ReportThatTheBarcodeScannerMayNotTransmitAimIdentifiers() =>
            new(AdviceType.MayNotTransmitAim);

        // 235
        AdviceItem ReportThatWeCannotReadAimIdentifiers() =>
            new(AdviceType.CannotReadAim);

        // 240
        AdviceItem ReportThatFormat05OrFormat06MayNotBeReadReliablyAssumingAgnosticism() =>
            new(AdviceType.MayNotReadPpn);

        // 241
        AdviceItem ReportThatFormat05OrFormat06MayNotBeReadReliablyAssumingNoCalibration() =>
            new(AdviceType.MayNotReadPpnNoCalibration);

        // 245
        AdviceItem ReportThatFormat05OrFormat06AreNotReadReliably() =>
            new(AdviceType.CannotReadPpnReliably);

        // 250
        AdviceItem ReportThatWeDidNotTestForIsoIec15434() =>
            new(AdviceType.NoPpnTest);

        // 255
        AdviceItem ReportThatTheDataInputPerformanceIsSlowerThanExpected() =>
            new(AdviceType.SlowScannerPerformance);

        // 256
        AdviceItem ReportThatTheDataInputPerformanceIsVeryPoor() =>
            new(AdviceType.VerySlowScannerPerformance);

        // 260
        AdviceItem ReportThatNonAdditionalDataMayNotBeReadReliablyAssumingAgnosticism() =>
            new(AdviceType.MayNotReadAdditionalDataReliably);

        // 261
        AdviceItem ReportThatNonAdditionalDataMayNotBeReadReliablyAssumingNoCalibration() =>
            new(AdviceType.MayNotReadAdditionalDataNoCalibration);

        // 265
        AdviceItem ReportThatTheSystemCannotReadAdditionalDataReliably() =>
            new(AdviceType.CannotReadAdditionalData);

        // 270
        AdviceItem ReportThatEdiCharactersMayNotBeReadReliablyAssumingAgnosticism() =>
            new(AdviceType.MayNotReadEdiCharactersReliably);

        // 271
        AdviceItem ReportThatEdiCharactersMayNotBeReadReliablyAssumingNoCalibration() =>
            new(AdviceType.MayNotReadEdiCharactersNoCalibration);

        // 275
        AdviceItem ReportThatTheSystemCannotReadEdiCharactersReliably() =>
            new(AdviceType.CannotReadEdiCharacters);

        // 276
        AdviceItem ReportThatTheSystemMayNotReadAscii28CharactersReliably() =>
            new(AdviceType.MayNotReadAscii28Characters);

        // 277
        AdviceItem ReportThatTheSystemCannotReadAscii28CharactersReliably() =>
            new(AdviceType.CannotReadAscii28Characters);

        // 278
        AdviceItem ReportThatTheSystemMayNotReadAscii31CharactersReliably() =>
            new(AdviceType.MayNotReadAscii31Characters);

        // 279
        AdviceItem ReportThatTheSystemCannotReadAscii31CharactersReliably() =>
            new(AdviceType.CannotReadAscii31Characters);

        // 280
        AdviceItem ReportThatTheSystemMayNotReadAscii04CharactersReliably() =>
            new(AdviceType.MayNotReadAscii04Characters);

        // 281
        AdviceItem ReportThatTheSystemCannotReadAscii04CharactersReliably() =>
            new(AdviceType.CannotReadAscii04Characters);

        // 300
        AdviceItem ReportThatTheTestFailed() =>
            new(AdviceType.TestFailed);

        // 301
        AdviceItem ReportThatNoScannedDataWasReportedForBaseLineBarcode() =>
            new(AdviceType.NoDataReported);

        // 303
        AdviceItem ReportThatScannedDataWasPartiallyReportedForBaselineBarcode() =>
            new(AdviceType.PartialDataReported);

        // 304
        AdviceItem ReportThatNoScannedDataWasReportedForDeadKeyBarcodes() =>
            new(AdviceType.NoDataReportedDeadKeys);

        // 305
        AdviceItem ReportThatUserScannedADeadKeyBarcodeOutOfSequence() =>
            new(AdviceType.IncorrectSequenceDeadKeys);

        // 306
        AdviceItem ReportThatScannedDataWasPartiallyReportedForDeadKeyBarcodes() =>
            new(AdviceType.PartialDataReportedDeadKeys);

        // 307
        AdviceItem ReportThatLayoutsDoNotMatch() =>
            new(AdviceType.LayoutsDoNotMatch);

        // 308
        AdviceItem ReportThatLayoutsDoNotMatchForNoCalibrationAssumption() =>
            new(AdviceType.LayoutsDoNotMatchNoCalibration);

        // 309
        AdviceItem ReportThatHiddenCharactersAreNotRepresentedCorrectly() =>
            new(AdviceType.HiddenCharactersNotRepresentedCorrectly);

        // 310
        AdviceItem ReportThatHiddenCharactersAreNotRepresentedCorrectlyAssumingNoCalibration() =>
            new(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoCalibration);

        // 311
        AdviceItem ReportThatHiddenCharactersAreNotRepresentedCorrectlyForGermany() =>
            new(AdviceType.HiddenCharactersNotRepresentedCorrectlyGermany);

        // 312
        AdviceItem ReportThatHiddenCharactersAreNotRepresentedCorrectlyAssumingNoCalibrationForGermany() =>
            new(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany);

        // 315
        AdviceItem ReportThatLayoutsDoNotMatchAndPpnBarcodesCannotBeReadReliably() =>
            new(AdviceType.LayoutsDoNotMatchNoPpn);

        // 316
        AdviceItem ReportThatHiddenCharactersAreNotReportedCorrectlyAndPpnBarcodesCannotBeReadReliably() =>
            new(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoPpn);

        // 320
        AdviceItem ReportThatSystemCannotReadUniqueIdentifiersReliably() =>
            new(AdviceType.CannotReadUniqueIdentifiersReliably);

        // 325
        AdviceItem ReportThatCapsLockIsSwitchedOn() =>
            new(AdviceType.CapsLockOn);

        // 326
        AdviceItem ReportThatCapsLockIsSwitchedOnForMacintosh() =>
            new(AdviceType.CapsLockOnMacintosh);

        // 327
        AdviceItem ReportThatCapsLockIsOnAndSystemConvertsToUpperCases() =>
            new(AdviceType.CapsLockOnConvertsToUpperCase);

        // 328
        AdviceItem ReportThatCapsLockIsOnAndSystemConvertsToLowerCases() =>
            new(AdviceType.CapsLockOnConvertsToLowerCase);

        // 330
        AdviceItem ReportThatSystemConvertsUpperAndLowerCases() =>
            new(AdviceType.CaseIsSwitched);

        // 331
        AdviceItem ReportThatSystemConvertsToUpperCase() =>
            new(AdviceType.ConvertsToUpperCase);

        // 332
        AdviceItem ReportThatSystemConvertsToLowerCase() =>
            new(AdviceType.ConvertsToLowerCase);

        // 335
        AdviceItem ReportBarcodesCannotBeReadReliablyForKeyboardScriptThatDoesNotSupportCase() =>
            new(AdviceType.NoSupportForCase, systemCapabilities.KeyboardScript);

        // 350
        AdviceItem ReportIncorrectRepresentationOfPpnRecordSeparators() =>
            new(AdviceType.RecordSeparatorIncorrectlyRepresentedGermany);

        // 351
        AdviceItem ReportIncorrectRepresentationOfPpnRecordSeparatorsAssumingNoCalibration() =>
            new(AdviceType.RecordSeparatorIncorrectlyRepresentedNoCalibrationGermany);

        // 355
        AdviceItem ReportThatPpnBarcodesCannotBeReadReliably() =>
            new(AdviceType.CannotReadPpnReliablyGermany);

        // 390
        AdviceItem ReportThatAnUnexpectedErrorWasReported() =>
            new(AdviceType.UnexpectedErrorReported);
    }

    /// <summary>
    ///   Gets an ordered collection of advice items.
    /// </summary>
    /// <returns>An ordered collection of advice items.</returns>
    public IEnumerable<AdviceItem> Items => _adviceItems;

    /// <summary>
    /// Factory method for creating new Advice.
    /// </summary>
    /// <param name="systemCapabilities">The system capabilities.</param>
    /// <param name="adviceTerritory">Territory for which advice will be provided.</param>
    /// <returns>An ordered sequence of advice items.</returns>
    public static Advice CreateAdvice(
        SystemCapabilities systemCapabilities,
        Territory adviceTerritory = Territory.Europe) {
        return new Advice(systemCapabilities, adviceTerritory);
    }
}