// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Territory.cs" company="Solidsoft Reply Ltd">
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
// Territory for which advice will be provided.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

/// <summary>
///   Territory for which advice will be provided.
/// </summary>
public enum Territory
{
    /// <summary>
    ///   Any market that has joined the EMVS, unless an alternative specific territory has been defined for the market.
    /// </summary>
    Europe,

    /// <summary>
    ///   The German market.
    /// </summary>
    /// <remarks>
    ///   The German market accepts packs encoded using either of two schemes - GS1 (FNC1, only - Format 05 is not allowed)
    ///   and PPN (Format 06 - the IFA specification is based on the ANSI ASC 10.8.2 materials handling standard, although it
    ///   no longer fully complies with that standard - specifically with regard to the character set and letter casing
    ///   allows in serial numbers and batch identifiers).
    /// </remarks>
    Germany,
}