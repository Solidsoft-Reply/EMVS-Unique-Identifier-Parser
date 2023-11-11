﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDisplayCalibrationBarcode.cs" company="Solidsoft Reply Ltd.">
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
// Manages the display of the calibration barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform;

using System;

/// <summary>
/// Manages the display of the calibration barcode.
/// </summary>
internal interface IDisplayCalibrationBarcode
{
    /// <summary>
    /// Displays the calibration barcode.
    /// </summary>
    /// <param name="hwnd">The handle of the pop-up window.</param>
    /// <param name="bitmap">A bitmap image to display.</param>
    void Display(IntPtr hwnd, System.Drawing.Bitmap bitmap);
}