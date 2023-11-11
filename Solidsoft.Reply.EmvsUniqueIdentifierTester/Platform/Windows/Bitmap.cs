// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bitmap.cs" company="Solidsoft Reply Ltd.">
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
// The Bitmap structure defines the height, width, color format, and bit values of a logical bitmap.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// The Bitmap structure defines the height, width, color format, and bit values of a logical bitmap.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Bitmap
{
    /// <summary>
    /// Specifies the bitmap type. For logical bitmaps, this member must be 0.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    // ReSharper disable once MemberCanBePrivate.Local
    public int BitmapType;

    /// <summary>
    /// Specifies the width of the bitmap in pixels. The width must be greater than 0.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    public int Width;

    /// <summary>
    /// Specifies the height of the bitmap in raster lines. The height must be greater than 0.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    public int Height;

    /// <summary>
    /// Specifies the number of bytes in each raster line. This value must be an even number
    /// since the graphics device interface (GDI) assumes that the bit values of a bitmap form
    /// an array of integer (2-byte) values. In other words, WidthBytes * 8 must be the next
    /// multiple of 16 greater than or equal to the value obtained when the Width member is
    /// multiplied by the BitsPixel member.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    // ReSharper disable once MemberCanBePrivate.Local
    public int WidthBytes;

    /// <summary>
    /// Specifies the number of color planes in the bitmap.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    // ReSharper disable once MemberCanBePrivate.Local
    public ushort Planes;

    /// <summary>
    /// Specifies the number of adjacent color bits on each plane needed to define a pixel.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    // ReSharper disable once MemberCanBePrivate.Local
    public ushort BitsPixel;

    /// <summary>
    /// Points to the location of the bit values for the bitmap. The Bits member must be a
    /// long pointer to an array of 1-byte values.
    /// </summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    // ReSharper disable once MemberCanBePrivate.Local
    public IntPtr Bits;
}