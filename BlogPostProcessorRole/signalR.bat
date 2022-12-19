ECHO '**************************************'
ECHO 'Attempting to locate SignalR Utilities in:'
ECHO %~dp0Utilities
ECHO '**************************************'

DIR

FOR /R "%~dp0Utilities" %%G IN (signalR.exe) DO (
  IF EXIST %%G (
    SET TOOLPATH=%%G
    GOTO FOUND
  )
)
IF '%TOOLPATH%'=='' GOTO NOTFOUND

:FOUND
ECHO '**************************************'
ECHO 'Found SignalR in:'
ECHO %TOOLPATH%
ECHO '**************************************'


ECHO '**************************************'
ECHO 'executing signalR utilties'
ECHO '**************************************'

"%TOOLPATH%" ghp  /o:%2
GOTO :EOF

:NOTFOUND
ECHO signalR not found.
EXIT /B 1

