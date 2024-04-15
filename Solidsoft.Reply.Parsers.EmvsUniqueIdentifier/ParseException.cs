// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseException.cs" company="Solidsoft Reply Ltd">
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
// Represents a parse exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using System;
using System.Runtime.Serialization;

using Packs;

/// <summary>
///   Represents a parse exception.
/// </summary>
[Serializable]
public class ParseException : PackIdentifierException
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="ParseException" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public ParseException()
        : base(-1, string.Empty)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ParseException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The message.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public ParseException(string message)
        : base(-1, message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ParseException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The message.
    /// </param>
    /// <param name="innerException">
    ///   The inner exception.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public ParseException(string message, Exception innerException)
        : base(-1, message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ParseException" /> class.
    /// </summary>
    /// <param name="errorNumber">
    ///   The error number.
    /// </param>
    /// <param name="message">
    ///   The message.
    /// </param>
    /// <param name="dataElementTitle">
    ///   The title of the data element where the parse exception occurred.
    /// </param>
    /// <param name="characterPosition">
    ///   The current character position.
    /// </param>
    /// <param name="isFatal">
    ///   Indicates whether the error is fatal.
    /// </param>
    public ParseException(
        int errorNumber,
        string message,
        string dataElementTitle,
        int characterPosition,
        bool isFatal)
        : base(errorNumber, message)
    {
        DataElementTitle = dataElementTitle;
        CharacterPosition = characterPosition;
        IsFatal = isFatal;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ParseException" /> class.
    /// </summary>
    /// <param name="info">The serialization information.</param>
    /// <param name="context">The streaming context.</param>
#if NET5_0_OR_GREATER
#pragma warning disable S1133 // Deprecated code should be removed
    [Obsolete("Formatter serialisation has been deprecated in .NET.", DiagnosticId = "SYSLIB0051")]
#pragma warning restore S1133 // Deprecated code should be removed
#endif
    protected ParseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the character position where the error occurred.
    /// </summary>
    public int CharacterPosition { get; }

    /// <summary>
    ///   Gets the title of the data element where the parse exception occurred.
    /// </summary>
    public string DataElementTitle { get; } = string.Empty;

    /// <summary>
    ///   Gets a value indicating whether the error is fatal (further parsing is aborted).
    /// </summary>
    public bool IsFatal { get; }
}