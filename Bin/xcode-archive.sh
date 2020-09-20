#!/bin/sh
# Xcode Archive Process を実行するシェルスクリプト

source ./settings.sh

buildName=$1
# xcodeproject Path
projectPath="${projectDir}/${buildName}/${scheme}.xcodeproj"
# Arichiveされる全体パス
archivePath="${archiveDir}/$scheme-$buildName.xcarchive"
mkdir -p $archivePath

# ARCHIVE
xcodebuild -project $projectPath \
    -scheme $scheme \
    archive -archivePath $archivePath
exit 1;