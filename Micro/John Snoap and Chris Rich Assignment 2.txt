* John Snoap and Chris Rich
* Intro to Microprocessors
* Assignment 2

* Variables
start	EQU	$E000
finish	EQU	$FFFE

* Main				COMMENTS
	ORG	start		Start program
	
	LDX	#$FF00		Load this number:  $FF00 -> X
	LDY	#$77		Load this number:  $77 -> Y
nextLnS	LDAA	0,X		Load X -> Accumulator
	STAA	0,Y		Store Accumulator -> Y
	CPX	#$FF27		Compare X to this number:  $FF27
	BEQ	theEnd		If (X == $FF27) go to "theEnd"
	INX			else X++
	DEY			Y--
	BRA	nextLnS		Go to "nextLnS" (next Load and Store)
theEnd	BRA	theEnd		Finish program

	ORG	finish
	FDB	start