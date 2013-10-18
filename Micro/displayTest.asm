* ;John Snoap and Chris Rich and Aubin Mukama
* ;Computer Systems
* ;displayTest

* ;Variables
LCD				EQU		$B000
LCDD				EQU		$B001
* ;Variables end

* ;Main														COMMENTS
				ORG		$E000								;Start program
				LDX		#LCD								;LCD location -> X		Initialize LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$1								;Load first command
				STAA		LCD								;Send first command -> LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$30								;Load second command
				STAA		LCD								;Send second command -> LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$8								;Load third command
				STAA		LCD								;Send third command -> LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$6								;Load fourth command
				STAA		LCD								;Send fourth command -> LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$3C								;Load fifth command
				STAA		LCD								;Send fifth command -> LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$F								;Load sixth command
				STAA		LCD								;Send sixth command -> LCD	Initialize LCD end
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001000							;Load "H"
				STAA		LCDD								;Send "H"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01000101							;Load "E"
				STAA		LCDD								;Send "E"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001100							;Load "L"
				STAA		LCDD								;Send "L"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001100							;Load "L"
				STAA		LCDD								;Send "L"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001111							;Load "O"
				STAA		LCDD								;Send "O"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%00100000							;Load " "
				STAA		LCDD								;Send " "
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01010111							;Load "W"
				STAA		LCDD								;Send "W"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001111							;Load "O"
				STAA		LCDD								;Send "O"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01010010							;Load "R"
				STAA		LCDD								;Send "R"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01001100							;Load "L"
				STAA		LCDD								;Send "L"
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#%01000100							;Load "D"
				STAA		LCDD								;Send "D"
eepromTest			LDAA		#$55								;Value $55 -> A
				STAA		$00								;A -> Location $00
				LDAA		$F000								;Value $AA -> A
				BRA		eepromTest
* ;Main end

* ;EEPROM
				ORG		$FFFE
				FDB		$E000
				ORG		$F000
				FCB		$AA
* ;EEPROM end 