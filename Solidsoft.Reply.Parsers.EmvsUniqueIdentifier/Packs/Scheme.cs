// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scheme.cs" company="Solidsoft Reply Ltd.">
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
// Barcode encoding schema.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

/// <summary>
///   Barcode encoding schema.
/// </summary>
public enum Scheme
{
    /// <summary>
    ///   The scheme is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    ///   GS1 encoding.
    /// </summary>
    Gs1,

    /// <summary>
    ///   IFA encoding for EMVS.
    /// </summary>
    Ifa
}