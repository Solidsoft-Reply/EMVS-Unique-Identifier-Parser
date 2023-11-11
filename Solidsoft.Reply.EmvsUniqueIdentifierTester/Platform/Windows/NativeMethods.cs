// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Solidsoft Reply Ltd.">
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
// Native methods for input.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Native methods for input.
/// </summary>
internal static class NativeMethods
{
    /// <summary>
    /// The GetDC function retrieves a handle to a device context (DC) for the client
    /// area of a specified window or for the entire screen. You can use the returned
    /// handle in subsequent GDI functions to draw in the DC. The device context is an
    /// opaque data structure, whose values are used internally by GDI.
    /// </summary>
    /// <param name="handleWnd">
    /// A handle to the window whose DC is to be retrieved. If this value is NULL,
    /// GetDC retrieves the DC for the entire screen.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is a handle to the DC for the
    /// specified window's client area.</p>
    /// <p>If the function fails, the return value is NULL.</p>
    /// </returns>
    [DllImport("user32")]
    internal static extern IntPtr GetDC(IntPtr handleWnd);

    /// <summary>
    /// The ReleaseDC function releases a device context (DC), freeing it for use by other
    /// applications. The effect of the ReleaseDC function depends on the type of DC. It
    /// frees only common and window DCs. It has no effect on class or private DCs.
    /// </summary>
    /// <param name="handleWnd">A handle to the window whose DC is to be released.</param>
    /// <param name="handleDc">A handle to the DC to be released.</param>
    /// <returns>
    /// <p>The return value indicates whether the DC was released. If the DC was released,
    /// the return value is 1.</p>
    /// <p>If the DC was not released, the return value is zero.</p>
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool ReleaseDC(IntPtr handleWnd, IntPtr handleDc);

    /// <summary>
    ///        Creates a memory device context (DC) compatible with the specified device.
    /// </summary>
    /// <param name="hdc">A handle to an existing DC. If this handle is NULL,
    ///        the function creates a memory DC compatible with the application's current screen.</param>
    /// <returns>
    ///        If the function succeeds, the return value is the handle to a memory DC.
    ///        If the function fails, the return value is <see cref="System.IntPtr.Zero"/>.
    /// </returns>
    [DllImport("gdi32.dll", EntryPoint = nameof(CreateCompatibleDC), SetLastError = true)]
    internal static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

    /// <summary>Selects an object into the specified device context (DC). The new object replaces the previous object of the same type.</summary>
    /// <param name="hdc">A handle to the DC.</param>
    /// <param name="handleGdiObject">A handle to the object to be selected.</param>
    /// <returns>
    ///   <para>If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced. If the selected object is a region and the function succeeds, the return value is one of the following values.</para>
    ///   <para>SIMPLEREGION - Region consists of a single rectangle.</para>
    ///   <para>COMPLEXREGION - Region consists of more than one rectangle.</para>
    ///   <para>NULLREGION - Region is empty.</para>
    ///   <para>If an error occurs and the selected object is not a region, the return value is <c>NULL</c>. Otherwise, it is <c>HGDI_ERROR</c>.</para>
    /// </returns>
    /// <remarks>
    ///   <para>This function returns the previously selected object of the specified type. An application should always replace a new object with the original, default object after it has finished drawing with the new object.</para>
    ///   <para>An application cannot select a single bitmap into more than one DC at a time.</para>
    ///   <para>ICM: If the object being selected is a brush or a pen, color management is performed.</para>
    /// </remarks>
    [DllImport("gdi32.dll", EntryPoint = nameof(SelectObject))]
    [SuppressMessage("ReSharper", "CommentTypo", Justification = "Capitalisation fits with Windows.")]
    internal static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr handleGdiObject);

    /// <summary>
    /// The GetObject function retrieves information for the specified graphics object.
    /// </summary>
    /// <param name="handleGdiObject">Handle to graphics object.</param>
    /// <param name="bufferSize">Size of buffer for object information.</param>
    /// <param name="lpvObject">Should be IntPtr, but we know we will use it only for BITMAP.</param>
    /// <returns>
    /// <p>If the function succeeds, and lpvObject is a valid pointer, the return value is the number
    /// of bytes stored into the buffer.</p>
    /// <p>If the function succeeds, and lpvObject is NULL, the return value is the number of bytes
    /// required to hold the information the function would store into the buffer.</p>
    /// <p>If the function fails, the return value is zero.</p>
    /// </returns>
    [DllImport("gdi32", CharSet = CharSet.Auto)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once StyleCop.SA1650
    internal static extern int GetObject(
        IntPtr handleGdiObject,
        int bufferSize,
        out Bitmap lpvObject);

    /// <summary>
    /// The DeleteDC function deletes the specified device context (DC).
    /// </summary>
    /// <param name="hdc">A handle to the device context.</param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero.</p>
    /// </returns>
    [DllImport("gdi32.dll", EntryPoint = nameof(DeleteDC))]
    internal static extern bool DeleteDC([In] IntPtr hdc);

    /// <summary>
    /// The DeleteObject function deletes a logical pen, brush, font, bitmap,
    /// region, or palette, freeing all system resources associated with the object.
    /// After the object is deleted, the specified handle is no longer valid.
    /// </summary>
    /// <param name="handleObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the specified handle is not valid or is currently selected into a DC, the return value is zero.</p>
    /// </returns>
    [DllImport("gdi32.dll", EntryPoint = nameof(DeleteObject))]
    internal static extern bool DeleteObject([In] IntPtr handleObject);

    /// <summary>
    /// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels
    /// from the specified source device context into a destination device context.
    /// </summary>
    /// <param name="contextHandleDestination">A handle to the destination device context.</param>
    /// <param name="xDestination">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
    /// <param name="yDestination">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
    /// <param name="width">The width, in logical units, of the source and destination rectangles.</param>
    /// <param name="height">The height, in logical units, of the source and the destination rectangles.</param>
    /// <param name="contextHandleSource">A handle to the source device context.</param>
    /// <param name="xSource">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
    /// <param name="ySource">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
    /// <param name="rasterOperationCode">
    /// A raster-operation code. These codes define how the color data for the source rectangle is to be
    /// combined with the color data for the destination rectangle to achieve the final color.
    /// </param>
    /// <returns>
    /// <p>If the function succeeds, the return value is nonzero.</p>
    /// <p>If the function fails, the return value is zero. To get extended error information, call GetLastError.</p>
    /// </returns>
    [DllImport("gdi32.DLL")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once StyleCop.SA1650
    internal static extern bool BitBlt(
        IntPtr contextHandleDestination,
        int xDestination,
        int yDestination,
        int width,
        int height,
        IntPtr contextHandleSource,
        int xSource,
        int ySource,
        uint rasterOperationCode);
}