﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Encoding.cs" company="Solidsoft Reply Ltd">
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
// The encodings used for data content in barcode records.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

/// <summary>
///   The encodings used for data content in barcode records.
/// </summary>
public enum Encoding {
    /// <summary>
    ///   The barcode record encoding is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    ///   The barcode record is encoded using GS1 Application Identifiers
    /// </summary>
    Gs1,

    /// <summary>
    ///   The barcode record is encoded using ASC MH10.8.2 Data Identifiers
    /// </summary>
    AscMh10,
}