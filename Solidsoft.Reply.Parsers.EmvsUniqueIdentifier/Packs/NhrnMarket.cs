// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NhrnMarket.cs" company="Solidsoft Reply Ltd">
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
// Markets with NHRNs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable UnusedMember.Global
namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Packs;

/// <summary>
///   Markets with NHRNs.
/// </summary>
public enum NhrnMarket
{
    /// <summary>
    ///   Unknown market.
    /// </summary>
    Unknown,

    /// <summary>
    ///   German market.
    /// </summary>
    Germany,

    /// <summary>
    ///   French market.
    /// </summary>
    France,

    /// <summary>
    ///   Spanish market.
    /// </summary>
    Spain,

    /// <summary>
    ///   Brazilian market.
    /// </summary>
    Brazil,

    /// <summary>
    ///   Portuguese market.
    /// </summary>
    Portugal,

    /// <summary>
    ///   United States of America market.
    /// </summary>
    UnitedStates,
}