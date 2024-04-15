// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataElement.cs" company="Solidsoft Reply Ltd">
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
// Represents a data element in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

/// <summary>
///   Represents a data element in a barcode.
/// </summary>
public interface IDataElement
{
    /// <summary>
    ///   Gets the element data.
    /// </summary>
    string Data { get; }

    /// <summary>
    ///   Gets the description of the data element.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    string Description { get; }

    /// <summary>
    ///   Gets the element identifier.
    /// </summary>
    string Identifier { get; }

    /// <summary>
    ///   Gets the character position of the data element within the barcode data.
    /// </summary>
    int Position { get; }

    /// <summary>
    ///   Gets the title of the data element.
    /// </summary>
    string Title { get; }
}