#!/bin/sh
# Unity をサイレントモードとして起動し、Android の本体と AssetBundle のビルドを実行する Bash Shell スクリプト

cd $(dirname $0)
source ./value_object.sh

while getopts ":t:a:" opt; do
    case $opt in
        t) build_tag="$OPTARG"
        ;;
        a) use_aab="$OPTARG"
        ;;
    esac
done

if [ -z "$build_tag" ]; then
    build_tag="dev"$default_build_tag;
fi

if [ -z "$use_aab" ]; then
    use_aab="no";
fi

# Unity Hub or ターゲットバージョンが存在するのかチェック、なければ終了
if [ ! -e $dir -o ! -e $full_path ]; then
    echo "[CHOI : Build Only Rom For PreRelease Process(Android)] Pre Check :: $target_version Not Exist."
    exit 1;
fi

# ログファイルが生成されるパス指定
log_path=$parent_path"/Builds/Rom/Log/Android/build_android_rom_"$curr_time"_"$build_tag"_log.txt"

echo ">>>> [CHOI : Build Only Rom For PreRelease Process(Android)]: build_tag=$build_tag, use_aab=$use_aab build start!"

# UnityHubによって、Unity側のビルドスクリプト実行
$launch -projectPath $parent_path -executeMethod Choi.MyProj.API.Editor.Build.BuildAPI.BuildOnlyRomPreReleaseAndroid -batchmode -quit -logFile $log_path -buildTarget android /build_tag $build_tag /use_aab $use_aab
 
 # 成功時の決まったキーワードがあるかをチェックして結果を色付けて表示
if echo "$(<$log_path)" | grep -sq $successed_key_msg; then
    echo "${ESC}"$color_start_successed">>>> [CHOI] build complete by SUCCESSED !!!"${ESC}$color_end_code
else
    echo "${ESC}"$color_start_failed">>>> [CHOI] build complete by FAILED !!!"${ESC}$color_end_code
    exit 1;
fi

# 正常終了コードを返して終了
echo ">>>> [CHOI] Build Only Rom For PreRelease (Android) : build Finished!"
exit 0