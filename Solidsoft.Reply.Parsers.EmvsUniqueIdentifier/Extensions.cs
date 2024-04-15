// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Solidsoft Reply Ltd">
// Copyright (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// </copyright>
// <summary>
// Extension methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using Properties;

using System;
using System.Globalization;
using System.Text;

/// <summary>
///   Extension methods.
/// </summary>
public static class Extensions
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Composite format for Parser_Error_100.
    /// </summary>
    private static readonly CompositeFormat ParserError100 = CompositeFormat.Parse(Resources.Parser_Error_100);
#endif

    /// <summary>
    ///   Converts the value of this instance to its equivalent string representation using culture-invariant format
    ///   information.
    /// </summary>
    /// <param name="thisCharacter">The character to be converted.</param>
    /// <returns>A culture-invariant string.</returns>
    public static string ToInvariantString(this char thisCharacter)
    {
        return thisCharacter.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Converts the numeric value of this instance to its equivalent string representation using the specified format and
    ///   culture-invariant format information.
    /// </summary>
    /// <param name="thisInteger">The integer to be converted.</param>
    /// <param name="format">The string format to apply.</param>
    /// <returns>A culture-invariant string representing the integer.</returns>
    public static string ToInvariantString(this int thisInteger, string format)
    {
        return thisInteger.ToString(format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Converts the value of the current DateTime object to its equivalent string representation using the specified
    ///   format and culture-neutral format information.
    /// </summary>
    /// <param name="thisDateTime">The DateTime object to be converted.</param>
    /// <param name="format">A standard or custom date and time format string.</param>
    /// <returns>A culture-invariant string representing the DateTime object.</returns>
    public static string ToInvariantString(this DateTime thisDateTime, string format)
    {
        return thisDateTime.ToString(format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Convert a character representation of an integer to an integer.
    /// </summary>
    /// <param name="character">A character representing a digit.</param>
    /// <returns>An integer.</returns>
    /// <exception cref="ArgumentException">The character cannot be converted to an integer.</exception>
    // ReSharper disable once UnusedMember.Global
    public static int ToInt(this char character)
    {
        if (int.TryParse(character.ToInvariantString(), out var integer))
        {
            return integer;
        }

#pragma warning disable SA1116 // Split parameters should start on-line after declaration
        throw new ArgumentException(
            string.Format(CultureInfo.InvariantCulture,
#if NET8_0_OR_GREATER
            ParserError100,
#else
            Resources.Parser_Error_100,
#endif
            character));
#pragma warning restore SA1116 // Split parameters should start on-line after declaration
    }
}