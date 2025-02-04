﻿using System;
using System.Linq;
using FluentAssertions;
using Solidsoft.Reply.BarcodeScanner.Calibration;

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests.StepDefinitions;

[Binding]
public sealed class AdviceStepDefinitions
{
    private Assumption _currentAssumption = Assumption.Agnostic;
    private string _baselineInput = string.Empty;
    private Calibrator _currentCalibrator = new(assumption: Assumption.Agnostic);
    private Token _currentToken = new();
    private Advice _currentAdvice;
    private int _checkedItemTypeCount = 0;

    private string GetBaselineInput(string input)
    {
        const string aimIdD2 = "]d2";
        const string lineTerminator = "\r";
        const string invariantUs =
            "! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z";
        const string nonInvariantUs = "# $ @ [ \\ ] ^ ` { | } ~";

        return input switch
        {
            "The United States" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    \x0004    {lineTerminator}",
            "The United States with no PPN assessment" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    {lineTerminator}",
            "The United States with no GS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}        \x001C    \x001E    \x001F    \x0004    {lineTerminator}",
            "The United States with no RS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C        \x001F    \x0004    {lineTerminator}",
            "The United States with no FS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D        \x001E    \x001F    \x0004    {lineTerminator}",
            "The United States with no US" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E        \x0004    {lineTerminator}",
            "The United States with no EOT" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F        {lineTerminator}",
            "The United States with null GS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \0    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with null RS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \0   \x001F    \x0004    {lineTerminator}",
            "The United States with null FS" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \0    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with null US" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \0   \x0004    {lineTerminator}",
            "The United States with null EOT" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    \0    {lineTerminator}",
            "The United States with GS as different character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001B    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as different character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001B   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as different character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001B    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as different character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001B   \x0004    {lineTerminator}",
            "The United States with EOT as different character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    \x001B    {lineTerminator}",
            "The United States with GS as ambiguous invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    Z    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as ambiguous invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    Z   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as ambiguous invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    Z    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as ambiguous invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    Z   \x0004    {lineTerminator}",
            "The United States with EOT as ambiguous invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    Z    {lineTerminator}",
            "The United States with GS as ambiguous non-invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    @    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as ambiguous non-invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    @   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as ambiguous non-invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    @    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as ambiguous non-invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    @   \x0004    {lineTerminator}",
            "The United States with EOT as ambiguous non-invariant character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    @    {lineTerminator}",
            "The United States with GS as AIM flag character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    ]    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as AIM flag character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    ]   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as AIM flag character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    ]    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as AIM flag character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    ]    \x0004    {lineTerminator}",
            "The United States with EOT as AIM flag character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    ]    {lineTerminator}",
            "The United States with GS as dead key character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \0é    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as dead key character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \0é   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as dead key character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \0é    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as dead key character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \0é    \x0004    {lineTerminator}",
            "The United States with EOT as dead key character" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    \0é    {lineTerminator}",
            "The United States with GS as ligature" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    óé    \x001C    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with RS as ligature" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    óé   \x001F    \x0004    {lineTerminator}",
            "The United States with FS as ligature" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    óé    \x001E   \x001F    \x0004    {lineTerminator}",
            "The United States with US as ligature" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    óé    \x0004    {lineTerminator}",
            "The United States with EOT as ligature" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \x001D    \x001C    \x001E    \x001F    óé    {lineTerminator}",
            "The United States with null GS - No PPN" => $"{aimIdD2}  {invariantUs}   {nonInvariantUs}    \0    {lineTerminator}",
            "Dodgy Scanner - No PPN" => $"  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` {{ ~ }} ¬    \u0000    {lineTerminator}",
           ////                             ! " % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \0    {lineTerminator}
            _ => throw new ArgumentException("Invalid baseline input", nameof(input))
        };
    }

    [Given("the baseline input is for (.*)")]
    public void GivenTheBaselineInputIsFor(string input) {
        _baselineInput = GetBaselineInput(input);
    }

    [When("the baseline input is submitted to an agnostic calibrator")]
    public void WhenTheBaselineInputIsSubmittedToTheAgnosticCalibrator() {
        _currentAssumption = Assumption.Agnostic;
        _currentCalibrator = new Calibrator(assumption: _currentAssumption);
        _currentToken = _currentCalibrator.Calibrate(_baselineInput, _currentCalibrator.CalibrationTokens().FirstOrDefault()  );
    }

    [When("the baseline input is submitted to an agnostic calibrator with no PPN test")]
    public void WhenTheBaselineInputIsSubmittedToTheAgnosticCalibratorWithNoPpnTest()
    {
        _currentAssumption = Assumption.Agnostic;
        _currentCalibrator = new Calibrator(assumption: _currentAssumption);
        _currentCalibrator.AssessFormatSupport = false;
        try {
            _currentToken = _currentCalibrator.Calibrate(_baselineInput, _currentCalibrator.CalibrationTokens().FirstOrDefault());
        }
        catch        {
            _currentCalibrator.AssessFormatSupport = true;
            throw;
        }
    }

    [When("the baseline input is submitted to an agnostic calibrator with no PPN assessment")]
    public void WhenTheBaselineInputIsSubmittedToTheAgnosticCalibratorWithNoPpnAssessment() {
        _currentAssumption = Assumption.Agnostic;
        _currentCalibrator = new Calibrator(assumption: _currentAssumption) {
            AssessFormatSupport = false
        };
        _currentToken = _currentCalibrator.Calibrate(_baselineInput, _currentCalibrator.CalibrationTokens().FirstOrDefault());
    }

    [When("the baseline input is submitted to a calibration calibrator")]
    public void WhenTheBaselineInputIsSubmittedToTheCalibrationCalibrator() {
        _currentAssumption = Assumption.Calibration;
        _currentCalibrator = new Calibrator(assumption: _currentAssumption);
        _currentToken = _currentCalibrator.Calibrate(_baselineInput, _currentCalibrator.CalibrationTokens().FirstOrDefault());
    }

    [When("the baseline input is submitted to a no calibration calibrator")]
    public void WhenTheBaselineInputIsSubmittedToTheNoCalibrationCalibrator() {
        _currentAssumption = Assumption.NoCalibration;
        _currentCalibrator = new Calibrator(assumption: _currentAssumption);
        _currentToken = _currentCalibrator.Calibrate(_baselineInput, _currentCalibrator.CalibrationTokens().FirstOrDefault());
    }


    [When("advice is generated from the calculated system capabilities")]
    public void WhenAdviceIsGeneratedFromTheCalculatedSystemCapabilities()
    {
        _checkedItemTypeCount = 0;

        if (_currentToken.SystemCapabilities == null) {
            _currentAdvice = null;
            return;
        }

        _currentAdvice = Advice.CreateAdvice(_currentToken.SystemCapabilities);
    }

    [Then("the advice should contain an advice item for (.*)")]
    public void ThenTheAdviceShouldContainAnAdviceItemFor(string adviceType) {
        if (_currentAdvice is null)
        {
            throw new InvalidOperationException("Advice has not been generated yet");
        }

        if (string.IsNullOrWhiteSpace(adviceType))
        {
            throw new ArgumentException("Advice type cannot be null or empty", nameof(adviceType));
        }

        _currentAdvice.Items.Should().Contain(ai => ai.AdviceType == Enum.Parse<AdviceType>(adviceType));
        _checkedItemTypeCount++;
    }

    [Then("the advice should contain no other advice items")]
    public void ThenTheAdviceShouldContainNoOtherAdviceItems()
    {
        _currentAdvice.Items.Count().Should().Be(_checkedItemTypeCount);
    }
}
