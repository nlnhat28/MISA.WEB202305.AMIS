@echo off
for /f "delims=" %%c in ('git log -1 --format^="%%H: %%cd" --date=iso') do (
    set commitHash=%%c
)
for /f "delims=" %%m in ('git log -1 --format^="%%s by %%an"') do (
    set message=%%m
)
echo console.log("lastCommit:", "%commitHash%"); //%message% > amis/amis-fe/src/last-commit.js
set commitHash=
set message=

