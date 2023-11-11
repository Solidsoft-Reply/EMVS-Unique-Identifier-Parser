// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardCalibratorTestsFromUnitedStatesPpn.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.  All rights reserved.
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
// Unit tests for the Keyboard Calibrator
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests;

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

using EmvsUniqueIdentifier;
using Packs;
using BarcodeScanner.Calibration;

/// <summary>
/// Unit tests for the Keyboard Calibrator.
/// </summary>
public class KeyboardCalibratorTestsFromUnitedStatesPpn
{
    private const string UnitedStatesBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \x000D";
    private const string UnitedStatesBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string UnitedStatesBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string UnitedStatesBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string UnitedStatesBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P\"\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string UnitedStatesBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string UnitedStatesBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j\"%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string UnitedStatesBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string UnitedStatesBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string UnitedStatesBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(\"P_nF8=9L\0\x0004\x000D";
    private const string UnitedStatesBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%\"\0\x0004\x000D";
    private const string UnitedStatesBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS\"j:AugCzJt\0\x0004\x000D";
    private const string UnitedStatesBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";

    // Test in the case that the ASCII 30 is reported as a character mapped to another character.
    private const string LexonPpnErrorBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \0    @    \0    \x000D";
    private const string LexonPpnErrorBarcode1 = "[)>@06\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab@\x0004\x000D";
    private const string LexonPpnErrorBarcode2 = "[)>@06\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK@\x0004\x000D";
    private const string LexonPpnErrorBarcode3 = "[)>@06\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS@\x0004\x000D";
    private const string LexonPpnErrorBarcode4 = "[)>@06\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ@\x0004\x000D";
    private const string LexonPpnErrorBarcode5 = "[)>@06\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y@\x0004\x000D";
    private const string LexonPpnErrorBarcode6 = "[)>@06\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA@\x0004\x000D";
    private const string LexonPpnErrorBarcode7 = "[)>@06\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!@\x0004\x000D";
    private const string LexonPpnErrorBarcode8 = "[)>@06\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf@\x0004\x000D";
    private const string LexonPpnErrorBarcode9 = "[)>@06\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L@\x0004\x000D";
    private const string LexonPpnErrorBarcode10 = "[)>@06\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@@\x0004\x000D";
    private const string LexonPpnErrorBarcode11 = "[)>@06\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt@\x0004\x000D";
    private const string LexonPpnErrorBarcode12 = "[)>@06\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ@\x0004\x000D";

    private const string BelgianFrenchBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \x000D";
    private const string BelgianFrenchDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianFrenchDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianFrenchBarcode1 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TDdVcX.t\x001DDé\"àéàè\x001DSyCH8'ùh&Qb\0\x0004\x000D";
    private const string BelgianFrenchBarcode2 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T:GRs1aO\x001DDé\"àèéç\x001DSTgIv+;§B,K\0\x0004\x000D";
    private const string BelgianFrenchBarcode3 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T9fpNxYi\x001DDé\"à'à(\x001DSW7Urè/àoAS\0\x0004\x000D";
    private const string BelgianFrenchBarcode4 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T-!Fn°P%\x001DDé'&ààé\x001DSEz(mé=wq?J\0\x0004\x000D";
    private const string BelgianFrenchBarcode5 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T\"kuZ0Lç\x001DDé'à\"à'\x001DS(j'CltEc)Y\0\x0004\x000D";
    private const string BelgianFrenchBarcode6 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TMj%5e_P\x001DDé\"&à\"&\x001DS(&itJwCguQ\0\x0004\x000D";
    private const string BelgianFrenchBarcode7 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TbQ&hù'8\x001DDé\"&é&'\x001DSt.XcVdDàa1\0\x0004\x000D";
    private const string BelgianFrenchBarcode8 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&THCyK,B§\x001DDé'à§éà\x001DSsRG:iYxNpf\0\x0004\x000D";
    private const string BelgianFrenchBarcode9 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T;+vIgTS\x001DDé'àé&!\x001DS9%P°nF!-çL\0\x0004\x000D";
    private const string BelgianFrenchBarcode10 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TAoà/èrU\x001DDé'àèà\"\x001DS0Zuk\"P_e5%\0\x0004\x000D";
    private const string BelgianFrenchBarcode11 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T7WJ?qw=\x001DDé\"&&é§\x001DS%jMQugCwJt\0\x0004\x000D";
    private const string BelgianFrenchBarcode12 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&Tém(zEY)\x001DDé\"à'à'\x001DSHIrAçAeTyA\0\x0004\x000D";
    private const string BelgianCommaBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \x000D";
    private const string BelgianCommaDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianCommaDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianCommaBarcode1 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TDdVcX.t\x001DDé\"àéàè\x001DSyCH8'ùh&Qb\0\x0004\x000D";
    private const string BelgianCommaBarcode2 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T:GRs1aO\x001DDé\"àèéç\x001DSTgIv+;§B,K\0\x0004\x000D";
    private const string BelgianCommaBarcode3 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T9fpNxYi\x001DDé\"à'à(\x001DSW7Urè/àoAS\0\x0004\x000D";
    private const string BelgianCommaBarcode4 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T-!Fn°P%\x001DDé'&ààé\x001DSEz(mé=wq?J\0\x0004\x000D";
    private const string BelgianCommaBarcode5 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T\"kuZ0Lç\x001DDé'à\"à'\x001DS(j'CltEc)Y\0\x0004\x000D";
    private const string BelgianCommaBarcode6 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TMj%5e_P\x001DDé\"&à\"&\x001DS(&itJwCguQ\0\x0004\x000D";
    private const string BelgianCommaBarcode7 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TbQ&hù'8\x001DDé\"&é&'\x001DSt.XcVdDàa1\0\x0004\x000D";
    private const string BelgianCommaBarcode8 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&THCyK,B§\x001DDé'à§éà\x001DSsRG:iYxNpf\0\x0004\x000D";
    private const string BelgianCommaBarcode9 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T;+vIgTS\x001DDé'àé&!\x001DS9%P°nF!-çL\0\x0004\x000D";
    private const string BelgianCommaBarcode10 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TAoà/èrU\x001DDé'àèà\"\x001DS0Zuk\"P_e5%\0\x0004\x000D";
    private const string BelgianCommaBarcode11 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T7WJ?qw=\x001DDé\"&&é§\x001DS%jMQugCwJt\0\x0004\x000D";
    private const string BelgianCommaBarcode12 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&Tém(zEY)\x001DDé\"à'à'\x001DSHIrAçAeTyA\0\x0004\x000D";
    private const string BelgianPeriodBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \x000D";
    private const string BelgianPeriodDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianPeriodDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianPeriodBarcode1 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TDdVcX.t\x001DDé\"àéàè\x001DSyCH8'ùh&Qb\0\x0004\x000D";
    private const string BelgianPeriodBarcode2 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T:GRs1aO\x001DDé\"àèéç\x001DSTgIv+;§B,K\0\x0004\x000D";
    private const string BelgianPeriodBarcode3 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T9fpNxYi\x001DDé\"à'à(\x001DSW7Urè/àoAS\0\x0004\x000D";
    private const string BelgianPeriodBarcode4 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T-!Fn°P%\x001DDé'&ààé\x001DSEz(mé=wq?J\0\x0004\x000D";
    private const string BelgianPeriodBarcode5 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T\"kuZ0Lç\x001DDé'à\"à'\x001DS(j'CltEc)Y\0\x0004\x000D";
    private const string BelgianPeriodBarcode6 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TMj%5e_P\x001DDé\"&à\"&\x001DS(&itJwCguQ\0\x0004\x000D";
    private const string BelgianPeriodBarcode7 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TbQ&hù'8\x001DDé\"&é&'\x001DSt.XcVdDàa1\0\x0004\x000D";
    private const string BelgianPeriodBarcode8 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&THCyK,B§\x001DDé'à§éà\x001DSsRG:iYxNpf\0\x0004\x000D";
    private const string BelgianPeriodBarcode9 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T;+vIgTS\x001DDé'àé&!\x001DS9%P°nF!-çL\0\x0004\x000D";
    private const string BelgianPeriodBarcode10 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&TAoà/èrU\x001DDé'àèà\"\x001DS0Zuk\"P_e5%\0\x0004\x000D";
    private const string BelgianPeriodBarcode11 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&T7WJ?qw=\x001DDé\"&&é§\x001DS%jMQugCwJt\0\x0004\x000D";
    private const string BelgianPeriodBarcode12 = "\0^0/\0à§\x001DçN&&&àà\"(çéèèà\x001D&Tém(zEY)\x001DDé\"à'à'\x001DSHIrAçAeTyA\0\x0004\x000D";
    private const string FrenchBaseline = "  1 % 5 7 ù 9 0 8 + ; ) : ! à & é \" ' ( - è _ ç M m . = / § Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^* $ 6 ² \0¨µ £ \0    \x001D    \x001C    \0    \0    \x000D";
    private const string FrenchDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^+\0^;\0^)\0^:\0^!\0^à\0^&\0^é\0^\"\0^'\0^(\0^-\0^è\0^_\0^ç\0^M\0^m\0^.\0^=\0^/\0^§\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^*\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^µ\0^£\0\0^";
    private const string FrenchDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨+\0¨;\0¨)\0¨:\0¨!\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨-\0¨è\0¨_\0¨ç\0¨M\0¨m\0¨.\0¨=\0¨/\0¨§\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨*\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨µ\0¨£\0\0¨";
    private const string FrenchBarcode1 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&TDdVcX.t\x001DDé\"àéàè\x001DSyCH8'ùh&Qb\0\x0004\x000D";
    private const string FrenchBarcode2 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T:GRs1aO\x001DDé\"àèéç\x001DSTgIv§;-B,K\0\x0004\x000D";
    private const string FrenchBarcode3 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T9fpNxYi\x001DDé\"à'à(\x001DSW7Urè/àoAS\0\x0004\x000D";
    private const string FrenchBarcode4 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T=_Fn°P%\x001DDé'&ààé\x001DSEz(mé!wq?J\0\x0004\x000D";
    private const string FrenchBarcode5 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T\"kuZ0Lç\x001DDé'à\"à'\x001DS(j'CltEc)Y\0\x0004\x000D";
    private const string FrenchBarcode6 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&TMj%5e+P\x001DDé\"&à\"&\x001DS(&itJwCguQ\0\x0004\x000D";
    private const string FrenchBarcode7 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&TbQ&hù'8\x001DDé\"&é&'\x001DSt.XcVdDàa1\0\x0004\x000D";
    private const string FrenchBarcode8 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&THCyK,B-\x001DDé'à-éà\x001DSsRG:iYxNpf\0\x0004\x000D";
    private const string FrenchBarcode9 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T;§vIgTS\x001DDé'àé&_\x001DS9%P°nF_=çL\0\x0004\x000D";
    private const string FrenchBarcode10 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&TAoà/èrU\x001DDé'àèà\"\x001DS0Zuk\"P+e5%\0\x0004\x000D";
    private const string FrenchBarcode11 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&T7WJ?qw!\x001DDé\"&&é-\x001DS%jMQugCwJt\0\x0004\x000D";
    private const string FrenchBarcode12 = "\0^0/\0à-\x001DçN&&&àà\"(çéèèà\x001D&Tém(zEY)\x001DDé\"à'à'\x001DSHIrAçAeTyA\0\x0004\x000D";
    private const string SwissFrenchBaseline = "  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \x001D    \x001C    \0    \0    \x000D";
    private const string SwissFrenchDeadKey1 = "\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\x000D";
    private const string SwissFrenchDeadKey2 = "\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\x000D";
    private const string SwissFrenchDeadKey3 = "\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\x000D";
    private const string SwissFrenchBarcode1 = "è=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4àh1Ab\0\x0004\x000D";
    private const string SwissFrenchBarcode2 = "è=:\006\x001D9N111003592770\x001D1T.GRs+qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string SwissFrenchBarcode3 = "è=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string SwissFrenchBarcode4 = "è=:\006\x001D9N111003592770\x001D1T\0^8Fn?Pä\x001DD241002\x001DSEw5é2-yaMJ\0\x0004\x000D";
    private const string SwissFrenchBarcode5 = "è=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Z\0\x0004\x000D";
    private const string SwissFrenchBarcode6 = "è=:\006\x001D9N111003592770\x001D1Töjä%e\0`P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string SwissFrenchBarcode7 = "è=:\006\x001D9N111003592770\x001D1TbA1hà4(\x001DD231214\x001DSt;XcVdD0q+\0\x0004\x000D";
    private const string SwissFrenchBarcode8 = "è=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string SwissFrenchBarcode9 = "è=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)äP?nF8\0^9L\0\x0004\x000D";
    private const string SwissFrenchBarcode10 = "è=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%ä\0\x0004\x000D";
    private const string SwissFrenchBarcode11 = "è=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSäjöAugCyJt\0\x0004\x000D";
    private const string SwissFrenchBarcode12 = "è=:\006\x001D9N111003592770\x001D1T2é5wEZ'\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string CroatianStandardBaseline = "  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \x001B    \x001C    \0    \0    \x000D";
    private const string CroatianStandardDeadKey1 = "\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\x000D";
    private const string CroatianStandardDeadKey2 = "\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\x000D";
    private const string CroatianStandardBarcode1 = "š=:\006\x001B9N111003592770\x001B1TDdVcX;t\x001BD230207\x001BSzCH(4ćh1Ab\0\x0004\x000D";
    private const string CroatianStandardBarcode2 = "š=:\006\x001B9N111003592770\x001B1T.GRs!qO\x001BD230729\x001BSTgIv_,6BmK\0\x0004\x000D";
    private const string CroatianStandardBarcode3 = "š=:\006\x001B9N111003592770\x001B1T)fpNxZi\x001BD230405\x001BSY/Ur7:0oQS\0\x0004\x000D";
    private const string CroatianStandardBarcode4 = "š=:\006\x001B9N111003592770\x001B1T+8Fn?PĆ\x001BD241002\x001BSEw5č2-yaMJ\0\x0004\x000D";
    private const string CroatianStandardBarcode5 = "š=:\006\x001B9N111003592770\x001B1T3kuW=L9\x001BD240304\x001BS5j4CltEc'Z\0\x0004\x000D";
    private const string CroatianStandardBarcode6 = "š=:\006\x001B9N111003592770\x001B1TČjĆ%e*P\x001BD231031\x001BS51itJyCguA\0\x0004\x000D";
    private const string CroatianStandardBarcode7 = "š=:\006\x001B9N111003592770\x001B1TbA1hć4(\x001BD231214\x001BSt;XcVdD0q!\0\x0004\x000D";
    private const string CroatianStandardBarcode8 = "š=:\006\x001B9N111003592770\x001B1THCzKmB6\x001BD240620\x001BSsRG.iZxNpf\0\x0004\x000D";
    private const string CroatianStandardBarcode9 = "š=:\006\x001B9N111003592770\x001B1T,_vIgTS\x001BD240218\x001BS)ĆP?nF8+9L\0\x0004\x000D";
    private const string CroatianStandardBarcode10 = "š=:\006\x001B9N111003592770\x001B1TQo0:7rU\x001BD240703\x001BS=Wuk3P*e%Ć\0\x0004\x000D";
    private const string CroatianStandardBarcode11 = "š=:\006\x001B9N111003592770\x001B1T/YJMay-\x001BD231126\x001BSĆjČAugCyJt\0\x0004\x000D";
    private const string CroatianStandardBarcode12 = "š=:\006\x001B9N111003592770\x001B1T2č5wEZ'\x001BD230404\x001BSHIrQ9QeTzQ\0\x0004\x000D";
    private const string BulgarianBaseline = "  ! Ч % : ч – № / € р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б ѝ Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю $ ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц „ ; = ( Ц “ § )    \0    \0    \0    \0    \x000D";
    private const string BulgarianBarcode1 = "ц№Л\006\09Х111003592770\01ШАаЭъЙРш\0А230207\0ЯщЪГ/4чг1ѝф\0\x0004\x000D";
    private const string BulgarianBarcode2 = "ц№Л\006\09Х111003592770\01ШлЖИя!,Д\0А230729\0ЯШжСэБр6ФпН\0\x0004\x000D";
    private const string BulgarianBarcode3 = "ц№Л\006\09Х111003592770\01Ш–озХйЩс\0А230405\0ЯЮ:Ки7Л0дыЯ\0\x0004\x000D";
    private const string BulgarianBarcode4 = "ц№Л\006\09Х111003592770\01Ш.8Ох$ЗЧ\0А241002\0ЯЕу5м2бюьПТ\0\x0004\x000D";
    private const string BulgarianBarcode5 = "ц№Л\006\09Х111003592770\01Ш3нкУ№В9\0А240304\0Я5т4ЪвшЕъ-Щ\0\x0004\x000D";
    private const string BulgarianBarcode6 = "ц№Л\006\09Х111003592770\01ШМтЧ%е€З\0А231031\0Я51сшТюЪжкѝ\0\x0004\x000D";
    private const string BulgarianBarcode7 = "ц№Л\006\09Х111003592770\01Шфѝ1гч4/\0А231214\0ЯшРЙъЭаА0,!\0\x0004\x000D";
    private const string BulgarianBarcode8 = "ц№Л\006\09Х111003592770\01ШГЪщНпФ6\0А240620\0ЯяИЖлсЩйХзо\0\x0004\x000D";
    private const string BulgarianBarcode9 = "ц№Л\006\09Х111003592770\01ШрБэСжШЯ\0А240218\0Я–ЧЗ$хО8.9В\0\x0004\x000D";
    private const string BulgarianBarcode10 = "ц№Л\006\09Х111003592770\01Шыд0Л7иК\0А240703\0Я№Укн3З€е%Ч\0\x0004\x000D";
    private const string BulgarianBarcode11 = "ц№Л\006\09Х111003592770\01Ш:ЮТПьюб\0А231126\0ЯЧтМѝкжЪюТш\0\x0004\x000D";
    private const string BulgarianBarcode12 = "ц№Л\006\09Х111003592770\01Ш2м5уЕЩ-\0А230404\0ЯГСиы9ыеШщы\0\x0004\x000D";
    private const string BulgarianLatinBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \x000D";
    private const string BulgarianLatinBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string BulgarianLatinBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string BulgarianLatinBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string BulgarianLatinBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P\"\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string BulgarianLatinBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string BulgarianLatinBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j\"%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string BulgarianLatinBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string BulgarianLatinBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string BulgarianLatinBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(\"P_nF8=9L\0\x0004\x000D";
    private const string BulgarianLatinBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%\"\0\x0004\x000D";
    private const string BulgarianLatinBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS\"j:AugCzJt\0\x0004\x000D";
    private const string BulgarianLatinBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBaseline = "  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Я Р С Т У Ж В ѝ Ъ З _ а б ц д е ф г х и й к л м н о п я р с т у ж в ь ъ з   № $ @ ш ю щ € ч Ш Ю Щ Ч    \0    \0    \0    \0    \x000D";
    private const string BulgarianPhoneticTraditionalBarcode1 = "ш)>\006\09Н111003592770\01ТДдЖцѝ<т\0Д230207\0СъЦХ*4'х1Аб\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode2 = "ш)>\006\09Н111003592770\01Т.ГРс!яО\0Д230729\0СТгИж?,6БмК\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode3 = "ш)>\006\09Н111003592770\01Т(фпНьЪи\0Д230405\0СЗ§Ур7>0оЯС\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode4 = "ш)>\006\09Н111003592770\01Т=8Фн_П\"\0Д241002\0СЕв5;2/заМЙ\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode5 = "ш)>\006\09Н111003592770\01Т3куВ)Л9\0Д240304\0С5й4ЦлтЕц-Ъ\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode6 = "ш)>\006\09Н111003592770\01Т:й\"%е+П\0Д231031\0С51итЙзЦгуА\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode7 = "ш)>\006\09Н111003592770\01ТбА1х'4*\0Д231214\0Ст<ѝцЖдД0я!\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode8 = "ш)>\006\09Н111003592770\01ТХЦъКмБ6\0Д240620\0СсРГ.иЪьНпф\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode9 = "ш)>\006\09Н111003592770\01Т,?жИгТС\0Д240218\0С(\"П_нФ8=9Л\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode10 = "ш)>\006\09Н111003592770\01ТЯо0>7рУ\0Д240703\0С)Вук3П+е%\"\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode11 = "ш)>\006\09Н111003592770\01Т§ЗЙМаз/\0Д231126\0С\"й:АугЦзЙт\0\x0004\x000D";
    private const string BulgarianPhoneticTraditionalBarcode12 = "ш)>\006\09Н111003592770\01Т2;5вЕЪ-\0Д230404\0СХИрЯ9ЯеТъЯ\0\x0004\x000D";
    private const string BulgarianPhoneticBaseline = "  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; „ = “ ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Ч Р С Т У В Ш Ж Ъ З – а б ц д е ф г х и й к л м н о п ч р с т у в ш ж ъ з   № $ @ я ь щ € ю Я ѝ Щ Ю    \0    \0    \0    \0    \x000D";
    private const string BulgarianPhoneticBarcode1 = "я)“\006\09Н111003592770\01ТДдВцЖ„т\0Д230207\0СъЦХ*4'х1Аб\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode2 = "я)“\006\09Н111003592770\01Т.ГРс!чО\0Д230729\0СТгИв?,6БмК\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode3 = "я)“\006\09Н111003592770\01Т(фпНжЪи\0Д230405\0СЗ§Ур7“0оЧС\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode4 = "я)“\006\09Н111003592770\01Т=8Фн–П\"\0Д241002\0СЕш5;2/заМЙ\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode5 = "я)“\006\09Н111003592770\01Т3куШ)Л9\0Д240304\0С5й4ЦлтЕц-Ъ\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode6 = "я)“\006\09Н111003592770\01Т:й\"%е+П\0Д231031\0С51итЙзЦгуА\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode7 = "я)“\006\09Н111003592770\01ТбА1х'4*\0Д231214\0Ст„ЖцВдД0ч!\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode8 = "я)“\006\09Н111003592770\01ТХЦъКмБ6\0Д240620\0СсРГ.иЪжНпф\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode9 = "я)“\006\09Н111003592770\01Т,?вИгТС\0Д240218\0С(\"П–нФ8=9Л\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode10 = "я)“\006\09Н111003592770\01ТЧо0“7рУ\0Д240703\0С)Шук3П+е%\"\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode11 = "я)“\006\09Н111003592770\01Т§ЗЙМаз/\0Д231126\0С\"й:АугЦзЙт\0\x0004\x000D";
    private const string BulgarianPhoneticBarcode12 = "я)“\006\09Н111003592770\01Т2;5шЕЪ-\0Д230404\0СХИрЧ9ЧеТъЧ\0\x0004\x000D";
    private const string BulgarianTypewriterBaseline = "  ! Ч % : ч _ № / V р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б Ь Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю І ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц ( ; = ` Ц ) § ~    \x001D    \x001C    \0    \0    \x000D";
    private const string BulgarianTypewriterBarcode1 = "ц№Л\006\x001D9Х111003592770\x001D1ШАаЭъЙРш\x001DА230207\x001DЯщЪГ/4чг1Ьф\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode2 = "ц№Л\006\x001D9Х111003592770\x001D1ШлЖИя!,Д\x001DА230729\x001DЯШжСэБр6ФпН\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode3 = "ц№Л\006\x001D9Х111003592770\x001D1Ш_озХйЩс\x001DА230405\x001DЯЮ:Ки7Л0дыЯ\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode4 = "ц№Л\006\x001D9Х111003592770\x001D1Ш.8ОхІЗЧ\x001DА241002\x001DЯЕу5м2бюьПТ\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode5 = "ц№Л\006\x001D9Х111003592770\x001D1Ш3нкУ№В9\x001DА240304\x001DЯ5т4ЪвшЕъ-Щ\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode6 = "ц№Л\006\x001D9Х111003592770\x001D1ШМтЧ%еVЗ\x001DА231031\x001DЯ51сшТюЪжкЬ\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode7 = "ц№Л\006\x001D9Х111003592770\x001D1ШфЬ1гч4/\x001DА231214\x001DЯшРЙъЭаА0,!\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode8 = "ц№Л\006\x001D9Х111003592770\x001D1ШГЪщНпФ6\x001DА240620\x001DЯяИЖлсЩйХзо\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode9 = "ц№Л\006\x001D9Х111003592770\x001D1ШрБэСжШЯ\x001DА240218\x001DЯ_ЧЗІхО8.9В\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode10 = "ц№Л\006\x001D9Х111003592770\x001D1Шыд0Л7иК\x001DА240703\x001DЯ№Укн3ЗVе%Ч\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode11 = "ц№Л\006\x001D9Х111003592770\x001D1Ш:ЮТПьюб\x001DА231126\x001DЯЧтМЬкжЪюТш\0\x0004\x000D";
    private const string BulgarianTypewriterBarcode12 = "ц№Л\006\x001D9Х111003592770\x001D1Ш2м5уЕЩ-\x001DА230404\x001DЯГСиы9ыеШщы\0\x0004\x000D";
    private const string SwedishBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \x001D    \0    \0    \0    \x000D";
    private const string SwedishDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string SwedishDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\x000D";
    private const string SwedishDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string SwedishDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string SwedishBarcode1 = "å=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4äh1Ab\0\x0004\x000D";
    private const string SwedishBarcode2 = "å=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string SwedishBarcode3 = "å=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string SwedishBarcode4 = "å=:\006\x001D9N111003592770\x001D1T\0´8Fn?PÄ\x001DD241002\x001DSEw5ö2-zaMJ\0\x0004\x000D";
    private const string SwedishBarcode5 = "å=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Y\0\x0004\x000D";
    private const string SwedishBarcode6 = "å=:\006\x001D9N111003592770\x001D1TÖjÄ%e\0`P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string SwedishBarcode7 = "å=:\006\x001D9N111003592770\x001D1TbA1hä4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string SwedishBarcode8 = "å=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string SwedishBarcode9 = "å=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SwedishBarcode10 = "å=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string SwedishBarcode11 = "å=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DSÄjÖAugCzJt\0\x0004\x000D";
    private const string SwedishBarcode12 = "å=:\006\x001D9N111003592770\x001D1T2ö5wEY+\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string SwedishWithSamiBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0    \0    \x000D";
    private const string SwedishWithSamiDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string SwedishWithSamiDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\x000D";
    private const string SwedishWithSamiDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string SwedishWithSamiDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string SwedishWithSamiBarcode1 = "å=:\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4äh1Ab\0\x0004\x000D";
    private const string SwedishWithSamiBarcode2 = "å=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string SwedishWithSamiBarcode3 = "å=:\006\09N111003592770\01T)fpNxYi\0D230405\0SZ/Ur7:0oQS\0\x0004\x000D";
    private const string SwedishWithSamiBarcode4 = "å=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-zaMJ\0\x0004\x000D";
    private const string SwedishWithSamiBarcode5 = "å=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEc+Y\0\x0004\x000D";
    private const string SwedishWithSamiBarcode6 = "å=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string SwedishWithSamiBarcode7 = "å=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string SwedishWithSamiBarcode8 = "å=:\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string SwedishWithSamiBarcode9 = "å=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SwedishWithSamiBarcode10 = "å=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string SwedishWithSamiBarcode11 = "å=:\006\09N111003592770\01T/ZJMaz-\0D231126\0SÄjÖAugCzJt\0\x0004\x000D";
    private const string SwedishWithSamiBarcode12 = "å=:\006\09N111003592770\01T2ö5wEY+\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string GreekBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 \0¨\0΄< = > ? Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ _ α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \x000D";
    private const string GreekDeadKey1 = "\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨.\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨`\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨~\x000D";
    private const string GreekDeadKey2 = "\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄.\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄`\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄~\x000D";
    private const string GreekDeadKey3 = "\0΅!\0΅\"\0΅#\0΅$\0΅%\0΅&\0΅'\0΅(\0΅)\0΅*\0΅+\0΅,\0΅-\0΅.\0΅/\0΅0\0΅1\0΅2\0΅3\0΅4\0΅5\0΅6\0΅7\0΅8\0΅9\0΅¨\0΅΄\0΅<\0΅=\0΅>\0΅?\0΅@\0΅Α\0΅Β\0΅Ψ\0΅Δ\0΅Ε\0΅Φ\0΅Γ\0΅Η\0΅Ι\0΅Ξ\0΅Κ\0΅Λ\0΅Μ\0΅Ν\0΅Ο\0΅Π\0΅:\0΅Ρ\0΅Σ\0΅Τ\0΅Θ\0΅Ω\0΅΅\0΅Χ\0΅Υ\0΅Ζ\0΅[\0΅\\\0΅]\0΅^\0΅_\0΅`\0΅α\0΅β\0΅ψ\0΅δ\0΅ε\0΅φ\0΅γ\0΅η\0ΐ\0΅ξ\0΅κ\0΅λ\0΅μ\0΅ν\0΅ο\0΅π\0΅;\0΅ρ\0΅σ\0΅τ\0΅θ\0΅ω\0΅ς\0΅χ\0ΰ\0΅ζ\0΅{\0΅|\0΅}\0΅~\x000D";
    private const string GreekBarcode1 = "[)>\006\x001D9Ν111003592770\x001D1ΤΔδΩψΧ<τ\x001DΔ230207\x001DΣυΨΗ*4'η1Αβ\0\x0004\x000D";
    private const string GreekBarcode2 = "[)>\006\x001D9Ν111003592770\x001D1Τ.ΓΡσ!;Ο\x001DΔ230729\x001DΣΤγΙω?,6ΒμΚ\0\x0004\x000D";
    private const string GreekBarcode3 = "[)>\006\x001D9Ν111003592770\x001D1Τ(φπΝχΥι\x001DΔ230405\x001DΣΖ&Θρ7>0ο:Σ\0\x0004\x000D";
    private const string GreekBarcode4 = "[)>\006\x001D9Ν111003592770\x001D1Τ=8Φν_Π\"\x001DΔ241002\x001DΣΕς5\0΄2/ζαΜΞ\0\x0004\x000D";
    private const string GreekBarcode5 = "[)>\006\x001D9Ν111003592770\x001D1Τ3κθ\0΅)Λ9\x001DΔ240304\x001DΣ5ξ4ΨλτΕψ-Υ\0\x0004\x000D";
    private const string GreekBarcode6 = "[)>\006\x001D9Ν111003592770\x001D1Τ\0¨ξ\"%ε+Π\x001DΔ231031\x001DΣ51ιτΞζΨγθΑ\0\x0004\x000D";
    private const string GreekBarcode7 = "[)>\006\x001D9Ν111003592770\x001D1ΤβΑ1η'4*\x001DΔ231214\x001DΣτ<ΧψΩδΔ0;!\0\x0004\x000D";
    private const string GreekBarcode8 = "[)>\006\x001D9Ν111003592770\x001D1ΤΗΨυΚμΒ6\x001DΔ240620\x001DΣσΡΓ.ιΥχΝπφ\0\x0004\x000D";
    private const string GreekBarcode9 = "[)>\006\x001D9Ν111003592770\x001D1Τ,?ωΙγΤΣ\x001DΔ240218\x001DΣ(\"Π_νΦ8=9Λ\0\x0004\x000D";
    private const string GreekBarcode10 = "[)>\006\x001D9Ν111003592770\x001D1Τ:ο0>7ρΘ\x001DΔ240703\x001DΣ)\0΅θκ3Π+ε%\"\0\x0004\x000D";
    private const string GreekBarcode11 = "[)>\006\x001D9Ν111003592770\x001D1Τ&ΖΞΜαζ/\x001DΔ231126\x001DΣ\"ξ\0¨ΑθγΨζΞτ\0\x0004\x000D";
    private const string GreekBarcode12 = "[)>\006\x001D9Ν111003592770\x001D1Τ2\0΄5ςΕΥ-\x001DΔ230404\x001DΣΗΙρ:9:εΤυ:\0\x0004\x000D";
    private const string Greek220Baseline = "  ! \0΅% / \0¨) = ( [ , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; ] : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω ~ Χ Υ Ζ ? α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   £ $ \" + # } & ½ * @ { ±    \x001D    \0    \0    \0    \x000D";
    private const string Greek319Baseline = "  ! ‘ % / ’ ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; + : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π ― Ρ Σ Τ Θ Ω ¦ Χ Υ Ζ ° α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π · ρ σ τ θ ω ς χ υ ζ   £ $ \" [ ² ] ¬ ½ « ³ » ±    \x001D    \x001C    \0    \0    \x000D";
    private const string Greek319DeadKey1 = "\0¨!\0¨‘\0¨£\0¨$\0¨%\0¨/\0¨’\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨;\0¨+\0¨:\0¨_\0¨\"\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨―\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨¦\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨²\0¨]\0¨¬\0¨°\0¨½\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨·\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨«\0¨³\0¨»\0¨±\x000D";
    private const string Greek319DeadKey2 = "\0΄!\0΄‘\0΄£\0΄$\0΄%\0΄/\0΄’\0΄)\0΄=\0΄(\0΄*\0΄,\0΄'\0΄.\0΄-\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄;\0΄+\0΄:\0΄_\0΄\"\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄―\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄¦\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄²\0΄]\0΄¬\0΄°\0΄½\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄·\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄«\0΄³\0΄»\0΄±\x000D";
    private const string Greek319Barcode1 = "[=:\006\x001D9Ν111003592770\x001D1ΤΔδΩψΧ;τ\x001DΔ230207\x001DΣυΨΗ(4’η1Αβ\0\x0004\x000D";
    private const string Greek319Barcode2 = "[=:\006\x001D9Ν111003592770\x001D1Τ.ΓΡσ!·Ο\x001DΔ230729\x001DΣΤγΙω_,6ΒμΚ\0\x0004\x000D";
    private const string Greek319Barcode3 = "[=:\006\x001D9Ν111003592770\x001D1Τ)φπΝχΥι\x001DΔ230405\x001DΣΖ/Θρ7:0ο―Σ\0\x0004\x000D";
    private const string Greek319Barcode4 = "[=:\006\x001D9Ν111003592770\x001D1Τ+8Φν°Π‘\x001DΔ241002\x001DΣΕς5\0΄2-ζαΜΞ\0\x0004\x000D";
    private const string Greek319Barcode5 = "[=:\006\x001D9Ν111003592770\x001D1Τ3κθ¦=Λ9\x001DΔ240304\x001DΣ5ξ4ΨλτΕψ'Υ\0\x0004\x000D";
    private const string Greek319Barcode6 = "[=:\006\x001D9Ν111003592770\x001D1Τ\0¨ξ‘%ε*Π\x001DΔ231031\x001DΣ51ιτΞζΨγθΑ\0\x0004\x000D";
    private const string Greek319Barcode7 = "[=:\006\x001D9Ν111003592770\x001D1ΤβΑ1η’4(\x001DΔ231214\x001DΣτ;ΧψΩδΔ0·!\0\x0004\x000D";
    private const string Greek319Barcode8 = "[=:\006\x001D9Ν111003592770\x001D1ΤΗΨυΚμΒ6\x001DΔ240620\x001DΣσΡΓ.ιΥχΝπφ\0\x0004\x000D";
    private const string Greek319Barcode9 = "[=:\006\x001D9Ν111003592770\x001D1Τ,_ωΙγΤΣ\x001DΔ240218\x001DΣ)‘Π°νΦ8+9Λ\0\x0004\x000D";
    private const string Greek319Barcode10 = "[=:\006\x001D9Ν111003592770\x001D1Τ―ο0:7ρΘ\x001DΔ240703\x001DΣ=¦θκ3Π*ε%‘\0\x0004\x000D";
    private const string Greek319Barcode11 = "[=:\006\x001D9Ν111003592770\x001D1Τ/ΖΞΜαζ-\x001DΔ231126\x001DΣ‘ξ\0¨ΑθγΨζΞτ\0\x0004\x000D";
    private const string Greek319Barcode12 = "[=:\006\x001D9Ν111003592770\x001D1Τ2\0΄5ςΕΥ'\x001DΔ230404\x001DΣΗΙρ―9―εΤυ―\0\x0004\x000D";
    private const string GreekPolytonicBaseline = "  ! \0\"% & \0'( ) * \0+, \0-. \0/0 1 2 3 4 5 6 7 8 9 \0¨\0΄< \0=> \0?Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π \0:Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ \0_α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π \0;ρ σ τ θ ω ς χ υ ζ   # $ @ \0[\0\\\0]^ \0~\0{\0|\0}\0`   \x001D    \x001C    \0    \0    \x000D";
    private const string GreekPolytonicDeadKey1 = "\0\"!\0\"\"\0\"#\0\"$\0\"%\0\"&\0\"'\0\"(\0\")\0\"*\0\"+\0\",\0\"-\0῾\0\"/\0\"0\0\"1\0\"2\0\"3\0\"4\0\"5\0\"6\0\"7\0\"8\0\"9\0\"¨\0\"΄\0\"<\0\"=\0\">\0\"?\0\"@\0Ἁ\0\"Β\0\"Ψ\0\"Δ\0Ἑ\0\"Φ\0\"Γ\0Ἡ\0Ἱ\0\"Ξ\0\"Κ\0\"Λ\0\"Μ\0\"Ν\0Ὁ\0\"Π\0\":\0Ῥ\0\"Σ\0\"Τ\0\"Θ\0Ὡ\0\"΅\0\"Χ\0Ὑ\0\"Ζ\0\"[\0\"\\\0\"]\0\"^\0\"_\0\"~\0ἁ\0\"β\0\"ψ\0\"δ\0ἑ\0\"φ\0\"γ\0ἡ\0ἱ\0\"ξ\0\"κ\0\"λ\0\"μ\0\"ν\0ὁ\0\"π\0\";\0ῥ\0\"σ\0\"τ\0\"θ\0ὡ\0\"ς\0\"χ\0ὑ\0\"ζ\0\"{\0\"|\0\"}\0\"`\x000D";
    private const string GreekPolytonicDeadKey2 = "\0'!\0'\"\0'#\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0᾿\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0'¨\0'΄\0'<\0'=\0'>\0'?\0'@\0Ἀ\0'Β\0'Ψ\0'Δ\0Ἐ\0'Φ\0'Γ\0Ἠ\0Ἰ\0'Ξ\0'Κ\0'Λ\0'Μ\0'Ν\0Ὀ\0'Π\0':\0'Ρ\0'Σ\0'Τ\0'Θ\0Ὠ\0'΅\0'Χ\0'Υ\0'Ζ\0'[\0'\\\0']\0'^\0'_\0'~\0ἀ\0'β\0'ψ\0'δ\0ἐ\0'φ\0'γ\0ἠ\0ἰ\0'ξ\0'κ\0'λ\0'μ\0'ν\0ὀ\0'π\0';\0ῤ\0'σ\0'τ\0'θ\0ὠ\0'ς\0'χ\0ὐ\0'ζ\0'{\0'|\0'}\0'`\x000D";
    private const string GreekPolytonicDeadKey3 = "\0+!\0+\"\0+#\0+$\0+%\0+&\0+'\0+(\0+)\0+*\0++\0+,\0+-\0῟\0+/\0+0\0+1\0+2\0+3\0+4\0+5\0+6\0+7\0+8\0+9\0+¨\0+΄\0+<\0+=\0+>\0+?\0+@\0Ἇ\0+Β\0+Ψ\0+Δ\0+Ε\0+Φ\0+Γ\0Ἧ\0Ἷ\0+Ξ\0+Κ\0+Λ\0+Μ\0+Ν\0+Ο\0+Π\0+:\0+Ρ\0+Σ\0+Τ\0+Θ\0Ὧ\0+΅\0+Χ\0Ὗ\0+Ζ\0+[\0+\\\0+]\0+^\0+_\0+~\0ἇ\0+β\0+ψ\0+δ\0+ε\0+φ\0+γ\0ἧ\0ἷ\0+ξ\0+κ\0+λ\0+μ\0+ν\0+ο\0+π\0+;\0+ρ\0+σ\0+τ\0+θ\0ὧ\0+ς\0+χ\0ὗ\0+ζ\0+{\0+|\0+}\0+`\x000D";
    private const string GreekPolytonicDeadKey4 = "\0-!\0-\"\0-#\0-$\0-%\0-&\0-'\0-(\0-)\0-*\0-+\0-,\0--\0¯\0-/\0-0\0-1\0-2\0-3\0-4\0-5\0-6\0-7\0-8\0-9\0-¨\0-΄\0-<\0-=\0->\0-?\0-@\0Ᾱ\0-Β\0-Ψ\0-Δ\0-Ε\0-Φ\0-Γ\0-Η\0Ῑ\0-Ξ\0-Κ\0-Λ\0-Μ\0-Ν\0-Ο\0-Π\0-:\0-Ρ\0-Σ\0-Τ\0-Θ\0-Ω\0-΅\0-Χ\0Ῡ\0-Ζ\0-[\0-\\\0-]\0-^\0-_\0-~\0ᾱ\0-β\0-ψ\0-δ\0-ε\0-φ\0-γ\0-η\0ῑ\0-ξ\0-κ\0-λ\0-μ\0-ν\0-ο\0-π\0-;\0-ρ\0-σ\0-τ\0-θ\0-ω\0-ς\0-χ\0ῡ\0-ζ\0-{\0-|\0-}\0-`\x000D";
    private const string GreekPolytonicDeadKey5 = "\0/!\0/\"\0/#\0/$\0/%\0/&\0/'\0/(\0/)\0/*\0/+\0/,\0/-\0῎\0//\0/0\0/1\0/2\0/3\0/4\0/5\0/6\0/7\0/8\0/9\0/¨\0/΄\0/<\0/=\0/>\0/?\0/@\0Ἄ\0/Β\0/Ψ\0/Δ\0Ἔ\0/Φ\0/Γ\0Ἤ\0Ἴ\0/Ξ\0/Κ\0/Λ\0/Μ\0/Ν\0Ὄ\0/Π\0/:\0/Ρ\0/Σ\0/Τ\0/Θ\0Ὤ\0/΅\0/Χ\0/Υ\0/Ζ\0/[\0/\\\0/]\0/^\0/_\0/~\0ἄ\0/β\0/ψ\0/δ\0ἔ\0/φ\0/γ\0ἤ\0ἴ\0/ξ\0/κ\0/λ\0/μ\0/ν\0ὄ\0/π\0/;\0/ρ\0/σ\0/τ\0/θ\0ὤ\0/ς\0/χ\0ὔ\0/ζ\0/{\0/|\0/}\0/`\x000D";
    private const string GreekPolytonicDeadKey6 = "\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨~\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨`\x000D";
    private const string GreekPolytonicDeadKey7 = "\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄~\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄`\x000D";
    private const string CzechBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú \0¨) 6 ; / ' ( \0°   \x001D    \x001C    \0    \0    \x000D";
    private const string CzechDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ'\0ˇ(\0ˇ°\x000D";
    private const string CzechDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´/\0´'\0´(\0´°\x000D";
    private const string CzechDeadKey3 = "\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0Ÿ\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨/\0¨'\0¨(\0¨°\x000D";
    private const string CzechDeadKey4 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Z\0°Y\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°z\0°y\0°/\0°'\0°(\0°°\x000D";
    private const string CzechQwertyBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú \0¨) 6 ; / ' ( \0°   \x001B    \x001C    \x001E    \x001F    \x000D";
    private const string CzechQwertyDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ'\0ˇ(\0ˇ°\x000D";
    private const string CzechQwertyDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´'\0´(\0´°\x000D";
    private const string CzechQwertyDeadKey3 = "\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0Ÿ\0¨Z\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨/\0¨'\0¨(\0¨°\x000D";
    private const string CzechQwertyDeadKey4 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°'\0°(\0°°\x000D";
    private const string CzechQwertyBarcode1 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+TDdVcX?t\x001BDěšéěéý\x001BSyCH8č§h+Ab\x001E\x0004\x000D";
    private const string CzechQwertyBarcode2 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T.GRs1qO\x001BDěšéýěí\x001BSTgIv_,žBmK\x001E\x0004\x000D";
    private const string CzechQwertyBarcode3 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T9fpNxYi\x001BDěšéčéř\x001BSZ7Urý:éoQS\x001E\x0004\x000D";
    private const string CzechQwertyBarcode4 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T\0´áFn%P!\x001BDěč+ééě\x001BSEwřůě-zaMJ\x001E\x0004\x000D";
    private const string CzechQwertyBarcode5 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+TškuW0Lí\x001BDěčéšéč\x001BSřjčCltEc=Y\x001E\x0004\x000D";
    private const string CzechQwertyBarcode6 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T\"j!5e\0ˇP\x001BDěš+éš+\x001BSř+itJzCguA\x001E\x0004\x000D";
    private const string CzechQwertyBarcode7 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+TbA+h§č8\x001BDěš+ě+č\x001BSt?XcVdDéq1\x001E\x0004\x000D";
    private const string CzechQwertyBarcode8 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+THCyKmBž\x001BDěčéžěé\x001BSsRG.iYxNpf\x001E\x0004\x000D";
    private const string CzechQwertyBarcode9 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T,_vIgTS\x001BDěčéě+á\x001BS9!P%nFá\0´íL\x001E\x0004\x000D";
    private const string CzechQwertyBarcode10 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+TQoé:ýrU\x001BDěčéýéš\x001BS0WukšP\0ě5!\x001E\x0004\x000D";
    private const string CzechQwertyBarcode11 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+T7ZJMaz-\x001BDěš++ěž\x001BS!j\"AugCzJt\x001E\x0004\x000D";
    private const string CzechQwertyBarcode12 = "ú0:\x001Eéž\x001BíN+++ééšříěýýé\x001B+TěůřwEY=\x001BDěšéčéč\x001BSHIrQíQeTyQ\x001E\x0004\x000D";
    private const string CzechProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001B    \x001C    \x001E    \x001F    \x000D";
    private const string CzechProgrammersBarcode1 = "[)>\x001E06\x001B9N111003592770\x001B1TDdVcX<t\x001BD230207\x001BSyCH*4'h1Ab\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode2 = "[)>\x001E06\x001B9N111003592770\x001B1T.GRs!qO\x001BD230729\x001BSTgIv?,6BmK\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode3 = "[)>\x001E06\x001B9N111003592770\x001B1T(fpNxYi\x001BD230405\x001BSZ&Ur7>0oQS\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode4 = "[)>\x001E06\x001B9N111003592770\x001B1T=8Fn_P\"\x001BD241002\x001BSEw5;2/zaMJ\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode5 = "[)>\x001E06\x001B9N111003592770\x001B1T3kuW)L9\x001BD240304\x001BS5j4CltEc-Y\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode6 = "[)>\x001E06\x001B9N111003592770\x001B1T:j\"%e+P\x001BD231031\x001BS51itJzCguA\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode7 = "[)>\x001E06\x001B9N111003592770\x001B1TbA1h'4*\x001BD231214\x001BSt<XcVdD0q!\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode8 = "[)>\x001E06\x001B9N111003592770\x001B1THCyKmB6\x001BD240620\x001BSsRG.iYxNpf\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode9 = "[)>\x001E06\x001B9N111003592770\x001B1T,?vIgTS\x001BD240218\x001BS(\"P_nF8=9L\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode10 = "[)>\x001E06\x001B9N111003592770\x001B1TQo0>7rU\x001BD240703\x001BS)Wuk3P+e%\"\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode11 = "[)>\x001E06\x001B9N111003592770\x001B1T&ZJMaz/\x001BD231126\x001BS\"j:AugCzJt\x001E\x0004\x000D";
    private const string CzechProgrammersBarcode12 = "[)>\x001E06\x001B9N111003592770\x001B1T2;5wEY-\x001BD230404\x001BSHIrQ9QeTyQ\x001E\x0004\x000D";
    private const string DutchBaseline = "  ! \0`% _ \0´) ' ( \0~, / . - 0 1 2 3 4 5 6 7 8 9 ± + ; ° : = A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" \0¨< * & @ \0^> | §    \0    \0    \0    \x001C    \x000D";
    private const string DutchDeadKey1 = "\0`!\0``\0`#\0`$\0`%\0`_\0`´\0`)\0`'\0`(\0`~\0`,\0`/\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`±\0`+\0`;\0`°\0`:\0`=\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`¨\0`<\0`*\0`&\0`?\0`@\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`>\0`|\0`§\x000D";
    private const string DutchDeadKey2 = "\0´!\0´`\0´#\0´$\0´%\0´_\0´´\0´)\0´'\0´(\0´~\0´,\0´/\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´±\0´+\0´;\0´°\0´:\0´=\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´¨\0´<\0´*\0´&\0´?\0´@\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´>\0´|\0´§\x000D";
    private const string DutchDeadKey3 = "\0~!\0~`\0~#\0~$\0~%\0~_\0~´\0~)\0~'\0~(\0~~\0~,\0~/\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~±\0~+\0~;\0~°\0~:\0~=\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~¨\0~<\0~*\0~&\0~?\0~@\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~^\0~>\0~|\0~§\x000D";
    private const string DutchDeadKey4 = "\0¨!\0¨`\0¨#\0¨$\0¨%\0¨_\0¨´\0¨)\0¨'\0¨(\0¨~\0¨,\0¨/\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨±\0¨+\0¨;\0¨°\0¨:\0¨=\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨¨\0¨<\0¨*\0¨&\0¨?\0¨@\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨>\0¨|\0¨§\x000D";
    private const string DutchDeadKey5 = "\0^!\0^`\0^#\0^$\0^%\0^_\0^´\0^)\0^'\0^(\0^~\0^,\0^/\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^±\0^+\0^;\0^°\0^:\0^=\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^¨\0^<\0^*\0^&\0^?\0^@\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^>\0^|\0^§\x000D";
    private const string DutchBarcode1 = "\0¨':\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4\0´h1Ab\0\x0004\x000D";
    private const string DutchBarcode2 = "\0¨':\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv=,6BmK\0\x0004\x000D";
    private const string DutchBarcode3 = "\0¨':\006\09N111003592770\01T)fpNxYi\0D230405\0SZ_Ur7:0oQS\0\x0004\x000D";
    private const string DutchBarcode4 = "\0¨':\006\09N111003592770\01T°8Fn?P\0\0`D241002\0SEw5+2-zaMJ\0\x0004\x000D";
    private const string DutchBarcode5 = "\0¨':\006\09N111003592770\01T3kuW'L9\0D240304\0S5j4CltEc/Y\0\x0004\x000D";
    private const string DutchBarcode6 = "\0¨':\006\09N111003592770\01T±j\0`%e\0~P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string DutchBarcode7 = "\0¨':\006\09N111003592770\01TbA1h\0´4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string DutchBarcode8 = "\0¨':\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string DutchBarcode9 = "\0¨':\006\09N111003592770\01T,=vIgTS\0D240218\0S)\0`P?nF8°9L\0\x0004\x000D";
    private const string DutchBarcode10 = "\0¨':\006\09N111003592770\01TQo0:7rU\0D240703\0S'Wuk3P\0~e%\0\0\x0004`";
    private const string DutchBarcode11 = "\0¨':\006\09N111003592770\01T_ZJMaz-\0D231126\0S\0`j±AugCzJt\0\x0004\x000D";
    private const string DutchBarcode12 = "\0¨':\006\09N111003592770\01T2+5wEY/\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string EstonianBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" ü ' õ & \0ˇÜ * Õ \0~   \0    \0    \0    \0    \x000D";
    private const string EstonianDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`ü\0`'\0`õ\0`&\0`?\0`ˇ\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Ü\0`*\0`Õ\0`~\x000D";
    private const string EstonianDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0´I\0´J\0´K\0´L\0´M\0Ń\0Ó\0´P\0´Q\0´R\0Ś\0´T\0´U\0´V\0´W\0´X\0´Y\0Ź\0´ü\0´'\0´õ\0´&\0´?\0´ˇ\0´a\0´b\0ć\0´d\0é\0´f\0´g\0´h\0´i\0´j\0´k\0´l\0´m\0ń\0ó\0´p\0´q\0´r\0ś\0´t\0´u\0´v\0´w\0´x\0´y\0ź\0´Ü\0´*\0´Õ\0´~\x000D";
    private const string EstonianDeadKey3 = "\0ˇ!\0ˇÄ\0ˇ#\0ˇ¤\0ˇ%\0ˇ/\0ˇä\0ˇ)\0ˇ=\0ˇ(\0ˇ`\0ˇ,\0ˇ+\0ˇ.\0ˇ-\0ˇ0\0ˇ1\0ˇ2\0ˇ3\0ˇ4\0ˇ5\0ˇ6\0ˇ7\0ˇ8\0ˇ9\0ˇÖ\0ˇö\0ˇ;\0ˇ´\0ˇ:\0ˇ_\0ˇ\"\0ˇA\0ˇB\0Č\0ˇD\0ˇE\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0ˇL\0ˇM\0ˇN\0ˇO\0ˇP\0ˇQ\0ˇR\0Š\0ˇT\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇü\0ˇ'\0ˇõ\0ˇ&\0ˇ?\0ˇˇ\0ˇa\0ˇb\0č\0ˇd\0ˇe\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ˇl\0ˇm\0ˇn\0ˇo\0ˇp\0ˇq\0ˇr\0š\0ˇt\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇÜ\0ˇ*\0ˇÕ\0ˇ~\x000D";
    private const string EstonianDeadKey4 = "\0~!\0~Ä\0~#\0~¤\0~%\0~/\0~ä\0~)\0~=\0~(\0~`\0~,\0~+\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ö\0~ö\0~;\0~´\0~:\0~_\0~\"\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~ü\0~'\0~õ\0~&\0~?\0~ˇ\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~Ü\0~*\0~Õ\0~~\x000D";
    private const string EstonianBarcode1 = "ü=:\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4äh1Ab\0\x0004\x000D";
    private const string EstonianBarcode2 = "ü=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string EstonianBarcode3 = "ü=:\006\09N111003592770\01T)fpNxYi\0D230405\0SZ/Ur7:0oQS\0\x0004\x000D";
    private const string EstonianBarcode4 = "ü=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-zaMJ\0\x0004\x000D";
    private const string EstonianBarcode5 = "ü=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEc+Y\0\x0004\x000D";
    private const string EstonianBarcode6 = "ü=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string EstonianBarcode7 = "ü=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string EstonianBarcode8 = "ü=:\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string EstonianBarcode9 = "ü=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string EstonianBarcode10 = "ü=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string EstonianBarcode11 = "ü=:\006\09N111003592770\01T/ZJMaz-\0D231126\0SÄjÖAugCzJt\0\x0004\x000D";
    private const string EstonianBarcode12 = "ü=:\006\09N111003592770\01T2ö5wEY+\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string FinnishBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \x001D    \0    \0    \0    \x000D";
    private const string FinnishDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string FinnishDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\x000D";
    private const string FinnishDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string FinnishDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string FinnishBarcode1 = "å=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4äh1Ab\0\x0004\x000D";
    private const string FinnishBarcode2 = "å=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string FinnishBarcode3 = "å=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string FinnishBarcode4 = "å=:\006\x001D9N111003592770\x001D1T\0´8Fn?PÄ\x001DD241002\x001DSEw5ö2-zaMJ\0\x0004\x000D";
    private const string FinnishBarcode5 = "å=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Y\0\x0004\x000D";
    private const string FinnishBarcode6 = "å=:\006\x001D9N111003592770\x001D1TÖjÄ%e\0`P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string FinnishBarcode7 = "å=:\006\x001D9N111003592770\x001D1TbA1hä4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string FinnishBarcode8 = "å=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string FinnishBarcode9 = "å=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string FinnishBarcode10 = "å=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string FinnishBarcode11 = "å=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DSÄjÖAugCzJt\0\x0004\x000D";
    private const string FinnishBarcode12 = "å=:\006\x001D9N111003592770\x001D1T2ö5wEY+\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string FinnishWithSamiBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0    \0    \x000D";
    private const string FinnishWithSamiDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string FinnishWithSamiDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\x000D";
    private const string FinnishWithSamiDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string FinnishWithSamiDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string FinnishWithSamiBarcode1 = "å=:\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4äh1Ab\0\x0004\x000D";
    private const string FinnishWithSamiBarcode2 = "å=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string FinnishWithSamiBarcode3 = "å=:\006\09N111003592770\01T)fpNxYi\0D230405\0SZ/Ur7:0oQS\0\x0004\x000D";
    private const string FinnishWithSamiBarcode4 = "å=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-zaMJ\0\x0004\x000D";
    private const string FinnishWithSamiBarcode5 = "å=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEc+Y\0\x0004\x000D";
    private const string FinnishWithSamiBarcode6 = "å=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string FinnishWithSamiBarcode7 = "å=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string FinnishWithSamiBarcode8 = "å=:\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string FinnishWithSamiBarcode9 = "å=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string FinnishWithSamiBarcode10 = "å=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string FinnishWithSamiBarcode11 = "å=:\006\09N111003592770\01T/ZJMaz-\0D231126\0SÄjÖAugCzJt\0\x0004\x000D";
    private const string FinnishWithSamiBarcode12 = "å=:\006\09N111003592770\01T2ö5wEY+\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string GermanBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \x001D    \x001C    \0    \0    \x000D";
    private const string GermanDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string GermanDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\x000D";
    private const string GermanDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string GermanBarcode1 = "ü=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4äh1Ab\0\x0004\x000D";
    private const string GermanBarcode2 = "ü=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string GermanBarcode3 = "ü=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string GermanBarcode4 = "ü=:\006\x001D9N111003592770\x001D1T\0´8Fn?PÄ\x001DD241002\x001DSEw5ö2-yaMJ\0\x0004\x000D";
    private const string GermanBarcode5 = "ü=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEcßZ\0\x0004\x000D";
    private const string GermanBarcode6 = "ü=:\006\x001D9N111003592770\x001D1TÖjÄ%e\0`P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string GermanBarcode7 = "ü=:\006\x001D9N111003592770\x001D1TbA1hä4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string GermanBarcode8 = "ü=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string GermanBarcode9 = "ü=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string GermanBarcode10 = "ü=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string GermanBarcode11 = "ü=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSÄjÖAugCyJt\0\x0004\x000D";
    private const string GermanBarcode12 = "ü=:\006\x001D9N111003592770\x001D1T2ö5wEZß\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string GermanIbmBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \x001D    \x001C    \0    \0    \x000D";
    private const string GermanIbmDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string GermanIbmDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\x000D";
    private const string GermanIbmDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string GermanIbmBarcode1 = "ü=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4äh1Ab\0\x0004\x000D";
    private const string GermanIbmBarcode2 = "ü=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string GermanIbmBarcode3 = "ü=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string GermanIbmBarcode4 = "ü=:\006\x001D9N111003592770\x001D1T\0´8Fn?PÄ\x001DD241002\x001DSEw5ö2-yaMJ\0\x0004\x000D";
    private const string GermanIbmBarcode5 = "ü=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEcßZ\0\x0004\x000D";
    private const string GermanIbmBarcode6 = "ü=:\006\x001D9N111003592770\x001D1TÖjÄ%e\0`P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string GermanIbmBarcode7 = "ü=:\006\x001D9N111003592770\x001D1TbA1hä4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string GermanIbmBarcode8 = "ü=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string GermanIbmBarcode9 = "ü=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string GermanIbmBarcode10 = "ü=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string GermanIbmBarcode11 = "ü=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSÄjÖAugCyJt\0\x0004\x000D";
    private const string GermanIbmBarcode12 = "ü=:\006\x001D9N111003592770\x001D1T2ö5wEZß\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string SwissGermanBaseline = "  + à % / ä ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 é ö ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" ü $ \0¨& § è £ ! °    \x001D    \x001C    \0    \0    \x000D";
    private const string SwissGermanDeadKey1 = "\0`+\0`à\0`*\0`ç\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`é\0`ö\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`è\0`£\0`!\0`°\x000D";
    private const string SwissGermanDeadKey2 = "\0^+\0^à\0^*\0^ç\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^é\0^ö\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^è\0^£\0^!\0^°\x000D";
    private const string SwissGermanDeadKey3 = "\0¨+\0¨à\0¨*\0¨ç\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨é\0¨ö\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨ü\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨è\0¨£\0¨!\0¨°\x000D";
    private const string SwissGermanBarcode1 = "ü=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4äh1Ab\0\x0004\x000D";
    private const string SwissGermanBarcode2 = "ü=:\006\x001D9N111003592770\x001D1T.GRs+qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string SwissGermanBarcode3 = "ü=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string SwissGermanBarcode4 = "ü=:\006\x001D9N111003592770\x001D1T\0^8Fn?Pà\x001DD241002\x001DSEw5ö2-yaMJ\0\x0004\x000D";
    private const string SwissGermanBarcode5 = "ü=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Z\0\x0004\x000D";
    private const string SwissGermanBarcode6 = "ü=:\006\x001D9N111003592770\x001D1Téjà%e\0`P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string SwissGermanBarcode7 = "ü=:\006\x001D9N111003592770\x001D1TbA1hä4(\x001DD231214\x001DSt;XcVdD0q+\0\x0004\x000D";
    private const string SwissGermanBarcode8 = "ü=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string SwissGermanBarcode9 = "ü=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)àP?nF8\0^9L\0\x0004\x000D";
    private const string SwissGermanBarcode10 = "ü=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%à\0\x0004\x000D";
    private const string SwissGermanBarcode11 = "ü=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSàjéAugCyJt\0\x0004\x000D";
    private const string SwissGermanBarcode12 = "ü=:\006\x001D9N111003592770\x001D1T2ö5wEZ'\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string DanishBaseline = "  ! Ø % / ø ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Æ æ ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& ½ Å * \0^§    \x001D    \0    \0    \0    \x000D";
    private const string DanishDeadKey1 = "\0`!\0`Ø\0`#\0`¤\0`%\0`/\0`ø\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Æ\0`æ\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`½\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string DanishDeadKey2 = "\0´!\0´Ø\0´#\0´¤\0´%\0´/\0´ø\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´½\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´§\x000D";
    private const string DanishDeadKey3 = "\0¨!\0¨Ø\0¨#\0¨¤\0¨%\0¨/\0¨ø\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨½\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string DanishDeadKey4 = "\0^!\0^Ø\0^#\0^¤\0^%\0^/\0^ø\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Æ\0^æ\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^½\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string DanishBarcode1 = "å=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4øh1Ab\0\x0004\x000D";
    private const string DanishBarcode2 = "å=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string DanishBarcode3 = "å=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string DanishBarcode4 = "å=:\006\x001D9N111003592770\x001D1T\0´8Fn?PØ\x001DD241002\x001DSEw5æ2-zaMJ\0\x0004\x000D";
    private const string DanishBarcode5 = "å=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Y\0\x0004\x000D";
    private const string DanishBarcode6 = "å=:\006\x001D9N111003592770\x001D1TÆjØ%e\0`P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string DanishBarcode7 = "å=:\006\x001D9N111003592770\x001D1TbA1hø4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string DanishBarcode8 = "å=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string DanishBarcode9 = "å=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ØP?nF8\0´9L\0\x0004\x000D";
    private const string DanishBarcode10 = "å=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Ø\0\x0004\x000D";
    private const string DanishBarcode11 = "å=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DSØjÆAugCzJt\0\x0004\x000D";
    private const string DanishBarcode12 = "å=:\006\x001D9N111003592770\x001D1T2æ5wEY+\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string HungarianBaseline = "  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y Ü a b c d e f g h i j k l m n o p q r s t u v w x z y   + ! \" ő ű ú / 0 Ő Ű Ú §    \x001D    \x001C    \0    \0    \x000D";
    private const string HungarianBarcode1 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TDdVcX?t\x001DD23ö2ö7\x001DSzCH(4áh1Ab\0\x0004\x000D";
    private const string HungarianBarcode2 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T.GRs'qO\x001DD23ö729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string HungarianBarcode3 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T)fpNxZi\x001DD23ö4ö5\x001DSY=Ur7:öoQS\0\x0004\x000D";
    private const string HungarianBarcode4 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1Tó8FnÜPÁ\x001DD241öö2\x001DSEw5é2-yaMJ\0\x0004\x000D";
    private const string HungarianBarcode5 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T3kuWÖL9\x001DD24ö3ö4\x001DS5j4CltEcüZ\0\x0004\x000D";
    private const string HungarianBarcode6 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TÉjÁ%eÓP\x001DD231ö31\x001DS51itJyCguA\0\x0004\x000D";
    private const string HungarianBarcode7 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TbA1há4(\x001DD231214\x001DSt?XcVdDöq'\0\x0004\x000D";
    private const string HungarianBarcode8 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1THCzKmB6\x001DD24ö62ö\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string HungarianBarcode9 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T,_vIgTS\x001DD24ö218\x001DS)ÁPÜnF8ó9L\0\x0004\x000D";
    private const string HungarianBarcode10 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TQoö:7rU\x001DD24ö7ö3\x001DSÖWuk3PÓe%Á\0\x0004\x000D";
    private const string HungarianBarcode11 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T=YJMay-\x001DD231126\x001DSÁjÉAugCyJt\0\x0004\x000D";
    private const string HungarianBarcode12 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T2é5wEZü\x001DD23ö4ö4\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string Hungarian101KeyBaseline = "  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ü a b c d e f g h i j k l m n o p q r s t u v w x y z   + ! \" ő ű ú / í Ő Ű Ú Í    \x001D    \x001C    \0    \0    \x000D";
    private const string Hungarian101KeyBarcode1 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TDdVcX?t\x001DD23ö2ö7\x001DSyCH(4áh1Ab\0\x0004\x000D";
    private const string Hungarian101KeyBarcode2 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T.GRs'qO\x001DD23ö729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string Hungarian101KeyBarcode3 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T)fpNxYi\x001DD23ö4ö5\x001DSZ=Ur7:öoQS\0\x0004\x000D";
    private const string Hungarian101KeyBarcode4 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1Tó8FnÜPÁ\x001DD241öö2\x001DSEw5é2-zaMJ\0\x0004\x000D";
    private const string Hungarian101KeyBarcode5 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T3kuWÖL9\x001DD24ö3ö4\x001DS5j4CltEcüY\0\x0004\x000D";
    private const string Hungarian101KeyBarcode6 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TÉjÁ%eÓP\x001DD231ö31\x001DS51itJzCguA\0\x0004\x000D";
    private const string Hungarian101KeyBarcode7 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TbA1há4(\x001DD231214\x001DSt?XcVdDöq'\0\x0004\x000D";
    private const string Hungarian101KeyBarcode8 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1THCyKmB6\x001DD24ö62ö\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string Hungarian101KeyBarcode9 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T,_vIgTS\x001DD24ö218\x001DS)ÁPÜnF8ó9L\0\x0004\x000D";
    private const string Hungarian101KeyBarcode10 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1TQoö:7rU\x001DD24ö7ö3\x001DSÖWuk3PÓe%Á\0\x0004\x000D";
    private const string Hungarian101KeyBarcode11 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T=ZJMaz-\x001DD231126\x001DSÁjÉAugCzJt\0\x0004\x000D";
    private const string Hungarian101KeyBarcode12 = "őÖ:\0ö6\x001D9N111öö359277ö\x001D1T2é5wEYü\x001DD23ö4ö4\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string IcelandicBaseline = "  ! ' % / \0´) = ( _ , ö . þ 0 1 2 3 4 5 6 7 8 9 Æ æ ; - : Þ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ö a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" ð + ' & \0°Ð * ? \0¨   \x001D    \0    \0    \0    \x000D";
    private const string IcelandicDeadKey1 = "\0´!\0´'\0´#\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´_\0´,\0´ö\0´.\0´þ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´-\0´:\0´Þ\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´ð\0´+\0´'\0´&\0´Ö\0´°\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Ð\0´*\0´?\0´¨\x000D";
    private const string IcelandicDeadKey2 = "\0°!\0°'\0°#\0°$\0°%\0°/\0°´\0°)\0°=\0°(\0°_\0°,\0°ö\0°.\0°þ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Æ\0°æ\0°;\0°-\0°:\0°Þ\0°\"\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0°Z\0°ð\0°+\0°'\0°&\0°Ö\0°°\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0°z\0°Ð\0°*\0°?\0°¨\x000D";
    private const string IcelandicDeadKey3 = "\0¨!\0¨'\0¨#\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨_\0¨,\0¨ö\0¨.\0¨þ\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨-\0¨:\0¨Þ\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨ð\0¨+\0¨'\0¨&\0¨Ö\0¨°\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Ð\0¨*\0¨?\0¨¨\x000D";
    private const string IcelandicBarcode1 = "ð=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4\0´h1Ab\0\x0004\x000D";
    private const string IcelandicBarcode2 = "ð=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIvÞ,6BmK\0\x0004\x000D";
    private const string IcelandicBarcode3 = "ð=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string IcelandicBarcode4 = "ð=:\006\x001D9N111003592770\x001D1T-8FnÖP'\x001DD241002\x001DSEw5æ2þzaMJ\0\x0004\x000D";
    private const string IcelandicBarcode5 = "ð=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEcöY\0\x0004\x000D";
    private const string IcelandicBarcode6 = "ð=:\006\x001D9N111003592770\x001D1TÆj'%e_P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string IcelandicBarcode7 = "ð=:\006\x001D9N111003592770\x001D1TbA1h\0´4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string IcelandicBarcode8 = "ð=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string IcelandicBarcode9 = "ð=:\006\x001D9N111003592770\x001D1T,ÞvIgTS\x001DD240218\x001DS)'PÖnF8-9L\0\x0004\x000D";
    private const string IcelandicBarcode10 = "ð=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P_e%'\0\x0004\x000D";
    private const string IcelandicBarcode11 = "ð=:\006\x001D9N111003592770\x001D1T/ZJMazþ\x001DD231126\x001DS'jÆAugCzJt\0\x0004\x000D";
    private const string IcelandicBarcode12 = "ð=:\006\x001D9N111003592770\x001D1T2æ5wEYö\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string IrishBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \x001D    \x001C    \0    \0    \x000D";
    private const string IrishDeadKey1 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0`¬\x000D";
    private const string IrishBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string IrishBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string IrishBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string IrishBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string IrishBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string IrishBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string IrishBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string IrishBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string IrishBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L\0\x0004\x000D";
    private const string IrishBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@\0\x0004\x000D";
    private const string IrishBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt\0\x0004\x000D";
    private const string IrishBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string ItalianBaseline = "  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \x001D    \x001C    \0    \0    \x000D";
    private const string ItalianBarcode1 = "è=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4àh1Ab\0\x0004\x000D";
    private const string ItalianBarcode2 = "è=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string ItalianBarcode3 = "è=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string ItalianBarcode4 = "è=:\006\x001D9N111003592770\x001D1Tì8Fn?P°\x001DD241002\x001DSEw5ò2-zaMJ\0\x0004\x000D";
    private const string ItalianBarcode5 = "è=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Y\0\x0004\x000D";
    private const string ItalianBarcode6 = "è=:\006\x001D9N111003592770\x001D1Tçj°%e^P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string ItalianBarcode7 = "è=:\006\x001D9N111003592770\x001D1TbA1hà4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string ItalianBarcode8 = "è=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string ItalianBarcode9 = "è=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)°P?nF8ì9L\0\x0004\x000D";
    private const string ItalianBarcode10 = "è=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P^e%°\0\x0004\x000D";
    private const string ItalianBarcode11 = "è=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DS°jçAugCzJt\0\x0004\x000D";
    private const string ItalianBarcode12 = "è=:\006\x001D9N111003592770\x001D1T2ò5wEY'\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string Italian142Baseline = "  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \x001D    \x001C    \0    \0    \x000D";
    private const string Italian142Barcode1 = "è=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4àh1Ab\0\x0004\x000D";
    private const string Italian142Barcode2 = "è=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string Italian142Barcode3 = "è=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string Italian142Barcode4 = "è=:\006\x001D9N111003592770\x001D1Tì8Fn?P°\x001DD241002\x001DSEw5ò2-zaMJ\0\x0004\x000D";
    private const string Italian142Barcode5 = "è=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Y\0\x0004\x000D";
    private const string Italian142Barcode6 = "è=:\006\x001D9N111003592770\x001D1Tçj°%e^P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string Italian142Barcode7 = "è=:\006\x001D9N111003592770\x001D1TbA1hà4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string Italian142Barcode8 = "è=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string Italian142Barcode9 = "è=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)°P?nF8ì9L\0\x0004\x000D";
    private const string Italian142Barcode10 = "è=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P^e%°\0\x0004\x000D";
    private const string Italian142Barcode11 = "è=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DS°jçAugCzJt\0\x0004\x000D";
    private const string Italian142Barcode12 = "è=:\006\x001D9N111003592770\x001D1T2ò5wEY'\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string LatvianStandardBaseline = "  ! \0\"% & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \x000D";
    private const string LatvianStandardDeadKey1 = "\0\"!\0\"\"\0\"#\0§\0°\0±\0\"'\0\"(\0\")\0×\0\"+\0\",\0—\0\".\0\"/\0\"0\0\"1\0\"2\0\"3\0§\0°\0\"6\0±\0×\0\"9\0\":\0\";\0\"<\0\"=\0\">\0\"?\0\"@\0Ā\0\"B\0Č\0\"D\0Ē\0\"F\0Ģ\0\"H\0Ī\0\"J\0Ķ\0Ļ\0\"M\0Ņ\0Ō\0\"P\0\"Q\0Ŗ\0Š\0\"T\0Ū\0\"V\0\"W\0\"X\0\"Y\0Ž\0\"[\0\"\\\0\"]\0\"^\0—\0\"`\0Ā\0\"b\0Č\0\"d\0Ē\0\"f\0Ģ\0\"h\0Ī\0\"j\0Ķ\0Ļ\0\"m\0Ņ\0Ō\0\"p\0\"q\0Ŗ\0Š\0\"t\0Ū\0\"v\0\"w\0\"x\0\"y\0Ž\0\"{\0\"|\0\"}\0\"~\x000D";
    private const string LatvianQwertyBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \0°] ^ ` { | } \0~   \x001D    \x001C    \0    \0    \x000D";
    private const string LatvianQwertyDeadKey1 = "\0°!\0°\"\0°#\0°$\0°%\0°&\0°'\0°(\0°)\0°*\0°+\0°,\0°-\0°.\0°/\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°:\0°;\0°<\0°=\0°>\0°?\0°@\0Å\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0Ż\0°[\0°°\0°]\0°^\0°_\0°`\0å\0°b\0°c\0°d\0ė\0°f\0ġ\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0ż\0°{\0°|\0°}\0°~\x000D";
    private const string LatvianQwertyDeadKey2 = "\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~[\0~°\0~]\0~^\0~_\0~`\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~{\0~|\0~}\0~~\x000D";
    private const string LatvianQwertyBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string LatvianQwertyBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string LatvianQwertyBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string LatvianQwertyBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P\"\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string LatvianQwertyBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string LatvianQwertyBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j\"%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string LatvianQwertyBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string LatvianQwertyBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string LatvianQwertyBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(\"P_nF8=9L\0\x0004\x000D";
    private const string LatvianQwertyBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%\"\0\x0004\x000D";
    private const string LatvianQwertyBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS\"j:AugCzJt\0\x0004\x000D";
    private const string LatvianQwertyBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string LatvianBaseline = "  ! \0°% & \0´( ) × F , - . ļ 0 1 2 3 4 5 6 7 8 9 C c ; f : Ļ Š P Ī S J I L D Z A T E Ā O Ē Č Ū R U M N K G B V Ņ _ š p ī s j i l d z a t e ā o ē č ū r u m n k g b v ņ   » $ « ž ķ h / ­ Ž Ķ H ?    \x0008    \x001C    \0    \0    \x000D";
    private const string LatvianDeadKey1 = "\0°!\0°°\0°»\0°$\0°%\0°&\0°´\0°(\0°)\0°×\0°F\0°,\0°-\0°.\0°ļ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°C\0°c\0°;\0°f\0°:\0°Ļ\0°«\0°Š\0°P\0°Ī\0°S\0°J\0°I\0°L\0°D\0Ż\0Å\0°T\0Ė\0°Ā\0°O\0°Ē\0°Č\0°Ū\0°R\0°U\0°M\0°N\0°K\0°G\0°B\0°V\0°Ņ\0°ž\0°ķ\0°h\0°/\0°_\0°­\0°š\0°p\0°ī\0°s\0°j\0°i\0°l\0°d\0ż\0å\0°t\0ė\0°ā\0°o\0°ē\0°č\0°ū\0°r\0°u\0°m\0°n\0°k\0ġ\0°b\0°v\0°ņ\0°Ž\0°Ķ\0°H\0°?\x000D";
    private const string LatvianDeadKey2 = "\0´!\0´°\0´»\0´$\0´%\0´&\0´´\0´(\0´)\0´×\0´F\0´,\0´-\0´.\0´ļ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0Ć\0ć\0´;\0´f\0´:\0´Ļ\0´«\0´Š\0´P\0´Ī\0Ś\0´J\0´I\0´L\0´D\0Ź\0´A\0´T\0É\0´Ā\0Ó\0´Ē\0´Č\0´Ū\0´R\0´U\0´M\0Ń\0´K\0´G\0´B\0´V\0´Ņ\0´ž\0´ķ\0´h\0´/\0´_\0´­\0´š\0´p\0´ī\0ś\0´j\0´i\0´l\0´d\0ź\0´a\0´t\0é\0´ā\0ó\0´ē\0´č\0´ū\0´r\0´u\0´m\0ń\0´k\0´g\0´b\0´v\0´ņ\0´Ž\0´Ķ\0´H\0´?\x000D";
    private const string LatvianBarcode1 = "ž):\006\x00089O111003592770\x00081MSsKīB;m\x0008S230207\x0008UvĪD×4\0´d1Šp\0";
    private const string LatvianBarcode2 = "ž):\006\x00089O111003592770\x00081M.LRu!ūĒ\x0008S230729\x0008UMlZkĻ,6PāT\0";
    private const string LatvianBarcode3 = "ž):\006\x00089O111003592770\x00081M(ičObVz\x0008S230405\x0008UŅ&Nr7:0ēŪU\0";
    private const string LatvianBarcode4 = "ž):\006\x00089O111003592770\x00081Mf8Io_Č\0\x0008°S241002\x0008UJg5c2ļņšĀA\0";
    private const string LatvianBarcode5 = "ž):\006\x00089O111003592770\x00081M3tnG)E9\x0008S240304\x0008U5a4ĪemJī-V\0";
    private const string LatvianBarcode6 = "ž):\006\x00089O111003592770\x00081MCa\0°%jFČ\x0008S231031\x0008U51zmAņĪlnŠ\0";
    private const string LatvianBarcode7 = "ž):\006\x00089O111003592770\x00081MpŠ1d\0´4×\x0008S231214\x0008Um;BīKsS0ū!\0";
    private const string LatvianBarcode8 = "ž):\006\x00089O111003592770\x00081MDĪvTāP6\x0008S240620\x0008UuRL.zVbOči\0";
    private const string LatvianBarcode9 = "ž):\006\x00089O111003592770\x00081M,ĻkZlMU\x0008S240218\x0008U(\0°Č_oI8f9E\0";
    private const string LatvianBarcode10 = "ž):\006\x00089O111003592770\x00081MŪē0:7rN\x0008S240703\x0008U)Gnt3ČFj%\0\0°";
    private const string LatvianBarcode11 = "ž):\006\x00089O111003592770\x00081M&ŅAĀšņļ\x0008S231126\x0008U\0åCŠnlĪņAm\0";
    private const string LatvianBarcode12 = "ž):\006\x00089O111003592770\x00081M2c5gJV-\x0008S230404\x0008UDZrŪ9ŪjMvŪ\0";
    private const string LithuanianBaseline = "  Ą \" Į Ų ' ( ) Ū Ž , - . / 0 ą č ę ė į š ų ū 9 : ; < ž > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   Ę Ė Č [ \\ ] Š ` { | } ~    \x001D    \x001C    \0    \0    \x000D";
    private const string LithuanianBarcode1 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTDdVcX<t\x001DDčę0č0ų\x001DSyCHŪė'hąAb\0\x0004\x000D";
    private const string LithuanianBarcode2 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąT.GRsĄqO\x001DDčę0ųč9\x001DSTgIv?,šBmK\0\x0004\x000D";
    private const string LithuanianBarcode3 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąT(fpNxYi\x001DDčę0ė0į\x001DSZŲUrų>0oQS\0\x0004\x000D";
    private const string LithuanianBarcode4 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTžūFn_P\"\x001DDčėą00č\x001DSEwį;č/zaMJ\0\x0004\x000D";
    private const string LithuanianBarcode5 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTękuW)L9\x001DDčė0ę0ė\x001DSįjėCltEc-Y\0\x0004\x000D";
    private const string LithuanianBarcode6 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąT:j\"ĮeŽP\x001DDčęą0ęą\x001DSįąitJzCguA\0\x0004\x000D";
    private const string LithuanianBarcode7 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTbAąh'ėŪ\x001DDčęąčąė\x001DSt<XcVdD0qĄ\0\x0004\x000D";
    private const string LithuanianBarcode8 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTHCyKmBš\x001DDčė0šč0\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string LithuanianBarcode9 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąT,?vIgTS\x001DDčė0čąū\x001DS(\"P_nFūž9L\0\x0004\x000D";
    private const string LithuanianBarcode10 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTQo0>ųrU\x001DDčė0ų0ę\x001DS)WukęPŽeĮ\"\0\x0004\x000D";
    private const string LithuanianBarcode11 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTŲZJMaz/\x001DDčęąąčš\x001DS\"j:AugCzJt\0\x0004\x000D";
    private const string LithuanianBarcode12 = "[)>\00š\x001D9Nąąą00ęį9čųų0\x001DąTč;įwEY-\x001DDčę0ė0ė\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string LithuanianIbmBaseline = "  1 Ė 5 7 ė 9 0 8 = č _ š ę ) ! \" / ; : , . ? ( Ų ų Č + Š Ę A B C D E F G H I J K L M N O P Ą R S T U V Ž Ū Y Z - a b c d e f g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į | “ 6 ` Į \\ ” ~    \x001D    \x001C    \0    \0    \x000D";
    private const string LithuanianIbmBarcode1 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TDdVcŪČt\x001DD\"/)\").\x001DSyCH8;ėh!Ab\0\x0004\x000D";
    private const string LithuanianIbmBarcode2 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TšGRs1ąO\x001DD\"/).\"(\x001DSTgIvĘč,BmK\0\x0004\x000D";
    private const string LithuanianIbmBarcode3 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!T9fpNūYi\x001DD\"/);):\x001DSZ7Ur.Š)oĄS\0\x0004\x000D";
    private const string LithuanianIbmBarcode4 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!T+?Fn-PĖ\x001DD\";!))\"\x001DSEž:ų\"ęzaMJ\0\x0004\x000D";
    private const string LithuanianIbmBarcode5 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!T/kuŽ0L(\x001DD\";)/);\x001DS:j;CltEc_Y\0\x0004\x000D";
    private const string LithuanianIbmBarcode6 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TŲjĖ5e=P\x001DD\"/!)/!\x001DS:!itJzCguA\0\x0004\x000D";
    private const string LithuanianIbmBarcode7 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TbA!hė;8\x001DD\"/!\"!;\x001DStČŪcVdD)ą1\0\x0004\x000D";
    private const string LithuanianIbmBarcode8 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!THCyKmB,\x001DD\";),\")\x001DSsRGšiYūNpf\0\x0004\x000D";
    private const string LithuanianIbmBarcode9 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TčĘvIgTS\x001DD\";)\"!?\x001DS9ĖP-nF?+(L\0\x0004\x000D";
    private const string LithuanianIbmBarcode10 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!TĄo)Š.rU\x001DD\";).)/\x001DS0Žuk/P=e5Ė\0\x0004\x000D";
    private const string LithuanianIbmBarcode11 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!T7ZJMazę\x001DD\"/!!\",\x001DSĖjŲAugCzJt\0\x0004\x000D";
    private const string LithuanianIbmBarcode12 = "į0Š\0),\x001D(N!!!))/:(\"..)\x001D!T\"ų:žEY_\x001DD\"/););\x001DSHIrĄ(ĄeTyĄ\0\x0004\x000D";
    private const string LithuanianStandardBaseline = "  1 Ė 5 7 ė 9 0 8 X č ? f ę ) ! - / ; : , . = ( Ų ų Č x F Ę A B C D E Š G H I J K L M N O P Ą R S T U V Ž Ū Y Z + a b c d e š g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į q w 6 ` Į Q W ~    \0    \0    \0    \0    \x000D";
    private const string LithuanianStandardBarcode1 = "į0F\0),\0(N!!!))/:(-..)\0!TDdVcŪČt\0D-/)-).\0SyCH8;ėh!Ab\0\x0004\x000D";
    private const string LithuanianStandardBarcode2 = "į0F\0),\0(N!!!))/:(-..)\0!TfGRs1ąO\0D-/).-(\0STgIvĘč,BmK\0\x0004\x000D";
    private const string LithuanianStandardBarcode3 = "į0F\0),\0(N!!!))/:(-..)\0!T9špNūYi\0D-/);):\0SZ7Ur.F)oĄS\0\x0004\x000D";
    private const string LithuanianStandardBarcode4 = "į0F\0),\0(N!!!))/:(-..)\0!Tx=Šn+PĖ\0D-;!))-\0SEž:ų-ęzaMJ\0\x0004\x000D";
    private const string LithuanianStandardBarcode5 = "į0F\0),\0(N!!!))/:(-..)\0!T/kuŽ0L(\0D-;)/);\0S:j;CltEc?Y\0\x0004\x000D";
    private const string LithuanianStandardBarcode6 = "į0F\0),\0(N!!!))/:(-..)\0!TŲjĖ5eXP\0D-/!)/!\0S:!itJzCguA\0\x0004\x000D";
    private const string LithuanianStandardBarcode7 = "į0F\0),\0(N!!!))/:(-..)\0!TbA!hė;8\0D-/!-!;\0StČŪcVdD)ą1\0\x0004\x000D";
    private const string LithuanianStandardBarcode8 = "į0F\0),\0(N!!!))/:(-..)\0!THCyKmB,\0D-;),-)\0SsRGfiYūNpš\0\x0004\x000D";
    private const string LithuanianStandardBarcode9 = "į0F\0),\0(N!!!))/:(-..)\0!TčĘvIgTS\0D-;)-!=\0S9ĖP+nŠ=x(L\0\x0004\x000D";
    private const string LithuanianStandardBarcode10 = "į0F\0),\0(N!!!))/:(-..)\0!TĄo)F.rU\0D-;).)/\0S0Žuk/PXe5Ė\0\x0004\x000D";
    private const string LithuanianStandardBarcode11 = "į0F\0),\0(N!!!))/:(-..)\0!T7ZJMazę\0D-/!!-,\0SĖjŲAugCzJt\0\x0004\x000D";
    private const string LithuanianStandardBarcode12 = "į0F\0),\0(N!!!))/:(-..)\0!T-ų:žEY?\0D-/););\0SHIrĄ(ĄeTyĄ\0\x0004\x000D";
    private const string SorbianBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \0    \0    \0    \0    \x000D";
    private const string SorbianDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string SorbianDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0´E\0´F\0´G\0´H\0´I\0´J\0´K\0Ł\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0´U\0´V\0´W\0´X\0Ź\0´Y\0´ü\0´#\0´+\0´&\0´?\0´^\0´a\0´b\0ć\0´d\0´e\0´f\0´g\0´h\0´i\0´j\0´k\0ł\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0´u\0´v\0´w\0´x\0ź\0´y\0´Ü\0´'\0´*\0´°\x000D";
    private const string SorbianDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0^D\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0^L\0^M\0^N\0^O\0^P\0^Q\0Ř\0Š\0^T\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0^a\0^b\0č\0^d\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0^l\0^m\0^n\0^o\0^p\0^q\0ř\0š\0^t\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string SorbianBarcode1 = "ü=:\006\09N111003592770\01TDdVcX;t\0D230207\0SzCH(4äh1Ab\0\x0004\x000D";
    private const string SorbianBarcode2 = "ü=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string SorbianBarcode3 = "ü=:\006\09N111003592770\01T)fpNxZi\0D230405\0SY/Ur7:0oQS\0\x0004\x000D";
    private const string SorbianBarcode4 = "ü=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-yaMJ\0\x0004\x000D";
    private const string SorbianBarcode5 = "ü=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEcßZ\0\x0004\x000D";
    private const string SorbianBarcode6 = "ü=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJyCguA\0\x0004\x000D";
    private const string SorbianBarcode7 = "ü=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string SorbianBarcode8 = "ü=:\006\09N111003592770\01THCzKmB6\0D240620\0SsRG.iZxNpf\0\x0004\x000D";
    private const string SorbianBarcode9 = "ü=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SorbianBarcode10 = "ü=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string SorbianBarcode11 = "ü=:\006\09N111003592770\01T/YJMay-\0D231126\0SÄjÖAugCyJt\0\x0004\x000D";
    private const string SorbianBarcode12 = "ü=:\006\09N111003592770\01T2ö5wEZß\0D230404\0SHIrQ9QeTzQ\0\x0004\x000D";
    private const string NorwegianWithSamiBaseline = "  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \0    \0    \0    \0    \x000D";
    private const string NorwegianWithSamiDeadKey1 = "\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string NorwegianWithSamiDeadKey2 = "\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string NorwegianWithSamiDeadKey3 = "\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string NorwegianWithSamiBarcode1 = "å=:\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4æh1Ab\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode2 = "å=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode3 = "å=:\006\09N111003592770\01T)fpNxYi\0D230405\0SZ/Ur7:0oQS\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode4 = "å=:\006\09N111003592770\01T\\8Fn?PÆ\0D241002\0SEw5ø2-zaMJ\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode5 = "å=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEc+Y\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode6 = "å=:\006\09N111003592770\01TØjÆ%e\0`P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode7 = "å=:\006\09N111003592770\01TbA1hæ4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode8 = "å=:\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode9 = "å=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÆP?nF8\\9L\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode10 = "å=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Æ\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode11 = "å=:\006\09N111003592770\01T/ZJMaz-\0D231126\0SÆjØAugCzJt\0\x0004\x000D";
    private const string NorwegianWithSamiBarcode12 = "å=:\006\09N111003592770\01T2ø5wEY+\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string LuxembourgishBaseline = "  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \x001D    \x001C    \0    \0    \x000D";
    private const string LuxembourgishDeadKey1 = "\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\x000D";
    private const string LuxembourgishDeadKey2 = "\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\x000D";
    private const string LuxembourgishDeadKey3 = "\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\x000D";
    private const string LuxembourgishBarcode1 = "è=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4àh1Ab\0\x0004\x000D";
    private const string LuxembourgishBarcode2 = "è=:\006\x001D9N111003592770\x001D1T.GRs+qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string LuxembourgishBarcode3 = "è=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string LuxembourgishBarcode4 = "è=:\006\x001D9N111003592770\x001D1T\0^8Fn?Pä\x001DD241002\x001DSEw5é2-yaMJ\0\x0004\x000D";
    private const string LuxembourgishBarcode5 = "è=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Z\0\x0004\x000D";
    private const string LuxembourgishBarcode6 = "è=:\006\x001D9N111003592770\x001D1Töjä%e\0`P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string LuxembourgishBarcode7 = "è=:\006\x001D9N111003592770\x001D1TbA1hà4(\x001DD231214\x001DSt;XcVdD0q+\0\x0004\x000D";
    private const string LuxembourgishBarcode8 = "è=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string LuxembourgishBarcode9 = "è=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)äP?nF8\0^9L\0\x0004\x000D";
    private const string LuxembourgishBarcode10 = "è=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%ä\0\x0004\x000D";
    private const string LuxembourgishBarcode11 = "è=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSäjöAugCyJt\0\x0004\x000D";
    private const string LuxembourgishBarcode12 = "è=:\006\x001D9N111003592770\x001D1T2é5wEZ'\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string NorwegianBaseline = "  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \x001D    \0    \0    \0    \x000D";
    private const string NorwegianDeadKey1 = "\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string NorwegianDeadKey2 = "\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string NorwegianDeadKey3 = "\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string NorwegianBarcode1 = "å=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4æh1Ab\0\x0004\x000D";
    private const string NorwegianBarcode2 = "å=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string NorwegianBarcode3 = "å=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string NorwegianBarcode4 = "å=:\006\x001D9N111003592770\x001D1T\\8Fn?PÆ\x001DD241002\x001DSEw5ø2-zaMJ\0\x0004\x000D";
    private const string NorwegianBarcode5 = "å=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Y\0\x0004\x000D";
    private const string NorwegianBarcode6 = "å=:\006\x001D9N111003592770\x001D1TØjÆ%e\0`P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string NorwegianBarcode7 = "å=:\006\x001D9N111003592770\x001D1TbA1hæ4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string NorwegianBarcode8 = "å=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string NorwegianBarcode9 = "å=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ÆP?nF8\\9L\0\x0004\x000D";
    private const string NorwegianBarcode10 = "å=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P\0è%Æ\0\x0004\x000D";
    private const string NorwegianBarcode11 = "å=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DSÆjØAugCzJt\0\x0004\x000D";
    private const string NorwegianBarcode12 = "å=:\006\x001D9N111003592770\x001D1T2ø5wEY+\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string Maltese47KeyBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ @ ġ ż ħ ^ ċ Ġ Ż Ħ Ċ    \0    \0    \0    \0    \x000D";
    private const string Maltese47KeyBarcode1 = "ġ)>\006\09N111003592770\01TDdVcX<t\0D230207\0SyCH*4'h1Ab\0\x0004\x000D";
    private const string Maltese47KeyBarcode2 = "ġ)>\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv?,6BmK\0\x0004\x000D";
    private const string Maltese47KeyBarcode3 = "ġ)>\006\09N111003592770\01T(fpNxYi\0D230405\0SZ&Ur7>0oQS\0\x0004\x000D";
    private const string Maltese47KeyBarcode4 = "ġ)>\006\09N111003592770\01T=8Fn_P\"\0D241002\0SEw5;2/zaMJ\0\x0004\x000D";
    private const string Maltese47KeyBarcode5 = "ġ)>\006\09N111003592770\01T3kuW)L9\0D240304\0S5j4CltEc-Y\0\x0004\x000D";
    private const string Maltese47KeyBarcode6 = "ġ)>\006\09N111003592770\01T:j\"%e+P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string Maltese47KeyBarcode7 = "ġ)>\006\09N111003592770\01TbA1h'4*\0D231214\0St<XcVdD0q!\0\x0004\x000D";
    private const string Maltese47KeyBarcode8 = "ġ)>\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string Maltese47KeyBarcode9 = "ġ)>\006\09N111003592770\01T,?vIgTS\0D240218\0S(\"P_nF8=9L\0\x0004\x000D";
    private const string Maltese47KeyBarcode10 = "ġ)>\006\09N111003592770\01TQo0>7rU\0D240703\0S)Wuk3P+e%\"\0\x0004\x000D";
    private const string Maltese47KeyBarcode11 = "ġ)>\006\09N111003592770\01T&ZJMaz/\0D231126\0S\"j:AugCzJt\0\x0004\x000D";
    private const string Maltese47KeyBarcode12 = "ġ)>\006\09N111003592770\01T2;5wEY-\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string Maltese48KeyBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ \" ġ # ħ ^ ċ Ġ ~ Ħ Ċ    \0    \0    \0    \0    \x000D";
    private const string Maltese48KeyBarcode1 = "ġ)>\006\09N111003592770\01TDdVcX<t\0D230207\0SyCH*4'h1Ab\0\x0004\x000D";
    private const string Maltese48KeyBarcode2 = "ġ)>\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv?,6BmK\0\x0004\x000D";
    private const string Maltese48KeyBarcode3 = "ġ)>\006\09N111003592770\01T(fpNxYi\0D230405\0SZ&Ur7>0oQS\0\x0004\x000D";
    private const string Maltese48KeyBarcode4 = "ġ)>\006\09N111003592770\01T=8Fn_P@\0D241002\0SEw5;2/zaMJ\0\x0004\x000D";
    private const string Maltese48KeyBarcode5 = "ġ)>\006\09N111003592770\01T3kuW)L9\0D240304\0S5j4CltEc-Y\0\x0004\x000D";
    private const string Maltese48KeyBarcode6 = "ġ)>\006\09N111003592770\01T:j@%e+P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string Maltese48KeyBarcode7 = "ġ)>\006\09N111003592770\01TbA1h'4*\0D231214\0St<XcVdD0q!\0\x0004\x000D";
    private const string Maltese48KeyBarcode8 = "ġ)>\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string Maltese48KeyBarcode9 = "ġ)>\006\09N111003592770\01T,?vIgTS\0D240218\0S(@P_nF8=9L\0\x0004\x000D";
    private const string Maltese48KeyBarcode10 = "ġ)>\006\09N111003592770\01TQo0>7rU\0D240703\0S)Wuk3P+e%@\0\x0004\x000D";
    private const string Maltese48KeyBarcode11 = "ġ)>\006\09N111003592770\01T&ZJMaz/\0D231126\0S@j:AugCzJt\0\x0004\x000D";
    private const string Maltese48KeyBarcode12 = "ġ)>\006\09N111003592770\01T2;5wEY-\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string PolishProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } \0~   \x001D    \x001C    \0    \x001F    \x000D";
    private const string PolishProgrammersDeadKey1 = "\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0Ą\0~B\0Ć\0~D\0Ę\0~F\0~G\0~H\0~I\0~J\0~K\0Ł\0~M\0Ń\0Ó\0~P\0~Q\0~R\0Ś\0~T\0~U\0~V\0~W\0Ź\0~Y\0Ż\0~[\0~\\\0~]\0~^\0~_\0~`\0ą\0~b\0ć\0~d\0ę\0~f\0~g\0~h\0~i\0~j\0~k\0ł\0~m\0ń\0ó\0~p\0~q\0~r\0ś\0~t\0~u\0~v\0~w\0ź\0~y\0ż\0~{\0~|\0~}\0~~\x000D";
    private const string PolishProgrammersBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string PolishProgrammersBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string PolishProgrammersBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string PolishProgrammersBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P\"\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string PolishProgrammersBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string PolishProgrammersBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j\"%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string PolishProgrammersBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string PolishProgrammersBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string PolishProgrammersBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(\"P_nF8=9L\0\x0004\x000D";
    private const string PolishProgrammersBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%\"\0\x0004\x000D";
    private const string PolishProgrammersBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS\"j:AugCzJt\0\x0004\x000D";
    private const string PolishProgrammersBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string Polish214Baseline = "  ! ę % / ą ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ł ł ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ż ó ś & \0˛ń ź ć \0·   \x001D    \x001C    \0    \0    \x000D";
    private const string Polish214DeadKey1 = "\0˛!\0˛ę\0˛#\0˛¤\0˛%\0˛/\0˛ą\0˛)\0˛=\0˛(\0˛*\0˛,\0˛+\0˛.\0˛-\0˛0\0˛1\0˛2\0˛3\0˛4\0˛5\0˛6\0˛7\0˛8\0˛9\0˛Ł\0˛ł\0˛;\0˛'\0˛:\0˛_\0˛\"\0Ą\0˛B\0˛C\0˛D\0Ę\0˛F\0˛G\0˛H\0˛I\0˛J\0˛K\0˛L\0˛M\0˛N\0˛O\0˛P\0˛Q\0˛R\0˛S\0˛T\0˛U\0˛V\0˛W\0˛X\0˛Z\0˛Y\0˛ż\0˛ó\0˛ś\0˛&\0˛?\0˛˛\0ą\0˛b\0˛c\0˛d\0ę\0˛f\0˛g\0˛h\0˛i\0˛j\0˛k\0˛l\0˛m\0˛n\0˛o\0˛p\0˛q\0˛r\0˛s\0˛t\0˛u\0˛v\0˛w\0˛x\0˛z\0˛y\0˛ń\0˛ź\0˛ć\0˛·\x000D";
    private const string Polish214DeadKey2 = "\0·!\0·ę\0·#\0·¤\0·%\0·/\0·ą\0·)\0·=\0·(\0·*\0·,\0·+\0·.\0·-\0·0\0·1\0·2\0·3\0·4\0·5\0·6\0·7\0·8\0·9\0·Ł\0·ł\0·;\0·'\0·:\0·_\0·\"\0·A\0·B\0·C\0·D\0·E\0·F\0·G\0·H\0·I\0·J\0·K\0·L\0·M\0·N\0·O\0·P\0·Q\0·R\0·S\0·T\0·U\0·V\0·W\0·X\0Ż\0·Y\0·ż\0·ó\0·ś\0·&\0·?\0·˛\0·a\0·b\0·c\0·d\0·e\0·f\0·g\0·h\0·i\0·j\0·k\0·l\0·m\0·n\0·o\0·p\0·q\0·r\0·s\0·t\0·u\0·v\0·w\0·x\0ż\0·y\0·ń\0·ź\0·ć\0··\x000D";
    private const string Polish214Barcode1 = "ż=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4ąh1Ab\0\x0004\x000D";
    private const string Polish214Barcode2 = "ż=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string Polish214Barcode3 = "ż=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string Polish214Barcode4 = "ż=:\006\x001D9N111003592770\x001D1T'8Fn?Pę\x001DD241002\x001DSEw5ł2-yaMJ\0\x0004\x000D";
    private const string Polish214Barcode5 = "ż=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Z\0\x0004\x000D";
    private const string Polish214Barcode6 = "ż=:\006\x001D9N111003592770\x001D1TŁję%e*P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string Polish214Barcode7 = "ż=:\006\x001D9N111003592770\x001D1TbA1hą4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string Polish214Barcode8 = "ż=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string Polish214Barcode9 = "ż=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ęP?nF8'9L\0\x0004\x000D";
    private const string Polish214Barcode10 = "ż=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P*e%ę\0\x0004\x000D";
    private const string Polish214Barcode11 = "ż=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSęjŁAugCyJt\0\x0004\x000D";
    private const string Polish214Barcode12 = "ż=:\006\x001D9N111003592770\x001D1T2ł5wEZ+\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string PortugueseBaseline = "  ! ª % / º ) = ( » , ' . - 0 1 2 3 4 5 6 7 8 9 Ç ç ; « : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" + \0~\0´& \\ * \0^\0`|    \0    \0    \0    \0    \x000D";
    private const string PortugueseDeadKey1 = "\0~!\0~ª\0~#\0~$\0~%\0~/\0~º\0~)\0~=\0~(\0~»\0~,\0~'\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ç\0~ç\0~;\0~«\0~:\0~_\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~+\0~~\0~´\0~&\0~?\0~\\\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~*\0~^\0~`\0~|\x000D";
    private const string PortugueseDeadKey2 = "\0´!\0´ª\0´#\0´$\0´%\0´/\0´º\0´)\0´=\0´(\0´»\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ç\0´ç\0´;\0´«\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´+\0´~\0´´\0´&\0´?\0´\\\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´*\0´^\0´`\0´|\x000D";
    private const string PortugueseDeadKey3 = "\0^!\0^ª\0^#\0^$\0^%\0^/\0^º\0^)\0^=\0^(\0^»\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ç\0^ç\0^;\0^«\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^+\0^~\0^´\0^&\0^?\0^\\\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^*\0^^\0^`\0^|\x000D";
    private const string PortugueseDeadKey4 = "\0`!\0`ª\0`#\0`$\0`%\0`/\0`º\0`)\0`=\0`(\0`»\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ç\0`ç\0`;\0`«\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`+\0`~\0`´\0`&\0`?\0`\\\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`*\0`^\0``\0`|\x000D";
    private const string PortugueseBarcode1 = "+=:\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH(4ºh1Ab\0\x0004\x000D";
    private const string PortugueseBarcode2 = "+=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string PortugueseBarcode3 = "+=:\006\09N111003592770\01T)fpNxYi\0D230405\0SZ/Ur7:0oQS\0\x0004\x000D";
    private const string PortugueseBarcode4 = "+=:\006\09N111003592770\01T«8Fn?Pª\0D241002\0SEw5ç2-zaMJ\0\x0004\x000D";
    private const string PortugueseBarcode5 = "+=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEc'Y\0\x0004\x000D";
    private const string PortugueseBarcode6 = "+=:\006\09N111003592770\01TÇjª%e»P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string PortugueseBarcode7 = "+=:\006\09N111003592770\01TbA1hº4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string PortugueseBarcode8 = "+=:\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string PortugueseBarcode9 = "+=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ªP?nF8«9L\0\x0004\x000D";
    private const string PortugueseBarcode10 = "+=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P»e%ª\0\x0004\x000D";
    private const string PortugueseBarcode11 = "+=:\006\09N111003592770\01T/ZJMaz-\0D231126\0SªjÇAugCzJt\0\x0004\x000D";
    private const string PortugueseBarcode12 = "+=:\006\09N111003592770\01T2ç5wEY'\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string RomanianStandardBaseline = "  ! Ț % & ț ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 Ș ș ; = : ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ ă â î ^ „ Ă Â Î ”    \0    \0    \0    \0    \x000D";
    private const string RomanianStandardBarcode1 = "ă):\006\09N111003592770\01TDdVcX;t\0D230207\0SyCH*4țh1Ab\0\x0004\x000D";
    private const string RomanianStandardBarcode2 = "ă):\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv?,6BmK\0\x0004\x000D";
    private const string RomanianStandardBarcode3 = "ă):\006\09N111003592770\01T(fpNxYi\0D230405\0SZ&Ur7:0oQS\0\x0004\x000D";
    private const string RomanianStandardBarcode4 = "ă):\006\09N111003592770\01T=8Fn_PȚ\0D241002\0SEw5ș2/zaMJ\0\x0004\x000D";
    private const string RomanianStandardBarcode5 = "ă):\006\09N111003592770\01T3kuW)L9\0D240304\0S5j4CltEc-Y\0\x0004\x000D";
    private const string RomanianStandardBarcode6 = "ă):\006\09N111003592770\01TȘjȚ%e+P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string RomanianStandardBarcode7 = "ă):\006\09N111003592770\01TbA1hț4*\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string RomanianStandardBarcode8 = "ă):\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string RomanianStandardBarcode9 = "ă):\006\09N111003592770\01T,?vIgTS\0D240218\0S(ȚP_nF8=9L\0\x0004\x000D";
    private const string RomanianStandardBarcode10 = "ă):\006\09N111003592770\01TQo0:7rU\0D240703\0S)Wuk3P+e%Ț\0\x0004\x000D";
    private const string RomanianStandardBarcode11 = "ă):\006\09N111003592770\01T&ZJMaz/\0D231126\0SȚjȘAugCzJt\0\x0004\x000D";
    private const string RomanianStandardBarcode12 = "ă):\006\09N111003592770\01T2ș5wEY-\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string RomanianLegacyBaseline = "  ! Ţ % / ţ ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ş ş ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ă â î & ] Ă Â Î [    \x001D    \x001C    \0    \0    \x000D";
    private const string RomanianLegacyBarcode1 = "ă=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSzCH(4ţh1Ab\0\x0004\x000D";
    private const string RomanianLegacyBarcode2 = "ă=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string RomanianLegacyBarcode3 = "ă=:\006\x001D9N111003592770\x001D1T)fpNxZi\x001DD230405\x001DSY/Ur7:0oQS\0\x0004\x000D";
    private const string RomanianLegacyBarcode4 = "ă=:\006\x001D9N111003592770\x001D1T'8Fn?PŢ\x001DD241002\x001DSEw5ş2-yaMJ\0\x0004\x000D";
    private const string RomanianLegacyBarcode5 = "ă=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc+Z\0\x0004\x000D";
    private const string RomanianLegacyBarcode6 = "ă=:\006\x001D9N111003592770\x001D1TŞjŢ%e*P\x001DD231031\x001DS51itJyCguA\0\x0004\x000D";
    private const string RomanianLegacyBarcode7 = "ă=:\006\x001D9N111003592770\x001D1TbA1hţ4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string RomanianLegacyBarcode8 = "ă=:\006\x001D9N111003592770\x001D1THCzKmB6\x001DD240620\x001DSsRG.iZxNpf\0\x0004\x000D";
    private const string RomanianLegacyBarcode9 = "ă=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)ŢP?nF8'9L\0\x0004\x000D";
    private const string RomanianLegacyBarcode10 = "ă=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P*e%Ţ\0\x0004\x000D";
    private const string RomanianLegacyBarcode11 = "ă=:\006\x001D9N111003592770\x001D1T/YJMay-\x001DD231126\x001DSŢjŞAugCyJt\0\x0004\x000D";
    private const string RomanianLegacyBarcode12 = "ă=:\006\x001D9N111003592770\x001D1T2ş5wEZ+\x001DD230404\x001DSHIrQ9QeTzQ\0\x0004\x000D";
    private const string RomanianProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \0    \0    \0    \0    \x000D";
    private const string RomanianProgrammersBarcode1 = "[)>\006\09N111003592770\01TDdVcX<t\0D230207\0SyCH*4'h1Ab\0\x0004\x000D";
    private const string RomanianProgrammersBarcode2 = "[)>\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv?,6BmK\0\x0004\x000D";
    private const string RomanianProgrammersBarcode3 = "[)>\006\09N111003592770\01T(fpNxYi\0D230405\0SZ&Ur7>0oQS\0\x0004\x000D";
    private const string RomanianProgrammersBarcode4 = "[)>\006\09N111003592770\01T=8Fn_P\"\0D241002\0SEw5;2/zaMJ\0\x0004\x000D";
    private const string RomanianProgrammersBarcode5 = "[)>\006\09N111003592770\01T3kuW)L9\0D240304\0S5j4CltEc-Y\0\x0004\x000D";
    private const string RomanianProgrammersBarcode6 = "[)>\006\09N111003592770\01T:j\"%e+P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string RomanianProgrammersBarcode7 = "[)>\006\09N111003592770\01TbA1h'4*\0D231214\0St<XcVdD0q!\0\x0004\x000D";
    private const string RomanianProgrammersBarcode8 = "[)>\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string RomanianProgrammersBarcode9 = "[)>\006\09N111003592770\01T,?vIgTS\0D240218\0S(\"P_nF8=9L\0\x0004\x000D";
    private const string RomanianProgrammersBarcode10 = "[)>\006\09N111003592770\01TQo0>7rU\0D240703\0S)Wuk3P+e%\"\0\x0004\x000D";
    private const string RomanianProgrammersBarcode11 = "[)>\006\09N111003592770\01T&ZJMaz/\0D231126\0S\"j:AugCzJt\0\x0004\x000D";
    private const string RomanianProgrammersBarcode12 = "[)>\006\09N111003592770\01T2;5wEY-\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";
    private const string ScottishGaelicBaseline = "  ! @ % & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } `    \x001D    \x001C    \0    \0    \x000D";
    private const string ScottishGaelicDeadKey1 = "\0'!\0'@\0'£\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0'.\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0':\0';\0'<\0'=\0'>\0'?\0'\"\0Á\0'B\0'C\0'D\0É\0'F\0'G\0'H\0Í\0'J\0'K\0'L\0'M\0'N\0Ó\0'P\0'Q\0'R\0'S\0'T\0Ú\0'V\0'W\0'X\0Ý\0'Z\0'[\0'#\0']\0'^\0'_\0'`\0á\0'b\0'c\0'd\0é\0'f\0'g\0'h\0í\0'j\0'k\0'l\0'm\0'n\0ó\0'p\0'q\0'r\0's\0't\0ú\0'v\0'w\0'x\0ý\0'z\0'{\0'~\0'}\0'`\x000D";
    private const string ScottishGaelicDeadKey2 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0``\x000D";
    private const string ScottishGaelicBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4\0'h1Ab\0\x0004\x000D";
    private const string ScottishGaelicBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string ScottishGaelicBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string ScottishGaelicBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string ScottishGaelicBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string ScottishGaelicBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string ScottishGaelicBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h\0'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string ScottishGaelicBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string ScottishGaelicBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L\0\x0004\x000D";
    private const string ScottishGaelicBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@\0\x0004\x000D";
    private const string ScottishGaelicBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt\0\x0004\x000D";
    private const string ScottishGaelicBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string SlovakBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú ň ä 6 ; / ) ( \0°   \0    \x001C    \0    \0    \x000D";
    private const string SlovakDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ)\0ˇ(\0ˇ°\x000D";
    private const string SlovakQwertyBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú ň ä 6 ; / ) ( \0°   \x001B    \x001C    \x001E    \0    \x000D";
    private const string SlovakQwertyDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ)\0ˇ(\0ˇ°\x000D";
    private const string SlovakQwertyDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ľ\0´š\0´č\0´ť\0´ž\0´ý\0´á\0´í\0´\"\0´ô\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´ň\0´ä\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´)\0´(\0´°\x000D";
    private const string SlovakQwertyDeadKey3 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ľ\0°š\0°č\0°ť\0°ž\0°ý\0°á\0°í\0°\"\0°ô\0°?\0°´\0°:\0°_\0°2\0°A\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°ň\0°ä\0°6\0°%\0°;\0°a\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°)\0°(\0°°\x000D";
    private const string SlovakQwertyBarcode1 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+TDdVcX?t\x001BDľšéľéý\x001BSyCH8č§h+Ab\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode2 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T.GRs1qO\x001BDľšéýľí\x001BSTgIv_,žBmK\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode3 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T9fpNxYi\x001BDľšéčéť\x001BSZ7Urý:éoQS\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode4 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T\0´áFn%P!\x001BDľč+ééľ\x001BSEwťôľ-zaMJ\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode5 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+TškuW0Lí\x001BDľčéšéč\x001BSťjčCltEc=Y\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode6 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T\"j!5e\0ˇP\x001BDľš+éš+\x001BSť+itJzCguA\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode7 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+TbA+h§č8\x001BDľš+ľ+č\x001BSt?XcVdDéq1\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode8 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+THCyKmBž\x001BDľčéžľé\x001BSsRG.iYxNpf\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode9 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T,_vIgTS\x001BDľčéľ+á\x001BS9!P%nFá\0´íL\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode10 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+TQoé:ýrU\x001BDľčéýéš\x001BS0WukšP\0ě5!\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode11 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+T7ZJMaz-\x001BDľš++ľž\x001BS!j\"AugCzJt\x001E\x0004\x000D";
    private const string SlovakQwertyBarcode12 = "ú0:\x001Eéž\x001BíN+++ééšťíľýýé\x001B+TľôťwEY=\x001BDľšéčéč\x001BSHIrQíQeTyQ\x001E\x0004\x000D";
    private const string SlovenianBaseline = "  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \x001B    \x001C    \0    \0    \x000D";
    private const string SlovenianDeadKey1 = "\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\x000D";
    private const string SlovenianDeadKey2 = "\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\x000D";
    private const string SlovenianBarcode1 = "š=:\006\x001B9N111003592770\x001B1TDdVcX;t\x001BD230207\x001BSzCH(4ćh1Ab\0\x0004\x000D";
    private const string SlovenianBarcode2 = "š=:\006\x001B9N111003592770\x001B1T.GRs!qO\x001BD230729\x001BSTgIv_,6BmK\0\x0004\x000D";
    private const string SlovenianBarcode3 = "š=:\006\x001B9N111003592770\x001B1T)fpNxZi\x001BD230405\x001BSY/Ur7:0oQS\0\x0004\x000D";
    private const string SlovenianBarcode4 = "š=:\006\x001B9N111003592770\x001B1T+8Fn?PĆ\x001BD241002\x001BSEw5č2-yaMJ\0\x0004\x000D";
    private const string SlovenianBarcode5 = "š=:\006\x001B9N111003592770\x001B1T3kuW=L9\x001BD240304\x001BS5j4CltEc'Z\0\x0004\x000D";
    private const string SlovenianBarcode6 = "š=:\006\x001B9N111003592770\x001B1TČjĆ%e*P\x001BD231031\x001BS51itJyCguA\0\x0004\x000D";
    private const string SlovenianBarcode7 = "š=:\006\x001B9N111003592770\x001B1TbA1hć4(\x001BD231214\x001BSt;XcVdD0q!\0\x0004\x000D";
    private const string SlovenianBarcode8 = "š=:\006\x001B9N111003592770\x001B1THCzKmB6\x001BD240620\x001BSsRG.iZxNpf\0\x0004\x000D";
    private const string SlovenianBarcode9 = "š=:\006\x001B9N111003592770\x001B1T,_vIgTS\x001BD240218\x001BS)ĆP?nF8+9L\0\x0004\x000D";
    private const string SlovenianBarcode10 = "š=:\006\x001B9N111003592770\x001B1TQo0:7rU\x001BD240703\x001BS=Wuk3P*e%Ć\0\x0004\x000D";
    private const string SlovenianBarcode11 = "š=:\006\x001B9N111003592770\x001B1T/YJMay-\x001BD231126\x001BSĆjČAugCyJt\0\x0004\x000D";
    private const string SlovenianBarcode12 = "š=:\006\x001B9N111003592770\x001B1T2č5wEZ'\x001BD230404\x001BSHIrQ9QeTzQ\0\x0004\x000D";
    private const string SpanishBaseline = "  ! \0¨% / \0´) = ( ¿ , ' . - 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; ¡ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   · $ \" \0`ç + & º \0^Ç * ª    \x001D    \x001C    \0    \0    \x000D";
    private const string SpanishDeadKey1 = "\0¨!\0¨¨\0¨·\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨¿\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ñ\0¨ñ\0¨;\0¨¡\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨`\0¨ç\0¨+\0¨&\0¨?\0¨º\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨Ç\0¨*\0¨ª\x000D";
    private const string SpanishDeadKey2 = "\0´!\0´¨\0´·\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´¿\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ñ\0´ñ\0´;\0´¡\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´`\0´ç\0´+\0´&\0´?\0´º\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´Ç\0´*\0´ª\x000D";
    private const string SpanishDeadKey3 = "\0`!\0`¨\0`·\0`$\0`%\0`/\0`´\0`)\0`=\0`(\0`¿\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ñ\0`ñ\0`;\0`¡\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0``\0`ç\0`+\0`&\0`?\0`º\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`Ç\0`*\0`ª\x000D";
    private const string SpanishDeadKey4 = "\0^!\0^¨\0^·\0^$\0^%\0^/\0^´\0^)\0^=\0^(\0^¿\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ñ\0^ñ\0^;\0^¡\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^`\0^ç\0^+\0^&\0^?\0^º\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^Ç\0^*\0^ª\x000D";
    private const string SpanishBarcode1 = "\0`=:\006\x001D9N111003592770\x001D1TDdVcX;t\x001DD230207\x001DSyCH(4\0´h1Ab\0\x0004\x000D";
    private const string SpanishBarcode2 = "\0`=:\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv_,6BmK\0\x0004\x000D";
    private const string SpanishBarcode3 = "\0`=:\006\x001D9N111003592770\x001D1T)fpNxYi\x001DD230405\x001DSZ/Ur7:0oQS\0\x0004\x000D";
    private const string SpanishBarcode4 = "\0`=:\006\x001D9N111003592770\x001D1T¡8Fn?P\0¨\x001DD241002\x001DSEw5ñ2-zaMJ\0\x0004\x000D";
    private const string SpanishBarcode5 = "\0`=:\006\x001D9N111003592770\x001D1T3kuW=L9\x001DD240304\x001DS5j4CltEc'Y\0\x0004\x000D";
    private const string SpanishBarcode6 = "\0`=:\006\x001D9N111003592770\x001D1TÑj\0¨%e¿P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string SpanishBarcode7 = "\0`=:\006\x001D9N111003592770\x001D1TbA1h\0´4(\x001DD231214\x001DSt;XcVdD0q!\0\x0004\x000D";
    private const string SpanishBarcode8 = "\0`=:\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string SpanishBarcode9 = "\0`=:\006\x001D9N111003592770\x001D1T,_vIgTS\x001DD240218\x001DS)\0¨P?nF8¡9L\0\x0004\x000D";
    private const string SpanishBarcode10 = "\0`=:\006\x001D9N111003592770\x001D1TQo0:7rU\x001DD240703\x001DS=Wuk3P¿e%\0\0\x0004¨";
    private const string SpanishBarcode11 = "\0`=:\006\x001D9N111003592770\x001D1T/ZJMaz-\x001DD231126\x001DS\0¨jÑAugCzJt\0\x0004\x000D";
    private const string SpanishBarcode12 = "\0`=:\006\x001D9N111003592770\x001D1T2ñ5wEY'\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string SpanishVariationBaseline = "  ª Ç ) ! ç ? ₧ ¿ \0¨, - . = 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; \0¨: % A B C D E F G H I J K L M N O P Q R S T U V W X Y Z + a b c d e f g h i j k l m n o p q r s t u v w x y z   / ( \" ÷ \0´\0`¡ ' × \0´\0`·    \x001D    \x001C    \0    \0    \x000D";
    private const string SorbianStandardBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \0    \0    \0    \0    \x000D";
    private const string SorbianStandardDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string SorbianStandardDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0´E\0´F\0´G\0´H\0´I\0´J\0´K\0Ł\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0´U\0´V\0´W\0´X\0Ź\0´Y\0´ü\0´#\0´+\0´&\0´?\0´^\0´a\0´b\0ć\0´d\0´e\0´f\0´g\0´h\0´i\0´j\0´k\0ł\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0´u\0´v\0´w\0´x\0ź\0´y\0´Ü\0´'\0´*\0´°\x000D";
    private const string SorbianStandardDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0^D\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0^L\0^M\0^N\0^O\0^P\0^Q\0Ř\0Š\0^T\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0^a\0^b\0č\0^d\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0^l\0^m\0^n\0^o\0^p\0^q\0ř\0š\0^t\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string SorbianStandardBarcode1 = "ü=:\006\09N111003592770\01TDdVcX;t\0D230207\0SzCH(4äh1Ab\0\x0004\x000D";
    private const string SorbianStandardBarcode2 = "ü=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string SorbianStandardBarcode3 = "ü=:\006\09N111003592770\01T)fpNxZi\0D230405\0SY/Ur7:0oQS\0\x0004\x000D";
    private const string SorbianStandardBarcode4 = "ü=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-yaMJ\0\x0004\x000D";
    private const string SorbianStandardBarcode5 = "ü=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEcßZ\0\x0004\x000D";
    private const string SorbianStandardBarcode6 = "ü=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJyCguA\0\x0004\x000D";
    private const string SorbianStandardBarcode7 = "ü=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string SorbianStandardBarcode8 = "ü=:\006\09N111003592770\01THCzKmB6\0D240620\0SsRG.iZxNpf\0\x0004\x000D";
    private const string SorbianStandardBarcode9 = "ü=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SorbianStandardBarcode10 = "ü=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string SorbianStandardBarcode11 = "ü=:\006\09N111003592770\01T/YJMay-\0D231126\0SÄjÖAugCyJt\0\x0004\x000D";
    private const string SorbianStandardBarcode12 = "ü=:\006\09N111003592770\01T2ö5wEZß\0D230404\0SHIrQ9QeTzQ\0\x0004\x000D";
    private const string SorbianExtendedBaseline = "  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0ˇÜ Ł * \0˙   \0    \0    \0    \0    \x000D";
    private const string SorbianExtendedDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0Ą\0`B\0Ç\0`D\0Ę\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ő\0`P\0`Q\0`R\0Ş\0`T\0Ű\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0ą\0`b\0ç\0`d\0ę\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ő\0`p\0`q\0`r\0ş\0`t\0ű\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\x000D";
    private const string SorbianExtendedDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\x000D";
    private const string SorbianExtendedDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0Ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\x000D";
    private const string SorbianExtendedDeadKey4 = "\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\x000D";
    private const string SorbianExtendedBarcode1 = "ü=:\006\09N111003592770\01TDdVcX;t\0D230207\0SzCH(4äh1Ab\0\x0004\x000D";
    private const string SorbianExtendedBarcode2 = "ü=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string SorbianExtendedBarcode3 = "ü=:\006\09N111003592770\01T)fpNxZi\0D230405\0SY/Ur7:0oQS\0\x0004\x000D";
    private const string SorbianExtendedBarcode4 = "ü=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-yaMJ\0\x0004\x000D";
    private const string SorbianExtendedBarcode5 = "ü=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEcßZ\0\x0004\x000D";
    private const string SorbianExtendedBarcode6 = "ü=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJyCguA\0\x0004\x000D";
    private const string SorbianExtendedBarcode7 = "ü=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string SorbianExtendedBarcode8 = "ü=:\006\09N111003592770\01THCzKmB6\0D240620\0SsRG.iZxNpf\0\x0004\x000D";
    private const string SorbianExtendedBarcode9 = "ü=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SorbianExtendedBarcode10 = "ü=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0ę%Ä\0\x0004\x000D";
    private const string SorbianExtendedBarcode11 = "ü=:\006\09N111003592770\01T/YJMay-\0D231126\0SÄjÖAugCyJt\0\x0004\x000D";
    private const string SorbianExtendedBarcode12 = "ü=:\006\09N111003592770\01T2ö5wEZß\0D230404\0SHIrQ9QeTzQ\0\x0004\x000D";
    private const string SorbianStandardLegacyBaseline = "  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0^Ü Ł * \0̇   \0    \0    \0    \0    \x000D";
    private const string SorbianStandardLegacyDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0Ç\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0Ş\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0à\0`b\0ç\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0ş\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\x000D";
    private const string SorbianStandardLegacyDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\x000D";
    private const string SorbianStandardLegacyDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0Ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\x000D";
    private const string SorbianStandardLegacyDeadKey4 = "\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\x000D";
    private const string SorbianStandardLegacyBarcode1 = "ü=:\006\09N111003592770\01TDdVcX;t\0D230207\0SzCH(4äh1Ab\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode2 = "ü=:\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv_,6BmK\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode3 = "ü=:\006\09N111003592770\01T)fpNxZi\0D230405\0SY/Ur7:0oQS\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode4 = "ü=:\006\09N111003592770\01T\0´8Fn?PÄ\0D241002\0SEw5ö2-yaMJ\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode5 = "ü=:\006\09N111003592770\01T3kuW=L9\0D240304\0S5j4CltEcßZ\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode6 = "ü=:\006\09N111003592770\01TÖjÄ%e\0`P\0D231031\0S51itJyCguA\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode7 = "ü=:\006\09N111003592770\01TbA1hä4(\0D231214\0St;XcVdD0q!\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode8 = "ü=:\006\09N111003592770\01THCzKmB6\0D240620\0SsRG.iZxNpf\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode9 = "ü=:\006\09N111003592770\01T,_vIgTS\0D240218\0S)ÄP?nF8\0´9L\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode10 = "ü=:\006\09N111003592770\01TQo0:7rU\0D240703\0S=Wuk3P\0è%Ä\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode11 = "ü=:\006\09N111003592770\01T/YJMay-\0D231126\0SÄjÖAugCyJt\0\x0004\x000D";
    private const string SorbianStandardLegacyBarcode12 = "ü=:\006\09N111003592770\01T2ö5wEZß\0D230404\0SHIrQ9QeTzQ\0\x0004\x000D";
    private const string UnitedKingdomBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \x001C    \0    \0    \x000D";
    private const string UnitedKingdomBarcode1 = "[)>\006\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\0\x0004\x000D";
    private const string UnitedKingdomBarcode2 = "[)>\006\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\0\x0004\x000D";
    private const string UnitedKingdomBarcode3 = "[)>\006\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\0\x0004\x000D";
    private const string UnitedKingdomBarcode4 = "[)>\006\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ\0\x0004\x000D";
    private const string UnitedKingdomBarcode5 = "[)>\006\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\0\x0004\x000D";
    private const string UnitedKingdomBarcode6 = "[)>\006\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA\0\x0004\x000D";
    private const string UnitedKingdomBarcode7 = "[)>\006\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\0\x0004\x000D";
    private const string UnitedKingdomBarcode8 = "[)>\006\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\0\x0004\x000D";
    private const string UnitedKingdomBarcode9 = "[)>\006\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L\0\x0004\x000D";
    private const string UnitedKingdomBarcode10 = "[)>\006\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@\0\x0004\x000D";
    private const string UnitedKingdomBarcode11 = "[)>\006\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt\0\x0004\x000D";
    private const string UnitedKingdomBarcode12 = "[)>\006\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\0\x0004\x000D";
    private const string UnitedKingdomExtendedBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \0    \0    \0    \0    \x000D";
    private const string UnitedKingdomExtendedDeadKey1 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`{\0`~\0`}\0`¬\x000D";
    private const string UnitedKingdomExtendedBarcode1 = "[)>\006\09N111003592770\01TDdVcX<t\0D230207\0SyCH*4'h1Ab\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode2 = "[)>\006\09N111003592770\01T.GRs!qO\0D230729\0STgIv?,6BmK\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode3 = "[)>\006\09N111003592770\01T(fpNxYi\0D230405\0SZ&Ur7>0oQS\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode4 = "[)>\006\09N111003592770\01T=8Fn_P@\0D241002\0SEw5;2/zaMJ\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode5 = "[)>\006\09N111003592770\01T3kuW)L9\0D240304\0S5j4CltEc-Y\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode6 = "[)>\006\09N111003592770\01T:j@%e+P\0D231031\0S51itJzCguA\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode7 = "[)>\006\09N111003592770\01TbA1h'4*\0D231214\0St<XcVdD0q!\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode8 = "[)>\006\09N111003592770\01THCyKmB6\0D240620\0SsRG.iYxNpf\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode9 = "[)>\006\09N111003592770\01T,?vIgTS\0D240218\0S(@P_nF8=9L\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode10 = "[)>\006\09N111003592770\01TQo0>7rU\0D240703\0S)Wuk3P+e%@\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode11 = "[)>\006\09N111003592770\01T&ZJMaz/\0D231126\0S@j:AugCzJt\0\x0004\x000D";
    private const string UnitedKingdomExtendedBarcode12 = "[)>\006\09N111003592770\01T2;5wEY-\0D230404\0SHIrQ9QeTyQ\0\x0004\x000D";

    // Test in the case that the ASCII 30 is reported correctly, and not as an ASCII 0.
    private const string UnitedKingdomVariant1Baseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \0    \x001E    \0    \x000D";
    private const string UnitedKingdomVariant1Barcode1 = "[)>\x001E06\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode2 = "[)>\x001E06\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode3 = "[)>\x001E06\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode4 = "[)>\x001E06\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode5 = "[)>\x001E06\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode6 = "[)>\x001E06\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode7 = "[)>\x001E06\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode8 = "[)>\x001E06\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode9 = "[)>\x001E06\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode10 = "[)>\x001E06\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode11 = "[)>\x001E06\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt\x001E\x0004\x000D";
    private const string UnitedKingdomVariant1Barcode12 = "[)>\x001E06\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\x001E\x0004\x000D";

    // Test in the case that the ASCII 30 as an variant ASCII character.
    private const string UnitedKingdomVariant2Baseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \0    \"    \0    \x000D";
    private const string UnitedKingdomVariant2Barcode1 = "[)>\"06\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode2 = "[)>\"06\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode3 = "[)>\"06\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode4 = "[)>\"06\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode5 = "[)>\"06\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode6 = "[)>\"06\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode7 = "[)>\"06\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode8 = "[)>\"06\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode9 = "[)>\"06\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode10 = "[)>\"06\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode11 = "[)>\"06\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt\"\x0004\x000D";
    private const string UnitedKingdomVariant2Barcode12 = "[)>\"06\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ\"\x0004\x000D";

    // Test in the case that the ASCII 30 as an invariant character.
    private const string UnitedKingdomVariant3Baseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \0    @    \0    \x000D";
    private const string UnitedKingdomVariant3Barcode1 = "[)>@06\x001D9N111003592770\x001D1TDdVcX<t\x001DD230207\x001DSyCH*4'h1Ab@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode2 = "[)>@06\x001D9N111003592770\x001D1T.GRs!qO\x001DD230729\x001DSTgIv?,6BmK@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode3 = "[)>@06\x001D9N111003592770\x001D1T(fpNxYi\x001DD230405\x001DSZ&Ur7>0oQS@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode4 = "[)>@06\x001D9N111003592770\x001D1T=8Fn_P@\x001DD241002\x001DSEw5;2/zaMJ@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode5 = "[)>@06\x001D9N111003592770\x001D1T3kuW)L9\x001DD240304\x001DS5j4CltEc-Y@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode6 = "[)>@06\x001D9N111003592770\x001D1T:j@%e+P\x001DD231031\x001DS51itJzCguA@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode7 = "[)>@06\x001D9N111003592770\x001D1TbA1h'4*\x001DD231214\x001DSt<XcVdD0q!@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode8 = "[)>@06\x001D9N111003592770\x001D1THCyKmB6\x001DD240620\x001DSsRG.iYxNpf@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode9 = "[)>@06\x001D9N111003592770\x001D1T,?vIgTS\x001DD240218\x001DS(@P_nF8=9L@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode10 = "[)>@06\x001D9N111003592770\x001D1TQo0>7rU\x001DD240703\x001DS)Wuk3P+e%@@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode11 = "[)>@06\x001D9N111003592770\x001D1T&ZJMaz/\x001DD231126\x001DS@j:AugCzJt@\x0004\x000D";
    private const string UnitedKingdomVariant3Barcode12 = "[)>@06\x001D9N111003592770\x001D1T2;5wEY-\x001DD230404\x001DSHIrQ9QeTyQ@\x0004\x000D";

    private readonly Dictionary<string, IPackIdentifier> _baseIdentifiers;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardCalibratorTestsFromUnitedStates"/> class.
    /// </summary>
    public KeyboardCalibratorTestsFromUnitedStatesPpn()
    {
        _baseIdentifiers = BasePackIdentifiers(
            BaseCalibration().CalibrationData);
    }

    /// <summary>
    /// Test a simple string
    /// </summary>
    [Fact]
    public void UnitedStatesToUnitedStates()
    {
        var calibrator = new Calibrator();
        var loopCount = 0;

        foreach (var token in calibrator.CalibrationTokens())
        {
            calibrator.Calibrate(ConvertToCharacterValues(BaselineCalibrationUsUs()), token);
            loopCount++;
        }

        Assert.Equal(1, loopCount);
    }

    /// <summary>
    /// Test calibration for the error discovered by Lexon Ireland.  In this case, they appeared
    /// to have a US keyboard scanner and a UK or Ireland keyboard, but for some reason the scanner
    /// reports the ASCII29 as a quote character.  It also reports @ as a quote.
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void LexonPpnError()
    {
        PerformParserTest(
            PerformCalibrationTest("Lexon PPN Error").CalibrationData,
            LexonPpnErrorBarcodeData());
    }


    /// <summary>
    /// Test calibration for a Belgian French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianFrench()
    {
        PerformParserTest(
            PerformCalibrationTest("Belgian French").CalibrationData,
            BelgianFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian (Comma) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianComma()
    {
        PerformParserTest(
            PerformCalibrationTest("Belgian (Comma)").CalibrationData,
            BelgianCommaBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian (Period) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBelgianPeriod()
    {
        PerformParserTest(
            PerformCalibrationTest("Belgian (Period)").CalibrationData,
            BelgianPeriodBarcodeData());
    }

    /// <summary>
    /// Test calibration for a French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFrench()
    {
        PerformParserTest(
            PerformCalibrationTest("French").CalibrationData,
            FrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwissFrench()
    {
        PerformParserTest(
            PerformCalibrationTest("Swiss French").CalibrationData,
            SwissFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Croatian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCroatianStandard()
    {
        PerformParserTest(
            PerformCalibrationTest("Croatian (Standard)").CalibrationData,
            CroatianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarian()
    {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian").CalibrationData,
            BulgarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Latin) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianLatin()
    {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Latin)").CalibrationData,
            BulgarianLatinBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic Traditional) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianPhoneticTraditional()
    {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Phonetic Traditional)").CalibrationData,
            BulgarianPhoneticTraditionalBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianPhonetic()
    {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Phonetic)").CalibrationData,
            BulgarianPhoneticBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Typewriter) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToBulgarianTypewriter()
    {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Typewriter)").CalibrationData,
            BulgarianTypewriterBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwedish()
    {
        PerformParserTest(
            PerformCalibrationTest("Swedish").CalibrationData,
            SwedishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwedishWithSami()
    {
        PerformParserTest(
            PerformCalibrationTest("Swedish with Sami").CalibrationData,
            SwedishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreek()
    {
        PerformParserTest(
            PerformCalibrationTest("Greek").CalibrationData,
            GreekBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek (220) computer keyboard layout
    /// </summary>
    [Fact]
    public void ToGreek220()
    {
        // Calibration fails
        var token = PerformCalibrationTest("Greek (220)");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Greek (319) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGreek319()
    {
        PerformParserTest(
            PerformCalibrationTest("Greek (319)").CalibrationData,
            Greek319BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek Polytonic computer keyboard layout
    /// </summary>
    [Fact]
    public void ToGreekPolytonic()
    {
        // Calibration fails
        var token = PerformCalibrationTest("Greek Polytonic");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Czech computer keyboard layout
    /// </summary>
    [Fact]
    public void ToCzech()
    {
        var token = PerformCalibrationTest("Czech");
        // Check warning for German PPN packs not recognised.
        Assert.Contains(token.Warnings, ci => ci.InformationType == CalibrationInformationType.IsoIec15434SyntaxNotRecognised);
    }

    /// <summary>
    /// Test calibration for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCzechQwerty()
    {
        PerformParserTest(
            PerformCalibrationTest("Czech (QWERTY)").CalibrationData,
            CzechQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech Programmers computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToCzechProgrammers()
    {
        PerformParserTest(
            PerformCalibrationTest("Czech Programmers").CalibrationData,
            CzechProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Dutch computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToDutch()
    {
        PerformParserTest(
            PerformCalibrationTest("Dutch").CalibrationData,
            DutchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Estonian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToEstonian()
    {
        PerformParserTest(
            PerformCalibrationTest("Estonian").CalibrationData,
            EstonianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFinnish()
    {
        PerformParserTest(
            PerformCalibrationTest("Finnish").CalibrationData,
            FinnishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToFinnishWithSami()
    {
        PerformParserTest(
            PerformCalibrationTest("Finnish with Sami").CalibrationData,
            FinnishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGerman()
    {
        PerformParserTest(
            PerformCalibrationTest("German").CalibrationData,
            GermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToGermanIbm()
    {
        PerformParserTest(
            PerformCalibrationTest("German (IBM)").CalibrationData,
            GermanIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSwissGerman()
    {
        PerformParserTest(
            PerformCalibrationTest("Swiss German").CalibrationData,
            SwissGermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Danish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToDanish()
    {
        PerformParserTest(
            PerformCalibrationTest("Danish").CalibrationData,
            DanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToHungarian()
    {
        PerformParserTest(
            PerformCalibrationTest("Hungarian").CalibrationData,
            HungarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Hungarian 101-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToHungarian101Key()
    {
        PerformParserTest(
            PerformCalibrationTest("Hungarian 101-key").CalibrationData,
            Hungarian101KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Icelandic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToIcelandic()
    {
        PerformParserTest(
            PerformCalibrationTest("Icelandic").CalibrationData,
            IcelandicBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Irish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToIrish()
    {
        PerformParserTest(
            PerformCalibrationTest("Irish").CalibrationData,
            IrishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Italian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToItalian()
    {
        PerformParserTest(
            PerformCalibrationTest("Italian").CalibrationData,
            ItalianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Italian (142) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToItalian142()
    {
        PerformParserTest(
            PerformCalibrationTest("Italian (142)").CalibrationData,
            Italian142BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    public void ToLatvianStandard()
    {
        // Calibration fails
        var token = PerformCalibrationTest("Latvian (Standard)");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Latvian (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLatvianQwerty()
    {
        PerformParserTest(
            PerformCalibrationTest("Latvian (QWERTY)").CalibrationData,
            LatvianQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLatvian()
    {
        PerformParserTest(
            PerformCalibrationTest("Latvian").CalibrationData,
            LatvianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanian()
    {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian").CalibrationData,
            LithuanianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanianIbm()
    {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian (IBM)").CalibrationData,
            LithuanianIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLithuanianStandard()
    {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian (Standard)").CalibrationData,
            LithuanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbian()
    {
        PerformParserTest(
            PerformCalibrationTest("Sorbian").CalibrationData,
            SorbianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToNorwegianWithSami()
    {
        PerformParserTest(
            PerformCalibrationTest("Norwegian with Sami").CalibrationData,
            NorwegianWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Luxembourgish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToLuxembourgish()
    {
        PerformParserTest(
            PerformCalibrationTest("Luxembourgish").CalibrationData,
            LuxembourgishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToNorwegian()
    {
        PerformParserTest(
            PerformCalibrationTest("Norwegian").CalibrationData,
            NorwegianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 47-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToMaltese47Key()
    {
        PerformParserTest(
            PerformCalibrationTest("Maltese 47-key").CalibrationData,
            Maltese47KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 48-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToMaltese48Key()
    {
        PerformParserTest(
            PerformCalibrationTest("Maltese 48-key").CalibrationData,
            Maltese48KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPolishProgrammers()
    {
        PerformParserTest(
            PerformCalibrationTest("Polish (Programmers)").CalibrationData,
            PolishProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (214) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPolish214()
    {
        PerformParserTest(
            PerformCalibrationTest("Polish (214)").CalibrationData,
            Polish214BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Portuguese computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToPortuguese()
    {
        PerformParserTest(
            PerformCalibrationTest("Portuguese").CalibrationData,
            PortugueseBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianStandard()
    {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Standard)").CalibrationData,
            RomanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianLegacy()
    {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Legacy)").CalibrationData,
            RomanianLegacyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToRomanianProgrammers()
    {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Programmers)").CalibrationData,
            RomanianProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Scottish Gaelic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToScottishGaelic()
    {
        PerformParserTest(
            PerformCalibrationTest("Scottish Gaelic").CalibrationData,
            ScottishGaelicBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovak computer keyboard layout
    /// </summary>
    [Fact]
    public void ToSlovak()
    {
        // Calibration fails
        var token = PerformCalibrationTest("Slovak");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Slovak (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    public void ToSlovakQwerty()
    {
        PerformParserTest(
            PerformCalibrationTest("Slovak (QWERTY)").CalibrationData,
            SlovakQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovenian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSlovenian()
    {
        PerformParserTest(
            PerformCalibrationTest("Slovenian").CalibrationData,
            SlovenianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Spanish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSpanish()
    {
        PerformParserTest(
            PerformCalibrationTest("Spanish").CalibrationData,
            SpanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Spanish Variation computer keyboard layout
    /// </summary>
    [Fact]
    public void ToSpanishVariation()
    {
        // Calibration fails
        var token = PerformCalibrationTest("Spanish Variation");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianStandard()
    {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Standard").CalibrationData,
            SorbianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianExtended()
    {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Extended").CalibrationData,
            SorbianExtendedBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToSorbianStandardLegacy()
    {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Standard (Legacy)").CalibrationData,
            SorbianStandardLegacyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a United Kingdom computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToUnitedKingdom()
    {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom").CalibrationData,
            UnitedKingdomBarcodeData());
    }

    /// <summary>
    /// Test calibration for a United Kingdom Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void ToUnitedKingdomExtended()
    {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom Extended").CalibrationData,
            UnitedKingdomExtendedBarcodeData());
    }

    /// <summary>
    /// Test calibration for ASCII 30 correctly represented
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void Ascii30AsRepresented()
    {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom Variant 1").CalibrationData,
            UnitedKingdomVariant1BarcodeData());
    }

    /// <summary>
    /// Test calibration for ASCII 30 represented as a variant ASCII character
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void Ascii30AsVariantCharacter()
    {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom Variant 2").CalibrationData,
            UnitedKingdomVariant2BarcodeData());
    }

    /// <summary>
    /// Test calibration for ASCII 30 represented as an invariant ASCII character
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Pending>")]
    public void Ascii30AsInvariantCharacter()
    {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom Variant 3").CalibrationData,
            UnitedKingdomVariant3BarcodeData());
    }

    /// <summary>
    /// Performs a calibration test.
    /// </summary>
    /// <param name="layoutName">The name of the computer keyboard layout</param>
    private static CalibrationToken PerformCalibrationTest(string layoutName)
    {
        Debug.WriteLine(layoutName);

        var expectedCalibrations = UnitedStatesExpectedCalibrations();
        var computerKeyboardLayout = UnitedStatesTestData()[layoutName];

        var calibrator = new Calibrator();
        var loopCount = -1;
        CalibrationToken currentToken = default;

        var recognisedDataElements = new List<RecognisedDataElement>
        {
            new(Syntax.Gs1ApplicationIdentifiers, "01"),
            new(Syntax.Gs1ApplicationIdentifiers, "10"),
            new(Syntax.Gs1ApplicationIdentifiers, "17"),
            new(Syntax.Gs1ApplicationIdentifiers, "21"),
            new(Syntax.Gs1ApplicationIdentifiers, "710"),
            new(Syntax.Gs1ApplicationIdentifiers, "711"),
            new(Syntax.Gs1ApplicationIdentifiers, "712"),
            new(Syntax.Gs1ApplicationIdentifiers, "714"),
            new(Syntax.AscMhDataIdentifiers, "9N"),
            new(Syntax.AscMhDataIdentifiers, "1T"),
            new(Syntax.AscMhDataIdentifiers, "D"),
            new(Syntax.AscMhDataIdentifiers, "S")
        };

        calibrator.RecognisedDataElements = recognisedDataElements;

        foreach (var token in calibrator.CalibrationTokens())
        {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0)
            {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(baseLine), currentToken);
                loopCount++;
            }
            else
            {
                if (loopCount < computerKeyboardLayout[baseLine].Count)
                {
                    currentToken = calibrator.Calibrate(
                        ConvertToCharacterValues(computerKeyboardLayout[baseLine][loopCount++]),
                        currentToken);
                }
            }

            foreach (var error in currentToken.Errors)
            {
                Debug.WriteLine(error.Description);
            }
        }

        // Assert that the calibrator calculated the expected calibration.
        Assert.Equal(expectedCalibrations[layoutName], currentToken.CalibrationData?.ToJson() ?? string.Empty);

        Trace.WriteLine(
            $"private const string {layoutName.Replace(" ", "").Replace("(", "").Replace(")", "")}Calibration = " +
            ToLiteral($"\"{calibrator.CalibrationData}\";"));

        return currentToken;

        static string ToLiteral(string input)
        {
            using var writer = new StringWriter();
            using var provider = CodeDomProvider.CreateProvider("CSharp");
            provider.GenerateCodeFromExpression(new System.CodeDom.CodePrimitiveExpression(input), writer, null!);
            return writer.ToString();
        }
    }

    private void PerformParserTest(CalibrationData calibrationData, Dictionary<string, string> scannedData)
    {
        Assert.NotNull(calibrationData);

        var parser = new Parser(calibrationData);

        foreach (var barcodeData in scannedData)
        {
            var identifier = parser.Parse(barcodeData.Value);
            Assert.True(identifier.IsValid);

            var baseIdentifier = _baseIdentifiers[barcodeData.Key];

            Assert.Equal(baseIdentifier.Scheme, identifier.Scheme);
            Assert.Equal(baseIdentifier.ProductCode, identifier.ProductCode);
            Assert.Equal(baseIdentifier.SerialNumber, identifier.SerialNumber);
            Assert.Equal(baseIdentifier.BatchIdentifier, identifier.BatchIdentifier);
            Assert.Equal(baseIdentifier.Expiry, identifier.Expiry);
        }
    }

    /// <summary>
    /// Returns a collection of pack identifiers for the scanner keyboard layout.
    /// </summary>
    /// <param name="calibrationData">Calibration data for the scanner keyboard layout.</param>
    /// <returns>A collection of pack identifiers for the scanner keyboard layout.</returns>
    private static Dictionary<string, IPackIdentifier> BasePackIdentifiers(CalibrationData calibrationData)
    {
        var identifiers = new Dictionary<string, IPackIdentifier>();

        if (calibrationData is null)
        {
            return identifiers;
        }

        var parser = new Parser(calibrationData);

        foreach (var barcodeData in UnitedStatesBarcodeData())
        {
            var identifier = parser.Parse(barcodeData.Value);
            identifiers.Add(barcodeData.Key, identifier);
            Assert.True(identifier.IsValid);
        }

        return identifiers;
    }

    /// <summary>
    /// Performs a calibration test.
    /// </summary>
    private static CalibrationToken BaseCalibration()
    {
        var computerKeyboardLayout = new Dictionary<string, IList<string>>
                                     {
                                         {
                                             UnitedStatesBaseline,
                                             new List<string>()
                                         }
                                     };

        var calibrator = new Calibrator();
        var loopCount = -1;
        CalibrationToken currentToken = default;

        foreach (var token in calibrator.CalibrationTokens())
        {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0)
            {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(baseLine), currentToken);
                loopCount++;
            }
            else
            {
                if (loopCount < computerKeyboardLayout[baseLine].Count)
                {
                    currentToken = calibrator.Calibrate(
                        ConvertToCharacterValues(computerKeyboardLayout[baseLine][loopCount++]),
                        currentToken);
                }
            }

            foreach (var error in currentToken.Errors)
            {
                Debug.WriteLine(error.Description);
            }
        }

        ////System.Diagnostics.Debug.WriteLine(calibrator.CalibrationData);
        return currentToken;
    }

    /// <summary>
    /// Returns test data for testing calibration for a scanner configured as a United States keyboard and
    /// computer keyboard layouts for each European keyboard defined in Windows.
    /// </summary>
    /// <returns>A dictionary of test data.</returns>
    private static Dictionary<string, Dictionary<string, IList<string>>> UnitedStatesTestData()
    {
        var unitedStatesTestData = new Dictionary<string, Dictionary<string, IList<string>>>
                                   {
                                       {
                                           "Lexon PPN Error",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   LexonPpnErrorBaseline,
                                                   new List<string>()
                                               }
                                           }
                                       },
                                       {
                                           "Belgian French",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   BelgianFrenchBaseline,
                                                   new List<string>
                                                   {
                                                       BelgianFrenchDeadKey1, BelgianFrenchDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Belgian (Comma)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   BelgianCommaBaseline,
                                                   new List<string>
                                                   {
                                                       BelgianCommaDeadKey1, BelgianCommaDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Belgian (Period)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   BelgianPeriodBaseline,
                                                   new List<string>
                                                   {
                                                       BelgianPeriodDeadKey1, BelgianPeriodDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "French",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   FrenchBaseline,
                                                   new List<string> { FrenchDeadKey1, FrenchDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Swiss French",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SwissFrenchBaseline,
                                                   new List<string>
                                                   {
                                                       SwissFrenchDeadKey1,
                                                       SwissFrenchDeadKey2,
                                                       SwissFrenchDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Croatian (Standard)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   CroatianStandardBaseline,
                                                   new List<string>
                                                   {
                                                       CroatianStandardDeadKey1,
                                                       CroatianStandardDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Bulgarian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { BulgarianBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Latin)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { BulgarianLatinBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Phonetic Traditional)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { BulgarianPhoneticTraditionalBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Phonetic)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { BulgarianPhoneticBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Typewriter)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { BulgarianTypewriterBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Swedish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SwedishBaseline,
                                                   new List<string>
                                                   {
                                                       SwedishDeadKey1,
                                                       SwedishDeadKey2,
                                                       SwedishDeadKey3,
                                                       SwedishDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Swedish with Sami",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SwedishWithSamiBaseline,
                                                   new List<string>
                                                   {
                                                       SwedishWithSamiDeadKey1,
                                                       SwedishWithSamiDeadKey2,
                                                       SwedishWithSamiDeadKey3,
                                                       SwedishWithSamiDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Greek",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   GreekBaseline,
                                                   new List<string>
                                                   {
                                                       GreekDeadKey1, GreekDeadKey2, GreekDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Greek (220)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { Greek220Baseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Greek (319)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   Greek319Baseline,
                                                   new List<string> { Greek319DeadKey1, Greek319DeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Greek Polytonic",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   GreekPolytonicBaseline,
                                                   new List<string>
                                                   {
                                                       GreekPolytonicDeadKey1,
                                                       GreekPolytonicDeadKey2,
                                                       GreekPolytonicDeadKey3,
                                                       GreekPolytonicDeadKey4,
                                                       GreekPolytonicDeadKey5,
                                                       GreekPolytonicDeadKey6,
                                                       GreekPolytonicDeadKey7
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Czech",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   CzechBaseline,
                                                   new List<string>
                                                   {
                                                       CzechDeadKey1,
                                                       CzechDeadKey2,
                                                       CzechDeadKey3,
                                                       CzechDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Czech (QWERTY)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   CzechQwertyBaseline,
                                                   new List<string>
                                                   {
                                                       CzechQwertyDeadKey1,
                                                       CzechQwertyDeadKey2,
                                                       CzechQwertyDeadKey3,
                                                       CzechQwertyDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Czech Programmers",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { CzechProgrammersBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Dutch",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   DutchBaseline,
                                                   new List<string>
                                                   {
                                                       DutchDeadKey1,
                                                       DutchDeadKey2,
                                                       DutchDeadKey3,
                                                       DutchDeadKey4,
                                                       DutchDeadKey5
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Estonian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   EstonianBaseline,
                                                   new List<string>
                                                   {
                                                       EstonianDeadKey1,
                                                       EstonianDeadKey2,
                                                       EstonianDeadKey3,
                                                       EstonianDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Finnish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   FinnishBaseline,
                                                   new List<string>
                                                   {
                                                       FinnishDeadKey1,
                                                       FinnishDeadKey2,
                                                       FinnishDeadKey3,
                                                       FinnishDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Finnish with Sami",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   FinnishWithSamiBaseline,
                                                   new List<string>
                                                   {
                                                       FinnishWithSamiDeadKey1,
                                                       FinnishWithSamiDeadKey2,
                                                       FinnishWithSamiDeadKey3,
                                                       FinnishWithSamiDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "German",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   GermanBaseline,
                                                   new List<string>
                                                   {
                                                       GermanDeadKey1, GermanDeadKey2, GermanDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "German (IBM)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   GermanIbmBaseline,
                                                   new List<string>
                                                   {
                                                       GermanIbmDeadKey1,
                                                       GermanIbmDeadKey2,
                                                       GermanIbmDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Swiss German",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SwissGermanBaseline,
                                                   new List<string>
                                                   {
                                                       SwissGermanDeadKey1,
                                                       SwissGermanDeadKey2,
                                                       SwissGermanDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Danish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   DanishBaseline,
                                                   new List<string>
                                                   {
                                                       DanishDeadKey1,
                                                       DanishDeadKey2,
                                                       DanishDeadKey3,
                                                       DanishDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Hungarian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { HungarianBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Hungarian 101-key",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { Hungarian101KeyBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Icelandic",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   IcelandicBaseline,
                                                   new List<string>
                                                   {
                                                       IcelandicDeadKey1,
                                                       IcelandicDeadKey2,
                                                       IcelandicDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Irish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { IrishBaseline, new List<string> { IrishDeadKey1 } }
                                           }
                                       },
                                       {
                                           "Italian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { ItalianBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Italian (142)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { Italian142Baseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Latvian (Standard)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   LatvianStandardBaseline,
                                                   new List<string> { LatvianStandardDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Latvian (QWERTY)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   LatvianQwertyBaseline,
                                                   new List<string>
                                                   {
                                                       LatvianQwertyDeadKey1, LatvianQwertyDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Latvian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   LatvianBaseline,
                                                   new List<string> { LatvianDeadKey1, LatvianDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Lithuanian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { LithuanianBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Lithuanian (IBM)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { LithuanianIbmBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Lithuanian (Standard)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { LithuanianStandardBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Sorbian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SorbianBaseline,
                                                   new List<string>
                                                   {
                                                       SorbianDeadKey1, SorbianDeadKey2, SorbianDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Norwegian with Sami",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   NorwegianWithSamiBaseline,
                                                   new List<string>
                                                   {
                                                       NorwegianWithSamiDeadKey1,
                                                       NorwegianWithSamiDeadKey2,
                                                       NorwegianWithSamiDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Luxembourgish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   LuxembourgishBaseline,
                                                   new List<string>
                                                   {
                                                       LuxembourgishDeadKey1,
                                                       LuxembourgishDeadKey2,
                                                       LuxembourgishDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Norwegian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   NorwegianBaseline,
                                                   new List<string>
                                                   {
                                                       NorwegianDeadKey1,
                                                       NorwegianDeadKey2,
                                                       NorwegianDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Maltese 47-key",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { Maltese47KeyBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Maltese 48-key",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { Maltese48KeyBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Polish (Programmers)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   PolishProgrammersBaseline,
                                                   new List<string> { PolishProgrammersDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Polish (214)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   Polish214Baseline,
                                                   new List<string> { Polish214DeadKey1, Polish214DeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Portuguese",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   PortugueseBaseline,
                                                   new List<string>
                                                   {
                                                       PortugueseDeadKey1,
                                                       PortugueseDeadKey2,
                                                       PortugueseDeadKey3,
                                                       PortugueseDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Romanian (Standard)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { RomanianStandardBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Romanian (Legacy)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { RomanianLegacyBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Romanian (Programmers)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { RomanianProgrammersBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Scottish Gaelic",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   ScottishGaelicBaseline,
                                                   new List<string>
                                                   {
                                                       ScottishGaelicDeadKey1, ScottishGaelicDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Slovak",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { SlovakBaseline, new List<string> { SlovakDeadKey1 } }
                                           }
                                       },
                                       {
                                           "Slovak (QWERTY)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SlovakQwertyBaseline,
                                                   new List<string>
                                                   {
                                                       SlovakQwertyDeadKey1,
                                                       SlovakQwertyDeadKey2,
                                                       SlovakQwertyDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Slovenian",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SlovenianBaseline,
                                                   new List<string> { SlovenianDeadKey1, SlovenianDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Spanish",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SpanishBaseline,
                                                   new List<string>
                                                   {
                                                       SpanishDeadKey1,
                                                       SpanishDeadKey2,
                                                       SpanishDeadKey3,
                                                       SpanishDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Spanish Variation",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { SpanishVariationBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "Sorbian Standard",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SorbianStandardBaseline,
                                                   new List<string>
                                                   {
                                                       SorbianStandardDeadKey1,
                                                       SorbianStandardDeadKey2,
                                                       SorbianStandardDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Sorbian Extended",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SorbianExtendedBaseline,
                                                   new List<string>
                                                   {
                                                       SorbianExtendedDeadKey1,
                                                       SorbianExtendedDeadKey2,
                                                       SorbianExtendedDeadKey3,
                                                       SorbianExtendedDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Sorbian Standard (Legacy)",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   SorbianStandardLegacyBaseline,
                                                   new List<string>
                                                   {
                                                       SorbianStandardLegacyDeadKey1,
                                                       SorbianStandardLegacyDeadKey2,
                                                       SorbianStandardLegacyDeadKey3,
                                                       SorbianStandardLegacyDeadKey4
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "United Kingdom",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { UnitedKingdomBaseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "United Kingdom Extended",
                                           new Dictionary<string, IList<string>>
                                           {
                                               {
                                                   UnitedKingdomExtendedBaseline,
                                                   new List<string> { UnitedKingdomExtendedDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "United Kingdom Variant 1",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { UnitedKingdomVariant1Baseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "United Kingdom Variant 2",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { UnitedKingdomVariant2Baseline, new List<string>() }
                                           }
                                       },
                                       {
                                           "United Kingdom Variant 3",
                                           new Dictionary<string, IList<string>>
                                           {
                                               { UnitedKingdomVariant3Baseline, new List<string>() }
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
    private static Dictionary<string, string> UnitedStatesExpectedCalibrations()
    {
        var unitedStatesTestCalibrations = new Dictionary<string, string>
                                           {
                                               { "Lexon PPN Error", Calibrations.LexonPpnErrorCalibration },
                                               { "Belgian French", Calibrations.BelgianFrenchCalibration },
                                               { "Belgian (Comma)", Calibrations.BelgianCommaCalibration },
                                               { "Belgian (Period)", Calibrations.BelgianPeriodCalibration },
                                               { "French", Calibrations.FrenchCalibration },
                                               { "Swiss French", Calibrations.SwissFrenchCalibration },
                                               { "Croatian (Standard)", Calibrations.CroatianStandardCalibration },
                                               { "Bulgarian", Calibrations.BulgarianCalibration },
                                               { "Bulgarian (Latin)", Calibrations.BulgarianLatinCalibration },
                                               { "Bulgarian (Phonetic Traditional)", Calibrations.BulgarianPhoneticTraditionalCalibration },
                                               { "Bulgarian (Phonetic)", Calibrations.BulgarianPhoneticCalibration },
                                               { "Bulgarian (Typewriter)", Calibrations.BulgarianTypewriterCalibration },
                                               { "Swedish", Calibrations.SwedishCalibration },
                                               { "Swedish with Sami", Calibrations.SwedishWithSamiCalibration },
                                               { "Greek", Calibrations.GreekCalibration },
                                               { "Greek (220)", Calibrations.Greek220Calibration },
                                               { "Greek (319)", Calibrations.Greek319Calibration },
                                               { "Greek Polytonic", Calibrations.GreekPolytonicCalibration },
                                               { "Czech", Calibrations.CzechCalibration },
                                               { "Czech (QWERTY)", Calibrations.CzechQwertyCalibration },
                                               { "Czech Programmers", Calibrations.CzechProgrammersCalibration },
                                               { "Dutch", Calibrations.DutchCalibration },
                                               { "Estonian", Calibrations.EstonianCalibration },
                                               { "Finnish", Calibrations.FinnishCalibration },
                                               { "Finnish with Sami", Calibrations.FinnishWithSamiCalibration },
                                               { "German", Calibrations.GermanCalibration },
                                               { "German (IBM)", Calibrations.GermanIbmCalibration },
                                               { "Swiss German", Calibrations.SwissGermanCalibration },
                                               { "Danish", Calibrations.DanishCalibration },
                                               { "Hungarian", Calibrations.HungarianCalibration },
                                               { "Hungarian 101-key", Calibrations.Hungarian101KeyCalibration },
                                               { "Icelandic", Calibrations.IcelandicCalibration },
                                               { "Irish", Calibrations.IrishCalibration },
                                               { "Italian", Calibrations.ItalianCalibration },
                                               { "Italian (142)", Calibrations.Italian142Calibration },
                                               { "Latvian (Standard)", Calibrations.LatvianStandardCalibration },
                                               { "Latvian (QWERTY)", Calibrations.LatvianQwertyCalibration },
                                               { "Latvian", Calibrations.LatvianCalibration },
                                               { "Lithuanian", Calibrations.LithuanianCalibration },
                                               { "Lithuanian (IBM)", Calibrations.LithuanianIbmCalibration },
                                               { "Lithuanian (Standard)", Calibrations.LithuanianStandardCalibration },
                                               { "Sorbian", Calibrations.SorbianCalibration },
                                               { "Norwegian with Sami", Calibrations.NorwegianWithSamiCalibration },
                                               { "Luxembourgish", Calibrations.LuxembourgishCalibration },
                                               { "Norwegian", Calibrations.NorwegianCalibration },
                                               { "Maltese 47-key", Calibrations.Maltese47KeyCalibration },
                                               { "Maltese 48-key", Calibrations.Maltese48KeyCalibration },
                                               { "Polish (Programmers)", Calibrations.PolishProgrammersCalibration },
                                               { "Polish (214)", Calibrations.Polish214Calibration },
                                               { "Portuguese", Calibrations.PortugueseCalibration },
                                               { "Romanian (Standard)", Calibrations.RomanianStandardCalibration },
                                               { "Romanian (Legacy)", Calibrations.RomanianLegacyCalibration },
                                               { "Romanian (Programmers)", Calibrations.RomanianProgrammersCalibration },
                                               { "Scottish Gaelic", Calibrations.ScottishGaelicCalibration },
                                               { "Slovak", Calibrations.SlovakCalibration },
                                               { "Slovak (QWERTY)", Calibrations.SlovakQwertyCalibration },
                                               { "Slovenian", Calibrations.SlovenianCalibration },
                                               { "Spanish", Calibrations.SpanishCalibration },
                                               { "Spanish Variation", Calibrations.SpanishVariationCalibration },
                                               { "Sorbian Standard", Calibrations.SorbianStandardCalibration },
                                               { "Sorbian Extended", Calibrations.SorbianExtendedCalibration },
                                               { "Sorbian Standard (Legacy)", Calibrations.SorbianStandardLegacyCalibration },
                                               { "United Kingdom", Calibrations.UnitedKingdomCalibration },
                                               { "United Kingdom Extended", Calibrations.UnitedKingdomExtendedCalibration },
                                               { "United Kingdom Variant 1", Calibrations.UnitedKingdomVariant1Calibration },
                                               { "United Kingdom Variant 2", Calibrations.UnitedKingdomVariant2Calibration },
                                               { "United Kingdom Variant 3", Calibrations.UnitedKingdomVariant3Calibration }
                                           };
        return unitedStatesTestCalibrations;
    }

    /// <summary>
    /// Returns the barcode data as entered using a United States keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedStatesBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedStatesBarcode1 },
                   { "Barcode2", UnitedStatesBarcode2 },
                   { "Barcode3", UnitedStatesBarcode3 },
                   { "Barcode4", UnitedStatesBarcode4 },
                   { "Barcode5", UnitedStatesBarcode5 },
                   { "Barcode6", UnitedStatesBarcode6 },
                   { "Barcode7", UnitedStatesBarcode7 },
                   { "Barcode8", UnitedStatesBarcode8 },
                   { "Barcode9", UnitedStatesBarcode9 },
                   { "Barcode10", UnitedStatesBarcode10 },
                   { "Barcode11", UnitedStatesBarcode11 },
                   { "Barcode12", UnitedStatesBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for the Lexon PPN error keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LexonPpnErrorBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LexonPpnErrorBarcode1 },
                   { "Barcode2", LexonPpnErrorBarcode2 },
                   { "Barcode3", LexonPpnErrorBarcode3 },
                   { "Barcode4", LexonPpnErrorBarcode4 },
                   { "Barcode5", LexonPpnErrorBarcode5 },
                   { "Barcode6", LexonPpnErrorBarcode6 },
                   { "Barcode7", LexonPpnErrorBarcode7 },
                   { "Barcode8", LexonPpnErrorBarcode8 },
                   { "Barcode9", LexonPpnErrorBarcode9 },
                   { "Barcode10", LexonPpnErrorBarcode10 },
                   { "Barcode11", LexonPpnErrorBarcode11 },
                   { "Barcode12", LexonPpnErrorBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian French keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianFrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BelgianFrenchBarcode1 },
                   { "Barcode2", BelgianFrenchBarcode2 },
                   { "Barcode3", BelgianFrenchBarcode3 },
                   { "Barcode4", BelgianFrenchBarcode4 },
                   { "Barcode5", BelgianFrenchBarcode5 },
                   { "Barcode6", BelgianFrenchBarcode6 },
                   { "Barcode7", BelgianFrenchBarcode7 },
                   { "Barcode8", BelgianFrenchBarcode8 },
                   { "Barcode9", BelgianFrenchBarcode9 },
                   { "Barcode10", BelgianFrenchBarcode10 },
                   { "Barcode11", BelgianFrenchBarcode11 },
                   { "Barcode12", BelgianFrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian (Comma) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianCommaBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BelgianCommaBarcode1 },
                   { "Barcode2", BelgianCommaBarcode2 },
                   { "Barcode3", BelgianCommaBarcode3 },
                   { "Barcode4", BelgianCommaBarcode4 },
                   { "Barcode5", BelgianCommaBarcode5 },
                   { "Barcode6", BelgianCommaBarcode6 },
                   { "Barcode7", BelgianCommaBarcode7 },
                   { "Barcode8", BelgianCommaBarcode8 },
                   { "Barcode9", BelgianCommaBarcode9 },
                   { "Barcode10", BelgianCommaBarcode10 },
                   { "Barcode11", BelgianCommaBarcode11 },
                   { "Barcode12", BelgianCommaBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian (Period) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianPeriodBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BelgianPeriodBarcode1 },
                   { "Barcode2", BelgianPeriodBarcode2 },
                   { "Barcode3", BelgianPeriodBarcode3 },
                   { "Barcode4", BelgianPeriodBarcode4 },
                   { "Barcode5", BelgianPeriodBarcode5 },
                   { "Barcode6", BelgianPeriodBarcode6 },
                   { "Barcode7", BelgianPeriodBarcode7 },
                   { "Barcode8", BelgianPeriodBarcode8 },
                   { "Barcode9", BelgianPeriodBarcode9 },
                   { "Barcode10", BelgianPeriodBarcode10 },
                   { "Barcode11", BelgianPeriodBarcode11 },
                   { "Barcode12", BelgianPeriodBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a French keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", FrenchBarcode1 },
                   { "Barcode2", FrenchBarcode2 },
                   { "Barcode3", FrenchBarcode3 },
                   { "Barcode4", FrenchBarcode4 },
                   { "Barcode5", FrenchBarcode5 },
                   { "Barcode6", FrenchBarcode6 },
                   { "Barcode7", FrenchBarcode7 },
                   { "Barcode8", FrenchBarcode8 },
                   { "Barcode9", FrenchBarcode9 },
                   { "Barcode10", FrenchBarcode10 },
                   { "Barcode11", FrenchBarcode11 },
                   { "Barcode12", FrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a SwissFrench keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwissFrenchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SwissFrenchBarcode1 },
                   { "Barcode2", SwissFrenchBarcode2 },
                   { "Barcode3", SwissFrenchBarcode3 },
                   { "Barcode4", SwissFrenchBarcode4 },
                   { "Barcode5", SwissFrenchBarcode5 },
                   { "Barcode6", SwissFrenchBarcode6 },
                   { "Barcode7", SwissFrenchBarcode7 },
                   { "Barcode8", SwissFrenchBarcode8 },
                   { "Barcode9", SwissFrenchBarcode9 },
                   { "Barcode10", SwissFrenchBarcode10 },
                   { "Barcode11", SwissFrenchBarcode11 },
                   { "Barcode12", SwissFrenchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Croatian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CroatianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", CroatianStandardBarcode1 },
                   { "Barcode2", CroatianStandardBarcode2 },
                   { "Barcode3", CroatianStandardBarcode3 },
                   { "Barcode4", CroatianStandardBarcode4 },
                   { "Barcode5", CroatianStandardBarcode5 },
                   { "Barcode6", CroatianStandardBarcode6 },
                   { "Barcode7", CroatianStandardBarcode7 },
                   { "Barcode8", CroatianStandardBarcode8 },
                   { "Barcode9", CroatianStandardBarcode9 },
                   { "Barcode10", CroatianStandardBarcode10 },
                   { "Barcode11", CroatianStandardBarcode11 },
                   { "Barcode12", CroatianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BulgarianBarcode1 },
                   { "Barcode2", BulgarianBarcode2 },
                   { "Barcode3", BulgarianBarcode3 },
                   { "Barcode4", BulgarianBarcode4 },
                   { "Barcode5", BulgarianBarcode5 },
                   { "Barcode6", BulgarianBarcode6 },
                   { "Barcode7", BulgarianBarcode7 },
                   { "Barcode8", BulgarianBarcode8 },
                   { "Barcode9", BulgarianBarcode9 },
                   { "Barcode10", BulgarianBarcode10 },
                   { "Barcode11", BulgarianBarcode11 },
                   { "Barcode12", BulgarianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Latin) keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianLatinBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BulgarianLatinBarcode1 },
                   { "Barcode2", BulgarianLatinBarcode2 },
                   { "Barcode3", BulgarianLatinBarcode3 },
                   { "Barcode4", BulgarianLatinBarcode4 },
                   { "Barcode5", BulgarianLatinBarcode5 },
                   { "Barcode6", BulgarianLatinBarcode6 },
                   { "Barcode7", BulgarianLatinBarcode7 },
                   { "Barcode8", BulgarianLatinBarcode8 },
                   { "Barcode9", BulgarianLatinBarcode9 },
                   { "Barcode10", BulgarianLatinBarcode10 },
                   { "Barcode11", BulgarianLatinBarcode11 },
                   { "Barcode12", BulgarianLatinBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Phonetic Traditional) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianPhoneticTraditionalBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BulgarianPhoneticTraditionalBarcode1 },
                   { "Barcode2", BulgarianPhoneticTraditionalBarcode2 },
                   { "Barcode3", BulgarianPhoneticTraditionalBarcode3 },
                   { "Barcode4", BulgarianPhoneticTraditionalBarcode4 },
                   { "Barcode5", BulgarianPhoneticTraditionalBarcode5 },
                   { "Barcode6", BulgarianPhoneticTraditionalBarcode6 },
                   { "Barcode7", BulgarianPhoneticTraditionalBarcode7 },
                   { "Barcode8", BulgarianPhoneticTraditionalBarcode8 },
                   { "Barcode9", BulgarianPhoneticTraditionalBarcode9 },
                   { "Barcode10", BulgarianPhoneticTraditionalBarcode10 },
                   { "Barcode11", BulgarianPhoneticTraditionalBarcode11 },
                   { "Barcode12", BulgarianPhoneticTraditionalBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianPhoneticBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BulgarianPhoneticBarcode1 },
                   { "Barcode2", BulgarianPhoneticBarcode2 },
                   { "Barcode3", BulgarianPhoneticBarcode3 },
                   { "Barcode4", BulgarianPhoneticBarcode4 },
                   { "Barcode5", BulgarianPhoneticBarcode5 },
                   { "Barcode6", BulgarianPhoneticBarcode6 },
                   { "Barcode7", BulgarianPhoneticBarcode7 },
                   { "Barcode8", BulgarianPhoneticBarcode8 },
                   { "Barcode9", BulgarianPhoneticBarcode9 },
                   { "Barcode10", BulgarianPhoneticBarcode10 },
                   { "Barcode11", BulgarianPhoneticBarcode11 },
                   { "Barcode12", BulgarianPhoneticBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Bulgarian (Typewriter) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BulgarianTypewriterBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", BulgarianTypewriterBarcode1 },
                   { "Barcode2", BulgarianTypewriterBarcode2 },
                   { "Barcode3", BulgarianTypewriterBarcode3 },
                   { "Barcode4", BulgarianTypewriterBarcode4 },
                   { "Barcode5", BulgarianTypewriterBarcode5 },
                   { "Barcode6", BulgarianTypewriterBarcode6 },
                   { "Barcode7", BulgarianTypewriterBarcode7 },
                   { "Barcode8", BulgarianTypewriterBarcode8 },
                   { "Barcode9", BulgarianTypewriterBarcode9 },
                   { "Barcode10", BulgarianTypewriterBarcode10 },
                   { "Barcode11", BulgarianTypewriterBarcode11 },
                   { "Barcode12", BulgarianTypewriterBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swedish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwedishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SwedishBarcode1 },
                   { "Barcode2", SwedishBarcode2 },
                   { "Barcode3", SwedishBarcode3 },
                   { "Barcode4", SwedishBarcode4 },
                   { "Barcode5", SwedishBarcode5 },
                   { "Barcode6", SwedishBarcode6 },
                   { "Barcode7", SwedishBarcode7 },
                   { "Barcode8", SwedishBarcode8 },
                   { "Barcode9", SwedishBarcode9 },
                   { "Barcode10", SwedishBarcode10 },
                   { "Barcode11", SwedishBarcode11 },
                   { "Barcode12", SwedishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swedish with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwedishWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SwedishWithSamiBarcode1 },
                   { "Barcode2", SwedishWithSamiBarcode2 },
                   { "Barcode3", SwedishWithSamiBarcode3 },
                   { "Barcode4", SwedishWithSamiBarcode4 },
                   { "Barcode5", SwedishWithSamiBarcode5 },
                   { "Barcode6", SwedishWithSamiBarcode6 },
                   { "Barcode7", SwedishWithSamiBarcode7 },
                   { "Barcode8", SwedishWithSamiBarcode8 },
                   { "Barcode9", SwedishWithSamiBarcode9 },
                   { "Barcode10", SwedishWithSamiBarcode10 },
                   { "Barcode11", SwedishWithSamiBarcode11 },
                   { "Barcode12", SwedishWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Greek computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GreekBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", GreekBarcode1 },
                   { "Barcode2", GreekBarcode2 },
                   { "Barcode3", GreekBarcode3 },
                   { "Barcode4", GreekBarcode4 },
                   { "Barcode5", GreekBarcode5 },
                   { "Barcode6", GreekBarcode6 },
                   { "Barcode7", GreekBarcode7 },
                   { "Barcode8", GreekBarcode8 },
                   { "Barcode9", GreekBarcode9 },
                   { "Barcode10", GreekBarcode10 },
                   { "Barcode11", GreekBarcode11 },
                   { "Barcode12", GreekBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Greek (319) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Greek319BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Greek319Barcode1 },
                   { "Barcode2", Greek319Barcode2 },
                   { "Barcode3", Greek319Barcode3 },
                   { "Barcode4", Greek319Barcode4 },
                   { "Barcode5", Greek319Barcode5 },
                   { "Barcode6", Greek319Barcode6 },
                   { "Barcode7", Greek319Barcode7 },
                   { "Barcode8", Greek319Barcode8 },
                   { "Barcode9", Greek319Barcode9 },
                   { "Barcode10", Greek319Barcode10 },
                   { "Barcode11", Greek319Barcode11 },
                   { "Barcode12", Greek319Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", CzechQwertyBarcode1 },
                   { "Barcode2", CzechQwertyBarcode2 },
                   { "Barcode3", CzechQwertyBarcode3 },
                   { "Barcode4", CzechQwertyBarcode4 },
                   { "Barcode5", CzechQwertyBarcode5 },
                   { "Barcode6", CzechQwertyBarcode6 },
                   { "Barcode7", CzechQwertyBarcode7 },
                   { "Barcode8", CzechQwertyBarcode8 },
                   { "Barcode9", CzechQwertyBarcode9 },
                   { "Barcode10", CzechQwertyBarcode10 },
                   { "Barcode11", CzechQwertyBarcode11 },
                   { "Barcode12", CzechQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech Programmers computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", CzechProgrammersBarcode1 },
                   { "Barcode2", CzechProgrammersBarcode2 },
                   { "Barcode3", CzechProgrammersBarcode3 },
                   { "Barcode4", CzechProgrammersBarcode4 },
                   { "Barcode5", CzechProgrammersBarcode5 },
                   { "Barcode6", CzechProgrammersBarcode6 },
                   { "Barcode7", CzechProgrammersBarcode7 },
                   { "Barcode8", CzechProgrammersBarcode8 },
                   { "Barcode9", CzechProgrammersBarcode9 },
                   { "Barcode10", CzechProgrammersBarcode10 },
                   { "Barcode11", CzechProgrammersBarcode11 },
                   { "Barcode12", CzechProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Dutch computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> DutchBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", DutchBarcode1 },
                   { "Barcode2", DutchBarcode2 },
                   { "Barcode3", DutchBarcode3 },
                   { "Barcode4", DutchBarcode4 },
                   { "Barcode5", DutchBarcode5 },
                   { "Barcode6", DutchBarcode6 },
                   { "Barcode7", DutchBarcode7 },
                   { "Barcode8", DutchBarcode8 },
                   { "Barcode9", DutchBarcode9 },
                   { "Barcode10", DutchBarcode10 },
                   { "Barcode11", DutchBarcode11 },
                   { "Barcode12", DutchBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Estonian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> EstonianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", EstonianBarcode1 },
                   { "Barcode2", EstonianBarcode2 },
                   { "Barcode3", EstonianBarcode3 },
                   { "Barcode4", EstonianBarcode4 },
                   { "Barcode5", EstonianBarcode5 },
                   { "Barcode6", EstonianBarcode6 },
                   { "Barcode7", EstonianBarcode7 },
                   { "Barcode8", EstonianBarcode8 },
                   { "Barcode9", EstonianBarcode9 },
                   { "Barcode10", EstonianBarcode10 },
                   { "Barcode11", EstonianBarcode11 },
                   { "Barcode12", EstonianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Finnish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FinnishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", FinnishBarcode1 },
                   { "Barcode2", FinnishBarcode2 },
                   { "Barcode3", FinnishBarcode3 },
                   { "Barcode4", FinnishBarcode4 },
                   { "Barcode5", FinnishBarcode5 },
                   { "Barcode6", FinnishBarcode6 },
                   { "Barcode7", FinnishBarcode7 },
                   { "Barcode8", FinnishBarcode8 },
                   { "Barcode9", FinnishBarcode9 },
                   { "Barcode10", FinnishBarcode10 },
                   { "Barcode11", FinnishBarcode11 },
                   { "Barcode12", FinnishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Finnish with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> FinnishWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", FinnishWithSamiBarcode1 },
                   { "Barcode2", FinnishWithSamiBarcode2 },
                   { "Barcode3", FinnishWithSamiBarcode3 },
                   { "Barcode4", FinnishWithSamiBarcode4 },
                   { "Barcode5", FinnishWithSamiBarcode5 },
                   { "Barcode6", FinnishWithSamiBarcode6 },
                   { "Barcode7", FinnishWithSamiBarcode7 },
                   { "Barcode8", FinnishWithSamiBarcode8 },
                   { "Barcode9", FinnishWithSamiBarcode9 },
                   { "Barcode10", FinnishWithSamiBarcode10 },
                   { "Barcode11", FinnishWithSamiBarcode11 },
                   { "Barcode12", FinnishWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a German computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GermanBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", GermanBarcode1 },
                   { "Barcode2", GermanBarcode2 },
                   { "Barcode3", GermanBarcode3 },
                   { "Barcode4", GermanBarcode4 },
                   { "Barcode5", GermanBarcode5 },
                   { "Barcode6", GermanBarcode6 },
                   { "Barcode7", GermanBarcode7 },
                   { "Barcode8", GermanBarcode8 },
                   { "Barcode9", GermanBarcode9 },
                   { "Barcode10", GermanBarcode10 },
                   { "Barcode11", GermanBarcode11 },
                   { "Barcode12", GermanBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a German (IBM) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> GermanIbmBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", GermanIbmBarcode1 },
                   { "Barcode2", GermanIbmBarcode2 },
                   { "Barcode3", GermanIbmBarcode3 },
                   { "Barcode4", GermanIbmBarcode4 },
                   { "Barcode5", GermanIbmBarcode5 },
                   { "Barcode6", GermanIbmBarcode6 },
                   { "Barcode7", GermanIbmBarcode7 },
                   { "Barcode8", GermanIbmBarcode8 },
                   { "Barcode9", GermanIbmBarcode9 },
                   { "Barcode10", GermanIbmBarcode10 },
                   { "Barcode11", GermanIbmBarcode11 },
                   { "Barcode12", GermanIbmBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Swiss German computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SwissGermanBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SwissGermanBarcode1 },
                   { "Barcode2", SwissGermanBarcode2 },
                   { "Barcode3", SwissGermanBarcode3 },
                   { "Barcode4", SwissGermanBarcode4 },
                   { "Barcode5", SwissGermanBarcode5 },
                   { "Barcode6", SwissGermanBarcode6 },
                   { "Barcode7", SwissGermanBarcode7 },
                   { "Barcode8", SwissGermanBarcode8 },
                   { "Barcode9", SwissGermanBarcode9 },
                   { "Barcode10", SwissGermanBarcode10 },
                   { "Barcode11", SwissGermanBarcode11 },
                   { "Barcode12", SwissGermanBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Danish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> DanishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", DanishBarcode1 },
                   { "Barcode2", DanishBarcode2 },
                   { "Barcode3", DanishBarcode3 },
                   { "Barcode4", DanishBarcode4 },
                   { "Barcode5", DanishBarcode5 },
                   { "Barcode6", DanishBarcode6 },
                   { "Barcode7", DanishBarcode7 },
                   { "Barcode8", DanishBarcode8 },
                   { "Barcode9", DanishBarcode9 },
                   { "Barcode10", DanishBarcode10 },
                   { "Barcode11", DanishBarcode11 },
                   { "Barcode12", DanishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Hungarian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> HungarianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", HungarianBarcode1 },
                   { "Barcode2", HungarianBarcode2 },
                   { "Barcode3", HungarianBarcode3 },
                   { "Barcode4", HungarianBarcode4 },
                   { "Barcode5", HungarianBarcode5 },
                   { "Barcode6", HungarianBarcode6 },
                   { "Barcode7", HungarianBarcode7 },
                   { "Barcode8", HungarianBarcode8 },
                   { "Barcode9", HungarianBarcode9 },
                   { "Barcode10", HungarianBarcode10 },
                   { "Barcode11", HungarianBarcode11 },
                   { "Barcode12", HungarianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Hungarian101 computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Hungarian101KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Hungarian101KeyBarcode1 },
                   { "Barcode2", Hungarian101KeyBarcode2 },
                   { "Barcode3", Hungarian101KeyBarcode3 },
                   { "Barcode4", Hungarian101KeyBarcode4 },
                   { "Barcode5", Hungarian101KeyBarcode5 },
                   { "Barcode6", Hungarian101KeyBarcode6 },
                   { "Barcode7", Hungarian101KeyBarcode7 },
                   { "Barcode8", Hungarian101KeyBarcode8 },
                   { "Barcode9", Hungarian101KeyBarcode9 },
                   { "Barcode10", Hungarian101KeyBarcode10 },
                   { "Barcode11", Hungarian101KeyBarcode11 },
                   { "Barcode12", Hungarian101KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Icelandic computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IcelandicBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", IcelandicBarcode1 },
                   { "Barcode2", IcelandicBarcode2 },
                   { "Barcode3", IcelandicBarcode3 },
                   { "Barcode4", IcelandicBarcode4 },
                   { "Barcode5", IcelandicBarcode5 },
                   { "Barcode6", IcelandicBarcode6 },
                   { "Barcode7", IcelandicBarcode7 },
                   { "Barcode8", IcelandicBarcode8 },
                   { "Barcode9", IcelandicBarcode9 },
                   { "Barcode10", IcelandicBarcode10 },
                   { "Barcode11", IcelandicBarcode11 },
                   { "Barcode12", IcelandicBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Irish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IrishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", IrishBarcode1 },
                   { "Barcode2", IrishBarcode2 },
                   { "Barcode3", IrishBarcode3 },
                   { "Barcode4", IrishBarcode4 },
                   { "Barcode5", IrishBarcode5 },
                   { "Barcode6", IrishBarcode6 },
                   { "Barcode7", IrishBarcode7 },
                   { "Barcode8", IrishBarcode8 },
                   { "Barcode9", IrishBarcode9 },
                   { "Barcode10", IrishBarcode10 },
                   { "Barcode11", IrishBarcode11 },
                   { "Barcode12", IrishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Italian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> ItalianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", ItalianBarcode1 },
                   { "Barcode2", ItalianBarcode2 },
                   { "Barcode3", ItalianBarcode3 },
                   { "Barcode4", ItalianBarcode4 },
                   { "Barcode5", ItalianBarcode5 },
                   { "Barcode6", ItalianBarcode6 },
                   { "Barcode7", ItalianBarcode7 },
                   { "Barcode8", ItalianBarcode8 },
                   { "Barcode9", ItalianBarcode9 },
                   { "Barcode10", ItalianBarcode10 },
                   { "Barcode11", ItalianBarcode11 },
                   { "Barcode12", ItalianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Italian (142) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Italian142BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Italian142Barcode1 },
                   { "Barcode2", Italian142Barcode2 },
                   { "Barcode3", Italian142Barcode3 },
                   { "Barcode4", Italian142Barcode4 },
                   { "Barcode5", Italian142Barcode5 },
                   { "Barcode6", Italian142Barcode6 },
                   { "Barcode7", Italian142Barcode7 },
                   { "Barcode8", Italian142Barcode8 },
                   { "Barcode9", Italian142Barcode9 },
                   { "Barcode10", Italian142Barcode10 },
                   { "Barcode11", Italian142Barcode11 },
                   { "Barcode12", Italian142Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Latvian (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LatvianQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LatvianQwertyBarcode1 },
                   { "Barcode2", LatvianQwertyBarcode2 },
                   { "Barcode3", LatvianQwertyBarcode3 },
                   { "Barcode4", LatvianQwertyBarcode4 },
                   { "Barcode5", LatvianQwertyBarcode5 },
                   { "Barcode6", LatvianQwertyBarcode6 },
                   { "Barcode7", LatvianQwertyBarcode7 },
                   { "Barcode8", LatvianQwertyBarcode8 },
                   { "Barcode9", LatvianQwertyBarcode9 },
                   { "Barcode10", LatvianQwertyBarcode10 },
                   { "Barcode11", LatvianQwertyBarcode11 },
                   { "Barcode12", LatvianQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Latvian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LatvianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LatvianBarcode1 },
                   { "Barcode2", LatvianBarcode2 },
                   { "Barcode3", LatvianBarcode3 },
                   { "Barcode4", LatvianBarcode4 },
                   { "Barcode5", LatvianBarcode5 },
                   { "Barcode6", LatvianBarcode6 },
                   { "Barcode7", LatvianBarcode7 },
                   { "Barcode8", LatvianBarcode8 },
                   { "Barcode9", LatvianBarcode9 },
                   { "Barcode10", LatvianBarcode10 },
                   { "Barcode11", LatvianBarcode11 },
                   { "Barcode12", LatvianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LithuanianBarcode1 },
                   { "Barcode2", LithuanianBarcode2 },
                   { "Barcode3", LithuanianBarcode3 },
                   { "Barcode4", LithuanianBarcode4 },
                   { "Barcode5", LithuanianBarcode5 },
                   { "Barcode6", LithuanianBarcode6 },
                   { "Barcode7", LithuanianBarcode7 },
                   { "Barcode8", LithuanianBarcode8 },
                   { "Barcode9", LithuanianBarcode9 },
                   { "Barcode10", LithuanianBarcode10 },
                   { "Barcode11", LithuanianBarcode11 },
                   { "Barcode12", LithuanianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian IBM computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianIbmBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LithuanianIbmBarcode1 },
                   { "Barcode2", LithuanianIbmBarcode2 },
                   { "Barcode3", LithuanianIbmBarcode3 },
                   { "Barcode4", LithuanianIbmBarcode4 },
                   { "Barcode5", LithuanianIbmBarcode5 },
                   { "Barcode6", LithuanianIbmBarcode6 },
                   { "Barcode7", LithuanianIbmBarcode7 },
                   { "Barcode8", LithuanianIbmBarcode8 },
                   { "Barcode9", LithuanianIbmBarcode9 },
                   { "Barcode10", LithuanianIbmBarcode10 },
                   { "Barcode11", LithuanianIbmBarcode11 },
                   { "Barcode12", LithuanianIbmBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Lithuanian Standard computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LithuanianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LithuanianStandardBarcode1 },
                   { "Barcode2", LithuanianStandardBarcode2 },
                   { "Barcode3", LithuanianStandardBarcode3 },
                   { "Barcode4", LithuanianStandardBarcode4 },
                   { "Barcode5", LithuanianStandardBarcode5 },
                   { "Barcode6", LithuanianStandardBarcode6 },
                   { "Barcode7", LithuanianStandardBarcode7 },
                   { "Barcode8", LithuanianStandardBarcode8 },
                   { "Barcode9", LithuanianStandardBarcode9 },
                   { "Barcode10", LithuanianStandardBarcode10 },
                   { "Barcode11", LithuanianStandardBarcode11 },
                   { "Barcode12", LithuanianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Norwegian with Sami computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> NorwegianWithSamiBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", NorwegianWithSamiBarcode1 },
                   { "Barcode2", NorwegianWithSamiBarcode2 },
                   { "Barcode3", NorwegianWithSamiBarcode3 },
                   { "Barcode4", NorwegianWithSamiBarcode4 },
                   { "Barcode5", NorwegianWithSamiBarcode5 },
                   { "Barcode6", NorwegianWithSamiBarcode6 },
                   { "Barcode7", NorwegianWithSamiBarcode7 },
                   { "Barcode8", NorwegianWithSamiBarcode8 },
                   { "Barcode9", NorwegianWithSamiBarcode9 },
                   { "Barcode10", NorwegianWithSamiBarcode10 },
                   { "Barcode11", NorwegianWithSamiBarcode11 },
                   { "Barcode12", NorwegianWithSamiBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Luxembourgish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LuxembourgishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LuxembourgishBarcode1 },
                   { "Barcode2", LuxembourgishBarcode2 },
                   { "Barcode3", LuxembourgishBarcode3 },
                   { "Barcode4", LuxembourgishBarcode4 },
                   { "Barcode5", LuxembourgishBarcode5 },
                   { "Barcode6", LuxembourgishBarcode6 },
                   { "Barcode7", LuxembourgishBarcode7 },
                   { "Barcode8", LuxembourgishBarcode8 },
                   { "Barcode9", LuxembourgishBarcode9 },
                   { "Barcode10", LuxembourgishBarcode10 },
                   { "Barcode11", LuxembourgishBarcode11 },
                   { "Barcode12", LuxembourgishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Norwegian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> NorwegianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", NorwegianBarcode1 },
                   { "Barcode2", NorwegianBarcode2 },
                   { "Barcode3", NorwegianBarcode3 },
                   { "Barcode4", NorwegianBarcode4 },
                   { "Barcode5", NorwegianBarcode5 },
                   { "Barcode6", NorwegianBarcode6 },
                   { "Barcode7", NorwegianBarcode7 },
                   { "Barcode8", NorwegianBarcode8 },
                   { "Barcode9", NorwegianBarcode9 },
                   { "Barcode10", NorwegianBarcode10 },
                   { "Barcode11", NorwegianBarcode11 },
                   { "Barcode12", NorwegianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Maltese 47-Key computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Maltese47KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Maltese47KeyBarcode1 },
                   { "Barcode2", Maltese47KeyBarcode2 },
                   { "Barcode3", Maltese47KeyBarcode3 },
                   { "Barcode4", Maltese47KeyBarcode4 },
                   { "Barcode5", Maltese47KeyBarcode5 },
                   { "Barcode6", Maltese47KeyBarcode6 },
                   { "Barcode7", Maltese47KeyBarcode7 },
                   { "Barcode8", Maltese47KeyBarcode8 },
                   { "Barcode9", Maltese47KeyBarcode9 },
                   { "Barcode10", Maltese47KeyBarcode10 },
                   { "Barcode11", Maltese47KeyBarcode11 },
                   { "Barcode12", Maltese47KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Maltese 48-Key computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Maltese48KeyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Maltese48KeyBarcode1 },
                   { "Barcode2", Maltese48KeyBarcode2 },
                   { "Barcode3", Maltese48KeyBarcode3 },
                   { "Barcode4", Maltese48KeyBarcode4 },
                   { "Barcode5", Maltese48KeyBarcode5 },
                   { "Barcode6", Maltese48KeyBarcode6 },
                   { "Barcode7", Maltese48KeyBarcode7 },
                   { "Barcode8", Maltese48KeyBarcode8 },
                   { "Barcode9", Maltese48KeyBarcode9 },
                   { "Barcode10", Maltese48KeyBarcode10 },
                   { "Barcode11", Maltese48KeyBarcode11 },
                   { "Barcode12", Maltese48KeyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Polish (Programmers) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> PolishProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", PolishProgrammersBarcode1 },
                   { "Barcode2", PolishProgrammersBarcode2 },
                   { "Barcode3", PolishProgrammersBarcode3 },
                   { "Barcode4", PolishProgrammersBarcode4 },
                   { "Barcode5", PolishProgrammersBarcode5 },
                   { "Barcode6", PolishProgrammersBarcode6 },
                   { "Barcode7", PolishProgrammersBarcode7 },
                   { "Barcode8", PolishProgrammersBarcode8 },
                   { "Barcode9", PolishProgrammersBarcode9 },
                   { "Barcode10", PolishProgrammersBarcode10 },
                   { "Barcode11", PolishProgrammersBarcode11 },
                   { "Barcode12", PolishProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Polish (214) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Polish214BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", Polish214Barcode1 },
                   { "Barcode2", Polish214Barcode2 },
                   { "Barcode3", Polish214Barcode3 },
                   { "Barcode4", Polish214Barcode4 },
                   { "Barcode5", Polish214Barcode5 },
                   { "Barcode6", Polish214Barcode6 },
                   { "Barcode7", Polish214Barcode7 },
                   { "Barcode8", Polish214Barcode8 },
                   { "Barcode9", Polish214Barcode9 },
                   { "Barcode10", Polish214Barcode10 },
                   { "Barcode11", Polish214Barcode11 },
                   { "Barcode12", Polish214Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Portuguese computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> PortugueseBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", PortugueseBarcode1 },
                   { "Barcode2", PortugueseBarcode2 },
                   { "Barcode3", PortugueseBarcode3 },
                   { "Barcode4", PortugueseBarcode4 },
                   { "Barcode5", PortugueseBarcode5 },
                   { "Barcode6", PortugueseBarcode6 },
                   { "Barcode7", PortugueseBarcode7 },
                   { "Barcode8", PortugueseBarcode8 },
                   { "Barcode9", PortugueseBarcode9 },
                   { "Barcode10", PortugueseBarcode10 },
                   { "Barcode11", PortugueseBarcode11 },
                   { "Barcode12", PortugueseBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Standard) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", RomanianStandardBarcode1 },
                   { "Barcode2", RomanianStandardBarcode2 },
                   { "Barcode3", RomanianStandardBarcode3 },
                   { "Barcode4", RomanianStandardBarcode4 },
                   { "Barcode5", RomanianStandardBarcode5 },
                   { "Barcode6", RomanianStandardBarcode6 },
                   { "Barcode7", RomanianStandardBarcode7 },
                   { "Barcode8", RomanianStandardBarcode8 },
                   { "Barcode9", RomanianStandardBarcode9 },
                   { "Barcode10", RomanianStandardBarcode10 },
                   { "Barcode11", RomanianStandardBarcode11 },
                   { "Barcode12", RomanianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Legacy) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianLegacyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", RomanianLegacyBarcode1 },
                   { "Barcode2", RomanianLegacyBarcode2 },
                   { "Barcode3", RomanianLegacyBarcode3 },
                   { "Barcode4", RomanianLegacyBarcode4 },
                   { "Barcode5", RomanianLegacyBarcode5 },
                   { "Barcode6", RomanianLegacyBarcode6 },
                   { "Barcode7", RomanianLegacyBarcode7 },
                   { "Barcode8", RomanianLegacyBarcode8 },
                   { "Barcode9", RomanianLegacyBarcode9 },
                   { "Barcode10", RomanianLegacyBarcode10 },
                   { "Barcode11", RomanianLegacyBarcode11 },
                   { "Barcode12", RomanianLegacyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Romanian (Programmers) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> RomanianProgrammersBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", RomanianProgrammersBarcode1 },
                   { "Barcode2", RomanianProgrammersBarcode2 },
                   { "Barcode3", RomanianProgrammersBarcode3 },
                   { "Barcode4", RomanianProgrammersBarcode4 },
                   { "Barcode5", RomanianProgrammersBarcode5 },
                   { "Barcode6", RomanianProgrammersBarcode6 },
                   { "Barcode7", RomanianProgrammersBarcode7 },
                   { "Barcode8", RomanianProgrammersBarcode8 },
                   { "Barcode9", RomanianProgrammersBarcode9 },
                   { "Barcode10", RomanianProgrammersBarcode10 },
                   { "Barcode11", RomanianProgrammersBarcode11 },
                   { "Barcode12", RomanianProgrammersBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Scottish Gaelic computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> ScottishGaelicBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", ScottishGaelicBarcode1 },
                   { "Barcode2", ScottishGaelicBarcode2 },
                   { "Barcode3", ScottishGaelicBarcode3 },
                   { "Barcode4", ScottishGaelicBarcode4 },
                   { "Barcode5", ScottishGaelicBarcode5 },
                   { "Barcode6", ScottishGaelicBarcode6 },
                   { "Barcode7", ScottishGaelicBarcode7 },
                   { "Barcode8", ScottishGaelicBarcode8 },
                   { "Barcode9", ScottishGaelicBarcode9 },
                   { "Barcode10", ScottishGaelicBarcode10 },
                   { "Barcode11", ScottishGaelicBarcode11 },
                   { "Barcode12", ScottishGaelicBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Slovak (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SlovakQwertyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SlovakQwertyBarcode1 },
                   { "Barcode2", SlovakQwertyBarcode2 },
                   { "Barcode3", SlovakQwertyBarcode3 },
                   { "Barcode4", SlovakQwertyBarcode4 },
                   { "Barcode5", SlovakQwertyBarcode5 },
                   { "Barcode6", SlovakQwertyBarcode6 },
                   { "Barcode7", SlovakQwertyBarcode7 },
                   { "Barcode8", SlovakQwertyBarcode8 },
                   { "Barcode9", SlovakQwertyBarcode9 },
                   { "Barcode10", SlovakQwertyBarcode10 },
                   { "Barcode11", SlovakQwertyBarcode11 },
                   { "Barcode12", SlovakQwertyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Slovenian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SlovenianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SlovenianBarcode1 },
                   { "Barcode2", SlovenianBarcode2 },
                   { "Barcode3", SlovenianBarcode3 },
                   { "Barcode4", SlovenianBarcode4 },
                   { "Barcode5", SlovenianBarcode5 },
                   { "Barcode6", SlovenianBarcode6 },
                   { "Barcode7", SlovenianBarcode7 },
                   { "Barcode8", SlovenianBarcode8 },
                   { "Barcode9", SlovenianBarcode9 },
                   { "Barcode10", SlovenianBarcode10 },
                   { "Barcode11", SlovenianBarcode11 },
                   { "Barcode12", SlovenianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Spanish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SpanishBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SpanishBarcode1 },
                   { "Barcode2", SpanishBarcode2 },
                   { "Barcode3", SpanishBarcode3 },
                   { "Barcode4", SpanishBarcode4 },
                   { "Barcode5", SpanishBarcode5 },
                   { "Barcode6", SpanishBarcode6 },
                   { "Barcode7", SpanishBarcode7 },
                   { "Barcode8", SpanishBarcode8 },
                   { "Barcode9", SpanishBarcode9 },
                   { "Barcode10", SpanishBarcode10 },
                   { "Barcode11", SpanishBarcode11 },
                   { "Barcode12", SpanishBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SorbianBarcode1 },
                   { "Barcode2", SorbianBarcode2 },
                   { "Barcode3", SorbianBarcode3 },
                   { "Barcode4", SorbianBarcode4 },
                   { "Barcode5", SorbianBarcode5 },
                   { "Barcode6", SorbianBarcode6 },
                   { "Barcode7", SorbianBarcode7 },
                   { "Barcode8", SorbianBarcode8 },
                   { "Barcode9", SorbianBarcode9 },
                   { "Barcode10", SorbianBarcode10 },
                   { "Barcode11", SorbianBarcode11 },
                   { "Barcode12", SorbianBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Standard computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianStandardBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SorbianStandardBarcode1 },
                   { "Barcode2", SorbianStandardBarcode2 },
                   { "Barcode3", SorbianStandardBarcode3 },
                   { "Barcode4", SorbianStandardBarcode4 },
                   { "Barcode5", SorbianStandardBarcode5 },
                   { "Barcode6", SorbianStandardBarcode6 },
                   { "Barcode7", SorbianStandardBarcode7 },
                   { "Barcode8", SorbianStandardBarcode8 },
                   { "Barcode9", SorbianStandardBarcode9 },
                   { "Barcode10", SorbianStandardBarcode10 },
                   { "Barcode11", SorbianStandardBarcode11 },
                   { "Barcode12", SorbianStandardBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Extended computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianExtendedBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SorbianExtendedBarcode1 },
                   { "Barcode2", SorbianExtendedBarcode2 },
                   { "Barcode3", SorbianExtendedBarcode3 },
                   { "Barcode4", SorbianExtendedBarcode4 },
                   { "Barcode5", SorbianExtendedBarcode5 },
                   { "Barcode6", SorbianExtendedBarcode6 },
                   { "Barcode7", SorbianExtendedBarcode7 },
                   { "Barcode8", SorbianExtendedBarcode8 },
                   { "Barcode9", SorbianExtendedBarcode9 },
                   { "Barcode10", SorbianExtendedBarcode10 },
                   { "Barcode11", SorbianExtendedBarcode11 },
                   { "Barcode12", SorbianExtendedBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Sorbian Standard (Legacy) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> SorbianStandardLegacyBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", SorbianStandardLegacyBarcode1 },
                   { "Barcode2", SorbianStandardLegacyBarcode2 },
                   { "Barcode3", SorbianStandardLegacyBarcode3 },
                   { "Barcode4", SorbianStandardLegacyBarcode4 },
                   { "Barcode5", SorbianStandardLegacyBarcode5 },
                   { "Barcode6", SorbianStandardLegacyBarcode6 },
                   { "Barcode7", SorbianStandardLegacyBarcode7 },
                   { "Barcode8", SorbianStandardLegacyBarcode8 },
                   { "Barcode9", SorbianStandardLegacyBarcode9 },
                   { "Barcode10", SorbianStandardLegacyBarcode10 },
                   { "Barcode11", SorbianStandardLegacyBarcode11 },
                   { "Barcode12", SorbianStandardLegacyBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedKingdomBarcode1 },
                   { "Barcode2", UnitedKingdomBarcode2 },
                   { "Barcode3", UnitedKingdomBarcode3 },
                   { "Barcode4", UnitedKingdomBarcode4 },
                   { "Barcode5", UnitedKingdomBarcode5 },
                   { "Barcode6", UnitedKingdomBarcode6 },
                   { "Barcode7", UnitedKingdomBarcode7 },
                   { "Barcode8", UnitedKingdomBarcode8 },
                   { "Barcode9", UnitedKingdomBarcode9 },
                   { "Barcode10", UnitedKingdomBarcode10 },
                   { "Barcode11", UnitedKingdomBarcode11 },
                   { "Barcode12", UnitedKingdomBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom Extended computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomExtendedBarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedKingdomExtendedBarcode1 },
                   { "Barcode2", UnitedKingdomExtendedBarcode2 },
                   { "Barcode3", UnitedKingdomExtendedBarcode3 },
                   { "Barcode4", UnitedKingdomExtendedBarcode4 },
                   { "Barcode5", UnitedKingdomExtendedBarcode5 },
                   { "Barcode6", UnitedKingdomExtendedBarcode6 },
                   { "Barcode7", UnitedKingdomExtendedBarcode7 },
                   { "Barcode8", UnitedKingdomExtendedBarcode8 },
                   { "Barcode9", UnitedKingdomExtendedBarcode9 },
                   { "Barcode10", UnitedKingdomExtendedBarcode10 },
                   { "Barcode11", UnitedKingdomExtendedBarcode11 },
                   { "Barcode12", UnitedKingdomExtendedBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout in the
    /// case that the ASCII character is reported correctly as an ASCII 30 character.
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomVariant1BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedKingdomVariant1Barcode1 },
                   { "Barcode2", UnitedKingdomVariant1Barcode2 },
                   { "Barcode3", UnitedKingdomVariant1Barcode3 },
                   { "Barcode4", UnitedKingdomVariant1Barcode4 },
                   { "Barcode5", UnitedKingdomVariant1Barcode5 },
                   { "Barcode6", UnitedKingdomVariant1Barcode6 },
                   { "Barcode7", UnitedKingdomVariant1Barcode7 },
                   { "Barcode8", UnitedKingdomVariant1Barcode8 },
                   { "Barcode9", UnitedKingdomVariant1Barcode9 },
                   { "Barcode10", UnitedKingdomVariant1Barcode10 },
                   { "Barcode11", UnitedKingdomVariant1Barcode11 },
                   { "Barcode12", UnitedKingdomVariant1Barcode12 }
               };
    }


    /// <summary>
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout in the
    /// case that the ASCII character is reported ambiguously as an invariant ASCII character.
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomVariant2BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedKingdomVariant2Barcode1 },
                   { "Barcode2", UnitedKingdomVariant2Barcode2 },
                   { "Barcode3", UnitedKingdomVariant2Barcode3 },
                   { "Barcode4", UnitedKingdomVariant2Barcode4 },
                   { "Barcode5", UnitedKingdomVariant2Barcode5 },
                   { "Barcode6", UnitedKingdomVariant2Barcode6 },
                   { "Barcode7", UnitedKingdomVariant2Barcode7 },
                   { "Barcode8", UnitedKingdomVariant2Barcode8 },
                   { "Barcode9", UnitedKingdomVariant2Barcode9 },
                   { "Barcode10", UnitedKingdomVariant2Barcode10 },
                   { "Barcode11", UnitedKingdomVariant2Barcode11 },
                   { "Barcode12", UnitedKingdomVariant2Barcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout in the
    /// case that the ASCII character is reported ambiguously as a variant ASCII character.
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomVariant3BarcodeData()
    {
        return new Dictionary<string, string>
               {
                   { "Barcode1", UnitedKingdomVariant3Barcode1 },
                   { "Barcode2", UnitedKingdomVariant3Barcode2 },
                   { "Barcode3", UnitedKingdomVariant3Barcode3 },
                   { "Barcode4", UnitedKingdomVariant3Barcode4 },
                   { "Barcode5", UnitedKingdomVariant3Barcode5 },
                   { "Barcode6", UnitedKingdomVariant3Barcode6 },
                   { "Barcode7", UnitedKingdomVariant3Barcode7 },
                   { "Barcode8", UnitedKingdomVariant3Barcode8 },
                   { "Barcode9", UnitedKingdomVariant3Barcode9 },
                   { "Barcode10", UnitedKingdomVariant3Barcode10 },
                   { "Barcode11", UnitedKingdomVariant3Barcode11 },
                   { "Barcode12", UnitedKingdomVariant3Barcode12 }
               };
    }

    /// <summary>
    /// Return the character input for the baseline calibration barcode using a scanner
    /// configured for a US keyboard and computer with US keyboard layout. 
    /// </summary>
    /// <returns>
    /// The <see cref="string"/> for scanning the baseline calibration barcode.
    /// The scanner is configured for a US keyboard.
    /// The computer is configured for a US keyboard.
    /// </returns>
    private static string BaselineCalibrationUsUs()
    {
        var testString =
            "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    "
          + (char)29;
        return testString + "    \x001C    \x001E    \x001F    ";
    }

    /// <summary>
    /// Convert an input string to a list of character values.
    /// </summary>
    /// <param name="input">The string of characters to be converted.</param>
    /// <returns>A comma-separated value list of character values.</returns>
    private static int[] ConvertToCharacterValues(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Array.Empty<int>();
        }

        var outputBuilder = new int[input.Length];

        for (var idx = 0; idx < input.Length; idx++)
        {
            outputBuilder[idx] = input[idx];
        }

        return outputBuilder;
    }
}