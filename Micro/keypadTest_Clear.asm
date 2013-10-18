* ;John Snoap, Chris Rich, and Aubin Mukamaishimwe
* ;Computer Systems
* ;keypadTest_Clear

* ;Variables													COMMENTS
characterCount			EQU		$A0								;For our program
currentQuadrant			EQU		$A1								;For our program
shiftFlag			EQU		$A2								;For our program
mode				EQU		$A3								;For our program
bufferStart			EQU		$B0								;For our program
keyboard			EQU		$A000								;For our hardware
LCD				EQU		$B000								;For our hardware
LCDD				EQU		$B001								;For our hardware
LCDStart			EQU		$E400								;For our EEPROM
* ;Variables end

* ;Main														COMMENTS
				ORG		$E000								;Start program
				CLI										;Clear the interupt flag
				LDS		#$FF								;Load the stack pointer
				CLR		characterCount							;Initialize characterCount
				CLR		currentQuadrant							;Initialize currentQuadrant
				CLR		shiftFlag							;Initialize shiftFlag
				CLR		mode								;Initialize mode (step 1 of 2)
				INC		mode								;Initialize mode (step 2 of 2)
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
eepromTest			LDAA		#$55								;Value $55 -> A
				STAA		$00								;A -> Location $00
				LDAA		$F000								;Value $AA -> A
				BRA		eepromTest
* ;Main end

* ;Interrupt Service Routine											COMMENTS
ISR				CLI										;Clear the interrupt flag
				LDAA		keyboard							;Load keypress
				ANDA		#$1F								;Clear the first three bits
				ADDA		shiftFlag							;Add shift
				CMPA		#%11								;if (keypress == shift && !shiftFlag)
				BEQ		shiftOn								;shiftFlag = on
				CMPA		#%10011								;else if (keypress == mode && !shiftFlag)
				BEQ		modePlusPlus							;mode++
				CMPA		#%100011							;else if (keypress == shift && shiftFlag)
				BEQ		shiftOff							;shiftFlag = off
				CMPA		#%110011							;else if (keypress == mode && shiftFlag)
				BEQ		modeMinusMinus							;mode--
				LDX		#bufferStart							;else, keypress is a character
				LDAB		characterCount							;characterCount -> B
				ABX										;Add character count to buffer start
				STAA		0,X								;Keypress -> buffer
				LDX		#LCDStart							;Start LCD translation
				TAB										;Keypress -> B
				ABX										;Add keypress to LCD start
				LDAA		0,X								;Finish LCD translation
				LDX		#LCD								;Prepare to send LCD translation to LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				STAA		LCDD								;LCD translation -> LCD
				INC		characterCount							;characterCount++
				LDAA		#5								;5 -> A
				LDAB		characterCount							;characterCount -> B
				CBA										;if (characterCount < 5)
				BNE		done								;Go to done
				CLR		characterCount							;else, characterCount = 0  Need to add clear display
				LDX		#LCD								;LCD location -> X		Clear LCD
				BRSET		0,X			%10000000	*			;LCD busy check
				LDAA		#$1								;Load first command
				STAA		LCD								;Send first command -> LCD
done				RTI										;Return from interrupt
shiftOn				BSET		shiftFlag		%100000					;Shift on
				BRA		done								;Go to done
shiftOff			BCLR		shiftFlag		%100000					;Shift off
				BRA		done								;Go to done
modePlusPlus			INC		mode								;mode++
				LDAA		#5								;5 -> A
				LDAB		mode								;mode -> B
				CBA										;if (mode < 5)
				BNE		done								;Go to done
				LDAA		#1								;else
				STAA		mode								;mode = 1
				BRA		done								;Go to done
modeMinusMinus			DEC		mode								;mode--
				LDAA		#0								;0 -> A
				LDAB		mode								;mode -> B
				CBA										;if (mode > 0)
				BNE		done								;Go to done
				LDAA		#4								;else
				STAA		mode								;mode = 4
				BRA		done								;Go to done
* ;Interrupt Service Routine end

* ;EEPROM													COMMENTS
				ORG		LCDStart							;LCD start vector
				FCB		%00110001							;"1"
				FCB		%01010001							;"Q"
				FCB		%01000001							;"A"
				FCB		%00011110							;Shift on
				FCB		%00110010							;"2"
				FCB		%01010111							;"W"
				FCB		%01010011							;"S"
				FCB		%01011000							;"X"
				FCB		%00110011							;"3"
				FCB		%01000101							;"E"
				FCB		%01000100							;"D"
				FCB		%01000011							;"C"
				FCB		%00110100							;"4"
				FCB		%01010010							;"R"
				FCB		%01000110							;"F"
				FCB		%01010110							;"V"
				FCB		%00110101							;"5"
				FCB		%01010100							;"T"
				FCB		%01000111							;"G"
				FCB		%11111111							;Mode++
				ORG		LCDStart+32							;LCD start vector + 32
				FCB		%00110110							;"6"
				FCB		%01011001							;"Y"
				FCB		%01001000							;"H"
				FCB		%00100000							;Shift off
				FCB		%00110111							;"7"
				FCB		%01010101							;"U"
				FCB		%01001010							;"J"
				FCB		%01000010							;"B"
				FCB		%00111000							;"8"
				FCB		%01001001							;"I"
				FCB		%01001011							;"K"
				FCB		%01001110							;"N"
				FCB		%00111001							;"9"
				FCB		%01001111							;"O"
				FCB		%01001100							;"L"
				FCB		%01001101							;"M"
				FCB		%00110000							;"0"
				FCB		%01010000							;"P"
				FCB		%01011010							;"Z"
				FCB		%00000000							;Mode--
				
				ORG		$F000
				FCB		$AA
				ORG		$FFF2								;ISR vector
				FDB		ISR								;ISR address
				ORG		$FFFE								;Reset vector
				FDB		$E000								;Reset address
* ;EEPROM end 