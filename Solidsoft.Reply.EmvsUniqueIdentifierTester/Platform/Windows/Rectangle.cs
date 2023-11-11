// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="Solidsoft Reply Ltd.">
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
// The Rectangle structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

/// <summary>
/// The Rectangle structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Rectangle : System.IEquatable<Rectangle>
{
    /// <summary>
    /// The x-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int Left;

    /// <summary>
    /// The y-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int Top;

    /// <summary>
    /// The x-coordinate of the lower-right corner of the rectangle.
    /// </summary>
    public int Right;

    /// <summary>
    /// The y-coordinate of the lower-right corner of the rectangle.
    /// </summary>
    public int Bottom;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> struct. 
    /// </summary>
    /// <param name="left">
    /// The x-coordinate of the upper-left corner of the rectangle.
    /// </param>
    /// <param name="top">
    /// The y-coordinate of the upper-left corner of the rectangle.
    /// </param>
    /// <param name="right">
    /// The x-coordinate of the lower-right corner of the rectangle.
    /// </param>
    /// <param name="bottom">
    /// The y-coordinate of the lower-right corner of the rectangle.
    /// </param>
    public Rectangle(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> struct. 
    /// </summary>
    /// <param name="r">
    /// A .NET rectangle used to initialize the Rect structure.
    /// </param>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    public Rectangle(System.Drawing.Rectangle r)
        : this(r.Left, r.Top, r.Right, r.Bottom)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int X
    {
        get => Left;
        set
        {
            Right -= Left - value;
            Left = value;
        }
    }

    /// <summary>
    /// Gets or sets the y-coordinate of the upper-left corner of the rectangle.
    /// </summary>
    public int Y
    {
        get => Top;
        set
        {
            Bottom -= Top - value;
            Top = value;
        }
    }

    /// <summary>
    /// Gets or sets the height of the rectangle.
    /// </summary>
    public int Height
    {
        get => Bottom - Top;
        set => Bottom = value + Top;
    }

    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    public int Width
    {
        get => Right - Left;
        set => Right = value + Left;
    }

    /// <summary>
    /// Gets or sets an ordered pair of integer x- and y-coordinates that defines the 
    /// origin of the rectangle.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public Point Location
    {
        get => new(Left, Top);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    /// <summary>
    /// Gets or sets an ordered set of integers that specify the size of the rectangle.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public Size Size
    {
        get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    /// <summary>
    /// Implicitly convert a Windows rectangle .NET rectangle.
    /// </summary>
    /// <param name="r">The Windows rectangle.</param>
    public static implicit operator System.Drawing.Rectangle(Rectangle r)
    {
        return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
    }

    /// <summary>
    /// Implicitly convert a .NET rectangle to a Windows rectangle.
    /// </summary>
    /// <param name="r">The .NET rectangle.</param>
    public static implicit operator Rectangle(System.Drawing.Rectangle r)
    {
        return new Rectangle(r);
    }

    /// <summary>
    /// The equality operator.  Compares the coordinates of two rectangles.
    /// </summary>
    /// <param name="r1">The first rectangle.</param>
    /// <param name="r2">The second rectangle.</param>
    /// <returns>
    /// True, if the rectangles overlap precisely; otherwise false.
    /// </returns>
    public static bool operator ==(Rectangle r1, Rectangle r2)
    {
        return r1.Equals(r2);
    }

    /// <summary>
    /// The inequality operator.  Compares the coordinates of two rectangles.
    /// </summary>
    /// <param name="r1">The first rectangle.</param>
    /// <param name="r2">The second rectangle.</param>
    /// <returns>
    /// True, if the rectangles do not overlap precisely; otherwise false.
    /// </returns>
    public static bool operator !=(Rectangle r1, Rectangle r2)
    {
        return !r1.Equals(r2);
    }

    /// <summary>
    /// Compares the coordinates of a rectangle with this rectangle.
    /// </summary>
    /// <param name="r">The rectangle to be compared.</param>
    /// <returns>
    /// True, if the rectangles overlap precisely; otherwise false.
    /// </returns>
    public bool Equals(Rectangle r)
    {
        return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
    }

    /// <summary>
    /// Compares the coordinates of a rectangle with this rectangle.
    /// </summary>
    /// <param name="obj">The rectangle to be compared.</param>
    /// <returns>
    /// True, if the rectangles overlap precisely; otherwise false.
    /// </returns>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public override bool Equals(object obj)
    {
        return obj switch
               {
                   Rectangle rectangle                => Equals(rectangle),
                   System.Drawing.Rectangle rectangle => Equals(new Rectangle(rectangle)),
                   _                                  => false
               };
    }

    /// <summary>
    /// Returns a hash code for this rectangle.
    /// </summary>
    /// <returns>
    /// A hash code for this rectangle.
    /// </returns>
    public override int GetHashCode()
    {
        return ((System.Drawing.Rectangle)this).GetHashCode();
    }

    /// <summary>
    /// Returns a textual representation of the rectangle.
    /// </summary>
    /// <returns>A textual representation of the rectangle.</returns>
    public override string ToString()
    {
        return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
    }
}