#!/bin/sh
# Unityをサイレントモードとして起動し、XCode の本体と AssetBundle のビルドを実行する Bash Shell スクリプト

cd $(dirname $0)
source ./value_object.sh

while getopts ":t:" opt; do
    case $opt in
        t) build_tag="$OPTARG"
        ;;
    esac
done

if [ -z "$build_tag" ]; then
    build_tag="release"$default_build_tag;
fi

# Unity Hub or ターゲットバージョンが存在するのかチェック、なければ終了
if [ ! -e $dir -o ! -e $full_path ]; then
    echo "[CHOI : Build All for Release Process(iOS)] Pre Check :: $target_version Not Exist."
    exit 1;
fi

# ログファイルが生成されるパス指定
log_path=$parent_path"/Builds/Rom/Log/iOS/build_iOS_rom_"$curr_time"_"$build_tag"_log.txt"

echo ">>>> [CHOI : Build All for Release Process(iOS)] build start! : build_tag=$build_tag"

# UnityHubによって、Unity側のビルドスクリプト実行
$launch -projectPath $parent_path -executeMethod Choi.MyProj.API.Editor.Build.BuildAPI.BuildAllDevIos -batchmode -quit -logFile $log_path -buildTarget iOS /build_tag $build_tag

 # 成功時の決まったキーワードがあるかをチェックして結果を色付けて表示
if echo "$(<$log_path)" | grep -sq $successed_key_msg; then
    echo "${ESC}"$color_start_successed">>>> [CHOI] XCode build complete by SUCCESSED !!!"${ESC}$color_end_code
else
    echo "${ESC}"$color_start_failed">>>> [CHOI] XCode build complete by FAILED !!!"${ESC}$color_end_code
    exit 1;
fi

archiveResult=$(source ./xcode-archive.sh $build_tag)
if echo $archiveResult | grep -sq 'ARCHIVE SUCCEEDED'; then
    echo "${ESC}"$color_start_successed">>>> [CHOI] Archive complete by SUCCESSED !!!"${ESC}$color_end_code
else
    echo "${ESC}"$color_start_failed">>>> [CHOI] Archive complete by FAILED !!!"${ESC}$color_end_code
    exit 1;
fi

exportResult=$(source ./ipa-export.sh $build_tag $positive_value)
if echo $exportResult | grep -sq 'EXPORT SUCCEEDED'; then
    echo "${ESC}"$color_start_successed">>>> [CHOI] Export IPA complete by SUCCESSED !!!"${ESC}$color_end_code
else
    echo "${ESC}"$color_start_failed">>>> [CHOI] Export IPA complete by FAILED !!!"${ESC}$color_end_code
    exit 1;
fi
ipa_path=$ipaDir"/"$build_tag"/Unity-iPhone.ipa"
xcrun altool --validate-app -f $ipa_path -t ios -u $login_id -p $app_passWord
xcrun altool --upload-app -f $ipa_path -t ios -u $login_id -p $app_passWord
# 正常終了コードを返して終了
echo ">>>> [CHOI : Build All for Release Process(iOS)] IPA Export Finished!"
exit 0;