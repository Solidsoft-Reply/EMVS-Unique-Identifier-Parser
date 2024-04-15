// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scan.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Controls scan mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Controllers;

using System;

using System.Diagnostics.CodeAnalysis;
using System.Text;

using System.IO;

using Model;
using Views;
using Properties;
using Components;
using ConsoleMvc;

using System.Collections.Generic;
using System.Linq;

using BarcodeScanner.Calibration;
using Parsers.EmvsUniqueIdentifier;


using static System.Console;

/// <summary>
/// Controls scan mode.
/// </summary>
public class Scan : IController {
    /// <summary>
    /// The mode manager for the application.
    /// </summary>
    private readonly ModeManager _modeManager;

    /// <summary>
    /// The input handler.
    /// </summary>
    private readonly ModalInputHandler _inputHandler;

    /// <summary>
    /// An instance of the pack parser.
    /// </summary>
    private Parser _baseParser;

    /// <summary>
    /// The view for scanning packs.
    /// </summary>
    private IView _scanningView;

    /// <summary>
    /// Set of name-value pair properties.
    /// </summary>
    private Tuple<string, object>[] _preambleProperties = [];

    /// <summary>
    /// Event handler.  Handles the key press event raised by the input handler.
    /// </summary>
    private readonly ConsoleCancelEventHandler _onCancel = (_, _) => {
        BackgroundColor = ConsoleColor.Black;
        ForegroundColor = ConsoleColor.White;

        Environment.Exit(-1);
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Scan"/> class.
    /// </summary>
    /// <param name="modeManager">The mode manager.</param>
    /// <param name="inputHandler">The input handler.</param>
    public Scan(ModeManager modeManager, ModalInputHandler inputHandler) {
        _modeManager = modeManager;
        _inputHandler = inputHandler;
        _inputHandler.KeyPress += OnKeyPress;
        _inputHandler.LineEntry += OnLineEntry;
    }

    /// <summary>
    /// Gets a value indicating whether to include calibration
    /// tests for Format 06, and by implication, Format 05.
    /// </summary>
    public bool IncludeFormat06 { get; set; }

    /// <summary>
    /// Gets a list of reported calibration information.
    /// </summary>
    public IList<Information> CalibrationInformation { get; set; }

    /// <summary>
    /// Gets a list of system capabilities.
    /// </summary>
    public SystemCapabilities SystemCapabilities { get; set; }

    /// <summary>
    /// Display the Calibration Failed message.
    /// </summary>
    public void CalibrationFailedMessage() {
        _scanningView.Render(nameof(Components.CalibrationFailedMessage));
    }

    /// <summary>
    /// Display the calibration report.
    /// </summary>
    public void DisplayCalibrationReport() {
        _scanningView.SetValue(nameof(IncludeFormat06), IncludeFormat06);
        _scanningView.SetValue(nameof(CalibrationInformation), CalibrationInformation);
        _scanningView.SetValue(nameof(SystemCapabilities), SystemCapabilities);
        _scanningView.Render(nameof(CalibrationReport));
    }

    /// <summary>
    /// Display the advice report.
    /// </summary>
    public void DisplayAdviceReport() {
        _scanningView.SetValue(nameof(SystemCapabilities), SystemCapabilities);
        _scanningView.Render(nameof(AdviceReport));
    }

    /// <summary>
    /// Display the command prompt report.
    /// </summary>
    public void DisplayCommandPrompt() {
        _scanningView.Render(nameof(CommandPrompt));
    }

    /// <summary>
    /// Initialise the application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private void Initialise() {
        // Initialise the input handler
        _inputHandler.Visibility = Visibility.Visible;
        _inputHandler.Reset();
        CancelKeyPress += _onCancel;

        _scanningView = new ScanningView(_inputHandler);

        // Set an preamble properties
        foreach (var (item1, item2) in _preambleProperties) {
            _scanningView.SetValue(item1, item2);
        }

        // Set the console to encode input and output.  Note that .NET Core (2) always uses UTF8,
        // regardless of these settings.
        InputEncoding = Encoding.UTF8;
        OutputEncoding = Encoding.UTF8;

        // Instantiate the pack identifier parser.
        _baseParser = new Parser();

        // Load the current calibration, if any
        var calibration = ReadCalibration(out var readErrorMessage);

        if (string.IsNullOrWhiteSpace(calibration)) {
            if (!string.IsNullOrWhiteSpace(readErrorMessage)) {
                new InitialisationError($@" The calibration could not be read: {readErrorMessage}").Render();
            }
        }
        else {
            _baseParser.Calibrator.CalibrationData = new Data(calibration);
        }

        if (CommandLineArguments.CodePage > 0) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try {
                OutputEncoding = Encoding.GetEncoding(CommandLineArguments.CodePage);
            }
            catch (IOException) {
                WriteLine(Resources.CodePageInitialiseFakeCommandLine, CommandLineArguments.CodePage);
                WriteLine(Resources.CodePageInitialiseError, CommandLineArguments.CodePage);
                WriteLine(Resources.CodePageInitialiseErrorRecovery);
                ReadKey();
            }
        }
        else if (!string.IsNullOrWhiteSpace(CommandLineArguments.CodePageName)) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try {
                OutputEncoding = Encoding.GetEncoding(CommandLineArguments.CodePageName);
            }
            catch (IOException) {
                WriteLine(Resources.CodePageInitialiseFakeCommandLine, CommandLineArguments.CodePage);
                WriteLine(Resources.CodePageInitialiseErrorRecovery);
                WriteLine(Resources.CodePageInitialiseError, CommandLineArguments.CodePageName);
                WriteLine(Resources.CodePageInitialiseErrorRecovery);
                ReadKey();
            }
        }
    }

    /// <summary>
    /// Read calibration from the keyboard calibration file, if any.
    /// </summary>
    /// <param name="errorMessage">The error message, if any.</param>
    /// <returns>The calibration, if any; otherwise and empty string</returns>
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
    private static string ReadCalibration(out string errorMessage) {
        errorMessage = string.Empty;

        try {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Keyboard.Calibration";

            if (File.Exists(filePath)) {
                var fileStream = new FileStream(filePath, FileMode.Open);

                using var reader = new StreamReader(fileStream);
                return reader.ReadToEnd();
            }
        }
        catch (Exception ex) {
            errorMessage = $"[{ex.GetType().Name}] - {ex.Message}";
        }

        return string.Empty;
    }

    /// <summary>
    /// Event handler.  Handles key press events.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="consoleKeyEventArgs">Console event arguments.</param>
    private void OnKeyPress(object sender, ConsoleKeyEventArgs consoleKeyEventArgs) {
        // Guard on mode.  This controller only handles events generated in scanning mode.
        if (consoleKeyEventArgs.Mode != Mode.Scanning.ToString()) {
            return;
        }

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (consoleKeyEventArgs.Key) {
            case ConsoleKey.F6:
                if ((consoleKeyEventArgs.Modifiers & ConsoleModifiers.Control)
                 == ConsoleModifiers.Control) {
                    _inputHandler.AutomaticEntry = !_inputHandler.AutomaticEntry;
                    _scanningView.Render(nameof(AutomaticEntryStatus));
                    _scanningView.Render(nameof(CommandPrompt));
                    _inputHandler.Reset();
                }
                break;

            case ConsoleKey.F11:
                if ((consoleKeyEventArgs.Modifiers & ConsoleModifiers.Control)
                 == ConsoleModifiers.Control) {
                    // Create controller for calibration
                    // ReSharper disable once AssignmentIsFullyDiscarded
                    _modeManager.CalibrateController.IncludeFormatTests =
                        (consoleKeyEventArgs.Modifiers & ConsoleModifiers.Shift)
                     == ConsoleModifiers.Shift;

                    CancelKeyPress -= _onCancel;

                    _modeManager.Mode = Mode.Calibrating;
                }

                break;
            case ConsoleKey.X:
                if (consoleKeyEventArgs.Modifiers == ConsoleModifiers.Control) {
                    _modeManager.Mode = Mode.Exiting;
                    Environment.Exit(0);
                }

                break;
            case ConsoleKey.F12:
                if (consoleKeyEventArgs.Modifiers == ConsoleModifiers.Control) {
                    _scanningView.SetValue("Input", string.Empty);
                    _scanningView.Render();
                }

                break;
        }
    }

    /// <summary>
    /// Event handler. Handles the line entry event raised by the input handler.
    /// </summary>
    /// <param name="sender">The input handler.</param>
    /// <param name="consoleLineEntryEventArgs">The line entry event arguments.</param>
    private void OnLineEntry(object sender, ConsoleLineEntryEventArgs consoleLineEntryEventArgs) {
        // Guard on mode.  This controller only handles events in scanning mode.
        if (consoleLineEntryEventArgs.Mode != Mode.Scanning.ToString()) {
            return;
        }

        var input = consoleLineEntryEventArgs.Line;

        if (input is "\r" or "\n" or "\r\n") {
            return;
        }

        ParseInput(input);

        // Reset the screen
        _scanningView.Render();
        _scanningView.Render(nameof(ParserResults));
        _scanningView.Render(nameof(CommandPrompt));
    }

    /// <inheritdoc />
    public Action<string> Activate(params Tuple<string, object>[] properties) {
        _preambleProperties = properties;
        Initialise();
        ////scanningView.Render();
        return _scanningView.Render;
    }

    /// <summary>
    /// Parse the keyboard input.
    /// </summary>
    /// <param name="input">The keyboard input.</param>
    private void ParseInput(string input) {
        // Strip off any CR or LF characters at end of string
        while (true) {
            if (!string.IsNullOrWhiteSpace(input) &&
                (input[^1] == '\r' ||
                 input[^1] == '\n')) {
                input = input[..^1];
            }
            else {
                break;
            }
        }

        // Parse the data
        var packIdentifier = _baseParser.Parse(input, out var preProcessedData);
        var packIdentifierFound = true;
        var dataWidth = new[]
                        {
                            packIdentifier.ProductCode.Length,
                            packIdentifier.SerialNumber.Length,
                            packIdentifier.BatchIdentifier.Length,
                            packIdentifier.Expiry.Length
                        }.Max();

        if (dataWidth == 0) {
            packIdentifierFound = false;
        }

        _scanningView.SetValue("Input", input);
        _scanningView.SetValue("PreprocessedInput", preProcessedData);
        _scanningView.SetValue("PackIdentifier", packIdentifier);
        _scanningView.SetValue("PackIdentifierFound", packIdentifierFound);
        _scanningView.SetValue("DataWidth", dataWidth);
    }
}