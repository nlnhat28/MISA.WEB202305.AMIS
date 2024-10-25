@REM @echo off
@REM for /f "delims=" %%c in ('git log -1 --format^="%%H: %%cd" --date=iso') do (
@REM     set commitHash=%%c
@REM )
@REM for /f "delims=" %%m in ('git log -1 --format^="%%s by %%an"') do (
@REM     set message=%%m
@REM )
@REM echo console.log("lastCommit:", "%commitHash%"); // %message% > amis/amis-fe/src/last-commit.js
@REM set commitHash=
@REM set message=

@echo off
setlocal enabledelayedexpansion

rem Get the latest commit hash and message in one command
for /f "tokens=1* delims=:" %%c in ('git log -1 --format^="%%H: %%s || %%cd || %%an" --date=iso') do (
    set commitHash=%%c
    set message=%%d
)

rem Output to last-commit.js
echo console.log("lastCommit:", "!commitHash!"); // !message! > amis/amis-fe/src/last-commit.js

rem Clear variables
set commitHash=
set message=

endlocal
