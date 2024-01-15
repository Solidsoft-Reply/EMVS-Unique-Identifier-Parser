// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackIdentifierException.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd. All rights reserved.
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
// A parse exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System;
using System.Runtime.Serialization;

/// <summary>
///   A parse exception.
/// </summary>
[Serializable]
public class PackIdentifierException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierException" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public PackIdentifierException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public PackIdentifierException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public PackIdentifierException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierException" /> class.
    /// </summary>
    /// <param name="errorNumber">
    ///   The error number.
    /// </param>
    /// <param name="message">
    ///   The message.
    /// </param>
    /// <param name="innerException">
    ///   The inner Exception.
    /// </param>
    public PackIdentifierException(int errorNumber, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        ErrorNumber = errorNumber;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackIdentifierException" /> class.
    /// </summary>
    /// <param name="info">The serialization information.</param>
    /// <param name="context">The streaming context.</param>
    [Obsolete("Formatter serialisation has been deprecated in .NET.", DiagnosticId = "SYSLIB0051")]
    protected PackIdentifierException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the error number.
    /// </summary>
    public int ErrorNumber { get; }
}