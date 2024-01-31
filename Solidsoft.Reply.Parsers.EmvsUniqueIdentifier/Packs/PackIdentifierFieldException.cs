// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackIdentifierFieldException.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// A parse exception pack identifier field exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System;
using System.Runtime.Serialization;

/// <summary>
///   A pack identifier field exception.
/// </summary>
[Serializable]
public class PackIdentifierFieldException : PackIdentifierException
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierFieldException" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public PackIdentifierFieldException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierFieldException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The exception message.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public PackIdentifierFieldException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierFieldException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The exception message.
    /// </param>
    /// <param name="innerException">
    ///   The inner exception.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public PackIdentifierFieldException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierFieldException" /> class.
    /// </summary>
    /// <param name="errorNumber">
    ///   The error number.
    /// </param>
    /// <param name="message">
    ///   The message.
    /// </param>
    /// <param name="elementId">
    ///   The identifier for the element where the error occurred.
    /// </param>
    /// <param name="elementTitle">
    ///   The title for the element where the error occurred.
    /// </param>
    /// <param name="elementIndex">
    ///   The index position (1-based) of the data element.
    /// </param>
    public PackIdentifierFieldException(
        int errorNumber,
        string message,
        string elementId,
        string elementTitle,
        int elementIndex)
        : base(errorNumber, message)
    {
        ElementIndex = elementIndex;
        ElementId = elementId;
        ElementTitle = elementTitle;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierFieldException" /> class.
    /// </summary>
    /// <param name="info">The serialization information.</param>
    /// <param name="context">The streaming context.</param>
#if NET5_0_OR_GREATER
    [Obsolete("Formatter serialisation has been deprecated in .NET.", DiagnosticId = "SYSLIB0051")]
#endif
    protected PackIdentifierFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the identifier for the element where the error occurred.
    /// </summary>
    public string ElementId { get; } = string.Empty;

    /// <summary>
    ///   Gets the element index where the error occurred.
    /// </summary>
    public int ElementIndex { get; }

    /// <summary>
    ///   Gets the title for the element where the error occurred.
    /// </summary>
    public string ElementTitle { get; } = string.Empty;
}