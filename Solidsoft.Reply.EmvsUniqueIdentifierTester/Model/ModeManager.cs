// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModeManager.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// A service that manages the activation and switching of modes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Model;

using System;
using Properties;
using ConsoleMvc;
using Controllers;

using static System.Console;

/// <summary>
/// A service that manages the activation and switching of modes.
/// </summary>
/// <remarks>
/// The pattern followed here is that modes are represented explicitly by
/// controllers, where there is a need for control.
/// </remarks>
public class ModeManager : IModeManager {
    /// <summary>
    /// The singleton input handler;
    /// </summary>
    private static ModalInputHandler _inputHandler;

    /// <summary>
    /// The current mode.
    /// </summary>
    private Mode _currentMode = Mode.Initialising;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModeManager"/> class.
    /// </summary>
    /// <param name="defaultMode"></param>
    public ModeManager(Mode defaultMode) {
        DefaultMode = defaultMode;

        try {
#pragma warning disable S3010
            _inputHandler ??= new ModalInputHandler(this, 13, true);
#pragma warning restore S3010
            _inputHandler.AutomaticEntry = true;
        }
        catch (VtUnsupportedException) {
            BackgroundColor = ConsoleColor.Red;
            ForegroundColor = ConsoleColor.White;
            WriteLine(Resources.VtSequencesNotSupported);
            ResetColor();
            throw;
        }

        // Create the controllers
        ScanController = new Scan(this, _inputHandler);
        CalibrateController = new Calibrate(this, _inputHandler);
    }

    /// <summary>
    /// The name of the current mode.
    /// </summary>
    public string ModeName { get; private set; }

    /// <summary>
    /// Gets or sets the current application mode.
    /// </summary>
    public Mode Mode {
        get => _currentMode;
        set => SetMode(value);
    }

    /// <summary>
    /// Gets or sets the scan controller.
    /// </summary>
    public Scan ScanController { get; }

    /// <summary>
    /// Gets or sets the calibration controller.
    /// </summary>
    public Calibrate CalibrateController { get; }

    /// <summary>
    /// Gets or sets the current controller.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public IController CurrentController { get; private set; }

    /// <summary>
    /// Gets or sets the previous mode.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public Mode LastMode { get; set; } = Mode.Initialising;

    /// <summary>
    /// Gets r sets the default mode.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Mode DefaultMode { get; }

    /// <summary>
    /// Sets the mode.
    /// </summary>
    /// <param name="mode">The required mode.</param>
    private void SetMode(Mode mode) {
        LastMode = _currentMode != mode ? _currentMode : LastMode;
        _currentMode = mode;
        ModeName = mode.ToString();

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (mode) {
            case Mode.Exiting:
                Environment.Exit(0);
                break;
            case Mode.Calibrating:
                CalibrateController.Activate();
                CurrentController = CalibrateController;
                break;
            default:
                ScanController.Activate();
                CurrentController = ScanController;
                break;
        }
    }
}