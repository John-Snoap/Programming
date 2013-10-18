* John Snoap and Alex Orr
* Intro to Microprocessors
* Assignment 3

* Variables
start		EQU		$E000
reset		EQU		$FFFE
stringStart	EQU		$FF00

* Main							COMMENTS
		ORG		start			Start program

		BCLR		$10	%10000000	Clear address $10
		LDX		#stringStart		Put start of string $FF00 -> X
		LDAA		#1			1 -> Accumulator A
nextCompare	CMPA		0,X			Compare Accumulator A to X
		BNE		skipMask		If (A != X) go to "skipMask"
		BSET		$10	%10000000	Else mask result
skipMask	CPX		#$FF0F			Compare X to this number:  $FF0F
		BEQ		theEnd			If (X == $FF0F) goto "theEnd"
		STAA		$C0			Accumulator A -> location $C0
		LDD		$10			Result -> Accumulator D
		LSRD					Shift Accumulator D right
		STD		$10			Accumulator D -> location $10
		LDAA		$C0			Location $C0 -> Accumulator A
		INCA					Accumulator A++
		INX					X++
		BRA		nextCompare		Goto "nextCompare"
theEnd		BRA		theEnd			Finish program

		ORG		reset			Reset vector
		FDB		start			Reset address