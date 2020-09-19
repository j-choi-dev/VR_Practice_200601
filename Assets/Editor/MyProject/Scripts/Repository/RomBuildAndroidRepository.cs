using System.Threading.Tasks;
using UnityEditor;
using Choi.MyProj.Domain.Editor.Build.Rom;
using Choi.MyProj.Repository.Editor.Build.Rom;
using System;
using System.IO;
using System.Linq;

namespace Choi.MyProj.Repository.Editor.Build.Rom
{
#if UNITY_EDITOR && UNITY_ANDROID
    /// <summary>
    /// Androidでの Rom Build Repository 具象クラス
    /// </summary>
    /// Auth : @Choi
    public sealed class RomBuildAndroidRepository : RomBuildRepository
    {
        /// <summary>
        /// ビルドターゲット
        /// </summary>
        protected override BuildTarget BuildTarget => BuildTarget.Android;

        /// <summary>
        /// ビルドターゲットグループ
        /// </summary>
        protected override BuildTargetGroup BuildTargetGroup => BuildTargetGroup.Android;

        /// <summary>
        /// The rom file suffix.
        /// </summary>
        /// <remarks>
        /// TODO :: 今後*.aabに変更される予定 @Choi
        /// </remarks>
        protected override string RomFileSuffix => ".apk";

        /// <summary>
        /// AABファイルの拡張子
        /// </summary>
        protected string RomFileSuffixAab => ".aab";

        /// <summary>
        /// Android ビルドアーキテクチャー ターゲット : ARM64とARMv7
        /// </summary>
        private const AndroidArchitecture m_architecture = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        /// <summary>
        /// Target Android SDK Minimum Version : SDK23(Android 6.0 Marshmellow)
        /// </summary>
        /// <remarks>
        /// 2020.02.26基準Editorの設定基準
        /// </remarks>
        private const AndroidSdkVersions m_minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;

        /// <summary>
        /// Target Android SDK Target Version : SDK23(Android 9.0 Pie)
        /// </summary>
        /// <remarks>
        /// 2020.02.26基準Editorの設定基準
        /// </remarks>
        private const AndroidSdkVersions m_targetSdkVersion = AndroidSdkVersions.AndroidApiLevel28;

        /// <summary>
        /// Android Rom Build pre process.
        /// </summary>
        /// <returns>The pre process Result.</returns>
        /// <param name="buildNumber">Build number.</param>
        protected override async Task<bool> PlatformPreProcess(int buildNumber, BuildMode buildMode)
        {
            Debug.Log("Start Android PlatformPreProcess");

            // 通常アプリケーションではなくゲームアプリケーションとして公開
            PlayerSettings.Android.androidIsGame = true;

            // Target Architecture 設定
            PlayerSettings.Android.targetArchitectures = m_architecture;

            // Target API Version 設定
            PlayerSettings.Android.minSdkVersion = m_minSdkVersion;
            PlayerSettings.Android.targetSdkVersion = m_targetSdkVersion;

            PlayerSettings.Android.bundleVersionCode = buildNumber;

            // AABビルド指定
            EditorUserBuildSettings.buildAppBundle = buildMode == BuildMode.Release || buildMode == BuildMode.PreReleaseAAB ? true : false;
            if (buildMode != BuildMode.Dev)
            {
                return true;
            }
            PlayerSettings.Android.useAPKExpansionFiles = buildMode != BuildMode.Dev ? true : false;
            var result = DoReleaseOnlyProcess();
            return result;
        }

        /// <summary>
        /// Android Rom Build post process.
        /// </summary>
        /// <returns>The post process Result.</returns>
        protected override async Task<bool> PlatformPostProcess(IBuildInfo buildInfo)
        {
            Debug.Log("Start Android PlatformPostProcess");

            // キーストアー情報リセット
            if(buildInfo.BuildMode != BuildMode.Release)
            {
                return true;
            }
            PlayerSettings.Android.keystoreName = string.Empty;
            PlayerSettings.Android.keystorePass = string.Empty;
            PlayerSettings.Android.keyaliasName = string.Empty;
            PlayerSettings.Android.keyaliasPass = string.Empty;

            return true;
        }

        /// <summary>
        /// AndroidのRomFileを収納するパスを取得
        /// </summary>
        /// <returns>The dist path.</returns>
        /// <param name="buildInfo">Info.</param>
        protected override string GetDistPath(IBuildInfo buildInfo)
        {
            return buildInfo.BuildMode == BuildMode.Release || buildInfo.BuildMode == BuildMode.PreReleaseAAB ?
                    $"Builds/Rom/Android/{buildInfo.FileNameByVersion}{RomFileSuffixAab}":
                    $"Builds/Rom/Android/{buildInfo.FileNameByVersion}{RomFileSuffix}";
        }

        /// <summary>
        /// 正式リリーズ版のみのプロセスを十sこう
        /// </summary>
        /// <returns>成功/失敗</returns>
        private bool DoReleaseOnlyProcess()
        {
            // キーストアーパス取得、File存在するかチェック

            // キーストアーアタッチ
            return true;
        }
    }
#endif
}