// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsToEuropeanGs1BarcodesWithAimParseTests.cs" company="Solidsoft Reply Ltd.">
//   (c) 2021 Solidsoft Reply Ltd.  All rights reserved.
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
// Unit tests for the Keyboard Calibrator
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests;

using System;
using System.Collections.Generic;
using System.Linq;

using Data;
using Xunit;

using BarcodeScanner.Calibration;
using Packs;

/// <summary>
/// Unit tests for parsing when the scanner is configured as a US keyboard
/// and the computer keyboard is a European keyboard, and the scanner outputs
/// an AIM identifier.
/// </summary>
public class UsToEuropeanGs1BarcodesWithAimParseTests
{
    /// <summary>
    /// A dictionary of base identifiers.
    /// </summary>
    private readonly Dictionary<string, IPackIdentifier> _baseIdentifiers;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsToEuropeanGs1BarcodesWithAimParseTests"/> class.
    /// </summary>
    public UsToEuropeanGs1BarcodesWithAimParseTests()
    {
        _baseIdentifiers = BasePackIdentifiers(
            BaseCalibration().CalibrationData);
    }

    /// <summary>
    /// Test a simple string
    /// </summary>
    [Fact]
    public void ToUnitedStates()
    {
        var calibrator = new Calibrator();
        var loopCount = 0;

        foreach (var token in calibrator.CalibrationTokens())
        {
            calibrator.Calibrate(ConvertToCharacterValues(BaselineCalibrationUsUs()), token);
            loopCount++;
        }

        Assert.Equal(1, loopCount);
    }

    /// <summary>
    /// Test calibration for a Belgian (Comma) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianComma()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BelgianCommaCalibration),
            BelgianCommaBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianFrench()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BelgianFrenchCalibration),
            BelgianFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian (Period) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianPeriod()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BelgianPeriodCalibration),
            BelgianPeriodBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BulgarianCalibration),
            BulgarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Latin) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianLatin()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BulgarianLatinCalibration),
            BulgarianLatinBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianPhonetic()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BulgarianPhoneticCalibration),
            BulgarianPhoneticBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic Traditional) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianPhoneticTraditional()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BulgarianPhoneticTraditionalCalibration),
            BulgarianPhoneticTraditionalBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Typewriter) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianTypewriter()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.BulgarianTypewriterCalibration),
            BulgarianTypewriterBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCzech()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.CzechCalibration),
            CzechBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech Programmers computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCzechProgrammers()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.CzechProgrammersCalibration),
            CzechProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCzechQwerty()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.CzechQwertyCalibration),
            CzechQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Danish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToDanish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.DanishCalibration),
            DanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Dutch computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToDutch()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.DutchCalibration),
            DutchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Estonian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToEstonian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.EstonianCalibration),
            EstonianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFinnish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.FinnishCalibration),
            FinnishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFinnishWithSami()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.FinnishWithSamiCalibration),
            FinnishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFrench()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.FrenchCalibration),
            FrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Scottish Gaelic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGaelic()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.GaelicCalibration),
            GaelicBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGerman()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.GermanCalibration),
            GermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGermanIbm()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.GermanIbmCalibration),
            GermanIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek (319) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreek319()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Greek319Calibration),
            Greek319BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek (319) Latin computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreek319Latin()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Greek319LatinCalibration),
            Greek319LatinBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreek()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.GreekCalibration),
            GreekBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreekLatin()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.GreekLatinCalibration),
            GreekLatinBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToHungarian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.HungarianCalibration),
            HungarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Hungarian 101-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToHungarian101Key()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Hungarian101KeyCalibration),
            Hungarian101KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Icelandic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToIcelandic()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.IcelandicCalibration),
            IcelandicBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Irish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToIrish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.IrishCalibration),
            IrishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Italian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToItalian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.ItalianCalibration),
            ItalianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Italian (142) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToItalian142()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Italian142Calibration),
            Italian142BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLatvian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LatvianCalibration),
            LatvianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLatvianQwerty()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LatvianQwertyCalibration),
            LatvianQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LithuanianCalibration),
            LithuanianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanianIbm()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LithuanianIbmCalibration),
            LithuanianIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanianStandard()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LithuanianStandardCalibration),
            LithuanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Luxembourgish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLuxembourgish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.LuxembourgishCalibration),
            LuxembourgishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 47-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToMaltese47Key()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Maltese47KeyCalibration),
            Maltese47KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 48-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToMaltese48Key()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Maltese48KeyCalibration),
            Maltese48KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToNorwegian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.NorwegianCalibration),
            NorwegianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToNorwegianWithSami()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.NorwegianWithSamiCalibration),
            NorwegianWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (214) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPolish214()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.Polish214Calibration),
            Polish214BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPolishProgrammers()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.PolishProgrammersCalibration),
            PolishProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Portuguese computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPortuguese()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.PortugueseCalibration),
            PortugueseBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianLegacy()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.RomanianLegacyCalibration),
            RomanianLegacyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianProgrammers()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.RomanianProgrammersCalibration),
            RomanianProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianStandard()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.RomanianStandardCalibration),
            RomanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sami Extended Finland Sweden computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSamiExtendedFinlandSwedenCalibration()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SamiExtendedFinlandSwedenCalibration),
            SamiExtendedFinlandSwedenBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sami Extended Norway computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSamiExtendedNorwayCalibration()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SamiExtendedNorwayCalibration),
            SamiExtendedNorwayBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovak (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSlovakQwerty()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SlovakQwertyCalibration),
            SlovakQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovenian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSlovenian()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SlovenianCalibration),
            SlovenianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianExtended()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SorbianExtendedCalibration),
            SorbianExtendedBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianStandard()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SorbianStandardCalibration),
            SorbianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianStandardLegacy()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SorbianStandardLegacyCalibration),
            SorbianStandardLegacyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Spanish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSpanish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SpanishCalibration),
            SpanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Croatian Standard computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToStandard()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.StandardCalibration),
            StandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwedish()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SwedishCalibration),
            SwedishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwedishWithSami()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SwedishWithSamiCalibration),
            SwedishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwissFrench()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SwissFrenchCalibration),
            SwissFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwissGerman()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.SwissGermanCalibration),
            SwissGermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a United Kingdom computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToUnitedKingdom()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.UnitedKingdomCalibration),
            UnitedKingdomBarcodeData());
    }

    /// <summary>
    /// Test calibration for a United Kingdom Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToUnitedKingdomExtended()
    {
        PerformParserTest(
            new CalibrationData(UsToEuropeanCalibrations.UnitedKingdomExtendedCalibration),
            UnitedKingdomExtendedBarcodeData());
    }

    /// <summary>
    /// Returns a collection of pack identifiers for the scanner keyboard layout.
    /// </summary>
    /// <param name="calibrationData">Calibration data for the scanner keyboard layout.</param>
    /// <returns>A collection of pack identifiers for the scanner keyboard layout.</returns>
    private static Dictionary<string, IPackIdentifier> BasePackIdentifiers(CalibrationData calibrationData)
    {
        var identifiers = new Dictionary<string, IPackIdentifier>();

        if (calibrationData is null)
        {
            return identifiers;
        }

        var parser = new Parser(calibrationData);

        foreach (var barcodeData in UnitedStatesBarcodeData())
        {
            var identifier = parser.Parse(barcodeData.Value);
            identifiers.Add(barcodeData.Key, identifier);
            Assert.True(identifier.IsValid);
        }

        return identifiers;
    }

    /// <summary>
    /// Returns the barcode data as entered using a United States keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedStatesBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.UsBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.UsBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.UsBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.UsBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.UsBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.UsBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.UsBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.UsBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.UsBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.UsBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.UsBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.UsBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian (Comma) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianCommaBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BelgianCommaBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian French keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianFrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BelgianFrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian (Period) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianPeriodBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BelgianPeriodBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BulgarianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Latin) keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianLatinBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BulgarianLatinBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianPhoneticBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Phonetic Traditional) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianPhoneticTraditionalBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BulgarianPhoneticTraditionalBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Typewriter) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianTypewriterBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.BulgarianTypewriterBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.CzechBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.CzechBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.CzechBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.CzechBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.CzechBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.CzechBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.CzechBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.CzechBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.CzechBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.CzechBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.CzechBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.CzechBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech Programmers computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.CzechProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.CzechQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Danish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> DanishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.DanishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.DanishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.DanishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.DanishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.DanishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.DanishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.DanishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.DanishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.DanishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.DanishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.DanishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.DanishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Dutch computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> DutchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.DutchBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.DutchBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.DutchBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.DutchBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.DutchBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.DutchBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.DutchBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.DutchBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.DutchBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.DutchBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.DutchBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.DutchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Estonian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> EstonianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.EstonianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Finnish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FinnishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.FinnishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Finnish with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FinnishWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.FinnishWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a French keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.FrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Scottish Gaelic computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GaelicBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.GaelicBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a German computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GermanBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.GermanBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.GermanBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.GermanBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.GermanBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.GermanBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.GermanBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.GermanBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.GermanBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.GermanBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.GermanBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.GermanBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.GermanBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a German (IBM) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GermanIbmBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.GermanIbmBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Greek (319) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Greek319BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Greek319Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Greek (319) Latin computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Greek319LatinBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Greek319LatinBarcode12 }
               };
    }
        
    /// <summary>
    /// Returns the expected barcode data for a Greek computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GreekBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.GreekBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.GreekBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.GreekBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.GreekBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.GreekBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.GreekBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.GreekBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.GreekBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.GreekBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.GreekBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.GreekBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.GreekBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Greek Latin computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GreekLatinBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.GreekLatinBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Hungarian101 computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Hungarian101KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Hungarian101KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Hungarian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> HungarianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.HungarianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Icelandic computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IcelandicBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.IcelandicBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Irish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IrishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.IrishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.IrishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.IrishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.IrishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.IrishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.IrishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.IrishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.IrishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.IrishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.IrishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.IrishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.IrishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Italian (142) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Italian142BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Italian142Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Italian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> ItalianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.ItalianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Latvian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LatvianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LatvianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Latvian (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LatvianQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LatvianQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LithuanianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian IBM computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianIbmBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LithuanianIbmBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian Standard computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LithuanianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a SwissFrench keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwissFrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SwissFrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Croatian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> StandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.StandardBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.StandardBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.StandardBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.StandardBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.StandardBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.StandardBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.StandardBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.StandardBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.StandardBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.StandardBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.StandardBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.StandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swedish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwedishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SwedishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swedish with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwedishWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SwedishWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swiss German computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwissGermanBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SwissGermanBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Norwegian with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> NorwegianWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.NorwegianWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Luxembourgish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LuxembourgishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.LuxembourgishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Norwegian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> NorwegianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.NorwegianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Maltese 47-Key computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Maltese47KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Maltese47KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Maltese 48-Key computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Maltese48KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Maltese48KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Polish (Programmers) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> PolishProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.PolishProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Polish (214) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Polish214BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.Polish214Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Portuguese computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> PortugueseBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.PortugueseBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Standard) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.RomanianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Legacy) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianLegacyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.RomanianLegacyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Programmers) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.RomanianProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Slovak (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SlovakQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SlovakQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sami Extended Finland-Sweden computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SamiExtendedFinlandSwedenBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SamiExtendedFinlandSwedenBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sami Extended Norway computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SamiExtendedNorwayBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SamiExtendedNorwayBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Slovenian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SlovenianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SlovenianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Spanish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SpanishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SpanishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Standard computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SorbianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Extended computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianExtendedBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SorbianExtendedBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Standard (Legacy) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianStandardLegacyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.SorbianStandardLegacyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom Extended computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomExtendedBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode1 },
                   { "Barcode2", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode2 },
                   { "Barcode3", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode3 },
                   { "Barcode4", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode4 },
                   { "Barcode5", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode5 },
                   { "Barcode6", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode6 },
                   { "Barcode7", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode7 },
                   { "Barcode8", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode8 },
                   { "Barcode9", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode9 },
                   { "Barcode10", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode10 },
                   { "Barcode11", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode11 },
                   { "Barcode12", UsToEuropeanGs1BarcodesWithAim.UnitedKingdomExtendedBarcode12 }
               };
    }

    /// <summary>
    /// Performs a calibration test.
    /// </summary>
    /// <returns>A calibration token.</returns>
    private static CalibrationToken BaseCalibration()
    {
        var computerKeyboardLayout = new Dictionary<string, IList<string>>
                                     {
                                         {
                                             UsToEuropeanGs1BarcodesWithAim.UsBaseline,
                                             new List<string>()
                                         }
                                     };

        var calibrator = new Calibrator();
        var loopCount = -1;
        CalibrationToken currentToken = default;

        foreach (var token in calibrator.CalibrationTokens())
        {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0)
            {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(baseLine), currentToken);
                loopCount++;
            }
            else
            {
                if (loopCount < computerKeyboardLayout[baseLine].Count)
                {
                    currentToken = calibrator.Calibrate(
                        ConvertToCharacterValues(computerKeyboardLayout[baseLine][loopCount++]),
                        currentToken);
                }
            }

            foreach (var error in currentToken.Errors)
            {
                System.Diagnostics.Debug.WriteLine(error.Description);
            }
        }

        return currentToken;
    }

    /// <summary>
    /// Return the character input for the baseline calibration barcode using a scanner
    /// configured for a US keyboard and computer with US keyboard layout. 
    /// </summary>
    /// <returns>
    /// The <see cref="string"/> for scanning the baseline calibration barcode.
    /// The scanner is configured for a US keyboard.
    /// The computer is configured for a US keyboard.
    /// </returns>
    private static string BaselineCalibrationUsUs()
    {
        var testString =
            "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    "
          + (char)29;
        return testString + "    \x001C    \x001E    \x001F    ";
    }

    /// <summary>
    /// Convert an input string to a list of character values.
    /// </summary>
    /// <param name="input">The string of characters to be converted.</param>
    /// <returns>A comma-separated value list of character values.</returns>
    private static int[] ConvertToCharacterValues(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Array.Empty<int>();
        }

        var outputBuilder = new int[input.Length];

        for (var idx = 0; idx < input.Length; idx++)
        {
            outputBuilder[idx] = input[idx];
        }

        return outputBuilder;
    }

    /// <summary>
    /// Perform a parser test.
    /// </summary>
    /// <param name="calibrationData">The calibration data.</param>
    /// <param name="scannedData">The scanned data.</param>
    private void PerformParserTest(CalibrationData calibrationData, Dictionary<string, string> scannedData)
    {
        Assert.NotNull(calibrationData);

        var parser = new Parser(calibrationData);

        foreach (var barcodeData in scannedData)
        {
            var identifier = parser.Parse(barcodeData.Value);
            Assert.True(identifier.IsValid);

            var baseIdentifier = _baseIdentifiers[barcodeData.Key];

            Assert.Equal(baseIdentifier.Scheme, identifier.Scheme);
            Assert.Equal(baseIdentifier.ProductCode, identifier.ProductCode);
            Assert.Equal(baseIdentifier.SerialNumber, identifier.SerialNumber);
            Assert.Equal(baseIdentifier.BatchIdentifier, identifier.BatchIdentifier);
            Assert.Equal(baseIdentifier.Expiry, identifier.Expiry);
        }
    }
}