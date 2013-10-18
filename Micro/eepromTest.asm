* ;John Snoap, Chris Rich, and Aubin Mukamaishimwe
* ;Computer Systems
* ;eepromTest

* ;Main								COMMENTS
				ORG		$E000		;Start program
eepromTest			LDAA		#$55		;Value $55 -> A
				STAA		$00		;A -> Location $00
				LDAA		$F000		;Value $AA -> A
				BRA		eepromTest		
* ;Main end	

* ;EEPROM	
				ORG		$FFFE		;Reset Vector
				FDB		$E000		;Reset Address
				ORG		$F000
				FCB		$AA
* ;EEPROM end 