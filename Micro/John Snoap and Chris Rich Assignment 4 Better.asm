* John Snoap and Chris Rich
* Intro to Microprocessors
* Assignment 4

* Variables
start		EQU		$E000
reset		EQU		$FFFE
latchSet	EQU		$A000
latchReset	EQU		$B000
mainDelay	EQU		10
isrDelay	EQU		1

* Main							COMMENTS
		ORG		start			Start program
		
		CLI
		LDS		#$FF			$FF -> stackPointer
mainLoop	LDAA		#mainDelay		Delay 10 * 50us = 500us
		BSR		setLatch		Go to subrutine "setLatch"
		BSR		delayN50us		Go to subrutine "delayN50us"
		BSR		resetLatch		Go to subrutine "resetLatch"
		BSR		delayN50us		Go to subrutine "delayN50us"
		BRA		mainLoop		Go to "mainLoop"
		
ISR		LDAA		#isrDelay		Delay 50us
		BSR		setLatch		Go to subrutine "setLatch"
		BSR		delayN50us		Go to subrutine "delayN50us"
		BSR		resetLatch		Go to subrutine "resetLatch"
		BSR		delayN50us		Go to subrutine "delayN50us"
		RTI					Return from interupt

setLatch	PSHA					Save A
		LDAA		#1			1 -> A
		STAA		latchSet		A -> $A000 (set)
		PULA					Restore A
		RTS					Return from subrutine
		
		
resetLatch	PSHA					Save A
		LDAA		#1			1 -> A
		STAA		latchReset		A -> $B000 (reset)
		PULA					Restore A
		RTS					Return from subrutine

delayN50us	PSHA					Saves A
outerLoop	LDX		#7			Magic number for 50us
innerLoop	DEX					X--
		CPX		#0			Add 4 clock cycles
		BNE		innerLoop		If (X != 0) go to "innerLoop"
		DECA					A--
		BNE		outerLoop		If (A != 0) go to "outerLoop"
		PULA					Restore A
		RTS					Return from subrutine

		ORG		$FFF2			ISR vector
		FDB		ISR			ISR address
		ORG		reset			Reset vector
		FDB		start			Reset address