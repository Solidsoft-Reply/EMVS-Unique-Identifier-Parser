// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsToEuropeanGs1BarcodesWithAim.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <summary>
// Data for GS1 Parser US to European GS1 barcodes with AIM identifiers calibration tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests.Data;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Data for GS1 Parser US to European GS1 barcodes with AIM identifiers calibration tests.
/// </summary>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
public static class UsToEuropeanGs1BarcodesWithAim {
    public const string UsBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \u001d    \u001c    \u001e        \r";

    public const string UsBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string UsBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string UsBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string UsBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P\"\u001d17261002";
    public const string UsBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string UsBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j\"%e+P\u001d17251031";
    public const string UsBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string UsBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string UsBarcode9 = "]d2010251991944283521(\"P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string UsBarcode10 = "]d2010251991944283521)Wuk3P+e%\"\u001d10Qo0>7rU\u001d17260703";
    public const string UsBarcode11 = "]d2010251991944283521\"j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string UsBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string UnitedKingdomBaseline =
        "]d1  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \u001d    \u001c    \u001e        \r";

    public const string UnitedKingdomBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string UnitedKingdomBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string UnitedKingdomBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string UnitedKingdomBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P@\u001d17261002";
    public const string UnitedKingdomBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string UnitedKingdomBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j@%e+P\u001d17251031";
    public const string UnitedKingdomBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string UnitedKingdomBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string UnitedKingdomBarcode9 = "]d2010251991944283521(@P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string UnitedKingdomBarcode10 = "]d2010251991944283521)Wuk3P+e%@\u001d10Qo0>7rU\u001d17260703";
    public const string UnitedKingdomBarcode11 = "]d2010251991944283521@j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string UnitedKingdomBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string UnitedKingdomExtendedBaseline =
        "]d1  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \0    \0    \0        \r";

    public const string UnitedKingdomExtendedDeadkeyBarcode1 =
        "]d1\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`{\0`~\0`}\0`¬\r";

    public const string UnitedKingdomExtendedBarcode1 = "]d2010251991944283521yCH*4'h1Ab\010DdVcX<t\017250209";
    public const string UnitedKingdomExtendedBarcode2 = "]d2010251991944283521TgIv?,6BmK\010.GRs!qO\017250729";
    public const string UnitedKingdomExtendedBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\010(fpNxYi\017250405";
    public const string UnitedKingdomExtendedBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\010=8Fn_P@\017261002";
    public const string UnitedKingdomExtendedBarcode5 = "]d20102519919442835215j4CltEc-Y\0103kuW)L9\017260304";
    public const string UnitedKingdomExtendedBarcode6 = "]d201025199194428352151itJzCguA\010:j@%e+P\017251031";
    public const string UnitedKingdomExtendedBarcode7 = "]d2010251991944283521t<XcVdD0q!\010bA1h'4*\017251214";
    public const string UnitedKingdomExtendedBarcode8 = "]d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string UnitedKingdomExtendedBarcode9 = "]d2010251991944283521(@P_nF8=9L\010,?vIgTS\017260218";
    public const string UnitedKingdomExtendedBarcode10 = "]d2010251991944283521)Wuk3P+e%@\010Qo0>7rU\017260703";
    public const string UnitedKingdomExtendedBarcode11 = "]d2010251991944283521@j:AugCzJt\010&ZJMaz/\017251126";
    public const string UnitedKingdomExtendedBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\0102;5wEY-\017250404";

    public const string IrishBaseline =
        "]d1  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \u001d    \u001c    \u001e        \r";

    public const string IrishDeadkeyBarcode1 =
        "]d1\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0`¬\r";

    public const string IrishBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string IrishBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string IrishBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string IrishBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P@\u001d17261002";
    public const string IrishBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string IrishBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j@%e+P\u001d17251031";
    public const string IrishBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string IrishBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string IrishBarcode9 = "]d2010251991944283521(@P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string IrishBarcode10 = "]d2010251991944283521)Wuk3P+e%@\u001d10Qo0>7rU\u001d17260703";
    public const string IrishBarcode11 = "]d2010251991944283521@j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string IrishBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string GaelicBaseline =
        "]d1  ! @ % & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } `    \u001d    \u001c    \u001e        \r";

    public const string GaelicDeadkeyBarcode1 =
        "]d1\0'!\0'@\0'£\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0'.\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0':\0';\0'<\0'=\0'>\0'?\0'\"\0Á\0'B\0'C\0'D\0É\0'F\0'G\0'H\0Í\0'J\0'K\0'L\0'M\0'N\0Ó\0'P\0'Q\0'R\0'S\0'T\0Ú\0'V\0'W\0'X\0Ý\0'Z\0'[\0'#\0']\0'^\0'_\0'`\0á\0'b\0'c\0'd\0é\0'f\0'g\0'h\0í\0'j\0'k\0'l\0'm\0'n\0ó\0'p\0'q\0'r\0's\0't\0ú\0'v\0'w\0'x\0ý\0'z\0'{\0'~\0'}\0'`\r";

    public const string GaelicDeadkeyBarcode2 =
        "]d1\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0``\r";

    public const string GaelicBarcode1 = "]d2010251991944283521yCH*4\0'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string GaelicBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string GaelicBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string GaelicBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P@\u001d17261002";
    public const string GaelicBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string GaelicBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j@%e+P\u001d17251031";
    public const string GaelicBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h\0'4*\u001d17251214";
    public const string GaelicBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string GaelicBarcode9 = "]d2010251991944283521(@P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string GaelicBarcode10 = "]d2010251991944283521)Wuk3P+e%@\u001d10Qo0>7rU\u001d17260703";
    public const string GaelicBarcode11 = "]d2010251991944283521@j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string GaelicBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string BelgianCommaBaseline =
        "$d&  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \u001d    \u001c    \u001e    \0    \r";

    public const string BelgianCommaDeadkeyBarcode1 =
        "$d&\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\r";

    public const string BelgianCommaDeadkeyBarcode2 =
        "$d&\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\r";

    public const string BelgianCommaBarcode1 = "$déà&àé(&çç&ç''é!\"(é&yCH8'ùh&Qb\u001d&àDdVcX.t\u001d&èé(àéàç";
    public const string BelgianCommaBarcode2 = "$déà&àé(&çç&ç''é!\"(é&TgIv+;§B,K\u001d&à:GRs1aO\u001d&èé(àèéç";
    public const string BelgianCommaBarcode3 = "$déà&àé(&çç&ç''é!\"(é&W7Urè/àoAS\u001d&à9fpNxYi\u001d&èé(à'à(";
    public const string BelgianCommaBarcode4 = "$déà&àé(&çç&ç''é!\"(é&Ez(mé=wq?J\u001d&à-!Fn°P%\u001d&èé§&ààé";
    public const string BelgianCommaBarcode5 = "$déà&àé(&çç&ç''é!\"(é&(j'CltEc)Y\u001d&à\"kuZ0Lç\u001d&èé§à\"à'";
    public const string BelgianCommaBarcode6 = "$déà&àé(&çç&ç''é!\"(é&(&itJwCguQ\u001d&àMj%5e_P\u001d&èé(&à\"&";
    public const string BelgianCommaBarcode7 = "$déà&àé(&çç&ç''é!\"(é&t.XcVdDàa1\u001d&àbQ&hù'8\u001d&èé(&é&'";
    public const string BelgianCommaBarcode8 = "$déà&àé(&çç&ç''é!\"(é&sRG:iYxNpf\u001d&àHCyK,B§\u001d&èé§à§éà";
    public const string BelgianCommaBarcode9 = "$déà&àé(&çç&ç''é!\"(é&9%P°nF!-çL\u001d&à;+vIgTS\u001d&èé§àé&!";
    public const string BelgianCommaBarcode10 = "$déà&àé(&çç&ç''é!\"(é&0Zuk\"P_e5%\u001d&àAoà/èrU\u001d&èé§àèà\"";
    public const string BelgianCommaBarcode11 = "$déà&àé(&çç&ç''é!\"(é&%jMQugCwJt\u001d&à7WJ?qw=\u001d&èé(&&é§";
    public const string BelgianCommaBarcode12 = "$déà&àé(&çç&ç''é!\"(é&HIrAçAeTyA\u001d&àém(zEY)\u001d&èé(à'à'";

    public const string BelgianPeriodBaseline =
        "$d&  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \u001d    \u001c    \u001e    \0    \r";

    public const string BelgianPeriodDeadkeyBarcode1 =
        "$d&\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\r";

    public const string BelgianPeriodDeadkeyBarcode2 =
        "$d&\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\r";

    public const string BelgianPeriodBarcode1 = "$déà&àé(&çç&ç''é!\"(é&yCH8'ùh&Qb\u001d&àDdVcX.t\u001d&èé(àéàç";
    public const string BelgianPeriodBarcode2 = "$déà&àé(&çç&ç''é!\"(é&TgIv+;§B,K\u001d&à:GRs1aO\u001d&èé(àèéç";
    public const string BelgianPeriodBarcode3 = "$déà&àé(&çç&ç''é!\"(é&W7Urè/àoAS\u001d&à9fpNxYi\u001d&èé(à'à(";
    public const string BelgianPeriodBarcode4 = "$déà&àé(&çç&ç''é!\"(é&Ez(mé=wq?J\u001d&à-!Fn°P%\u001d&èé§&ààé";
    public const string BelgianPeriodBarcode5 = "$déà&àé(&çç&ç''é!\"(é&(j'CltEc)Y\u001d&à\"kuZ0Lç\u001d&èé§à\"à'";
    public const string BelgianPeriodBarcode6 = "$déà&àé(&çç&ç''é!\"(é&(&itJwCguQ\u001d&àMj%5e_P\u001d&èé(&à\"&";
    public const string BelgianPeriodBarcode7 = "$déà&àé(&çç&ç''é!\"(é&t.XcVdDàa1\u001d&àbQ&hù'8\u001d&èé(&é&'";
    public const string BelgianPeriodBarcode8 = "$déà&àé(&çç&ç''é!\"(é&sRG:iYxNpf\u001d&àHCyK,B§\u001d&èé§à§éà";
    public const string BelgianPeriodBarcode9 = "$déà&àé(&çç&ç''é!\"(é&9%P°nF!-çL\u001d&à;+vIgTS\u001d&èé§àé&!";
    public const string BelgianPeriodBarcode10 = "$déà&àé(&çç&ç''é!\"(é&0Zuk\"P_e5%\u001d&àAoà/èrU\u001d&èé§àèà\"";
    public const string BelgianPeriodBarcode11 = "$déà&àé(&çç&ç''é!\"(é&%jMQugCwJt\u001d&à7WJ?qw=\u001d&èé(&&é§";
    public const string BelgianPeriodBarcode12 = "$déà&àé(&çç&ç''é!\"(é&HIrAçAeTyA\u001d&àém(zEY)\u001d&èé(à'à'";

    public const string BelgianFrenchBaseline =
        "$d&  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \u001d    \u001c    \u001e    \0    \r";

    public const string BelgianFrenchDeadkeyBarcode1 =
        "$d&\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\r";

    public const string BelgianFrenchDeadkeyBarcode2 =
        "$d&\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\r";

    public const string BelgianFrenchBarcode1 = "$déà&àé(&çç&ç''é!\"(é&yCH8'ùh&Qb\u001d&àDdVcX.t\u001d&èé(àéàç";
    public const string BelgianFrenchBarcode2 = "$déà&àé(&çç&ç''é!\"(é&TgIv+;§B,K\u001d&à:GRs1aO\u001d&èé(àèéç";
    public const string BelgianFrenchBarcode3 = "$déà&àé(&çç&ç''é!\"(é&W7Urè/àoAS\u001d&à9fpNxYi\u001d&èé(à'à(";
    public const string BelgianFrenchBarcode4 = "$déà&àé(&çç&ç''é!\"(é&Ez(mé=wq?J\u001d&à-!Fn°P%\u001d&èé§&ààé";
    public const string BelgianFrenchBarcode5 = "$déà&àé(&çç&ç''é!\"(é&(j'CltEc)Y\u001d&à\"kuZ0Lç\u001d&èé§à\"à'";
    public const string BelgianFrenchBarcode6 = "$déà&àé(&çç&ç''é!\"(é&(&itJwCguQ\u001d&àMj%5e_P\u001d&èé(&à\"&";
    public const string BelgianFrenchBarcode7 = "$déà&àé(&çç&ç''é!\"(é&t.XcVdDàa1\u001d&àbQ&hù'8\u001d&èé(&é&'";
    public const string BelgianFrenchBarcode8 = "$déà&àé(&çç&ç''é!\"(é&sRG:iYxNpf\u001d&àHCyK,B§\u001d&èé§à§éà";
    public const string BelgianFrenchBarcode9 = "$déà&àé(&çç&ç''é!\"(é&9%P°nF!-çL\u001d&à;+vIgTS\u001d&èé§àé&!";
    public const string BelgianFrenchBarcode10 = "$déà&àé(&çç&ç''é!\"(é&0Zuk\"P_e5%\u001d&àAoà/èrU\u001d&èé§àèà\"";
    public const string BelgianFrenchBarcode11 = "$déà&àé(&çç&ç''é!\"(é&%jMQugCwJt\u001d&à7WJ?qw=\u001d&èé(&&é§";
    public const string BelgianFrenchBarcode12 = "$déà&àé(&çç&ç''é!\"(é&HIrAçAeTyA\u001d&àém(zEY)\u001d&èé(à'à'";

    public const string CzechBaseline =
        ")d+  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú \0¨) 6 ; / ' ( \0°   \u001d    \u001c    \u001e        \r";

    public const string CzechDeadkeyBarcode1 =
        ")d+\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ'\0ˇ(\0ˇ°\r";

    public const string CzechDeadkeyBarcode2 =
        ")d+\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´/\0´'\0´(\0´°\r";

    public const string CzechDeadkeyBarcode3 =
        ")d+\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0Ÿ\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨/\0¨'\0¨(\0¨°\r";

    public const string CzechDeadkeyBarcode4 =
        ")d+\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Z\0°Y\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°z\0°y\0°/\0°'\0°(\0°°\r";

    public const string CzechBarcode1 = ")děé+éěř+íí+íččěášřě+zCH8č§h+Ab\u001d+éDdVcX?t\u001d+ýěřéěéí";
    public const string CzechBarcode2 = ")děé+éěř+íí+íččěášřě+TgIv_,žBmK\u001d+é.GRs1qO\u001d+ýěřéýěí";
    public const string CzechBarcode3 = ")děé+éěř+íí+íččěášřě+Y7Urý:éoQS\u001d+é9fpNxZi\u001d+ýěřéčéř";
    public const string CzechBarcode4 = ")děé+éěř+íí+íččěášřě+Ewřůě-yaMJ\u001d+é\0´áFn%P!\u001d+ýěž+ééě";
    public const string CzechBarcode5 = ")děé+éěř+íí+íččěášřě+řjčCltEc=Z\u001d+éškuW0Lí\u001d+ýěžéšéč";
    public const string CzechBarcode6 = ")děé+éěř+íí+íččěášřě+ř+itJyCguA\u001d+é\"j!5e\0ˇP\u001d+ýěř+éš+";
    public const string CzechBarcode7 = ")děé+éěř+íí+íččěášřě+t?XcVdDéq1\u001d+ébA+h§č8\u001d+ýěř+ě+č";
    public const string CzechBarcode8 = ")děé+éěř+íí+íččěášřě+sRG.iZxNpf\u001d+éHCzKmBž\u001d+ýěžéžěé";
    public const string CzechBarcode9 = ")děé+éěř+íí+íččěášřě+9!P%nFá\0´íL\u001d+é,_vIgTS\u001d+ýěžéě+á";
    public const string CzechBarcode10 = ")děé+éěř+íí+íččěášřě+0WukšP\0ě5!\u001d+éQoé:ýrU\u001d+ýěžéýéš";
    public const string CzechBarcode11 = ")děé+éěř+íí+íččěášřě+!j\"AugCyJt\u001d+é7YJMay-\u001d+ýěř++ěž";
    public const string CzechBarcode12 = ")děé+éěř+íí+íččěášřě+HIrQíQeTzQ\u001d+éěůřwEZ=\u001d+ýěřéčéč";

    public const string CzechQwertyBaseline =
        ")d+  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú \0¨) 6 ; / ' ( \0°   \u001b    \u001c    \0        \r";

    public const string CzechQwertyDeadkeyBarcode1 =
        ")d+\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ'\0ˇ(\0ˇ°\r";

    public const string CzechQwertyDeadkeyBarcode2 =
        ")d+\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´'\0´(\0´°\r";

    public const string CzechQwertyDeadkeyBarcode3 =
        ")d+\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0Ÿ\0¨Z\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨/\0¨'\0¨(\0¨°\r";

    public const string CzechQwertyDeadkeyBarcode4 =
        ")d+\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°'\0°(\0°°\r";

    public const string CzechQwertyBarcode1 = ")děé+éěř+íí+íččěášřě+yCH8č§h+Ab\u001b+éDdVcX?t\u001b+ýěřéěéí";
    public const string CzechQwertyBarcode2 = ")děé+éěř+íí+íččěášřě+TgIv_,žBmK\u001b+é.GRs1qO\u001b+ýěřéýěí";
    public const string CzechQwertyBarcode3 = ")děé+éěř+íí+íččěášřě+Z7Urý:éoQS\u001b+é9fpNxYi\u001b+ýěřéčéř";
    public const string CzechQwertyBarcode4 = ")děé+éěř+íí+íččěášřě+Ewřůě-zaMJ\u001b+é\0´áFn%P!\u001b+ýěž+ééě";
    public const string CzechQwertyBarcode5 = ")děé+éěř+íí+íččěášřě+řjčCltEc=Y\u001b+éškuW0Lí\u001b+ýěžéšéč";
    public const string CzechQwertyBarcode6 = ")děé+éěř+íí+íččěášřě+ř+itJzCguA\u001b+é\"j!5e\0ˇP\u001b+ýěř+éš+";
    public const string CzechQwertyBarcode7 = ")děé+éěř+íí+íččěášřě+t?XcVdDéq1\u001b+ébA+h§č8\u001b+ýěř+ě+č";
    public const string CzechQwertyBarcode8 = ")děé+éěř+íí+íččěášřě+sRG.iYxNpf\u001b+éHCyKmBž\u001b+ýěžéžěé";
    public const string CzechQwertyBarcode9 = ")děé+éěř+íí+íččěášřě+9!P%nFá\0´íL\u001b+é,_vIgTS\u001b+ýěžéě+á";
    public const string CzechQwertyBarcode10 = ")děé+éěř+íí+íččěášřě+0WukšP\0ě5!\u001b+éQoé:ýrU\u001b+ýěžéýéš";
    public const string CzechQwertyBarcode11 = ")děé+éěř+íí+íččěášřě+!j\"AugCzJt\u001b+é7ZJMaz-\u001b+ýěř++ěž";
    public const string CzechQwertyBarcode12 = ")děé+éěř+íí+íččěášřě+HIrQíQeTyQ\u001b+éěůřwEY=\u001b+ýěřéčéč";

    public const string CzechProgrammersBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \u001b    \u001c    \0        \r";

    public const string CzechProgrammersBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001b10DdVcX<t\u001b17250209";
    public const string CzechProgrammersBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001b10.GRs!qO\u001b17250729";
    public const string CzechProgrammersBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001b10(fpNxYi\u001b17250405";
    public const string CzechProgrammersBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001b10=8Fn_P\"\u001b17261002";
    public const string CzechProgrammersBarcode5 = "]d20102519919442835215j4CltEc-Y\u001b103kuW)L9\u001b17260304";
    public const string CzechProgrammersBarcode6 = "]d201025199194428352151itJzCguA\u001b10:j\"%e+P\u001b17251031";
    public const string CzechProgrammersBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001b10bA1h'4*\u001b17251214";
    public const string CzechProgrammersBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001b10HCyKmB6\u001b17260620";
    public const string CzechProgrammersBarcode9 = "]d2010251991944283521(\"P_nF8=9L\u001b10,?vIgTS\u001b17260218";

    public const string CzechProgrammersBarcode10 =
        "]d2010251991944283521)Wuk3P+e%\"\u001b10Qo0>7rU\u001b17260703";

    public const string CzechProgrammersBarcode11 =
        "]d2010251991944283521\"j:AugCzJt\u001b10&ZJMaz/\u001b17251126";

    public const string CzechProgrammersBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001b102;5wEY-\u001b17250404";

    public const string DanishBaseline =
        "\0¨d1  ! Ø % / ø ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Æ æ ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& ½ Å * \0^§    \u001d    \0    \u001e        \r";

    public const string DanishDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Ø\0`#\0`¤\0`%\0`/\0`ø\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Æ\0`æ\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`½\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\r";

    public const string DanishDeadkeyBarcode2 =
        "\0¨d1\0´!\0´Ø\0´#\0´¤\0´%\0´/\0´ø\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´½\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´§\r";

    public const string DanishDeadkeyBarcode3 =
        "\0¨d1\0¨!\0¨Ø\0¨#\0¨¤\0¨%\0¨/\0¨ø\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨½\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\r";

    public const string DanishDeadkeyBarcode4 =
        "\0¨d1\0^!\0^Ø\0^#\0^¤\0^%\0^/\0^ø\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Æ\0^æ\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^½\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\r";

    public const string DanishBarcode1 = "\0¨d2010251991944283521yCH(4øh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string DanishBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string DanishBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string DanishBarcode4 = "\0¨d2010251991944283521Ew5æ2-zaMJ\u001d10\0´8Fn?PØ\u001d17261002";
    public const string DanishBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\u001d103kuW=L9\u001d17260304";
    public const string DanishBarcode6 = "\0¨d201025199194428352151itJzCguA\u001d10ÆjØ%e\0`P\u001d17251031";
    public const string DanishBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\u001d10bA1hø4(\u001d17251214";
    public const string DanishBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string DanishBarcode9 = "\0¨d2010251991944283521)ØP?nF8\0´9L\u001d10,_vIgTS\u001d17260218";
    public const string DanishBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Ø\u001d10Qo0:7rU\u001d17260703";
    public const string DanishBarcode11 = "\0¨d2010251991944283521ØjÆAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string DanishBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\u001d102æ5wEY+\u001d17250404";

    public const string DutchBaseline =
        "*d1  ! \0`% _ \0´) ' ( \0~, / . - 0 1 2 3 4 5 6 7 8 9 ± + ; ° : = A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" \0¨< * & @ \0^> | §    \0    \0    \u001e    \0    \r";

    public const string DutchDeadkeyBarcode1 =
        "*d1\0`!\0``\0`#\0`$\0`%\0`_\0`´\0`)\0`'\0`(\0`~\0`,\0`/\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`±\0`+\0`;\0`°\0`:\0`=\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`¨\0`<\0`*\0`&\0`?\0`@\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`>\0`|\0`§\r";

    public const string DutchDeadkeyBarcode2 =
        "*d1\0´!\0´`\0´#\0´$\0´%\0´_\0´´\0´)\0´'\0´(\0´~\0´,\0´/\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´±\0´+\0´;\0´°\0´:\0´=\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´¨\0´<\0´*\0´&\0´?\0´@\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´>\0´|\0´§\r";

    public const string DutchDeadkeyBarcode3 =
        "*d1\0~!\0~`\0~#\0~$\0~%\0~_\0~´\0~)\0~'\0~(\0~~\0~,\0~/\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~±\0~+\0~;\0~°\0~:\0~=\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~¨\0~<\0~*\0~&\0~?\0~@\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~^\0~>\0~|\0~§\r";

    public const string DutchDeadkeyBarcode4 =
        "*d1\0¨!\0¨`\0¨#\0¨$\0¨%\0¨_\0¨´\0¨)\0¨'\0¨(\0¨~\0¨,\0¨/\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨±\0¨+\0¨;\0¨°\0¨:\0¨=\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨¨\0¨<\0¨*\0¨&\0¨?\0¨@\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨>\0¨|\0¨§\r";

    public const string DutchDeadkeyBarcode5 =
        "*d1\0^!\0^`\0^#\0^$\0^%\0^_\0^´\0^)\0^'\0^(\0^~\0^,\0^/\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^±\0^+\0^;\0^°\0^:\0^=\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^¨\0^<\0^*\0^&\0^?\0^@\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^>\0^|\0^§\r";

    public const string DutchBarcode1 = "*d2010251991944283521yCH(4\0´h1Ab\010DdVcX;t\017250209";
    public const string DutchBarcode2 = "*d2010251991944283521TgIv=,6BmK\010.GRs!qO\017250729";
    public const string DutchBarcode3 = "*d2010251991944283521Z_Ur7:0oQS\010)fpNxYi\017250405";
    public const string DutchBarcode4 = "*d2010251991944283521Ew5+2-zaMJ\010°8Fn?P\0\0`17261002";
    public const string DutchBarcode5 = "*d20102519919442835215j4CltEc/Y\0103kuW'L9\017260304";
    public const string DutchBarcode6 = "*d201025199194428352151itJzCguA\010±j\0`%e\0~P\017251031";
    public const string DutchBarcode7 = "*d2010251991944283521t;XcVdD0q!\010bA1h\0´4(\017251214";
    public const string DutchBarcode8 = "*d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string DutchBarcode9 = "*d2010251991944283521)\0`P?nF8°9L\010,=vIgTS\017260218";
    public const string DutchBarcode10 = "*d2010251991944283521'Wuk3P\0~e%\0\0`10Qo0:7rU\017260703";
    public const string DutchBarcode11 = "*d2010251991944283521\0`j±AugCzJt\010_ZJMaz-\017251126";
    public const string DutchBarcode12 = "*d2010251991944283521HIrQ9QeTyQ\0102+5wEY/\017250404";

    public const string EstonianBaseline =
        "õd1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" ü ' õ & \0ˇÜ * Õ \0~   \0    \0    \u001e        \r";

    public const string EstonianDeadkeyBarcode1 =
        "õd1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`ü\0`'\0`õ\0`&\0`?\0`ˇ\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Ü\0`*\0`Õ\0`~\r";

    public const string EstonianDeadkeyBarcode2 =
        "õd1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0´I\0´J\0´K\0´L\0´M\0Ń\0Ó\0´P\0´Q\0´R\0Ś\0´T\0´U\0´V\0´W\0´X\0´Y\0Ź\0´ü\0´'\0´õ\0´&\0´?\0´ˇ\0´a\0´b\0ć\0´d\0é\0´f\0´g\0´h\0´i\0´j\0´k\0´l\0´m\0ń\0ó\0´p\0´q\0´r\0ś\0´t\0´u\0´v\0´w\0´x\0´y\0ź\0´Ü\0´*\0´Õ\0´~\r";

    public const string EstonianDeadkeyBarcode3 =
        "õd1\0ˇ!\0ˇÄ\0ˇ#\0ˇ¤\0ˇ%\0ˇ/\0ˇä\0ˇ)\0ˇ=\0ˇ(\0ˇ`\0ˇ,\0ˇ+\0ˇ.\0ˇ-\0ˇ0\0ˇ1\0ˇ2\0ˇ3\0ˇ4\0ˇ5\0ˇ6\0ˇ7\0ˇ8\0ˇ9\0ˇÖ\0ˇö\0ˇ;\0ˇ´\0ˇ:\0ˇ_\0ˇ\"\0ˇA\0ˇB\0Č\0ˇD\0ˇE\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0ˇL\0ˇM\0ˇN\0ˇO\0ˇP\0ˇQ\0ˇR\0Š\0ˇT\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇü\0ˇ'\0ˇõ\0ˇ&\0ˇ?\0ˇˇ\0ˇa\0ˇb\0č\0ˇd\0ˇe\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ˇl\0ˇm\0ˇn\0ˇo\0ˇp\0ˇq\0ˇr\0š\0ˇt\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇÜ\0ˇ*\0ˇÕ\0ˇ~\r";

    public const string EstonianDeadkeyBarcode4 =
        "õd1\0~!\0~Ä\0~#\0~¤\0~%\0~/\0~ä\0~)\0~=\0~(\0~`\0~,\0~+\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ö\0~ö\0~;\0~´\0~:\0~_\0~\"\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~ü\0~'\0~õ\0~&\0~?\0~ˇ\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~Ü\0~*\0~Õ\0~~\r";

    public const string EstonianBarcode1 = "õd2010251991944283521yCH(4äh1Ab\010DdVcX;t\017250209";
    public const string EstonianBarcode2 = "õd2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string EstonianBarcode3 = "õd2010251991944283521Z/Ur7:0oQS\010)fpNxYi\017250405";
    public const string EstonianBarcode4 = "õd2010251991944283521Ew5ö2-zaMJ\010\0´8Fn?PÄ\017261002";
    public const string EstonianBarcode5 = "õd20102519919442835215j4CltEc+Y\0103kuW=L9\017260304";
    public const string EstonianBarcode6 = "õd201025199194428352151itJzCguA\010ÖjÄ%e\0`P\017251031";
    public const string EstonianBarcode7 = "õd2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string EstonianBarcode8 = "õd2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string EstonianBarcode9 = "õd2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string EstonianBarcode10 = "õd2010251991944283521=Wuk3P\0è%Ä\010Qo0:7rU\017260703";
    public const string EstonianBarcode11 = "õd2010251991944283521ÄjÖAugCzJt\010/ZJMaz-\017251126";
    public const string EstonianBarcode12 = "õd2010251991944283521HIrQ9QeTyQ\0102ö5wEY+\017250404";

    public const string FinnishBaseline =
        "\0¨d1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \u001d    \0    \u001e        \r";

    public const string FinnishDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½";

    public const string FinnishDeadkeyBarcode2 =
        "\0¨d1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\r";

    public const string FinnishDeadkeyBarcode3 =
        "\0¨d1\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\r";

    public const string FinnishDeadkeyBarcode4 =
        "\0¨d1\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\r";

    public const string FinnishBarcode1 = "\0¨d2010251991944283521yCH(4äh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string FinnishBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string FinnishBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string FinnishBarcode4 = "\0¨d2010251991944283521Ew5ö2-zaMJ\u001d10\0´8Fn?PÄ\u001d17261002";
    public const string FinnishBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\u001d103kuW=L9\u001d17260304";
    public const string FinnishBarcode6 = "\0¨d201025199194428352151itJzCguA\u001d10ÖjÄ%e\0`P\u001d17251031";
    public const string FinnishBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\u001d10bA1hä4(\u001d17251214";
    public const string FinnishBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string FinnishBarcode9 = "\0¨d2010251991944283521)ÄP?nF8\0´9L\u001d10,_vIgTS\u001d17260218";
    public const string FinnishBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Ä\u001d10Qo0:7rU\u001d17260703";
    public const string FinnishBarcode11 = "\0¨d2010251991944283521ÄjÖAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string FinnishBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\u001d102ö5wEY+\u001d17250404";

    public const string FinnishWithSamiBaseline =
        "\0¨d1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0        \r";

    public const string FinnishWithSamiDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\r";

    public const string FinnishWithSamiDeadkeyBarcode2 =
        "\0¨d1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\r";

    public const string FinnishWithSamiDeadkeyBarcode3 =
        "\0¨d1\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\r";

    public const string FinnishWithSamiDeadkeyBarcode4 =
        "\0¨d1\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\r";

    public const string FinnishWithSamiBarcode1 = "\0¨d2010251991944283521yCH(4äh1Ab\010DdVcX;t\017250209";
    public const string FinnishWithSamiBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string FinnishWithSamiBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\010)fpNxYi\017250405";
    public const string FinnishWithSamiBarcode4 = "\0¨d2010251991944283521Ew5ö2-zaMJ\010\0´8Fn?PÄ\017261002";
    public const string FinnishWithSamiBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\0103kuW=L9\017260304";
    public const string FinnishWithSamiBarcode6 = "\0¨d201025199194428352151itJzCguA\010ÖjÄ%e\0`P\017251031";
    public const string FinnishWithSamiBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string FinnishWithSamiBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string FinnishWithSamiBarcode9 = "\0¨d2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string FinnishWithSamiBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Ä\010Qo0:7rU\017260703";
    public const string FinnishWithSamiBarcode11 = "\0¨d2010251991944283521ÄjÖAugCzJt\010/ZJMaz-\017251126";
    public const string FinnishWithSamiBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\0102ö5wEY+\017250404";

    public const string FrenchBaseline =
        "$d&  1 % 5 7 ù 9 0 8 + ; ) : ! à & é \" ' ( - è _ ç M m . = / § Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^* $ 6 ² \0¨µ £ \0    \u001d    \u001c    \u001f    \0    \r";

    public const string FrenchDeadkeyBarcode1 =
        "$d&\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^+\0^;\0^)\0^:\0^!\0^à\0^&\0^é\0^\"\0^'\0^(\0^-\0^è\0^_\0^ç\0^M\0^m\0^.\0^=\0^/\0^§\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^*\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^µ\0^£\0\0^";

    public const string FrenchDeadkeyBarcode2 =
        "$d&\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨+\0¨;\0¨)\0¨:\0¨!\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨-\0¨è\0¨_\0¨ç\0¨M\0¨m\0¨.\0¨=\0¨/\0¨§\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨*\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨µ\0¨£\0\0¨";

    public const string FrenchBarcode1 = "$déà&àé(&çç&ç''é_\"(é&yCH8'ùh&Qb\u001d&àDdVcX.t\u001d&èé(àéàç";
    public const string FrenchBarcode2 = "$déà&àé(&çç&ç''é_\"(é&TgIv§;-B,K\u001d&à:GRs1aO\u001d&èé(àèéç";
    public const string FrenchBarcode3 = "$déà&àé(&çç&ç''é_\"(é&W7Urè/àoAS\u001d&à9fpNxYi\u001d&èé(à'à(";
    public const string FrenchBarcode4 = "$déà&àé(&çç&ç''é_\"(é&Ez(mé!wq?J\u001d&à=_Fn°P%\u001d&èé-&ààé";
    public const string FrenchBarcode5 = "$déà&àé(&çç&ç''é_\"(é&(j'CltEc)Y\u001d&à\"kuZ0Lç\u001d&èé-à\"à'";
    public const string FrenchBarcode6 = "$déà&àé(&çç&ç''é_\"(é&(&itJwCguQ\u001d&àMj%5e+P\u001d&èé(&à\"&";
    public const string FrenchBarcode7 = "$déà&àé(&çç&ç''é_\"(é&t.XcVdDàa1\u001d&àbQ&hù'8\u001d&èé(&é&'";
    public const string FrenchBarcode8 = "$déà&àé(&çç&ç''é_\"(é&sRG:iYxNpf\u001d&àHCyK,B-\u001d&èé-à-éà";
    public const string FrenchBarcode9 = "$déà&àé(&çç&ç''é_\"(é&9%P°nF_=çL\u001d&à;§vIgTS\u001d&èé-àé&_";
    public const string FrenchBarcode10 = "$déà&àé(&çç&ç''é_\"(é&0Zuk\"P+e5%\u001d&àAoà/èrU\u001d&èé-àèà\"";
    public const string FrenchBarcode11 = "$déà&àé(&çç&ç''é_\"(é&%jMQugCwJt\u001d&à7WJ?qw!\u001d&èé(&&é-";
    public const string FrenchBarcode12 = "$déà&àé(&çç&ç''é_\"(é&HIrAçAeTyA\u001d&àém(zEY)\u001d&èé(à'à'";

    public const string GermanBaseline =
        "+d1  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \u001d    \u001c    \u001e    \0    \r";

    public const string GermanDeadkeyBarcode1 =
        "+d1\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\r";

    public const string GermanDeadkeyBarcode2 =
        "+d1\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\r";

    public const string GermanDeadkeyBarcode3 =
        "+d1\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\r";

    public const string GermanBarcode1 = "+d2010251991944283521zCH(4äh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string GermanBarcode2 = "+d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string GermanBarcode3 = "+d2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string GermanBarcode4 = "+d2010251991944283521Ew5ö2-yaMJ\u001d10\0´8Fn?PÄ\u001d17261002";
    public const string GermanBarcode5 = "+d20102519919442835215j4CltEcßZ\u001d103kuW=L9\u001d17260304";
    public const string GermanBarcode6 = "+d201025199194428352151itJyCguA\u001d10ÖjÄ%e\0`P\u001d17251031";
    public const string GermanBarcode7 = "+d2010251991944283521t;XcVdD0q!\u001d10bA1hä4(\u001d17251214";
    public const string GermanBarcode8 = "+d2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string GermanBarcode9 = "+d2010251991944283521)ÄP?nF8\0´9L\u001d10,_vIgTS\u001d17260218";
    public const string GermanBarcode10 = "+d2010251991944283521=Wuk3P\0è%Ä\u001d10Qo0:7rU\u001d17260703";
    public const string GermanBarcode11 = "+d2010251991944283521ÄjÖAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string GermanBarcode12 = "+d2010251991944283521HIrQ9QeTzQ\u001d102ö5wEZß\u001d17250404";

    public const string GermanIbmBaseline =
        "+d1  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \u001d    \u001c    \u001e    \0    \r";

    public const string GermanIbmDeadkeyBarcode1 =
        "+d1\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\r";

    public const string GermanIbmDeadkeyBarcode2 =
        "+d1\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\r";

    public const string GermanIbmDeadkeyBarcode3 =
        "+d1\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\r";

    public const string GermanIbmBarcode1 = "+d2010251991944283521zCH(4äh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string GermanIbmBarcode2 = "+d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string GermanIbmBarcode3 = "+d2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string GermanIbmBarcode4 = "+d2010251991944283521Ew5ö2-yaMJ\u001d10\0´8Fn?PÄ\u001d17261002";
    public const string GermanIbmBarcode5 = "+d20102519919442835215j4CltEcßZ\u001d103kuW=L9\u001d17260304";
    public const string GermanIbmBarcode6 = "+d201025199194428352151itJyCguA\u001d10ÖjÄ%e\0`P\u001d17251031";
    public const string GermanIbmBarcode7 = "+d2010251991944283521t;XcVdD0q!\u001d10bA1hä4(\u001d17251214";
    public const string GermanIbmBarcode8 = "+d2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string GermanIbmBarcode9 = "+d2010251991944283521)ÄP?nF8\0´9L\u001d10,_vIgTS\u001d17260218";
    public const string GermanIbmBarcode10 = "+d2010251991944283521=Wuk3P\0è%Ä\u001d10Qo0:7rU\u001d17260703";
    public const string GermanIbmBarcode11 = "+d2010251991944283521ÄjÖAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string GermanIbmBarcode12 = "+d2010251991944283521HIrQ9QeTzQ\u001d102ö5wEZß\u001d17250404";

    public const string HungarianBaseline =
        "úd1  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y Ü a b c d e f g h i j k l m n o p q r s t u v w x z y   + ! \" ő ű ú / 0 Ő Ű Ú §    \u001d    \u001c    \u001e    \0    \r";

    public const string HungarianBarcode1 = "úd2ö1ö251991944283521zCH(4áh1Ab\u001d1öDdVcX?t\u001d1725ö2ö9";
    public const string HungarianBarcode2 = "úd2ö1ö251991944283521TgIv_,6BmK\u001d1ö.GRs'qO\u001d1725ö729";
    public const string HungarianBarcode3 = "úd2ö1ö251991944283521Y=Ur7:öoQS\u001d1ö)fpNxZi\u001d1725ö4ö5";
    public const string HungarianBarcode4 = "úd2ö1ö251991944283521Ew5é2-yaMJ\u001d1öó8FnÜPÁ\u001d17261öö2";
    public const string HungarianBarcode5 = "úd2ö1ö2519919442835215j4CltEcüZ\u001d1ö3kuWÖL9\u001d1726ö3ö4";
    public const string HungarianBarcode6 = "úd2ö1ö25199194428352151itJyCguA\u001d1öÉjÁ%eÓP\u001d17251ö31";
    public const string HungarianBarcode7 = "úd2ö1ö251991944283521t?XcVdDöq'\u001d1öbA1há4(\u001d17251214";
    public const string HungarianBarcode8 = "úd2ö1ö251991944283521sRG.iZxNpf\u001d1öHCzKmB6\u001d1726ö62ö";
    public const string HungarianBarcode9 = "úd2ö1ö251991944283521)ÁPÜnF8ó9L\u001d1ö,_vIgTS\u001d1726ö218";
    public const string HungarianBarcode10 = "úd2ö1ö251991944283521ÖWuk3PÓe%Á\u001d1öQoö:7rU\u001d1726ö7ö3";
    public const string HungarianBarcode11 = "úd2ö1ö251991944283521ÁjÉAugCyJt\u001d1ö=YJMay-\u001d17251126";
    public const string HungarianBarcode12 = "úd2ö1ö251991944283521HIrQ9QeTzQ\u001d1ö2é5wEZü\u001d1725ö4ö4";

    public const string Hungarian101KeyBaseline =
        "úd1  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ü a b c d e f g h i j k l m n o p q r s t u v w x y z   + ! \" ő ű ú / í Ő Ű Ú Í    \u001d    \u001c    \u001e        \r";

    public const string Hungarian101KeyBarcode1 = "úd2ö1ö251991944283521yCH(4áh1Ab\u001d1öDdVcX?t\u001d1725ö2ö9";
    public const string Hungarian101KeyBarcode2 = "úd2ö1ö251991944283521TgIv_,6BmK\u001d1ö.GRs'qO\u001d1725ö729";
    public const string Hungarian101KeyBarcode3 = "úd2ö1ö251991944283521Z=Ur7:öoQS\u001d1ö)fpNxYi\u001d1725ö4ö5";
    public const string Hungarian101KeyBarcode4 = "úd2ö1ö251991944283521Ew5é2-zaMJ\u001d1öó8FnÜPÁ\u001d17261öö2";
    public const string Hungarian101KeyBarcode5 = "úd2ö1ö2519919442835215j4CltEcüY\u001d1ö3kuWÖL9\u001d1726ö3ö4";
    public const string Hungarian101KeyBarcode6 = "úd2ö1ö25199194428352151itJzCguA\u001d1öÉjÁ%eÓP\u001d17251ö31";
    public const string Hungarian101KeyBarcode7 = "úd2ö1ö251991944283521t?XcVdDöq'\u001d1öbA1há4(\u001d17251214";
    public const string Hungarian101KeyBarcode8 = "úd2ö1ö251991944283521sRG.iYxNpf\u001d1öHCyKmB6\u001d1726ö62ö";
    public const string Hungarian101KeyBarcode9 = "úd2ö1ö251991944283521)ÁPÜnF8ó9L\u001d1ö,_vIgTS\u001d1726ö218";
    public const string Hungarian101KeyBarcode10 = "úd2ö1ö251991944283521ÖWuk3PÓe%Á\u001d1öQoö:7rU\u001d1726ö7ö3";
    public const string Hungarian101KeyBarcode11 = "úd2ö1ö251991944283521ÁjÉAugCzJt\u001d1ö=ZJMaz-\u001d17251126";
    public const string Hungarian101KeyBarcode12 = "úd2ö1ö251991944283521HIrQ9QeTyQ\u001d1ö2é5wEYü\u001d1725ö4ö4";

    public const string IcelandicBaseline =
        "'d1  ! ' % / \0´) = ( _ , ö . þ 0 1 2 3 4 5 6 7 8 9 Æ æ ; - : Þ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ö a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" ð + ' & \0°Ð * ? \0¨   \u001d    \0    \u001e        \r";

    public const string IcelandicDeadkeyBarcode1 =
        "'d1\0´!\0´'\0´#\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´_\0´,\0´ö\0´.\0´þ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´-\0´:\0´Þ\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´ð\0´+\0´'\0´&\0´Ö\0´°\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Ð\0´*\0´?\0´¨\r";

    public const string IcelandicDeadkeyBarcode2 =
        "'d1\0°!\0°'\0°#\0°$\0°%\0°/\0°´\0°)\0°=\0°(\0°_\0°,\0°ö\0°.\0°þ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Æ\0°æ\0°;\0°-\0°:\0°Þ\0°\"\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0°Z\0°ð\0°+\0°'\0°&\0°Ö\0°°\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0°z\0°Ð\0°*\0°?\0°¨\r";

    public const string IcelandicDeadkeyBarcode3 =
        "'d1\0¨!\0¨'\0¨#\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨_\0¨,\0¨ö\0¨.\0¨þ\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨-\0¨:\0¨Þ\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨ð\0¨+\0¨'\0¨&\0¨Ö\0¨°\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Ð\0¨*\0¨?\0¨¨\r";

    public const string IcelandicBarcode1 = "'d2010251991944283521yCH(4\0´h1Ab\u001d10DdVcX;t\u001d17250209";
    public const string IcelandicBarcode2 = "'d2010251991944283521TgIvÞ,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string IcelandicBarcode3 = "'d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string IcelandicBarcode4 = "'d2010251991944283521Ew5æ2þzaMJ\u001d10-8FnÖP'\u001d17261002";
    public const string IcelandicBarcode5 = "'d20102519919442835215j4CltEcöY\u001d103kuW=L9\u001d17260304";
    public const string IcelandicBarcode6 = "'d201025199194428352151itJzCguA\u001d10Æj'%e_P\u001d17251031";
    public const string IcelandicBarcode7 = "'d2010251991944283521t;XcVdD0q!\u001d10bA1h\0´4(\u001d17251214";
    public const string IcelandicBarcode8 = "'d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string IcelandicBarcode9 = "'d2010251991944283521)'PÖnF8-9L\u001d10,ÞvIgTS\u001d17260218";
    public const string IcelandicBarcode10 = "'d2010251991944283521=Wuk3P_e%'\u001d10Qo0:7rU\u001d17260703";
    public const string IcelandicBarcode11 = "'d2010251991944283521'jÆAugCzJt\u001d10/ZJMazþ\u001d17251126";
    public const string IcelandicBarcode12 = "'d2010251991944283521HIrQ9QeTyQ\u001d102æ5wEYö\u001d17250404";

    public const string ItalianBaseline =
        "+d1  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \u001d    \u001c    \u001e    \0    \r";

    public const string ItalianBarcode1 = "+d2010251991944283521yCH(4àh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string ItalianBarcode2 = "+d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string ItalianBarcode3 = "+d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string ItalianBarcode4 = "+d2010251991944283521Ew5ò2-zaMJ\u001d10ì8Fn?P°\u001d17261002";
    public const string ItalianBarcode5 = "+d20102519919442835215j4CltEc'Y\u001d103kuW=L9\u001d17260304";
    public const string ItalianBarcode6 = "+d201025199194428352151itJzCguA\u001d10çj°%e^P\u001d17251031";
    public const string ItalianBarcode7 = "+d2010251991944283521t;XcVdD0q!\u001d10bA1hà4(\u001d17251214";
    public const string ItalianBarcode8 = "+d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string ItalianBarcode9 = "+d2010251991944283521)°P?nF8ì9L\u001d10,_vIgTS\u001d17260218";
    public const string ItalianBarcode10 = "+d2010251991944283521=Wuk3P^e%°\u001d10Qo0:7rU\u001d17260703";
    public const string ItalianBarcode11 = "+d2010251991944283521°jçAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string ItalianBarcode12 = "+d2010251991944283521HIrQ9QeTyQ\u001d102ò5wEY'\u001d17250404";

    public const string Italian142Baseline =
        "+d1  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \u001d    \u001c    \u001e    \0    \r";

    public const string Italian142Barcode1 = "+d2010251991944283521yCH(4àh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string Italian142Barcode2 = "+d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string Italian142Barcode3 = "+d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string Italian142Barcode4 = "+d2010251991944283521Ew5ò2-zaMJ\u001d10ì8Fn?P°\u001d17261002";
    public const string Italian142Barcode5 = "+d20102519919442835215j4CltEc'Y\u001d103kuW=L9\u001d17260304";
    public const string Italian142Barcode6 = "+d201025199194428352151itJzCguA\u001d10çj°%e^P\u001d17251031";
    public const string Italian142Barcode7 = "+d2010251991944283521t;XcVdD0q!\u001d10bA1hà4(\u001d17251214";
    public const string Italian142Barcode8 = "+d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string Italian142Barcode9 = "+d2010251991944283521)°P?nF8ì9L\u001d10,_vIgTS\u001d17260218";
    public const string Italian142Barcode10 = "+d2010251991944283521=Wuk3P^e%°\u001d10Qo0:7rU\u001d17260703";
    public const string Italian142Barcode11 = "+d2010251991944283521°jçAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string Italian142Barcode12 = "+d2010251991944283521HIrQ9QeTyQ\u001d102ò5wEY'\u001d17250404";

    public const string LatvianBaseline =
        "hs1  ! \0°% & \0´( ) × F , - . ļ 0 1 2 3 4 5 6 7 8 9 C c ; f : Ļ Š P Ī S J I L D Z A T E Ā O Ē Č Ū R U M N K G B V Ņ _ š p ī s j i l d z a t e ā o ē č ū r u m n k g b v ņ   » $ « ž ķ h / ­ Ž Ķ H ?    \b    \u001c    \u001e        \r";

    public const string LatvianDeadkeyBarcode1 =
        "hs1\0°!\0°°\0°»\0°$\0°%\0°&\0°´\0°(\0°)\0°×\0°F\0°,\0°-\0°.\0°ļ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°C\0°c\0°;\0°f\0°:\0°Ļ\0°«\0°Š\0°P\0°Ī\0°S\0°J\0°I\0°L\0°D\0Ż\0Å\0°T\0Ė\0°Ā\0°O\0°Ē\0°Č\0°Ū\0°R\0°U\0°M\0°N\0°K\0°G\0°B\0°V\0°Ņ\0°ž\0°ķ\0°h\0°/\0°_\0°­\0°š\0°p\0°ī\0°s\0°j\0°i\0°l\0°d\0ż\0å\0°t\0ė\0°ā\0°o\0°ē\0°č\0°ū\0°r\0°u\0°m\0°n\0°k\0ġ\0°b\0°v\0°ņ\0°Ž\0°Ķ\0°H\0°?\r";

    public const string LatvianDeadkeyBarcode2 =
        "hs1\0´!\0´°\0´»\0´$\0´%\0´&\0´´\0´(\0´)\0´×\0´F\0´,\0´-\0´.\0´ļ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0Ć\0ć\0´;\0´f\0´:\0´Ļ\0´«\0´Š\0´P\0´Ī\0Ś\0´J\0´I\0´L\0´D\0Ź\0´A\0´T\0É\0´Ā\0Ó\0´Ē\0´Č\0´Ū\0´R\0´U\0´M\0Ń\0´K\0´G\0´B\0´V\0´Ņ\0´ž\0´ķ\0´h\0´/\0´_\0´­\0´š\0´p\0´ī\0ś\0´j\0´i\0´l\0´d\0ź\0´a\0´t\0é\0´ā\0ó\0´ē\0´č\0´ū\0´r\0´u\0´m\0ń\0´k\0´g\0´b\0´v\0´ņ\0´Ž\0´Ķ\0´H\0´?\r";

    public const string LatvianBarcode1 = "hs2010251991944283521vĪD×4\0´d1Šp\b10SsKīB;m\b17250209";
    public const string LatvianBarcode2 = "hs2010251991944283521MlZkĻ,6PāT\b10.LRu!ūĒ\b17250729";
    public const string LatvianBarcode3 = "hs2010251991944283521Ņ&Nr7:0ēŪU\b10(ičObVz\b17250405";
    public const string LatvianBarcode4 = "hs2010251991944283521Jg5c2ļņšĀA\b10f8Io_Č\0\b°17261002";
    public const string LatvianBarcode5 = "hs20102519919442835215a4ĪemJī-V\b103tnG)E9\b17260304";
    public const string LatvianBarcode6 = "hs201025199194428352151zmAņĪlnŠ\b10Ca\0°%jFČ\b17251031";
    public const string LatvianBarcode7 = "hs2010251991944283521m;BīKsS0ū!\b10pŠ1d\0´4×\b17251214";
    public const string LatvianBarcode8 = "hs2010251991944283521uRL.zVbOči\b10DĪvTāP6\b17260620";
    public const string LatvianBarcode9 = "hs2010251991944283521(\0°Č_oI8f9E\b10,ĻkZlMU\b17260218";
    public const string LatvianBarcode10 = "hs2010251991944283521)Gnt3ČFj%\0\b°10Ūē0:7rN\b17260703";
    public const string LatvianBarcode11 = "hs2010251991944283521\0åCŠnlĪņAm\b10&ŅAĀšņļ\b17251126";
    public const string LatvianBarcode12 = "hs2010251991944283521DZrŪ9ŪjMvŪ\b102c5gJV-\b17250404";

    public const string LatvianQwertyBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \0°] ^ ` { | } \0~   \u001d    \u001c    \u001e        \r";

    public const string LatvianQwertyDeadkeyBarcode1 =
        "]d1\0°!\0°\"\0°#\0°$\0°%\0°&\0°'\0°(\0°)\0°*\0°+\0°,\0°-\0°.\0°/\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°:\0°;\0°<\0°=\0°>\0°?\0°@\0Å\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0Ż\0°[\0°°\0°]\0°^\0°_\0°`\0å\0°b\0°c\0°d\0ė\0°f\0ġ\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0ż\0°{\0°|\0°}\0°~\r";

    public const string LatvianQwertyDeadkeyBarcode2 =
        "]d1\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~[\0~°\0~]\0~^\0~_\0~`\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~{\0~|\0~}\0~~\r";

    public const string LatvianQwertyBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string LatvianQwertyBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string LatvianQwertyBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string LatvianQwertyBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P\"\u001d17261002";
    public const string LatvianQwertyBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string LatvianQwertyBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j\"%e+P\u001d17251031";
    public const string LatvianQwertyBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string LatvianQwertyBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string LatvianQwertyBarcode9 = "]d2010251991944283521(\"P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string LatvianQwertyBarcode10 = "]d2010251991944283521)Wuk3P+e%\"\u001d10Qo0>7rU\u001d17260703";
    public const string LatvianQwertyBarcode11 = "]d2010251991944283521\"j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string LatvianQwertyBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string LatvianStandardBaseline =
        "]d1  ! \0\"% & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \u001d    \u001c    \0        \r";

    public const string LatvianStandardDeadkeyBarcode1 =
        "]d1\0\"!\0\"\"\0\"#\0§\0°\0±\0\"'\0\"(\0\")\0×\0\"+\0\",\0—\0\".\0\"/\0\"0\0\"1\0\"2\0\"3\0§\0°\0\"6\0±\0×\0\"9\0\":\0\";\0\"<\0\"=\0\">\0\"?\0\"@\0Ā\0\"B\0Č\0\"D\0Ē\0\"F\0Ģ\0\"H\0Ī\0\"J\0Ķ\0Ļ\0\"M\0Ņ\0Ō\0\"P\0\"Q\0Ŗ\0Š\0\"T\0Ū\0\"V\0\"W\0\"X\0\"Y\0Ž\0\"[\0\"\\\0\"]\0\"^\0—\0\"`\0Ā\0\"b\0Č\0\"d\0Ē\0\"f\0Ģ\0\"h\0Ī\0\"j\0Ķ\0Ļ\0\"m\0Ņ\0Ō\0\"p\0\"q\0Ŗ\0Š\0\"t\0Ū\0\"v\0\"w\0\"x\0\"y\0Ž\0\"{\0\"|\0\"}\0\"~\r";

    public const string LithuanianBaseline =
        "]dą  Ą \" Į Ų ' ( ) Ū Ž , - . / 0 ą č ę ė į š ų ū 9 : ; < ž > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   Ę Ė Č [ \\ ] Š ` { | } ~    \u001d    \u001c    \u001e        \r";

    public const string LithuanianBarcode1 = "]dč0ą0čįą99ą9ėėčūęįčąyCHŪė'hąAb\u001dą0DdVcX<t\u001dąųčį0č09";
    public const string LithuanianBarcode2 = "]dč0ą0čįą99ą9ėėčūęįčąTgIv?,šBmK\u001dą0.GRsĄqO\u001dąųčį0ųč9";
    public const string LithuanianBarcode3 = "]dč0ą0čįą99ą9ėėčūęįčąZŲUrų>0oQS\u001dą0(fpNxYi\u001dąųčį0ė0į";
    public const string LithuanianBarcode4 = "]dč0ą0čįą99ą9ėėčūęįčąEwį;č/zaMJ\u001dą0žūFn_P\"\u001dąųčšą00č";
    public const string LithuanianBarcode5 = "]dč0ą0čįą99ą9ėėčūęįčąįjėCltEc-Y\u001dą0ękuW)L9\u001dąųčš0ę0ė";
    public const string LithuanianBarcode6 = "]dč0ą0čįą99ą9ėėčūęįčąįąitJzCguA\u001dą0:j\"ĮeŽP\u001dąųčįą0ęą";
    public const string LithuanianBarcode7 = "]dč0ą0čįą99ą9ėėčūęįčąt<XcVdD0qĄ\u001dą0bAąh'ėŪ\u001dąųčįąčąė";
    public const string LithuanianBarcode8 = "]dč0ą0čįą99ą9ėėčūęįčąsRG.iYxNpf\u001dą0HCyKmBš\u001dąųčš0šč0";
    public const string LithuanianBarcode9 = "]dč0ą0čįą99ą9ėėčūęįčą(\"P_nFūž9L\u001dą0,?vIgTS\u001dąųčš0čąū";
    public const string LithuanianBarcode10 = "]dč0ą0čįą99ą9ėėčūęįčą)WukęPŽeĮ\"\u001dą0Qo0>ųrU\u001dąųčš0ų0ę";
    public const string LithuanianBarcode11 = "]dč0ą0čįą99ą9ėėčūęįčą\"j:AugCzJt\u001dą0ŲZJMaz/\u001dąųčįąąčš";
    public const string LithuanianBarcode12 = "]dč0ą0čįą99ą9ėėčūęįčąHIrQ9QeTyQ\u001dą0č;įwEY-\u001dąųčį0ė0ė";

    public const string LithuanianIbmBaseline =
        "“d!  1 Ė 5 7 ė 9 0 8 = č _ š ę ) ! \" / ; : , . ? ( Ų ų Č + Š Ę A B C D E F G H I J K L M N O P Ą R S T U V Ž Ū Y Z - a b c d e f g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į | “ 6 ` Į \\ ” ~    \u001d    \u001c    \u001e        \r";

    public const string LithuanianIbmBarcode1 =
        "“d\")!)\":!((!(;;\"?/:\"!yCH8;ėh!Ab\u001d!)DdVcŪČt\u001d!.\":)\")(";

    public const string LithuanianIbmBarcode2 =
        "“d\")!)\":!((!(;;\"?/:\"!TgIvĘč,BmK\u001d!)šGRs1ąO\u001d!.\":).\"(";

    public const string LithuanianIbmBarcode3 =
        "“d\")!)\":!((!(;;\"?/:\"!Z7Ur.Š)oĄS\u001d!)9fpNūYi\u001d!.\":);):";

    public const string LithuanianIbmBarcode4 =
        "“d\")!)\":!((!(;;\"?/:\"!Ež:ų\"ęzaMJ\u001d!)+?Fn-PĖ\u001d!.\",!))\"";

    public const string LithuanianIbmBarcode5 =
        "“d\")!)\":!((!(;;\"?/:\"!:j;CltEc_Y\u001d!)/kuŽ0L(\u001d!.\",)/);";

    public const string LithuanianIbmBarcode6 =
        "“d\")!)\":!((!(;;\"?/:\"!:!itJzCguA\u001d!)ŲjĖ5e=P\u001d!.\":!)/!";

    public const string LithuanianIbmBarcode7 =
        "“d\")!)\":!((!(;;\"?/:\"!tČŪcVdD)ą1\u001d!)bA!hė;8\u001d!.\":!\"!;";

    public const string LithuanianIbmBarcode8 =
        "“d\")!)\":!((!(;;\"?/:\"!sRGšiYūNpf\u001d!)HCyKmB,\u001d!.\",),\")";

    public const string LithuanianIbmBarcode9 =
        "“d\")!)\":!((!(;;\"?/:\"!9ĖP-nF?+(L\u001d!)čĘvIgTS\u001d!.\",)\"!?";

    public const string LithuanianIbmBarcode10 =
        "“d\")!)\":!((!(;;\"?/:\"!0Žuk/P=e5Ė\u001d!)Ąo)Š.rU\u001d!.\",).)/";

    public const string LithuanianIbmBarcode11 =
        "“d\")!)\":!((!(;;\"?/:\"!ĖjŲAugCzJt\u001d!)7ZJMazę\u001d!.\":!!\",";

    public const string LithuanianIbmBarcode12 =
        "“d\")!)\":!((!(;;\"?/:\"!HIrĄ(ĄeTyĄ\u001d!)\"ų:žEY_\u001d!.\":););";

    public const string LithuanianStandardBaseline =
        "wd!  1 Ė 5 7 ė 9 0 8 X č ? f ę ) ! - / ; : , . = ( Ų ų Č x F Ę A B C D E Š G H I J K L M N O P Ą R S T U V Ž Ū Y Z + a b c d e š g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į q w 6 ` Į Q W ~    \0    \0    \0        \r";

    public const string LithuanianStandardBarcode1 = "wd-)!)-:!((!(;;-=/:-!yCH8;ėh!Ab\0!)DdVcŪČt\0!.-:)-)(";
    public const string LithuanianStandardBarcode2 = "wd-)!)-:!((!(;;-=/:-!TgIvĘč,BmK\0!)fGRs1ąO\0!.-:).-(";
    public const string LithuanianStandardBarcode3 = "wd-)!)-:!((!(;;-=/:-!Z7Ur.F)oĄS\0!)9špNūYi\0!.-:);):";
    public const string LithuanianStandardBarcode4 = "wd-)!)-:!((!(;;-=/:-!Ež:ų-ęzaMJ\0!)x=Šn+PĖ\0!.-,!))-";
    public const string LithuanianStandardBarcode5 = "wd-)!)-:!((!(;;-=/:-!:j;CltEc?Y\0!)/kuŽ0L(\0!.-,)/);";
    public const string LithuanianStandardBarcode6 = "wd-)!)-:!((!(;;-=/:-!:!itJzCguA\0!)ŲjĖ5eXP\0!.-:!)/!";
    public const string LithuanianStandardBarcode7 = "wd-)!)-:!((!(;;-=/:-!tČŪcVdD)ą1\0!)bA!hė;8\0!.-:!-!;";
    public const string LithuanianStandardBarcode8 = "wd-)!)-:!((!(;;-=/:-!sRGfiYūNpš\0!)HCyKmB,\0!.-,),-)";
    public const string LithuanianStandardBarcode9 = "wd-)!)-:!((!(;;-=/:-!9ĖP+nŠ=x(L\0!)čĘvIgTS\0!.-,)-!=";
    public const string LithuanianStandardBarcode10 = "wd-)!)-:!((!(;;-=/:-!0Žuk/PXe5Ė\0!)Ąo)F.rU\0!.-,).)/";
    public const string LithuanianStandardBarcode11 = "wd-)!)-:!((!(;;-=/:-!ĖjŲAugCzJt\0!)7ZJMazę\0!.-:!!-,";
    public const string LithuanianStandardBarcode12 = "wd-)!)-:!((!(;;-=/:-!HIrĄ(ĄeTyĄ\0!)-ų:žEY?\0!.-:););";

    public const string LuxembourgishBaseline =
        "\0¨d1  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \u001d    \u001c    \u001e    \0    \r";

    public const string LuxembourgishDeadkeyBarcode1 =
        "\0¨d1\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\r";

    public const string LuxembourgishDeadkeyBarcode2 =
        "\0¨d1\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\r";

    public const string LuxembourgishDeadkeyBarcode3 =
        "\0¨d1\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\r";

    public const string LuxembourgishBarcode1 = "\0¨d2010251991944283521zCH(4àh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string LuxembourgishBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs+qO\u001d17250729";
    public const string LuxembourgishBarcode3 = "\0¨d2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string LuxembourgishBarcode4 = "\0¨d2010251991944283521Ew5é2-yaMJ\u001d10\0^8Fn?Pä\u001d17261002";
    public const string LuxembourgishBarcode5 = "\0¨d20102519919442835215j4CltEc'Z\u001d103kuW=L9\u001d17260304";
    public const string LuxembourgishBarcode6 = "\0¨d201025199194428352151itJyCguA\u001d10öjä%e\0`P\u001d17251031";
    public const string LuxembourgishBarcode7 = "\0¨d2010251991944283521t;XcVdD0q+\u001d10bA1hà4(\u001d17251214";
    public const string LuxembourgishBarcode8 = "\0¨d2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string LuxembourgishBarcode9 = "\0¨d2010251991944283521)äP?nF8\0^9L\u001d10,_vIgTS\u001d17260218";
    public const string LuxembourgishBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%ä\u001d10Qo0:7rU\u001d17260703";
    public const string LuxembourgishBarcode11 = "\0¨d2010251991944283521äjöAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string LuxembourgishBarcode12 = "\0¨d2010251991944283521HIrQ9QeTzQ\u001d102é5wEZ'\u001d17250404";

    public const string Maltese47KeyBaseline =
        "ħd1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ @ ġ ż ħ ^ ċ Ġ Ż Ħ Ċ    \0    \0    \0        \r";

    public const string Maltese47KeyBarcode1 = "ħd2010251991944283521yCH*4'h1Ab\010DdVcX<t\017250209";
    public const string Maltese47KeyBarcode2 = "ħd2010251991944283521TgIv?,6BmK\010.GRs!qO\017250729";
    public const string Maltese47KeyBarcode3 = "ħd2010251991944283521Z&Ur7>0oQS\010(fpNxYi\017250405";
    public const string Maltese47KeyBarcode4 = "ħd2010251991944283521Ew5;2/zaMJ\010=8Fn_P\"\017261002";
    public const string Maltese47KeyBarcode5 = "ħd20102519919442835215j4CltEc-Y\0103kuW)L9\017260304";
    public const string Maltese47KeyBarcode6 = "ħd201025199194428352151itJzCguA\010:j\"%e+P\017251031";
    public const string Maltese47KeyBarcode7 = "ħd2010251991944283521t<XcVdD0q!\010bA1h'4*\017251214";
    public const string Maltese47KeyBarcode8 = "ħd2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string Maltese47KeyBarcode9 = "ħd2010251991944283521(\"P_nF8=9L\010,?vIgTS\017260218";
    public const string Maltese47KeyBarcode10 = "ħd2010251991944283521)Wuk3P+e%\"\010Qo0>7rU\017260703";
    public const string Maltese47KeyBarcode11 = "ħd2010251991944283521\"j:AugCzJt\010&ZJMaz/\017251126";
    public const string Maltese47KeyBarcode12 = "ħd2010251991944283521HIrQ9QeTyQ\0102;5wEY-\017250404";

    public const string Maltese48KeyBaseline =
        "ħd1  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ \" ġ # ħ ^ ċ Ġ ~ Ħ Ċ    \0    \0    \0        \r";

    public const string Maltese48KeyBarcode1 = "ħd2010251991944283521yCH*4'h1Ab\010DdVcX<t\017250209";
    public const string Maltese48KeyBarcode2 = "ħd2010251991944283521TgIv?,6BmK\010.GRs!qO\017250729";
    public const string Maltese48KeyBarcode3 = "ħd2010251991944283521Z&Ur7>0oQS\010(fpNxYi\017250405";
    public const string Maltese48KeyBarcode4 = "ħd2010251991944283521Ew5;2/zaMJ\010=8Fn_P@\017261002";
    public const string Maltese48KeyBarcode5 = "ħd20102519919442835215j4CltEc-Y\0103kuW)L9\017260304";
    public const string Maltese48KeyBarcode6 = "ħd201025199194428352151itJzCguA\010:j@%e+P\017251031";
    public const string Maltese48KeyBarcode7 = "ħd2010251991944283521t<XcVdD0q!\010bA1h'4*\017251214";
    public const string Maltese48KeyBarcode8 = "ħd2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string Maltese48KeyBarcode9 = "ħd2010251991944283521(@P_nF8=9L\010,?vIgTS\017260218";
    public const string Maltese48KeyBarcode10 = "ħd2010251991944283521)Wuk3P+e%@\010Qo0>7rU\017260703";
    public const string Maltese48KeyBarcode11 = "ħd2010251991944283521@j:AugCzJt\010&ZJMaz/\017251126";
    public const string Maltese48KeyBarcode12 = "ħd2010251991944283521HIrQ9QeTyQ\0102;5wEY-\017250404";

    public const string NorwegianBaseline =
        "\0¨d1  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \u001d    \0    \u001e        \r";

    public const string NorwegianDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\r";

    public const string NorwegianDeadkeyBarcode2 =
        "\0¨d1\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\r";

    public const string NorwegianDeadkeyBarcode3 =
        "\0¨d1\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\r";

    public const string NorwegianBarcode1 = "\0¨d2010251991944283521yCH(4æh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string NorwegianBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string NorwegianBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string NorwegianBarcode4 = "\0¨d2010251991944283521Ew5ø2-zaMJ\u001d10\\8Fn?PÆ\u001d17261002";
    public const string NorwegianBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\u001d103kuW=L9\u001d17260304";
    public const string NorwegianBarcode6 = "\0¨d201025199194428352151itJzCguA\u001d10ØjÆ%e\0`P\u001d17251031";
    public const string NorwegianBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\u001d10bA1hæ4(\u001d17251214";
    public const string NorwegianBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string NorwegianBarcode9 = "\0¨d2010251991944283521)ÆP?nF8\\9L\u001d10,_vIgTS\u001d17260218";
    public const string NorwegianBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Æ\u001d10Qo0:7rU\u001d17260703";
    public const string NorwegianBarcode11 = "\0¨d2010251991944283521ÆjØAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string NorwegianBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\u001d102ø5wEY+\u001d17250404";

    public const string NorwegianWithSamiBaseline =
        "\0¨d1  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \0    \0    \0        \r";

    public const string NorwegianWithSamiDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`§\r";

    public const string NorwegianWithSamiDeadkeyBarcode2 =
        "\0¨d1\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\r";

    public const string NorwegianWithSamiDeadkeyBarcode3 =
        "\0¨d1\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^§\r";

    public const string NorwegianWithSamiBarcode1 = "\0¨d2010251991944283521yCH(4æh1Ab\010DdVcX;t\017250209";
    public const string NorwegianWithSamiBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string NorwegianWithSamiBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\010)fpNxYi\017250405";
    public const string NorwegianWithSamiBarcode4 = "\0¨d2010251991944283521Ew5ø2-zaMJ\010\\8Fn?PÆ\017261002";
    public const string NorwegianWithSamiBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\0103kuW=L9\017260304";
    public const string NorwegianWithSamiBarcode6 = "\0¨d201025199194428352151itJzCguA\010ØjÆ%e\0`P\017251031";
    public const string NorwegianWithSamiBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\010bA1hæ4(\017251214";
    public const string NorwegianWithSamiBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string NorwegianWithSamiBarcode9 = "\0¨d2010251991944283521)ÆP?nF8\\9L\010,_vIgTS\017260218";
    public const string NorwegianWithSamiBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Æ\010Qo0:7rU\017260703";
    public const string NorwegianWithSamiBarcode11 = "\0¨d2010251991944283521ÆjØAugCzJt\010/ZJMaz-\017251126";
    public const string NorwegianWithSamiBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\0102ø5wEY+\017250404";

    public const string Polish214Baseline =
        "śd1  ! ę % / ą ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ł ł ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ż ó ś & \0˛ń ź ć \0·   \u001d    \u001c    \u001e        \r";

    public const string Polish214DeadkeyBarcode1 =
        "śd1\0˛!\0˛ę\0˛#\0˛¤\0˛%\0˛/\0˛ą\0˛)\0˛=\0˛(\0˛*\0˛,\0˛+\0˛.\0˛-\0˛0\0˛1\0˛2\0˛3\0˛4\0˛5\0˛6\0˛7\0˛8\0˛9\0˛Ł\0˛ł\0˛;\0˛'\0˛:\0˛_\0˛\"\0Ą\0˛B\0˛C\0˛D\0Ę\0˛F\0˛G\0˛H\0˛I\0˛J\0˛K\0˛L\0˛M\0˛N\0˛O\0˛P\0˛Q\0˛R\0˛S\0˛T\0˛U\0˛V\0˛W\0˛X\0˛Z\0˛Y\0˛ż\0˛ó\0˛ś\0˛&\0˛?\0˛˛\0ą\0˛b\0˛c\0˛d\0ę\0˛f\0˛g\0˛h\0˛i\0˛j\0˛k\0˛l\0˛m\0˛n\0˛o\0˛p\0˛q\0˛r\0˛s\0˛t\0˛u\0˛v\0˛w\0˛x\0˛z\0˛y\0˛ń\0˛ź\0˛ć\0˛·\r";

    public const string Polish214DeadkeyBarcode2 =
        "śd1\0·!\0·ę\0·#\0·¤\0·%\0·/\0·ą\0·)\0·=\0·(\0·*\0·,\0·+\0·.\0·-\0·0\0·1\0·2\0·3\0·4\0·5\0·6\0·7\0·8\0·9\0·Ł\0·ł\0·;\0·'\0·:\0·_\0·\"\0·A\0·B\0·C\0·D\0·E\0·F\0·G\0·H\0·I\0·J\0·K\0·L\0·M\0·N\0·O\0·P\0·Q\0·R\0·S\0·T\0·U\0·V\0·W\0·X\0Ż\0·Y\0·ż\0·ó\0·ś\0·&\0·?\0·˛\0·a\0·b\0·c\0·d\0·e\0·f\0·g\0·h\0·i\0·j\0·k\0·l\0·m\0·n\0·o\0·p\0·q\0·r\0·s\0·t\0·u\0·v\0·w\0·x\0ż\0·y\0·ń\0·ź\0·ć\0··\r";

    public const string Polish214Barcode1 = "śd2010251991944283521zCH(4ąh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string Polish214Barcode2 = "śd2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string Polish214Barcode3 = "śd2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string Polish214Barcode4 = "śd2010251991944283521Ew5ł2-yaMJ\u001d10'8Fn?Pę\u001d17261002";
    public const string Polish214Barcode5 = "śd20102519919442835215j4CltEc+Z\u001d103kuW=L9\u001d17260304";
    public const string Polish214Barcode6 = "śd201025199194428352151itJyCguA\u001d10Łję%e*P\u001d17251031";
    public const string Polish214Barcode7 = "śd2010251991944283521t;XcVdD0q!\u001d10bA1hą4(\u001d17251214";
    public const string Polish214Barcode8 = "śd2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string Polish214Barcode9 = "śd2010251991944283521)ęP?nF8'9L\u001d10,_vIgTS\u001d17260218";
    public const string Polish214Barcode10 = "śd2010251991944283521=Wuk3P*e%ę\u001d10Qo0:7rU\u001d17260703";
    public const string Polish214Barcode11 = "śd2010251991944283521ęjŁAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string Polish214Barcode12 = "śd2010251991944283521HIrQ9QeTzQ\u001d102ł5wEZ+\u001d17250404";

    public const string PolishProgrammersBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } \0~   \u001d    \u001c    \u001e        \r";

    public const string PolishProgrammersDeadkeyBarcode1 =
        "]d1\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0Ą\0~B\0Ć\0~D\0Ę\0~F\0~G\0~H\0~I\0~J\0~K\0Ł\0~M\0Ń\0Ó\0~P\0~Q\0~R\0Ś\0~T\0~U\0~V\0~W\0Ź\0~Y\0Ż\0~[\0~\\\0~]\0~^\0~_\0~`\0ą\0~b\0ć\0~d\0ę\0~f\0~g\0~h\0~i\0~j\0~k\0ł\0~m\0ń\0ó\0~p\0~q\0~r\0ś\0~t\0~u\0~v\0~w\0ź\0~y\0ż\0~{\0~|\0~}\0~~\r";

    public const string PolishProgrammersBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string PolishProgrammersBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string PolishProgrammersBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";

    public const string PolishProgrammersBarcode4 =
        "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P\"\u001d17261002";

    public const string PolishProgrammersBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";

    public const string PolishProgrammersBarcode6 =
        "]d201025199194428352151itJzCguA\u001d10:j\"%e+P\u001d17251031";

    public const string PolishProgrammersBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string PolishProgrammersBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";

    public const string PolishProgrammersBarcode9 =
        "]d2010251991944283521(\"P_nF8=9L\u001d10,?vIgTS\u001d17260218";

    public const string PolishProgrammersBarcode10 =
        "]d2010251991944283521)Wuk3P+e%\"\u001d10Qo0>7rU\u001d17260703";

    public const string PolishProgrammersBarcode11 =
        "]d2010251991944283521\"j:AugCzJt\u001d10&ZJMaz/\u001d17251126";

    public const string PolishProgrammersBarcode12 =
        "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string PortugueseBaseline =
        "\0´d1  ! ª % / º ) = ( » , ' . - 0 1 2 3 4 5 6 7 8 9 Ç ç ; « : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" + \0~\0´& \\ * \0^\0`|    \0    \0    \u001e    \0    \r";

    public const string PortugueseDeadkeyBarcode1 =
        "\0´d1\0~!\0~ª\0~#\0~$\0~%\0~/\0~º\0~)\0~=\0~(\0~»\0~,\0~'\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ç\0~ç\0~;\0~«\0~:\0~_\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~+\0~~\0~´\0~&\0~?\0~\\\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~*\0~^\0~`\0~|\r";

    public const string PortugueseDeadkeyBarcode2 =
        "\0´d1\0´!\0´ª\0´#\0´$\0´%\0´/\0´º\0´)\0´=\0´(\0´»\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ç\0´ç\0´;\0´«\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´+\0´~\0´´\0´&\0´?\0´\\\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´*\0´^\0´`\0´|\r";

    public const string PortugueseDeadkeyBarcode3 =
        "\0´d1\0^!\0^ª\0^#\0^$\0^%\0^/\0^º\0^)\0^=\0^(\0^»\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ç\0^ç\0^;\0^«\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^+\0^~\0^´\0^&\0^?\0^\\\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^*\0^^\0^`\0^|";

    public const string PortugueseDeadkeyBarcode4 =
        "\0´d1\0`!\0`ª\0`#\0`$\0`%\0`/\0`º\0`)\0`=\0`(\0`»\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ç\0`ç\0`;\0`«\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`+\0`~\0`´\0`&\0`?\0`\\\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`*\0`^\0``\0`|\r";

    public const string PortugueseBarcode1 = "\0´d2010251991944283521yCH(4ºh1Ab\010DdVcX;t\017250209";
    public const string PortugueseBarcode2 = "\0´d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string PortugueseBarcode3 = "\0´d2010251991944283521Z/Ur7:0oQS\010)fpNxYi\017250405";
    public const string PortugueseBarcode4 = "\0´d2010251991944283521Ew5ç2-zaMJ\010«8Fn?Pª\017261002";
    public const string PortugueseBarcode5 = "\0´d20102519919442835215j4CltEc'Y\0103kuW=L9\017260304";
    public const string PortugueseBarcode6 = "\0´d201025199194428352151itJzCguA\010Çjª%e»P\017251031";
    public const string PortugueseBarcode7 = "\0´d2010251991944283521t;XcVdD0q!\010bA1hº4(\017251214";
    public const string PortugueseBarcode8 = "\0´d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string PortugueseBarcode9 = "\0´d2010251991944283521)ªP?nF8«9L\010,_vIgTS\017260218";
    public const string PortugueseBarcode10 = "\0´d2010251991944283521=Wuk3P»e%ª\010Qo0:7rU\017260703";
    public const string PortugueseBarcode11 = "\0´d2010251991944283521ªjÇAugCzJt\010/ZJMaz-\017251126";
    public const string PortugueseBarcode12 = "\0´d2010251991944283521HIrQ9QeTyQ\0102ç5wEY'\017250404";

    public const string RomanianLegacyBaseline =
        "îd1  ! Ţ % / ţ ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ş ş ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ă â î & ] Ă Â Î [    \u001d    \u001c    \0        \r";

    public const string RomanianLegacyBarcode1 = "îd2010251991944283521zCH(4ţh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string RomanianLegacyBarcode2 = "îd2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string RomanianLegacyBarcode3 = "îd2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string RomanianLegacyBarcode4 = "îd2010251991944283521Ew5ş2-yaMJ\u001d10'8Fn?PŢ\u001d17261002";
    public const string RomanianLegacyBarcode5 = "îd20102519919442835215j4CltEc+Z\u001d103kuW=L9\u001d17260304";
    public const string RomanianLegacyBarcode6 = "îd201025199194428352151itJyCguA\u001d10ŞjŢ%e*P\u001d17251031";
    public const string RomanianLegacyBarcode7 = "îd2010251991944283521t;XcVdD0q!\u001d10bA1hţ4(\u001d17251214";
    public const string RomanianLegacyBarcode8 = "îd2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string RomanianLegacyBarcode9 = "îd2010251991944283521)ŢP?nF8'9L\u001d10,_vIgTS\u001d17260218";
    public const string RomanianLegacyBarcode10 = "îd2010251991944283521=Wuk3P*e%Ţ\u001d10Qo0:7rU\u001d17260703";
    public const string RomanianLegacyBarcode11 = "îd2010251991944283521ŢjŞAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string RomanianLegacyBarcode12 = "îd2010251991944283521HIrQ9QeTzQ\u001d102ş5wEZ+\u001d17250404";

    public const string RomanianProgrammersBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \0    \0    \0        \r";

    public const string RomanianProgrammersBarcode1 = "]d2010251991944283521yCH*4'h1Ab\010DdVcX<t\017250209";
    public const string RomanianProgrammersBarcode2 = "]d2010251991944283521TgIv?,6BmK\010.GRs!qO\017250729";
    public const string RomanianProgrammersBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\010(fpNxYi\017250405";
    public const string RomanianProgrammersBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\010=8Fn_P\"\017261002";
    public const string RomanianProgrammersBarcode5 = "]d20102519919442835215j4CltEc-Y\0103kuW)L9\017260304";
    public const string RomanianProgrammersBarcode6 = "]d201025199194428352151itJzCguA\010:j\"%e+P\017251031";
    public const string RomanianProgrammersBarcode7 = "]d2010251991944283521t<XcVdD0q!\010bA1h'4*\017251214";
    public const string RomanianProgrammersBarcode8 = "]d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string RomanianProgrammersBarcode9 = "]d2010251991944283521(\"P_nF8=9L\010,?vIgTS\017260218";
    public const string RomanianProgrammersBarcode10 = "]d2010251991944283521)Wuk3P+e%\"\010Qo0>7rU\017260703";
    public const string RomanianProgrammersBarcode11 = "]d2010251991944283521\"j:AugCzJt\010&ZJMaz/\017251126";
    public const string RomanianProgrammersBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\0102;5wEY-\017250404";

    public const string RomanianStandardBaseline =
        "îd1  ! Ț % & ț ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 Ș ș ; = : ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ ă â î ^ „ Ă Â Î ”    \0    \0    \0        \r";

    public const string RomanianStandardBarcode1 = "îd2010251991944283521yCH*4țh1Ab\010DdVcX;t\017250209";
    public const string RomanianStandardBarcode2 = "îd2010251991944283521TgIv?,6BmK\010.GRs!qO\017250729";
    public const string RomanianStandardBarcode3 = "îd2010251991944283521Z&Ur7:0oQS\010(fpNxYi\017250405";
    public const string RomanianStandardBarcode4 = "îd2010251991944283521Ew5ș2/zaMJ\010=8Fn_PȚ\017261002";
    public const string RomanianStandardBarcode5 = "îd20102519919442835215j4CltEc-Y\0103kuW)L9\017260304";
    public const string RomanianStandardBarcode6 = "îd201025199194428352151itJzCguA\010ȘjȚ%e+P\017251031";
    public const string RomanianStandardBarcode7 = "îd2010251991944283521t;XcVdD0q!\010bA1hț4*\017251214";
    public const string RomanianStandardBarcode8 = "îd2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string RomanianStandardBarcode9 = "îd2010251991944283521(ȚP_nF8=9L\010,?vIgTS\017260218";
    public const string RomanianStandardBarcode10 = "îd2010251991944283521)Wuk3P+e%Ț\010Qo0:7rU\017260703";
    public const string RomanianStandardBarcode11 = "îd2010251991944283521ȚjȘAugCzJt\010&ZJMaz/\017251126";
    public const string RomanianStandardBarcode12 = "îd2010251991944283521HIrQ9QeTyQ\0102ș5wEY-\017250404";

    public const string SamiExtendedFinlandSwedenBaseline =
        "ŋd1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Á R S T U V Š Č Ŧ Z ? a b c d e f g h i j k l m n o p á r s t u v š č ŧ z   # ¤ \" å đ ŋ & § Å Đ Ŋ ½    \0    \0    \0        \r";

    public const string SamiExtendedFinlandSwedenDeadkeyBarcode1 =
        "ŋd1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Á\0`R\0`S\0`T\0Ù\0`V\0`Š\0`Č\0`Ŧ\0`Z\0`å\0`đ\0`ŋ\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`á\0`r\0`s\0`t\0ù\0`v\0`š\0`č\0`ŧ\0`z\0`Å\0`Đ\0`Ŋ\0`½\r";

    public const string SamiExtendedFinlandSwedenDeadkeyBarcode2 =
        "ŋd1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Á\0Ŕ\0Ś\0´T\0Ú\0´V\0´Š\0´Č\0´Ŧ\0Ź\0ǻ\0´đ\0´ŋ\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´á\0ŕ\0ś\0´t\0ú\0´v\0´š\0´č\0´ŧ\0ź\0Ǻ\0´Đ\0´Ŋ\0´½\r";

    public const string SamiExtendedFinlandSwedenBarcode1 = "ŋd2010251991944283521ŧCH(4äh1Ab\010DdVcČ;t\017250209";
    public const string SamiExtendedFinlandSwedenBarcode2 = "ŋd2010251991944283521TgIv_,6BmK\010.GRs!áO\017250729";
    public const string SamiExtendedFinlandSwedenBarcode3 = "ŋd2010251991944283521Z/Ur7:0oÁS\010)fpNčŦi\017250405";

    public const string SamiExtendedFinlandSwedenBarcode4 =
        "ŋd2010251991944283521Eš5ö2-zaMJ\010\0´8Fn?PÄ\017261002";

    public const string SamiExtendedFinlandSwedenBarcode5 = "ŋd20102519919442835215j4CltEc+Ŧ\0103kuŠ=L9\017260304";

    public const string SamiExtendedFinlandSwedenBarcode6 =
        "ŋd201025199194428352151itJzCguA\010ÖjÄ%e\0`P\017251031";

    public const string SamiExtendedFinlandSwedenBarcode7 = "ŋd2010251991944283521t;ČcVdD0á!\010bA1hä4(\017251214";
    public const string SamiExtendedFinlandSwedenBarcode8 = "ŋd2010251991944283521sRG.iŦčNpf\010HCŧKmB6\017260620";

    public const string SamiExtendedFinlandSwedenBarcode9 =
        "ŋd2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";

    public const string SamiExtendedFinlandSwedenBarcode10 =
        "ŋd2010251991944283521=Šuk3P\0è%Ä\010Áo0:7rU\017260703";

    public const string SamiExtendedFinlandSwedenBarcode11 =
        "ŋd2010251991944283521ÄjÖAugCzJt\010/ZJMaz-\017251126";

    public const string SamiExtendedFinlandSwedenBarcode12 =
        "ŋd2010251991944283521HIrÁ9ÁeTŧÁ\0102ö5šEŦ+\017250404";

    public const string SamiExtendedNorwayBaseline =
        "ŋd1  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Á R S T U V Š Č Ŧ Z ? a b c d e f g h i j k l m n o p á r s t u v š č ŧ z   # ¤ \" å đ ŋ & | Å Đ Ŋ §    \0    \0    \0        \r";

    public const string SamiExtendedNorwayDeadkeyBarcode1 =
        "ŋd1\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Á\0`R\0`S\0`T\0Ù\0`V\0`Š\0`Č\0`Ŧ\0`Z\0`å\0`đ\0`ŋ\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`á\0`r\0`s\0`t\0ù\0`v\0`š\0`č\0`ŧ\0`z\0`Å\0`Đ\0`Ŋ\0`§\r";

    public const string SamiExtendedNorwayBarcode1 = "ŋd2010251991944283521ŧCH(4æh1Ab\010DdVcČ;t\017250209";
    public const string SamiExtendedNorwayBarcode2 = "ŋd2010251991944283521TgIv_,6BmK\010.GRs!áO\017250729";
    public const string SamiExtendedNorwayBarcode3 = "ŋd2010251991944283521Z/Ur7:0oÁS\010)fpNčŦi\017250405";
    public const string SamiExtendedNorwayBarcode4 = "ŋd2010251991944283521Eš5ø2-zaMJ\010\\8Fn?PÆ\017261002";
    public const string SamiExtendedNorwayBarcode5 = "ŋd20102519919442835215j4CltEc+Ŧ\0103kuŠ=L9\017260304";
    public const string SamiExtendedNorwayBarcode6 = "ŋd201025199194428352151itJzCguA\010ØjÆ%e\0`P\017251031";
    public const string SamiExtendedNorwayBarcode7 = "ŋd2010251991944283521t;ČcVdD0á!\010bA1hæ4(\017251214";
    public const string SamiExtendedNorwayBarcode8 = "ŋd2010251991944283521sRG.iŦčNpf\010HCŧKmB6\017260620";
    public const string SamiExtendedNorwayBarcode9 = "ŋd2010251991944283521)ÆP?nF8\\9L\010,_vIgTS\017260218";
    public const string SamiExtendedNorwayBarcode10 = "ŋd2010251991944283521=Šuk3P\0è%Æ\010Áo0:7rU\017260703";
    public const string SamiExtendedNorwayBarcode11 = "ŋd2010251991944283521ÆjØAugCzJt\010/ZJMaz-\017251126";
    public const string SamiExtendedNorwayBarcode12 = "ŋd2010251991944283521HIrÁ9ÁeTŧÁ\0102ø5šEŦ+\017250404";

    public const string SlovakBaseline =
        "äd+  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú ň ä 6 ; / ) ( \0°   \0    \u001c    \u001e    \0    \r";

    public const string SlovakDeadkeyBarcode1 =
        "äd+\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ)\0ˇ(\0ˇ°\r";

    public const string SlovakQwertyBaseline =
        "äd+  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú ň ä 6 ; / ) ( \0°   \u001b    \u001c    \u001e        \r";

    public const string SlovakQwertyDeadkeyBarcode1 =
        "äd+\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ)\0ˇ(\0ˇ°\r";

    public const string SlovakQwertyDeadkeyBarcode2 =
        "äd+\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ľ\0´š\0´č\0´ť\0´ž\0´ý\0´á\0´í\0´\"\0´ô\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´ň\0´ä\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´)\0´(\0´°\r";

    public const string SlovakQwertyDeadkeyBarcode3 =
        "äd+\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ľ\0°š\0°č\0°ť\0°ž\0°ý\0°á\0°í\0°\"\0°ô\0°?\0°´\0°:\0°_\0°2\0°A\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°ň\0°ä\0°6\0°%\0°;\0°a\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°)\0°(\0°°\r";

    public const string SlovakQwertyBarcode1 = "ädľé+éľť+íí+íččľášťľ+yCH8č§h+Ab\u001b+éDdVcX?t\u001b+ýľťéľéí";
    public const string SlovakQwertyBarcode2 = "ädľé+éľť+íí+íččľášťľ+TgIv_,žBmK\u001b+é.GRs1qO\u001b+ýľťéýľí";
    public const string SlovakQwertyBarcode3 = "ädľé+éľť+íí+íččľášťľ+Z7Urý:éoQS\u001b+é9fpNxYi\u001b+ýľťéčéť";
    public const string SlovakQwertyBarcode4 = "ädľé+éľť+íí+íččľášťľ+Ewťôľ-zaMJ\u001b+é\0´áFn%P!\u001b+ýľž+ééľ";
    public const string SlovakQwertyBarcode5 = "ädľé+éľť+íí+íččľášťľ+ťjčCltEc=Y\u001b+éškuW0Lí\u001b+ýľžéšéč";
    public const string SlovakQwertyBarcode6 = "ädľé+éľť+íí+íččľášťľ+ť+itJzCguA\u001b+é\"j!5e\0ˇP\u001b+ýľť+éš+";
    public const string SlovakQwertyBarcode7 = "ädľé+éľť+íí+íččľášťľ+t?XcVdDéq1\u001b+ébA+h§č8\u001b+ýľť+ľ+č";
    public const string SlovakQwertyBarcode8 = "ädľé+éľť+íí+íččľášťľ+sRG.iYxNpf\u001b+éHCyKmBž\u001b+ýľžéžľé";
    public const string SlovakQwertyBarcode9 = "ädľé+éľť+íí+íččľášťľ+9!P%nFá\0´íL\u001b+é,_vIgTS\u001b+ýľžéľ+á";
    public const string SlovakQwertyBarcode10 = "ädľé+éľť+íí+íččľášťľ+0WukšP\0ě5!\u001b+éQoé:ýrU\u001b+ýľžéýéš";
    public const string SlovakQwertyBarcode11 = "ädľé+éľť+íí+íččľášťľ+!j\"AugCzJt\u001b+é7ZJMaz-\u001b+ýľť++ľž";
    public const string SlovakQwertyBarcode12 = "ädľé+éľť+íí+íččľášťľ+HIrQíQeTyQ\u001b+éľôťwEY=\u001b+ýľťéčéč";

    public const string SlovenianBaseline =
        "đd1  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \u001b    \u001c    \u001e    \u001f    \r";

    public const string SlovenianDeadkeyBarcode1 =
        "đd1\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\r";

    public const string SlovenianDeadkeyBarcode2 =
        "đd1\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\r";

    public const string SlovenianBarcode1 = "đd2010251991944283521zCH(4ćh1Ab\u001b10DdVcX;t\u001b17250209";
    public const string SlovenianBarcode2 = "đd2010251991944283521TgIv_,6BmK\u001b10.GRs!qO\u001b17250729";
    public const string SlovenianBarcode3 = "đd2010251991944283521Y/Ur7:0oQS\u001b10)fpNxZi\u001b17250405";
    public const string SlovenianBarcode4 = "đd2010251991944283521Ew5č2-yaMJ\u001b10+8Fn?PĆ\u001b17261002";
    public const string SlovenianBarcode5 = "đd20102519919442835215j4CltEc'Z\u001b103kuW=L9\u001b17260304";
    public const string SlovenianBarcode6 = "đd201025199194428352151itJyCguA\u001b10ČjĆ%e*P\u001b17251031";
    public const string SlovenianBarcode7 = "đd2010251991944283521t;XcVdD0q!\u001b10bA1hć4(\u001b17251214";
    public const string SlovenianBarcode8 = "đd2010251991944283521sRG.iZxNpf\u001b10HCzKmB6\u001b17260620";
    public const string SlovenianBarcode9 = "đd2010251991944283521)ĆP?nF8+9L\u001b10,_vIgTS\u001b17260218";
    public const string SlovenianBarcode10 = "đd2010251991944283521=Wuk3P*e%Ć\u001b10Qo0:7rU\u001b17260703";
    public const string SlovenianBarcode11 = "đd2010251991944283521ĆjČAugCyJt\u001b10/YJMay-\u001b17251126";
    public const string SlovenianBarcode12 = "đd2010251991944283521HIrQ9QeTzQ\u001b102č5wEZ'\u001b17250404";

    public const string SorbianExtendedBaseline =
        "+d1  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0ˇÜ Ł * \0˙   \0    \0    \0    \0    \r";

    public const string SorbianExtendedDeadkeyBarcode1 =
        "+d1\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0Ą\0`B\0Ç\0`D\0Ę\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ő\0`P\0`Q\0`R\0Ş\0`T\0Ű\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0ą\0`b\0ç\0`d\0ę\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ő\0`p\0`q\0`r\0ş\0`t\0ű\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\r";

    public const string SorbianExtendedDeadkeyBarcode2 =
        "+d1\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\r";

    public const string SorbianExtendedDeadkeyBarcode3 =
        "+d1\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0Ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\r";

    public const string SorbianExtendedDeadkeyBarcode4 =
        "+d1\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\r";

    public const string SorbianExtendedBarcode1 = "+d2010251991944283521zCH(4äh1Ab\010DdVcX;t\017250209";
    public const string SorbianExtendedBarcode2 = "+d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string SorbianExtendedBarcode3 = "+d2010251991944283521Y/Ur7:0oQS\010)fpNxZi\017250405";
    public const string SorbianExtendedBarcode4 = "+d2010251991944283521Ew5ö2-yaMJ\010\0´8Fn?PÄ\017261002";
    public const string SorbianExtendedBarcode5 = "+d20102519919442835215j4CltEcßZ\0103kuW=L9\017260304";
    public const string SorbianExtendedBarcode6 = "+d201025199194428352151itJyCguA\010ÖjÄ%e\0`P\017251031";
    public const string SorbianExtendedBarcode7 = "+d2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string SorbianExtendedBarcode8 = "+d2010251991944283521sRG.iZxNpf\010HCzKmB6\017260620";
    public const string SorbianExtendedBarcode9 = "+d2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string SorbianExtendedBarcode10 = "+d2010251991944283521=Wuk3P\0ę%Ä\010Qo0:7rU\017260703";
    public const string SorbianExtendedBarcode11 = "+d2010251991944283521ÄjÖAugCyJt\010/YJMay-\017251126";
    public const string SorbianExtendedBarcode12 = "+d2010251991944283521HIrQ9QeTzQ\0102ö5wEZß\017250404";

    public const string SorbianStandardBaseline =
        "+d1  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \0    \0    \0    \0    \r";

    public const string SorbianStandardDeadkeyBarcode1 =
        "+d1\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\r";

    public const string SorbianStandardDeadkeyBarcode2 =
        "+d1\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0´E\0´F\0´G\0´H\0´I\0´J\0´K\0Ł\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0´U\0´V\0´W\0´X\0Ź\0´Y\0´ü\0´#\0´+\0´&\0´?\0´^\0´a\0´b\0ć\0´d\0´e\0´f\0´g\0´h\0´i\0´j\0´k\0ł\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0´u\0´v\0´w\0´x\0ź\0´y\0´Ü\0´'\0´*\0´°\r";

    public const string SorbianStandardDeadkeyBarcode3 =
        "+d1\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0^D\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0^L\0^M\0^N\0^O\0^P\0^Q\0Ř\0Š\0^T\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0^a\0^b\0č\0^d\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0^l\0^m\0^n\0^o\0^p\0^q\0ř\0š\0^t\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^'\0^*\0^°\r";

    public const string SorbianStandardBarcode1 = "+d2010251991944283521zCH(4äh1Ab\010DdVcX;t\017250209";
    public const string SorbianStandardBarcode2 = "+d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string SorbianStandardBarcode3 = "+d2010251991944283521Y/Ur7:0oQS\010)fpNxZi\017250405";
    public const string SorbianStandardBarcode4 = "+d2010251991944283521Ew5ö2-yaMJ\010\0´8Fn?PÄ\017261002";
    public const string SorbianStandardBarcode5 = "+d20102519919442835215j4CltEcßZ\0103kuW=L9\017260304";
    public const string SorbianStandardBarcode6 = "+d201025199194428352151itJyCguA\010ÖjÄ%e\0`P\017251031";
    public const string SorbianStandardBarcode7 = "+d2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string SorbianStandardBarcode8 = "+d2010251991944283521sRG.iZxNpf\010HCzKmB6\017260620";
    public const string SorbianStandardBarcode9 = "+d2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string SorbianStandardBarcode10 = "+d2010251991944283521=Wuk3P\0è%Ä\010Qo0:7rU\017260703";
    public const string SorbianStandardBarcode11 = "+d2010251991944283521ÄjÖAugCyJt\010/YJMay-\017251126";
    public const string SorbianStandardBarcode12 = "+d2010251991944283521HIrQ9QeTzQ\0102ö5wEZß\017250404";

    public const string SorbianStandardLegacyBaseline =
        "+d1  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0^Ü Ł * \0̇   \0    \0    \0    \0    \r";

    public const string SorbianStandardLegacyDeadkeyBarcode1 =
        "+d1\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0Ç\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0Ş\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0à\0`b\0ç\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0ş\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\r";

    public const string SorbianStandardLegacyDeadkeyBarcode2 =
        "+d1\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\r";

    public const string SorbianStandardLegacyDeadkeyBarcode3 =
        "+d1\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0Ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\r";

    public const string SorbianStandardLegacyDeadkeyBarcode4 =
        "+d1\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\r";

    public const string SorbianStandardLegacyBarcode1 = "+d2010251991944283521zCH(4äh1Ab\010DdVcX;t\017250209";
    public const string SorbianStandardLegacyBarcode2 = "+d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string SorbianStandardLegacyBarcode3 = "+d2010251991944283521Y/Ur7:0oQS\010)fpNxZi\017250405";
    public const string SorbianStandardLegacyBarcode4 = "+d2010251991944283521Ew5ö2-yaMJ\010\0´8Fn?PÄ\017261002";
    public const string SorbianStandardLegacyBarcode5 = "+d20102519919442835215j4CltEcßZ\0103kuW=L9\017260304";
    public const string SorbianStandardLegacyBarcode6 = "+d201025199194428352151itJyCguA\010ÖjÄ%e\0`P\017251031";
    public const string SorbianStandardLegacyBarcode7 = "+d2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string SorbianStandardLegacyBarcode8 = "+d2010251991944283521sRG.iZxNpf\010HCzKmB6\017260620";
    public const string SorbianStandardLegacyBarcode9 = "+d2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string SorbianStandardLegacyBarcode10 = "+d2010251991944283521=Wuk3P\0è%Ä\010Qo0:7rU\017260703";
    public const string SorbianStandardLegacyBarcode11 = "+d2010251991944283521ÄjÖAugCyJt\010/YJMay-\017251126";
    public const string SorbianStandardLegacyBarcode12 = "+d2010251991944283521HIrQ9QeTzQ\0102ö5wEZß\017250404";

    public const string SpanishBaseline =
        "+d1  ! \0¨% / \0´) = ( ¿ , ' . - 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; ¡ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   · $ \" \0`ç + & º \0^Ç * ª    \u001d    \u001c    \u001e    \0    \r";

    public const string SpanishDeadkeyBarcode1 =
        "+d1\0¨!\0¨¨\0¨·\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨¿\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ñ\0¨ñ\0¨;\0¨¡\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨`\0¨ç\0¨+\0¨&\0¨?\0¨º\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨Ç\0¨*\0¨ª\r";

    public const string SpanishDeadkeyBarcode2 =
        "+d1\0´!\0´¨\0´·\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´¿\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ñ\0´ñ\0´;\0´¡\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´`\0´ç\0´+\0´&\0´?\0´º\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´Ç\0´*\0´ª\r";

    public const string SpanishDeadkeyBarcode3 =
        "+d1\0`!\0`¨\0`·\0`$\0`%\0`/\0`´\0`)\0`=\0`(\0`¿\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ñ\0`ñ\0`;\0`¡\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0``\0`ç\0`+\0`&\0`?\0`º\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`Ç\0`*\0`ª\r";

    public const string SpanishDeadkeyBarcode4 =
        "+d1\0^!\0^¨\0^·\0^$\0^%\0^/\0^´\0^)\0^=\0^(\0^¿\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ñ\0^ñ\0^;\0^¡\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^`\0^ç\0^+\0^&\0^?\0^º\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^Ç\0^*\0^ª\r";

    public const string SpanishBarcode1 = "+d2010251991944283521yCH(4\0´h1Ab\u001d10DdVcX;t\u001d17250209";
    public const string SpanishBarcode2 = "+d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string SpanishBarcode3 = "+d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string SpanishBarcode4 = "+d2010251991944283521Ew5ñ2-zaMJ\u001d10¡8Fn?P\0¨\u001d17261002";
    public const string SpanishBarcode5 = "+d20102519919442835215j4CltEc'Y\u001d103kuW=L9\u001d17260304";
    public const string SpanishBarcode6 = "+d201025199194428352151itJzCguA\u001d10Ñj\0¨%e¿P\u001d17251031";
    public const string SpanishBarcode7 = "+d2010251991944283521t;XcVdD0q!\u001d10bA1h\0´4(\u001d17251214";
    public const string SpanishBarcode8 = "+d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string SpanishBarcode9 = "+d2010251991944283521)\0¨P?nF8¡9L\u001d10,_vIgTS\u001d17260218";
    public const string SpanishBarcode10 = "+d2010251991944283521=Wuk3P¿e%\0¨\u001d10Qo0:7rU\u001d17260703";
    public const string SpanishBarcode11 = "+d2010251991944283521\0¨jÑAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string SpanishBarcode12 = "+d2010251991944283521HIrQ9QeTyQ\u001d102ñ5wEY'\u001d17250404";

    public const string SpanishVariationBaseline =
        "\0`d1  ª Ç ) ! ç ? ₧ ¿ \0¨, - . = 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; \0¨: % A B C D E F G H I J K L M N O P Q R S T U V W X Y Z + a b c d e f g h i j k l m n o p q r s t u v w x y z   / ( \" ÷ \0´\0`¡ ' × \0´\0`·    \u001d    \u001c    \u001e        \r";

    public const string StandardBaseline =
        "đd1  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \u001b    \u001c    \u001e    \u001f    \r";

    public const string StandardDeadkeyBarcode1 =
        "đd1\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\r";

    public const string StandardDeadkeyBarcode2 =
        "đd1\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\r";

    public const string StandardBarcode1 = "đd2010251991944283521zCH(4ćh1Ab\u001b10DdVcX;t\u001b17250209";
    public const string StandardBarcode2 = "đd2010251991944283521TgIv_,6BmK\u001b10.GRs!qO\u001b17250729";
    public const string StandardBarcode3 = "đd2010251991944283521Y/Ur7:0oQS\u001b10)fpNxZi\u001b17250405";
    public const string StandardBarcode4 = "đd2010251991944283521Ew5č2-yaMJ\u001b10+8Fn?PĆ\u001b17261002";
    public const string StandardBarcode5 = "đd20102519919442835215j4CltEc'Z\u001b103kuW=L9\u001b17260304";
    public const string StandardBarcode6 = "đd201025199194428352151itJyCguA\u001b10ČjĆ%e*P\u001b17251031";
    public const string StandardBarcode7 = "đd2010251991944283521t;XcVdD0q!\u001b10bA1hć4(\u001b17251214";
    public const string StandardBarcode8 = "đd2010251991944283521sRG.iZxNpf\u001b10HCzKmB6\u001b17260620";
    public const string StandardBarcode9 = "đd2010251991944283521)ĆP?nF8+9L\u001b10,_vIgTS\u001b17260218";
    public const string StandardBarcode10 = "đd2010251991944283521=Wuk3P*e%Ć\u001b10Qo0:7rU\u001b17260703";
    public const string StandardBarcode11 = "đd2010251991944283521ĆjČAugCyJt\u001b10/YJMay-\u001b17251126";
    public const string StandardBarcode12 = "đd2010251991944283521HIrQ9QeTzQ\u001b102č5wEZ'\u001b17250404";

    public const string SwedishBaseline =
        "\0¨d1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \u001d    \0    \u001e        \r";

    public const string SwedishDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½\r";

    public const string SwedishDeadkeyBarcode2 =
        "\0¨d1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\r";

    public const string SwedishDeadkeyBarcode3 =
        "\0¨d1\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\r";

    public const string SwedishDeadkeyBarcode4 =
        "\0¨d1\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\r";

    public const string SwedishBarcode1 = "\0¨d2010251991944283521yCH(4äh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string SwedishBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string SwedishBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string SwedishBarcode4 = "\0¨d2010251991944283521Ew5ö2-zaMJ\u001d10\0´8Fn?PÄ\u001d17261002";
    public const string SwedishBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\u001d103kuW=L9\u001d17260304";
    public const string SwedishBarcode6 = "\0¨d201025199194428352151itJzCguA\u001d10ÖjÄ%e\0`P\u001d17251031";
    public const string SwedishBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\u001d10bA1hä4(\u001d17251214";
    public const string SwedishBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string SwedishBarcode9 = "\0¨d2010251991944283521)ÄP?nF8\0´9L\u001d10,_vIgTS\u001d17260218";
    public const string SwedishBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Ä\u001d10Qo0:7rU\u001d17260703";
    public const string SwedishBarcode11 = "\0¨d2010251991944283521ÄjÖAugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string SwedishBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\u001d102ö5wEY+\u001d17250404";

    public const string SwedishWithSamiBaseline =
        "\0¨d1  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0        \r";

    public const string SwedishWithSamiDeadkeyBarcode1 =
        "\0¨d1\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\r";

    public const string SwedishWithSamiDeadkeyBarcode2 =
        "\0¨d1\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\r";

    public const string SwedishWithSamiDeadkeyBarcode3 =
        "\0¨d1\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\r";

    public const string SwedishWithSamiDeadkeyBarcode4 =
        "\0¨d1\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\r";

    public const string SwedishWithSamiBarcode1 = "\0¨d2010251991944283521yCH(4äh1Ab\010DdVcX;t\017250209";
    public const string SwedishWithSamiBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\010.GRs!qO\017250729";
    public const string SwedishWithSamiBarcode3 = "\0¨d2010251991944283521Z/Ur7:0oQS\010)fpNxYi\017250405";
    public const string SwedishWithSamiBarcode4 = "\0¨d2010251991944283521Ew5ö2-zaMJ\010\0´8Fn?PÄ\017261002";
    public const string SwedishWithSamiBarcode5 = "\0¨d20102519919442835215j4CltEc+Y\0103kuW=L9\017260304";
    public const string SwedishWithSamiBarcode6 = "\0¨d201025199194428352151itJzCguA\010ÖjÄ%e\0`P\017251031";
    public const string SwedishWithSamiBarcode7 = "\0¨d2010251991944283521t;XcVdD0q!\010bA1hä4(\017251214";
    public const string SwedishWithSamiBarcode8 = "\0¨d2010251991944283521sRG.iYxNpf\010HCyKmB6\017260620";
    public const string SwedishWithSamiBarcode9 = "\0¨d2010251991944283521)ÄP?nF8\0´9L\010,_vIgTS\017260218";
    public const string SwedishWithSamiBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%Ä\010Qo0:7rU\017260703";
    public const string SwedishWithSamiBarcode11 = "\0¨d2010251991944283521ÄjÖAugCzJt\010/ZJMaz-\017251126";
    public const string SwedishWithSamiBarcode12 = "\0¨d2010251991944283521HIrQ9QeTyQ\0102ö5wEY+\017250404";

    public const string SwissFrenchBaseline =
        "\0¨d1  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \u001d    \u001c    \u001e    \0    \r";

    public const string SwissFrenchDeadkeyBarcode1 =
        "\0¨d1\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\r";

    public const string SwissFrenchDeadkeyBarcode2 =
        "\0¨d1\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\r";

    public const string SwissFrenchDeadkeyBarcode3 =
        "\0¨d1\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\r";

    public const string SwissFrenchBarcode1 = "\0¨d2010251991944283521zCH(4àh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string SwissFrenchBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs+qO\u001d17250729";
    public const string SwissFrenchBarcode3 = "\0¨d2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string SwissFrenchBarcode4 = "\0¨d2010251991944283521Ew5é2-yaMJ\u001d10\0^8Fn?Pä\u001d17261002";
    public const string SwissFrenchBarcode5 = "\0¨d20102519919442835215j4CltEc'Z\u001d103kuW=L9\u001d17260304";
    public const string SwissFrenchBarcode6 = "\0¨d201025199194428352151itJyCguA\u001d10öjä%e\0`P\u001d17251031";
    public const string SwissFrenchBarcode7 = "\0¨d2010251991944283521t;XcVdD0q+\u001d10bA1hà4(\u001d17251214";
    public const string SwissFrenchBarcode8 = "\0¨d2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string SwissFrenchBarcode9 = "\0¨d2010251991944283521)äP?nF8\0^9L\u001d10,_vIgTS\u001d17260218";
    public const string SwissFrenchBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%ä\u001d10Qo0:7rU\u001d17260703";
    public const string SwissFrenchBarcode11 = "\0¨d2010251991944283521äjöAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string SwissFrenchBarcode12 = "\0¨d2010251991944283521HIrQ9QeTzQ\u001d102é5wEZ'\u001d17250404";

    public const string SwissGermanBaseline =
        "\0¨d1  + à % / ä ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 é ö ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" ü $ \0¨& § è £ ! °    \u001d    \u001c    \u001e    \0    \r";

    public const string SwissGermanDeadkeyBarcode1 =
        "\0¨d1\0`+\0`à\0`*\0`ç\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`é\0`ö\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`è\0`£\0`!\0`°\r";

    public const string SwissGermanDeadkeyBarcode2 =
        "\0¨d1\0^+\0^à\0^*\0^ç\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^é\0^ö\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^è\0^£\0^!\0^°\r";

    public const string SwissGermanDeadkeyBarcode3 =
        "\0¨d1\0¨+\0¨à\0¨*\0¨ç\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨é\0¨ö\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨ü\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨è\0¨£\0¨!\0¨°\r";

    public const string SwissGermanBarcode1 = "\0¨d2010251991944283521zCH(4äh1Ab\u001d10DdVcX;t\u001d17250209";
    public const string SwissGermanBarcode2 = "\0¨d2010251991944283521TgIv_,6BmK\u001d10.GRs+qO\u001d17250729";
    public const string SwissGermanBarcode3 = "\0¨d2010251991944283521Y/Ur7:0oQS\u001d10)fpNxZi\u001d17250405";
    public const string SwissGermanBarcode4 = "\0¨d2010251991944283521Ew5ö2-yaMJ\u001d10\0^8Fn?Pà\u001d17261002";
    public const string SwissGermanBarcode5 = "\0¨d20102519919442835215j4CltEc'Z\u001d103kuW=L9\u001d17260304";
    public const string SwissGermanBarcode6 = "\0¨d201025199194428352151itJyCguA\u001d10éjà%e\0`P\u001d17251031";
    public const string SwissGermanBarcode7 = "\0¨d2010251991944283521t;XcVdD0q+\u001d10bA1hä4(\u001d17251214";
    public const string SwissGermanBarcode8 = "\0¨d2010251991944283521sRG.iZxNpf\u001d10HCzKmB6\u001d17260620";
    public const string SwissGermanBarcode9 = "\0¨d2010251991944283521)àP?nF8\0^9L\u001d10,_vIgTS\u001d17260218";
    public const string SwissGermanBarcode10 = "\0¨d2010251991944283521=Wuk3P\0è%à\u001d10Qo0:7rU\u001d17260703";
    public const string SwissGermanBarcode11 = "\0¨d2010251991944283521àjéAugCyJt\u001d10/YJMay-\u001d17251126";
    public const string SwissGermanBarcode12 = "\0¨d2010251991944283521HIrQ9QeTzQ\u001d102ö5wEZ'\u001d17250404";

    public const string BulgarianBaseline =
        ";а1  ! Ч % : ч – № / € р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б ѝ Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю $ ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц „ ; = ( Ц “ § )    \0    \0    \0        \r";

    public const string BulgarianBarcode1 = ";а2010251991944283521щЪГ/4чг1ѝф\010АаЭъЙРш\017250209";
    public const string BulgarianBarcode2 = ";а2010251991944283521ШжСэБр6ФпН\010лЖИя!,Д\017250729";
    public const string BulgarianBarcode3 = ";а2010251991944283521Ю:Ки7Л0дыЯ\010–озХйЩс\017250405";
    public const string BulgarianBarcode4 = ";а2010251991944283521Еу5м2бюьПТ\010.8Ох$ЗЧ\017261002";
    public const string BulgarianBarcode5 = ";а20102519919442835215т4ЪвшЕъ-Щ\0103нкУ№В9\017260304";
    public const string BulgarianBarcode6 = ";а201025199194428352151сшТюЪжкѝ\010МтЧ%е€З\017251031";
    public const string BulgarianBarcode7 = ";а2010251991944283521шРЙъЭаА0,!\010фѝ1гч4/\017251214";
    public const string BulgarianBarcode8 = ";а2010251991944283521яИЖлсЩйХзо\010ГЪщНпФ6\017260620";
    public const string BulgarianBarcode9 = ";а2010251991944283521–ЧЗ$хО8.9В\010рБэСжШЯ\017260218";
    public const string BulgarianBarcode10 = ";а2010251991944283521№Укн3З€е%Ч\010ыд0Л7иК\017260703";
    public const string BulgarianBarcode11 = ";а2010251991944283521ЧтМѝкжЪюТш\010:ЮТПьюб\017251126";
    public const string BulgarianBarcode12 = ";а2010251991944283521ГСиы9ыеШщы\0102м5уЕЩ-\017250404";

    public const string BulgarianLatinBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \u001d    \u001c    \u001e        \r";

    public const string BulgarianLatinBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string BulgarianLatinBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string BulgarianLatinBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string BulgarianLatinBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P\"\u001d17261002";
    public const string BulgarianLatinBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string BulgarianLatinBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j\"%e+P\u001d17251031";
    public const string BulgarianLatinBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string BulgarianLatinBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string BulgarianLatinBarcode9 = "]d2010251991944283521(\"P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string BulgarianLatinBarcode10 = "]d2010251991944283521)Wuk3P+e%\"\u001d10Qo0>7rU\u001d17260703";
    public const string BulgarianLatinBarcode11 = "]d2010251991944283521\"j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string BulgarianLatinBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string BulgarianPhoneticTraditionalBaseline =
        "щд1  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Я Р С Т У Ж В ѝ Ъ З _ а б ц д е ф г х и й к л м н о п я р с т у ж в ь ъ з   № $ @ ш ю щ € ч Ш Ю Щ Ч    \0    \0    \0        \r";

    public const string BulgarianPhoneticTraditionalBarcode1 =
        "щд2010251991944283521ъЦХ*4'х1Аб\010ДдЖцѝ<т\017250209";

    public const string BulgarianPhoneticTraditionalBarcode2 =
        "щд2010251991944283521ТгИж?,6БмК\010.ГРс!яО\017250729";

    public const string BulgarianPhoneticTraditionalBarcode3 =
        "щд2010251991944283521З§Ур7>0оЯС\010(фпНьЪи\017250405";

    public const string BulgarianPhoneticTraditionalBarcode4 =
        "щд2010251991944283521Ев5;2/заМЙ\010=8Фн_П\"\017261002";

    public const string BulgarianPhoneticTraditionalBarcode5 =
        "щд20102519919442835215й4ЦлтЕц-Ъ\0103куВ)Л9\017260304";

    public const string BulgarianPhoneticTraditionalBarcode6 =
        "щд201025199194428352151итЙзЦгуА\010:й\"%е+П\017251031";

    public const string BulgarianPhoneticTraditionalBarcode7 =
        "щд2010251991944283521т<ѝцЖдД0я!\010бА1х'4*\017251214";

    public const string BulgarianPhoneticTraditionalBarcode8 =
        "щд2010251991944283521сРГ.иЪьНпф\010ХЦъКмБ6\017260620";

    public const string BulgarianPhoneticTraditionalBarcode9 =
        "щд2010251991944283521(\"П_нФ8=9Л\010,?жИгТС\017260218";

    public const string BulgarianPhoneticTraditionalBarcode10 =
        "щд2010251991944283521)Вук3П+е%\"\010Яо0>7рУ\017260703";

    public const string BulgarianPhoneticTraditionalBarcode11 =
        "щд2010251991944283521\"й:АугЦзЙт\010§ЗЙМаз/\017251126";

    public const string BulgarianPhoneticTraditionalBarcode12 =
        "щд2010251991944283521ХИрЯ9ЯеТъЯ\0102;5вЕЪ-\017250404";

    public const string BulgarianPhoneticBaseline =
        "щд1  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; „ = “ ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Ч Р С Т У В Ш Ж Ъ З – а б ц д е ф г х и й к л м н о п ч р с т у в ш ж ъ з   № $ @ я ь щ € ю Я ѝ Щ Ю    \0    \0    \0    \0    \r";

    public const string BulgarianPhoneticBarcode1 = "щд2010251991944283521ъЦХ*4'х1Аб\010ДдВцЖ„т\017250209";
    public const string BulgarianPhoneticBarcode2 = "щд2010251991944283521ТгИв?,6БмК\010.ГРс!чО\017250729";
    public const string BulgarianPhoneticBarcode3 = "щд2010251991944283521З§Ур7“0оЧС\010(фпНжЪи\017250405";
    public const string BulgarianPhoneticBarcode4 = "щд2010251991944283521Еш5;2/заМЙ\010=8Фн–П\"\017261002";
    public const string BulgarianPhoneticBarcode5 = "щд20102519919442835215й4ЦлтЕц-Ъ\0103куШ)Л9\017260304";
    public const string BulgarianPhoneticBarcode6 = "щд201025199194428352151итЙзЦгуА\010:й\"%е+П\017251031";
    public const string BulgarianPhoneticBarcode7 = "щд2010251991944283521т„ЖцВдД0ч!\010бА1х'4*\017251214";
    public const string BulgarianPhoneticBarcode8 = "щд2010251991944283521сРГ.иЪжНпф\010ХЦъКмБ6\017260620";
    public const string BulgarianPhoneticBarcode9 = "щд2010251991944283521(\"П–нФ8=9Л\010,?вИгТС\017260218";
    public const string BulgarianPhoneticBarcode10 = "щд2010251991944283521)Шук3П+е%\"\010Чо0“7рУ\017260703";
    public const string BulgarianPhoneticBarcode11 = "щд2010251991944283521\"й:АугЦзЙт\010§ЗЙМаз/\017251126";
    public const string BulgarianPhoneticBarcode12 = "щд2010251991944283521ХИрЧ9ЧеТъЧ\0102;5шЕЪ-\017250404";

    public const string BulgarianTypewriterBaseline =
        ";а1  ! Ч % : ч _ № / V р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б Ь Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю І ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц ( ; = ` Ц ) § ~    \u001d    \u001c    \u001e        \r";

    public const string BulgarianTypewriterBarcode1 =
        ";а2010251991944283521щЪГ/4чг1Ьф\u001d10АаЭъЙРш\u001d17250209";

    public const string BulgarianTypewriterBarcode2 =
        ";а2010251991944283521ШжСэБр6ФпН\u001d10лЖИя!,Д\u001d17250729";

    public const string BulgarianTypewriterBarcode3 =
        ";а2010251991944283521Ю:Ки7Л0дыЯ\u001d10_озХйЩс\u001d17250405";

    public const string BulgarianTypewriterBarcode4 =
        ";а2010251991944283521Еу5м2бюьПТ\u001d10.8ОхІЗЧ\u001d17261002";

    public const string BulgarianTypewriterBarcode5 =
        ";а20102519919442835215т4ЪвшЕъ-Щ\u001d103нкУ№В9\u001d17260304";

    public const string BulgarianTypewriterBarcode6 =
        ";а201025199194428352151сшТюЪжкЬ\u001d10МтЧ%еVЗ\u001d17251031";

    public const string BulgarianTypewriterBarcode7 =
        ";а2010251991944283521шРЙъЭаА0,!\u001d10фЬ1гч4/\u001d17251214";

    public const string BulgarianTypewriterBarcode8 =
        ";а2010251991944283521яИЖлсЩйХзо\u001d10ГЪщНпФ6\u001d17260620";

    public const string BulgarianTypewriterBarcode9 =
        ";а2010251991944283521_ЧЗІхО8.9В\u001d10рБэСжШЯ\u001d17260218";

    public const string BulgarianTypewriterBarcode10 =
        ";а2010251991944283521№Укн3ЗVе%Ч\u001d10ыд0Л7иК\u001d17260703";

    public const string BulgarianTypewriterBarcode11 =
        ";а2010251991944283521ЧтМЬкжЪюТш\u001d10:ЮТПьюб\u001d17251126";

    public const string BulgarianTypewriterBarcode12 =
        ";а2010251991944283521ГСиы9ыеШщы\u001d102м5уЕЩ-\u001d17250404";

    public const string GreekBaseline =
        "]δ1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 \0¨\0΄< = > ? Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ _ α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   # $ @ [ \\ ] ^ ` { | } ~    \u001d    \u001c    \u001e        \r";

    public const string GreekDeadkeyBarcode1 =
        "]δ1\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨.\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨`\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨~\r";

    public const string GreekDeadkeyBarcode2 =
        "]δ1\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄.\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄`\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄~\r";

    public const string GreekDeadkeyBarcode3 =
        "]δ1\0΅!\0΅\"\0΅#\0΅$\0΅%\0΅&\0΅'\0΅(\0΅)\0΅*\0΅+\0΅,\0΅-\0΅.\0΅/\0΅0\0΅1\0΅2\0΅3\0΅4\0΅5\0΅6\0΅7\0΅8\0΅9\0΅¨\0΅΄\0΅<\0΅=\0΅>\0΅?\0΅@\0΅Α\0΅Β\0΅Ψ\0΅Δ\0΅Ε\0΅Φ\0΅Γ\0΅Η\0΅Ι\0΅Ξ\0΅Κ\0΅Λ\0΅Μ\0΅Ν\0΅Ο\0΅Π\0΅:\0΅Ρ\0΅Σ\0΅Τ\0΅Θ\0΅Ω\0΅΅\0΅Χ\0΅Υ\0΅Ζ\0΅[\0΅\\\0΅]\0΅^\0΅_\0΅`\0΅α\0΅β\0΅ψ\0΅δ\0΅ε\0΅φ\0΅γ\0΅η\0ΐ\0΅ξ\0΅κ\0΅λ\0΅μ\0΅ν\0΅ο\0΅π\0΅;\0΅ρ\0΅σ\0΅τ\0΅θ\0΅ω\0΅ς\0΅χ\0ΰ\0΅ζ\0΅{\0΅|\0΅}\0΅~\r";

    public const string GreekBarcode1 = "]δ2010251991944283521υΨΗ*4'η1Αβ\u001d10ΔδΩψΧ<τ\u001d17250209";
    public const string GreekBarcode2 = "]δ2010251991944283521ΤγΙω?,6ΒμΚ\u001d10.ΓΡσ!;Ο\u001d17250729";
    public const string GreekBarcode3 = "]δ2010251991944283521Ζ&Θρ7>0ο:Σ\u001d10(φπΝχΥι\u001d17250405";
    public const string GreekBarcode4 = "]δ2010251991944283521Ες5\0΄2/ζαΜΞ\u001d10=8Φν_Π\"\u001d17261002";
    public const string GreekBarcode5 = "]δ20102519919442835215ξ4ΨλτΕψ-Υ\u001d103κθ\0΅)Λ9\u001d17260304";
    public const string GreekBarcode6 = "]δ201025199194428352151ιτΞζΨγθΑ\u001d10\0¨ξ\"%ε+Π\u001d17251031";
    public const string GreekBarcode7 = "]δ2010251991944283521τ<ΧψΩδΔ0;!\u001d10βΑ1η'4*\u001d17251214";
    public const string GreekBarcode8 = "]δ2010251991944283521σΡΓ.ιΥχΝπφ\u001d10ΗΨυΚμΒ6\u001d17260620";
    public const string GreekBarcode9 = "]δ2010251991944283521(\"Π_νΦ8=9Λ\u001d10,?ωΙγΤΣ\u001d17260218";
    public const string GreekBarcode10 = "]δ2010251991944283521)\0΅θκ3Π+ε%\"\u001d10:ο0>7ρΘ\u001d17260703";
    public const string GreekBarcode11 = "]δ2010251991944283521\"ξ\0¨ΑθγΨζΞτ\u001d10&ΖΞΜαζ/\u001d17251126";
    public const string GreekBarcode12 = "]δ2010251991944283521ΗΙρ:9:εΤυ:\u001d102\0΄5ςΕΥ-\u001d17250404";

    public const string Greek220Baseline =
        "}δ1  ! \0΅% / \0¨) = ( [ , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; ] : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω ~ Χ Υ Ζ ? α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   £ $ \" + # } & ½ * @ { ±    \u001d    \0    \u001e    \0    \r";

    public const string Greek319Baseline =
        "]δ1  ! ‘ % / ’ ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; + : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π ― Ρ Σ Τ Θ Ω ¦ Χ Υ Ζ ° α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π · ρ σ τ θ ω ς χ υ ζ   £ $ \" [ ² ] ¬ ½ « ³ » ±    \u001d    \u001c    \u001e    \u001f    \r";

    public const string Greek319DeadkeyBarcode1 =
        "]δ1\0¨!\0¨‘\0¨£\0¨$\0¨%\0¨/\0¨’\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨;\0¨+\0¨:\0¨_\0¨\"\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨―\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨¦\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨²\0¨]\0¨¬\0¨°\0¨½\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨·\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨«\0¨³\0¨»\0¨±\r";

    public const string Greek319DeadkeyBarcode2 =
        "]δ1\0΄!\0΄‘\0΄£\0΄$\0΄%\0΄/\0΄’\0΄)\0΄=\0΄(\0΄*\0΄,\0΄'\0΄.\0΄-\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄;\0΄+\0΄:\0΄_\0΄\"\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄―\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄¦\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄²\0΄]\0΄¬\0΄°\0΄½\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄·\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄«\0΄³\0΄»\0΄±\r";

    public const string Greek319Barcode1 = "]δ2010251991944283521υΨΗ(4’η1Αβ\u001d10ΔδΩψΧ;τ\u001d17250209";
    public const string Greek319Barcode2 = "]δ2010251991944283521ΤγΙω_,6ΒμΚ\u001d10.ΓΡσ!·Ο\u001d17250729";
    public const string Greek319Barcode3 = "]δ2010251991944283521Ζ/Θρ7:0ο―Σ\u001d10)φπΝχΥι\u001d17250405";
    public const string Greek319Barcode4 = "]δ2010251991944283521Ες5\0΄2-ζαΜΞ\u001d10+8Φν°Π‘\u001d17261002";
    public const string Greek319Barcode5 = "]δ20102519919442835215ξ4ΨλτΕψ'Υ\u001d103κθ¦=Λ9\u001d17260304";
    public const string Greek319Barcode6 = "]δ201025199194428352151ιτΞζΨγθΑ\u001d10\0¨ξ‘%ε*Π\u001d17251031";
    public const string Greek319Barcode7 = "]δ2010251991944283521τ;ΧψΩδΔ0·!\u001d10βΑ1η’4(\u001d17251214";
    public const string Greek319Barcode8 = "]δ2010251991944283521σΡΓ.ιΥχΝπφ\u001d10ΗΨυΚμΒ6\u001d17260620";
    public const string Greek319Barcode9 = "]δ2010251991944283521)‘Π°νΦ8+9Λ\u001d10,_ωΙγΤΣ\u001d17260218";
    public const string Greek319Barcode10 = "]δ2010251991944283521=¦θκ3Π*ε%‘\u001d10―ο0:7ρΘ\u001d17260703";
    public const string Greek319Barcode11 = "]δ2010251991944283521‘ξ\0¨ΑθγΨζΞτ\u001d10/ΖΞΜαζ-\u001d17251126";
    public const string Greek319Barcode12 = "]δ2010251991944283521ΗΙρ―9―εΤυ―\u001d102\0΄5ςΕΥ'\u001d17250404";

    public const string Greek220LatinBaseline =
        "}d1  ! \0΅% / \0¨) = ( [ , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; ] : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" + # } & \\ * @ { |    \u001d    \0    \u001e    \0    \r";

    public const string Greek319LatinBaseline =
        "]d1  ! \0~% / \0^) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0´; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" [ \0`] & \\ { @ } |    \u001d    \0    \0    \0    \r";

    public const string Greek319LatinDeadkeyBarcode1 =
        "]d1\0~!\0~~\0~#\0~$\0~%\0~/\0~^\0~)\0~=\0~(\0~*\0~,\0~'\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~¨\0~´\0~;\0~+\0~:\0~_\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~[\0~`\0~]\0~&\0~?\0~\\\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~{\0~@\0~}\0~|\r";

    public const string Greek319LatinDeadkeyBarcode2 =
        "]d1\0^!\0^~\0^#\0^$\0^%\0^/\0^^\0^)\0^=\0^(\0^*\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^¨\0^´\0^;\0^+\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^[\0^`\0^]\0^&\0^?\0^\\\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^{\0^@\0^}\0^|\r";

    public const string Greek319LatinDeadkeyBarcode3 =
        "]d1\0¨!\0¨~\0¨#\0¨$\0¨%\0¨/\0¨^\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨´\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨[\0¨`\0¨]\0¨&\0¨?\0¨\\\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨{\0¨@\0¨}\0¨|\r";

    public const string Greek319LatinDeadkeyBarcode4 =
        "]d1\0´!\0´~\0´#\0´$\0´%\0´/\0´^\0´)\0´=\0´(\0´*\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´¨\0´´\0´;\0´+\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´[\0´`\0´]\0´&\0´?\0´\\\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´{\0´@\0´}\0´|\r";

    public const string Greek319LatinDeadkeyBarcode5 =
        "]d1\0`!\0`~\0`#\0`$\0`%\0`/\0`^\0`)\0`=\0`(\0`*\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`¨\0`´\0`;\0`+\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0``\0`]\0`&\0`?\0`\\\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`@\0`}\0`|\r";

    public const string Greek319LatinBarcode1 = "]d2010251991944283521yCH(4\0^h1Ab\u001d10DdVcX;t\u001d17250209";
    public const string Greek319LatinBarcode2 = "]d2010251991944283521TgIv_,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string Greek319LatinBarcode3 = "]d2010251991944283521Z/Ur7:0oQS\u001d10)fpNxYi\u001d17250405";
    public const string Greek319LatinBarcode4 = "]d2010251991944283521Ew5\0´2-zaMJ\u001d10+8Fn?P\0~\u001d17261002";
    public const string Greek319LatinBarcode5 = "]d20102519919442835215j4CltEc'Y\u001d103kuW=L9\u001d17260304";
    public const string Greek319LatinBarcode6 = "]d201025199194428352151itJzCguA\u001d10\0¨j\0~%e*P\u001d17251031";
    public const string Greek319LatinBarcode7 = "]d2010251991944283521t;XcVdD0q!\u001d10bA1h\0^4(\u001d17251214";
    public const string Greek319LatinBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string Greek319LatinBarcode9 = "]d2010251991944283521)\0~P?nF8+9L\u001d10,_vIgTS\u001d17260218";
    public const string Greek319LatinBarcode10 = "]d2010251991944283521=Wuk3P*e%\0~\u001d10Qo0:7rU\u001d17260703";
    public const string Greek319LatinBarcode11 = "]d2010251991944283521\0~j\0ÄugCzJt\u001d10/ZJMaz-\u001d17251126";
    public const string Greek319LatinBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102\0´5wEY'\u001d17250404";

    public const string GreekLatinBaseline =
        "]d1  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] \0^\0`{ | } \0~   \u001d    \u001c    \u001e        \r";

    public const string GreekLatinDeadkeyBarcode1 =
        "]d1\0^!\0^\"\0^#\0^$\0^%\0^&\0^'\0^(\0^)\0^*\0^+\0^,\0^-\0^.\0^/\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^:\0^;\0^<\0^=\0^>\0^?\0^@\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^[\0^\\\0^]\0^^\0^_\0^`\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^{\0^|\0^}\0^~\r";

    public const string GreekLatinDeadkeyBarcode2 =
        "]d1\0`!\0`\"\0`#\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`@\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`\\\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`|\0`}\0`~\r";

    public const string GreekLatinDeadkeyBarcode3 =
        "]d1\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~[\0~\\\0~]\0~^\0~_\0~`\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~{\0~|\0~}\0~~\r";

    public const string GreekLatinBarcode1 = "]d2010251991944283521yCH*4'h1Ab\u001d10DdVcX<t\u001d17250209";
    public const string GreekLatinBarcode2 = "]d2010251991944283521TgIv?,6BmK\u001d10.GRs!qO\u001d17250729";
    public const string GreekLatinBarcode3 = "]d2010251991944283521Z&Ur7>0oQS\u001d10(fpNxYi\u001d17250405";
    public const string GreekLatinBarcode4 = "]d2010251991944283521Ew5;2/zaMJ\u001d10=8Fn_P\"\u001d17261002";
    public const string GreekLatinBarcode5 = "]d20102519919442835215j4CltEc-Y\u001d103kuW)L9\u001d17260304";
    public const string GreekLatinBarcode6 = "]d201025199194428352151itJzCguA\u001d10:j\"%e+P\u001d17251031";
    public const string GreekLatinBarcode7 = "]d2010251991944283521t<XcVdD0q!\u001d10bA1h'4*\u001d17251214";
    public const string GreekLatinBarcode8 = "]d2010251991944283521sRG.iYxNpf\u001d10HCyKmB6\u001d17260620";
    public const string GreekLatinBarcode9 = "]d2010251991944283521(\"P_nF8=9L\u001d10,?vIgTS\u001d17260218";
    public const string GreekLatinBarcode10 = "]d2010251991944283521)Wuk3P+e%\"\u001d10Qo0>7rU\u001d17260703";
    public const string GreekLatinBarcode11 = "]d2010251991944283521\"j:AugCzJt\u001d10&ZJMaz/\u001d17251126";
    public const string GreekLatinBarcode12 = "]d2010251991944283521HIrQ9QeTyQ\u001d102;5wEY-\u001d17250404";

    public const string GreekPolytonicBaseline =
        "\0]δ1  ! \0\"% & \0'( ) * \0+, \0-. \0/0 1 2 3 4 5 6 7 8 9 \0¨\0΄< \0=> \0?Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π \0:Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ \0_α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π \0;ρ σ τ θ ω ς χ υ ζ   # $ @ \0[\0\\\0]^ \0~\0{\0|\0}\0`   \u001d    \u001c    \u001e        \r";

    public const string GreekPolytonicDeadkeyBarcode1 =
        "\0]δ1\0\"!\0\"\"\0\"#\0\"$\0\"%\0\"&\0\"'\0\"(\0\")\0\"*\0\"+\0\",\0\"-\0῾\0\"/\0\"0\0\"1\0\"2\0\"3\0\"4\0\"5\0\"6\0\"7\0\"8\0\"9\0\"¨\0\"΄\0\"<\0\"=\0\">\0\"?\0\"@\0Ἁ\0\"Β\0\"Ψ\0\"Δ\0Ἑ\0\"Φ\0\"Γ\0Ἡ\0Ἱ\0\"Ξ\0\"Κ\0\"Λ\0\"Μ\0\"Ν\0Ὁ\0\"Π\0\":\0Ῥ\0\"Σ\0\"Τ\0\"Θ\0Ὡ\0\"΅\0\"Χ\0Ὑ\0\"Ζ\0\"[\0\"\\\0\"]\0\"^\0\"_\0\"~\0ἁ\0\"β\0\"ψ\0\"δ\0ἑ\0\"φ\0\"γ\0ἡ\0ἱ\0\"ξ\0\"κ\0\"λ\0\"μ\0\"ν\0ὁ\0\"π\0\";\0ῥ\0\"σ\0\"τ\0\"θ\0ὡ\0\"ς\0\"χ\0ὑ\0\"ζ\0\"{\0\"|\0\"}\0\"`\r";

    public const string GreekPolytonicDeadkeyBarcode2 =
        "\0]δ1\0'!\0'\"\0'#\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0᾿\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0'¨\0'΄\0'<\0'=\0'>\0'?\0'@\0Ἀ\0'Β\0'Ψ\0'Δ\0Ἐ\0'Φ\0'Γ\0Ἠ\0Ἰ\0'Ξ\0'Κ\0'Λ\0'Μ\0'Ν\0Ὀ\0'Π\0':\0'Ρ\0'Σ\0'Τ\0'Θ\0Ὠ\0'΅\0'Χ\0'Υ\0'Ζ\0'[\0'\\\0']\0'^\0'_\0'~\0ἀ\0'β\0'ψ\0'δ\0ἐ\0'φ\0'γ\0ἠ\0ἰ\0'ξ\0'κ\0'λ\0'μ\0'ν\0ὀ\0'π\0';\0ῤ\0'σ\0'τ\0'θ\0ὠ\0'ς\0'χ\0ὐ\0'ζ\0'{\0'|\0'}\0'`\r";

    public const string GreekPolytonicDeadkeyBarcode3 =
        "\0]δ1\0+!\0+\"\0+#\0+$\0+%\0+&\0+'\0+(\0+)\0+*\0++\0+,\0+-\0῟\0+/\0+0\0+1\0+2\0+3\0+4\0+5\0+6\0+7\0+8\0+9\0+¨\0+΄\0+<\0+=\0+>\0+?\0+@\0Ἇ\0+Β\0+Ψ\0+Δ\0+Ε\0+Φ\0+Γ\0Ἧ\0Ἷ\0+Ξ\0+Κ\0+Λ\0+Μ\0+Ν\0+Ο\0+Π\0+:\0+Ρ\0+Σ\0+Τ\0+Θ\0Ὧ\0+΅\0+Χ\0Ὗ\0+Ζ\0+[\0+\\\0+]\0+^\0+_\0+~\0ἇ\0+β\0+ψ\0+δ\0+ε\0+φ\0+γ\0ἧ\0ἷ\0+ξ\0+κ\0+λ\0+μ\0+ν\0+ο\0+π\0+;\0+ρ\0+σ\0+τ\0+θ\0ὧ\0+ς\0+χ\0ὗ\0+ζ\0+{\0+|\0+}\0+`\r";

    public const string GreekPolytonicDeadkeyBarcode4 =
        "\0]δ1\0-!\0-\"\0-#\0-$\0-%\0-&\0-'\0-(\0-)\0-*\0-+\0-,\0--\0¯\0-/\0-0\0-1\0-2\0-3\0-4\0-5\0-6\0-7\0-8\0-9\0-¨\0-΄\0-<\0-=\0->\0-?\0-@\0Ᾱ\0-Β\0-Ψ\0-Δ\0-Ε\0-Φ\0-Γ\0-Η\0Ῑ\0-Ξ\0-Κ\0-Λ\0-Μ\0-Ν\0-Ο\0-Π\0-:\0-Ρ\0-Σ\0-Τ\0-Θ\0-Ω\0-΅\0-Χ\0Ῡ\0-Ζ\0-[\0-\\\0-]\0-^\0-_\0-~\0ᾱ\0-β\0-ψ\0-δ\0-ε\0-φ\0-γ\0-η\0ῑ\0-ξ\0-κ\0-λ\0-μ\0-ν\0-ο\0-π\0-;\0-ρ\0-σ\0-τ\0-θ\0-ω\0-ς\0-χ\0ῡ\0-ζ\0-{\0-|\0-}\0-`\r";

    public const string GreekPolytonicDeadkeyBarcode5 =
        "\0]δ1\0/!\0/\"\0/#\0/$\0/%\0/&\0/'\0/(\0/)\0/*\0/+\0/,\0/-\0῎\0//\0/0\0/1\0/2\0/3\0/4\0/5\0/6\0/7\0/8\0/9\0/¨\0/΄\0/<\0/=\0/>\0/?\0/@\0Ἄ\0/Β\0/Ψ\0/Δ\0Ἔ\0/Φ\0/Γ\0Ἤ\0Ἴ\0/Ξ\0/Κ\0/Λ\0/Μ\0/Ν\0Ὄ\0/Π\0/:\0/Ρ\0/Σ\0/Τ\0/Θ\0Ὤ\0/΅\0/Χ\0/Υ\0/Ζ\0/[\0/\\\0/]\0/^\0/_\0/~\0ἄ\0/β\0/ψ\0/δ\0ἔ\0/φ\0/γ\0ἤ\0ἴ\0/ξ\0/κ\0/λ\0/μ\0/ν\0ὄ\0/π\0/;\0/ρ\0/σ\0/τ\0/θ\0ὤ\0/ς\0/χ\0ὔ\0/ζ\0/{\0/|\0/}\0/`\r";

    public const string GreekPolytonicDeadkeyBarcode6 =
        "\0]δ1\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨~\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨`\r";

    public const string GreekPolytonicDeadkeyBarcode7 =
        "\0]δ1\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄~\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄`\r";
}