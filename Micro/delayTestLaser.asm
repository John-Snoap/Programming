* ;John Snoap, Chris Rich, and Aubin Mukamaishimwe
* ;Computer Systems
* ;delayTestLaser

* ;Variables													COMMENTS
characterCount			EQU		$A0								;For our program
currentQuadrant			EQU		$A1								;For our program
shiftFlag			EQU		$A2								;For our program
mode				EQU		$A3								;For our program
bufferStart			EQU		$B0								;For our program
keyboard			EQU		$A000								;For our hardware
LCD				EQU		$B000								;For our hardware
LCDD				EQU		$B001								;For our hardware
DDRA				EQU		$C000								;For our hardware
CRA				EQU		$C001								;For our hardware
DDRB				EQU		$C002								;For our hardware
CRB				EQU		$C003								;For our hardware
bright				EQU		$9000								;For our hardware
dim				EQU		$8000								;For our hardware
stringLength			EQU		36								;The number of bits allocated for each laser string
quadrantLength			EQU		60								;The horizontal length allocated for each quadrant
LCDStart			EQU		$E400								;For our EEPROM
testPatternStart		EQU		$E450								;For our EEPROM
laserStart			EQU		$E500								;For our EEPROM
animationStart			EQU		$ED00								;For our EEPROM
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
				BSET		$1039			%01000000				;Initialize internal A/D converter
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
				LDX		#CRA								;PIA CRA -> X			Initialize laser
				BCLR		0,X			%00000100				;Trust Watson -> PIA CRA
				LDX		#DDRA								;PIA DDRA -> X
				BSET		0,X			$FF					;Trust Watson -> PIA DDRA
				LDX		#CRA								;PIA CRA -> X
				BSET		0,X			%00000100				;Trust Watson -> PIA CRA
				LDX		#CRB								;PIA CRB -> X
				BCLR		0,X			%00000100				;Trust Watson -> PIA CRB
				LDX		#DDRB								;PIA DDRB -> X
				BSET		0,X			$FF					;Trust Watson -> PIA DDRB
				LDX		#CRB								;PIA CRB -> X
				BSET		0,X			%00000100				;Trust Watson -> PIA CRB	Initialize laser end
				LDAA		#$20								;Trust Watson
				STAA		$1030								;Trust Watson, for delay knob
				STAA		dim								;Make sure the laser starts off
start				LDAA		mode								;Begin main loop
				CMPA		#1								;if (mode == 1)
				BEQ		start								;Go back to start (do not use laser)
				CMPA		#2								;else if (mode == 2)
				BEQ		displayCharacter						;Write letters on wall with laser
				CMPA		#3								;else if (mode == 3)
				BEQ		displayTestPattern						;Display test pattern on wall with laser
				JMP		displayAnimation						;else, display animation on wall with laser
displayCharacter		LDAA		characterCount							;See if there are any characters to draw
				CMPA		#0								;if (characterCount == 0)
				BEQ		characterDisplayReset						;Go to character display reset
				LDAA		currentQuadrant							;else, determine whether or not to draw character
				CMPA		characterCount							;if (currentQuadrant != characterCount)
				BNE		setPointer							;Prepare to send the character to the laser
characterDisplayReset		BCLR		currentQuadrant		%11111111				;else, reset currentQuadrant and
				BRA		start								;Go back to start
setPointer			LDX		#bufferStart							;First of four characters -> X
				LDAB		currentQuadrant							;currentQuadrant -> B
				ABX										;Add current quadrant to buffer start
				LDAA		0,X								;Desired character -> A
				LDAB		#stringLength							;Get laser string offset (A * 32)
				MUL										;Laser string offset -> D
				ADDD		#laserStart							;Laser string offset + laser start -> D
				XGDX										;D <-> X
getNext				LDAA		0,X								;Get next character byte
				CMPA		#$B0								;if (byte == bright)
				BEQ		laserOn								;Turn the laser on
				CMPA		#$D0								;else if (byte == dim)
				BEQ		laserOff							;Turn the laser off
				CMPA		#$70								;else if (byte == complete)
				BEQ		currentQuadrantPlusPlus						;Letter is completed
				LDAA		currentQuadrant							;else, byte is an x-coordinate
				LDAB		#quadrantLength							;Determine location offset for x-coordinate
				MUL										;Location offset -> B
				LDAA		0,X								;X-coordinate -> A
				ABA										;X-coordinate + location offset -> A
				STAA		DDRA								;Send x-coordinate -> PIA
				INX										;Prepare to send y-coordinate
				LDAA		0,X								;Load y-coordinate -> PIA
				STAA		DDRB								;Send y-coordinate -> PIA
				JSR		delay								;Delay for 200 microseconds
				INX										;Position for next character byte
				BRA		getNext								;Go to get next character byte
laserOn				STAA		bright								;Send turn laser on -> PIA
				JSR		delay								;Delay for 200 microseconds
				INX										;Position for next character byte
				BRA		getNext								;Go to get next character byte
laserOff			STAA		dim								;Send turn laser off -> PIA
				JSR		delay								;Delay for 200 microseconds
				INX										;Position for next character byte
				BRA		getNext								;Go to get next character byte
currentQuadrantPlusPlus		INC		currentQuadrant							;currentQuadrant++
				BRA		displayCharacter						;Go to determine whether or not to draw character
displayTestPattern		LDD		#testPatternStart						;Load test pattern start -> D
				XGDX										;D <-> X
getNext1			LDAA		0,X								;Get next byte
				CMPA		#$B0								;if (byte == bright)
				BEQ		laserOn1							;Turn the laser on
				CMPA		#$D0								;else if (byte == dim)
				BEQ		laserOff1							;Turn the laser off
				CMPA		#$70								;else if (byte == complete)
				BEQ		return								;Go to return
				STAA		DDRA								;Send x-coordinate -> PIA
				INX										;Prepare to send y-coordinate
				LDAA		0,X								;Load y-coordinate -> PIA
				STAA		DDRB								;Send y-coordinate -> PIA
				JSR		delay								;Delay for 5 miliseconds
				INX										;Position for next byte
				BRA		getNext1							;Go to get next byte
laserOn1			STAA		bright								;Send turn laser on -> PIA
				JSR		delay								;Delay for 5 miliseconds
				INX										;Position for next byte
				BRA		getNext1							;Go to get next byte
laserOff1			STAA		dim								;Send turn laser off -> PIA
				JSR		delay								;Delay for 5 miliseconds
				INX										;Position for next byte
				BRA		getNext1							;Go to get next byte
return				JMP		start								;Go back to start
displayAnimation		JMP		start								;Go back to start
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

* ;Subrutine delay												COMMENTS
delay				PSHX										;Saves X
				LDAA		#130								;100 * 50us = 5ms
outerLoop			LDX		#7								;Magic number for 50us
innerLoop			DEX										;X--
				CPX		#0								;Add 4 clock cycles
				BNE		innerLoop							;If (X != 0) go to "innerLoop"
				DECA										;A--
				BNE		outerLoop							;If (A != 0) go to "outerLoop"
				PULX										;Restores X
				RTS										;Return from subrutine
* ;Subrutine delay end

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
				
				ORG		testPatternStart						;Test pattern
				FCB		$14								;X
				FCB		$A								;Y
				FCB		$B0								;Bright
				FCB		$14								;X
				FCB		$A0								;Y
				FCB		$8C								;X
				FCB		$A0								;Y
				FCB		$8C								;X
				FCB		$A								;Y
				FCB		$14								;X
				FCB		$A								;Y
				FCB		$D0								;Dim
				FCB		$70								;Test pattern end
				
				ORG		0*stringLength+laserStart					;"1" laser string
				FCB		$0D								;X
				FCB		$41								;Y
				FCB		$B0								;Bright
				FCB		$1A								;X
				FCB		$4E								;Y
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"1" laser string end
				ORG		1*stringLength+laserStart					;"Q" laser string
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$1A								;X
				FCB		$1A								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"Q" laser string end
				ORG		2*stringLength+laserStart					;"A" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"A" laser string end
				ORG		4*stringLength+laserStart					;"2" laser string
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"2" laser string end
				ORG		5*stringLength+laserStart					;"W" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$1A								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$1A								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$1A								;X
				FCB		$D0								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"W" laser string end
				ORG		6*stringLength+laserStart					;"S" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"S" laser string end
				ORG		7*stringLength+laserStart					;"X" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"X" laser string end
				ORG		8*stringLength+laserStart					;"3" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$1A								;X
				FCB		$34								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$70								;"3" laser string end
				ORG		9*stringLength+laserStart					;"E" laser string
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"E" laser string end
				ORG		10*stringLength+laserStart					;"D" laser string
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$70								;"D" laser string end
				ORG		11*stringLength+laserStart					;"C" laser string
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$70								;"C" laser string end
				ORG		12*stringLength+laserStart					;"4" laser string
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$1A								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$D0								;Dim
				FCB		$70								;"4" laser string end
				ORG		13*stringLength+laserStart					;"R" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"R" laser string end
				ORG		14*stringLength+laserStart					;"F" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"F" laser string end
				ORG		15*stringLength+laserStart					;"V" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$0D								;X
				FCB		$1A								;Y
				FCB		$0D								;X
				FCB		$0D								;Y
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$0D								;Y
				FCB		$27								;X
				FCB		$1A								;Y
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"V" laser string end
				ORG		16*stringLength+laserStart					;"5" laser string
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$27								;X
				FCB		$34								;Y
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"5" laser string end
				ORG		17*stringLength+laserStart					;"T" laser string
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$1A								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"T" laser string end
				ORG		18*stringLength+laserStart					;"G" laser string
				FCB		$1A								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$D0								;Dim
				FCB		$70								;"G" laser string end
				ORG		0+32*stringLength+laserStart					;"6" laser string
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$1A								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"6" laser string end
				ORG		1+32*stringLength+laserStart					;"Y" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$1A								;X
				FCB		$1A								;Y
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$1A								;X
				FCB		$1A								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"Y" laser string end
				ORG		2+32*stringLength+laserStart					;"H" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"H" laser string end
				ORG		4+32*stringLength+laserStart					;"7" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$0D								;X
				FCB		$1A								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"7" laser string end
				ORG		5+32*stringLength+laserStart					;"U" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"U" laser string end
				ORG		6+32*stringLength+laserStart					;"J" laser string
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$0D								;Y
				FCB		$27								;X
				FCB		$0D								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$1A								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"J" laser string end
				ORG		7+32*stringLength+laserStart					;"B" laser string
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"B" laser string end
				ORG		8+32*stringLength+laserStart					;"8" laser string
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$1A								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"8" laser string end
				ORG		9+32*stringLength+laserStart					;"I" laser string
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$1A								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"I" laser string end
				ORG		10+32*stringLength+laserStart					;"K" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"K" laser string end
				ORG		11+32*stringLength+laserStart					;"N" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$D0								;Dim
				FCB		$70								;"N" laser string end
				ORG		12+32*stringLength+laserStart					;"9" laser string
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$1A								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$1A								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$34								;Y
				FCB		$0D								;X
				FCB		$27								;Y
				FCB		$34								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"9" laser string end
				ORG		13+32*stringLength+laserStart					;"O" laser string
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$D0								;Dim
				FCB		$70								;"O" laser string end
				ORG		14+32*stringLength+laserStart					;"L" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"L" laser string end
				ORG		15+32*stringLength+laserStart					;"M" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$1A								;X
				FCB		$34								;Y
				FCB		$1A								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$1A								;X
				FCB		$34								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"M" laser string end
				ORG		16+32*stringLength+laserStart					;"0" laser string
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$B0								;Bright
				FCB		$0D								;X
				FCB		$00								;Y
				FCB		$27								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$0D								;X
				FCB		$4E								;Y
				FCB		$00								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$D0								;Dim
				FCB		$70								;"0" laser string end
				ORG		17+32*stringLength+laserStart					;"P" laser string
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$B0								;Bright
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$27								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$34								;X
				FCB		$34								;Y
				FCB		$27								;X
				FCB		$27								;Y
				FCB		$00								;X
				FCB		$27								;Y
				FCB		$D0								;Dim
				FCB		$70								;"P" laser string end
				ORG		18+32*stringLength+laserStart					;"Z" laser string
				FCB		$00								;X
				FCB		$4E								;Y
				FCB		$B0								;Bright
				FCB		$34								;X
				FCB		$4E								;Y
				FCB		$34								;X
				FCB		$41								;Y
				FCB		$00								;X
				FCB		$0D								;Y
				FCB		$00								;X
				FCB		$00								;Y
				FCB		$34								;X
				FCB		$00								;Y
				FCB		$D0								;Dim
				FCB		$70								;"Z" laser string end
				ORG		animationStart							;Animation
				FCB		$D0								;Dim
				FCB		$70								;Animation end

				ORG		$FFF2								;ISR vector
				FDB		ISR								;ISR address
				ORG		$FFFE								;Reset vector
				FDB		$E000								;Reset address
* ;EEPROM end 