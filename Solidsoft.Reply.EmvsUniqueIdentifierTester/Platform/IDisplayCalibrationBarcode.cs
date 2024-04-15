// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDisplayCalibrationBarcode.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Manages the display of the calibration barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform;

using System;

/// <summary>
/// Manages the display of the calibration barcode.
/// </summary>
internal interface IDisplayCalibrationBarcode {
    /// <summary>
    /// Displays the calibration barcode.
    /// </summary>
    /// <param name="hwnd">The handle of the pop-up window.</param>
    /// <param name="bitmap">A bitmap image to display.</param>
    void Display(IntPtr hwnd, System.Drawing.Bitmap bitmap);
}