* John Snoap and Chris Rich
* Intro to Microprocessors
* Assignment 1

* Variables
start	EQU	$E000
finish	EQU	$FFFE

* Main				COMMENTS
	ORG	start		Start program
	
	LDX	#$EA00		Load $EA00 -> X
	CLRA			Load 0 -> A
next	ADDA	0,X		A + MX -> A
	CPX	#$EA03		Compare X to $EA03
	BEQ	done		If (X == $EA03) go to "done"
	INX			Else X++
	BRA	next		Go to "next"
done	STAA	$2F		Store result -> $2F
here	BRA	here		Finish program

	ORG	finish
	FDB	start