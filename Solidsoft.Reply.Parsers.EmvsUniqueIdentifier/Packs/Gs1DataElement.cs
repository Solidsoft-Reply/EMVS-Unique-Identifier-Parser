// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gs1DataElement.cs" company="Solidsoft Reply Ltd">
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
// A data element in a GS1-encoded barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using Gs1Ai;

/// <summary>
///   A data element in a GS1-encoded barcode.
/// </summary>
public class Gs1DataElement : IDataElement {
    /// <summary>
    ///   Initializes a new instance of the <see cref="Gs1DataElement" /> class.
    /// </summary>
    /// <param name="applicationIdentifier">The GS1 application identifier.</param>
    /// <param name="identifier">The GS1 application identifier text.</param>
    /// <param name="data">The element data.</param>
    /// <param name="title">The element title.</param>
    /// <param name="description">The element description.</param>
    /// <param name="position">The character position of the start of the element.</param>
    public Gs1DataElement(
        ApplicationIdentifier applicationIdentifier,
        string identifier,
        string data,
        string title,
        string description,
        int position) {
        ApplicationIdentifier = applicationIdentifier;
        Identifier = identifier;
        Data = data;
        Title = title;
        Description = description;
        Position = position;
    }

    /// <summary>
    ///   Gets the GS1 Application Identifier.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public ApplicationIdentifier ApplicationIdentifier { get; }

    /// <summary>
    ///   Gets the element data.
    /// </summary>
    public string Data { get; }

    /// <summary>
    ///   Gets the description of the data element.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///   Gets the GS1 application identifier text.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    ///   Gets the character position of the data element within the barcode data.
    /// </summary>
    public int Position { get; }

    /// <summary>
    ///   Gets the title of the data element.
    /// </summary>
    public string Title { get; }
}