* John Snoap
* Intro to Microprocessors
* Test Program

* Variables
start	EQU	$E000
finish	EQU	$FFFE

* Main
	ORG	start	Program starts
	
	LDAA	#$10
	ADDA	#$20
	STAA	$30
here	BRA	here

	ORG	finish	Program finishes
	FDB	start