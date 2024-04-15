﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdviceType.cs" company="Solidsoft Reply Ltd">
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
// The types of advice provided through analysis of the calibration results.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

/// <summary>
///   The types of advice provided through analysis of the calibration results.
/// </summary>
public enum AdviceType
{
    /// <summary>
    /// <p>No advice provided.</p>
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    None = 0,

    /// <summary>
    /// <p>The test failed.</p>
    /// <p>You may have scanned a wrong barcode. Try again, making sure you scan the correct barcode(s).</p>
    /// </summary>
    TestFailed = 300,

    /// <summary>
    /// <p>No data was reported for the barcode.</p>
    /// <p>Try again, making sure you scan the correct barcode(s).</p>
    /// </summary>
    NoDataReported = 301,

    /// <summary>
    /// <p>Some data was not reported when you scanned the barcode.</p>
    /// <p>Your barcode scanner or system may be slow or there may be a fault. Try again.</p>
    /// </summary>
    PartialDataReported = 303,

    /// <summary>
    /// <p>No data was reported for one or more barcodes.</p>
    /// <p>Try again, making sure you scan the correct barcode(s).</p>
    /// </summary>
    NoDataReportedDeadKeys = 304,

    /// <summary>
    /// <p>You scanned a barcode out of sequence.</p>
    /// <p>Try again, making sure you scan the correct barcode. If you need to scan multiple barcodes, make
    ///   sure you scan them in the correct order.</p>
    /// </summary>
    IncorrectSequenceDeadKeys = 305,

    /// <summary>
    /// <p>Some data was not reported when you scanned one or more barcodes.</p>
    /// <p>Your barcode scanner or system may be slow or there may be a fault. Try again.</p>
    /// </summary>
    PartialDataReportedDeadKeys = 306,

    /// <summary>
    /// <p>Your barcode scanner and computer keyboard layouts are different.</p>
    /// <p>Your software may not be able to compensate. Configure your barcode scanner to match your computer
    ///   keyboard layout or emulate a numeric keypad.</p>
    /// </summary>
    LayoutsDoNotMatch = 307,

    /// <summary>
    /// <p>Your barcode scanner and computer keyboard layouts are different.</p>
    /// <p>Configure your barcode scanner to match your computer keyboard layout or emulate a numeric keypad.</p>
    /// </summary>
    LayoutsDoNotMatchNoCalibration = 308,

    /// <summary>
    /// <p>Hidden characters in barcodes are not reported correctly.</p>
    /// <p>Your software may not be able to compensate. Configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    HiddenCharactersNotRepresentedCorrectly = 309,

    /// <summary>
    /// <p>Hidden characters in barcodes are not reported correctly.</p>
    /// <p>Configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    HiddenCharactersNotRepresentedCorrectlyNoCalibration = 310,

    /// <summary>
    /// <p>Hidden characters in barcodes are not reported correctly.</p>
    /// <p>Your software may not be able to compensate. Configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    HiddenCharactersNotRepresentedCorrectlyGermany = 311,

    /// <summary>
    /// <p>Hidden characters in barcodes are not reported correctly.</p>
    /// <p>Configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    HiddenCharactersNotRepresentedCorrectlyNoCalibrationGermany = 312,

    /// <summary>
    /// <p>Your barcode scanner and computer keyboard layouts are different.</p>
    /// <p>Your software may be able to compensate, but cannot read German PPN barcodes reliably. If necessary,
    ///   configure your barcode scanner to match your computer keyboard layout or emulate a numeric keypad.</p>
    /// </summary>
    LayoutsDoNotMatchNoPpn = 315,

    /// <summary>
    /// <p>Hidden characters in barcodes are not reported correctly.</p>
    /// <p>Your software may be able to compensate, but cannot read German PPN barcodes reliably.
    ///   If necessary, configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    HiddenCharactersNotRepresentedCorrectlyNoPpn = 316,

    /// <summary>
    /// <p>Your current configuration is incompatible with the European Medicines Verification System.</p>
    /// <p>Configure your barcode scanner to emulate a different keyboard layout or a numeric keypad.</p>
    /// </summary>
    CannotReadUniqueIdentifiersReliably = 320,

    /// <summary>
    /// <p>Caps Lock is switched on.</p>
    /// <p>Switch Caps Lock off and try again. If you must keep Caps Lock switched on while verifying unique
    ///   identifiers, you may be able to configure your barcode scanner to compensate.</p>
    /// </summary>
    CapsLockOn = 325,

    /// <summary>
    /// <p>Caps Lock is switched on.</p>
    /// <p>Switch Caps Lock off and try again.</p>
    /// </summary>
    CapsLockOnMacintosh = 326,

    /// <summary>
    /// <p>Your system converts characters to upper case.</p>
    /// <p>Check your scanner, keyboard and computer configuration and reconfigure them if necessary. Switch
    ///   off Caps Lock and test again.</p>
    /// </summary>
    CapsLockOnConvertsToUpperCase = 327,

    /// <summary>
    /// <p>Your system converts characters to lower case.</p>
    /// <p>Check your scanner, keyboard and computer configuration and reconfigure them if necessary. Switch
    ///   off Caps Lock and test again.</p>
    /// </summary>
    CapsLockOnConvertsToLowerCase = 328,

    /// <summary>
    /// <p>You system converts upper and lower case characters.</p>
    /// <p>Your scanner may be configured to emulate Caps Lock. Check your scanner, keyboard and computer
    ///   configuration and reconfigure them if necessary.</p>
    /// </summary>
    CaseIsSwitched = 330,

    /// <summary>
    /// <p>Your system converts characters to upper case.</p>
    /// <p>Check your scanner, keyboard and computer configuration and reconfigure them if necessary.</p>
    /// </summary>
    ConvertsToUpperCase = 331,

    /// <summary>
    /// <p>Your system converts characters to lower case.</p>
    /// <p>Check your scanner, keyboard and computer configuration and reconfigure them if necessary.</p>
    /// </summary>
    ConvertsToLowerCase = 332,

    /// <summary>
    /// <p>Your keyboard layout supports {0} characters and is incompatible with the European Medicines
    ///   Verification System.</p>
    /// <p>If possible, configure your computer to use a different keyboard layout. Otherwise, configure
    ///   your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    NoSupportForCase = 335,

    /// <summary>
    /// <p>Your system may not be able to read German PPN barcodes reliably.</p>
    /// <p>If your software cannot compensate, configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    RecordSeparatorIncorrectlyRepresentedGermany = 350,

    /// <summary>
    /// <p>Your system cannot read German PPN barcodes reliably.</p>
    /// <p>Make sure your keyboard layouts match. If necessary, configure your barcode scanner to emulate
    ///   a numeric keypad.</p>
    /// </summary>
    RecordSeparatorIncorrectlyRepresentedNoCalibrationGermany = 351,

    /// <summary>
    /// <p>Your system cannot read German PPN barcodes reliably.</p>
    /// <p>Make sure your keyboard layouts match. If necessary, configure your barcode scanner to emulate
    ///   a numeric keypad.</p>
    /// </summary>
    CannotReadPpnReliablyGermany = 355,

    /// <summary>
    /// <p>An unexpected error was reported.</p>
    /// <p>Try again.</p>
    /// </summary>
    UnexpectedErrorReported = 390,

    /// <summary>
    /// <p>Your barcode scanner does not transmit AIM identifiers.</p>
    /// <p>Configure your barcode scanner to transmit AIM identifiers. Your software can then determine
    ///   what kind of barcode you scanned and do a better job if you scan the wrong barcode.</p>
    /// </summary>
    NotTransmittingAim = 200,

    /// <summary>
    /// <p>Caps Lock is on, but case is preserved.</p>
    /// <p>Your scanner may be configured to compensate automatically for Caps Lock. Switch off Caps
    ///   Lock and test again.</p>
    /// </summary>
    CapsLockCompensation = 205,

    /// <summary>
    /// <p>Caps Lock is on, but case is preserved.</p>
    /// <p>Check your scanner, keyboard and computer configuration and reconfigure them if necessary.
    ///   Switch off Caps Lock and test again.</p>
    /// </summary>
    CapsLockOnPreservationMacintosh = 206,

    /// <summary>
    /// <p>Caps Lock is switched on.</p>
    /// <p>However, your computer keyboard layout does not support upper and lower-case letters. You
    ///   should probably switch Caps Lock off.</p>
    /// </summary>
    CapsLockOnNoCase = 210,

    /// <summary>
    /// <p>Your barcode scanner does not transmit an end-of-line sequence.</p>
    /// <p>Configure your barcode scanner to transmit end-of-line sequences. This may speed up
    ///   scanning.</p>
    /// </summary>
    NotTransmittingEndOfLine = 215,

    /// <summary>
    /// <p>Your barcode scanner transmits a prefix.</p>
    /// <p>Your system may not recognise the prefix. If you have problems verifying medicine packs,
    ///   configure your barcode scanner so that it does not transmit a prefix.</p>
    /// </summary>
    TransmittingPrefix = 220,

    /// <summary>
    /// <p>Your barcode scanner transmits a suffix.</p>
    /// <p>Your system may not recognise the suffix. If you have problems verifying medicine packs,
    ///   configure your barcode scanner so that it does not transmit any suffix.</p>
    /// </summary>
    TransmittingSuffix = 225,

    /// <summary>
    /// <p>Your system may not read AIM identifier characters.</p>
    /// <p>AIM identifiers represent the barcode type. Your verification software may use them to
    /// eliminate unnecessary alerts. However, the software must implement character mapping to
    /// read AIM identifiers reliably.</p>
    /// <p>Make sure your keyboard layouts match. If necessary, configure your barcode scanner to
    /// emulate a numeric keypad.</p>;
    /// </summary>
    MayNotReadAim = 230,

    /// <summary>
    /// <p>Your system cannot read AIM identifier characters.</p>
    /// <p>AIM identifiers represent the barcode type. Your verification software may use them to
    /// eliminate unnecessary alerts.</p>
    /// <p>Make sure your keyboard layouts match. If necessary, configure your barcode scanner to
    /// emulate a numeric keypad.</p>
    /// </summary>
    CannotReadAimNoCalibration = 231,

    /// <summary>
    /// <p>Your barcode scanner may not transmit AIM identifiers.</p>
    /// <p>Check that your barcode scanner is configured to transmit AIM identifiers. Your software can use
    ///   AIM identifiers to determine what kind of barcode you scanned and do a better job if you scan the
    ///   wrong barcode.</p>
    /// </summary>
    MayNotTransmitAim = 232,

    /// <summary>
    /// <p>Your system cannot read the barcode type identifier.</p>
    /// <p>Make sure your keyboard layouts match. If necessary, configure your barcode scanner to
    ///   emulate a numeric keypad and to transmit AIM identifiers.</p>
    /// </summary>
    CannotReadAim = 235,

    /// <summary>
    /// <p>Your system may not be able to read German PPN barcodes reliably.</p>
    /// <p>If your software cannot compensate, use manual data entry to verify PPN unique identifiers
    ///   or configure your barcode scanner to emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadPpn = 240,

    /// <summary>
    /// <p>Your system cannot read German PPN barcodes reliably.</p>
    /// <p>Use manual data entry to verify PPN unique identifiers or configure your barcode scanner
    ///   to emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadPpnNoCalibration = 241,

    /// <summary>
    /// <p>Your system cannot read German PPN barcodes reliably.</p>
    /// <p>Use manual data entry to verify PPN unique identifiers or configure your barcode scanner
    ///   to emulate a numeric keypad.</p>
    /// </summary>
    CannotReadPpnReliably = 245,

    /// <summary>
    /// <p>You did not test compatibility for German PPN barcodes.</p>
    /// <p>If you expect to verify PPN barcodes, run the test again, this time including the German
    ///   PPN compatibility test.</p>
    /// </summary>
    NoPpnTest = 250,

    /// <summary>
    /// <p>Your barcode scanner input performance is slower than expected.</p>
    /// <p>Check the configuration of your barcode scanner, looking for settings that will improve keyboard entry performance.</p>
    /// </summary>
    SlowScannerPerformance = 255,

    /// <summary>
    /// <p>Your barcode scanner input performance is very poor.</p>
    /// <p>Check the configuration of your barcode scanner, looking for settings that will improve keyboard
    ///   entry performance.</p>
    /// </summary>
    VerySlowScannerPerformance = 256,

    /// <summary>
    /// <p>Your system cannot read additional characters reliably.</p>
    /// <p>However, your software may be able to compensate for this. Some barcodes may contain additional
    ///   characters that are never included in unique identifiers.</p>
    /// <p>If you scan other barcodes, and experience difficulty, try configuring your barcode scanner to
    ///   emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadAdditionalDataReliably = 260,

    /// <summary>
    /// <p>Your system may not read additional data reliably.</p>
    /// <p>Some barcodes may contain additional characters that are never included in unique identifiers.</p>
    /// <p>If you scan other barcodes, and experience difficulty, try configuring your barcode scanner
    ///   to emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadAdditionalDataNoCalibration = 261,

    /// <summary>
    /// <p>Your system may not read additional data reliably.</p>
    /// <p>Some barcodes may contain additional characters that are never included in unique identifiers.</p>
    /// <p>If you scan other barcodes, and experience difficulty, try configuring your barcode scanner
    ///   to emulate a numeric keypad.</p>
    /// </summary>
    CannotReadAdditionalData = 265,

    /// <summary>
    /// <p>Your system cannot read EDI data reliably.</p>
    /// <p>Some barcodes may contain EDI data. Your software must implement character mapping to read these barcodes reliably.</p>
    /// <p>If you scan EDI barcodes, and experience difficulty, try configuring your barcode scanner to
    ///   emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadEdiCharactersReliably = 270,

    /// <summary>
    /// <p>Your system may not read EDI characters reliably.</p>
    /// <p>Some barcodes may contain EDI characters.</p>
    /// <p>If you scan EDI barcodes, and experience difficulty, try configuring your barcode scanner
    ///   to emulate a numeric keypad.</p>
    /// </summary>
    MayNotReadEdiCharactersNoCalibration = 271,

    /// <summary>
    /// <p>Your system cannot read EDI characters reliably.</p>
    /// <p>Some barcodes may contain EDI data.</p>
    /// <p>If you scan EDI barcodes, and experience difficulty, try configuring your barcode scanner to
    /// emulate a numeric keypad.</p>
    /// </summary>
    CannotReadEdiCharacters = 275,

    /// <summary>
    /// <p>Your system reads unique identifiers reliably.</p>
    /// </summary>
    ReadsUniqueIdentifiersReliably = 100,

    /// <summary>
    /// <p>Your system reads unique identifiers reliably.</p>
    /// <p>However, you did not test compatibility for German PPN barcodes.
    ///   Your system may not be able to read German PPN barcodes reliably.
    ///   You may need to use manual data entry to verify PPN barcodes.</p>
    /// </summary>
    ReadsUniqueIdentifiersReliablyNoPpnTest = 105,

    /// <summary>
    /// <p>Your system reads most unique identifiers reliably.</p>
    /// <p>However, it may not be able to read German PPN barcodes reliably.
    ///   If your software cannot compensate, use manual data entry to verify
    ///   PPN unique identifiers or configure your barcode scanner to emulate
    ///   a numeric keypad.</p>
    /// </summary>
    ReadsUniqueIdentifiersReliablyMayNotReadPpn = 110,

    /// <summary>
    /// <p>Your system reads most unique identifiers reliably.</p>
    /// <p>However, it cannot read German PPN barcodes reliably. Use manual
    ///   data entry to verify PPN unique identifiers or configure your barcode
    ///   scanner to emulate a numeric keypad.</p>
    /// </summary>
    ReadsUniqueIdentifiersReliablyExceptPpn = 115,
}