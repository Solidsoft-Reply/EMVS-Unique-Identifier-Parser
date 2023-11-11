// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbologyValidity.cs" company="Solidsoft Reply Ltd.">
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
// The legal validity of the use of the symbology to encode a unique pack identifier.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

/// <summary>
///   The legal validity of the use of the symbology to encode a unique pack identifier.
/// </summary>
public enum SymbologyValidity
{
    /// <summary>
    ///   The legal validity of the use of the symbology to encode a unique pack identifier is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    ///   The use of the symbology to encode a unique pack identifier is legally valid.
    /// </summary>
    True,

    /// <summary>
    ///   The use of the symbology to encode a unique pack identifier is legally invalid.
    /// </summary>
    False
}