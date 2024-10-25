#!/bin/bash

# Get the latest commit hash, message, date, and author in one command
commitInfo=$(git log -1 --format="%H: %s by %an || %cd" --date=iso)

# Parse the commit hash and message using IFS
IFS=': ' read -r commitHash message <<< "$commitInfo"

# Output to last-commit.js
echo "console.log('lastCommit:', '$commitHash'); // $message" > amis/amis-fe/src/last-commit.js
