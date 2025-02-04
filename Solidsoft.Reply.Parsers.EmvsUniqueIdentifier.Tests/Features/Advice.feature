Feature: Advice

Basic tests for general advice provided by the Calibrator library.
All tests assume a computer configured for a standard USA keyboard.

Scenario: System reads Invariant Characters reliably
    # Format tests are included
	Given the baseline input is for The United States
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: System reads Invariant Characters reliably with no PPN assessment

	Given the baseline input is for The United States with no PPN assessment
	When the baseline input is submitted to an agnostic calibrator with no PPN assessment
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyNoPpnTest
		And the advice should contain no other advice items

Scenario: No GS character reported
    # Format tests are included
	Given the baseline input is for The United States with no GS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for CannotReadUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: No RS character reported
    # Format tests are included
	Given the baseline input is for The United States with no RS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyExceptPpn
	    And the advice should contain an advice item for CannotReadPpnBarcodes
		And the advice should contain no other advice items

Scenario: No FS character reported
    # Format tests are included
	Given the baseline input is for The United States with no FS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: No US character reported
    # Format tests are included
	Given the baseline input is for The United States with no US
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: No EOT character reported
    # Format tests are included
	Given the baseline input is for The United States with no EOT
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyExceptPpn
	    And the advice should contain an advice item for CannotReadAscii04Characters
	    And the advice should contain an advice item for CannotReadPpnBarcodes
		And the advice should contain no other advice items

	Scenario: Null GS character reported
    # Format tests are included
	Given the baseline input is for The United States with null GS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
		And the advice should contain no other advice items

Scenario: Null RS character reported
    # Format tests are included
	Given the baseline input is for The United States with null RS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
		And the advice should contain no other advice items

Scenario: Null FS character reported

	Given the baseline input is for The United States with null FS
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: Null US character reported

	Given the baseline input is for The United States with null US
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: Null EOT character reported

	Given the baseline input is for The United States with null EOT
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
    	And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items

Scenario: GS character reported as different control character - agnostic
    # Format tests are included
	Given the baseline input is for The United States with GS as different character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
		And the advice should contain no other advice items

Scenario: RS character reported as different control character - agnostic
    # Format tests are included
	Given the baseline input is for The United States with RS as different character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
		And the advice should contain no other advice items

Scenario: FS character reported as different control character - agnostic
    # Format tests are included
	Given the baseline input is for The United States with FS as different character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as different control character - agnostic
    # Format tests are included
	Given the baseline input is for The United States with US as different character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as different control character - agnostic
    # Format tests are included
	Given the baseline input is for The United States with EOT as different character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
    	And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items

Scenario: GS character reported as different control character - calibration
    # Format tests are included
	Given the baseline input is for The United States with GS as different character
	When the baseline input is submitted to a calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: RS character reported as different control character - calibration
    # Format tests are included
	Given the baseline input is for The United States with RS as different character
	When the baseline input is submitted to a calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

 Scenario: FS character reported as different control character - calibration
    # Format tests are included
	Given the baseline input is for The United States with FS as different character
	When the baseline input is submitted to a calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: US character reported as different control character - calibration
    # Format tests are included
	Given the baseline input is for The United States with US as different character
	When the baseline input is submitted to a calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: EOT character reported as different control character - calibration
    # Format tests are included
	Given the baseline input is for The United States with EOT as different character
	When the baseline input is submitted to a calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: GS character reported as different control character - no calibration
    # Format tests are included
	Given the baseline input is for The United States with GS as different character
	When the baseline input is submitted to a no calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectlyNoCalibration
		And the advice should contain no other advice items

Scenario: RS character reported as different control character - no calibration
    # Format tests are included
	Given the baseline input is for The United States with RS as different character
	When the baseline input is submitted to a no calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyExceptPpn
    	And the advice should contain an advice item for MayNotReadPpnNoCalibration
		And the advice should contain no other advice items

Scenario: FS character reported as different control character - no calibration
    # Format tests are included
	Given the baseline input is for The United States with FS as different character
	When the baseline input is submitted to a no calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as different control character - no calibration
    # Format tests are included
	Given the baseline input is for The United States with US as different character
	When the baseline input is submitted to a no calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as different control character - no calibration
    # Format tests are included
	Given the baseline input is for The United States with EOT as different character
	When the baseline input is submitted to a no calibration calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyExceptPpn
	    And the advice should contain an advice item for CannotReadAscii04Characters
	    And the advice should contain an advice item for CannotReadPpnBarcodes
		And the advice should contain no other advice items

Scenario: GS character reported as ambiguous invariant character
    # Format tests are included
	Given the baseline input is for The United States with GS as ambiguous invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for CannotReadUniqueIdentifiersReliably
		And the advice should contain no other advice items

Scenario: RS character reported as ambiguous invariant character
    # Format tests are included
	Given the baseline input is for The United States with RS as ambiguous invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for CannotReadUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadPpnBarcodes
		And the advice should contain no other advice items

Scenario: FS character reported as ambiguous invariant character
    # Format tests are included
	Given the baseline input is for The United States with FS as ambiguous invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as ambiguous invariant character
    # Format tests are included
	Given the baseline input is for The United States with US as ambiguous invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as ambiguous invariant character
    # Format tests are included
	Given the baseline input is for The United States with EOT as ambiguous invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for CannotReadUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadAscii04Characters
	    And the advice should contain an advice item for CannotReadPpnBarcodes
		And the advice should contain no other advice items

Scenario: GS character reported as ambiguous non-invariant character
    # Format tests are included
	Given the baseline input is for The United States with GS as ambiguous non-invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
		And the advice should contain no other advice items

Scenario: RS character reported as ambiguous non-invariant character
    # Format tests are included
	Given the baseline input is for The United States with RS as ambiguous non-invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
		And the advice should contain no other advice items

Scenario: FS character reported as ambiguous non-invariant character
    # Format tests are included
	Given the baseline input is for The United States with FS as ambiguous non-invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as ambiguous non-invariant character
    # Format tests are included
	Given the baseline input is for The United States with US as ambiguous non-invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as ambiguous non-invariant character
    # Format tests are included
	Given the baseline input is for The United States with EOT as ambiguous non-invariant character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items

Scenario: GS character reported as AIM flag character
    # Format tests are included
	Given the baseline input is for The United States with GS as AIM flag character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
	    And the advice should contain an advice item for CannotReadAim
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
		And the advice should contain no other advice items

Scenario: RS character reported as AIM flag character
    # Format tests are included
	Given the baseline input is for The United States with RS as AIM flag character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
	    And the advice should contain an advice item for CannotReadAim
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
		And the advice should contain no other advice items

Scenario: FS character reported as AIM flag character

	Given the baseline input is for The United States with FS as AIM flag character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as AIM flag character

	Given the baseline input is for The United States with US as AIM flag character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for CannotReadEdiCharacters
	    And the advice should contain an advice item for CannotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as AIM flag character

	Given the baseline input is for The United States with EOT as AIM flag character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
	    And the advice should contain an advice item for CannotReadAim
	    And the advice should contain an advice item for CannotReadAdditionalCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items

Scenario: GS character reported as dead key character
    # Format tests are included
	Given the baseline input is for The United States with GS as dead key character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
		And the advice should contain no other advice items

Scenario: RS character reported as dead key character
    # Format tests are included
	Given the baseline input is for The United States with RS as dead key character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
		And the advice should contain no other advice items

Scenario: FS character reported as dead key character

	Given the baseline input is for The United States with FS as dead key character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as dead key character

	Given the baseline input is for The United States with US as dead key character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
	    And the advice should contain an advice item for MayNotReadEdiCharactersReliably
	    And the advice should contain an advice item for MayNotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as dead key character

	Given the baseline input is for The United States with EOT as dead key character
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
	    And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items

Scenario: GS character reported as ligature
    # Format tests are included
	Given the baseline input is for The United States with GS as ligature
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for HiddenCharactersNotRepresentedCorrectly
		And the advice should contain no other advice items

Scenario: RS character reported as ligature
    # Format tests are included
	Given the baseline input is for The United States with RS as ligature
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
		And the advice should contain no other advice items

Scenario: FS character reported as ligature

	Given the baseline input is for The United States with FS as ligature
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain an advice item for MayNotReadEdiCharactersReliably
		And the advice should contain an advice item for MayNotReadAscii28Characters
		And the advice should contain no other advice items

Scenario: US character reported as ligature

	Given the baseline input is for The United States with US as ligature
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliably
		And the advice should contain an advice item for MayNotReadEdiCharactersReliably
		And the advice should contain an advice item for MayNotReadAscii31Characters
		And the advice should contain no other advice items

Scenario: EOT character reported as ligature

	Given the baseline input is for The United States with EOT as ligature
	When the baseline input is submitted to an agnostic calibrator
	    And advice is generated from the calculated system capabilities
	Then the advice should contain an advice item for ReadsUniqueIdentifiersReliablyMayNotReadPpn
		And the advice should contain an advice item for MayNotReadAscii04Characters
		And the advice should contain no other advice items