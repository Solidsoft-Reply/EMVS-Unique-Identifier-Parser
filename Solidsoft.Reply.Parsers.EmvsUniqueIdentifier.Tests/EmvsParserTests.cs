// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmvsParserTests.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <summary>
// Tests for GS1 Parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests;

using System;
using System.Diagnostics.CodeAnalysis;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using EmvsUniqueIdentifier;
using BarcodeScanner.Calibration;
using BarcodeScanner.Calibration.DataMatrix;
using Packs;

using Xunit;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class EmvsParserTests {
    private const string BelgianFrenchBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BelgianFrenchDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianFrenchDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianFrenchCalibration = "{\"characterMap\":{\"1\":\"!\",\"%\":\"\\\"\",\"5\":\"%\",\"7\":\"&\",\"\\u00f9\":\"'\",\"9\":\"(\",\"0\":\")\",\"8\":\"*\",\"_\":\"+\",\";\":\",\",\")\":\"-\",\":\":\".\",\"=\":\"/\",\"\\u00e0\":\"0\",\"&\":\"1\",\"\\u00e9\":\"2\",\"\\\"\":\"3\",\"'\":\"4\",\"(\":\"5\",\"\\u00a7\":\"6\",\"\\u00e8\":\"7\",\"!\":\"8\",\"\\u00e7\":\"9\",\"M\":\":\",\"m\":\";\",\".\":\"<\",\"-\":\"=\",\"/\":\">\",\"+\":\"?\",\"Q\":\"A\",\"?\":\"M\",\"A\":\"Q\",\"Z\":\"W\",\"W\":\"Z\",\"\\u00b0\":\"_\",\"q\":\"a\",\",\":\"m\",\"a\":\"q\",\"z\":\"w\",\"w\":\"z\",\"3\":\"#\",\"4\":\"$\",\"2\":\"@\",\"\\u00b5\":\"\\\\\",\"6\":\"^\",\"\\u00b2\":\"`\",\"\\u00a3\":\"|\",\"*\":\"}\",\"\\u00b3\":\"~\",\"^\":\"[\",\"\\u00a8\":\"{\"},\"deadKeysMap\":{\"^\":\"[\",\"\\u00a8\":\"{\",\"\\u00ca\":\"[E\",\"\\u00ce\":\"[I\",\"\\u00d4\":\"[O\",\"\\u00c2\":\"[Q\",\"\\u00db\":\"[U\",\"\\u00ea\":\"[e\",\"\\u00ee\":\"[i\",\"\\u00f4\":\"[o\",\"\\u00e2\":\"[q\",\"\\u00fb\":\"[u\",\"\\u00cb\":\"{E\",\"\\u00cf\":\"{I\",\"\\u00d6\":\"{O\",\"\\u00c4\":\"{Q\",\"\\u00dc\":\"{U\",\"\\u00eb\":\"{e\",\"\\u00ef\":\"{i\",\"\\u00f6\":\"{o\",\"\\u00e4\":\"{q\",\"\\u00fc\":\"{u\",\"\\u00ff\":\"{y\"},\"deadKeyCharacterMap\":{\"^\":\"[\",\"\\u00a8\":\"{\"},\"reportedCharacters\":\"1%57\\u00f9908_;):=\\u00e0&\\u00e9\\\"'(\\u00a7\\u00e8!\\u00e7Mm.-/+QBCDEFGHIJKL?NOPARSTUVZXYW\\u00b0qbcdefghijkl,noparstuvzxyw342\\u00b5$6\\u00b2\\u00a3*\\u00b3\\u0000\\u001c\\u001d\\u001e\\u001f\\u0004\",\"aimFlagCharacterSequence\":\"$\",\"keyboardScript\":\"Latin\"}";
    private const string BelgianFrenchCalibrationNoNull = "{\"characterMap\":{\"1\":\"!\",\"%\":\"\\\"\",\"5\":\"%\",\"7\":\"&\",\"\\u00f9\":\"'\",\"9\":\"(\",\"0\":\")\",\"8\":\"*\",\"_\":\"+\",\";\":\",\",\")\":\"-\",\":\":\".\",\"=\":\"/\",\"\\u00e0\":\"0\",\"&\":\"1\",\"\\u00e9\":\"2\",\"\\\"\":\"3\",\"'\":\"4\",\"(\":\"5\",\"\\u00a7\":\"6\",\"\\u00e8\":\"7\",\"!\":\"8\",\"\\u00e7\":\"9\",\"M\":\":\",\"m\":\";\",\".\":\"<\",\"-\":\"=\",\"/\":\">\",\"+\":\"?\",\"Q\":\"A\",\"?\":\"M\",\"A\":\"Q\",\"Z\":\"W\",\"W\":\"Z\",\"\\u00b0\":\"_\",\"q\":\"a\",\",\":\"m\",\"a\":\"q\",\"z\":\"w\",\"w\":\"z\",\"\\u0000\":\"\\u0004\"},\"ligatureMap\":{\"^\\u00b5\":\"[\",\"\\u00a8\\u00a3\":\"`\"},\"reportedCharacters\":\"1%57\\u00f9908_;):=\\u00e0&\\u00e9\\\"'(\\u00a7\\u00e8!\\u00e7Mm.-/+QBCDEFGHIJKL?NOPARSTUVZXYW\\u00b0qbcdefghijkl,noparstuvzxyw\\u0000\\u001c\\u001d\\u001e\\u001f\\u0004\",\"aimFlagCharacterSequence\":\"]\",\"keyboardScript\":\"Latin\"}";

    private const string GermanToGermanBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] \0^ \0` { | } ~    \u001d    \u001c    \0    \u001f    \0    \x000D";
    private const string GermanToGermanCalibration = "{\"characterMap\":{\"\\u0000\":\"\\u001e\"},\"deadKeysMap\":{\"^\":\"^\",\"`\":\"`\"},\"scannerDeadKeysMap\":{\"^\":\"\\u0000^\",\"`\":\"\\u0000`\"},\"reportedCharacters\":\"!\\\"%&'()*+,-./0123456789:;<=>?ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz#$@[\\\\]{|}~\\u0000\\u001c\\u001d\\u001e\\u001f\\u0004\",\"aimFlagCharacterSequence\":\"]\",\"keyboardScript\":\"Latin\"}";
    private const string GermanToUkWithAimBaseline = "\u0000d1  ! \" % ^ ~ * ( } ] , / . \u0026 0 1 2 3 4 5 6 7 8 9 \u003e \u003c \\ ) | _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \u0000 \u0000 \u0000 \u0000 `  +  \u0000 \u0000 \u0000 \u0000    \u001d    \u0000    \u001e    \u001f    \r";
    private const string GermanToUkWithAimCalibration = "{\"characterMap\":{\"^\":\"&\",\"~\":\"'\",\"*\":\"(\",\"(\":\")\",\"}\":\"*\",\"]\":\"+\",\"/\":\"-\",\"&\":\"/\",\">\":\":\",\"<\":\";\",\"\\\\\":\"<\",\")\":\"=\",\"|\":\">\",\"_\":\"?\",\"Z\":\"Y\",\"Y\":\"Z\",\"?\":\"_\",\"z\":\"y\",\"y\":\"z\",\"\\u0000\":\"]\"},\"scannerDeadKeysMap\":{\"^\":\"` \",\"`\":\"+ \"},\"scannerUnassignedKeys\":[\"~\"],\"reportedCharacters\":\"!\\\"%^~*(}],/.&0123456789><\\\\)|_ABCDEFGHIJKLMNOPQRSTUVWXZY?abcdefghijklmnopqrstuvwxzy#$\\u0000\\u0000\\u001c\\u001d\\u001e\\u001f\\u0004\",\"aimFlagCharacterSequence\":\"\\u0000\",\"keyboardScript\":\"Latin\"}";

    private const string NotABaseline = "fh shdfkjshdfkjhsdkjfhskjdfh ksjdhfk jhsjkfh skjdhf kjshfkjshdfjkh jkshdfiuhweufh weiuhfiusdhfshdofhweuhf ewiohfohfesbfhsaofhwqofhouehfohefo usdhfosdhf ohwfouwheoufhsudhfosdjfowehfouwehfouwh";
    private const string NotADeadKey = "72672y4273462478923y98432y948y23 9y289v37482937489b237482347892f3749823749b8237b489237572138094801y9817u42097892y58203q4098325y9235y8023vu4023v85yb923u509";
    private const string EmptyBaseline = "";
    private const string EmptyDeadKey = "";
    private const string PartialBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B ";
    private const string PartialDeadkey = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^";
    private const string NoCalibration = "";

    private const string SwissFrench2424BaselineA = "  + ä % / à ) = ( \0`, ' . - 0 1 2";
    private const string SwissFrench2424BaselineB = " 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D";
    private const string SwissFrench2424BaselineC = " E F G H I J K L M N O P Q R S T U";
    private const string SwissFrench2424BaselineD = " V W X Z Y ? a b c d e f g h i j k";
    private const string SwissFrench2424BaselineE = " l m n o p q r s t u v w x z y   *";
    private const string SwissFrench2424BaselineF = " ç \" è $ \0\"& § ü £ ! °    \x001D";
    private const string SwissFrench2424BaselineG = "    \x001C    \0    \0    \0    ";
    private const string SwissFrench2424DeadKey1A = "\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1";
    private const string SwissFrench2424DeadKey1B = "\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B";
    private const string SwissFrench2424DeadKey1C = "\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S";
    private const string SwissFrench2424DeadKey1D = "\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d";
    private const string SwissFrench2424DeadKey1E = "\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q";
    private const string SwissFrench2424DeadKey1F = "\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°";
    private const string SwissFrench2424DeadKey2A = "\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1";
    private const string SwissFrench2424DeadKey2B = "\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B";
    private const string SwissFrench2424DeadKey2C = "\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S";
    private const string SwissFrench2424DeadKey2D = "\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d";
    private const string SwissFrench2424DeadKey2E = "\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q";
    private const string SwissFrench2424DeadKey2F = "\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°";
    private const string SwissFrench2424DeadKey3A = "\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1";
    private const string SwissFrench2424DeadKey3B = "\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B";
    private const string SwissFrench2424DeadKey3C = "\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S";
    private const string SwissFrench2424DeadKey3D = "\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d";
    private const string SwissFrench2424DeadKey3E = "\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q";
    private const string SwissFrench2424DeadKey3F = "\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°";

    private const string UnitedKingdomBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string UnitedKingdomBaselinePartial = "r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \x001C    \0    \0    \x000D";

    private const string BelgianFrenchBaselineNoNull = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 ^µ $ 6 ² ¨£ * ³    \x001D    \x001C            \0    \x000D";
    private const string BelgianFrenchDeadKey1NoNull = "^1^%^3^4^5^7^ù^9^0^8^_^;^)^:^=^à^&^é^\"^'^(^§^è^!^ç^M^m^.^-^/^+^2^Q^B^C^DÊ^F^G^HÎ^J^K^L^?^NÔ^PÂ^R^S^TÛ^V^Z^X^Y^W^^^µ^$^6^°^²^q^b^c^dê^f^g^hî^j^k^l^,^nô^pâ^r^s^tû^v^z^x^y^w^¨^£^*^³\x000D";
    private const string BelgianFrenchDeadKey2NoNull = "¨1¨%¨3¨4¨5¨7¨ù¨9¨0¨8¨_¨;¨)¨:¨=¨à¨&¨é¨\"¨'¨(¨§¨è¨!¨ç¨M¨m¨.¨-¨/¨+¨2¨Q¨B¨C¨DË¨F¨G¨HÏ¨J¨K¨L¨?¨NÖ¨PÄ¨R¨S¨TÜ¨V¨Z¨X¨Y¨W¨^¨µ¨$¨6¨°¨²¨q¨b¨c¨dë¨f¨g¨hï¨j¨k¨l¨,¨nö¨pä¨r¨s¨tü¨v¨z¨xÿ¨w¨¨¨£¨*¨³\x000D";

    private const string SwissFrenchBaselineNoNull = "  + ä % / à ) = ( `, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; ^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \"& § ü £ ! °    \x001D    \x001C            \0    \x000D";
    private const string SwissFrenchDeadKey1NoNull = "`+`ä`*`ç`%`/`à`)`=`(```,`'`.`-`0`1`2`3`4`5`6`7`8`9`ö`é`;`^`:`_`\"À`B`C`DÈ`F`G`HÌ`J`K`L`M`NÒ`P`Q`R`S`TÙ`V`W`X`Z`Y`è`$`¨`&`?`§à`b`c`dè`f`g`hì`j`k`l`m`nò`p`q`r`s`tù`v`w`x`z`y`ü`£`!`°\x000D";
    private const string SwissFrenchDeadKey2NoNull = "^+^ä^*^ç^%^/^à^)^=^(^`^,^'^.^-^0^1^2^3^4^5^6^7^8^9^ö^é^;^^^:^_^\"Â^B^C^DÊ^F^G^HÎ^J^K^L^M^NÔ^P^Q^R^S^TÛ^V^W^X^Z^Y^è^$^¨^&^?^§â^b^c^dê^f^g^hî^j^k^l^m^nô^p^q^r^s^tû^v^w^x^z^y^ü^£^!^°\x000D";
    private const string SwissFrenchDeadKey3NoNull = "¨+¨ä¨*¨ç¨%¨/¨à¨)¨=¨(¨`¨,¨'¨.¨-¨0¨1¨2¨3¨4¨5¨6¨7¨8¨9¨ö¨é¨;¨^¨:¨_¨\"Ä¨B¨C¨DË¨F¨G¨HÏ¨J¨K¨L¨M¨NÖ¨P¨Q¨R¨S¨TÜ¨V¨W¨X¨Z¨Y¨è¨$¨¨¨&¨?¨§ä¨b¨c¨dë¨f¨g¨hï¨j¨k¨l¨m¨nö¨p¨q¨r¨s¨tü¨v¨w¨x¨zÿ¨ü¨£¨!¨°\x000D";

    /// <summary>
    /// GS1 Character Set 82 - test 01.
    /// </summary>
    [Fact]
    public void EmvsGs1CharacterSet8201() {
        var emvsTestData = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04772985431594", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// GS1 Trade Item Grouping.
    /// </summary>
    [Fact]
    public void EmvsGs1TradeItemGrouping() {
        var emvsTestData = $"011477298543159110DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("14772985431591", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.True(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(1, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// GS1 Variable Measure Trade Item.
    /// </summary>
    [Fact]
    public void EmvsGs1VariableMeasureTradeItem() {
        var emvsTestData = $"019477298543159710DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("94772985431597", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.True(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(9, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// GS1 Variable Measure Trade Item.
    /// </summary>
    [Fact]
    public void EmvsGs1BadGtin() {
        var emvsTestData = $"010477298543159710DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04772985431597", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// Get the baseline barcode.
    /// </summary>
    [Fact]
    public void BaselineBarcodes() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.BaselineBarcodes();
        Assert.Single(barcodes);
    }

    /// <summary>
    /// Get the small baseline barcodes.
    /// </summary>
    [Fact]
    public void BaselineBarcodesSmall() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.BaselineBarcodes(size: Size.Dm24X24);
        Assert.Equal(7, barcodes.Count);
    }

    /// <summary>
    /// Get the supplementary barcode.
    /// </summary>
    [Fact]
    public void NoSupplementaryBarcodes() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.SupplementalBarcodes();
        Assert.Empty(barcodes);
    }

    /// <summary>
    /// Get the small supplementary barcodes.
    /// </summary>
    [Fact]
    public void NoSupplementaryBarcodesSmall() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.SupplementalBarcodes(size: Size.Dm24X24);
        Assert.Empty(barcodes);
    }

    /// <summary>
    /// Get the supplementary barcode.
    /// </summary>
    [Fact]
    public void SupplementaryBarcodes() {
        var parser = CalibrateBaseline("Belgian French");
        var barcodes = parser.Calibrator.SupplementalBarcodes();
        Assert.Equal(2, barcodes.Count);
    }

    /// <summary>
    /// Get the small supplementary barcodes.
    /// </summary>
    [Fact]
    public void SupplementaryBarcodesSmall() {
        var parser = CalibrateBaseline("Swiss French 24x24", size: Size.Dm24X24);
        var barcodes = parser.Calibrator.SupplementalBarcodes(size: Size.Dm24X24);
        Assert.Equal(3, barcodes.Count);
        Assert.Equal(6, barcodes.Values.ElementAt(0).Count);
        Assert.Equal(6, barcodes.Values.ElementAt(1).Count);
        Assert.Equal(6, barcodes.Values.ElementAt(2).Count);
    }

    /// <summary>
    /// Get the baseline barcode as SVG content.
    /// </summary>
    [Fact]
    public void BaselineBarcodesSvg() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.BaselineBarcodesSvg();
        Assert.Single(barcodes);
    }

    /// <summary>
    /// Get the small baseline barcodes as SVG content.
    /// </summary>
    [Fact]
    public void BaselineBarcodesSmallSvg() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.BaselineBarcodesSvg(size: Size.Dm24X24);
        Assert.Equal(7, barcodes.Count);
    }

    /// <summary>
    /// Get the supplementary barcode as SVG content.
    /// </summary>
    [Fact]
    public void NoSupplementaryBarcodesSvg() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.SupplementalBarcodesSvg();
        Assert.Empty(barcodes);
    }

    /// <summary>
    /// Get the small supplementary barcodes as SVG content.
    /// </summary>
    [Fact]
    public void NoSupplementaryBarcodesSmallSvg() {
        var parser = new Parser();
        var barcodes = parser.Calibrator.SupplementalBarcodesSvg(size: Size.Dm24X24);
        Assert.Empty(barcodes);
    }

    /// <summary>
    /// Get the supplementary barcode as SVG content.
    /// </summary>
    [Fact]
    public void SupplementaryBarcodesSvg() {
        var parser = CalibrateBaseline("Belgian French");
        var barcodes = parser.Calibrator.SupplementalBarcodesSvg();
        Assert.Equal(2, barcodes.Count);
    }

    /// <summary>
    /// Get the small supplementary barcodes as SVG content.
    /// </summary>
    [Fact]
    public void SupplementaryBarcodesSmallSvg() {
        var parser = CalibrateBaseline("Swiss French 24x24", size: Size.Dm24X24);
        var barcodes = parser.Calibrator.SupplementalBarcodesSvg(size: Size.Dm24X24);
        Assert.Equal(3, barcodes.Count);
        Assert.Equal(6, barcodes.Values.ElementAt(0).Count);
        Assert.Equal(6, barcodes.Values.ElementAt(1).Count);
        Assert.Equal(6, barcodes.Values.ElementAt(2).Count);
    }

    [Fact]
    public void MatchingDeadKeys() {
        var token = PerformCalibrationTest("German to German");
        Assert.Empty(token.Errors);
    }

    [Fact]
    public void AimAsAsciiNull() {
        var token = PerformCalibrationTest("German to UK with AIM");
        Assert.Empty(token.Errors);
        var emvsTestData = $"01089028055000561716083110DL13010B{(char)29}21D6222N9424";
        var identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("08902805500056", identifier.ProductCode);
        Assert.Equal("D6222N9424", identifier.SerialNumber);
        Assert.Equal("DL13010B", identifier.BatchIdentifier);
        Assert.Equal("160831", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// Test a valid PPN.
    /// </summary>
    [Fact]
    public void ValidPpn() {
        var emvsTestData = $"[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}";
        var identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Ifa, identifier.Scheme);
        Assert.Equal("111234567842", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)29}9N111234567842{(char)30}{(char)4}";
        identifier = DoTestEmvsWithNoErrors(emvsTestData);
        Assert.Equal(Scheme.Ifa, identifier.Scheme);
        Assert.Equal("111234567842", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.True(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// Test an invalid PPN.
    /// </summary>
    [Fact]
    public void InvalidPpn() {
        var emvsTestData = $"[)>{(char)30}06{(char)29}9N111234567843{(char)29}D2302071TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}";
        var identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Ifa, identifier.Scheme);
        Assert.Equal("111234567843", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("", identifier.BatchIdentifier);
        Assert.Equal("2302071TDdVcX<t", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)29}9N117479361970{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Ifa, identifier.Scheme);
        Assert.Equal(string.Empty, identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.False(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDd VcX<t{(char)29}SyCH*4'h1Ab{(char)29}{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Ifa, identifier.Scheme);
        Assert.Equal("111234567842", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("Dd VcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    /// <summary>
    /// Test an invalid PPN.
    /// </summary>
    [Fact]
    public void InvalidFormat05() {
        var emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670731723020710DdVcX<t{(char)29}21yCH*4'h1Ab{(char)30}{(char)4}";
        var identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04070071967073", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670721723020710DdVcX<t{(char)29}21yCH*4'h1Ab{(char)29}0194772985431597{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal(string.Empty, identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.False(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670721723020710DdVcX<t{(char)29}21yCH*4'h1Ab{(char)29}214773416853892{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04070071967072", identifier.ProductCode);
        Assert.Equal(string.Empty, identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.False(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670721723020710DdVcX<t{(char)29}21yCH*4'h1Ab{(char)29}1053378039BJ6{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04070071967072", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal(string.Empty, identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670721723020710DdVcX<t{(char)29}21yCH*4'h1Ab{(char)29}17200207{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04070071967072", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
        Assert.Equal(string.Empty, identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);

        emvsTestData = $"[)>{(char)30}05{(char)29}01040700719670721723020710Dd VcX<t{(char)29}21yCH*4'h1Ab{(char)29}{(char)30}{(char)4}";
        identifier = DoTestEmvsWithErrors(emvsTestData);
        Assert.Equal(Scheme.Gs1, identifier.Scheme);
        Assert.Equal("04070071967072", identifier.ProductCode);
        Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
        Assert.Equal("Dd VcX<t", identifier.BatchIdentifier);
        Assert.Equal("230207", identifier.Expiry);
        Assert.Equal(SymbologyValidity.Unknown, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.True(identifier.Submit);
        Assert.False(identifier.IsTradeItemGrouping);
        Assert.False(identifier.IsVariableMeasureTradeItem);
        Assert.Equal(0, identifier.Indicator);
        Assert.Empty(identifier.NationalNumbers);
    }

    [Fact]
    public void Prefix() {
        var parser = CalibrateBaseline("United Kingdom", prefix: "prefix");
        var emvsTestData = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithAim = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithPrefix = $"prefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithPrefixAndAim1 = $"prefix]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithPrefixAndAim2 = $"]d2prefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithWrongPrefix = $"incorrectprefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithWrongPrefixAndAim = $"]d2incorrectprefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";

        DoNoErrorAsserts(parser.Parse(emvsTestData));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithAim));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefixAndAim1));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefixAndAim2));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefixAndAim));
#pragma warning disable S3626
        return;
#pragma warning restore S3626

        static void DoNoErrorAsserts(IPackIdentifier identifier) {
            Assert.Empty(identifier.ParseExceptions);
            Assert.Empty(identifier.Exceptions);
            Assert.Equal(Scheme.Gs1, identifier.Scheme);
            Assert.Equal("04772985431594", identifier.ProductCode);
            Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
            Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
            Assert.Equal("230207", identifier.Expiry);
            Assert.True(identifier.IsValid);
            Assert.True(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }

        static void DoWithErrorAsserts(IPackIdentifier identifier) {
            Assert.NotEmpty(identifier.ParseExceptions);
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            Assert.Contains(identifier.ParseExceptions, e => e.ErrorNumber == 1003);
            Assert.False(identifier.IsValid);
            Assert.False(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }
    }

    [Fact]
    public void Suffix() {
        var parser = CalibrateBaseline("United Kingdom", suffix: "suffix");
        var emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuffix";
        var emvsTestDataWithSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuffix";
        var emvsTestDataWithWrongSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abincorrectsuffix1";
        var emvsTestDataWithWrongSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abincorrectsuffix1";
        var emvsTestDataWithSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suffix{(char)4}";
        var emvsTestDataWithSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}suffix";
        var emvsTestDataWithWrongSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}incorrectsuffix1{(char)4}";
        var emvsTestDataWithWrongSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}incorrectsuffix1";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04A));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04B));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04A));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04B));
#pragma warning disable S3626
        return;
#pragma warning restore S3626

        static void DoNoErrorAsserts(IPackIdentifier identifier) {
            Assert.Empty(identifier.ParseExceptions);
            Assert.Empty(identifier.Exceptions);
            Assert.True(identifier.IsValid);
            Assert.True(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }

        static void DoWithErrorAsserts(IPackIdentifier identifier) {
            Assert.NotEmpty(identifier.ParseExceptions);
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            Assert.Contains(identifier.ParseExceptions, e => e.ErrorNumber == 1003);
        }
    }

    [Fact]
    public void PrefixAndSuffix() {
        var parser = CalibrateBaseline("United Kingdom", prefix: "prefix", suffix: "suffix");
        var emvsTestDataWithSuffix = $"prefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuffix";
        var emvsTestDataWithSuffixAndAim = $"]d2prefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuffix";
        var emvsTestDataWithWrongSuffix = $"incorrectprefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abincorrectsuffix1";
        var emvsTestDataWithWrongSuffixAndAim = $"]d2incorrectprefix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abincorrectsuffix1";
        var emvsTestDataWithSuffixAndAim04A = $"]d2prefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suffix{(char)4}";
        var emvsTestDataWithSuffixAndAim04B = $"]d2prefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suffix{(char)4}";
        var emvsTestDataWithSuffixAndAim04C = $"]d2prefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}suffix";
        var emvsTestDataWithWrongSuffixAndAim04A = $"]d2prefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}incorrectsuffix1{(char)4}";
        var emvsTestDataWithWrongSuffixAndAim04B = $"]d2prefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}incorrectsuffix1";
        var emvsTestDataWithWrongSuffixAndAim04C = $"]d2incorrectprefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}incorrectsuffix1{(char)4}";
        var emvsTestDataWithWrongSuffixAndAim04D = $"]d2incorrectorefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}incorrectsuffix1";
        var emvsTestDataWithWrongSuffixAndAim04E = $"]d2incorrectprefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suffix{(char)4}";
        var emvsTestDataWithWrongSuffixAndAim04F = $"]d2incorrectorefix[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}suffix";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04A));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04B));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04C));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04A));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04B));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04C));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04D));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04E));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04F));
#pragma warning disable S3626
        return;
#pragma warning restore S3626

        static void DoNoErrorAsserts(IPackIdentifier identifier) {
            Assert.Empty(identifier.ParseExceptions);
            Assert.Empty(identifier.Exceptions);
            Assert.True(identifier.IsValid);
            Assert.True(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }

        static void DoWithErrorAsserts(IPackIdentifier identifier) {
            Assert.NotEmpty(identifier.ParseExceptions);
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            Assert.Contains(identifier.ParseExceptions, e => e.ErrorNumber == 1003);
        }
    }

    [Fact]
    public void PrefixWithSpaces() {
        // Single space
        var parser = CalibrateBaseline("United Kingdom", prefix: "pre fix");
        var emvsTestDataWithPrefix = $"pre fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithPrefixAndAim = $"]d2pre fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithWrongPrefix = $"in correct010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        var emvsTestDataWithWrongPrefixAndAim = $"]d2in correct010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefixAndAim));

        // Double space
        parser = CalibrateBaseline("United Kingdom", prefix: "pre  fix");
        emvsTestDataWithPrefix = $"pre  fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        emvsTestDataWithPrefixAndAim = $"]d2pre  fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        emvsTestDataWithWrongPrefix = $"in  correct010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        emvsTestDataWithWrongPrefixAndAim = $"]d2in  correct010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongPrefixAndAim));

        // Various
        parser = CalibrateBaseline("United Kingdom", prefix: "pre  fix   ");
        emvsTestDataWithPrefix = $"pre  fix   010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));

        parser = CalibrateBaseline("United Kingdom", prefix: "p  re  fix");
        emvsTestDataWithPrefix = $"p  re  fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));

        parser = CalibrateBaseline("United Kingdom", prefix: "p      re      fix");
        emvsTestDataWithPrefix = $"p      re      fix010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Ab";
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithPrefix));
#pragma warning disable S3626
        return;
#pragma warning restore S3626

        static void DoWithErrorAsserts(IPackIdentifier identifier) {
            Assert.NotEmpty(identifier.ParseExceptions);
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            Assert.Contains(identifier.ParseExceptions, e => e.ErrorNumber == 1003);
            Assert.False(identifier.IsValid);
            Assert.False(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }

        static void DoNoErrorAsserts(IPackIdentifier identifier) {
            Assert.Empty(identifier.ParseExceptions);
            Assert.Empty(identifier.Exceptions);
            Assert.Equal(Scheme.Gs1, identifier.Scheme);
            Assert.Equal("04772985431594", identifier.ProductCode);
            Assert.Equal("yCH*4'h1Ab", identifier.SerialNumber);
            Assert.Equal("DdVcX<t", identifier.BatchIdentifier);
            Assert.Equal("230207", identifier.Expiry);
            Assert.True(identifier.IsValid);
            Assert.True(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }
    }

    [Fact]
    public void SuffixWithSpaces() {
        // Single space
        var parser = CalibrateBaseline("United Kingdom", suffix: "suf fix");
        var emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuf fix";
        var emvsTestDataWithSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuf fix";
        var emvsTestDataWithWrongSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abin correctsuffix1";
        var emvsTestDataWithWrongSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abin correctsuffix1";
        var emvsTestDataWithSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suf fix{(char)4}";
        var emvsTestDataWithSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}suf fix";
        var emvsTestDataWithWrongSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}in correctsuffix1{(char)4}";
        var emvsTestDataWithWrongSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}in correctsuffix1";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04A));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04B));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04A));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04B));

        // Double space
        parser = CalibrateBaseline("United Kingdom", suffix: "suf  fix");
        emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuf  fix";
        emvsTestDataWithSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuf  fix";
        emvsTestDataWithWrongSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abin  correctsuffix1";
        emvsTestDataWithWrongSuffixAndAim = $"]d2010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abin  correctsuffix1";
        emvsTestDataWithSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}suf  fix{(char)4}";
        emvsTestDataWithSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}suf  fix";
        emvsTestDataWithWrongSuffixAndAim04A = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}in  correctsuffix1{(char)4}";
        emvsTestDataWithWrongSuffixAndAim04B = $"]d2[)>{(char)30}06{(char)29}9N111234567842{(char)29}D230207{(char)29}1TDdVcX<t{(char)29}SyCH*4'h1Ab{(char)30}{(char)4}in  correctsuffix1";

        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffix));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04A));
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffixAndAim04B));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04A));
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithWrongSuffixAndAim04B));

        // Various
        parser = CalibrateBaseline("United Kingdom", suffix: "suf  fix   ");
        emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Absuf  fix   ";
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));

        parser = CalibrateBaseline("United Kingdom", suffix: "s  uf  fix");
        emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abs  uf  fix";
        DoNoErrorAsserts(parser.Parse(emvsTestDataWithSuffix));

        parser = CalibrateBaseline("United Kingdom", suffix: "s      uf       fix");
        emvsTestDataWithSuffix = $"010477298543159410DdVcX<t{(char)29}1723020721yCH*4'h1Abs      uf       fix";
        DoWithErrorAsserts(parser.Parse(emvsTestDataWithSuffix));
#pragma warning disable S3626
        return;
#pragma warning restore S3626

        static void DoWithErrorAsserts(IPackIdentifier identifier) {
            Assert.NotEmpty(identifier.ParseExceptions);
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            Assert.Contains(identifier.ParseExceptions, e => e.ErrorNumber == 1003);
        }

        static void DoNoErrorAsserts(IPackIdentifier identifier) {
            Assert.Empty(identifier.ParseExceptions);
            Assert.Empty(identifier.Exceptions);
            Assert.True(identifier.IsValid);
            Assert.True(identifier.Submit);
            Assert.False(identifier.IsTradeItemGrouping);
            Assert.False(identifier.IsVariableMeasureTradeItem);
            Assert.Equal(0, identifier.Indicator);
            Assert.Empty(identifier.NationalNumbers);
        }
    }

    [Fact]
    public void PartialOrInvalidCalibrationBarcodes() {
        var token = PerformCalibrationTest("Not a Baseline");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        e => e.InformationType == InformationType.UnrecognisedData);

        token = PerformCalibrationTest("Not a Deadkey");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        e => e.InformationType == InformationType.UnrecognisedData);

        token = PerformCalibrationTest("Empty Baseline");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        e => e.InformationType == InformationType.NoCalibrationDataReported);

        token = PerformCalibrationTest("Empty Deadkey");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        e => e.InformationType == InformationType.NoCalibrationDataReported);

        token = PerformCalibrationTest("Partial Baseline");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        w => w.InformationType == InformationType.PartialCalibrationDataReported);

        token = PerformCalibrationTest("Partial Deadkey");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        w => w.InformationType == InformationType.PartialCalibrationDataReported);

        token = PerformCalibrationTest("Wrong Baseline");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        w => w.InformationType == InformationType.IncorrectCalibrationDataReported);

        token = PerformCalibrationTest("Wrong Deadkey");
        Assert.Null(token.CalibrationData);
        Assert.Contains(token.Errors,
                        w => w.InformationType == InformationType.IncorrectCalibrationDataReported);
    }

    [Fact]
    public void NoAsciiNull() {
        var token = PerformCalibrationTest("Swiss French with No ASCII Null");
        Assert.Null(token.CalibrationData);

        token = PerformCalibrationTest("Belgian French with No ASCII Null");
        Assert.NotNull(token.CalibrationData);
    }

    /// <summary>
    /// A valid AIM identifier without any additional content.
    /// </summary>
    [Fact]
    public void AimIdentifierOnly() {
        const string aimTestData = "]d2";

        var identifier = DoTestEmvsWithErrors(aimTestData);
        Assert.Equal(SymbologyValidity.True, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.False(identifier.Submit);
    }

    /// <summary>
    /// An invalid AIM identifier without any additional content.
    /// </summary>
    [Fact]
    public void InvalidAimIdentifierOnly() {
        const string aimTestData = "]f0";

        var identifier = DoTestEmvsWithErrors(aimTestData);
        Assert.Equal(SymbologyValidity.False, identifier.ValidSymbology);
        Assert.False(identifier.IsValid);
        Assert.False(identifier.Submit);
    }

    /// <summary>
    /// Calibrates the baseline barcode.
    /// </summary>
    /// <param name="layoutName">The name of the computer keyboard layout</param>
    /// <param name="prefix">Optional prefix.</param>
    /// <param name="suffix">Optional suffix.</param>
    /// <param name="multiplier">The multiplier for the size of the data matrix image.</param>
    /// <param name="size">The size of the data matrix.</param>
    /// <returns>A calibration token.</returns>
    private static Parser CalibrateBaseline(string layoutName, string prefix = "", string suffix = "", float multiplier = 1F, Size size = Size.Automatic) {
        var computerKeyboardLayout = UnitedStatesTestData()[layoutName];

        var calibrator = new Calibrator();
        var loopCountForBaseline = 0;
        var loopCount = -1;
        Token currentToken = default;

        // If a prefix is provided with two consecutive spaces, we should pre-configure the calibrator with the prefix value.
        if (!string.IsNullOrEmpty(prefix) && prefix.Contains("  ")) {
            calibrator.SetReportedPrefix(prefix);
        }

        foreach (var token in calibrator.CalibrationTokens(multiplier, size)) {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0) {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(prefix + AttachSuffix(baseLine[loopCountForBaseline++])), currentToken);
                loopCount = loopCountForBaseline == baseLine.Length ? ++loopCount : loopCount;
            }
            else {
                if (loopCount < computerKeyboardLayout[baseLine].Count) {
                    currentToken = calibrator.Calibrate(
                        ConvertToCharacterValues(prefix + AttachSuffix(computerKeyboardLayout[baseLine][loopCount++])),
                        currentToken);
                }
            }

            foreach (var error in currentToken.Errors) {
                Debug.WriteLine(error.Description);
            }

#pragma warning disable S3626
#pragma warning disable S1751
            continue;
#pragma warning restore S1751
#pragma warning restore S3626

            string AttachSuffix(string baseline) {
                if (string.IsNullOrEmpty(suffix)) {
                    return baseline;
                }

                var eolSeq = string.Empty;

                while (baseline.EndsWith('\r') || baseline.EndsWith('\n')) {
                    eolSeq += baseline.Last();
                    baseline = baseline[..^1];
                }

                var outBaseline = baseline + suffix;

                return eolSeq.Reverse().Aggregate(outBaseline, (current, c) => current + c);

            }
        }

        return new Parser(currentToken.CalibrationData ?? new BarcodeScanner.Calibration.Data(
            CharacterMap: null,
            DeadKeysMap: null,
            DeadKeyCharacterMap: null,
            LigatureMap: null,
            ScannerDeadKeysMap: null,
            ScannerUnassignedKeys: null,
            ReportedCharacters: null,
            AimFlagCharacterSequence: null,
            Prefix: null,
            Code: null,
            Suffix: null,
            KeyboardScript: null,
            ScannerKeyboardPerformance: ScannerKeyboardPerformance.High,
            LineFeedCharacter: null));
    }

    /// <summary>
    /// Performs a calibration test.
    /// </summary>
    /// <param name="layoutName">The name of the computer keyboard layout</param>
    /// <param name="multiplier">The multiplier for the size of the data matrix image.</param>
    /// <param name="size">The size of the data matrix.</param>
    /// <returns>A calibration token.</returns>
    private static Token PerformCalibrationTest(string layoutName, float multiplier = 1F, Size size = Size.Automatic) {
        Debug.WriteLine(layoutName);

        var expectedCalibrations = UnitedStatesExpectedCalibrations();
        var computerKeyboardLayout = UnitedStatesTestData()[layoutName];

        var calibrator = new Calibrator();
        var loopCountForBaseline = 0;
        var loopCount = -1;
        Token currentToken = default;

        foreach (var token in calibrator.CalibrationTokens(multiplier, size)) {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0) {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(baseLine[loopCountForBaseline++]), currentToken);
                loopCount = loopCountForBaseline == baseLine.Length ? ++loopCount : loopCount;
            }
            else {
                if (loopCount < computerKeyboardLayout[baseLine].Count) {
                    currentToken = calibrator.Calibrate(
                        ConvertToCharacterValues(computerKeyboardLayout[baseLine][loopCount++]),
                        currentToken);
                }
            }

            foreach (var error in currentToken.Errors) {
                Debug.WriteLine(error.Description);
            }
        }

        Trace.WriteLine($"private const string {layoutName.Replace(" ", "")}Calibration = \"{calibrator.CalibrationData}\";");

        // Assert that the calibrator calculated the expected calibration.
        Assert.Equal(expectedCalibrations[layoutName], currentToken.CalibrationData?.ToJson() ?? string.Empty);

        return currentToken;
    }

    /// <summary>
    /// Convert an input string to a list of character values.
    /// </summary>
    /// <param name="input">The string of characters to be converted.</param>
    /// <returns>A comma-separated value list of character values.</returns>
    private static int[] ConvertToCharacterValues(string input) {
        if (string.IsNullOrWhiteSpace(input)) {
            return [];
        }

        var outputBuilder = new int[input.Length];

        for (var idx = 0; idx < input.Length; idx++) {
            outputBuilder[idx] = input[idx];
        }

        return outputBuilder;
    }

    /// <summary>
    /// Test EMVS data and ensure there are no errors.
    /// </summary>
    /// <param name="gs1TestData">The GS1 data to be tested.</param>
    /// <returns>The results.</returns>
    // ReSharper disable once UnusedMethodReturnValue.Local
    private static IPackIdentifier DoTestEmvsWithNoErrors(string gs1TestData) {
        var identifier = DoTestEmvs(gs1TestData);
        Assert.Empty(identifier.ParseExceptions);
        Assert.Empty(identifier.Exceptions);

        return identifier;
    }


    /// <summary>
    /// Test EMVS pak identifier data and ensure there are expected errors.
    /// </summary>
    /// <param name="emvsTestData">The EMVS data to be tested.</param>
    /// <returns>The results.</returns>
    private static IPackIdentifier DoTestEmvsWithErrors(string emvsTestData) {
        var identifier = DoTestEmvs(emvsTestData);
        var anyParseExceptions = identifier.ParseExceptions.Any();
        var anyExceptions = identifier.Exceptions.Any();

        Assert.True(anyParseExceptions || anyExceptions);

        return identifier;
    }

    /// <summary>
    /// Test GS1 data.
    /// </summary>
    /// <param name="emvsTestData">The data to be tested.</param>
    /// <returns>The results.</returns>
    // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
    private static IPackIdentifier DoTestEmvs(string emvsTestData) {
        var parser = new Parser();
        return parser.Parse(emvsTestData);
    }

    /// <summary>
    /// Returns test data for testing calibration for a scanner configured as a United States keyboard and
    /// computer keyboard layouts for each European keyboard defined in Windows.
    /// </summary>
    /// <returns>A dictionary of test data.</returns>
    private static Dictionary<string, Dictionary<string[], IList<string>>> UnitedStatesTestData() {
        var unitedStatesTestData = new Dictionary<string, Dictionary<string[], IList<string>>>
                                   {
                                       {
                                           "United Kingdom",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [UnitedKingdomBaseline], new List<string>()}
                                           }
                                       },
                                       {
                                           "United Kingdom Partial",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [UnitedKingdomBaselinePartial], new List<string>()}
                                           }
                                       },
                                       {
                                           "Belgian French",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [BelgianFrenchBaseline],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1, BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Belgian French with No ASCII Null",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [BelgianFrenchBaselineNoNull],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1NoNull, BelgianFrenchDeadKey2NoNull
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "German to German",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GermanToGermanBaseline],
                                                   new List<string>()
                                               }
                                           }
                                       },
                                       {
                                           "German to UK with AIM",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GermanToUkWithAimBaseline],
                                                   new List<string>()
                                               }
                                           }
                                       },
                                       {
                                           "Swiss French with No ASCII Null",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SwissFrenchBaselineNoNull],
                                                   new List<string>
                                                   {
                                                       SwissFrenchDeadKey1NoNull,
                                                       SwissFrenchDeadKey2NoNull,
                                                       SwissFrenchDeadKey3NoNull
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Swiss French 24x24",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       SwissFrench2424BaselineA,
                                                       SwissFrench2424BaselineB,
                                                       SwissFrench2424BaselineC,
                                                       SwissFrench2424BaselineD,
                                                       SwissFrench2424BaselineE,
                                                       SwissFrench2424BaselineF,
                                                       SwissFrench2424BaselineG
                                                   ],
                                                   new List<string>
                                                   {
                                                       SwissFrench2424DeadKey1A,
                                                       SwissFrench2424DeadKey1B,
                                                       SwissFrench2424DeadKey1C,
                                                       SwissFrench2424DeadKey1D,
                                                       SwissFrench2424DeadKey1E,
                                                       SwissFrench2424DeadKey1F,
                                                       SwissFrench2424DeadKey2A,
                                                       SwissFrench2424DeadKey2B,
                                                       SwissFrench2424DeadKey2C,
                                                       SwissFrench2424DeadKey2D,
                                                       SwissFrench2424DeadKey2E,
                                                       SwissFrench2424DeadKey2F,
                                                       SwissFrench2424DeadKey3A,
                                                       SwissFrench2424DeadKey3B,
                                                       SwissFrench2424DeadKey3C,
                                                       SwissFrench2424DeadKey3D,
                                                       SwissFrench2424DeadKey3E,
                                                       SwissFrench2424DeadKey3F
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Not a Baseline",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       NotABaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Not a Deadkey",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       BelgianFrenchBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       NotADeadKey,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Empty Baseline",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       EmptyBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Empty Deadkey",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       BelgianFrenchBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       EmptyDeadKey,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Partial Baseline",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       PartialBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Partial Deadkey",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       BelgianFrenchBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       PartialDeadkey,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Wrong Baseline",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       BelgianFrenchDeadKey1
                                                   ],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Wrong Deadkey",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   // ReSharper disable once RedundantExplicitArrayCreation
                                                   [
                                                       BelgianFrenchBaseline
                                                   ],
                                                   new List<string>
                                                   {
                                                       BelgianFrenchBaseline,
                                                       BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       }
                                   };

        return unitedStatesTestData;
    }

    /// <summary>
    /// Returns expected calibrations for a scanner configured as a United States keyboard and computer
    /// keyboard layouts for each European keyboard defined in Windows.
    /// </summary>
    /// <returns>A dictionary of expected calibrations.</returns>
    private static Dictionary<string, string> UnitedStatesExpectedCalibrations() {
        var unitedStatesTestCalibrations = new Dictionary<string, string>
                                           {
                                               { "Belgian French", BelgianFrenchCalibration },
                                               { "Not a Baseline", NoCalibration },
                                               { "Not a Deadkey", NoCalibration },
                                               { "Empty Baseline", NoCalibration },
                                               { "Empty Deadkey", NoCalibration },
                                               { "Partial Baseline", NoCalibration },
                                               { "Partial Deadkey", NoCalibration },
                                               { "Wrong Baseline", NoCalibration },
                                               { "Wrong Deadkey", NoCalibration },
                                               { "Belgian French with No ASCII Null", BelgianFrenchCalibrationNoNull },
                                               { "Swiss French with No ASCII Null", NoCalibration },
                                               { "German to German", GermanToGermanCalibration },
                                               { "German to UK with AIM", GermanToUkWithAimCalibration }

                                           };
        return unitedStatesTestCalibrations;
    }
}