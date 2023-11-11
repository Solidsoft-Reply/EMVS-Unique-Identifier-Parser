// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanningView.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
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
// The view used for scanning and parsing.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Views;

using BarcodeScanner.Calibration;
using Parsers.EmvsUniqueIdentifier.Packs;

using System;
using System.Collections.Generic;
using Components;

using static System.Console;
using ConsoleMvc;

/// <summary>
/// The view used for scanning and parsing.
/// </summary>
public class ScanningView : IView {
    /// <summary>
    /// The Automatic Entry Status component.
    /// </summary>
    private readonly AutomaticEntryStatus _automaticEntryStatus = new();

    /// <summary>
    /// The Command Prompt component.
    /// </summary>
    private readonly CommandPrompt _commandPrompt = new();

    /// <summary>
    /// The Parser Results component.
    /// </summary>
    private readonly ParserResults _parserResults = new();

    /// <summary>
    /// The Calibration Report component.
    /// </summary>
    private readonly CalibrationReport _calibrationReport = new();

    /// <summary>
    /// The Advice Report component.
    /// </summary>
    private readonly AdviceReport _adviceReport = new();

    /// <summary>
    /// The Calibration Failed Message component.
    /// </summary>
    private readonly CalibrationFailedMessage _calibrationFailedMessage = new();

    /// <summary>
    /// The Menu component.
    /// </summary>
    private readonly Menu _menu = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ScanningView"/> class.
    /// </summary>
    /// <param name="inputHandler">The input handler used in this view.</param>
    public ScanningView(ModalInputHandler inputHandler)
    {
        InputHandler = inputHandler;

        // Set command prompt
        InputHandler.Reset();
        InputHandler.PromptOffset = 13;

        // Render up console to its default.
        Render();
    }

    /// <summary>
    /// Gets the input handler used in this view.
    /// </summary>
    private ModalInputHandler InputHandler { get; }

    /// <summary>
    /// Gets or sets the raw input from the scanner.
    /// </summary>
    private string Input { get; set; }

    /// <summary>
    /// Gets or sets the pre-processed input from the scanner.
    /// </summary>
    private string PreprocessedInput { get; set; }

    /// <summary>
    /// Gets or sets the pack identifier represented by the processed input from the scanner.
    /// </summary>
    private PackIdentifier PackIdentifier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a pack identifier was found.
    /// </summary>
    private bool PackIdentifierFound { get; set; }

    /// <summary>
    /// Get or sets the width of the parsed data.
    /// </summary>
    private int DataWidth { get; set; }

    /// <summary>
    /// Gets a value indicating whether to include calibration
    /// tests for Format 06, and by implication, Format 05.
    /// </summary>
    private bool IncludeFormat06 { get; set; }

    /// <summary>
    /// Gets a list of reported calibration information.
    /// </summary>
    private IList<CalibrationInformation> CalibrationInformation { get; set; }

    /// <summary>
    /// Gets a list of reported calibration information.
    /// </summary>
    private SystemCapabilities SystemCapabilities { get; set; }

    /// <summary>
    /// Renders the view to the default.  In this case, the default is to clear the console and return to
    /// the default colours.
    /// </summary>
    public void Render()
    {
        ResetColor();
        Utilities.ClearConsole();
        _menu.Render();
        InputHandler.Reset();

        if (string.IsNullOrWhiteSpace(Input))
        {
            // Write the prompt or content.
            _commandPrompt.Render();
            return;
        }

        WriteLine(new string('\u2550', BufferWidth - 1));
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine($@"{ConvertControlCharactersToDosConventions(Input) ?? string.Empty}");
        ResetColor();
    }

    /// <summary>
    /// Renders a component in the view.
    /// </summary>
    /// <param name="component">The name of identifier of the component to be rendered.</param>
    public void Render(string component)
    {
        switch (component)
        {
            case nameof(AutomaticEntryStatus):
                _automaticEntryStatus.AutomaticEntry = InputHandler.AutomaticEntry;
                _automaticEntryStatus.Render();
                break;
            case nameof(ParserResults):
                _parserResults.PreprocessedInput = PreprocessedInput;
                _parserResults.PackIdentifier = PackIdentifier;
                _parserResults.PackIdentifierFound = PackIdentifierFound;
                _parserResults.DataWidth = DataWidth;
                _parserResults.Render();
                break;
            case nameof(CalibrationReport):
                _calibrationReport.IncludeFormat06 = IncludeFormat06;
                _calibrationReport.CalibrationInformation = CalibrationInformation;
                _calibrationReport.Render();
                break;
            case nameof(AdviceReport):
                _adviceReport.SystemCapabilities = SystemCapabilities;
                _adviceReport.Render();
                break;
            case nameof(CommandPrompt):
                _commandPrompt.Render();
                break;
            case nameof(CalibrationFailedMessage):
                _menu.Render();
                _calibrationFailedMessage.Render();
                break;
        }
    }

    /// <summary>
    /// Sets a property value.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    public void SetValue(string propertyName, object value)
    {
        switch (propertyName)
        {
            case "Input":
                Input = (string)value;
                break;
            case "PreprocessedInput":
                PreprocessedInput = (string)value;
                break;
            case "PackIdentifier":
                PackIdentifier = (PackIdentifier)value;
                break;
            case "PackIdentifierFound":
                PackIdentifierFound = (bool)value;
                break;
            case "DataWidth":
                DataWidth = (int)value;
                break;
            case "IncludeFormat06":
                IncludeFormat06 = (bool)value;
                break;
            case "CalibrationInformation":
                CalibrationInformation = (IList<CalibrationInformation>)value;
                break;
            case "SystemCapabilities":
                SystemCapabilities = (SystemCapabilities)value;
                break;
        }
    }

    private static string ConvertControlCharactersToDosConventions(string input)
    {
        if (input == null) return null;

        foreach (var c in input)
            input = (int)c switch
            {
                < 32 => input.Replace(c.ToString(), $"^{(char)(c + 64)}"),
                _ => input
            };

        return input;
    }
}