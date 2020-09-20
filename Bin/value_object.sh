#!/bin/sh

#########################################
# シェルで本体ビルド時必要な共通変数をまとめ
#########################################
# プロジェクトルートパス指定
parent_path=$(cd $(dirname $0)/..;pwd)
# 現在の時間指定
curr_time=$(date +"%Y%m%d_%H%M")

# Target UnityEditor Version
target_version="2019.4.10f1"
# Unity Hub Editorパス
dir=$"/Applications/Unity/Hub/Editor/$target_version/"
# ファイル名
full_path=$dir"Unity.app"
# Unity Hub慶祐Unityを起動する全体パス
launch=$"$full_path/Contents/MacOS/Unity"
# ビルド成功の場合Logファイルに記載されるべきのメッセージ
successed_key_msg="Result:Succeeded"
# 文字列色付けのためのESCコード
ESC=$(printf '\033')
# 文字列色付け:太字;緑
color_start_successed="[1;32m"
# 文字列色付け:太字;赤
color_start_failed="[1;31m"
# 文字列色付け、太文字を終わらせるコード
color_end_code="[m"

# Buid Tag Parameter Default Value(Jenkinsなどによるビルド番号)
default_build_tag="_local_$curr_time"


#########################################
# シェルで Xcode to IPA 吐き出しに必要な変数
#########################################
# XCode Projectが保存されるルートパス
projectDir=$parent_path"/Builds/Rom/iOS"
# XCodeでのスキマ名
scheme="Unity-iPhone"
# Arichiveされるディレクトリー
archiveDir="${projectDir}/Archive"
# IPAファイルを
ipaDir="${projectDir}/IPA"

login_id="aaa@bbb.ccc"
app_passWord="696969"

# ExportOptionPlist(AdHoc)
originExportOptionPlistAdHoc="ExportOptionsAdHoc.plist"
# ExportOptionPlist(App-Store)
originExportOptionPlistRelease="ExportOptionsAppStore.plist"
# IPAファイル出力時実際使うExportPlistの名前
targetExportOptionPlist="ExportOptions.plist"
# Provisioning Profile 値(ビルドマシン基準)
provisioningProfile="provisioning-provisioning-provisioning-provisioning-provisioning"

# ポジティブ値定義
positive_value="true"
