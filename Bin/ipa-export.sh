#!/bin/sh

source ./settings.sh


# Arichiveされる全体パス
buildName=$1
archivePath="${archiveDir}/$scheme-$buildName.xcarchive"

# IPA Build Process を実行するシェルスクリプト
isRelease=$2
if [[ $isRelease == $positive_value ]]; then
    originExportOptionPlist=$originExportOptionPlistRelease;
else
    originExportOptionPlist=$originExportOptionPlistAdHoc;
fi

if [[ ! -e $originExportOptionPlist ]]; then
    echo "Not Exist Origin";
    exit 1;
fi

exportOptionPlist=${projectDir}/${build_tag}/${targetExportOptionPlist}
cp $originExportOptionPlist $exportOptionPlist

echi $exportOptionPlist

if [[ ! -e $exportOptionPlist ]]; then
    echo "Not Exist Target";
    exit 1;
fi

# ipaファイルの作成
xcodebuild \
        -exportArchive -exportOptionsPlist $exportOptionPlist \
        -archivePath $archivePath\
        -exportPath "$ipaDir/$buildName" \
exit 1;
