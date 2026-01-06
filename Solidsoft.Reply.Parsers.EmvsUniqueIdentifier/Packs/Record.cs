// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Record.cs" company="Solidsoft Reply Ltd">
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
// A data record in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System;
using System.Collections.Generic;

using HighCapacityAidc;
using HighCapacityAidc.Syntax;

using AnsiMhDi;
using Gs1Ai;

/// <summary>
///   A data record in a barcode.
/// </summary>
public class Record : IRecord {
    /// <summary>
    ///   Initializes a new instance of the <see cref="Record" /> class.
    /// </summary>
    /// <param name="format">
    ///   The ISO/IEC 1434 format indicator.
    /// </param>
    /// <param name="elements">
    ///   The elements in the record.
    /// </param>
    public Record(FormatIndicator format, IEnumerable<IDataEntity>? elements) {
        Encoding = format switch {
            FormatIndicator.Gs1Ai => Encoding.Gs1,
            FormatIndicator.AscMh10Di => Encoding.AscMh10,
            _ => Encoding.Unknown
        };

        var packElements = new List<IDataElement>();

        foreach (var element in elements ?? new List<IDataEntity>()) {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (format) {
                case FormatIndicator.Gs1Ai:
                    if (Enum.TryParse(
                            ((DataElement)element).Identifier,
                            out ApplicationIdentifier gs1Identifier)) {
                        packElements.Add(
                            new Gs1DataElement(
                                gs1Identifier,
                                ((DataElement)element).Identifier,
                                element.Data,
                                element.Title,
                                element.Description,
                                element.Position));
                    }

                    break;
                case FormatIndicator.AscMh10Di:
                    packElements.Add(
                        new IfaDataElement(
                            (DataIdentifier)element.Data.AsSpan().Resolve(
                                ((DataElement)element).Identifier.AsSpan(),
                                0).Entity,
                            ((DataElement)element).Identifier,
                            element.Data,
                            element.Title,
                            element.Description,
                            element.Position));
                    break;
                case FormatIndicator.Transportation:
                case FormatIndicator.Edi:
                case FormatIndicator.AscX12:
                case FormatIndicator.UnEdifact:
                case FormatIndicator.Text:
                case FormatIndicator.Cii:
                case FormatIndicator.Binary:
                case FormatIndicator.StructuredData:
                    packElements.Add(
                        new IsoIec15434DataElement(
                            element.Data,
                            element.Title,
                            element.Description,
                            element.Position));
                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        Elements = packElements;
    }

    /// <summary>
    ///   Gets the data elements of the record.
    /// </summary>
    public IEnumerable<IDataElement> Elements { get; }

    /// <summary>
    ///   Gets the data element encoding (GS1 or ASC MH10.8.2).
    /// </summary>
    public Encoding Encoding { get; }
}