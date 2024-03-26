// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalibrationView.cs" company="Solidsoft Reply Ltd.">
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
// The view used for calibration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Views;

using Components;
using BarcodeScanner.Calibration;
using System.Collections.Generic;

using ConsoleMvc;

using Properties;

using static System.Console;

/// <summary>
/// The view used for calibration.
/// </summary>
public class CalibrationView : IView {

    /// <summary>
    /// The Calibration Options component.
    /// </summary>
    private readonly CalibrationOptions _calibrationOptions = new();

    /// <summary>
    /// The Calibration Background component.
    /// </summary>
    private readonly CalibrationBackground _calibrationBackground = new();

    /// <summary>
    /// The Keep Scanning Message component
    /// </summary>
    private readonly KeepScanningMessage _keepScanningMessage = new();

    /// <summary>
    /// The Calibration Save Error component.
    /// </summary>
    private readonly CalibrationSaveError _calibrationSaveError = new();

    /// <summary>
    /// The Standard Background component.
    /// </summary>
    private readonly StandardBackground _standardBackground = new();

#if STATELESS_MODEL
    /// <summary>
    /// Gets the current calibration token.
    /// </summary>
    public CalibrationToken CurrentCalibrationToken { get; private set; }
#else
    /// <summary>
    /// Gets the current calibration token enumerator.
    /// </summary>
    public IEnumerator<Token> CalibrationTokens { get; private set; }
#endif

    /// <summary>
    /// Gets the error message when an error is encountered while saving calibration data..
    /// </summary>
    public string SaveErrorMessage { get; private set; }

    /// <summary>
    /// Gets a value indicating whether to include calibration
    /// tests for Format 06, and by implication, Format 05.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool IncludeFormat06 { get; private set; }

    /// <summary>
    /// Gets a list of reported calibration information.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public IList<Information> CalibrationInformation { get; private set; }

    /// <summary>
    /// Render the calibration view.
    /// </summary>
    public void Render()
    {
        _calibrationBackground.Render();
        WriteLine(Resources.CalibrationViewDoNotCallRender);
    }

    /// <summary>
    /// Render an inline component within the Calibration view.
    /// </summary>
    /// <param name="component">The name of identifier of the component to be rendered.</param>
    public void Render(string component) {
        switch (component)
        {
            case nameof(CalibrationOptions):
                _calibrationBackground.Render();
                _calibrationOptions.Render();
                break;
            case nameof(KeepScanningMessage):
#if STATELESS_MODEL
                _keepScanningMessage.RemainingBarcodeCount = CurrentCalibrationToken.Remaining;
#else
                _keepScanningMessage.RemainingBarcodeCount = CalibrationTokens.Current.Remaining;
#endif
                _keepScanningMessage.Render();
                break;
            case nameof(CalibrationSaveError):
                _calibrationSaveError.SaveErrorMessage = SaveErrorMessage;
                _calibrationSaveError.Render();
                break;
            case nameof(StandardBackground):
                _standardBackground.Render();
                break;
        }
    }

    /// <summary>
    /// Sets a property value.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    public void SetValue(string propertyName, object value)
    {
        switch (propertyName)
        {
#if STATELESS_MODEL
            case "CurrentCalibrationToken":
                CurrentCalibrationToken = (CalibrationToken)value;
#else
            case "CalibrationTokens":
                CalibrationTokens = (IEnumerator<Token>)value;
#endif
                break;
            case "SaveErrorMessage":
                SaveErrorMessage = (string)value;
                break;
            case "IncludeFormat06":
                IncludeFormat06 = (bool)value;
                break;
            case "CalibrationInformation":
                CalibrationInformation = (IList<Information>)value;
                break;
        }
    }
}