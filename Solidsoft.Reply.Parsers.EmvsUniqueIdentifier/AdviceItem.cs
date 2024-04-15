// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdviceItem.cs" company="Solidsoft Reply Ltd">
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
// Represents an individual item of advice for a given condition.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BarcodeScanner.Calibration;

/// <summary>
///   Represents an individual item of advice for a given condition.
/// </summary>
/// <param name="AdviceType">The type of advice.</param>
/// <param name="Condition">The condition for which advice is provided.</param>
/// <param name="Description">Supplemental description of the condition.</param>
/// <param name="Advice">The advice for the condition.</param>
/// <param name="Severity">The severity of the condition.</param>

[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "<Pending>")]
public sealed record AdviceItem(
    [property: JsonProperty("adviceType", Order = 0)] AdviceType AdviceType,
    [property: JsonProperty("condition", Order = 1)] string Condition,
    [property: JsonProperty("description", Order = 2)] string Description,
    [property: JsonProperty("advice", Order = 3)] IList<string> Advice,
    [property: JsonProperty("severity", Order = 4)] ConditionSeverity Severity)
    : IAdviceItem<AdviceType> {
    /// <summary>
    ///   Initializes a new instance of the <see cref="AdviceItem"/> class;.
    /// </summary>
    /// <param name="adviceType">The type of advice.</param>
    /// <param name="substitutions">Substituted text items for formatted strings.</param>
    public AdviceItem(AdviceType adviceType, params object[] substitutions)
        : this(
            adviceType,
#pragma warning disable SA1118 // Parameter should not span multiple lines
            adviceType switch {
                AdviceType.NoSupportForCase => string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Advice.ResourceManager.GetString(
                        $"Condition_{(int)adviceType}",
                        Thread.CurrentThread.CurrentUICulture) ?? string.Empty,
                    substitutions),
                _ => Properties.Advice.ResourceManager.GetString(
                    $"Condition_{(int)adviceType}",
                    Thread.CurrentThread.CurrentUICulture) ?? string.Empty
            },
#pragma warning restore SA1118 // Parameter should not span multiple lines

#pragma warning disable SA1118 // Parameter should not span multiple lines
            adviceType switch {
                AdviceType.NoSupportForCase => string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Advice.ResourceManager.GetString(
                        $"Description_{(int)adviceType}",
                        Thread.CurrentThread.CurrentUICulture) ?? string.Empty,
                    substitutions),
                _ => Properties.Advice.ResourceManager.GetString(
                    $"Description_{(int)adviceType}",
                    Thread.CurrentThread.CurrentUICulture) ?? string.Empty
            },
#pragma warning restore SA1118 // Parameter should not span multiple lines
#pragma warning disable SA1118 // Parameter should not span multiple lines
            Properties.Advice.ResourceManager.GetString(
                    $"Advice_{(int)adviceType}",
                    Thread.CurrentThread.CurrentUICulture)
                ?.Split(";;", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToList() ?? new List<string>(),
#pragma warning restore SA1118 // Parameter should not span multiple lines
#pragma warning disable SA1118 // Parameter should not span multiple lines
            (int)adviceType switch {
                < 200 and >= 100 => ConditionSeverity.Low,
                < 300 and >= 200 => ConditionSeverity.Medium,
                >= 300 => ConditionSeverity.High,
                _ => 0
            }) {
    }
#pragma warning restore SA1118 // Parameter should not span multiple lines

    /// <summary>
    ///   Creates an advice item from a JSON string representing the serialized data.
    /// </summary>
    /// <param name="json">A JSON string representing the serialized data.</param>
    /// <returns>An advice item.</returns>
    // ReSharper disable once UnusedMember.Global
    public static AdviceItem? FromJson(string json) {
        var adviceItem = JsonConvert.DeserializeObject<AdviceItem>(json);

        return string.IsNullOrWhiteSpace(json) || adviceItem is null
                   ? null
                   : new AdviceItem(
                       adviceItem.AdviceType,
                       adviceItem.Condition,
                       adviceItem.Description,
                       adviceItem.Advice,
                       adviceItem.Severity);
    }

    /// <summary>
    ///   Tests the equality of this advice item with another.
    /// </summary>
    /// <param name="other">The advice item to be tested.</param>
    /// <returns>True, if the advice items are not equal; otherwise false.</returns>
    public bool Equals(AdviceItem? other) =>
        other is not null &&
        (ReferenceEquals(this, other) || AdviceType == other.AdviceType &&
         string.Equals(Condition, other.Condition, StringComparison.Ordinal) &&
         string.Equals(Description, other.Description, StringComparison.Ordinal) &&
         Advice.SequenceEqual(other.Advice) &&
         Severity == other.Severity);

    /// <summary>
    ///   Returns a hash value for the current advice item.
    /// </summary>
    /// <returns>The hash value.</returns>
    // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() =>
        Fnv.CreateHashFnv1A(
            AdviceType,
            Condition,
            Description,
            Advice,
            Severity);

    /// <summary>
    ///   Returns a JSON representation of the advice item.
    /// </summary>
    /// <returns>A JSON representation of the advice item.</returns>
    public override string ToString() =>
        ToJson();

    /// <summary>
    ///   Returns a JSON representation of the advice item.
    /// </summary>
    /// <param name="formatting">Specifies the formatting to be applied to the JSON.</param>
    /// <returns>A JSON representation of the calibration data.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public string ToJson(Formatting formatting = Formatting.None) =>
        JsonConvert.SerializeObject(
            this,
            formatting,
            new JsonSerializerSettings {
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ConstructorHandling = ConstructorHandling.Default,
                ContractResolver = new DataIgnoreEmptyEnumerableResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            });
}