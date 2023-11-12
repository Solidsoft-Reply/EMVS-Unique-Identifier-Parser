// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Advice.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd. All rights reserved.
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
// Provides an ordered sequence of advice items.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using BarcodeScanner.Calibration;
using Properties;

/// <summary>
///   Provides an ordered sequence of advice items.
/// </summary>
public class Advice : IAdvice<AdviceItem, AdviceType>
{
    /// <summary>
    ///   An ordered list of advice items.
    /// </summary>
    private readonly List<AdviceItem> _adviceItems = new();

    /// <summary>
    ///   Initializes a new instance of the <see cref="Advice"/> class.
    /// </summary>
    /// <param name="systemCapabilities">The capabilities of the barcode scanner/computer combination.</param>
    /// <param name="adviceTerritory">Territory for which advice will be provided.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "<Pending>")]
    private Advice(
        SystemCapabilities systemCapabilities,
        Territory adviceTerritory = Territory.Europe)
    {
        var lowSeverity = new List<AdviceItem>();
        var mediumSeverity = new List<AdviceItem>();
        var highSeverity = new List<AdviceItem>();

        if (systemCapabilities is null)
        {
            throw new ArgumentNullException(nameof(systemCapabilities));
        }

        var testGs1Only = !systemCapabilities.FormatnnSupportAssessed;

        AddAdviceItemToList(
            systemCapabilities.ScannerKeyboardPerformance switch
            {
                ScannerKeyboardPerformance.Low    => new AdviceItem(AdviceType.VerySlowScannerPerformance),
                ScannerKeyboardPerformance.Medium => new AdviceItem(AdviceType.SlowScannerPerformance),
                _                                 => null
            });

        // Get boolean values
#pragma warning disable CA1062 // Validate arguments of public methods
        var testsSucceeded = systemCapabilities.TestsSucceeded;
        var dataReported = systemCapabilities.DataReported;
        var correctSequenceReported = systemCapabilities.CorrectSequenceReported;
        var completeDataReported = systemCapabilities.CompleteDataReported;
        var keyboardLayoutsCorrespondForCharacterSet82 = systemCapabilities.KeyboardLayoutsCorrespondForInvariants;
        var keyboardLayoutsCorrespondForAdditionalAsciiCharacters = systemCapabilities.KeyboardLayoutsCorrespondForNonInvariantCharacters;
        var keyboardLayoutsCanRepresentGroupSeparator = systemCapabilities.KeyboardLayoutsCanRepresentGroupSeparator;
        var keyboardLayoutsCanRepresentRecordSeparator = systemCapabilities.KeyboardLayoutsCanRepresentRecordSeparator;
        var keyboardLayoutsCanRepresentEdiSeparator = systemCapabilities.KeyboardLayoutsCanRepresentEdiSeparator;
        var keyboardLayoutsCorrespondForAimIdentifier = systemCapabilities.KeyboardLayoutsCorrespondForAimIdentifier;
        var canReadUniqueIdentifiersReliably = systemCapabilities.CanReadInvariantsReliably;
        var canReadFormat05AndFormat06Reliably = systemCapabilities.CanReadFormat05AndFormat06Reliably;
        var canReadAimIdentifiersReliably = systemCapabilities.CanReadAimIdentifiersReliably;
        var canReadAdditionalAsciiCharactersReliably = systemCapabilities.CanReadAdditionalAsciiCharactersReliably;
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
        var calibrationAssumption = systemCapabilities.CalibrationAssumption;
        var deadKeys = systemCapabilities.DeadKeys;
        var platform = systemCapabilities.Platform;
#pragma warning restore CA1062 // Validate arguments of public methods

        // AdviceType: 300
        AddAdviceItemToList(!testsSucceeded && dataReported ? new AdviceItem(AdviceType.TestsFailed) : null);

        // AdviceType: 301, 304
        AddAdviceItemToList(!dataReported ? deadKeys ? new AdviceItem(AdviceType.NoDataReportedDeadKeys) : new AdviceItem(AdviceType.NoDataReported) : null);

        // AdviceType: 305
        AddAdviceItemToList(!correctSequenceReported ? new AdviceItem(AdviceType.IncorrectSequenceDeadKeys) : null);

        // AdviceType: 303, 306
        AddAdviceItemToList(!completeDataReported ? deadKeys ? new AdviceItem(AdviceType.PartialDataReportedDeadKeys) : new AdviceItem(AdviceType.PartialDataReported) : null);

        // AdviceTypes: 307, 308
        AddAdviceItemToList(
            keyboardLayoutsCorrespondForCharacterSet82.Map(inv => !inv) ?? false
                ? TestLayoutsDoNotMatch()
                : null);

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (adviceTerritory)
        {
            case Territory.Germany:

                // AdviceTypes: 311, 312
                AddAdviceItemToList(
                    keyboardLayoutsCorrespondForCharacterSet82.Map(cs82 => cs82) ?? false
                        ? TestTestHiddenCharactersGermany()
                        : null);

                break;

                AdviceItem? TestTestHiddenCharactersGermany() =>
                    (keyboardLayoutsCanRepresentGroupSeparator.Map(repGs => !repGs) ?? false) ||
                    (keyboardLayoutsCanRepresentRecordSeparator.Map(repRs => !repRs) ?? false)
                            ? TestHiddenCharactersNotRepresentedCorrectlyGermany()
                            : null;

                AdviceItem? TestHiddenCharactersNotRepresentedCorrectlyGermany() =>
                    calibrationAssumption == CalibrationAssumption.Agnostic
                        ? canReadUniqueIdentifiersReliably is null && canReadFormat05AndFormat06Reliably is null
                            ? null
                            : (canReadUniqueIdentifiersReliably ?? false) &&
                              (canReadFormat05AndFormat06Reliably ?? false)
                                ? new AdviceItem(AdviceType.HiddenCharactersNotRepresentedCorrectlyGermany)
                                : TestHiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany()
                        : TestHiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany();

                AdviceItem? TestHiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany() => 
                    calibrationAssumption == CalibrationAssumption.NoCalibration
                        ? new AdviceItem(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany)
                        : null;
            default:

                // AdviceTypes: 309, 310
                AddAdviceItemToList(
                    keyboardLayoutsCorrespondForCharacterSet82.Map(inv => inv) ?? false
                        ? keyboardLayoutsCanRepresentGroupSeparator.Map(inv => !inv) ?? false
                            ? TestHiddenCharactersNotRepresentedCorrectly()
                            : null
                        : null);
                break;

                AdviceItem? TestHiddenCharactersNotRepresentedCorrectly() =>
                    calibrationAssumption == CalibrationAssumption.Agnostic
                        ? canReadUniqueIdentifiersReliably is null && canReadFormat05AndFormat06Reliably is null
                            ? null
                            : (canReadUniqueIdentifiersReliably ?? false) &&
                              (canReadFormat05AndFormat06Reliably ?? false)
                                ? new AdviceItem(AdviceType.HiddenCharactersNotRepresentedCorrectly)
                                : TestHiddenCharactersNotRepresentedCorrectlyNoCalibration()
                        : TestHiddenCharactersNotRepresentedCorrectlyNoCalibration();

                AdviceItem? TestHiddenCharactersNotRepresentedCorrectlyNoCalibration() =>
                    calibrationAssumption == CalibrationAssumption.NoCalibration
                        ? new AdviceItem(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoCalibration)
                        : null;
        }

        // AdviceType: 315, 316
        AddAdviceItemToList(
            calibrationAssumption == CalibrationAssumption.Agnostic
                ? canReadUniqueIdentifiersReliably.Map(canRead => canRead) ?? false
                    ? canReadFormat05AndFormat06Reliably.Map(readRead => !readRead) ?? false
                        ? TestLayoutsDoNotMatchNoPpn()
                        : null
                    : null
                : null);

        // Advice Type: 320
        AddAdviceItemToList(
            !(canReadUniqueIdentifiersReliably.Map(canRead => canRead) ?? false)
                ? new AdviceItem(AdviceType.CannotReadUniqueIdentifiersReliably)
                : null);

        // Advice Type 335
        AddAdviceItemToList(
            canReadUniqueIdentifiersReliably.Map(canRead => !canRead) ?? false
                ? keyboardScriptDoesNotSupportCase.Map(@case => @case) ?? false
                    ? new AdviceItem(AdviceType.NoSupportForCase, systemCapabilities.KeyboardScript)
                    : null
                : null);

        if (systemCapabilities.CapsLock)
        {
            AdviceItem? TestCapsLockCompensation() =>
                scannerMayCompensateForCapsLock
                    ? new AdviceItem(AdviceType.CapsLockCompensation)
                    : null;

            AdviceItem? TestCapsLockOnPreservationMacintosh() =>
                platform == SupportedPlatform.Macintosh
                    ? new AdviceItem(AdviceType.CapsLockOnPreservationMacintosh)
                    : TestCapsLockCompensation();

            // AdviceType: 205, 206
            AddAdviceItemToList(
                !(keyboardScriptDoesNotSupportCase.Map(@case => @case) ?? false)
                    ? TestCapsLockOnPreservationMacintosh()
                    : null);

            // AdviceType: 210
            AddAdviceItemToList(
                keyboardScriptDoesNotSupportCase.Map(@case => @case) ?? false
                    ? new AdviceItem(AdviceType.CapsLockOnNoCase)
                    : null);

            AdviceItem TestCapsLockOnMacintosh() =>
                platform == SupportedPlatform.Macintosh
                    ? new AdviceItem(AdviceType.CapsLockOnMacintosh)
                    : new AdviceItem(AdviceType.CapsLockOn);

            AdviceItem TestCapsLockOnConvertsToLowerCase() =>
                scannerMayConvertToLowerCase
                    ? new AdviceItem(AdviceType.CapsLockOnConvertsToLowerCase)
                    : TestCapsLockOnMacintosh();

            AdviceItem TestCapsLockOnConvertsToUpperCase() =>
                scannerMayConvertToUpperCase
                    ? new AdviceItem(AdviceType.CapsLockOnConvertsToUpperCase)
                    : TestCapsLockOnConvertsToLowerCase();

            // AdviceType: 325, 326, 327, 328
            AddAdviceItemToList(
                !(keyboardScriptDoesNotSupportCase.Map(@case => @case) ?? false)
                    ? TestCapsLockOnConvertsToUpperCase()
                    : null);
        }
        else
        {
            AdviceItem? TestConvertsToLowerCase() =>
                scannerMayConvertToLowerCase
                    ? new AdviceItem(AdviceType.ConvertsToLowerCase)
                    : null;

            AdviceItem? TestConvertsToUpperCase() =>
                scannerMayConvertToUpperCase
                    ? new AdviceItem(AdviceType.ConvertsToUpperCase)
                    : TestConvertsToLowerCase();

            AdviceItem? TestCaseIsSwitched() =>
                scannerMayInvertCase
                    ? new AdviceItem(AdviceType.CaseIsSwitched)
                    : TestConvertsToUpperCase();

            // AdviceType: 330, 331, 332
            AddAdviceItemToList(
                !(keyboardScriptDoesNotSupportCase.Map(@case => @case) ?? false)
                    ? TestCaseIsSwitched()
                    : null);
        }

        // AdviceType: 350, 351, 355
        AddAdviceItemToList(
            adviceTerritory == Territory.Germany
                ? TestCannotReadPpnReliablyGermany()
                : null);

        // AdviceType: 200
        AddAdviceItemToList(
            !(scannerTransmitsAimIdentifiers ?? false)
                ? new AdviceItem(AdviceType.NotTransmittingAim)
                : null);

        // AdviceType: 215
        AddAdviceItemToList(
            !(scannerTransmitsEndOfLineSequence ?? false)
                ? new AdviceItem(AdviceType.NotTransmittingEndOfLine)
                : null);

        // AdviceType: 220
        AddAdviceItemToList(
            scannerTransmitsAdditionalPrefix
                ? new AdviceItem(AdviceType.TransmittingPrefix)
                : null);

        // AdviceType: 225
        AddAdviceItemToList(
            scannerTransmitsAdditionalSuffix
                ? new AdviceItem(AdviceType.TransmittingSuffix)
                : null);

        // AdviceType: 260, 261, 265
        AddAdviceItemToList(
            !(keyboardLayoutsCorrespondForAdditionalAsciiCharacters.Map(ascii => ascii) ?? false)
                ? canReadAdditionalAsciiCharactersReliably.Map(readRead => !readRead) ?? false
                    ? new AdviceItem(AdviceType.CannotReadAdditionalData)
                    : TestMayNotReadAdditionalData()
                : null);

        // AdviceTypes: 230, 231, 235
        AddAdviceItemToList(
            !(keyboardLayoutsCorrespondForAimIdentifier.Map(aim => aim) ?? false)
                ? canReadAimIdentifiersReliably.Map(readAim => !readAim) ?? false
                    ? new AdviceItem(AdviceType.CannotReadAim)
                    : calibrationAssumption switch {
                        CalibrationAssumption.Agnostic => new AdviceItem(AdviceType.MayNotReadAim),
                        CalibrationAssumption.NoCalibration => new AdviceItem(AdviceType.MayNotReadAimNoCalibration),
                        _ => null
                    }
                : null);

        // AdviceTypes: 232
        AddAdviceItemToList(
            !(keyboardLayoutsCorrespondForAimIdentifier.Map(aim => aim) ?? false)
                ? !aimIdentifierUncertain
                    ? null
                    : new AdviceItem(AdviceType.MayNotTransmitAim)
                : null);

        if (testGs1Only)
        {
            // AdviceType: 250
            AddAdviceItemToList(new AdviceItem(AdviceType.NoPpnTest));
        }

        // AdviceTypes: 240, 241, 245
        AddAdviceItemToList(TestCannotReadPpnReliably());

        // AdviceTypes: 100, 105, 110, 115 (Not Calibration)
        AddAdviceItemToList(
            canReadUniqueIdentifiersReliably.Map(read => read) ?? false
                ? keyboardLayoutsCorrespondForCharacterSet82.Map(inv => inv) ?? false
                    ? (keyboardLayoutsCanRepresentGroupSeparator.Map(repGs => repGs) ?? false) &&
                      testsSucceeded &&
                      calibrationAssumption != CalibrationAssumption.Calibration
                        ? TestReadsUniqueIdentifiersReliablyNoPpnTest()
                        : null
                    : null
                : null);

        // AdviceTypes: 100, 105, 155 (Calibration)
        AddAdviceItemToList(
            (canReadUniqueIdentifiersReliably.Map(read => !read) ?? false) &&
            testsSucceeded &&
            calibrationAssumption == CalibrationAssumption.Calibration
                ? TestAdviceTerritory()
                : null);

        // General fix-up for other issues
        // Even if German PPN barcode tests are not selected, it is possible to detect incompatibility
        // with PPN barcodes - e.g., if [ is detected as an ambiguous character. If we report a PPN
        // issue, it feels redundant and confusing to an end user to warn them that they didn't run the
        // PPN tests. So, we will remove the warning if this occurs.
        if ((from ppnTestWarning in mediumSeverity
             let isPpnError= (from ppnError in highSeverity
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
             select ppnTestWarning).Any())
        {
            mediumSeverity.RemoveAll(item => item.AdviceType == AdviceType.NoPpnTest);
        }

        // Some PPN-related warnings duplicate others. This redundancy should be removed.
        var layoutsDoNotMatchNoPpn = highSeverity.Find(a => a.AdviceType == AdviceType.LayoutsDoNotMatchNoPpn);
        var hiddenCharactersNotRepresentedCorrectlyNoPpn = highSeverity.Find(a => a.AdviceType == AdviceType.HiddenCharactersNotRepresentedCorrectlyNoPpn);

        if (layoutsDoNotMatchNoPpn is not null || hiddenCharactersNotRepresentedCorrectlyNoPpn is not null ) {
            var cannotReadPpnReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliably);
            var cannotReadPpnReliablyGermany = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliablyGermany);
 
            if (cannotReadPpnReliably is not null) {
                mediumSeverity.Remove(cannotReadPpnReliably);
            }

            if (cannotReadPpnReliablyGermany is not null)
            {
                mediumSeverity.Remove(cannotReadPpnReliablyGermany);
            }
        }

        // If the advice territory is Germany, there is no need to include 355 if 320 is already included.
        if (adviceTerritory == Territory.Germany) {
            var cannotReadPpnReliablyGermany = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliablyGermany);
            var cannotReadUniqueIdentifiersReliably = highSeverity.Find(a => a.AdviceType == AdviceType.CannotReadUniqueIdentifiersReliably);

            if (cannotReadPpnReliablyGermany is not null && cannotReadUniqueIdentifiersReliably is not null)
            {
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
            if (cannotReadUniqueIdentifiersReliably is not null)
            {
                highSeverity.Remove(cannotReadUniqueIdentifiersReliably);
            }
            var recordSeparatorIncorrectlyRepresentedGermany = highSeverity.Find(a => a.AdviceType == AdviceType.RecordSeparatorIncorrectlyRepresentedGermany);
            if (recordSeparatorIncorrectlyRepresentedGermany is not null)
            {
                highSeverity.Remove(recordSeparatorIncorrectlyRepresentedGermany);
            }

            var mayNotReadAim = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAim);
            if (mayNotReadAim is not null)
            {
                mediumSeverity.Remove(mayNotReadAim);
            }

            var mayNotReadAimNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAimNoCalibration);
            if (mayNotReadAimNoCalibration is not null)
            {
                mediumSeverity.Remove(mayNotReadAimNoCalibration);
            }

            var cannotReadAim = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAim);
            if (cannotReadAim is not null)
            {
                mediumSeverity.Remove(cannotReadAim);
            }

            var mayNotReadPpn = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadPpn);
            if (mayNotReadPpn is not null)
            {
                mediumSeverity.Remove(mayNotReadPpn);
            }

            var mayNotReadPpnNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadPpnNoCalibration);
            if (mayNotReadPpnNoCalibration is not null)
            {
                mediumSeverity.Remove(mayNotReadPpnNoCalibration);
            }

            var cannotReadPpnReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadPpnReliably);
            if (cannotReadPpnReliably is not null)
            {
                mediumSeverity.Remove(cannotReadPpnReliably);
            }

            var mayNotReadAdditionalDataReliably = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAdditionalDataReliably);
            if (mayNotReadAdditionalDataReliably is not null)
            {
                mediumSeverity.Remove(mayNotReadAdditionalDataReliably);
            }

            var mayNotReadAdditionalDataNoCalibration = mediumSeverity.Find(a => a.AdviceType == AdviceType.MayNotReadAdditionalDataNoCalibration);
            if (mayNotReadAdditionalDataNoCalibration is not null)
            {
                mediumSeverity.Remove(mayNotReadAdditionalDataNoCalibration);
            }

            var cannotReadAdditionalData = mediumSeverity.Find(a => a.AdviceType == AdviceType.CannotReadAdditionalData);
            if (cannotReadAdditionalData is not null)
            {
                mediumSeverity.Remove(cannotReadAdditionalData);
            }
        }

        // Fix up the issue with CAPS LOCK where, if CAPS LOCK is reported as being on, but the system also
        // determines that te scanner appears to be automatically compensating for this, we don't need to
        // report the CAPS LOCK being on, as this is subsumed into the information about compensation.
        var capsLockOn = highSeverity.Find(a => a.AdviceType == AdviceType.CapsLockOn);
        if (capsLockOn is not null)
        {
            var capsLockCompensation = mediumSeverity.Find(a => a.AdviceType == AdviceType.CapsLockCompensation);
            if (capsLockCompensation is not null)
            {
                highSeverity.Remove(capsLockOn);
            }
        }

        // Fix up test failures. If a 301, 303, 304, 305 0r 306 error occurs, remove the 300 error, as this is
        // redundant.
        var noDataReported = highSeverity.Find(a => a.AdviceType == AdviceType.NoDataReported);
        var partialDataReported = highSeverity.Find(a => a.AdviceType == AdviceType.PartialDataReported);

        if (noDataReported is not null ||
            partialDataReported is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.NoDataReportedDeadKeys) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.IncorrectSequenceDeadKeys) is not null ||
            highSeverity.Find(a => a.AdviceType == AdviceType.PartialDataReportedDeadKeys) is not null)
        {
            var testsFailed = highSeverity.Find(a => a.AdviceType == AdviceType.TestsFailed);

            if (testsFailed is not null)
            {
                highSeverity.Remove(testsFailed);
            }

            if (noDataReported is not null && partialDataReported is not null)
            {
                highSeverity.Remove(partialDataReported);
            }

            if (noDataReported is not null || partialDataReported is not null)
            {
                // Remove any report about AIM identifiers, additional characters or control characters.
            }
        }

        if (highSeverity.Any())
        {
            // Do not report low-severity, as these are used to represent 'green' conditions. 
            // Given that there are high-severity problems, it can be very confusing if the 
            // list also contains low-priority entries ("there is a significant problem...but all is well").
            _adviceItems.AddRange(highSeverity.OrderBy(ai => (int)ai.AdviceType));
            _adviceItems.AddRange(mediumSeverity.OrderBy(ai => (int)ai.AdviceType));
        }
        else
        {
            // Fix-up for 'green' messages.
            if (mediumSeverity.Any())
            {
                // Add a hint that there are other issues to 'green' messages. If a UI shows one
                // advice message at a time, this will improve the UX.
                for (var idx = 0; idx < lowSeverity.Count; idx++)
                {
                    lowSeverity[idx] = new AdviceItem(
                        lowSeverity[idx].AdviceType,
                        lowSeverity[idx].Condition,
                        lowSeverity[idx].Description +
                        lowSeverity[idx].Advice +
                        (mediumSeverity.Count > 1
                            ? "There are also some additional issues:"
                            : "There is also an additional issue."),
                        lowSeverity[idx].Severity);
                }
            }

            _adviceItems.AddRange(lowSeverity.OrderBy(ai => (int)ai.AdviceType));
            _adviceItems.AddRange(mediumSeverity.OrderBy(ai => (int)ai.AdviceType));
        }

        return;

        AdviceItem? TestReadsUniqueIdentifiersReliablyNoPpnTest() =>
            adviceTerritory != Territory.Germany && testGs1Only
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyNoPpnTest)
                : TestCanReadFormat05AndFormat06Reliably();

        AdviceItem? TestAdviceTerritory() =>
            adviceTerritory != Territory.Germany
                ? TestSupportForFormat06()
                : TestCalibrationCanReadFormat05AndFormat06Reliably();

        AdviceItem? TestCanReadFormat05AndFormat06Reliably() =>
            canReadFormat05AndFormat06Reliably is null
                ? null
                : (bool)canReadFormat05AndFormat06Reliably
                    ? TestNotCalibrationIsAgnostic()
                    : Test006UnreliableReadsUniqueIdentifiersReliablyExceptPpn();

        AdviceItem? TestNotCalibrationIsAgnostic() =>
            calibrationAssumption == CalibrationAssumption.Agnostic
                ? TestReadsUniqueIdentifiersReliably()
                : TestReadsUniqueIdentifiersReliablyExceptPpn();

        AdviceItem? TestSupportForFormat06() =>
            testGs1Only
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyNoPpnTest)
                : TestFormat06CanReadFormat05AndFormat06Reliably();

        AdviceItem? TestCalibrationCanReadFormat05AndFormat06Reliably() =>
            canReadFormat05AndFormat06Reliably ?? false
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliably)
                : null;

        AdviceItem? TestFormat06CanReadFormat05AndFormat06Reliably() =>
            canReadFormat05AndFormat06Reliably is null
                ? null
                : (bool)canReadFormat05AndFormat06Reliably
                    ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliably)
                    : new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyExceptPpn);

        AdviceItem? Test006UnreliableReadsUniqueIdentifiersReliablyExceptPpn() =>
            adviceTerritory != Territory.Germany && 
            !testGs1Only
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyExceptPpn)
                : null;

        AdviceItem? TestReadsUniqueIdentifiersReliably() =>
            keyboardLayoutsCanRepresentRecordSeparator ?? false
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliably)
                : TestReadsUniqueIdentifiersReliablyMayNotReadPpn();

        AdviceItem? TestReadsUniqueIdentifiersReliablyExceptPpn() =>
            calibrationAssumption == CalibrationAssumption.NoCalibration &&
            (keyboardLayoutsCanRepresentRecordSeparator.Map(repRs => !repRs) ?? false) &&
            adviceTerritory != Territory.Germany &&
            !testGs1Only
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyExceptPpn)
                : null;

        AdviceItem? TestMayNotReadAdditionalData() =>
            calibrationAssumption switch
            {
                CalibrationAssumption.Agnostic => new AdviceItem(AdviceType.MayNotReadAdditionalDataReliably),
                CalibrationAssumption.NoCalibration => new AdviceItem(AdviceType.MayNotReadAdditionalDataNoCalibration),
                _ => null
            };

        AdviceItem? TestReadsUniqueIdentifiersReliablyMayNotReadPpn() =>
            adviceTerritory != Territory.Germany && !testGs1Only
                ? new AdviceItem(AdviceType.ReadsUniqueIdentifiersReliablyMayNotReadPpn)
                : null;

        AdviceItem? TestCannotReadPpnReliablyGermany() =>
            canReadFormat05AndFormat06Reliably is null
                ? null
                : (bool)canReadFormat05AndFormat06Reliably
                    ? TestRecordSeparatorIncorrectlyRepresentedGermany()
                    : new AdviceItem(AdviceType.CannotReadPpnReliablyGermany);

        AdviceItem? TestCannotReadPpnReliably() =>
            canReadFormat05AndFormat06Reliably is null
                ? null
                : (bool)canReadFormat05AndFormat06Reliably
                    ? TestMayNotReadPpn()
                    : new AdviceItem(AdviceType.CannotReadPpnReliably);

        AdviceItem? TestMayNotReadPpn() =>
            (keyboardLayoutsCanRepresentRecordSeparator.Map(repRs => !repRs) ?? false) &&
            (canReadUniqueIdentifiersReliably ?? false)
                ? calibrationAssumption switch {
                    CalibrationAssumption.Agnostic => new AdviceItem(AdviceType.MayNotReadPpn),
                    CalibrationAssumption.NoCalibration => new AdviceItem(AdviceType.MayNotReadPpnNoCalibration),
                    _ => null
                }
                : null;

        AdviceItem? TestRecordSeparatorIncorrectlyRepresentedGermany() =>
            (keyboardLayoutsCanRepresentRecordSeparator.Map(repRs => !repRs) ?? false) &&
            (canReadUniqueIdentifiersReliably ?? false)
                ? calibrationAssumption switch
                {
                    CalibrationAssumption.Agnostic => new AdviceItem(AdviceType.RecordSeparatorIncorrectlyRepresentedGermany),
                    CalibrationAssumption.NoCalibration => new AdviceItem(AdviceType.RecordSeparatorIncorrectlyRepresentedNoCalibrationGermany),
                    _ => null
                }
                : null;

        AdviceItem? TestLayoutsDoNotMatchNoPpn() =>
            keyboardLayoutsCorrespondForCharacterSet82 is null
                ? TestHiddenCharactersNotRepresentedCorrectlyNoPpn()
                : !(bool)keyboardLayoutsCorrespondForCharacterSet82
                    ? new AdviceItem(AdviceType.LayoutsDoNotMatchNoPpn)
                    : TestHiddenCharactersNotRepresentedCorrectlyNoPpn();

        AdviceItem? TestHiddenCharactersNotRepresentedCorrectlyNoPpn() =>
            keyboardLayoutsCanRepresentGroupSeparator.Map(repGs => !repGs) ?? false
                ? new AdviceItem(AdviceType.HiddenCharactersNotRepresentedCorrectlyNoPpn)
                : null;

        AdviceItem? TestLayoutsDoNotMatch() =>
            calibrationAssumption == CalibrationAssumption.Agnostic
                ? canReadUniqueIdentifiersReliably is null && canReadFormat05AndFormat06Reliably is null
                    ? null
                    : (canReadUniqueIdentifiersReliably ?? false) && (canReadFormat05AndFormat06Reliably ?? false)
                        ? new AdviceItem(AdviceType.LayoutsDoNotMatch)
                        : TestLayoutsDoNotMatchNoCalibration()
                : TestLayoutsDoNotMatchNoCalibration();

        AdviceItem? TestLayoutsDoNotMatchNoCalibration()
            => calibrationAssumption == CalibrationAssumption.NoCalibration
                ? new AdviceItem(AdviceType.LayoutsDoNotMatchNoCalibration)
                : null;

        void AddAdviceItemToList(AdviceItem? adviceItem)
        {
            switch (adviceItem?.Severity)
            {
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
    }

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

    /// <summary>
    ///   Gets an ordered collection of advice items.
    /// </summary>
    /// <returns>An ordered collection of advice items.</returns>
    public IEnumerable<AdviceItem> Items => _adviceItems;
}