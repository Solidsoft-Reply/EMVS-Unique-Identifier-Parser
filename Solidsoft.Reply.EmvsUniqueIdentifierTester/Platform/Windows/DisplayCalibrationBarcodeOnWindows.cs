// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayCalibrationBarcodeOnWindows.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.
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
// Manages the display of the calibration barcode on Windows devices.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

using static NativeMethods;

/// <summary>
/// Manages the display of the calibration barcode on Windows devices.
/// </summary>
internal class DisplayCalibrationBarcodeOnWindows : IDisplayCalibrationBarcode
{
    /// <summary>
    /// Copies the source rectangle directly to the destination rectangle.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    private const int SRCCOPY = 0xcc0020;

    /// <inheritdoc />
    public void Display(IntPtr hwnd, System.Drawing.Bitmap bitmap)
    {
        DisplayCalibrationBarcode(hwnd, bitmap);
    }

    /// <summary>
    /// Displays the calibration barcode.
    /// </summary>
    /// <param name="hwnd">The handle of the pop-up window.</param>
    /// <param name="bitmap">A bitmap image to display.</param>
    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
    private static void DisplayCalibrationBarcode(IntPtr hwnd, System.Drawing.Bitmap bitmap)
    {
        // get a device context for the client area of the window
        var hdc = GetDC(hwnd);
        const int marginPercent = 10;
        const double scaleFactor = 8D;

        var originalBitmapWidth = bitmap.Width;
        var originalBitmapWidthHeight = bitmap.Height;
        var scaledBitMapWidth = Convert.ToInt32(originalBitmapWidth / scaleFactor);
        var scaledBitMapHeight = Convert.ToInt32(originalBitmapWidthHeight / scaleFactor);

        using (bitmap = new System.Drawing.Bitmap(bitmap, new Size(scaledBitMapWidth, scaledBitMapHeight)))
        {
            var x = Convert.ToInt32((originalBitmapWidth * (1 + marginPercent / 100D) / scaleFactor - scaledBitMapWidth) / 2);
            var y = Convert.ToInt32((originalBitmapWidthHeight * (1D + marginPercent / 100D) / scaleFactor - scaledBitMapHeight) / 2);

            var handleBmp = bitmap.GetHbitmap();

            // create a memory DC for the bitmap
            var handleMemDc = CreateCompatibleDC(hdc);
            SelectObject(handleMemDc, handleBmp);

            // get the image size and display the whole thing
            if (GetObject(handleBmp, Marshal.SizeOf(typeof(Bitmap)), out var bmp) > 0)
            {
                BitBlt(hdc, x, y, bmp.Width, bmp.Height, handleMemDc, 0, 0, SRCCOPY);
            }

            // Failed to get the bitmap.
            // release objects
            DeleteDC(handleMemDc);
            DeleteObject(handleBmp);
            ReleaseDC(hwnd, hdc);
        }
    }
}