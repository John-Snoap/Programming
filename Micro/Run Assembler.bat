REM John Snoap
REM Intro to Microprocessors Run Assembler File

REM Set file name and file path
set fileName=CurrentUp

set filePath=C:\Micro

REM Change directory
cd "%filePath%"

REM Clear the screen
cls

REM Run The Assembler!
as11 "%fileName%.asm" -l >"%fileName%.lst"