// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRecord.cs" company="Solidsoft Reply Ltd.">
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
// Represents a data record in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

using System.Collections.Generic;

/// <summary>
///   Represents a data record in a barcode.
/// </summary>
public interface IRecord
{
    /// <summary>
    ///   Gets the data elements of the record.
    /// </summary>
    IEnumerable<IDataElement> Elements { get; }

    /// <summary>
    ///   Gets the data element encoding (GS1 or ASC MH10.8.2)
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    Encoding Encoding { get; }
}