// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardCalibratorTestsFromUnitedStates.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.  All rights reserved.
// </copyright>
// <summary>
// Unit tests for the Keyboard Calibrator
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.EmvsUniqueIdentifier.Tests;

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

using EmvsUniqueIdentifier;
using Packs;
using BarcodeScanner.Calibration;
using BarcodeScanner.Calibration.DataMatrix;

/// <summary>
/// Unit tests for the Keyboard Calibrator.
/// </summary>
public class KeyboardCalibratorTestsFromUnitedStates {
    private const string UnitedStatesBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string UnitedStatesBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string UnitedStatesBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string UnitedStatesBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string UnitedStatesBarcode4 = "010477298543159410=8Fn_P\"\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string UnitedStatesBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string UnitedStatesBarcode6 = "010477298543159410:j\"%e+P\x001D172310312151itJzCguA\x000D";
    private const string UnitedStatesBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string UnitedStatesBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string UnitedStatesBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(\"P_nF8=9L\x000D";
    private const string UnitedStatesBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%\"\x000D";
    private const string UnitedStatesBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621\"j:AugCzJt\x000D";
    private const string UnitedStatesBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";

    private const string LexonErrorABaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \"\x000D";
    private const string LexonErrorABarcode1 = "010477298543159410DdVcX<t\"1723020721yCH*4'h1Ab\x000D";
    private const string LexonErrorABarcode2 = "010477298543159410.GRs!qO\"1723072921TgIv?,6BmK\x000D";
    private const string LexonErrorABarcode3 = "010477298543159410(fpNxYi\"1723040521Z&Ur7>0oQS\x000D";
    private const string LexonErrorABarcode4 = "010477298543159410=8Fn_P@\"1724100221Ew5;2/zaMJ\x000D";
    private const string LexonErrorABarcode5 = "0104772985431594103kuW)L9\"17240304215j4CltEc-Y\x000D";
    private const string LexonErrorABarcode6 = "010477298543159410:j@%e+P\"172310312151itJzCguA\x000D";
    private const string LexonErrorABarcode7 = "010477298543159410bA1h'4*\"1723121421t<XcVdD0q!\x000D";
    private const string LexonErrorABarcode8 = "010477298543159410HCyKmB6\"1724062021sRG.iYxNpf\x000D";
    private const string LexonErrorABarcode9 = "010477298543159410,?vIgTS\"1724021821(@P_nF8=9L\x000D";
    private const string LexonErrorABarcode10 = "010477298543159410Qo0>7rU\"1724070321)Wuk3P+e%@\x000D";
    private const string LexonErrorABarcode11 = "010477298543159410&ZJMaz/\"1723112621@j:AugCzJt\x000D";
    private const string LexonErrorABarcode12 = "0104772985431594102;5wEY-\"1723040421HIrQ9QeTyQ\x000D";
    private const string LexonErrorBBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \"    \x001C    \0    \0    \0    \x000D";
    private const string LexonErrorCBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    @    \x001C    \0    \0    \0    \x000D";
    private const string LexonErrorCBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string LexonErrorCBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string LexonErrorCBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string LexonErrorCBarcode4 = "010477298543159410=8Fn_P\"\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string LexonErrorCBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string LexonErrorCBarcode6 = "010477298543159410:j\"%e+P\x001D172310312151itJzCguA\x000D";
    private const string LexonErrorCBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string LexonErrorCBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string LexonErrorCBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(\"P_nF8=9L\x000D";
    private const string LexonErrorCBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%\"\x000D";
    private const string LexonErrorCBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621\"j:AugCzJt\x000D";
    private const string LexonErrorCBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";

    private const string BelgianFrenchBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BelgianFrenchDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianFrenchDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianFrenchBarcode1 = "à&à'èèéç!('\"&(ç'&àDdVcX.t\x001D&èé\"àéàèé&yCH8'ùh&Qb\x000D";
    private const string BelgianFrenchBarcode2 = "à&à'èèéç!('\"&(ç'&à:GRs1aO\x001D&èé\"àèéçé&TgIv+;§B,K\x000D";
    private const string BelgianFrenchBarcode3 = "à&à'èèéç!('\"&(ç'&à9fpNxYi\x001D&èé\"à'à(é&W7Urè/àoAS\x000D";
    private const string BelgianFrenchBarcode4 = "à&à'èèéç!('\"&(ç'&à-!Fn°P%\x001D&èé'&ààéé&Ez(mé=wq?J\x000D";
    private const string BelgianFrenchBarcode5 = "à&à'èèéç!('\"&(ç'&à\"kuZ0Lç\x001D&èé'à\"à'é&(j'CltEc)Y\x000D";
    private const string BelgianFrenchBarcode6 = "à&à'èèéç!('\"&(ç'&àMj%5e_P\x001D&èé\"&à\"&é&(&itJwCguQ\x000D";
    private const string BelgianFrenchBarcode7 = "à&à'èèéç!('\"&(ç'&àbQ&hù'8\x001D&èé\"&é&'é&t.XcVdDàa1\x000D";
    private const string BelgianFrenchBarcode8 = "à&à'èèéç!('\"&(ç'&àHCyK,B§\x001D&èé'à§éàé&sRG:iYxNpf\x000D";
    private const string BelgianFrenchBarcode9 = "à&à'èèéç!('\"&(ç'&à;+vIgTS\x001D&èé'àé&!é&9%P°nF!-çL\x000D";
    private const string BelgianFrenchBarcode10 = "à&à'èèéç!('\"&(ç'&àAoà/èrU\x001D&èé'àèà\"é&0Zuk\"P_e5%\x000D";
    private const string BelgianFrenchBarcode11 = "à&à'èèéç!('\"&(ç'&à7WJ?qw=\x001D&èé\"&&é§é&%jMQugCwJt\x000D";
    private const string BelgianFrenchBarcode12 = "à&à'èèéç!('\"&(ç'&àém(zEY)\x001D&èé\"à'à'é&HIrAçAeTyA\x000D";
    private const string BelgianCommaBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BelgianCommaDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianCommaDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianCommaBarcode1 = "à&à'èèéç!('\"&(ç'&àDdVcX.t\x001D&èé\"àéàèé&yCH8'ùh&Qb\x000D";
    private const string BelgianCommaBarcode2 = "à&à'èèéç!('\"&(ç'&à:GRs1aO\x001D&èé\"àèéçé&TgIv+;§B,K\x000D";
    private const string BelgianCommaBarcode3 = "à&à'èèéç!('\"&(ç'&à9fpNxYi\x001D&èé\"à'à(é&W7Urè/àoAS\x000D";
    private const string BelgianCommaBarcode4 = "à&à'èèéç!('\"&(ç'&à-!Fn°P%\x001D&èé'&ààéé&Ez(mé=wq?J\x000D";
    private const string BelgianCommaBarcode5 = "à&à'èèéç!('\"&(ç'&à\"kuZ0Lç\x001D&èé'à\"à'é&(j'CltEc)Y\x000D";
    private const string BelgianCommaBarcode6 = "à&à'èèéç!('\"&(ç'&àMj%5e_P\x001D&èé\"&à\"&é&(&itJwCguQ\x000D";
    private const string BelgianCommaBarcode7 = "à&à'èèéç!('\"&(ç'&àbQ&hù'8\x001D&èé\"&é&'é&t.XcVdDàa1\x000D";
    private const string BelgianCommaBarcode8 = "à&à'èèéç!('\"&(ç'&àHCyK,B§\x001D&èé'à§éàé&sRG:iYxNpf\x000D";
    private const string BelgianCommaBarcode9 = "à&à'èèéç!('\"&(ç'&à;+vIgTS\x001D&èé'àé&!é&9%P°nF!-çL\x000D";
    private const string BelgianCommaBarcode10 = "à&à'èèéç!('\"&(ç'&àAoà/èrU\x001D&èé'àèà\"é&0Zuk\"P_e5%\x000D";
    private const string BelgianCommaBarcode11 = "à&à'èèéç!('\"&(ç'&à7WJ?qw=\x001D&èé\"&&é§é&%jMQugCwJt\x000D";
    private const string BelgianCommaBarcode12 = "à&à'èèéç!('\"&(ç'&àém(zEY)\x001D&èé\"à'à'é&HIrAçAeTyA\x000D";
    private const string BelgianPeriodBaseline = "  1 % 5 7 ù 9 0 8 _ ; ) : = à & é \" ' ( § è ! ç M m . - / + Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^µ $ 6 ² \0¨£ * ³    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BelgianPeriodDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^_\0^;\0^)\0^:\0^=\0^à\0^&\0^é\0^\"\0^'\0^(\0^§\0^è\0^!\0^ç\0^M\0^m\0^.\0^-\0^/\0^+\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^µ\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^£\0^*\0^³\x000D";
    private const string BelgianPeriodDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨_\0¨;\0¨)\0¨:\0¨=\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨§\0¨è\0¨!\0¨ç\0¨M\0¨m\0¨.\0¨-\0¨/\0¨+\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨µ\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨£\0¨*\0¨³\x000D";
    private const string BelgianPeriodBarcode1 = "à&à'èèéç!('\"&(ç'&àDdVcX.t\x001D&èé\"àéàèé&yCH8'ùh&Qb\x000D";
    private const string BelgianPeriodBarcode2 = "à&à'èèéç!('\"&(ç'&à:GRs1aO\x001D&èé\"àèéçé&TgIv+;§B,K\x000D";
    private const string BelgianPeriodBarcode3 = "à&à'èèéç!('\"&(ç'&à9fpNxYi\x001D&èé\"à'à(é&W7Urè/àoAS\x000D";
    private const string BelgianPeriodBarcode4 = "à&à'èèéç!('\"&(ç'&à-!Fn°P%\x001D&èé'&ààéé&Ez(mé=wq?J\x000D";
    private const string BelgianPeriodBarcode5 = "à&à'èèéç!('\"&(ç'&à\"kuZ0Lç\x001D&èé'à\"à'é&(j'CltEc)Y\x000D";
    private const string BelgianPeriodBarcode6 = "à&à'èèéç!('\"&(ç'&àMj%5e_P\x001D&èé\"&à\"&é&(&itJwCguQ\x000D";
    private const string BelgianPeriodBarcode7 = "à&à'èèéç!('\"&(ç'&àbQ&hù'8\x001D&èé\"&é&'é&t.XcVdDàa1\x000D";
    private const string BelgianPeriodBarcode8 = "à&à'èèéç!('\"&(ç'&àHCyK,B§\x001D&èé'à§éàé&sRG:iYxNpf\x000D";
    private const string BelgianPeriodBarcode9 = "à&à'èèéç!('\"&(ç'&à;+vIgTS\x001D&èé'àé&!é&9%P°nF!-çL\x000D";
    private const string BelgianPeriodBarcode10 = "à&à'èèéç!('\"&(ç'&àAoà/èrU\x001D&èé'àèà\"é&0Zuk\"P_e5%\x000D";
    private const string BelgianPeriodBarcode11 = "à&à'èèéç!('\"&(ç'&à7WJ?qw=\x001D&èé\"&&é§é&%jMQugCwJt\x000D";
    private const string BelgianPeriodBarcode12 = "à&à'èèéç!('\"&(ç'&àém(zEY)\x001D&èé\"à'à'é&HIrAçAeTyA\x000D";
    private const string FrenchBaseline = "  1 % 5 7 ù 9 0 8 + ; ) : ! à & é \" ' ( - è _ ç M m . = / § Q B C D E F G H I J K L ? N O P A R S T U V Z X Y W ° q b c d e f g h i j k l , n o p a r s t u v z x y w   3 4 2 \0^* $ 6 ² \0¨µ £ \0    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string FrenchDeadKey1 = "\0^1\0^%\0^3\0^4\0^5\0^7\0^ù\0^9\0^0\0^8\0^+\0^;\0^)\0^:\0^!\0^à\0^&\0^é\0^\"\0^'\0^(\0^-\0^è\0^_\0^ç\0^M\0^m\0^.\0^=\0^/\0^§\0^2\0^Q\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^?\0^N\0Ô\0^P\0Â\0^R\0^S\0^T\0Û\0^V\0^Z\0^X\0^Y\0^W\0^^\0^*\0^$\0^6\0^°\0^²\0^q\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^,\0^n\0ô\0^p\0â\0^r\0^s\0^t\0û\0^v\0^z\0^x\0^y\0^w\0^¨\0^µ\0^£\0\0^";
    private const string FrenchDeadKey2 = "\0¨1\0¨%\0¨3\0¨4\0¨5\0¨7\0¨ù\0¨9\0¨0\0¨8\0¨+\0¨;\0¨)\0¨:\0¨!\0¨à\0¨&\0¨é\0¨\"\0¨'\0¨(\0¨-\0¨è\0¨_\0¨ç\0¨M\0¨m\0¨.\0¨=\0¨/\0¨§\0¨2\0¨Q\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨?\0¨N\0Ö\0¨P\0Ä\0¨R\0¨S\0¨T\0Ü\0¨V\0¨Z\0¨X\0¨Y\0¨W\0¨^\0¨*\0¨$\0¨6\0¨°\0¨²\0¨q\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨,\0¨n\0ö\0¨p\0ä\0¨r\0¨s\0¨t\0ü\0¨v\0¨z\0¨x\0ÿ\0¨w\0¨¨\0¨µ\0¨£\0\0¨";
    private const string FrenchBarcode1 = "à&à'èèéç_('\"&(ç'&àDdVcX.t\x001D&èé\"àéàèé&yCH8'ùh&Qb\x000D";
    private const string FrenchBarcode2 = "à&à'èèéç_('\"&(ç'&à:GRs1aO\x001D&èé\"àèéçé&TgIv§;-B,K\x000D";
    private const string FrenchBarcode3 = "à&à'èèéç_('\"&(ç'&à9fpNxYi\x001D&èé\"à'à(é&W7Urè/àoAS\x000D";
    private const string FrenchBarcode4 = "à&à'èèéç_('\"&(ç'&à=_Fn°P%\x001D&èé'&ààéé&Ez(mé!wq?J\x000D";
    private const string FrenchBarcode5 = "à&à'èèéç_('\"&(ç'&à\"kuZ0Lç\x001D&èé'à\"à'é&(j'CltEc)Y\x000D";
    private const string FrenchBarcode6 = "à&à'èèéç_('\"&(ç'&àMj%5e+P\x001D&èé\"&à\"&é&(&itJwCguQ\x000D";
    private const string FrenchBarcode7 = "à&à'èèéç_('\"&(ç'&àbQ&hù'8\x001D&èé\"&é&'é&t.XcVdDàa1\x000D";
    private const string FrenchBarcode8 = "à&à'èèéç_('\"&(ç'&àHCyK,B-\x001D&èé'à-éàé&sRG:iYxNpf\x000D";
    private const string FrenchBarcode9 = "à&à'èèéç_('\"&(ç'&à;§vIgTS\x001D&èé'àé&_é&9%P°nF_=çL\x000D";
    private const string FrenchBarcode10 = "à&à'èèéç_('\"&(ç'&àAoà/èrU\x001D&èé'àèà\"é&0Zuk\"P+e5%\x000D";
    private const string FrenchBarcode11 = "à&à'èèéç_('\"&(ç'&à7WJ?qw!\x001D&èé\"&&é-é&%jMQugCwJt\x000D";
    private const string FrenchBarcode12 = "à&à'èèéç_('\"&(ç'&àém(zEY)\x001D&èé\"à'à'é&HIrAçAeTyA\x000D";
    private const string SwissFrenchBaseline = "  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string SwissFrenchDeadKey1 = "\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\x000D";
    private const string SwissFrenchDeadKey2 = "\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\x000D";
    private const string SwissFrenchDeadKey3 = "\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\x000D";
    private const string SwissFrenchBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4àh1Ab\x000D";
    private const string SwissFrenchBarcode2 = "010477298543159410.GRs+qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string SwissFrenchBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string SwissFrenchBarcode4 = "010477298543159410\0^8Fn?Pä\x001D1724100221Ew5é2-yaMJ\x000D";
    private const string SwissFrenchBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Z\x000D";
    private const string SwissFrenchBarcode6 = "010477298543159410öjä%e\0`P\x001D172310312151itJyCguA\x000D";
    private const string SwissFrenchBarcode7 = "010477298543159410bA1hà4(\x001D1723121421t;XcVdD0q+\x000D";
    private const string SwissFrenchBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string SwissFrenchBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)äP?nF8\0^9L\x000D";
    private const string SwissFrenchBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%ä\x000D";
    private const string SwissFrenchBarcode11 = "010477298543159410/YJMay-\x001D1723112621äjöAugCyJt\x000D";
    private const string SwissFrenchBarcode12 = "0104772985431594102é5wEZ'\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string CroatianStandardBaseline = "  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \x001B    \x001C    \0    \0    \0    \x000D";
    private const string CroatianStandardDeadKey1 = "\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\x000D";
    private const string CroatianStandardDeadKey2 = "\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\x000D";
    private const string CroatianStandardBarcode1 = "010477298543159410DdVcX;t\x001B1723020721zCH(4ćh1Ab\x000D";
    private const string CroatianStandardBarcode2 = "010477298543159410.GRs!qO\x001B1723072921TgIv_,6BmK\x000D";
    private const string CroatianStandardBarcode3 = "010477298543159410)fpNxZi\x001B1723040521Y/Ur7:0oQS\x000D";
    private const string CroatianStandardBarcode4 = "010477298543159410+8Fn?PĆ\x001B1724100221Ew5č2-yaMJ\x000D";
    private const string CroatianStandardBarcode5 = "0104772985431594103kuW=L9\x001B17240304215j4CltEc'Z\x000D";
    private const string CroatianStandardBarcode6 = "010477298543159410ČjĆ%e*P\x001B172310312151itJyCguA\x000D";
    private const string CroatianStandardBarcode7 = "010477298543159410bA1hć4(\x001B1723121421t;XcVdD0q!\x000D";
    private const string CroatianStandardBarcode8 = "010477298543159410HCzKmB6\x001B1724062021sRG.iZxNpf\x000D";
    private const string CroatianStandardBarcode9 = "010477298543159410,_vIgTS\x001B1724021821)ĆP?nF8+9L\x000D";
    private const string CroatianStandardBarcode10 = "010477298543159410Qo0:7rU\x001B1724070321=Wuk3P*e%Ć\x000D";
    private const string CroatianStandardBarcode11 = "010477298543159410/YJMay-\x001B1723112621ĆjČAugCyJt\x000D";
    private const string CroatianStandardBarcode12 = "0104772985431594102č5wEZ'\x001B1723040421HIrQ9QeTzQ\x000D";
    private const string BulgarianBaseline = "  ! Ч % : ч – № / € р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б ѝ Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю $ ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц „ ; = ( Ц “ § )    \0    \0    \0    \0    \0    \x000D";
    private const string BulgarianBarcode1 = "010477298543159410АаЭъЙРш\01723020721щЪГ/4чг1ѝф\x000D";
    private const string BulgarianBarcode2 = "010477298543159410лЖИя!,Д\01723072921ШжСэБр6ФпН\x000D";
    private const string BulgarianBarcode3 = "010477298543159410–озХйЩс\01723040521Ю:Ки7Л0дыЯ\x000D";
    private const string BulgarianBarcode4 = "010477298543159410.8Ох$ЗЧ\01724100221Еу5м2бюьПТ\x000D";
    private const string BulgarianBarcode5 = "0104772985431594103нкУ№В9\017240304215т4ЪвшЕъ-Щ\x000D";
    private const string BulgarianBarcode6 = "010477298543159410МтЧ%е€З\0172310312151сшТюЪжкѝ\x000D";
    private const string BulgarianBarcode7 = "010477298543159410фѝ1гч4/\01723121421шРЙъЭаА0,!\x000D";
    private const string BulgarianBarcode8 = "010477298543159410ГЪщНпФ6\01724062021яИЖлсЩйХзо\x000D";
    private const string BulgarianBarcode9 = "010477298543159410рБэСжШЯ\01724021821–ЧЗ$хО8.9В\x000D";
    private const string BulgarianBarcode10 = "010477298543159410ыд0Л7иК\01724070321№Укн3З€е%Ч\x000D";
    private const string BulgarianBarcode11 = "010477298543159410:ЮТПьюб\01723112621ЧтМѝкжЪюТш\x000D";
    private const string BulgarianBarcode12 = "0104772985431594102м5уЕЩ-\01723040421ГСиы9ыеШщы\x000D";
    private const string BulgarianLatinBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BulgarianLatinBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string BulgarianLatinBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string BulgarianLatinBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string BulgarianLatinBarcode4 = "010477298543159410=8Fn_P\"\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string BulgarianLatinBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string BulgarianLatinBarcode6 = "010477298543159410:j\"%e+P\x001D172310312151itJzCguA\x000D";
    private const string BulgarianLatinBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string BulgarianLatinBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string BulgarianLatinBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(\"P_nF8=9L\x000D";
    private const string BulgarianLatinBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%\"\x000D";
    private const string BulgarianLatinBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621\"j:AugCzJt\x000D";
    private const string BulgarianLatinBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string BulgarianPhoneticTraditionalBaseline = "  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Я Р С Т У Ж В ѝ Ъ З _ а б ц д е ф г х и й к л м н о п я р с т у ж в ь ъ з   № $ @ ш ю щ € ч Ш Ю Щ Ч    \0    \0    \0    \0    \0    \x000D";
    private const string BulgarianPhoneticTraditionalBarcode1 = "010477298543159410ДдЖцѝ<т\01723020721ъЦХ*4'х1Аб\x000D";
    private const string BulgarianPhoneticTraditionalBarcode2 = "010477298543159410.ГРс!яО\01723072921ТгИж?,6БмК\x000D";
    private const string BulgarianPhoneticTraditionalBarcode3 = "010477298543159410(фпНьЪи\01723040521З§Ур7>0оЯС\x000D";
    private const string BulgarianPhoneticTraditionalBarcode4 = "010477298543159410=8Фн_П\"\01724100221Ев5;2/заМЙ\x000D";
    private const string BulgarianPhoneticTraditionalBarcode5 = "0104772985431594103куВ)Л9\017240304215й4ЦлтЕц-Ъ\x000D";
    private const string BulgarianPhoneticTraditionalBarcode6 = "010477298543159410:й\"%е+П\0172310312151итЙзЦгуА\x000D";
    private const string BulgarianPhoneticTraditionalBarcode7 = "010477298543159410бА1х'4*\01723121421т<ѝцЖдД0я!\x000D";
    private const string BulgarianPhoneticTraditionalBarcode8 = "010477298543159410ХЦъКмБ6\01724062021сРГ.иЪьНпф\x000D";
    private const string BulgarianPhoneticTraditionalBarcode9 = "010477298543159410,?жИгТС\01724021821(\"П_нФ8=9Л\x000D";
    private const string BulgarianPhoneticTraditionalBarcode10 = "010477298543159410Яо0>7рУ\01724070321)Вук3П+е%\"\x000D";
    private const string BulgarianPhoneticTraditionalBarcode11 = "010477298543159410§ЗЙМаз/\01723112621\"й:АугЦзЙт\x000D";
    private const string BulgarianPhoneticTraditionalBarcode12 = "0104772985431594102;5вЕЪ-\01723040421ХИрЯ9ЯеТъЯ\x000D";
    private const string BulgarianPhoneticBaseline = "  ! \" % § ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; „ = “ ? А Б Ц Д Е Ф Г Х И Й К Л М Н О П Ч Р С Т У В Ш Ж Ъ З – а б ц д е ф г х и й к л м н о п ч р с т у в ш ж ъ з   № $ @ я ь щ € ю Я ѝ Щ Ю    \0    \0    \0    \0    \0    \x000D";
    private const string BulgarianPhoneticBarcode1 = "010477298543159410ДдВцЖ„т\01723020721ъЦХ*4'х1Аб\x000D";
    private const string BulgarianPhoneticBarcode2 = "010477298543159410.ГРс!чО\01723072921ТгИв?,6БмК\x000D";
    private const string BulgarianPhoneticBarcode3 = "010477298543159410(фпНжЪи\01723040521З§Ур7“0оЧС\x000D";
    private const string BulgarianPhoneticBarcode4 = "010477298543159410=8Фн–П\"\01724100221Еш5;2/заМЙ\x000D";
    private const string BulgarianPhoneticBarcode5 = "0104772985431594103куШ)Л9\017240304215й4ЦлтЕц-Ъ\x000D";
    private const string BulgarianPhoneticBarcode6 = "010477298543159410:й\"%е+П\0172310312151итЙзЦгуА\x000D";
    private const string BulgarianPhoneticBarcode7 = "010477298543159410бА1х'4*\01723121421т„ЖцВдД0ч!\x000D";
    private const string BulgarianPhoneticBarcode8 = "010477298543159410ХЦъКмБ6\01724062021сРГ.иЪжНпф\x000D";
    private const string BulgarianPhoneticBarcode9 = "010477298543159410,?вИгТС\01724021821(\"П–нФ8=9Л\x000D";
    private const string BulgarianPhoneticBarcode10 = "010477298543159410Чо0“7рУ\01724070321)Шук3П+е%\"\x000D";
    private const string BulgarianPhoneticBarcode11 = "010477298543159410§ЗЙМаз/\01723112621\"й:АугЦзЙт\x000D";
    private const string BulgarianPhoneticBarcode12 = "0104772985431594102;5шЕЪ-\01723040421ХИрЧ9ЧеТъЧ\x000D";
    private const string BulgarianTypewriterBaseline = "  ! Ч % : ч _ № / V р - л б 0 1 2 3 4 5 6 7 8 9 М м Р . Л Б Ь Ф Ъ А Е О Ж Г С Т Н В П Х Д З ы И Я Ш К Э У Й Щ Ю І ь ф ъ а е о ж г с т н в п х д з , и я ш к э у й щ ю   + \" ? ц ( ; = ` Ц ) § ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string BulgarianTypewriterBarcode1 = "010477298543159410АаЭъЙРш\x001D1723020721щЪГ/4чг1Ьф\x000D";
    private const string BulgarianTypewriterBarcode2 = "010477298543159410лЖИя!,Д\x001D1723072921ШжСэБр6ФпН\x000D";
    private const string BulgarianTypewriterBarcode3 = "010477298543159410_озХйЩс\x001D1723040521Ю:Ки7Л0дыЯ\x000D";
    private const string BulgarianTypewriterBarcode4 = "010477298543159410.8ОхІЗЧ\x001D1724100221Еу5м2бюьПТ\x000D";
    private const string BulgarianTypewriterBarcode5 = "0104772985431594103нкУ№В9\x001D17240304215т4ЪвшЕъ-Щ\x000D";
    private const string BulgarianTypewriterBarcode6 = "010477298543159410МтЧ%еVЗ\x001D172310312151сшТюЪжкЬ\x000D";
    private const string BulgarianTypewriterBarcode7 = "010477298543159410фЬ1гч4/\x001D1723121421шРЙъЭаА0,!\x000D";
    private const string BulgarianTypewriterBarcode8 = "010477298543159410ГЪщНпФ6\x001D1724062021яИЖлсЩйХзо\x000D";
    private const string BulgarianTypewriterBarcode9 = "010477298543159410рБэСжШЯ\x001D1724021821_ЧЗІхО8.9В\x000D";
    private const string BulgarianTypewriterBarcode10 = "010477298543159410ыд0Л7иК\x001D1724070321№Укн3ЗVе%Ч\x000D";
    private const string BulgarianTypewriterBarcode11 = "010477298543159410:ЮТПьюб\x001D1723112621ЧтМЬкжЪюТш\x000D";
    private const string BulgarianTypewriterBarcode12 = "0104772985431594102м5уЕЩ-\x001D1723040421ГСиы9ыеШщы\x000D";
    private const string SwedishBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \x001D    \0    \0    \0    \0    \x000D";
    private const string SwedishDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string SwedishDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\x000D";
    private const string SwedishDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string SwedishDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string SwedishBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4äh1Ab\x000D";
    private const string SwedishBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string SwedishBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string SwedishBarcode4 = "010477298543159410\0´8Fn?PÄ\x001D1724100221Ew5ö2-zaMJ\x000D";
    private const string SwedishBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Y\x000D";
    private const string SwedishBarcode6 = "010477298543159410ÖjÄ%e\0`P\x001D172310312151itJzCguA\x000D";
    private const string SwedishBarcode7 = "010477298543159410bA1hä4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string SwedishBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string SwedishBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ÄP?nF8\0´9L\x000D";
    private const string SwedishBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Ä\x000D";
    private const string SwedishBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621ÄjÖAugCzJt\x000D";
    private const string SwedishBarcode12 = "0104772985431594102ö5wEY+\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string SwedishWithSamiBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0    \0    \0    \x000D";
    private const string SwedishWithSamiDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string SwedishWithSamiDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\x000D";
    private const string SwedishWithSamiDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string SwedishWithSamiDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string SwedishWithSamiBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4äh1Ab\x000D";
    private const string SwedishWithSamiBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string SwedishWithSamiBarcode3 = "010477298543159410)fpNxYi\01723040521Z/Ur7:0oQS\x000D";
    private const string SwedishWithSamiBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-zaMJ\x000D";
    private const string SwedishWithSamiBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEc+Y\x000D";
    private const string SwedishWithSamiBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJzCguA\x000D";
    private const string SwedishWithSamiBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string SwedishWithSamiBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string SwedishWithSamiBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string SwedishWithSamiBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string SwedishWithSamiBarcode11 = "010477298543159410/ZJMaz-\01723112621ÄjÖAugCzJt\x000D";
    private const string SwedishWithSamiBarcode12 = "0104772985431594102ö5wEY+\01723040421HIrQ9QeTyQ\x000D";
    private const string GreekBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 \0¨\0΄< = > ? Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ _ α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string GreekDeadKey1 = "\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨.\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨`\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨~\x000D";
    private const string GreekDeadKey2 = "\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄.\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄`\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄~\x000D";
    private const string GreekDeadKey3 = "\0΅!\0΅\"\0΅#\0΅$\0΅%\0΅&\0΅'\0΅(\0΅)\0΅*\0΅+\0΅,\0΅-\0΅.\0΅/\0΅0\0΅1\0΅2\0΅3\0΅4\0΅5\0΅6\0΅7\0΅8\0΅9\0΅¨\0΅΄\0΅<\0΅=\0΅>\0΅?\0΅@\0΅Α\0΅Β\0΅Ψ\0΅Δ\0΅Ε\0΅Φ\0΅Γ\0΅Η\0΅Ι\0΅Ξ\0΅Κ\0΅Λ\0΅Μ\0΅Ν\0΅Ο\0΅Π\0΅:\0΅Ρ\0΅Σ\0΅Τ\0΅Θ\0΅Ω\0΅΅\0΅Χ\0΅Υ\0΅Ζ\0΅[\0΅\\\0΅]\0΅^\0΅_\0΅`\0΅α\0΅β\0΅ψ\0΅δ\0΅ε\0΅φ\0΅γ\0΅η\0ΐ\0΅ξ\0΅κ\0΅λ\0΅μ\0΅ν\0΅ο\0΅π\0΅;\0΅ρ\0΅σ\0΅τ\0΅θ\0΅ω\0΅ς\0΅χ\0ΰ\0΅ζ\0΅{\0΅|\0΅}\0΅~\x000D";
    private const string GreekBarcode1 = "010477298543159410ΔδΩψΧ<τ\x001D1723020721υΨΗ*4'η1Αβ\x000D";
    private const string GreekBarcode2 = "010477298543159410.ΓΡσ!;Ο\x001D1723072921ΤγΙω?,6ΒμΚ\x000D";
    private const string GreekBarcode3 = "010477298543159410(φπΝχΥι\x001D1723040521Ζ&Θρ7>0ο:Σ\x000D";
    private const string GreekBarcode4 = "010477298543159410=8Φν_Π\"\x001D1724100221Ες5\0΄2/ζαΜΞ\x000D";
    private const string GreekBarcode5 = "0104772985431594103κθ\0΅)Λ9\x001D17240304215ξ4ΨλτΕψ-Υ\x000D";
    private const string GreekBarcode6 = "010477298543159410\0¨ξ\"%ε+Π\x001D172310312151ιτΞζΨγθΑ\x000D";
    private const string GreekBarcode7 = "010477298543159410βΑ1η'4*\x001D1723121421τ<ΧψΩδΔ0;!\x000D";
    private const string GreekBarcode8 = "010477298543159410ΗΨυΚμΒ6\x001D1724062021σΡΓ.ιΥχΝπφ\x000D";
    private const string GreekBarcode9 = "010477298543159410,?ωΙγΤΣ\x001D1724021821(\"Π_νΦ8=9Λ\x000D";
    private const string GreekBarcode10 = "010477298543159410:ο0>7ρΘ\x001D1724070321)\0΅θκ3Π+ε%\"\x000D";
    private const string GreekBarcode11 = "010477298543159410&ΖΞΜαζ/\x001D1723112621\"ξ\0¨ΑθγΨζΞτ\x000D";
    private const string GreekBarcode12 = "0104772985431594102\0΄5ςΕΥ-\x001D1723040421ΗΙρ:9:εΤυ:\x000D";
    private const string Greek220Baseline = "  ! \0΅% / \0¨) = ( [ , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; ] : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π : Ρ Σ Τ Θ Ω ~ Χ Υ Ζ ? α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π ; ρ σ τ θ ω ς χ υ ζ   £ $ \" + # } & ½ * @ { ±    \x001D    \0    \0    \0    \0    \x000D";
    private const string Greek319Baseline = "  ! ‘ % / ’ ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 \0¨\0΄; + : _ Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π ― Ρ Σ Τ Θ Ω ¦ Χ Υ Ζ ° α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π · ρ σ τ θ ω ς χ υ ζ   £ $ \" [ ² ] ¬ ½ « ³ » ±    \x001D    \x001C    \0    \0    \x000D";
    private const string Greek319DeadKey1 = "\0¨!\0¨‘\0¨£\0¨$\0¨%\0¨/\0¨’\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨;\0¨+\0¨:\0¨_\0¨\"\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨―\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨¦\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨²\0¨]\0¨¬\0¨°\0¨½\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨·\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨«\0¨³\0¨»\0¨±\x000D";
    private const string Greek319DeadKey2 = "\0΄!\0΄‘\0΄£\0΄$\0΄%\0΄/\0΄’\0΄)\0΄=\0΄(\0΄*\0΄,\0΄'\0΄.\0΄-\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄;\0΄+\0΄:\0΄_\0΄\"\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄―\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄¦\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄²\0΄]\0΄¬\0΄°\0΄½\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄·\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄«\0΄³\0΄»\0΄±\x000D";
    private const string Greek319Barcode1 = "010477298543159410ΔδΩψΧ;τ\x001D1723020721υΨΗ(4’η1Αβ\x000D";
    private const string Greek319Barcode2 = "010477298543159410.ΓΡσ!·Ο\x001D1723072921ΤγΙω_,6ΒμΚ\x000D";
    private const string Greek319Barcode3 = "010477298543159410)φπΝχΥι\x001D1723040521Ζ/Θρ7:0ο―Σ\x000D";
    private const string Greek319Barcode4 = "010477298543159410+8Φν°Π‘\x001D1724100221Ες5\0΄2-ζαΜΞ\x000D";
    private const string Greek319Barcode5 = "0104772985431594103κθ¦=Λ9\x001D17240304215ξ4ΨλτΕψ'Υ\x000D";
    private const string Greek319Barcode6 = "010477298543159410\0¨ξ‘%ε*Π\x001D172310312151ιτΞζΨγθΑ\x000D";
    private const string Greek319Barcode7 = "010477298543159410βΑ1η’4(\x001D1723121421τ;ΧψΩδΔ0·!\x000D";
    private const string Greek319Barcode8 = "010477298543159410ΗΨυΚμΒ6\x001D1724062021σΡΓ.ιΥχΝπφ\x000D";
    private const string Greek319Barcode9 = "010477298543159410,_ωΙγΤΣ\x001D1724021821)‘Π°νΦ8+9Λ\x000D";
    private const string Greek319Barcode10 = "010477298543159410―ο0:7ρΘ\x001D1724070321=¦θκ3Π*ε%‘\x000D";
    private const string Greek319Barcode11 = "010477298543159410/ΖΞΜαζ-\x001D1723112621‘ξ\0¨ΑθγΨζΞτ\x000D";
    private const string Greek319Barcode12 = "0104772985431594102\0΄5ςΕΥ'\x001D1723040421ΗΙρ―9―εΤυ―\x000D";
    private const string GreekPolytonicBaseline = "  ! \0\"% & \0'( ) * \0+, \0-. \0/0 1 2 3 4 5 6 7 8 9 \0¨\0΄< \0=> \0?Α Β Ψ Δ Ε Φ Γ Η Ι Ξ Κ Λ Μ Ν Ο Π \0:Ρ Σ Τ Θ Ω \0΅Χ Υ Ζ \0_α β ψ δ ε φ γ η ι ξ κ λ μ ν ο π \0;ρ σ τ θ ω ς χ υ ζ   # $ @ \0[\0\\\0]^ \0~\0{\0|\0}\0`   \x001D    \x001C    \0    \0    \0    \x000D";
    private const string GreekPolytonicDeadKey1 = "\0\"!\0\"\"\0\"#\0\"$\0\"%\0\"&\0\"'\0\"(\0\")\0\"*\0\"+\0\",\0\"-\0῾\0\"/\0\"0\0\"1\0\"2\0\"3\0\"4\0\"5\0\"6\0\"7\0\"8\0\"9\0\"¨\0\"΄\0\"<\0\"=\0\">\0\"?\0\"@\0Ἁ\0\"Β\0\"Ψ\0\"Δ\0Ἑ\0\"Φ\0\"Γ\0Ἡ\0Ἱ\0\"Ξ\0\"Κ\0\"Λ\0\"Μ\0\"Ν\0Ὁ\0\"Π\0\":\0Ῥ\0\"Σ\0\"Τ\0\"Θ\0Ὡ\0\"΅\0\"Χ\0Ὑ\0\"Ζ\0\"[\0\"\\\0\"]\0\"^\0\"_\0\"~\0ἁ\0\"β\0\"ψ\0\"δ\0ἑ\0\"φ\0\"γ\0ἡ\0ἱ\0\"ξ\0\"κ\0\"λ\0\"μ\0\"ν\0ὁ\0\"π\0\";\0ῥ\0\"σ\0\"τ\0\"θ\0ὡ\0\"ς\0\"χ\0ὑ\0\"ζ\0\"{\0\"|\0\"}\0\"`\x000D";
    private const string GreekPolytonicDeadKey2 = "\0'!\0'\"\0'#\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0᾿\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0'¨\0'΄\0'<\0'=\0'>\0'?\0'@\0Ἀ\0'Β\0'Ψ\0'Δ\0Ἐ\0'Φ\0'Γ\0Ἠ\0Ἰ\0'Ξ\0'Κ\0'Λ\0'Μ\0'Ν\0Ὀ\0'Π\0':\0'Ρ\0'Σ\0'Τ\0'Θ\0Ὠ\0'΅\0'Χ\0'Υ\0'Ζ\0'[\0'\\\0']\0'^\0'_\0'~\0ἀ\0'β\0'ψ\0'δ\0ἐ\0'φ\0'γ\0ἠ\0ἰ\0'ξ\0'κ\0'λ\0'μ\0'ν\0ὀ\0'π\0';\0ῤ\0'σ\0'τ\0'θ\0ὠ\0'ς\0'χ\0ὐ\0'ζ\0'{\0'|\0'}\0'`\x000D";
    private const string GreekPolytonicDeadKey3 = "\0+!\0+\"\0+#\0+$\0+%\0+&\0+'\0+(\0+)\0+*\0++\0+,\0+-\0῟\0+/\0+0\0+1\0+2\0+3\0+4\0+5\0+6\0+7\0+8\0+9\0+¨\0+΄\0+<\0+=\0+>\0+?\0+@\0Ἇ\0+Β\0+Ψ\0+Δ\0+Ε\0+Φ\0+Γ\0Ἧ\0Ἷ\0+Ξ\0+Κ\0+Λ\0+Μ\0+Ν\0+Ο\0+Π\0+:\0+Ρ\0+Σ\0+Τ\0+Θ\0Ὧ\0+΅\0+Χ\0Ὗ\0+Ζ\0+[\0+\\\0+]\0+^\0+_\0+~\0ἇ\0+β\0+ψ\0+δ\0+ε\0+φ\0+γ\0ἧ\0ἷ\0+ξ\0+κ\0+λ\0+μ\0+ν\0+ο\0+π\0+;\0+ρ\0+σ\0+τ\0+θ\0ὧ\0+ς\0+χ\0ὗ\0+ζ\0+{\0+|\0+}\0+`\x000D";
    private const string GreekPolytonicDeadKey4 = "\0-!\0-\"\0-#\0-$\0-%\0-&\0-'\0-(\0-)\0-*\0-+\0-,\0--\0¯\0-/\0-0\0-1\0-2\0-3\0-4\0-5\0-6\0-7\0-8\0-9\0-¨\0-΄\0-<\0-=\0->\0-?\0-@\0Ᾱ\0-Β\0-Ψ\0-Δ\0-Ε\0-Φ\0-Γ\0-Η\0Ῑ\0-Ξ\0-Κ\0-Λ\0-Μ\0-Ν\0-Ο\0-Π\0-:\0-Ρ\0-Σ\0-Τ\0-Θ\0-Ω\0-΅\0-Χ\0Ῡ\0-Ζ\0-[\0-\\\0-]\0-^\0-_\0-~\0ᾱ\0-β\0-ψ\0-δ\0-ε\0-φ\0-γ\0-η\0ῑ\0-ξ\0-κ\0-λ\0-μ\0-ν\0-ο\0-π\0-;\0-ρ\0-σ\0-τ\0-θ\0-ω\0-ς\0-χ\0ῡ\0-ζ\0-{\0-|\0-}\0-`\x000D";
    private const string GreekPolytonicDeadKey5 = "\0/!\0/\"\0/#\0/$\0/%\0/&\0/'\0/(\0/)\0/*\0/+\0/,\0/-\0῎\0//\0/0\0/1\0/2\0/3\0/4\0/5\0/6\0/7\0/8\0/9\0/¨\0/΄\0/<\0/=\0/>\0/?\0/@\0Ἄ\0/Β\0/Ψ\0/Δ\0Ἔ\0/Φ\0/Γ\0Ἤ\0Ἴ\0/Ξ\0/Κ\0/Λ\0/Μ\0/Ν\0Ὄ\0/Π\0/:\0/Ρ\0/Σ\0/Τ\0/Θ\0Ὤ\0/΅\0/Χ\0/Υ\0/Ζ\0/[\0/\\\0/]\0/^\0/_\0/~\0ἄ\0/β\0/ψ\0/δ\0ἔ\0/φ\0/γ\0ἤ\0ἴ\0/ξ\0/κ\0/λ\0/μ\0/ν\0ὄ\0/π\0/;\0/ρ\0/σ\0/τ\0/θ\0ὤ\0/ς\0/χ\0ὔ\0/ζ\0/{\0/|\0/}\0/`\x000D";
    private const string GreekPolytonicDeadKey6 = "\0¨!\0¨\"\0¨#\0¨$\0¨%\0¨&\0¨'\0¨(\0¨)\0¨*\0¨+\0¨,\0¨-\0¨\0¨/\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨¨\0¨΄\0¨<\0¨=\0¨>\0¨?\0¨@\0¨Α\0¨Β\0¨Ψ\0¨Δ\0¨Ε\0¨Φ\0¨Γ\0¨Η\0Ϊ\0¨Ξ\0¨Κ\0¨Λ\0¨Μ\0¨Ν\0¨Ο\0¨Π\0¨:\0¨Ρ\0¨Σ\0¨Τ\0¨Θ\0¨Ω\0¨΅\0¨Χ\0Ϋ\0¨Ζ\0¨[\0¨\\\0¨]\0¨^\0¨_\0¨~\0¨α\0¨β\0¨ψ\0¨δ\0¨ε\0¨φ\0¨γ\0¨η\0ϊ\0¨ξ\0¨κ\0¨λ\0¨μ\0¨ν\0¨ο\0¨π\0¨;\0¨ρ\0¨σ\0¨τ\0¨θ\0¨ω\0¨ς\0¨χ\0ϋ\0¨ζ\0¨{\0¨|\0¨}\0¨`\x000D";
    private const string GreekPolytonicDeadKey7 = "\0΄!\0΄\"\0΄#\0΄$\0΄%\0΄&\0΄'\0΄(\0΄)\0΄*\0΄+\0΄,\0΄-\0΄\0΄/\0΄0\0΄1\0΄2\0΄3\0΄4\0΄5\0΄6\0΄7\0΄8\0΄9\0΄¨\0΄΄\0΄<\0΄=\0΄>\0΄?\0΄@\0Ά\0΄Β\0΄Ψ\0΄Δ\0Έ\0΄Φ\0΄Γ\0Ή\0Ί\0΄Ξ\0΄Κ\0΄Λ\0΄Μ\0΄Ν\0Ό\0΄Π\0΄:\0΄Ρ\0΄Σ\0΄Τ\0΄Θ\0Ώ\0΄΅\0΄Χ\0Ύ\0΄Ζ\0΄[\0΄\\\0΄]\0΄^\0΄_\0΄~\0ά\0΄β\0΄ψ\0΄δ\0έ\0΄φ\0΄γ\0ή\0ί\0΄ξ\0΄κ\0΄λ\0΄μ\0΄ν\0ό\0΄π\0΄;\0΄ρ\0΄σ\0΄τ\0΄θ\0ώ\0΄ς\0΄χ\0ύ\0΄ζ\0΄{\0΄|\0΄}\0΄`\x000D";
    private const string CzechBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú \0¨) 6 ; / ' ( \0°   \x001D    \x001C    \0    \0    \0    \x000D";
    private const string CzechDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ'\0ˇ(\0ˇ°\x000D";
    private const string CzechDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´/\0´'\0´(\0´°\x000D";
    private const string CzechDeadKey3 = "\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0Ÿ\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨/\0¨'\0¨(\0¨°\x000D";
    private const string CzechDeadKey4 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Z\0°Y\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°z\0°y\0°/\0°'\0°(\0°°\x000D";
    private const string CzechBarcode1 = "é+éčýýěíářčš+říč+éDdVcX?t\x001D+ýěšéěéýě+zCH8č§h+Ab\x000D";
    private const string CzechBarcode2 = "é+éčýýěíářčš+říč+é.GRs1qO\x001D+ýěšéýěíě+TgIv_,žBmK\x000D";
    private const string CzechBarcode3 = "é+éčýýěíářčš+říč+é9fpNxZi\x001D+ýěšéčéřě+Y7Urý:éoQS\x000D";
    private const string CzechBarcode4 = "é+éčýýěíářčš+říč+é\0´áFn%P!\x001D+ýěč+ééěě+Ewřůě-yaMJ\x000D";
    private const string CzechBarcode5 = "é+éčýýěíářčš+říč+éškuW0Lí\x001D+ýěčéšéčě+řjčCltEc=Z\x000D";
    private const string CzechBarcode6 = "é+éčýýěíářčš+říč+é\"j!5e\0ˇP\x001D+ýěš+éš+ě+ř+itJyCguA\x000D";
    private const string CzechBarcode7 = "é+éčýýěíářčš+říč+ébA+h§č8\x001D+ýěš+ě+čě+t?XcVdDéq1\x000D";
    private const string CzechBarcode8 = "é+éčýýěíářčš+říč+éHCzKmBž\x001D+ýěčéžěéě+sRG.iZxNpf\x000D";
    private const string CzechBarcode9 = "é+éčýýěíářčš+říč+é,_vIgTS\x001D+ýěčéě+áě+9!P%nFá\0´íL\x000D";
    private const string CzechBarcode10 = "é+éčýýěíářčš+říč+éQoé:ýrU\x001D+ýěčéýéšě+0WukšP\0ě5!\x000D";
    private const string CzechBarcode11 = "é+éčýýěíářčš+říč+é7YJMay-\x001D+ýěš++ěžě+!j\"AugCyJt\x000D";
    private const string CzechBarcode12 = "é+éčýýěíářčš+říč+éěůřwEZ=\x001D+ýěšéčéčě+HIrQíQeTzQ\x000D";
    private const string CzechQwertyBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ě š č ř ž ý á í \" ů ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú \0¨) 6 ; / ' ( \0°   \x001B    \x001C    \x001E    \x001F    \0    \x000D";
    private const string CzechQwertyDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇě\0ˇš\0ˇč\0ˇř\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇů\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇ¨\0ˇ)\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ'\0ˇ(\0ˇ°\x000D";
    private const string CzechQwertyDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ě\0´š\0´č\0´ř\0´ž\0´ý\0´á\0´í\0´\"\0´ů\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´¨\0´)\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´'\0´(\0´°\x000D";
    private const string CzechQwertyDeadKey3 = "\0¨1\0¨!\0¨3\0¨4\0¨5\0¨7\0¨§\0¨9\0¨0\0¨8\0¨ˇ\0¨,\0¨=\0¨.\0¨-\0¨é\0¨+\0¨ě\0¨š\0¨č\0¨ř\0¨ž\0¨ý\0¨á\0¨í\0¨\"\0¨ů\0¨?\0¨´\0¨:\0¨_\0¨2\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0Ÿ\0¨Z\0¨ú\0¨¨\0¨)\0¨6\0¨%\0¨;\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨/\0¨'\0¨(\0¨°\x000D";
    private const string CzechQwertyDeadKey4 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ě\0°š\0°č\0°ř\0°ž\0°ý\0°á\0°í\0°\"\0°ů\0°?\0°´\0°:\0°_\0°2\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°¨\0°)\0°6\0°%\0°;\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°'\0°(\0°°\x000D";
    private const string CzechQwertyBarcode1 = "é+éčýýěíářčš+říč+éDdVcX?t\x001B+ýěšéěéýě+yCH8č§h+Ab\x000D";
    private const string CzechQwertyBarcode2 = "é+éčýýěíářčš+říč+é.GRs1qO\x001B+ýěšéýěíě+TgIv_,žBmK\x000D";
    private const string CzechQwertyBarcode3 = "é+éčýýěíářčš+říč+é9fpNxYi\x001B+ýěšéčéřě+Z7Urý:éoQS\x000D";
    private const string CzechQwertyBarcode4 = "é+éčýýěíářčš+říč+é\0´áFn%P!\x001B+ýěč+ééěě+Ewřůě-zaMJ\x000D";
    private const string CzechQwertyBarcode5 = "é+éčýýěíářčš+říč+éškuW0Lí\x001B+ýěčéšéčě+řjčCltEc=Y\x000D";
    private const string CzechQwertyBarcode6 = "é+éčýýěíářčš+říč+é\"j!5e\0ˇP\x001B+ýěš+éš+ě+ř+itJzCguA\x000D";
    private const string CzechQwertyBarcode7 = "é+éčýýěíářčš+říč+ébA+h§č8\x001B+ýěš+ě+čě+t?XcVdDéq1\x000D";
    private const string CzechQwertyBarcode8 = "é+éčýýěíářčš+říč+éHCyKmBž\x001B+ýěčéžěéě+sRG.iYxNpf\x000D";
    private const string CzechQwertyBarcode9 = "é+éčýýěíářčš+říč+é,_vIgTS\x001B+ýěčéě+áě+9!P%nFá\0´íL\x000D";
    private const string CzechQwertyBarcode10 = "é+éčýýěíářčš+říč+éQoé:ýrU\x001B+ýěčéýéšě+0WukšP\0ě5!\x000D";
    private const string CzechQwertyBarcode11 = "é+éčýýěíářčš+říč+é7ZJMaz-\x001B+ýěš++ěžě+!j\"AugCzJt\x000D";
    private const string CzechQwertyBarcode12 = "é+éčýýěíářčš+říč+éěůřwEY=\x001B+ýěšéčéčě+HIrQíQeTyQ\x000D";
    private const string CzechProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001B    \x001C    \x001E    \x001F    \0    \x000D";
    private const string CzechProgrammersBarcode1 = "010477298543159410DdVcX<t\x001B1723020721yCH*4'h1Ab\x000D";
    private const string CzechProgrammersBarcode2 = "010477298543159410.GRs!qO\x001B1723072921TgIv?,6BmK\x000D";
    private const string CzechProgrammersBarcode3 = "010477298543159410(fpNxYi\x001B1723040521Z&Ur7>0oQS\x000D";
    private const string CzechProgrammersBarcode4 = "010477298543159410=8Fn_P\"\x001B1724100221Ew5;2/zaMJ\x000D";
    private const string CzechProgrammersBarcode5 = "0104772985431594103kuW)L9\x001B17240304215j4CltEc-Y\x000D";
    private const string CzechProgrammersBarcode6 = "010477298543159410:j\"%e+P\x001B172310312151itJzCguA\x000D";
    private const string CzechProgrammersBarcode7 = "010477298543159410bA1h'4*\x001B1723121421t<XcVdD0q!\x000D";
    private const string CzechProgrammersBarcode8 = "010477298543159410HCyKmB6\x001B1724062021sRG.iYxNpf\x000D";
    private const string CzechProgrammersBarcode9 = "010477298543159410,?vIgTS\x001B1724021821(\"P_nF8=9L\x000D";
    private const string CzechProgrammersBarcode10 = "010477298543159410Qo0>7rU\x001B1724070321)Wuk3P+e%\"\x000D";
    private const string CzechProgrammersBarcode11 = "010477298543159410&ZJMaz/\x001B1723112621\"j:AugCzJt\x000D";
    private const string CzechProgrammersBarcode12 = "0104772985431594102;5wEY-\x001B1723040421HIrQ9QeTyQ\x000D";
    private const string DutchBaseline = "  ! \0`% _ \0´) ' ( \0~, / . - 0 1 2 3 4 5 6 7 8 9 ± + ; ° : = A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" \0¨< * & @ \0^> | §    \0    \0    \0    \x001C    \0    \x000D";
    private const string DutchDeadKey1 = "\0`!\0``\0`#\0`$\0`%\0`_\0`´\0`)\0`'\0`(\0`~\0`,\0`/\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`±\0`+\0`;\0`°\0`:\0`=\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`¨\0`<\0`*\0`&\0`?\0`@\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`>\0`|\0`§\x000D";
    private const string DutchDeadKey2 = "\0´!\0´`\0´#\0´$\0´%\0´_\0´´\0´)\0´'\0´(\0´~\0´,\0´/\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´±\0´+\0´;\0´°\0´:\0´=\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´¨\0´<\0´*\0´&\0´?\0´@\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´>\0´|\0´§\x000D";
    private const string DutchDeadKey3 = "\0~!\0~`\0~#\0~$\0~%\0~_\0~´\0~)\0~'\0~(\0~~\0~,\0~/\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~±\0~+\0~;\0~°\0~:\0~=\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~¨\0~<\0~*\0~&\0~?\0~@\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~^\0~>\0~|\0~§\x000D";
    private const string DutchDeadKey4 = "\0¨!\0¨`\0¨#\0¨$\0¨%\0¨_\0¨´\0¨)\0¨'\0¨(\0¨~\0¨,\0¨/\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨±\0¨+\0¨;\0¨°\0¨:\0¨=\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨¨\0¨<\0¨*\0¨&\0¨?\0¨@\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨>\0¨|\0¨§\x000D";
    private const string DutchDeadKey5 = "\0^!\0^`\0^#\0^$\0^%\0^_\0^´\0^)\0^'\0^(\0^~\0^,\0^/\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^±\0^+\0^;\0^°\0^:\0^=\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^¨\0^<\0^*\0^&\0^?\0^@\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^>\0^|\0^§\x000D";
    private const string DutchBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4\0´h1Ab\x000D";
    private const string DutchBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv=,6BmK\x000D";
    private const string DutchBarcode3 = "010477298543159410)fpNxYi\01723040521Z_Ur7:0oQS\x000D";
    private const string DutchBarcode4 = "010477298543159410°8Fn?P\0\0`1724100221Ew5+2-zaMJ\x000D";
    private const string DutchBarcode5 = "0104772985431594103kuW'L9\017240304215j4CltEc/Y\x000D";
    private const string DutchBarcode6 = "010477298543159410±j\0`%e\0~P\0172310312151itJzCguA\x000D";
    private const string DutchBarcode7 = "010477298543159410bA1h\0´4(\01723121421t;XcVdD0q!\x000D";
    private const string DutchBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string DutchBarcode9 = "010477298543159410,=vIgTS\01724021821)\0`P?nF8°9L\x000D";
    private const string DutchBarcode10 = "010477298543159410Qo0:7rU\01724070321'Wuk3P\0~e%\0`";
    private const string DutchBarcode11 = "010477298543159410_ZJMaz-\01723112621\0`j±AugCzJt\x000D";
    private const string DutchBarcode12 = "0104772985431594102+5wEY/\01723040421HIrQ9QeTyQ\x000D";
    private const string EstonianBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" ü ' õ & \0ˇÜ * Õ \0~   \0    \0    \0    \0    \0    \x000D";
    private const string EstonianDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`ü\0`'\0`õ\0`&\0`?\0`ˇ\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Ü\0`*\0`Õ\0`~\x000D";
    private const string EstonianDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0´I\0´J\0´K\0´L\0´M\0Ń\0Ó\0´P\0´Q\0´R\0Ś\0´T\0´U\0´V\0´W\0´X\0´Y\0Ź\0´ü\0´'\0´õ\0´&\0´?\0´ˇ\0´a\0´b\0ć\0´d\0é\0´f\0´g\0´h\0´i\0´j\0´k\0´l\0´m\0ń\0ó\0´p\0´q\0´r\0ś\0´t\0´u\0´v\0´w\0´x\0´y\0ź\0´Ü\0´*\0´Õ\0´~\x000D";
    private const string EstonianDeadKey3 = "\0ˇ!\0ˇÄ\0ˇ#\0ˇ¤\0ˇ%\0ˇ/\0ˇä\0ˇ)\0ˇ=\0ˇ(\0ˇ`\0ˇ,\0ˇ+\0ˇ.\0ˇ-\0ˇ0\0ˇ1\0ˇ2\0ˇ3\0ˇ4\0ˇ5\0ˇ6\0ˇ7\0ˇ8\0ˇ9\0ˇÖ\0ˇö\0ˇ;\0ˇ´\0ˇ:\0ˇ_\0ˇ\"\0ˇA\0ˇB\0Č\0ˇD\0ˇE\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0ˇL\0ˇM\0ˇN\0ˇO\0ˇP\0ˇQ\0ˇR\0Š\0ˇT\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇü\0ˇ'\0ˇõ\0ˇ&\0ˇ?\0ˇˇ\0ˇa\0ˇb\0č\0ˇd\0ˇe\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ˇl\0ˇm\0ˇn\0ˇo\0ˇp\0ˇq\0ˇr\0š\0ˇt\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇÜ\0ˇ*\0ˇÕ\0ˇ~\x000D";
    private const string EstonianDeadKey4 = "\0~!\0~Ä\0~#\0~¤\0~%\0~/\0~ä\0~)\0~=\0~(\0~`\0~,\0~+\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ö\0~ö\0~;\0~´\0~:\0~_\0~\"\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~ü\0~'\0~õ\0~&\0~?\0~ˇ\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~Ü\0~*\0~Õ\0~~\x000D";
    private const string EstonianBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4äh1Ab\x000D";
    private const string EstonianBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string EstonianBarcode3 = "010477298543159410)fpNxYi\01723040521Z/Ur7:0oQS\x000D";
    private const string EstonianBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-zaMJ\x000D";
    private const string EstonianBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEc+Y\x000D";
    private const string EstonianBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJzCguA\x000D";
    private const string EstonianBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string EstonianBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string EstonianBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string EstonianBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string EstonianBarcode11 = "010477298543159410/ZJMaz-\01723112621ÄjÖAugCzJt\x000D";
    private const string EstonianBarcode12 = "0104772985431594102ö5wEY+\01723040421HIrQ9QeTyQ\x000D";
    private const string FinnishBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \x001D    \0    \0    \0    \0    \x000D";
    private const string FinnishDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string FinnishDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´½\x000D";
    private const string FinnishDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string FinnishDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string FinnishBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4äh1Ab\x000D";
    private const string FinnishBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string FinnishBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string FinnishBarcode4 = "010477298543159410\0´8Fn?PÄ\x001D1724100221Ew5ö2-zaMJ\x000D";
    private const string FinnishBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Y\x000D";
    private const string FinnishBarcode6 = "010477298543159410ÖjÄ%e\0`P\x001D172310312151itJzCguA\x000D";
    private const string FinnishBarcode7 = "010477298543159410bA1hä4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string FinnishBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string FinnishBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ÄP?nF8\0´9L\x000D";
    private const string FinnishBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Ä\x000D";
    private const string FinnishBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621ÄjÖAugCzJt\x000D";
    private const string FinnishBarcode12 = "0104772985431594102ö5wEY+\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string FinnishWithSamiBaseline = "  ! Ä % / ä ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& § Å * \0^½    \0    \0    \0    \0    \0    \x000D";
    private const string FinnishWithSamiDeadKey1 = "\0`!\0`Ä\0`#\0`¤\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`½\x000D";
    private const string FinnishWithSamiDeadKey2 = "\0´!\0´Ä\0´#\0´¤\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0Ẃ\0´X\0Ý\0Ź\0ǻ\0´'\0´¨\0´&\0´?\0´§\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0ẃ\0´x\0ý\0ź\0Ǻ\0´*\0´^\0´½\x000D";
    private const string FinnishWithSamiDeadKey3 = "\0¨!\0¨Ä\0¨#\0¨¤\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ö\0¨ö\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨½\x000D";
    private const string FinnishWithSamiDeadKey4 = "\0^!\0^Ä\0^#\0^¤\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^§\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^½\x000D";
    private const string FinnishWithSamiBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4äh1Ab\x000D";
    private const string FinnishWithSamiBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string FinnishWithSamiBarcode3 = "010477298543159410)fpNxYi\01723040521Z/Ur7:0oQS\x000D";
    private const string FinnishWithSamiBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-zaMJ\x000D";
    private const string FinnishWithSamiBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEc+Y\x000D";
    private const string FinnishWithSamiBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJzCguA\x000D";
    private const string FinnishWithSamiBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string FinnishWithSamiBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string FinnishWithSamiBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string FinnishWithSamiBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string FinnishWithSamiBarcode11 = "010477298543159410/ZJMaz-\01723112621ÄjÖAugCzJt\x000D";
    private const string FinnishWithSamiBarcode12 = "0104772985431594102ö5wEY+\01723040421HIrQ9QeTyQ\x000D";
    private const string GermanBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string GermanDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string GermanDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\x000D";
    private const string GermanDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string GermanBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4äh1Ab\x000D";
    private const string GermanBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string GermanBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string GermanBarcode4 = "010477298543159410\0´8Fn?PÄ\x001D1724100221Ew5ö2-yaMJ\x000D";
    private const string GermanBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEcßZ\x000D";
    private const string GermanBarcode6 = "010477298543159410ÖjÄ%e\0`P\x001D172310312151itJyCguA\x000D";
    private const string GermanBarcode7 = "010477298543159410bA1hä4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string GermanBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string GermanBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ÄP?nF8\0´9L\x000D";
    private const string GermanBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Ä\x000D";
    private const string GermanBarcode11 = "010477298543159410/YJMay-\x001D1723112621ÄjÖAugCyJt\x000D";
    private const string GermanBarcode12 = "0104772985431594102ö5wEZß\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string GermanIbmBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string GermanIbmDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string GermanIbmDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0´Z\0Ý\0´ü\0´#\0´+\0´&\0´?\0´^\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0´z\0ý\0´Ü\0´'\0´*\0´°\x000D";
    private const string GermanIbmDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string GermanIbmBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4äh1Ab\x000D";
    private const string GermanIbmBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string GermanIbmBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string GermanIbmBarcode4 = "010477298543159410\0´8Fn?PÄ\x001D1724100221Ew5ö2-yaMJ\x000D";
    private const string GermanIbmBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEcßZ\x000D";
    private const string GermanIbmBarcode6 = "010477298543159410ÖjÄ%e\0`P\x001D172310312151itJyCguA\x000D";
    private const string GermanIbmBarcode7 = "010477298543159410bA1hä4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string GermanIbmBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string GermanIbmBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ÄP?nF8\0´9L\x000D";
    private const string GermanIbmBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Ä\x000D";
    private const string GermanIbmBarcode11 = "010477298543159410/YJMay-\x001D1723112621ÄjÖAugCyJt\x000D";
    private const string GermanIbmBarcode12 = "0104772985431594102ö5wEZß\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string SwissGermanBaseline = "  + à % / ä ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 é ö ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" ü $ \0¨& § è £ ! °    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string SwissGermanDeadKey1 = "\0`+\0`à\0`*\0`ç\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`é\0`ö\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`è\0`£\0`!\0`°\x000D";
    private const string SwissGermanDeadKey2 = "\0^+\0^à\0^*\0^ç\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^é\0^ö\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^ü\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^è\0^£\0^!\0^°\x000D";
    private const string SwissGermanDeadKey3 = "\0¨+\0¨à\0¨*\0¨ç\0¨%\0¨/\0¨ä\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨é\0¨ö\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨ü\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨è\0¨£\0¨!\0¨°\x000D";
    private const string SwissGermanBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4äh1Ab\x000D";
    private const string SwissGermanBarcode2 = "010477298543159410.GRs+qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string SwissGermanBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string SwissGermanBarcode4 = "010477298543159410\0^8Fn?Pà\x001D1724100221Ew5ö2-yaMJ\x000D";
    private const string SwissGermanBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Z\x000D";
    private const string SwissGermanBarcode6 = "010477298543159410éjà%e\0`P\x001D172310312151itJyCguA\x000D";
    private const string SwissGermanBarcode7 = "010477298543159410bA1hä4(\x001D1723121421t;XcVdD0q+\x000D";
    private const string SwissGermanBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string SwissGermanBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)àP?nF8\0^9L\x000D";
    private const string SwissGermanBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%à\x000D";
    private const string SwissGermanBarcode11 = "010477298543159410/YJMay-\x001D1723112621àjéAugCyJt\x000D";
    private const string SwissGermanBarcode12 = "0104772985431594102ö5wEZ'\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string DanishBaseline = "  ! Ø % / ø ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Æ æ ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& ½ Å * \0^§    \x001D    \0    \0    \0    \0    \x000D";
    private const string DanishDeadKey1 = "\0`!\0`Ø\0`#\0`¤\0`%\0`/\0`ø\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Æ\0`æ\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`½\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string DanishDeadKey2 = "\0´!\0´Ø\0´#\0´¤\0´%\0´/\0´ø\0´)\0´=\0´(\0´`\0´,\0´+\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´å\0´'\0´¨\0´&\0´?\0´½\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Å\0´*\0´^\0´§\x000D";
    private const string DanishDeadKey3 = "\0¨!\0¨Ø\0¨#\0¨¤\0¨%\0¨/\0¨ø\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨´\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨½\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string DanishDeadKey4 = "\0^!\0^Ø\0^#\0^¤\0^%\0^/\0^ø\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Æ\0^æ\0^;\0^´\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^½\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string DanishBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4øh1Ab\x000D";
    private const string DanishBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string DanishBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string DanishBarcode4 = "010477298543159410\0´8Fn?PØ\x001D1724100221Ew5æ2-zaMJ\x000D";
    private const string DanishBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Y\x000D";
    private const string DanishBarcode6 = "010477298543159410ÆjØ%e\0`P\x001D172310312151itJzCguA\x000D";
    private const string DanishBarcode7 = "010477298543159410bA1hø4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string DanishBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string DanishBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ØP?nF8\0´9L\x000D";
    private const string DanishBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Ø\x000D";
    private const string DanishBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621ØjÆAugCzJt\x000D";
    private const string DanishBarcode12 = "0104772985431594102æ5wEY+\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string HungarianBaseline = "  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y Ü a b c d e f g h i j k l m n o p q r s t u v w x z y   + ! \" ő ű ú / 0 Ő Ű Ú §    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string HungarianBarcode1 = "ö1ö47729854315941öDdVcX?t\x001D1723ö2ö721zCH(4áh1Ab\x000D";
    private const string HungarianBarcode2 = "ö1ö47729854315941ö.GRs'qO\x001D1723ö72921TgIv_,6BmK\x000D";
    private const string HungarianBarcode3 = "ö1ö47729854315941ö)fpNxZi\x001D1723ö4ö521Y=Ur7:öoQS\x000D";
    private const string HungarianBarcode4 = "ö1ö47729854315941öó8FnÜPÁ\x001D17241öö221Ew5é2-yaMJ\x000D";
    private const string HungarianBarcode5 = "ö1ö47729854315941ö3kuWÖL9\x001D1724ö3ö4215j4CltEcüZ\x000D";
    private const string HungarianBarcode6 = "ö1ö47729854315941öÉjÁ%eÓP\x001D17231ö312151itJyCguA\x000D";
    private const string HungarianBarcode7 = "ö1ö47729854315941öbA1há4(\x001D1723121421t?XcVdDöq'\x000D";
    private const string HungarianBarcode8 = "ö1ö47729854315941öHCzKmB6\x001D1724ö62ö21sRG.iZxNpf\x000D";
    private const string HungarianBarcode9 = "ö1ö47729854315941ö,_vIgTS\x001D1724ö21821)ÁPÜnF8ó9L\x000D";
    private const string HungarianBarcode10 = "ö1ö47729854315941öQoö:7rU\x001D1724ö7ö321ÖWuk3PÓe%Á\x000D";
    private const string HungarianBarcode11 = "ö1ö47729854315941ö=YJMay-\x001D1723112621ÁjÉAugCyJt\x000D";
    private const string HungarianBarcode12 = "ö1ö47729854315941ö2é5wEZü\x001D1723ö4ö421HIrQ9QeTzQ\x000D";
    private const string Hungarian101KeyBaseline = "  ' Á % = á ) Ö ( Ó , ü . - ö 1 2 3 4 5 6 7 8 9 É é ? ó : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ü a b c d e f g h i j k l m n o p q r s t u v w x y z   + ! \" ő ű ú / í Ő Ű Ú Í    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string Hungarian101KeyBarcode1 = "ö1ö47729854315941öDdVcX?t\x001D1723ö2ö721yCH(4áh1Ab\x000D";
    private const string Hungarian101KeyBarcode2 = "ö1ö47729854315941ö.GRs'qO\x001D1723ö72921TgIv_,6BmK\x000D";
    private const string Hungarian101KeyBarcode3 = "ö1ö47729854315941ö)fpNxYi\x001D1723ö4ö521Z=Ur7:öoQS\x000D";
    private const string Hungarian101KeyBarcode4 = "ö1ö47729854315941öó8FnÜPÁ\x001D17241öö221Ew5é2-zaMJ\x000D";
    private const string Hungarian101KeyBarcode5 = "ö1ö47729854315941ö3kuWÖL9\x001D1724ö3ö4215j4CltEcüY\x000D";
    private const string Hungarian101KeyBarcode6 = "ö1ö47729854315941öÉjÁ%eÓP\x001D17231ö312151itJzCguA\x000D";
    private const string Hungarian101KeyBarcode7 = "ö1ö47729854315941öbA1há4(\x001D1723121421t?XcVdDöq'\x000D";
    private const string Hungarian101KeyBarcode8 = "ö1ö47729854315941öHCyKmB6\x001D1724ö62ö21sRG.iYxNpf\x000D";
    private const string Hungarian101KeyBarcode9 = "ö1ö47729854315941ö,_vIgTS\x001D1724ö21821)ÁPÜnF8ó9L\x000D";
    private const string Hungarian101KeyBarcode10 = "ö1ö47729854315941öQoö:7rU\x001D1724ö7ö321ÖWuk3PÓe%Á\x000D";
    private const string Hungarian101KeyBarcode11 = "ö1ö47729854315941ö=ZJMaz-\x001D1723112621ÁjÉAugCzJt\x000D";
    private const string Hungarian101KeyBarcode12 = "ö1ö47729854315941ö2é5wEYü\x001D1723ö4ö421HIrQ9QeTyQ\x000D";
    private const string IcelandicBaseline = "  ! ' % / \0´) = ( _ , ö . þ 0 1 2 3 4 5 6 7 8 9 Æ æ ; - : Þ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ö a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" ð + ' & \0°Ð * ? \0¨   \x001D    \0    \0    \0    \0    \x000D";
    private const string IcelandicDeadKey1 = "\0´!\0´'\0´#\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´_\0´,\0´ö\0´.\0´þ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Æ\0´æ\0´;\0´-\0´:\0´Þ\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´ð\0´+\0´'\0´&\0´Ö\0´°\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´Ð\0´*\0´?\0´¨\x000D";
    private const string IcelandicDeadKey2 = "\0°!\0°'\0°#\0°$\0°%\0°/\0°´\0°)\0°=\0°(\0°_\0°,\0°ö\0°.\0°þ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Æ\0°æ\0°;\0°-\0°:\0°Þ\0°\"\0Å\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0°Z\0°ð\0°+\0°'\0°&\0°Ö\0°°\0å\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0°z\0°Ð\0°*\0°?\0°¨\x000D";
    private const string IcelandicDeadKey3 = "\0¨!\0¨'\0¨#\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨_\0¨,\0¨ö\0¨.\0¨þ\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Æ\0¨æ\0¨;\0¨-\0¨:\0¨Þ\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨ð\0¨+\0¨'\0¨&\0¨Ö\0¨°\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Ð\0¨*\0¨?\0¨¨\x000D";
    private const string IcelandicBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4\0´h1Ab\x000D";
    private const string IcelandicBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIvÞ,6BmK\x000D";
    private const string IcelandicBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string IcelandicBarcode4 = "010477298543159410-8FnÖP'\x001D1724100221Ew5æ2þzaMJ\x000D";
    private const string IcelandicBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEcöY\x000D";
    private const string IcelandicBarcode6 = "010477298543159410Æj'%e_P\x001D172310312151itJzCguA\x000D";
    private const string IcelandicBarcode7 = "010477298543159410bA1h\0´4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string IcelandicBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string IcelandicBarcode9 = "010477298543159410,ÞvIgTS\x001D1724021821)'PÖnF8-9L\x000D";
    private const string IcelandicBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P_e%'\x000D";
    private const string IcelandicBarcode11 = "010477298543159410/ZJMazþ\x001D1723112621'jÆAugCzJt\x000D";
    private const string IcelandicBarcode12 = "0104772985431594102æ5wEYö\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string IrishBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string IrishDeadKey1 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0`¬\x000D";
    private const string IrishBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string IrishBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string IrishBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string IrishBarcode4 = "010477298543159410=8Fn_P@\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string IrishBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string IrishBarcode6 = "010477298543159410:j@%e+P\x001D172310312151itJzCguA\x000D";
    private const string IrishBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string IrishBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string IrishBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(@P_nF8=9L\x000D";
    private const string IrishBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%@\x000D";
    private const string IrishBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621@j:AugCzJt\x000D";
    private const string IrishBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string ItalianBaseline = "  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string ItalianBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4àh1Ab\x000D";
    private const string ItalianBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string ItalianBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string ItalianBarcode4 = "010477298543159410ì8Fn?P°\x001D1724100221Ew5ò2-zaMJ\x000D";
    private const string ItalianBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Y\x000D";
    private const string ItalianBarcode6 = "010477298543159410çj°%e^P\x001D172310312151itJzCguA\x000D";
    private const string ItalianBarcode7 = "010477298543159410bA1hà4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string ItalianBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string ItalianBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)°P?nF8ì9L\x000D";
    private const string ItalianBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P^e%°\x000D";
    private const string ItalianBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621°jçAugCzJt\x000D";
    private const string ItalianBarcode12 = "0104772985431594102ò5wEY'\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string Italian142Baseline = "  ! ° % / à ) = ( ^ , ' . - 0 1 2 3 4 5 6 7 8 9 ç ò ; ì : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" è ù + & \\ é § * |    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string Italian142Barcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4àh1Ab\x000D";
    private const string Italian142Barcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string Italian142Barcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string Italian142Barcode4 = "010477298543159410ì8Fn?P°\x001D1724100221Ew5ò2-zaMJ\x000D";
    private const string Italian142Barcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Y\x000D";
    private const string Italian142Barcode6 = "010477298543159410çj°%e^P\x001D172310312151itJzCguA\x000D";
    private const string Italian142Barcode7 = "010477298543159410bA1hà4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string Italian142Barcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string Italian142Barcode9 = "010477298543159410,_vIgTS\x001D1724021821)°P?nF8ì9L\x000D";
    private const string Italian142Barcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P^e%°\x000D";
    private const string Italian142Barcode11 = "010477298543159410/ZJMaz-\x001D1723112621°jçAugCzJt\x000D";
    private const string Italian142Barcode12 = "0104772985431594102ò5wEY'\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string LatvianStandardBaseline = "  ! \0\"% & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string LatvianStandardDeadKey1 = "\0\"!\0\"\"\0\"#\0§\0°\0±\0\"'\0\"(\0\")\0×\0\"+\0\",\0—\0\".\0\"/\0\"0\0\"1\0\"2\0\"3\0§\0°\0\"6\0±\0×\0\"9\0\":\0\";\0\"<\0\"=\0\">\0\"?\0\"@\0Ā\0\"B\0Č\0\"D\0Ē\0\"F\0Ģ\0\"H\0Ī\0\"J\0Ķ\0Ļ\0\"M\0Ņ\0Ō\0\"P\0\"Q\0Ŗ\0Š\0\"T\0Ū\0\"V\0\"W\0\"X\0\"Y\0Ž\0\"[\0\"\\\0\"]\0\"^\0—\0\"`\0Ā\0\"b\0Č\0\"d\0Ē\0\"f\0Ģ\0\"h\0Ī\0\"j\0Ķ\0Ļ\0\"m\0Ņ\0Ō\0\"p\0\"q\0Ŗ\0Š\0\"t\0Ū\0\"v\0\"w\0\"x\0\"y\0Ž\0\"{\0\"|\0\"}\0\"~\x000D";
    private const string LatvianQwertyBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \0°] ^ ` { | } \0~   \x001D    \x001C    \0    \0    \0    \x000D";
    private const string LatvianQwertyDeadKey1 = "\0°!\0°\"\0°#\0°$\0°%\0°&\0°'\0°(\0°)\0°*\0°+\0°,\0°-\0°.\0°/\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°:\0°;\0°<\0°=\0°>\0°?\0°@\0Å\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0°U\0°V\0°W\0°X\0°Y\0Ż\0°[\0°°\0°]\0°^\0°_\0°`\0å\0°b\0°c\0°d\0ė\0°f\0ġ\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0°u\0°v\0°w\0°x\0°y\0ż\0°{\0°|\0°}\0°~\x000D";
    private const string LatvianQwertyDeadKey2 = "\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0~A\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0~N\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~[\0~°\0~]\0~^\0~_\0~`\0~a\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0~n\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~{\0~|\0~}\0~~\x000D";
    private const string LatvianQwertyBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string LatvianQwertyBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string LatvianQwertyBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string LatvianQwertyBarcode4 = "010477298543159410=8Fn_P\"\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string LatvianQwertyBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string LatvianQwertyBarcode6 = "010477298543159410:j\"%e+P\x001D172310312151itJzCguA\x000D";
    private const string LatvianQwertyBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string LatvianQwertyBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string LatvianQwertyBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(\"P_nF8=9L\x000D";
    private const string LatvianQwertyBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%\"\x000D";
    private const string LatvianQwertyBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621\"j:AugCzJt\x000D";
    private const string LatvianQwertyBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string LatvianBaseline = "  ! \0°% & \0´( ) × F , - . ļ 0 1 2 3 4 5 6 7 8 9 C c ; f : Ļ Š P Ī S J I L D Z A T E Ā O Ē Č Ū R U M N K G B V Ņ _ š p ī s j i l d z a t e ā o ē č ū r u m n k g b v ņ   » $ « ž ķ h / ­ Ž Ķ H ?    \x0008    \x001C    \0    \0    \0    \x000D";
    private const string LatvianDeadKey1 = "\0°!\0°°\0°»\0°$\0°%\0°&\0°´\0°(\0°)\0°×\0°F\0°,\0°-\0°.\0°ļ\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°C\0°c\0°;\0°f\0°:\0°Ļ\0°«\0°Š\0°P\0°Ī\0°S\0°J\0°I\0°L\0°D\0Ż\0Å\0°T\0Ė\0°Ā\0°O\0°Ē\0°Č\0°Ū\0°R\0°U\0°M\0°N\0°K\0°G\0°B\0°V\0°Ņ\0°ž\0°ķ\0°h\0°/\0°_\0°­\0°š\0°p\0°ī\0°s\0°j\0°i\0°l\0°d\0ż\0å\0°t\0ė\0°ā\0°o\0°ē\0°č\0°ū\0°r\0°u\0°m\0°n\0°k\0ġ\0°b\0°v\0°ņ\0°Ž\0°Ķ\0°H\0°?\x000D";
    private const string LatvianDeadKey2 = "\0´!\0´°\0´»\0´$\0´%\0´&\0´´\0´(\0´)\0´×\0´F\0´,\0´-\0´.\0´ļ\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0Ć\0ć\0´;\0´f\0´:\0´Ļ\0´«\0´Š\0´P\0´Ī\0Ś\0´J\0´I\0´L\0´D\0Ź\0´A\0´T\0É\0´Ā\0Ó\0´Ē\0´Č\0´Ū\0´R\0´U\0´M\0Ń\0´K\0´G\0´B\0´V\0´Ņ\0´ž\0´ķ\0´h\0´/\0´_\0´­\0´š\0´p\0´ī\0ś\0´j\0´i\0´l\0´d\0ź\0´a\0´t\0é\0´ā\0ó\0´ē\0´č\0´ū\0´r\0´u\0´m\0ń\0´k\0´g\0´b\0´v\0´ņ\0´Ž\0´Ķ\0´H\0´?\x000D";
    private const string LatvianBarcode1 = "010477298543159410SsKīB;m\x00081723020721vĪD×4\0´d1Šp\x000D";
    private const string LatvianBarcode2 = "010477298543159410.LRu!ūĒ\x00081723072921MlZkĻ,6PāT\x000D";
    private const string LatvianBarcode3 = "010477298543159410(ičObVz\x00081723040521Ņ&Nr7:0ēŪU\x000D";
    private const string LatvianBarcode4 = "010477298543159410f8Io_Č\0\x0008°1724100221Jg5c2ļņšĀA\x000D";
    private const string LatvianBarcode5 = "0104772985431594103tnG)E9\x000817240304215a4ĪemJī-V\x000D";
    private const string LatvianBarcode6 = "010477298543159410Ca\0°%jFČ\x0008172310312151zmAņĪlnŠ\x000D";
    private const string LatvianBarcode7 = "010477298543159410pŠ1d\0´4×\x00081723121421m;BīKsS0ū!\x000D";
    private const string LatvianBarcode8 = "010477298543159410DĪvTāP6\x00081724062021uRL.zVbOči\x000D";
    private const string LatvianBarcode9 = "010477298543159410,ĻkZlMU\x00081724021821(\0°Č_oI8f9E\x000D";
    private const string LatvianBarcode10 = "010477298543159410Ūē0:7rN\x00081724070321)Gnt3ČFj%\0°";
    private const string LatvianBarcode11 = "010477298543159410&ŅAĀšņļ\x00081723112621\0åCŠnlĪņAm\x000D";
    private const string LatvianBarcode12 = "0104772985431594102c5gJV-\x00081723040421DZrŪ9ŪjMvŪ\x000D";
    private const string LithuanianBaseline = "  Ą \" Į Ų ' ( ) Ū Ž , - . / 0 ą č ę ė į š ų ū 9 : ; < ž > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   Ę Ė Č [ \\ ] Š ` { | } ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string LithuanianBarcode1 = "0ą0ėųųč9ūįėęąį9ėą0DdVcX<t\x001Dąųčę0č0ųčąyCHŪė'hąAb\x000D";
    private const string LithuanianBarcode2 = "0ą0ėųųč9ūįėęąį9ėą0.GRsĄqO\x001Dąųčę0ųč9čąTgIv?,šBmK\x000D";
    private const string LithuanianBarcode3 = "0ą0ėųųč9ūįėęąį9ėą0(fpNxYi\x001Dąųčę0ė0įčąZŲUrų>0oQS\x000D";
    private const string LithuanianBarcode4 = "0ą0ėųųč9ūįėęąį9ėą0žūFn_P\"\x001Dąųčėą00ččąEwį;č/zaMJ\x000D";
    private const string LithuanianBarcode5 = "0ą0ėųųč9ūįėęąį9ėą0ękuW)L9\x001Dąųčė0ę0ėčąįjėCltEc-Y\x000D";
    private const string LithuanianBarcode6 = "0ą0ėųųč9ūįėęąį9ėą0:j\"ĮeŽP\x001Dąųčęą0ęąčąįąitJzCguA\x000D";
    private const string LithuanianBarcode7 = "0ą0ėųųč9ūįėęąį9ėą0bAąh'ėŪ\x001Dąųčęąčąėčąt<XcVdD0qĄ\x000D";
    private const string LithuanianBarcode8 = "0ą0ėųųč9ūįėęąį9ėą0HCyKmBš\x001Dąųčė0šč0čąsRG.iYxNpf\x000D";
    private const string LithuanianBarcode9 = "0ą0ėųųč9ūįėęąį9ėą0,?vIgTS\x001Dąųčė0čąūčą(\"P_nFūž9L\x000D";
    private const string LithuanianBarcode10 = "0ą0ėųųč9ūįėęąį9ėą0Qo0>ųrU\x001Dąųčė0ų0ęčą)WukęPŽeĮ\"\x000D";
    private const string LithuanianBarcode11 = "0ą0ėųųč9ūįėęąį9ėą0ŲZJMaz/\x001Dąųčęąąčščą\"j:AugCzJt\x000D";
    private const string LithuanianBarcode12 = "0ą0ėųųč9ūįėęąį9ėą0č;įwEY-\x001Dąųčę0ė0ėčąHIrQ9QeTyQ\x000D";
    private const string LithuanianIbmBaseline = "  1 Ė 5 7 ė 9 0 8 = č _ š ę ) ! \" / ; : , . ? ( Ų ų Č + Š Ę A B C D E F G H I J K L M N O P Ą R S T U V Ž Ū Y Z - a b c d e f g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į | “ 6 ` Į \\ ” ~    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string LithuanianIbmBarcode1 = ")!);..\"(?:;/!:(;!)DdVcŪČt\x001D!.\"/)\").\"!yCH8;ėh!Ab\x000D";
    private const string LithuanianIbmBarcode2 = ")!);..\"(?:;/!:(;!)šGRs1ąO\x001D!.\"/).\"(\"!TgIvĘč,BmK\x000D";
    private const string LithuanianIbmBarcode3 = ")!);..\"(?:;/!:(;!)9fpNūYi\x001D!.\"/);):\"!Z7Ur.Š)oĄS\x000D";
    private const string LithuanianIbmBarcode4 = ")!);..\"(?:;/!:(;!)+?Fn-PĖ\x001D!.\";!))\"\"!Ež:ų\"ęzaMJ\x000D";
    private const string LithuanianIbmBarcode5 = ")!);..\"(?:;/!:(;!)/kuŽ0L(\x001D!.\";)/);\"!:j;CltEc_Y\x000D";
    private const string LithuanianIbmBarcode6 = ")!);..\"(?:;/!:(;!)ŲjĖ5e=P\x001D!.\"/!)/!\"!:!itJzCguA\x000D";
    private const string LithuanianIbmBarcode7 = ")!);..\"(?:;/!:(;!)bA!hė;8\x001D!.\"/!\"!;\"!tČŪcVdD)ą1\x000D";
    private const string LithuanianIbmBarcode8 = ")!);..\"(?:;/!:(;!)HCyKmB,\x001D!.\";),\")\"!sRGšiYūNpf\x000D";
    private const string LithuanianIbmBarcode9 = ")!);..\"(?:;/!:(;!)čĘvIgTS\x001D!.\";)\"!?\"!9ĖP-nF?+(L\x000D";
    private const string LithuanianIbmBarcode10 = ")!);..\"(?:;/!:(;!)Ąo)Š.rU\x001D!.\";).)/\"!0Žuk/P=e5Ė\x000D";
    private const string LithuanianIbmBarcode11 = ")!);..\"(?:;/!:(;!)7ZJMazę\x001D!.\"/!!\",\"!ĖjŲAugCzJt\x000D";
    private const string LithuanianIbmBarcode12 = ")!);..\"(?:;/!:(;!)\"ų:žEY_\x001D!.\"/););\"!HIrĄ(ĄeTyĄ\x000D";
    private const string LithuanianStandardBaseline = "  1 Ė 5 7 ė 9 0 8 X č ? f ę ) ! - / ; : , . = ( Ų ų Č x F Ę A B C D E Š G H I J K L M N O P Ą R S T U V Ž Ū Y Z + a b c d e š g h i j k l m n o p ą r s t u v ž ū y z   3 4 2 į q w 6 ` Į Q W ~    \0    \0    \0    \0    \0    \x000D";
    private const string LithuanianStandardBarcode1 = ")!);..-(=:;/!:(;!)DdVcŪČt\0!.-/)-).-!yCH8;ėh!Ab\x000D";
    private const string LithuanianStandardBarcode2 = ")!);..-(=:;/!:(;!)fGRs1ąO\0!.-/).-(-!TgIvĘč,BmK\x000D";
    private const string LithuanianStandardBarcode3 = ")!);..-(=:;/!:(;!)9špNūYi\0!.-/);):-!Z7Ur.F)oĄS\x000D";
    private const string LithuanianStandardBarcode4 = ")!);..-(=:;/!:(;!)x=Šn+PĖ\0!.-;!))--!Ež:ų-ęzaMJ\x000D";
    private const string LithuanianStandardBarcode5 = ")!);..-(=:;/!:(;!)/kuŽ0L(\0!.-;)/);-!:j;CltEc?Y\x000D";
    private const string LithuanianStandardBarcode6 = ")!);..-(=:;/!:(;!)ŲjĖ5eXP\0!.-/!)/!-!:!itJzCguA\x000D";
    private const string LithuanianStandardBarcode7 = ")!);..-(=:;/!:(;!)bA!hė;8\0!.-/!-!;-!tČŪcVdD)ą1\x000D";
    private const string LithuanianStandardBarcode8 = ")!);..-(=:;/!:(;!)HCyKmB,\0!.-;),-)-!sRGfiYūNpš\x000D";
    private const string LithuanianStandardBarcode9 = ")!);..-(=:;/!:(;!)čĘvIgTS\0!.-;)-!=-!9ĖP+nŠ=x(L\x000D";
    private const string LithuanianStandardBarcode10 = ")!);..-(=:;/!:(;!)Ąo)F.rU\0!.-;).)/-!0Žuk/PXe5Ė\x000D";
    private const string LithuanianStandardBarcode11 = ")!);..-(=:;/!:(;!)7ZJMazę\0!.-/!!-,-!ĖjŲAugCzJt\x000D";
    private const string LithuanianStandardBarcode12 = ")!);..-(=:;/!:(;!)-ų:žEY?\0!.-/););-!HIrĄ(ĄeTyĄ\x000D";
    private const string SorbianBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \0    \0    \0    \0    \0    \x000D";
    private const string SorbianDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string SorbianDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0´E\0´F\0´G\0´H\0´I\0´J\0´K\0Ł\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0´U\0´V\0´W\0´X\0Ź\0´Y\0´ü\0´#\0´+\0´&\0´?\0´^\0´a\0´b\0ć\0´d\0´e\0´f\0´g\0´h\0´i\0´j\0´k\0ł\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0´u\0´v\0´w\0´x\0ź\0´y\0´Ü\0´'\0´*\0´°\x000D";
    private const string SorbianDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0^D\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0^L\0^M\0^N\0^O\0^P\0^Q\0Ř\0Š\0^T\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0^a\0^b\0č\0^d\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0^l\0^m\0^n\0^o\0^p\0^q\0ř\0š\0^t\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string SorbianBarcode1 = "010477298543159410DdVcX;t\01723020721zCH(4äh1Ab\x000D";
    private const string SorbianBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string SorbianBarcode3 = "010477298543159410)fpNxZi\01723040521Y/Ur7:0oQS\x000D";
    private const string SorbianBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-yaMJ\x000D";
    private const string SorbianBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEcßZ\x000D";
    private const string SorbianBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJyCguA\x000D";
    private const string SorbianBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string SorbianBarcode8 = "010477298543159410HCzKmB6\01724062021sRG.iZxNpf\x000D";
    private const string SorbianBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string SorbianBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string SorbianBarcode11 = "010477298543159410/YJMay-\01723112621ÄjÖAugCyJt\x000D";
    private const string SorbianBarcode12 = "0104772985431594102ö5wEZß\01723040421HIrQ9QeTzQ\x000D";
    private const string NorwegianWithSamiBaseline = "  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \0    \0    \0    \0    \0    \x000D";
    private const string NorwegianWithSamiDeadKey1 = "\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string NorwegianWithSamiDeadKey2 = "\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0Ẅ\0¨X\0Ÿ\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0ẅ\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string NorwegianWithSamiDeadKey3 = "\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0Ĉ\0^D\0Ê\0^F\0Ĝ\0Ĥ\0Î\0Ĵ\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0Ŝ\0^T\0Û\0^V\0Ŵ\0^X\0Ŷ\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0ĉ\0^d\0ê\0^f\0ĝ\0ĥ\0î\0ĵ\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0ŝ\0^t\0û\0^v\0ŵ\0^x\0ŷ\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string NorwegianWithSamiBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4æh1Ab\x000D";
    private const string NorwegianWithSamiBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string NorwegianWithSamiBarcode3 = "010477298543159410)fpNxYi\01723040521Z/Ur7:0oQS\x000D";
    private const string NorwegianWithSamiBarcode4 = "010477298543159410\\8Fn?PÆ\01724100221Ew5ø2-zaMJ\x000D";
    private const string NorwegianWithSamiBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEc+Y\x000D";
    private const string NorwegianWithSamiBarcode6 = "010477298543159410ØjÆ%e\0`P\0172310312151itJzCguA\x000D";
    private const string NorwegianWithSamiBarcode7 = "010477298543159410bA1hæ4(\01723121421t;XcVdD0q!\x000D";
    private const string NorwegianWithSamiBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string NorwegianWithSamiBarcode9 = "010477298543159410,_vIgTS\01724021821)ÆP?nF8\\9L\x000D";
    private const string NorwegianWithSamiBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Æ\x000D";
    private const string NorwegianWithSamiBarcode11 = "010477298543159410/ZJMaz-\01723112621ÆjØAugCzJt\x000D";
    private const string NorwegianWithSamiBarcode12 = "0104772985431594102ø5wEY+\01723040421HIrQ9QeTyQ\x000D";
    private const string LuxembourgishBaseline = "  + ä % / à ) = ( \0`, ' . - 0 1 2 3 4 5 6 7 8 9 ö é ; \0^: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   * ç \" è $ \0\"& § ü £ ! °    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string LuxembourgishDeadKey1 = "\0`+\0`ä\0`*\0`ç\0`%\0`/\0`à\0`)\0`=\0`(\0``\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`ö\0`é\0`;\0`^\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`è\0`$\0`¨\0`&\0`?\0`§\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`ü\0`£\0`!\0`°\x000D";
    private const string LuxembourgishDeadKey2 = "\0^+\0^ä\0^*\0^ç\0^%\0^/\0^à\0^)\0^=\0^(\0^`\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^ö\0^é\0^;\0^^\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Z\0^Y\0^è\0^$\0^¨\0^&\0^?\0^§\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^z\0^y\0^ü\0^£\0^!\0^°\x000D";
    private const string LuxembourgishDeadKey3 = "\0¨+\0¨ä\0¨*\0¨ç\0¨%\0¨/\0¨à\0¨)\0¨=\0¨(\0¨`\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨ö\0¨é\0¨;\0¨^\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨è\0¨$\0¨¨\0¨&\0¨?\0¨§\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0ÿ\0¨ü\0¨£\0¨!\0¨°\x000D";
    private const string LuxembourgishBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4àh1Ab\x000D";
    private const string LuxembourgishBarcode2 = "010477298543159410.GRs+qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string LuxembourgishBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string LuxembourgishBarcode4 = "010477298543159410\0^8Fn?Pä\x001D1724100221Ew5é2-yaMJ\x000D";
    private const string LuxembourgishBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Z\x000D";
    private const string LuxembourgishBarcode6 = "010477298543159410öjä%e\0`P\x001D172310312151itJyCguA\x000D";
    private const string LuxembourgishBarcode7 = "010477298543159410bA1hà4(\x001D1723121421t;XcVdD0q+\x000D";
    private const string LuxembourgishBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string LuxembourgishBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)äP?nF8\0^9L\x000D";
    private const string LuxembourgishBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%ä\x000D";
    private const string LuxembourgishBarcode11 = "010477298543159410/YJMay-\x001D1723112621äjöAugCyJt\x000D";
    private const string LuxembourgishBarcode12 = "0104772985431594102é5wEZ'\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string NorwegianBaseline = "  ! Æ % / æ ) = ( \0`, + . - 0 1 2 3 4 5 6 7 8 9 Ø ø ; \\ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # ¤ \" å ' \0¨& | Å * \0^§    \x001D    \0    \0    \0    \0    \x000D";
    private const string NorwegianDeadKey1 = "\0`!\0`Æ\0`#\0`¤\0`%\0`/\0`æ\0`)\0`=\0`(\0``\0`,\0`+\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ø\0`ø\0`;\0`\\\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`å\0`'\0`¨\0`&\0`?\0`|\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`Å\0`*\0`^\0`§\x000D";
    private const string NorwegianDeadKey2 = "\0¨!\0¨Æ\0¨#\0¨¤\0¨%\0¨/\0¨æ\0¨)\0¨=\0¨(\0¨`\0¨,\0¨+\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ø\0¨ø\0¨;\0¨\\\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨å\0¨'\0¨¨\0¨&\0¨?\0¨|\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨Å\0¨*\0¨^\0¨§\x000D";
    private const string NorwegianDeadKey3 = "\0^!\0^Æ\0^#\0^¤\0^%\0^/\0^æ\0^)\0^=\0^(\0^`\0^,\0^+\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ø\0^ø\0^;\0^\\\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^å\0^'\0^¨\0^&\0^?\0^|\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^Å\0^*\0^^\0^§\x000D";
    private const string NorwegianBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4æh1Ab\x000D";
    private const string NorwegianBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string NorwegianBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string NorwegianBarcode4 = "010477298543159410\\8Fn?PÆ\x001D1724100221Ew5ø2-zaMJ\x000D";
    private const string NorwegianBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Y\x000D";
    private const string NorwegianBarcode6 = "010477298543159410ØjÆ%e\0`P\x001D172310312151itJzCguA\x000D";
    private const string NorwegianBarcode7 = "010477298543159410bA1hæ4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string NorwegianBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string NorwegianBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ÆP?nF8\\9L\x000D";
    private const string NorwegianBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P\0è%Æ\x000D";
    private const string NorwegianBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621ÆjØAugCzJt\x000D";
    private const string NorwegianBarcode12 = "0104772985431594102ø5wEY+\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string Maltese47KeyBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ @ ġ ż ħ ^ ċ Ġ Ż Ħ Ċ    \0    \0    \0    \0    \0    \x000D";
    private const string Maltese47KeyBarcode1 = "010477298543159410DdVcX<t\01723020721yCH*4'h1Ab\x000D";
    private const string Maltese47KeyBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv?,6BmK\x000D";
    private const string Maltese47KeyBarcode3 = "010477298543159410(fpNxYi\01723040521Z&Ur7>0oQS\x000D";
    private const string Maltese47KeyBarcode4 = "010477298543159410=8Fn_P\"\01724100221Ew5;2/zaMJ\x000D";
    private const string Maltese47KeyBarcode5 = "0104772985431594103kuW)L9\017240304215j4CltEc-Y\x000D";
    private const string Maltese47KeyBarcode6 = "010477298543159410:j\"%e+P\0172310312151itJzCguA\x000D";
    private const string Maltese47KeyBarcode7 = "010477298543159410bA1h'4*\01723121421t<XcVdD0q!\x000D";
    private const string Maltese47KeyBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string Maltese47KeyBarcode9 = "010477298543159410,?vIgTS\01724021821(\"P_nF8=9L\x000D";
    private const string Maltese47KeyBarcode10 = "010477298543159410Qo0>7rU\01724070321)Wuk3P+e%\"\x000D";
    private const string Maltese47KeyBarcode11 = "010477298543159410&ZJMaz/\01723112621\"j:AugCzJt\x000D";
    private const string Maltese47KeyBarcode12 = "0104772985431594102;5wEY-\01723040421HIrQ9QeTyQ\x000D";
    private const string Maltese48KeyBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   € $ \" ġ # ħ ^ ċ Ġ ~ Ħ Ċ    \0    \0    \0    \0    \0    \x000D";
    private const string Maltese48KeyBarcode1 = "010477298543159410DdVcX<t\01723020721yCH*4'h1Ab\x000D";
    private const string Maltese48KeyBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv?,6BmK\x000D";
    private const string Maltese48KeyBarcode3 = "010477298543159410(fpNxYi\01723040521Z&Ur7>0oQS\x000D";
    private const string Maltese48KeyBarcode4 = "010477298543159410=8Fn_P@\01724100221Ew5;2/zaMJ\x000D";
    private const string Maltese48KeyBarcode5 = "0104772985431594103kuW)L9\017240304215j4CltEc-Y\x000D";
    private const string Maltese48KeyBarcode6 = "010477298543159410:j@%e+P\0172310312151itJzCguA\x000D";
    private const string Maltese48KeyBarcode7 = "010477298543159410bA1h'4*\01723121421t<XcVdD0q!\x000D";
    private const string Maltese48KeyBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string Maltese48KeyBarcode9 = "010477298543159410,?vIgTS\01724021821(@P_nF8=9L\x000D";
    private const string Maltese48KeyBarcode10 = "010477298543159410Qo0>7rU\01724070321)Wuk3P+e%@\x000D";
    private const string Maltese48KeyBarcode11 = "010477298543159410&ZJMaz/\01723112621@j:AugCzJt\x000D";
    private const string Maltese48KeyBarcode12 = "0104772985431594102;5wEY-\01723040421HIrQ9QeTyQ\x000D";
    private const string PolishProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } \0~   \x001D    \x001C    \0    \x001F    \0    \x000D";
    private const string PolishProgrammersDeadKey1 = "\0~!\0~\"\0~#\0~$\0~%\0~&\0~'\0~(\0~)\0~*\0~+\0~,\0~-\0~.\0~/\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~:\0~;\0~<\0~=\0~>\0~?\0~@\0Ą\0~B\0Ć\0~D\0Ę\0~F\0~G\0~H\0~I\0~J\0~K\0Ł\0~M\0Ń\0Ó\0~P\0~Q\0~R\0Ś\0~T\0~U\0~V\0~W\0Ź\0~Y\0Ż\0~[\0~\\\0~]\0~^\0~_\0~`\0ą\0~b\0ć\0~d\0ę\0~f\0~g\0~h\0~i\0~j\0~k\0ł\0~m\0ń\0ó\0~p\0~q\0~r\0ś\0~t\0~u\0~v\0~w\0ź\0~y\0ż\0~{\0~|\0~}\0~~\x000D";
    private const string PolishProgrammersBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string PolishProgrammersBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string PolishProgrammersBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string PolishProgrammersBarcode4 = "010477298543159410=8Fn_P\"\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string PolishProgrammersBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string PolishProgrammersBarcode6 = "010477298543159410:j\"%e+P\x001D172310312151itJzCguA\x000D";
    private const string PolishProgrammersBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string PolishProgrammersBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string PolishProgrammersBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(\"P_nF8=9L\x000D";
    private const string PolishProgrammersBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%\"\x000D";
    private const string PolishProgrammersBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621\"j:AugCzJt\x000D";
    private const string PolishProgrammersBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string Polish214Baseline = "  ! ę % / ą ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ł ł ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ż ó ś & \0˛ń ź ć \0·   \x001D    \x001C    \0    \0    \0    \x000D";
    private const string Polish214DeadKey1 = "\0˛!\0˛ę\0˛#\0˛¤\0˛%\0˛/\0˛ą\0˛)\0˛=\0˛(\0˛*\0˛,\0˛+\0˛.\0˛-\0˛0\0˛1\0˛2\0˛3\0˛4\0˛5\0˛6\0˛7\0˛8\0˛9\0˛Ł\0˛ł\0˛;\0˛'\0˛:\0˛_\0˛\"\0Ą\0˛B\0˛C\0˛D\0Ę\0˛F\0˛G\0˛H\0˛I\0˛J\0˛K\0˛L\0˛M\0˛N\0˛O\0˛P\0˛Q\0˛R\0˛S\0˛T\0˛U\0˛V\0˛W\0˛X\0˛Z\0˛Y\0˛ż\0˛ó\0˛ś\0˛&\0˛?\0˛˛\0ą\0˛b\0˛c\0˛d\0ę\0˛f\0˛g\0˛h\0˛i\0˛j\0˛k\0˛l\0˛m\0˛n\0˛o\0˛p\0˛q\0˛r\0˛s\0˛t\0˛u\0˛v\0˛w\0˛x\0˛z\0˛y\0˛ń\0˛ź\0˛ć\0˛·\x000D";
    private const string Polish214DeadKey2 = "\0·!\0·ę\0·#\0·¤\0·%\0·/\0·ą\0·)\0·=\0·(\0·*\0·,\0·+\0·.\0·-\0·0\0·1\0·2\0·3\0·4\0·5\0·6\0·7\0·8\0·9\0·Ł\0·ł\0·;\0·'\0·:\0·_\0·\"\0·A\0·B\0·C\0·D\0·E\0·F\0·G\0·H\0·I\0·J\0·K\0·L\0·M\0·N\0·O\0·P\0·Q\0·R\0·S\0·T\0·U\0·V\0·W\0·X\0Ż\0·Y\0·ż\0·ó\0·ś\0·&\0·?\0·˛\0·a\0·b\0·c\0·d\0·e\0·f\0·g\0·h\0·i\0·j\0·k\0·l\0·m\0·n\0·o\0·p\0·q\0·r\0·s\0·t\0·u\0·v\0·w\0·x\0ż\0·y\0·ń\0·ź\0·ć\0··\x000D";
    private const string Polish214Barcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4ąh1Ab\x000D";
    private const string Polish214Barcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string Polish214Barcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string Polish214Barcode4 = "010477298543159410'8Fn?Pę\x001D1724100221Ew5ł2-yaMJ\x000D";
    private const string Polish214Barcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Z\x000D";
    private const string Polish214Barcode6 = "010477298543159410Łję%e*P\x001D172310312151itJyCguA\x000D";
    private const string Polish214Barcode7 = "010477298543159410bA1hą4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string Polish214Barcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string Polish214Barcode9 = "010477298543159410,_vIgTS\x001D1724021821)ęP?nF8'9L\x000D";
    private const string Polish214Barcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P*e%ę\x000D";
    private const string Polish214Barcode11 = "010477298543159410/YJMay-\x001D1723112621ęjŁAugCyJt\x000D";
    private const string Polish214Barcode12 = "0104772985431594102ł5wEZ+\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string PortugueseBaseline = "  ! ª % / º ) = ( » , ' . - 0 1 2 3 4 5 6 7 8 9 Ç ç ; « : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ \" + \0~\0´& \\ * \0^\0`|    \0    \0    \0    \0    \0    \x000D";
    private const string PortugueseDeadKey1 = "\0~!\0~ª\0~#\0~$\0~%\0~/\0~º\0~)\0~=\0~(\0~»\0~,\0~'\0~.\0~-\0~0\0~1\0~2\0~3\0~4\0~5\0~6\0~7\0~8\0~9\0~Ç\0~ç\0~;\0~«\0~:\0~_\0~\"\0Ã\0~B\0~C\0~D\0~E\0~F\0~G\0~H\0~I\0~J\0~K\0~L\0~M\0Ñ\0Õ\0~P\0~Q\0~R\0~S\0~T\0~U\0~V\0~W\0~X\0~Y\0~Z\0~+\0~~\0~´\0~&\0~?\0~\\\0ã\0~b\0~c\0~d\0~e\0~f\0~g\0~h\0~i\0~j\0~k\0~l\0~m\0ñ\0õ\0~p\0~q\0~r\0~s\0~t\0~u\0~v\0~w\0~x\0~y\0~z\0~*\0~^\0~`\0~|\x000D";
    private const string PortugueseDeadKey2 = "\0´!\0´ª\0´#\0´$\0´%\0´/\0´º\0´)\0´=\0´(\0´»\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ç\0´ç\0´;\0´«\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´+\0´~\0´´\0´&\0´?\0´\\\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´*\0´^\0´`\0´|\x000D";
    private const string PortugueseDeadKey3 = "\0^!\0^ª\0^#\0^$\0^%\0^/\0^º\0^)\0^=\0^(\0^»\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ç\0^ç\0^;\0^«\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^+\0^~\0^´\0^&\0^?\0^\\\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^*\0^^\0^`\0^|\x000D";
    private const string PortugueseDeadKey4 = "\0`!\0`ª\0`#\0`$\0`%\0`/\0`º\0`)\0`=\0`(\0`»\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ç\0`ç\0`;\0`«\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`+\0`~\0`´\0`&\0`?\0`\\\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`*\0`^\0``\0`|\x000D";
    private const string PortugueseBarcode1 = "010477298543159410DdVcX;t\01723020721yCH(4ºh1Ab\x000D";
    private const string PortugueseBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string PortugueseBarcode3 = "010477298543159410)fpNxYi\01723040521Z/Ur7:0oQS\x000D";
    private const string PortugueseBarcode4 = "010477298543159410«8Fn?Pª\01724100221Ew5ç2-zaMJ\x000D";
    private const string PortugueseBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEc'Y\x000D";
    private const string PortugueseBarcode6 = "010477298543159410Çjª%e»P\0172310312151itJzCguA\x000D";
    private const string PortugueseBarcode7 = "010477298543159410bA1hº4(\01723121421t;XcVdD0q!\x000D";
    private const string PortugueseBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string PortugueseBarcode9 = "010477298543159410,_vIgTS\01724021821)ªP?nF8«9L\x000D";
    private const string PortugueseBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P»e%ª\x000D";
    private const string PortugueseBarcode11 = "010477298543159410/ZJMaz-\01723112621ªjÇAugCzJt\x000D";
    private const string PortugueseBarcode12 = "0104772985431594102ç5wEY'\01723040421HIrQ9QeTyQ\x000D";
    private const string RomanianStandardBaseline = "  ! Ț % & ț ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 Ș ș ; = : ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ ă â î ^ „ Ă Â Î ”    \0    \0    \0    \0    \0    \x000D";
    private const string RomanianStandardBarcode1 = "010477298543159410DdVcX;t\01723020721yCH*4țh1Ab\x000D";
    private const string RomanianStandardBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv?,6BmK\x000D";
    private const string RomanianStandardBarcode3 = "010477298543159410(fpNxYi\01723040521Z&Ur7:0oQS\x000D";
    private const string RomanianStandardBarcode4 = "010477298543159410=8Fn_PȚ\01724100221Ew5ș2/zaMJ\x000D";
    private const string RomanianStandardBarcode5 = "0104772985431594103kuW)L9\017240304215j4CltEc-Y\x000D";
    private const string RomanianStandardBarcode6 = "010477298543159410ȘjȚ%e+P\0172310312151itJzCguA\x000D";
    private const string RomanianStandardBarcode7 = "010477298543159410bA1hț4*\01723121421t;XcVdD0q!\x000D";
    private const string RomanianStandardBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string RomanianStandardBarcode9 = "010477298543159410,?vIgTS\01724021821(ȚP_nF8=9L\x000D";
    private const string RomanianStandardBarcode10 = "010477298543159410Qo0:7rU\01724070321)Wuk3P+e%Ț\x000D";
    private const string RomanianStandardBarcode11 = "010477298543159410&ZJMaz/\01723112621ȚjȘAugCzJt\x000D";
    private const string RomanianStandardBarcode12 = "0104772985431594102ș5wEY-\01723040421HIrQ9QeTyQ\x000D";
    private const string RomanianLegacyBaseline = "  ! Ţ % / ţ ) = ( * , + . - 0 1 2 3 4 5 6 7 8 9 Ş ş ; ' : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # ¤ \" ă â î & ] Ă Â Î [    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string RomanianLegacyBarcode1 = "010477298543159410DdVcX;t\x001D1723020721zCH(4ţh1Ab\x000D";
    private const string RomanianLegacyBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string RomanianLegacyBarcode3 = "010477298543159410)fpNxZi\x001D1723040521Y/Ur7:0oQS\x000D";
    private const string RomanianLegacyBarcode4 = "010477298543159410'8Fn?PŢ\x001D1724100221Ew5ş2-yaMJ\x000D";
    private const string RomanianLegacyBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc+Z\x000D";
    private const string RomanianLegacyBarcode6 = "010477298543159410ŞjŢ%e*P\x001D172310312151itJyCguA\x000D";
    private const string RomanianLegacyBarcode7 = "010477298543159410bA1hţ4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string RomanianLegacyBarcode8 = "010477298543159410HCzKmB6\x001D1724062021sRG.iZxNpf\x000D";
    private const string RomanianLegacyBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)ŢP?nF8'9L\x000D";
    private const string RomanianLegacyBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P*e%Ţ\x000D";
    private const string RomanianLegacyBarcode11 = "010477298543159410/YJMay-\x001D1723112621ŢjŞAugCyJt\x000D";
    private const string RomanianLegacyBarcode12 = "0104772985431594102ş5wEZ+\x001D1723040421HIrQ9QeTzQ\x000D";
    private const string RomanianProgrammersBaseline = "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    \0    \0    \0    \0    \0    \x000D";
    private const string RomanianProgrammersBarcode1 = "010477298543159410DdVcX<t\01723020721yCH*4'h1Ab\x000D";
    private const string RomanianProgrammersBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv?,6BmK\x000D";
    private const string RomanianProgrammersBarcode3 = "010477298543159410(fpNxYi\01723040521Z&Ur7>0oQS\x000D";
    private const string RomanianProgrammersBarcode4 = "010477298543159410=8Fn_P\"\01724100221Ew5;2/zaMJ\x000D";
    private const string RomanianProgrammersBarcode5 = "0104772985431594103kuW)L9\017240304215j4CltEc-Y\x000D";
    private const string RomanianProgrammersBarcode6 = "010477298543159410:j\"%e+P\0172310312151itJzCguA\x000D";
    private const string RomanianProgrammersBarcode7 = "010477298543159410bA1h'4*\01723121421t<XcVdD0q!\x000D";
    private const string RomanianProgrammersBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string RomanianProgrammersBarcode9 = "010477298543159410,?vIgTS\01724021821(\"P_nF8=9L\x000D";
    private const string RomanianProgrammersBarcode10 = "010477298543159410Qo0>7rU\01724070321)Wuk3P+e%\"\x000D";
    private const string RomanianProgrammersBarcode11 = "010477298543159410&ZJMaz/\01723112621\"j:AugCzJt\x000D";
    private const string RomanianProgrammersBarcode12 = "0104772985431594102;5wEY-\01723040421HIrQ9QeTyQ\x000D";
    private const string ScottishGaelicBaseline = "  ! @ % & \0'( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } `    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string ScottishGaelicDeadKey1 = "\0'!\0'@\0'£\0'$\0'%\0'&\0''\0'(\0')\0'*\0'+\0',\0'-\0'.\0'/\0'0\0'1\0'2\0'3\0'4\0'5\0'6\0'7\0'8\0'9\0':\0';\0'<\0'=\0'>\0'?\0'\"\0Á\0'B\0'C\0'D\0É\0'F\0'G\0'H\0Í\0'J\0'K\0'L\0'M\0'N\0Ó\0'P\0'Q\0'R\0'S\0'T\0Ú\0'V\0'W\0'X\0Ý\0'Z\0'[\0'#\0']\0'^\0'_\0'`\0á\0'b\0'c\0'd\0é\0'f\0'g\0'h\0í\0'j\0'k\0'l\0'm\0'n\0ó\0'p\0'q\0'r\0's\0't\0ú\0'v\0'w\0'x\0ý\0'z\0'{\0'~\0'}\0'`\x000D";
    private const string ScottishGaelicDeadKey2 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`{\0`~\0`}\0``\x000D";
    private const string ScottishGaelicBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4\0'h1Ab\x000D";
    private const string ScottishGaelicBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string ScottishGaelicBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string ScottishGaelicBarcode4 = "010477298543159410=8Fn_P@\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string ScottishGaelicBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string ScottishGaelicBarcode6 = "010477298543159410:j@%e+P\x001D172310312151itJzCguA\x000D";
    private const string ScottishGaelicBarcode7 = "010477298543159410bA1h\0'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string ScottishGaelicBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string ScottishGaelicBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(@P_nF8=9L\x000D";
    private const string ScottishGaelicBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%@\x000D";
    private const string ScottishGaelicBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621@j:AugCzJt\x000D";
    private const string ScottishGaelicBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string SlovakBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y % a b c d e f g h i j k l m n o p q r s t u v w x z y   3 4 2 ú ň ä 6 ; / ) ( \0°   \0    \x001C    \0    \0    \0    \x000D";
    private const string SlovakDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0Ž\0ˇY\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ž\0ˇy\0ˇ/\0ˇ)\0ˇ(\0ˇ°\x000D";
    private const string SlovakQwertyBaseline = "  1 ! 5 7 § 9 0 8 \0ˇ, = . - é + ľ š č ť ž ý á í \" ô ? \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z % a b c d e f g h i j k l m n o p q r s t u v w x y z   3 4 2 ú ň ä 6 ; / ) ( \0°   \x001B    \x001C    \x001E    \0    \0    \x000D";
    private const string SlovakQwertyDeadKey1 = "\0ˇ1\0ˇ!\0ˇ3\0ˇ4\0ˇ5\0ˇ7\0ˇ§\0ˇ9\0ˇ0\0ˇ8\0ˇˇ\0ˇ,\0ˇ=\0ˇ.\0ˇ-\0ˇé\0ˇ+\0ˇľ\0ˇš\0ˇč\0ˇť\0ˇž\0ˇý\0ˇá\0ˇí\0ˇ\"\0ˇô\0ˇ?\0ˇ´\0ˇ:\0ˇ_\0ˇ2\0ˇA\0ˇB\0Č\0Ď\0Ě\0ˇF\0ˇG\0ˇH\0ˇI\0ˇJ\0ˇK\0Ľ\0ˇM\0Ň\0ˇO\0ˇP\0ˇQ\0Ř\0Š\0Ť\0ˇU\0ˇV\0ˇW\0ˇX\0ˇY\0Ž\0ˇú\0ˇň\0ˇä\0ˇ6\0ˇ%\0ˇ;\0ˇa\0ˇb\0č\0ď\0ě\0ˇf\0ˇg\0ˇh\0ˇi\0ˇj\0ˇk\0ľ\0ˇm\0ň\0ˇo\0ˇp\0ˇq\0ř\0š\0ť\0ˇu\0ˇv\0ˇw\0ˇx\0ˇy\0ž\0ˇ/\0ˇ)\0ˇ(\0ˇ°\x000D";
    private const string SlovakQwertyDeadKey2 = "\0´1\0´!\0´3\0´4\0´5\0´7\0´§\0´9\0´0\0´8\0´ˇ\0´,\0´=\0´.\0´-\0´é\0´+\0´ľ\0´š\0´č\0´ť\0´ž\0´ý\0´á\0´í\0´\"\0´ô\0´?\0´´\0´:\0´_\0´2\0Á\0´B\0Ć\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ý\0Ź\0´ú\0´ň\0´ä\0´6\0´%\0´;\0á\0´b\0ć\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ý\0ź\0´/\0´)\0´(\0´°\x000D";
    private const string SlovakQwertyDeadKey3 = "\0°1\0°!\0°3\0°4\0°5\0°7\0°§\0°9\0°0\0°8\0°ˇ\0°,\0°=\0°.\0°-\0°é\0°+\0°ľ\0°š\0°č\0°ť\0°ž\0°ý\0°á\0°í\0°\"\0°ô\0°?\0°´\0°:\0°_\0°2\0°A\0°B\0°C\0°D\0°E\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0°Y\0°Z\0°ú\0°ň\0°ä\0°6\0°%\0°;\0°a\0°b\0°c\0°d\0°e\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0°s\0°t\0ů\0°v\0°w\0°x\0°y\0°z\0°/\0°)\0°(\0°°\x000D";
    private const string SlovakQwertyBarcode1 = "é+éčýýľíáťčš+ťíč+éDdVcX?t\x001B+ýľšéľéýľ+yCH8č§h+Ab\x000D";
    private const string SlovakQwertyBarcode2 = "é+éčýýľíáťčš+ťíč+é.GRs1qO\x001B+ýľšéýľíľ+TgIv_,žBmK\x000D";
    private const string SlovakQwertyBarcode3 = "é+éčýýľíáťčš+ťíč+é9fpNxYi\x001B+ýľšéčéťľ+Z7Urý:éoQS\x000D";
    private const string SlovakQwertyBarcode4 = "é+éčýýľíáťčš+ťíč+é\0´áFn%P!\x001B+ýľč+ééľľ+Ewťôľ-zaMJ\x000D";
    private const string SlovakQwertyBarcode5 = "é+éčýýľíáťčš+ťíč+éškuW0Lí\x001B+ýľčéšéčľ+ťjčCltEc=Y\x000D";
    private const string SlovakQwertyBarcode6 = "é+éčýýľíáťčš+ťíč+é\"j!5e\0ˇP\x001B+ýľš+éš+ľ+ť+itJzCguA\x000D";
    private const string SlovakQwertyBarcode7 = "é+éčýýľíáťčš+ťíč+ébA+h§č8\x001B+ýľš+ľ+čľ+t?XcVdDéq1\x000D";
    private const string SlovakQwertyBarcode8 = "é+éčýýľíáťčš+ťíč+éHCyKmBž\x001B+ýľčéžľéľ+sRG.iYxNpf\x000D";
    private const string SlovakQwertyBarcode9 = "é+éčýýľíáťčš+ťíč+é,_vIgTS\x001B+ýľčéľ+áľ+9!P%nFá\0´íL\x000D";
    private const string SlovakQwertyBarcode10 = "é+éčýýľíáťčš+ťíč+éQoé:ýrU\x001B+ýľčéýéšľ+0WukšP\0ě5!\x000D";
    private const string SlovakQwertyBarcode11 = "é+éčýýľíáťčš+ťíč+é7ZJMaz-\x001B+ýľš++ľžľ+!j\"AugCzJt\x000D";
    private const string SlovakQwertyBarcode12 = "é+éčýýľíáťčš+ťíč+éľôťwEY=\x001B+ýľšéčéčľ+HIrQíQeTyQ\x000D";
    private const string SlovenianBaseline = "  ! Ć % / ć ) = ( * , ' . - 0 1 2 3 4 5 6 7 8 9 Č č ; + : _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   # $ \" š ž đ & \0¸Š Ž Đ \0¨   \x001B    \x001C    \0    \0    \0    \x000D";
    private const string SlovenianDeadKey1 = "\0¸!\0¸Ć\0¸#\0¸$\0¸%\0¸/\0¸ć\0¸)\0¸=\0¸(\0¸*\0¸,\0¸'\0¸.\0¸-\0¸0\0¸1\0¸2\0¸3\0¸4\0¸5\0¸6\0¸7\0¸8\0¸9\0¸Č\0¸č\0¸;\0¸+\0¸:\0¸_\0¸\"\0¸A\0¸B\0Ç\0¸D\0¸E\0¸F\0¸G\0¸H\0¸I\0¸J\0¸K\0¸L\0¸M\0¸N\0¸O\0¸P\0¸Q\0¸R\0Ş\0¸T\0¸U\0¸V\0¸W\0¸X\0¸Z\0¸Y\0¸š\0¸ž\0¸đ\0¸&\0¸?\0¸¸\0¸a\0¸b\0ç\0¸d\0¸e\0¸f\0¸g\0¸h\0¸i\0¸j\0¸k\0¸l\0¸m\0¸n\0¸o\0¸p\0¸q\0¸r\0ş\0¸t\0¸u\0¸v\0¸w\0¸x\0¸z\0¸y\0¸Š\0¸Ž\0¸Đ\0¸¨\x000D";
    private const string SlovenianDeadKey2 = "\0¨!\0¨Ć\0¨#\0¨$\0¨%\0¨/\0¨ć\0¨)\0¨=\0¨(\0¨*\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Č\0¨č\0¨;\0¨+\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0¨I\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Z\0¨Y\0¨š\0¨ž\0¨đ\0¨&\0¨?\0¨¸\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0¨i\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0¨z\0¨y\0¨Š\0¨Ž\0¨Đ\0¨¨\x000D";
    private const string SlovenianBarcode1 = "010477298543159410DdVcX;t\x001B1723020721zCH(4ćh1Ab\x000D";
    private const string SlovenianBarcode2 = "010477298543159410.GRs!qO\x001B1723072921TgIv_,6BmK\x000D";
    private const string SlovenianBarcode3 = "010477298543159410)fpNxZi\x001B1723040521Y/Ur7:0oQS\x000D";
    private const string SlovenianBarcode4 = "010477298543159410+8Fn?PĆ\x001B1724100221Ew5č2-yaMJ\x000D";
    private const string SlovenianBarcode5 = "0104772985431594103kuW=L9\x001B17240304215j4CltEc'Z\x000D";
    private const string SlovenianBarcode6 = "010477298543159410ČjĆ%e*P\x001B172310312151itJyCguA\x000D";
    private const string SlovenianBarcode7 = "010477298543159410bA1hć4(\x001B1723121421t;XcVdD0q!\x000D";
    private const string SlovenianBarcode8 = "010477298543159410HCzKmB6\x001B1724062021sRG.iZxNpf\x000D";
    private const string SlovenianBarcode9 = "010477298543159410,_vIgTS\x001B1724021821)ĆP?nF8+9L\x000D";
    private const string SlovenianBarcode10 = "010477298543159410Qo0:7rU\x001B1724070321=Wuk3P*e%Ć\x000D";
    private const string SlovenianBarcode11 = "010477298543159410/YJMay-\x001B1723112621ĆjČAugCyJt\x000D";
    private const string SlovenianBarcode12 = "0104772985431594102č5wEZ'\x001B1723040421HIrQ9QeTzQ\x000D";
    private const string SpanishBaseline = "  ! \0¨% / \0´) = ( ¿ , ' . - 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; ¡ : _ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ? a b c d e f g h i j k l m n o p q r s t u v w x y z   · $ \" \0`ç + & º \0^Ç * ª    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string SpanishDeadKey1 = "\0¨!\0¨¨\0¨·\0¨$\0¨%\0¨/\0¨´\0¨)\0¨=\0¨(\0¨¿\0¨,\0¨'\0¨.\0¨-\0¨0\0¨1\0¨2\0¨3\0¨4\0¨5\0¨6\0¨7\0¨8\0¨9\0¨Ñ\0¨ñ\0¨;\0¨¡\0¨:\0¨_\0¨\"\0Ä\0¨B\0¨C\0¨D\0Ë\0¨F\0¨G\0¨H\0Ï\0¨J\0¨K\0¨L\0¨M\0¨N\0Ö\0¨P\0¨Q\0¨R\0¨S\0¨T\0Ü\0¨V\0¨W\0¨X\0¨Y\0¨Z\0¨`\0¨ç\0¨+\0¨&\0¨?\0¨º\0ä\0¨b\0¨c\0¨d\0ë\0¨f\0¨g\0¨h\0ï\0¨j\0¨k\0¨l\0¨m\0¨n\0ö\0¨p\0¨q\0¨r\0¨s\0¨t\0ü\0¨v\0¨w\0¨x\0ÿ\0¨z\0¨^\0¨Ç\0¨*\0¨ª\x000D";
    private const string SpanishDeadKey2 = "\0´!\0´¨\0´·\0´$\0´%\0´/\0´´\0´)\0´=\0´(\0´¿\0´,\0´'\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ñ\0´ñ\0´;\0´¡\0´:\0´_\0´\"\0Á\0´B\0´C\0´D\0É\0´F\0´G\0´H\0Í\0´J\0´K\0´L\0´M\0´N\0Ó\0´P\0´Q\0´R\0´S\0´T\0Ú\0´V\0´W\0´X\0Ý\0´Z\0´`\0´ç\0´+\0´&\0´?\0´º\0á\0´b\0´c\0´d\0é\0´f\0´g\0´h\0í\0´j\0´k\0´l\0´m\0´n\0ó\0´p\0´q\0´r\0´s\0´t\0ú\0´v\0´w\0´x\0ý\0´z\0´^\0´Ç\0´*\0´ª\x000D";
    private const string SpanishDeadKey3 = "\0`!\0`¨\0`·\0`$\0`%\0`/\0`´\0`)\0`=\0`(\0`¿\0`,\0`'\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ñ\0`ñ\0`;\0`¡\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Y\0`Z\0``\0`ç\0`+\0`&\0`?\0`º\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`y\0`z\0`^\0`Ç\0`*\0`ª\x000D";
    private const string SpanishDeadKey4 = "\0^!\0^¨\0^·\0^$\0^%\0^/\0^´\0^)\0^=\0^(\0^¿\0^,\0^'\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ñ\0^ñ\0^;\0^¡\0^:\0^_\0^\"\0Â\0^B\0^C\0^D\0Ê\0^F\0^G\0^H\0Î\0^J\0^K\0^L\0^M\0^N\0Ô\0^P\0^Q\0^R\0^S\0^T\0Û\0^V\0^W\0^X\0^Y\0^Z\0^`\0^ç\0^+\0^&\0^?\0^º\0â\0^b\0^c\0^d\0ê\0^f\0^g\0^h\0î\0^j\0^k\0^l\0^m\0^n\0ô\0^p\0^q\0^r\0^s\0^t\0û\0^v\0^w\0^x\0^y\0^z\0^^\0^Ç\0^*\0^ª\x000D";
    private const string SpanishBarcode1 = "010477298543159410DdVcX;t\x001D1723020721yCH(4\0´h1Ab\x000D";
    private const string SpanishBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv_,6BmK\x000D";
    private const string SpanishBarcode3 = "010477298543159410)fpNxYi\x001D1723040521Z/Ur7:0oQS\x000D";
    private const string SpanishBarcode4 = "010477298543159410¡8Fn?P\0¨\x001D1724100221Ew5ñ2-zaMJ\x000D";
    private const string SpanishBarcode5 = "0104772985431594103kuW=L9\x001D17240304215j4CltEc'Y\x000D";
    private const string SpanishBarcode6 = "010477298543159410Ñj\0¨%e¿P\x001D172310312151itJzCguA\x000D";
    private const string SpanishBarcode7 = "010477298543159410bA1h\0´4(\x001D1723121421t;XcVdD0q!\x000D";
    private const string SpanishBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string SpanishBarcode9 = "010477298543159410,_vIgTS\x001D1724021821)\0¨P?nF8¡9L\x000D";
    private const string SpanishBarcode10 = "010477298543159410Qo0:7rU\x001D1724070321=Wuk3P¿e%\0¨";
    private const string SpanishBarcode11 = "010477298543159410/ZJMaz-\x001D1723112621\0¨jÑAugCzJt\x000D";
    private const string SpanishBarcode12 = "0104772985431594102ñ5wEY'\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string SpanishVariationBaseline = "  ª Ç ) ! ç ? ₧ ¿ \0¨, - . = 0 1 2 3 4 5 6 7 8 9 Ñ ñ ; \0¨: % A B C D E F G H I J K L M N O P Q R S T U V W X Y Z + a b c d e f g h i j k l m n o p q r s t u v w x y z   / ( \" ÷ \0´\0`¡ ' × \0´\0`·    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string SorbianStandardBaseline = "  ! Ä % / ä ) = ( \0`, ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü # + & \0^Ü ' * °    \0    \0    \0    \0    \x000D";
    private const string SorbianStandardDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`#\0`+\0`&\0`?\0`^\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`'\0`*\0`°\x000D";
    private const string SorbianStandardDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0´A\0´B\0Ć\0´D\0´E\0´F\0´G\0´H\0´I\0´J\0´K\0Ł\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0´U\0´V\0´W\0´X\0Ź\0´Y\0´ü\0´#\0´+\0´&\0´?\0´^\0´a\0´b\0ć\0´d\0´e\0´f\0´g\0´h\0´i\0´j\0´k\0ł\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0´u\0´v\0´w\0´x\0ź\0´y\0´Ü\0´'\0´*\0´°\x000D";
    private const string SorbianStandardDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0^D\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0^L\0^M\0^N\0^O\0^P\0^Q\0Ř\0Š\0^T\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^#\0^+\0^&\0^?\0^^\0^a\0^b\0č\0^d\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0^l\0^m\0^n\0^o\0^p\0^q\0ř\0š\0^t\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^'\0^*\0^°\x000D";
    private const string SorbianStandardBarcode1 = "010477298543159410DdVcX;t\01723020721zCH(4äh1Ab\x000D";
    private const string SorbianStandardBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string SorbianStandardBarcode3 = "010477298543159410)fpNxZi\01723040521Y/Ur7:0oQS\x000D";
    private const string SorbianStandardBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-yaMJ\x000D";
    private const string SorbianStandardBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEcßZ\x000D";
    private const string SorbianStandardBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJyCguA\x000D";
    private const string SorbianStandardBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string SorbianStandardBarcode8 = "010477298543159410HCzKmB6\01724062021sRG.iZxNpf\x000D";
    private const string SorbianStandardBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string SorbianStandardBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string SorbianStandardBarcode11 = "010477298543159410/YJMay-\01723112621ÄjÖAugCyJt\x000D";
    private const string SorbianStandardBarcode12 = "0104772985431594102ö5wEZß\01723040421HIrQ9QeTzQ\x000D";
    private const string SorbianExtendedBaseline = "  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0ˇÜ Ł * \0˙   \0    \0    \0    \0    \0    \x000D";
    private const string SorbianExtendedDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0Ą\0`B\0Ç\0`D\0Ę\0`F\0`G\0`H\0`I\0`J\0`K\0`L\0`M\0`N\0Ő\0`P\0`Q\0`R\0Ş\0`T\0Ű\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0ą\0`b\0ç\0`d\0ę\0`f\0`g\0`h\0`i\0`j\0`k\0`l\0`m\0`n\0ő\0`p\0`q\0`r\0ş\0`t\0ű\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\x000D";
    private const string SorbianExtendedDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\x000D";
    private const string SorbianExtendedDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0Ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\x000D";
    private const string SorbianExtendedDeadKey4 = "\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\x000D";
    private const string SorbianExtendedBarcode1 = "010477298543159410DdVcX;t\01723020721zCH(4äh1Ab\x000D";
    private const string SorbianExtendedBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string SorbianExtendedBarcode3 = "010477298543159410)fpNxZi\01723040521Y/Ur7:0oQS\x000D";
    private const string SorbianExtendedBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-yaMJ\x000D";
    private const string SorbianExtendedBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEcßZ\x000D";
    private const string SorbianExtendedBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJyCguA\x000D";
    private const string SorbianExtendedBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string SorbianExtendedBarcode8 = "010477298543159410HCzKmB6\01724062021sRG.iZxNpf\x000D";
    private const string SorbianExtendedBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string SorbianExtendedBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0ę%Ä\x000D";
    private const string SorbianExtendedBarcode11 = "010477298543159410/YJMay-\01723112621ÄjÖAugCyJt\x000D";
    private const string SorbianExtendedBarcode12 = "0104772985431594102ö5wEZß\01723040421HIrQ9QeTzQ\x000D";
    private const string SorbianStandardLegacyBaseline = "  ! Ä % / ä ) = ( \0', ß . - 0 1 2 3 4 5 6 7 8 9 Ö ö ; \0´: _ A B C D E F G H I J K L M N O P Q R S T U V W X Z Y ? a b c d e f g h i j k l m n o p q r s t u v w x z y   § $ \" ü ł + & \0^Ü Ł * \0̇   \0    \0    \0    \0    \0    \x000D";
    private const string SorbianStandardLegacyDeadKey1 = "\0`!\0`Ä\0`§\0`$\0`%\0`/\0`ä\0`)\0`=\0`(\0``\0`,\0`ß\0`.\0`-\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`Ö\0`ö\0`;\0`´\0`:\0`_\0`\"\0À\0`B\0Ç\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0Ş\0`T\0Ù\0`V\0`W\0`X\0`Z\0`Y\0`ü\0`ł\0`+\0`&\0`?\0`^\0à\0`b\0ç\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0ş\0`t\0ù\0`v\0`w\0`x\0`z\0`y\0`Ü\0`Ł\0`*\0`°\x000D";
    private const string SorbianStandardLegacyDeadKey2 = "\0´!\0´Ä\0´§\0´$\0´%\0´/\0´ä\0´)\0´=\0´(\0´`\0´,\0´ß\0´.\0´-\0´0\0´1\0´2\0´3\0´4\0´5\0´6\0´7\0´8\0´9\0´Ö\0´ö\0´;\0´´\0´:\0´_\0´\"\0Á\0´B\0Ć\0Đ\0É\0´F\0´G\0´H\0Í\0´J\0´K\0Ĺ\0´M\0Ń\0Ó\0´P\0´Q\0Ŕ\0Ś\0´T\0Ú\0´V\0´W\0´X\0Ź\0Ý\0´ü\0´ł\0´+\0´&\0´?\0´^\0á\0´b\0ć\0đ\0é\0´f\0´g\0´h\0í\0´j\0´k\0ĺ\0´m\0ń\0ó\0´p\0´q\0ŕ\0ś\0´t\0ú\0´v\0´w\0´x\0ź\0ý\0´Ü\0´Ł\0´*\0´°\x000D";
    private const string SorbianStandardLegacyDeadKey3 = "\0^!\0^Ä\0^§\0^$\0^%\0^/\0^ä\0^)\0^=\0^(\0^`\0^,\0^ß\0^.\0^-\0^0\0^1\0^2\0^3\0^4\0^5\0^6\0^7\0^8\0^9\0^Ö\0^ö\0^;\0^´\0^:\0^_\0^\"\0^A\0^B\0Č\0Ď\0Ě\0^F\0^G\0^H\0^I\0^J\0^K\0Ľ\0^M\0Ň\0ô\0^P\0^Q\0Ř\0Š\0Ť\0^U\0^V\0^W\0^X\0Ž\0^Y\0^ü\0^ł\0^+\0^&\0^?\0^^\0^a\0^b\0č\0ď\0ě\0^f\0^g\0^h\0^i\0^j\0^k\0ľ\0^m\0ň\0Ô\0^p\0^q\0ř\0š\0ť\0^u\0^v\0^w\0^x\0ž\0^y\0^Ü\0^Ł\0^*\0^°\x000D";
    private const string SorbianStandardLegacyDeadKey4 = "\0°!\0°Ä\0°§\0°$\0°%\0°/\0°ä\0°)\0°=\0°(\0°`\0°,\0°ß\0°.\0°-\0°0\0°1\0°2\0°3\0°4\0°5\0°6\0°7\0°8\0°9\0°Ö\0°ö\0°;\0°´\0°:\0°_\0°\"\0°A\0°B\0°C\0°D\0Ė\0°F\0°G\0°H\0°I\0°J\0°K\0°L\0°M\0°N\0°O\0°P\0°Q\0°R\0°S\0°T\0Ů\0°V\0°W\0°X\0Ż\0°Y\0°ü\0°ł\0°+\0°&\0°?\0°^\0°a\0°b\0°c\0°d\0ė\0°f\0°g\0°h\0°i\0°j\0°k\0°l\0°m\0°n\0°o\0°p\0°q\0°r\0ſ\0°t\0ů\0°v\0°w\0°x\0ż\0°y\0°Ü\0°Ł\0°*\0°°\x000D";
    private const string SorbianStandardLegacyBarcode1 = "010477298543159410DdVcX;t\01723020721zCH(4äh1Ab\x000D";
    private const string SorbianStandardLegacyBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv_,6BmK\x000D";
    private const string SorbianStandardLegacyBarcode3 = "010477298543159410)fpNxZi\01723040521Y/Ur7:0oQS\x000D";
    private const string SorbianStandardLegacyBarcode4 = "010477298543159410\0´8Fn?PÄ\01724100221Ew5ö2-yaMJ\x000D";
    private const string SorbianStandardLegacyBarcode5 = "0104772985431594103kuW=L9\017240304215j4CltEcßZ\x000D";
    private const string SorbianStandardLegacyBarcode6 = "010477298543159410ÖjÄ%e\0`P\0172310312151itJyCguA\x000D";
    private const string SorbianStandardLegacyBarcode7 = "010477298543159410bA1hä4(\01723121421t;XcVdD0q!\x000D";
    private const string SorbianStandardLegacyBarcode8 = "010477298543159410HCzKmB6\01724062021sRG.iZxNpf\x000D";
    private const string SorbianStandardLegacyBarcode9 = "010477298543159410,_vIgTS\01724021821)ÄP?nF8\0´9L\x000D";
    private const string SorbianStandardLegacyBarcode10 = "010477298543159410Qo0:7rU\01724070321=Wuk3P\0è%Ä\x000D";
    private const string SorbianStandardLegacyBarcode11 = "010477298543159410/YJMay-\01723112621ÄjÖAugCyJt\x000D";
    private const string SorbianStandardLegacyBarcode12 = "0104772985431594102ö5wEZß\01723040421HIrQ9QeTzQ\x000D";
    private const string UnitedKingdomBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    \x001D    \x001C    \0    \0    \0    \x000D";
    private const string UnitedKingdomBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string UnitedKingdomBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string UnitedKingdomBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string UnitedKingdomBarcode4 = "010477298543159410=8Fn_P@\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string UnitedKingdomBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string UnitedKingdomBarcode6 = "010477298543159410:j@%e+P\x001D172310312151itJzCguA\x000D";
    private const string UnitedKingdomBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string UnitedKingdomBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string UnitedKingdomBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(@P_nF8=9L\x000D";
    private const string UnitedKingdomBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%@\x000D";
    private const string UnitedKingdomBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621@j:AugCzJt\x000D";
    private const string UnitedKingdomBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string UnitedKingdomVmBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ ` { ~ } ¬    0029    0028    0030    0031    \0    \x000D";
    private const string UnitedKingdomVmBarcode1 = "010477298543159410DdVcX<t\x001D1723020721yCH*4'h1Ab\x000D";
    private const string UnitedKingdomVmBarcode2 = "010477298543159410.GRs!qO\x001D1723072921TgIv?,6BmK\x000D";
    private const string UnitedKingdomVmBarcode3 = "010477298543159410(fpNxYi\x001D1723040521Z&Ur7>0oQS\x000D";
    private const string UnitedKingdomVmBarcode4 = "010477298543159410=8Fn_P@\x001D1724100221Ew5;2/zaMJ\x000D";
    private const string UnitedKingdomVmBarcode5 = "0104772985431594103kuW)L9\x001D17240304215j4CltEc-Y\x000D";
    private const string UnitedKingdomVmBarcode6 = "010477298543159410:j@%e+P\x001D172310312151itJzCguA\x000D";
    private const string UnitedKingdomVmBarcode7 = "010477298543159410bA1h'4*\x001D1723121421t<XcVdD0q!\x000D";
    private const string UnitedKingdomVmBarcode8 = "010477298543159410HCyKmB6\x001D1724062021sRG.iYxNpf\x000D";
    private const string UnitedKingdomVmBarcode9 = "010477298543159410,?vIgTS\x001D1724021821(@P_nF8=9L\x000D";
    private const string UnitedKingdomVmBarcode10 = "010477298543159410Qo0>7rU\x001D1724070321)Wuk3P+e%@\x000D";
    private const string UnitedKingdomVmBarcode11 = "010477298543159410&ZJMaz/\x001D1723112621@j:AugCzJt\x000D";
    private const string UnitedKingdomVmBarcode12 = "0104772985431594102;5wEY-\x001D1723040421HIrQ9QeTyQ\x000D";
    private const string UnitedKingdomExtendedBaseline = "  ! @ % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   £ $ \" [ # ] ^ \0`{ ~ } ¬    \0    \0    \0    \0    \0    \x000D";
    private const string UnitedKingdomExtendedDeadKey1 = "\0`!\0`@\0`£\0`$\0`%\0`&\0`'\0`(\0`)\0`*\0`+\0`,\0`-\0`.\0`/\0`0\0`1\0`2\0`3\0`4\0`5\0`6\0`7\0`8\0`9\0`:\0`;\0`<\0`=\0`>\0`?\0`\"\0À\0`B\0`C\0`D\0È\0`F\0`G\0`H\0Ì\0`J\0`K\0`L\0`M\0`N\0Ò\0`P\0`Q\0`R\0`S\0`T\0Ù\0`V\0Ẁ\0`X\0Ỳ\0`Z\0`[\0`#\0`]\0`^\0`_\0``\0à\0`b\0`c\0`d\0è\0`f\0`g\0`h\0ì\0`j\0`k\0`l\0`m\0`n\0ò\0`p\0`q\0`r\0`s\0`t\0ù\0`v\0ẁ\0`x\0ỳ\0`z\0`{\0`~\0`}\0`¬\x000D";
    private const string UnitedKingdomExtendedBarcode1 = "010477298543159410DdVcX<t\01723020721yCH*4'h1Ab\x000D";
    private const string UnitedKingdomExtendedBarcode2 = "010477298543159410.GRs!qO\01723072921TgIv?,6BmK\x000D";
    private const string UnitedKingdomExtendedBarcode3 = "010477298543159410(fpNxYi\01723040521Z&Ur7>0oQS\x000D";
    private const string UnitedKingdomExtendedBarcode4 = "010477298543159410=8Fn_P@\01724100221Ew5;2/zaMJ\x000D";
    private const string UnitedKingdomExtendedBarcode5 = "0104772985431594103kuW)L9\017240304215j4CltEc-Y\x000D";
    private const string UnitedKingdomExtendedBarcode6 = "010477298543159410:j@%e+P\0172310312151itJzCguA\x000D";
    private const string UnitedKingdomExtendedBarcode7 = "010477298543159410bA1h'4*\01723121421t<XcVdD0q!\x000D";
    private const string UnitedKingdomExtendedBarcode8 = "010477298543159410HCyKmB6\01724062021sRG.iYxNpf\x000D";
    private const string UnitedKingdomExtendedBarcode9 = "010477298543159410,?vIgTS\01724021821(@P_nF8=9L\x000D";
    private const string UnitedKingdomExtendedBarcode10 = "010477298543159410Qo0>7rU\01724070321)Wuk3P+e%@\x000D";
    private const string UnitedKingdomExtendedBarcode11 = "010477298543159410&ZJMaz/\01723112621@j:AugCzJt\x000D";
    private const string UnitedKingdomExtendedBarcode12 = "0104772985431594102;5wEY-\01723040421HIrQ9QeTyQ\x000D";

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

    private readonly Dictionary<string, IPackIdentifier> _baseIdentifiers;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardCalibratorTestsFromUnitedStates"/> class.
    /// </summary>
    public KeyboardCalibratorTestsFromUnitedStates() {
        _baseIdentifiers = BasePackIdentifiers(
            BaseCalibration().CalibrationData);
    }

    /// <summary>
    /// Test a simple string
    /// </summary>
    [Fact]
    public void UnitedStatesToUnitedStates() {
        var calibrator = new Calibrator();
        var loopCount = 0;

        foreach (var token in calibrator.CalibrationTokens()) {
            calibrator.Calibrate(ConvertToCharacterValues(BaselineCalibrationUsUs()), token);
            loopCount++;
        }

        Assert.Equal(1, loopCount);
    }

    /// <summary>
    /// Test calibration for the error discovered by Lexon Ireland.  In this case, they appeared
    /// to have a US keyboard scanner and a UK or Ireland keyboard, but for some reason the scanner
    /// reports the ASCII 29 as a quote character.  It also reports @ as a quote.
    /// </summary>
    [Fact]
    public void LexonError() {
        PerformParserTest(
            PerformCalibrationTest("Lexon Error A").CalibrationData,
            LexonErrorABarcodeData());

        // Calibration fails
        var token = PerformCalibrationTest("Lexon Error B");
        Assert.Null(token.CalibrationData);

        PerformParserTest(
            PerformCalibrationTest("Lexon Error C").CalibrationData,
            LexonErrorCBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBelgianFrench() {
        PerformParserTest(
            PerformCalibrationTest("Belgian French").CalibrationData,
            BelgianFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian (Comma) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBelgianComma() {
        PerformParserTest(
            PerformCalibrationTest("Belgian (Comma)").CalibrationData,
            BelgianCommaBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Belgian (Period) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBelgianPeriod() {
        PerformParserTest(
            PerformCalibrationTest("Belgian (Period)").CalibrationData,
            BelgianPeriodBarcodeData());
    }

    /// <summary>
    /// Test calibration for a French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToFrench() {
        PerformParserTest(
            PerformCalibrationTest("French").CalibrationData,
            FrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss French computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSwissFrench() {
        PerformParserTest(
            PerformCalibrationTest("Swiss French").CalibrationData,
            SwissFrenchBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Croatian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToCroatianStandard() {
        PerformParserTest(
            PerformCalibrationTest("Croatian (Standard)").CalibrationData,
            CroatianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBulgarian() {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian").CalibrationData,
            BulgarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Latin) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBulgarianLatin() {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Latin)").CalibrationData,
            BulgarianLatinBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic Traditional) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBulgarianPhoneticTraditional() {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Phonetic Traditional)").CalibrationData,
            BulgarianPhoneticTraditionalBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBulgarianPhonetic() {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Phonetic)").CalibrationData,
            BulgarianPhoneticBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Typewriter) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToBulgarianTypewriter() {
        PerformParserTest(
            PerformCalibrationTest("Bulgarian (Typewriter)").CalibrationData,
            BulgarianTypewriterBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSwedish() {
        PerformParserTest(
            PerformCalibrationTest("Swedish").CalibrationData,
            SwedishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swedish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSwedishWithSami() {
        PerformParserTest(
            PerformCalibrationTest("Swedish with Sami").CalibrationData,
            SwedishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToGreek() {
        PerformParserTest(
            PerformCalibrationTest("Greek").CalibrationData,
            GreekBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek (220) computer keyboard layout
    /// </summary>
    [Fact]
    public void ToGreek220() {
        // Calibration fails
        var token = PerformCalibrationTest("Greek (220)");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Greek (319) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToGreek319() {
        PerformParserTest(
            PerformCalibrationTest("Greek (319)").CalibrationData,
            Greek319BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Greek Polytonic computer keyboard layout
    /// </summary>
    [Fact]
    public void ToGreekPolytonic() {
        // Calibration fails
        var token = PerformCalibrationTest("Greek Polytonic");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Czech computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToCzech() {
        PerformParserTest(
            PerformCalibrationTest("Czech").CalibrationData,
            CzechBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToCzechQwerty() {
        PerformParserTest(
            PerformCalibrationTest("Czech (QWERTY)").CalibrationData,
            CzechQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Czech Programmers computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToCzechProgrammers() {
        PerformParserTest(
            PerformCalibrationTest("Czech Programmers").CalibrationData,
            CzechProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Dutch computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToDutch() {
        PerformParserTest(
            PerformCalibrationTest("Dutch").CalibrationData,
            DutchBarcodeData());
    }

    /// <summary>
    /// Test calibration for an Estonian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToEstonian() {
        PerformParserTest(
            PerformCalibrationTest("Estonian").CalibrationData,
            EstonianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToFinnish() {
        PerformParserTest(
            PerformCalibrationTest("Finnish").CalibrationData,
            FinnishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Finnish with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToFinnishWithSami() {
        PerformParserTest(
            PerformCalibrationTest("Finnish with Sami").CalibrationData,
            FinnishWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToGerman() {
        PerformParserTest(
            PerformCalibrationTest("German").CalibrationData,
            GermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a German (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToGermanIbm() {
        PerformParserTest(
            PerformCalibrationTest("German (IBM)").CalibrationData,
            GermanIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss German computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSwissGerman() {
        PerformParserTest(
            PerformCalibrationTest("Swiss German").CalibrationData,
            SwissGermanBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Danish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToDanish() {
        PerformParserTest(
            PerformCalibrationTest("Danish").CalibrationData,
            DanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Bulgarian (Phonetic) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToHungarian() {
        PerformParserTest(
            PerformCalibrationTest("Hungarian").CalibrationData,
            HungarianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Hungarian 101-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToHungarian101Key() {
        PerformParserTest(
            PerformCalibrationTest("Hungarian 101-key").CalibrationData,
            Hungarian101KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for an Icelandic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToIcelandic() {
        PerformParserTest(
            PerformCalibrationTest("Icelandic").CalibrationData,
            IcelandicBarcodeData());
    }

    /// <summary>
    /// Test calibration for an Irish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToIrish() {
        PerformParserTest(
            PerformCalibrationTest("Irish").CalibrationData,
            IrishBarcodeData());
    }

    /// <summary>
    /// Test calibration for an Italian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToItalian() {
        PerformParserTest(
            PerformCalibrationTest("Italian").CalibrationData,
            ItalianBarcodeData());
    }

    /// <summary>
    /// Test calibration for an Italian (142) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToItalian142() {
        PerformParserTest(
            PerformCalibrationTest("Italian (142)").CalibrationData,
            Italian142BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    public void ToLatvianStandard() {
        // Calibration fails
        var token = PerformCalibrationTest("Latvian (Standard)");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Latvian (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLatvianQwerty() {
        PerformParserTest(
            PerformCalibrationTest("Latvian (QWERTY)").CalibrationData,
            LatvianQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Latvian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLatvian() {
        PerformParserTest(
            PerformCalibrationTest("Latvian").CalibrationData,
            LatvianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLithuanian() {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian").CalibrationData,
            LithuanianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (IBM) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLithuanianIbm() {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian (IBM)").CalibrationData,
            LithuanianIbmBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Lithuanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLithuanianStandard() {
        PerformParserTest(
            PerformCalibrationTest("Lithuanian (Standard)").CalibrationData,
            LithuanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSorbian() {
        PerformParserTest(
            PerformCalibrationTest("Sorbian").CalibrationData,
            SorbianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian with Sami computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToNorwegianWithSami() {
        PerformParserTest(
            PerformCalibrationTest("Norwegian with Sami").CalibrationData,
            NorwegianWithSamiBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Luxembourgish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToLuxembourgish() {
        PerformParserTest(
            PerformCalibrationTest("Luxembourgish").CalibrationData,
            LuxembourgishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Norwegian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToNorwegian() {
        PerformParserTest(
            PerformCalibrationTest("Norwegian").CalibrationData,
            NorwegianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 47-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToMaltese47Key() {
        PerformParserTest(
            PerformCalibrationTest("Maltese 47-key").CalibrationData,
            Maltese47KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Maltese 48-key computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToMaltese48Key() {
        PerformParserTest(
            PerformCalibrationTest("Maltese 48-key").CalibrationData,
            Maltese48KeyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToPolishProgrammers() {
        PerformParserTest(
            PerformCalibrationTest("Polish (Programmers)").CalibrationData,
            PolishProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Polish (214) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToPolish214() {
        PerformParserTest(
            PerformCalibrationTest("Polish (214)").CalibrationData,
            Polish214BarcodeData());
    }

    /// <summary>
    /// Test calibration for a Portuguese computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToPortuguese() {
        PerformParserTest(
            PerformCalibrationTest("Portuguese").CalibrationData,
            PortugueseBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Standard) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToRomanianStandard() {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Standard)").CalibrationData,
            RomanianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToRomanianLegacy() {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Legacy)").CalibrationData,
            RomanianLegacyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Romanian (Programmers) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToRomanianProgrammers() {
        PerformParserTest(
            PerformCalibrationTest("Romanian (Programmers)").CalibrationData,
            RomanianProgrammersBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Scottish Gaelic computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToScottishGaelic() {
        PerformParserTest(
            PerformCalibrationTest("Scottish Gaelic").CalibrationData,
            ScottishGaelicBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovak computer keyboard layout
    /// </summary>
    [Fact]
    public void ToSlovak() {
        // Calibration fails
        var token = PerformCalibrationTest("Slovak");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Slovak (QWERTY) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSlovakQwerty() {
        PerformParserTest(
            PerformCalibrationTest("Slovak (QWERTY)").CalibrationData,
            SlovakQwertyBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Slovenian computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSlovenian() {
        PerformParserTest(
            PerformCalibrationTest("Slovenian").CalibrationData,
            SlovenianBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Spanish computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSpanish() {
        PerformParserTest(
            PerformCalibrationTest("Spanish").CalibrationData,
            SpanishBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Spanish Variation computer keyboard layout
    /// </summary>
    [Fact]
    public void ToSpanishVariation() {
        // Calibration fails
        var token = PerformCalibrationTest("Spanish Variation");
        Assert.Null(token.CalibrationData);
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSorbianStandard() {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Standard").CalibrationData,
            SorbianStandardBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSorbianExtended() {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Extended").CalibrationData,
            SorbianExtendedBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Sorbian Standard (Legacy) computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSorbianStandardLegacy() {
        PerformParserTest(
            PerformCalibrationTest("Sorbian Standard (Legacy)").CalibrationData,
            SorbianStandardLegacyBarcodeData());
    }
    /// <summary>
    /// Test calibration for a United Kingdom computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToUnitedKingdom() {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom").CalibrationData,
            UnitedKingdomBarcodeData());
    }

    /// <summary>
    /// Test calibration for a United Kingdom computer keyboard layout
    /// </summary>
    [Fact]
    public void ToUnitedKingdomVm() {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom VM").CalibrationData,
            UnitedKingdomVmBarcodeData());
    }
    
    /// <summary>
    /// Test calibration for a United Kingdom Extended computer keyboard layout
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToUnitedKingdomExtended() {
        PerformParserTest(
            PerformCalibrationTest("United Kingdom Extended").CalibrationData,
            UnitedKingdomExtendedBarcodeData());
    }

    /// <summary>
    /// Test calibration for a Swiss French computer keyboard layout using 24x24 data matrix barcodes.
    /// </summary>
    [Fact]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<Approved>")]
    public void ToSwissFrench2424() {
        PerformParserTest(
            PerformCalibrationTest("Swiss French 24x24", size: Size.Dm24X24).CalibrationData,
            SwissFrenchBarcodeData());
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

        Trace.WriteLine(
            $"private const string {layoutName.Replace(" ", "").Replace("(", "").Replace(")", "")}Calibration = " +
            ToLiteral($"\"{calibrator.CalibrationData}\";"));

        // Assert that the calibrator calculated the expected calibration.
        Assert.Equal(expectedCalibrations[layoutName], currentToken.CalibrationData?.ToJson() ?? string.Empty);

        return currentToken;

        static string ToLiteral(string input) {
            using var writer = new StringWriter();
            using var provider = CodeDomProvider.CreateProvider("CSharp");
            provider.GenerateCodeFromExpression(new System.CodeDom.CodePrimitiveExpression(input), writer, null!);
            return writer.ToString();
        }
    }

    private void PerformParserTest(BarcodeScanner.Calibration.Data data, Dictionary<string, string> scannedData) {
        Assert.NotNull(data);

        var parser = new Parser(data);

        foreach (var barcodeData in scannedData) {
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
    /// <param name="data">Calibration data for the scanner keyboard layout.</param>
    /// <returns>A collection of pack identifiers for the scanner keyboard layout.</returns>
    private static Dictionary<string, IPackIdentifier> BasePackIdentifiers(BarcodeScanner.Calibration.Data data) {
        var identifiers = new Dictionary<string, IPackIdentifier>();

        if (data is null) {
            return identifiers;
        }

        var parser = new Parser(data);

        foreach (var barcodeData in UnitedStatesBarcodeData()) {
            var identifier = parser.Parse(barcodeData.Value);
            identifiers.Add(barcodeData.Key, identifier);
            Assert.True(identifier.IsValid);
        }

        return identifiers;
    }

    /// <summary>
    /// Performs a calibration test.
    /// </summary>
    private static Token BaseCalibration() {
        var computerKeyboardLayout = new Dictionary<string, IList<string>>
                                     {
                                         {
                                             UnitedStatesBaseline,
                                             new List<string>()
                                         }
                                     };

        var calibrator = new Calibrator();
        var loopCount = -1;
        Token currentToken = default;

        foreach (var token in calibrator.CalibrationTokens()) {
            var baseLine = computerKeyboardLayout.Keys.First();
            currentToken = token;

            if (loopCount < 0) {
                currentToken = calibrator.Calibrate(ConvertToCharacterValues(baseLine), currentToken);
                loopCount++;
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

        return currentToken;
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
                                           "Lexon Error A",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LexonErrorABaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Lexon Error B",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LexonErrorBBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Lexon Error C",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LexonErrorCBaseline], new List<string>() }
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
                                           "Belgian (Comma)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [BelgianCommaBaseline],
                                                   new List<string>
                                                   {
                                                       BelgianCommaDeadKey1, BelgianCommaDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Belgian (Period)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [BelgianPeriodBaseline],
                                                   new List<string>
                                                   {
                                                       BelgianPeriodDeadKey1, BelgianPeriodDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "French",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [FrenchBaseline],
                                                   new List<string> { FrenchDeadKey1, FrenchDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Swiss French",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SwissFrenchBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [CroatianStandardBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [BulgarianBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Latin)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [BulgarianLatinBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Phonetic Traditional)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [BulgarianPhoneticTraditionalBaseline],
                                                   new List<string>()
                                               }
                                           }
                                       },
                                       {
                                           "Bulgarian (Phonetic)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [BulgarianPhoneticBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Bulgarian (Typewriter)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [BulgarianTypewriterBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Swedish",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SwedishBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SwedishWithSamiBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GreekBaseline],
                                                   new List<string>
                                                   {
                                                       GreekDeadKey1, GreekDeadKey2, GreekDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Greek (220)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [Greek220Baseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Greek (319)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [Greek319Baseline],
                                                   new List<string> { Greek319DeadKey1, Greek319DeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Greek Polytonic",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GreekPolytonicBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [CzechBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [CzechQwertyBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [CzechProgrammersBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Dutch",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [DutchBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [EstonianBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [FinnishBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [FinnishWithSamiBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GermanBaseline],
                                                   new List<string>
                                                   {
                                                       GermanDeadKey1, GermanDeadKey2, GermanDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "German (IBM)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [GermanIbmBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SwissGermanBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [DanishBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [HungarianBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Hungarian 101-key",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [Hungarian101KeyBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Icelandic",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [IcelandicBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [IrishBaseline],
                                                   new List<string> { IrishDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Italian",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [ItalianBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Italian (142)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [Italian142Baseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Latvian (Standard)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [LatvianStandardBaseline],
                                                   new List<string> { LatvianStandardDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Latvian (QWERTY)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [LatvianQwertyBaseline],
                                                   new List<string>
                                                   {
                                                       LatvianQwertyDeadKey1, LatvianQwertyDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Latvian",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [LatvianBaseline],
                                                   new List<string> { LatvianDeadKey1, LatvianDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Lithuanian",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LithuanianBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Lithuanian (IBM)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LithuanianIbmBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Lithuanian (Standard)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [LithuanianStandardBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Sorbian",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SorbianBaseline],
                                                   new List<string>
                                                   {
                                                       SorbianDeadKey1, SorbianDeadKey2, SorbianDeadKey3
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Norwegian with Sami",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [NorwegianWithSamiBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [LuxembourgishBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [NorwegianBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [Maltese47KeyBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Maltese 48-key",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [Maltese48KeyBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Polish (Programmers)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [PolishProgrammersBaseline],
                                                   new List<string> { PolishProgrammersDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Polish (214)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [Polish214Baseline],
                                                   new List<string> { Polish214DeadKey1, Polish214DeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Portuguese",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [PortugueseBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [RomanianStandardBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Romanian (Legacy)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [RomanianLegacyBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Romanian (Programmers)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [RomanianProgrammersBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Scottish Gaelic",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [ScottishGaelicBaseline],
                                                   new List<string>
                                                   {
                                                       ScottishGaelicDeadKey1, ScottishGaelicDeadKey2
                                                   }
                                               }
                                           }
                                       },
                                       {
                                           "Slovak",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SlovakBaseline],
                                                   new List<string> { SlovakDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Slovak (QWERTY)",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SlovakQwertyBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SlovenianBaseline],
                                                   new List<string> { SlovenianDeadKey1, SlovenianDeadKey2 }
                                               }
                                           }
                                       },
                                       {
                                           "Spanish",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SpanishBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [SpanishVariationBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "Sorbian Standard",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SorbianStandardBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SorbianExtendedBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [SorbianStandardLegacyBaseline],
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
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [UnitedKingdomBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "United Kingdom VM",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               { [UnitedKingdomVmBaseline], new List<string>() }
                                           }
                                       },
                                       {
                                           "United Kingdom Extended",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
                                                   [UnitedKingdomExtendedBaseline],
                                                   new List<string> { UnitedKingdomExtendedDeadKey1 }
                                               }
                                           }
                                       },
                                       {
                                           "Swiss French 24x24",
                                           new Dictionary<string[], IList<string>>
                                           {
                                               {
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
                                               { "Lexon Error A", Calibrations.LexonErrorACalibration },
                                               { "Lexon Error B", Calibrations.LexonErrorBCalibration },
                                               { "Lexon Error C", Calibrations.LexonErrorCCalibration },
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
                                               { "United Kingdom VM", Calibrations.UnitedKingdomVmCalibration },
                                               { "United Kingdom Extended", Calibrations.UnitedKingdomExtendedCalibration },
                                               { "Swiss French 24x24", Calibrations.SwissFrenchCalibration }
                                           };
        return unitedStatesTestCalibrations;
    }

    /// <summary>
    /// Returns the barcode data as entered using a United States keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedStatesBarcodeData() {
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
    /// Returns the expected barcode data for the Lexon error keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LexonErrorABarcodeData() {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LexonErrorABarcode1 },
                   { "Barcode2", LexonErrorABarcode2 },
                   { "Barcode3", LexonErrorABarcode3 },
                   { "Barcode4", LexonErrorABarcode4 },
                   { "Barcode5", LexonErrorABarcode5 },
                   { "Barcode6", LexonErrorABarcode6 },
                   { "Barcode7", LexonErrorABarcode7 },
                   { "Barcode8", LexonErrorABarcode8 },
                   { "Barcode9", LexonErrorABarcode9 },
                   { "Barcode10", LexonErrorABarcode10 },
                   { "Barcode11", LexonErrorABarcode11 },
                   { "Barcode12", LexonErrorABarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for the Lexon error keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> LexonErrorCBarcodeData() {
        return new Dictionary<string, string>
               {
                   { "Barcode1", LexonErrorCBarcode1 },
                   { "Barcode2", LexonErrorCBarcode2 },
                   { "Barcode3", LexonErrorCBarcode3 },
                   { "Barcode4", LexonErrorCBarcode4 },
                   { "Barcode5", LexonErrorCBarcode5 },
                   { "Barcode6", LexonErrorCBarcode6 },
                   { "Barcode7", LexonErrorCBarcode7 },
                   { "Barcode8", LexonErrorCBarcode8 },
                   { "Barcode9", LexonErrorCBarcode9 },
                   { "Barcode10", LexonErrorCBarcode10 },
                   { "Barcode11", LexonErrorCBarcode11 },
                   { "Barcode12", LexonErrorCBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Belgian French keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> BelgianFrenchBarcodeData() {
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
    private static Dictionary<string, string> BelgianCommaBarcodeData() {
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
    private static Dictionary<string, string> BelgianPeriodBarcodeData() {
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
    private static Dictionary<string, string> FrenchBarcodeData() {
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
    private static Dictionary<string, string> SwissFrenchBarcodeData() {
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
    private static Dictionary<string, string> CroatianStandardBarcodeData() {
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
    private static Dictionary<string, string> BulgarianBarcodeData() {
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
    private static Dictionary<string, string> BulgarianLatinBarcodeData() {
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
    private static Dictionary<string, string> BulgarianPhoneticTraditionalBarcodeData() {
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
    private static Dictionary<string, string> BulgarianPhoneticBarcodeData() {
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
    private static Dictionary<string, string> BulgarianTypewriterBarcodeData() {
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
    private static Dictionary<string, string> SwedishBarcodeData() {
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
    private static Dictionary<string, string> SwedishWithSamiBarcodeData() {
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
    private static Dictionary<string, string> GreekBarcodeData() {
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
    private static Dictionary<string, string> Greek319BarcodeData() {
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
    /// Returns the expected barcode data for a Czech computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechBarcodeData() {
        return new Dictionary<string, string>
               {
                   { "Barcode1", CzechBarcode1 },
                   { "Barcode2", CzechBarcode2 },
                   { "Barcode3", CzechBarcode3 },
                   { "Barcode4", CzechBarcode4 },
                   { "Barcode5", CzechBarcode5 },
                   { "Barcode6", CzechBarcode6 },
                   { "Barcode7", CzechBarcode7 },
                   { "Barcode8", CzechBarcode8 },
                   { "Barcode9", CzechBarcode9 },
                   { "Barcode10", CzechBarcode10 },
                   { "Barcode11", CzechBarcode11 },
                   { "Barcode12", CzechBarcode12 }
               };
    }

    /// <summary>
    /// Returns the expected barcode data for a Czech (QWERTY) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> CzechQwertyBarcodeData() {
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
    private static Dictionary<string, string> CzechProgrammersBarcodeData() {
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
    private static Dictionary<string, string> DutchBarcodeData() {
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
    /// Returns the expected barcode data for an Estonian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> EstonianBarcodeData() {
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
    private static Dictionary<string, string> FinnishBarcodeData() {
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
    private static Dictionary<string, string> FinnishWithSamiBarcodeData() {
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
    private static Dictionary<string, string> GermanBarcodeData() {
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
    private static Dictionary<string, string> GermanIbmBarcodeData() {
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
    private static Dictionary<string, string> SwissGermanBarcodeData() {
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
    private static Dictionary<string, string> DanishBarcodeData() {
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
    private static Dictionary<string, string> HungarianBarcodeData() {
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
    private static Dictionary<string, string> Hungarian101KeyBarcodeData() {
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
    /// Returns the expected barcode data for an Icelandic computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IcelandicBarcodeData() {
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
    /// Returns the expected barcode data for an Irish computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> IrishBarcodeData() {
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
    /// Returns the expected barcode data for an Italian computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> ItalianBarcodeData() {
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
    /// Returns the expected barcode data for an Italian (142) computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> Italian142BarcodeData() {
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
    private static Dictionary<string, string> LatvianQwertyBarcodeData() {
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
    private static Dictionary<string, string> LatvianBarcodeData() {
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
    private static Dictionary<string, string> LithuanianBarcodeData() {
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
    private static Dictionary<string, string> LithuanianIbmBarcodeData() {
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
    private static Dictionary<string, string> LithuanianStandardBarcodeData() {
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
    private static Dictionary<string, string> NorwegianWithSamiBarcodeData() {
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
    private static Dictionary<string, string> LuxembourgishBarcodeData() {
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
    private static Dictionary<string, string> NorwegianBarcodeData() {
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
    private static Dictionary<string, string> Maltese47KeyBarcodeData() {
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
    private static Dictionary<string, string> Maltese48KeyBarcodeData() {
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
    private static Dictionary<string, string> PolishProgrammersBarcodeData() {
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
    private static Dictionary<string, string> Polish214BarcodeData() {
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
    private static Dictionary<string, string> PortugueseBarcodeData() {
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
    private static Dictionary<string, string> RomanianStandardBarcodeData() {
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
    private static Dictionary<string, string> RomanianLegacyBarcodeData() {
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
    private static Dictionary<string, string> RomanianProgrammersBarcodeData() {
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
    private static Dictionary<string, string> ScottishGaelicBarcodeData() {
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
    private static Dictionary<string, string> SlovakQwertyBarcodeData() {
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
    private static Dictionary<string, string> SlovenianBarcodeData() {
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
    private static Dictionary<string, string> SpanishBarcodeData() {
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
    private static Dictionary<string, string> SorbianBarcodeData() {
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
    private static Dictionary<string, string> SorbianStandardBarcodeData() {
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
    private static Dictionary<string, string> SorbianExtendedBarcodeData() {
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
    private static Dictionary<string, string> SorbianStandardLegacyBarcodeData() {
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
    private static Dictionary<string, string> UnitedKingdomBarcodeData() {
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
    /// Returns the expected barcode data for a United Kingdom computer keyboard layout for the VM issue
    /// in which ASCII control characters are reported as strings of digits. 
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomVmBarcodeData() {
        return new Dictionary<string, string>
        {
            { "Barcode1", UnitedKingdomVmBarcode1 },
            { "Barcode2", UnitedKingdomVmBarcode2 },
            { "Barcode3", UnitedKingdomVmBarcode3 },
            { "Barcode4", UnitedKingdomVmBarcode4 },
            { "Barcode5", UnitedKingdomVmBarcode5 },
            { "Barcode6", UnitedKingdomVmBarcode6 },
            { "Barcode7", UnitedKingdomVmBarcode7 },
            { "Barcode8", UnitedKingdomVmBarcode8 },
            { "Barcode9", UnitedKingdomVmBarcode9 },
            { "Barcode10", UnitedKingdomVmBarcode10 },
            { "Barcode11", UnitedKingdomVmBarcode11 },
            { "Barcode12", UnitedKingdomVmBarcode12 }
        };
    }


    /// <summary>
    /// Returns the expected barcode data for a United Kingdom Extended computer keyboard layout
    /// </summary>
    /// <returns>A dictionary of barcode data.</returns>
    private static Dictionary<string, string> UnitedKingdomExtendedBarcodeData() {
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
    /// Return the character input for the baseline calibration barcode using a scanner
    /// configured for a US keyboard and computer with US keyboard layout. 
    /// </summary>
    /// <returns>
    /// The <see cref="string"/> for scanning the baseline calibration barcode.
    /// The scanner is configured for a US keyboard.
    /// The computer is configured for a US keyboard.
    /// </returns>
    private static string BaselineCalibrationUsUs() {
        var testString =
            "  ! \" % & ' ( ) * + , - . / 0 1 2 3 4 5 6 7 8 9 : ; < = > ? A B C D E F G H I J K L M N O P Q R S T U V W X Y Z _ a b c d e f g h i j k l m n o p q r s t u v w x y z   # $ @ [ \\ ] ^ ` { | } ~    "
          + (char)29;
        return testString + "    \x001C    \x001E    \x001F    \0    ";
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
}