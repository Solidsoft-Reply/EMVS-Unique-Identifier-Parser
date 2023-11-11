// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RECT.cs" company="Solidsoft Reply Ltd.">
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
// The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

using System.Runtime.InteropServices;

/// <summary>
/// The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public struct RECT
{
    public int Left, Top, Right, Bottom;

    public RECT(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

    public int X
    {
        // ReSharper disable once UnusedMember.Global
#pragma warning disable IDE0251 // Make member 'readonly'
        get => Left;
#pragma warning restore IDE0251 // Make member 'readonly'
        set
        {
            Right -= Left - value; 
            Left = value;
        }
    }

    public int Y
    {
        // ReSharper disable once UnusedMember.Global
#pragma warning disable IDE0251 // Make member 'readonly'
        get => Top;
#pragma warning restore IDE0251 // Make member 'readonly'
        set
        {
            Bottom -= Top - value; 
            Top = value;
        }
    }

    public int Height
    {
#pragma warning disable IDE0251 // Make member 'readonly'
        get => Bottom - Top;
#pragma warning restore IDE0251 // Make member 'readonly'
        set => Bottom = value + Top;
    }

    public int Width
    {
#pragma warning disable IDE0251 // Make member 'readonly'
        get => Right - Left;
#pragma warning restore IDE0251 // Make member 'readonly'
        set => Right = value + Left;
    }

    public static implicit operator System.Drawing.Rectangle(RECT r)
    {
        return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
    }

    public static implicit operator RECT(System.Drawing.Rectangle r)
    {
        return new RECT(r);
    }

    public static bool operator ==(RECT r1, RECT r2)
    {
        return r1.Equals(r2);
    }

    public static bool operator !=(RECT r1, RECT r2)
    {
        return !r1.Equals(r2);
    }

    // ReSharper disable once MemberCanBePrivate.Global
#pragma warning disable IDE0251 // Make member 'readonly'
    public bool Equals(RECT r)
#pragma warning restore IDE0251 // Make member 'readonly'
    {
        return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
    }

    public override bool Equals(object obj)
    {
        return obj switch
               {
                   RECT rect                          => Equals(rect),
                   System.Drawing.Rectangle rectangle => Equals(new RECT(rectangle)),
                   _                                  => false
               };
    }

#pragma warning disable IDE0251 // Make member 'readonly'
    public override int GetHashCode()
#pragma warning restore IDE0251 // Make member 'readonly'
    {
        return ((System.Drawing.Rectangle)this).GetHashCode();
    }

#pragma warning disable IDE0251 // Make member 'readonly'
    public override string ToString()
#pragma warning restore IDE0251 // Make member 'readonly'
    {
        return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
    }
}