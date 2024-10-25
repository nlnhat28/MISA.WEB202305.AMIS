@echo off
rem Get the latest commit message
for /f %%i in ('git rev-parse HEAD') do (
    set commitHash=%%i
)
for /f "delims=" %%j in ('git log -1 --format^=%%cd --date=iso') do (
    set commitHash=%commitHash%: %%j
)

rem Output to lastCommit.js
echo console.log("lastCommit:", "%commitHash%"); > amis/amis-fe/src/last-commit.js
set commitHash=

