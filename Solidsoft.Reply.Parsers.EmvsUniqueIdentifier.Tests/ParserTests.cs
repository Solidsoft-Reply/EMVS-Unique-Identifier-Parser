// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gs1ParserTests.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <summary>
// Tests for GS1 Parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests;

using System.Collections.Generic;
using System.Linq;
using Gs1Ai;

using Common;

using Xunit;

/// <summary>
/// Tests for GS1 Parser.
/// </summary>
public class ParserTests {
    /// <summary>
    /// GS1 Character Set 82 - test 01.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8201() {
        var gs1TestData = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "DdVcX<t" },
                                  { "17", "230207" },
                                  { "21", "yCH*4'h1Ab" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 02.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8202() {
        // ReSharper disable once StringLiteralTypo
        var gs1TestData = $"010477298543159410.GRs!qO{(char)29}1723072921Tglv?,6BmK";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", ".GRs!qO" },
                                  { "17", "230729" },
                                  // ReSharper disable once StringLiteralTypo
                                  { "21", "Tglv?,6BmK" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 03.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8203() {
        var gs1TestData = $"010477298543159410(fpNxYi{(char)29}1723040521Z&Ur7>0oQS";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "(fpNxYi" },
                                  { "17", "230405" },
                                  { "21", "Z&Ur7>0oQS" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 04.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8204() {
        var gs1TestData = $"010477298543159410=8Fn_P\"{(char)29}1723100221Ew5;2/zaMJ";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "=8Fn_P\"" },
                                  { "17", "231002" },
                                  { "21", "Ew5;2/zaMJ" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 05.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8205() {
        var gs1TestData = $"0104772985431594103kuW)L9{(char)29}17230304215j4CltEc-Y";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "3kuW)L9" },
                                  { "17", "230304" },
                                  { "21", "5j4CltEc-Y" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 06.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8206() {
        var gs1TestData = $"010477298543159410:j\"%e+P{(char)29}172310312151itJzCguA";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", ":j\"%e+P" },
                                  { "17", "231031" },
                                  { "21", "51itJzCguA" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 07.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8207() {
        var gs1TestData = $"010477298543159410bA1h'4*{(char)29}1723121421t<XcVdD0q!";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "bA1h'4*" },
                                  { "17", "231214" },
                                  { "21", "t<XcVdD0q!" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 08.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8208() {
        var gs1TestData = $"010477298543159410HCyKmB6{(char)29}1724062021sRG.iYxNpf";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "HCyKmB6" },
                                  { "17", "240620" },
                                  { "21", "sRG.iYxNpf" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 09.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8209() {
        var gs1TestData = $"010477298543159410,?vigTS{(char)29}1724021821(\"P-nF8=9L";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", ",?vigTS" },
                                  { "17", "240218" },
                                  { "21", "(\"P-nF8=9L" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 10.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8210() {
        var gs1TestData = $"010477298543159410Qo0>7rU{(char)29}1724070321)Wuk3P+e%\"";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "Qo0>7rU" },
                                  { "17", "240703" },
                                  { "21", ")Wuk3P+e%\"" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 11.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8211() {
        var gs1TestData = $"010477298543159410&ZjMaz/{(char)29}1723112621j:AugCzJt";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "&ZjMaz/" },
                                  { "17", "231126" },
                                  { "21", "j:AugCzJt" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Character Set 82 - test 12.
    /// </summary>
    [Fact]
    public void Gs1CharacterSet8212() {
        var gs1TestData = $"0104772985431594102;5wEY-{(char)29}1723040421HIrQ9QeTyQ";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "2;5wEY-" },
                                  { "17", "230404" },
                                  { "21", "HIrQ9QeTyQ" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GS1 Disallowed characters - test 01.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters01() {
        var gs1TestData = $"010477298543159410AB#0123{(char)29}1723020721622~409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB#0123" },
                                  { "17", "230207" },
                                  { "21", "622~409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 02.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters02() {
        var gs1TestData = $"010477298543159410AB$0123{(char)29}1723072921622|409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB$0123" },
                                  { "17", "230729" },
                                  { "21", "622|409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 03.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters03() {
        var gs1TestData = $"010477298543159410AB@0123{(char)29}1723040521{{622409215}}";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB@0123" },
                                  { "17", "230405" },
                                  { "21", "{622409215}" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 04.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters04() {
        var gs1TestData = $"010477298543159410[AB0123]{(char)29}1723100221`622409215`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "[AB0123]" },
                                  { "17", "231002" },
                                  { "21", "`622409215`" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 05.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters05() {
        var gs1TestData = $"010477298543159410AB\\0123{(char)29}1723030421^622409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB\\0123" },
                                  { "17", "230304" },
                                  { "21", "^622409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 06.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters06() {
        var gs1TestData = $"010477298543159410AB^0123{(char)29}1723103121622\\409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB^0123" },
                                  { "17", "231031" },
                                  { "21", "622\\409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 07.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters07() {
        var gs1TestData = $"010477298543159410`AB`0123{(char)29}1723121421[622409215]";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "`AB`0123" },
                                  { "17", "231214" },
                                  { "21", "[622409215]" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 08.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters08() {
        var gs1TestData = $"010477298543159410{{AB0123}}{(char)29}1724062021@622409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "{AB0123}" },
                                  { "17", "240620" },
                                  { "21", "@622409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 09.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters09() {
        var gs1TestData = $"010477298543159410AB|0123{(char)29}1724021821$622409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB|0123" },
                                  { "17", "240218" },
                                  { "21", "$622409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// GS1 Disallowed characters - test 10.
    /// </summary>
    [Fact]
    public void Gs1DisallowedCharacters10() {
        var gs1TestData = $"010477298543159410AB~0123{(char)29}1724070321#622409215";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04772985431594" },
                                  { "10", "AB~0123" },
                                  { "17", "240703" },
                                  { "21", "#622409215" }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// No value - test.
    /// </summary>
    [Fact]
    public void Gs1NoValue() {
        const string gs1TestData = "01";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", string.Empty }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        Assert.Contains(results, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2004));
    }

    /// <summary>
    /// Whitespace value - test.
    /// </summary>
    [Fact]
    public void Gs1InvalidForAiValue() {
        var gs1TestData = "10    ";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "10", "    " }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        var resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2006));

        gs1TestData = "17459272";

        expectedResults = new Dictionary<string, string>
        {
            { "17", "459272" }
        };

        results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        resolvedEntities = results ?? [.. results];
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2005));
        Assert.Contains(resolvedEntities, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2100));
    }

    /// <summary>
    /// Insufficient fixed width value value (2) - test.
    /// </summary>
    [Fact]
    public void Gs1WInsufficientFixedWidthValue() {
        const string gs1TestData = "010123";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "    " }
                              };

        var results = DoTestGs1WithErrors(gs1TestData, expectedResults);
        Assert.Contains(results, r => r.IsError && r.Exceptions.Any(e => e.ErrorNumber == 2004));
    }

    /// <summary>
    /// Null value - test.
    /// </summary>
    [Fact]
    public void Gs1NullValue() {
        var expectedResults = new Dictionary<string, string>();

        DoTestGs1WithErrors(null, expectedResults);
    }

    /// <summary>
    /// Serial Shipping Container Code - test.
    /// </summary>
    [Fact]
    public void Gs1SerialShippingContainerCode() {
        const string gs1TestData = "00007189085627231896";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "00", "007189085627231896" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Serial Shipping Container Code - test.
    /// </summary>
    [Fact]
    public void Gs1SerialShippingContainerCodeWithErrors() {
        const string gs1TestData = "00A07189085627231896";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "00", "A07189085627231896" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Global Trade Item Number - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalTradeItemNumber() {
        const string gs1TestData = "0104070071967072";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "04070071967072" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Global Trade Item Number - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalTradeItemNumberWithErrors() {
        const string gs1TestData = "01A4070071967072";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "01", "A4070071967072" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Global Trade Item Number - test.
    /// </summary>
    [Fact]
    public void Gs1GtinOfContainedTradeItems() {
        const string gs1TestData = "0210634158608732";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "02", "10634158608732" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Global Trade Item Number - test.
    /// </summary>
    [Fact]
    public void Gs1GtinOfContainedTradeItemsWithErrors() {
        const string gs1TestData = "02A0634158608732";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "02", "A0634158608732" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Batch or Lot Number - test.
    /// </summary>
    [Fact]
    public void Gs1BatchLotNumber() {
        const string gs1TestData = "104512XA";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "10", "4512XA" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Batch or Lot Number - test.
    /// </summary>
    [Fact]
    public void Gs1BatchLotNumberWithErrors() {
        const string gs1TestData = "104512~XA";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "10", "4512~XA" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Production date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1ProductionDateYymmdd() {
        const string gs1TestData = "11190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "11", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Production date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1ProductionDateYymmddWithErrors() {
        const string gs1TestData = "11192712";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "11", "192712" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Due date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1DueDateYymmdd() {
        const string gs1TestData = "12190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "12", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Due date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1DueDateYymmddWithErrors() {
        const string gs1TestData = "1219AP12";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "12", "19AP12" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Packaging date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1PackagingDateYymmdd() {
        const string gs1TestData = "13190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "13", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Packaging date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1PackagingDateYymmddWithErrors() {
        const string gs1TestData = "13190440";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "13", "190440" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Best before date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1BetBeforeDateYymmdd() {
        const string gs1TestData = "15190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "15", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Best before date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1BetBeforeDateYymmddWithErrors() {
        const string gs1TestData = "151904TU";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "15", "1904TU" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Sell By date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1SellByDateYymmdd() {
        const string gs1TestData = "16190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "16", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Sell By date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1SellByDateYymmddWithErrors() {
        const string gs1TestData = "161900412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "16", "1900412" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Use By or Expiry date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1ExpiryDateYymmdd() {
        const string gs1TestData = "17190412";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "17", "190412" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Use By or Expiry date (YYMMDD) - test.
    /// </summary>
    [Fact]
    // ReSharper disable once IdentifierTypo
    public void Gs1ExpiryDateYymmddWithErrors() {
        const string gs1TestData = "1719\\04\\12";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "17", "19\\04\\12" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Internal product variant - test.
    /// </summary>
    [Fact]
    public void Gs1InternalProductVariant() {
        const string gs1TestData = "2001";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "20", "01" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Internal product variant - test.
    /// </summary>
    [Fact]
    public void Gs1InternalProductVariantWithErrors() {
        const string gs1TestData = "20001";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "20", "001" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Serial number - test.
    /// </summary>
    [Fact]
    public void Gs1SerialNumber() {
        const string gs1TestData = "2112R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "21", "12R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Serial number - test.
    /// </summary>
    [Fact]
    public void Gs1SerialNumberWithErrors() {
        const string gs1TestData = "2112R213[4IU7]";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "21", "12R213[4IU7]" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Consumer Product Variant - test.
    /// </summary>
    [Fact]
    public void Gs1ConsumerProductVariant() {
        const string gs1TestData = "2212R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "22", "12R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Consumer Product Variant - test.
    /// </summary>
    [Fact]
    public void Gs1ConsumerProductVariantWithErrors() {
        const string gs1TestData = "2212R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "22", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Third Party Controlled, Serialised Extension of GTIN (TPX) - test.
    /// </summary>
    [Fact]
    public void Gs1ThirdPartyControlledSerialisedExtensionOfGtinTpx() {
        const string gs1TestData = "2352R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "235", "2R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Third Party Controlled, Serialised Extension of GTIN (TPX) - test.
    /// </summary>
    [Fact]
    public void Gs1ThirdPartyControlledSerialisedExtensionOfGtinTpxWithErrors() {
        const string gs1TestData = "2352R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "235", "2R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Additional Item Identification - test.
    /// </summary>
    [Fact]
    public void Gs1AdditionalItemIdentification() {
        const string gs1TestData = "240122R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "240", "122R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Additional Item Identification - test.
    /// </summary>
    [Fact]
    public void Gs1AdditionalItemIdentificationWithErrors() {
        const string gs1TestData = "24012R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "240", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Customer Part Number - test.
    /// </summary>
    [Fact]
    public void Gs1CustomerPartNumber() {
        const string gs1TestData = "241122R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "241", "122R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Customer Part Number - test.
    /// </summary>
    [Fact]
    public void Gs1CustomerPartNumberWithErrors() {
        const string gs1TestData = "24112R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "241", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Made to Order Variation Number - test.
    /// </summary>
    [Fact]
    public void Gs1MadeToOrderVariationNumber() {
        const string gs1TestData = "242190213";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "242", "190213" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Made to Order Variation Number - test.
    /// </summary>
    [Fact]
    public void Gs1MadeToOrderVariationNumberWithErrors() {
        const string gs1TestData = "242A90213";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "242", "A90213" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Packaging Component Number - test.
    /// </summary>
    [Fact]
    public void Gs1PackagingComponentNumber() {
        const string gs1TestData = "243122R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "243", "122R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Packaging Component Number - test.
    /// </summary>
    [Fact]
    public void Gs1PackagingComponentNumberWithErrors() {
        const string gs1TestData = "24312R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "243", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Secondary Serial Number - test.
    /// </summary>
    [Fact]
    public void Gs1SecondarySerialNumber() {
        const string gs1TestData = "250122R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "250", "122R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Secondary Serial Number - test.
    /// </summary>
    [Fact]
    public void Gs1SecondarySerialNumberWithErrors() {
        const string gs1TestData = "25012R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "250", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Reference to Source Entity - test.
    /// </summary>
    [Fact]
    public void Gs1ReferenceToSourceEntity() {
        const string gs1TestData = "251122R2134IU7";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "251", "122R2134IU7" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Reference to Source Entity - test.
    /// </summary>
    [Fact]
    public void Gs1ReferenceToSourceEntityWithErrors() {
        const string gs1TestData = "25112R213`4IU7`";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "251", "12R213`4IU7`" }
                              };

        DoTestGs1WithErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Global Document Type Identifier - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalDocumentTypeIdentifier() {
        const string gs1TestData1 = "2530123456789012";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "253", "0123456789012" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "25301234567890124FG79034+T";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "253", "01234567890124FG79034+T" }
                               };

        DoTestGs1WithNoErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// Global Document Type Identifier - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalDocumentTypeIdentifierWithErrors() {
        const string gs1TestData1 = "253DUMMYDATA1234";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   // ReSharper disable once StringLiteralTypo
                                   { "253", "DUMMYDATA1234" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "2530123456789012AB123456CDF~6";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "253", "0123456789012AB123456CDF~6" }
                               };

        DoTestGs1WithErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// GLN Extension Component - test.
    /// </summary>
    [Fact]
    public void Gs1GlnExtensionComponent() {
        const string gs1TestData = "254AB123456CDF+6";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "254", "AB123456CDF+6" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// GLN Extension Component - test.
    /// </summary>
    [Fact]
    public void Gs1GlnExtensionComponentWithErrors() {
        const string gs1TestData1 = "254AB123456CDF~6";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "254", "AB123456CDF~6" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "254AB123456CDF695GIW7003";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "254", "AB123456CDF695GIW7003" }
                               };

        DoTestGs1WithErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// Global Coupon Number (GCN) - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalCouponNumber() {
        const string gs1TestData1 = "2550123456789012";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "255", "0123456789012" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "25501234567890120178463";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "255", "01234567890120178463" }
                               };

        DoTestGs1WithNoErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// Global Coupon Number (GCN) - test.
    /// </summary>
    [Fact]
    public void Gs1GlobalCouponNumberWithErrors() {
        const string gs1TestData1 = "255DUMMYDATA1234";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   // ReSharper disable once StringLiteralTypo
                                   { "255", "DUMMYDATA1234" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "2550123456789012AB123456CDF~6";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "255", "0123456789012AB123456CDF~6" }
                               };

        DoTestGs1WithErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// Variable count of items (variable measure trade item) (VAR. COUNT) - test.
    /// </summary>
    [Fact]
    public void Gs1VariableCountOfItemsVariableMeasureTradeItem() {
        const string gs1TestData1 = "3001234567";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "30", "01234567" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Variable count of items (variable measure trade item) (VAR. COUNT) - test.
    /// </summary>
    [Fact]
    public void Gs1VariableCountOfItemsVariableMeasureTradeItemWithErrors() {
        // ReSharper disable once StringLiteralTypo
        const string gs1TestData1 = "30DUMMYDAT";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   // ReSharper disable once StringLiteralTypo
                                   { "30", "DUMMYDAT" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        const string gs1TestData2 = "3001234567890";

        var expectedResults2 = new Dictionary<string, string>
                               {
                                   { "30", "01234567890" }
                               };

        DoTestGs1WithErrors(gs1TestData2, expectedResults2);
    }

    /// <summary>
    /// Trade Measures - test.
    /// </summary>
    [Fact]
    public void Gs1TradeMeasures() {
        for (var idx = 310; idx <= 316; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 320; idx <= 329; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 350; idx <= 352; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 356; idx <= 357; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 360; idx <= 361; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 364; idx <= 366; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }
    }

    /// <summary>
    /// Trade Measures - test.
    /// </summary>
    [Fact]
    public void Gs1TradeMeasuresWithErrors() {
        for (var idx = 310; idx <= 316; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 320; idx <= 329; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 350; idx <= 352; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 356; idx <= 357; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 360; idx <= 361; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 364; idx <= 366; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }
    }

    /// <summary>
    /// Logistic Measures - test.
    /// </summary>
    [Fact]
    public void Gs1LogisticMeasures() {
        for (var idx = 330; idx <= 336; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 340; idx <= 349; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 353; idx <= 355; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 362; idx <= 363; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }

        for (var idx = 367; idx <= 369; idx++) {
            Gs1Vmti6DecimalPlaces(idx.ToString());
        }
    }

    /// <summary>
    /// Logistic Measures - test.
    /// </summary>
    [Fact]
    public void Gs1LogisticWithErrors() {
        for (var idx = 330; idx <= 336; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 340; idx <= 349; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 353; idx <= 355; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 362; idx <= 363; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }

        for (var idx = 367; idx <= 369; idx++) {
            Gs1Vmti6DecimalPlacesWithErrors(idx.ToString());
        }
    }

    /// <summary>
    /// Kilograms per Square Meter - test.
    /// </summary>
    [Fact]
    public void Gs1KilogramsPerSquareMeter() {
        Gs1Vmti6DecimalPlaces("337");
    }

    /// <summary>
    /// Kilograms per Square Meter - test.
    /// </summary>
    [Fact]
    public void Gs1KilogramsPerSquareMeterWithErrors() {
        Gs1Vmti6DecimalPlacesWithErrors("337");
    }

    /// <summary>
    /// Count of Trade Item Pieces contained in Logistics Unit - test.
    /// </summary>
    [Fact]
    public void Gs1CountOfTradeItemPiecesContainedInLogisticsUnit() {
        var gs1TestData1 = "3712345678";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "37", "12345678" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "371";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "37", "1" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Count of Trade Item Pieces contained in Logistics Unit - test.
    /// </summary>
    [Fact]
    public void Gs1CountOfTradeItemPiecesContainedInLogisticsUnitWithErrors() {
        // ReSharper disable once StringLiteralTypo
        const string gs1TestData1 = "37DUMMYDAT";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   // ReSharper disable once StringLiteralTypo
                                   { "37", "DUMMYDAT" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable or Coupon Value - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableOrCouponValue() {
        Gs1Vmti6DecimalPlaces("390");

        var gs1TestData1 = "3902123456";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "3902", "123456" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "39001";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3900", "1" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "3901123456789012345";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3901", "123456789012345" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable or Coupon Value - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableOrCouponValueWithErrors() {
        const string gs1TestData1 = "3902€12345";

        var expectedResults1 = new Dictionary<string, string>
                           {
                               { "390", "€12345" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable and ISO Currency Code - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableAndIsoCurrencyCode() {
        Gs1Vmti6DecimalPlaces("391");

        var gs1TestData1 = "3912978123456";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "3912", "978123456" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "39109781";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3910", "9781" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "3911978123456789012345";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3911", "978123456789012345" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable and ISO Currency Code - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableAndIsoCurrencyCodeWithErrors() {
        var gs1TestData1 = "3912";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "391", string.Empty }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "391297";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "391", "97" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "3912978€12345";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "391", "978€12345" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable for a Variable Measure Trade Item – Single Monetary Area - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableForVariableMeasureAreaSingleMonetaryArea() {
        Gs1Vmti6DecimalPlaces("392");

        var gs1TestData1 = "3922123456";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { "3922", "123456" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "39201";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3920", "1" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = "3921123456789012345";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { "3921", "123456789012345" }
                           };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Amount Payable for a Variable Measure Trade Item – Single Monetary Area - test.
    /// </summary>
    [Fact]
    public void Gs1AmountPayableForVariableMeasureAreaSingleMonetaryAreaWithErrors() {
        const string gs1TestData1 = "3922€12345";

        var expectedResults1 = new Dictionary<string, string>
                           {
                               { "392", "€12345" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - Germany (PZN).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberGermanyPzn() {
        const string gs1TestData = "71003752864";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "710", "03752864" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - France (CIP).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberFranceCip() {
        const string gs1TestData = "7113400930000120";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "711", "3400930000120" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - Spain (CN).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberSpainCn() {
        const string gs1TestData = "712713479.4";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "712", "713479.4" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - Brazil (DRN).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberBrazilDrn() {
        const string gs1TestData = "7134005632804976";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "713", "4005632804976" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - Portugal (AIM).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberPortugalAim() {
        const string gs1TestData = "7141234567";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "714", "1234567" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - United States of America (NDC).
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberUnitedStatesAim() {
        const string gs1TestData = "7150777310502";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "715", "0777310502" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// National Healthcare Reimbursement Number (NHRN) - Multi-Market.
    /// </summary>
    [Fact]
    public void Gs1NationalHealthcareReimbursementNumberMulti() {
        var gs1TestData = $"71003752864{(char)29}7113400930000120{(char)29}712713479.4{(char)29}7134005632804976{(char)29}7141234567{(char)29}7150777310502";

        var expectedResults = new Dictionary<string, string>
                              {
                                  { "710", "03752864" },
                                  { "711", "3400930000120" },
                                  { "712", "713479.4" },
                                  { "713", "4005632804976" },
                                  { "714", "1234567" },
                                  { "715", "0777310502" }
                              };

        DoTestGs1WithNoErrors(gs1TestData, expectedResults);
    }

    /// <summary>
    /// Variable measure trade item with 6 decimal places - test.
    /// </summary>
    /// <param name="identifier">An application identifier.</param>
    // ReSharper disable once IdentifierTypo
    private static void Gs1Vmti6DecimalPlaces(string identifier) {
        var gs1TestData1 = identifier + "2124456";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { identifier + "2", "124456" }
                               };

        DoTestGs1WithNoErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Variable measure trade item with 6 decimal places - test.
    /// </summary>
    /// <param name="identifier">An application identifier.</param>
    // ReSharper disable once IdentifierTypo
    private static void Gs1Vmti6DecimalPlacesWithErrors(string identifier) {
        var gs1TestData1 = identifier + "31234";

        var expectedResults1 = new Dictionary<string, string>
                               {
                                   { identifier, "1234" }
                               };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = identifier + "A123456";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { identifier, "123456" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);

        gs1TestData1 = identifier + "2A23456";

        expectedResults1 = new Dictionary<string, string>
                           {
                               { identifier, "A23456" }
                           };

        DoTestGs1WithErrors(gs1TestData1, expectedResults1);
    }

    /// <summary>
    /// Test GS1 data and ensure there are no errors.
    /// </summary>
    /// <param name="gs1TestData">The GS1 data to be tested.</param>
    /// <param name="expectedResults">The expected results.</param>
    // ReSharper disable once UnusedMethodReturnValue.Local
    private static void DoTestGs1WithNoErrors(string gs1TestData, IReadOnlyDictionary<string, string> expectedResults) {
        var results = DoTestGs1(gs1TestData, expectedResults);
        var doTestGs1WithNoErrors = results ?? [.. results];

        foreach (var result in doTestGs1WithNoErrors) {
            Assert.False(result.IsError);
        }
    }

    /// <summary>
    /// Test GS1 data and ensure there are expected errors.
    /// </summary>
    /// <param name="gs1TestData">The GS1 data to be tested.</param>
    /// <param name="expectedResults">The expected results.</param>
    /// <returns>The results.</returns>
    private static IResolvedEntity[] DoTestGs1WithErrors(string gs1TestData, IReadOnlyDictionary<string, string> expectedResults) {
        var results = DoTestGs1(gs1TestData, expectedResults, true);

        var doTestGs1WithErrors = results ?? [.. results];
        var containsErrors = System.Array.Exists(doTestGs1WithErrors, result => result.IsError);

        Assert.True(containsErrors);

        return doTestGs1WithErrors;
    }

    /// <summary>
    /// Test GS1 data.
    /// </summary>
    /// <param name="gs1TestData">The GS1 data to be tested.</param>
    /// <param name="expectedResults">The expected results.</param>
    /// <param name="suppressErrors">If true, assertion errors are suppressed.</param>
    /// <returns>The results.</returns>
    private static IResolvedEntity[] DoTestGs1(string gs1TestData, IReadOnlyDictionary<string, string> expectedResults, bool suppressErrors = false) {
        var results = new List<IResolvedEntity>();
        Parser.Parse(gs1TestData, entity => results.Add(entity));

        foreach (var result in results) {
            try {
                Assert.True(expectedResults.ContainsKey(result.Identifier));
                Assert.Equal(expectedResults[result.Identifier], result.Value);
            }
            catch {
                if (!suppressErrors) {
                    throw;
                }
            }
        }

        return [.. results];
    }
}