using UnityEngine;
using System.Threading.Tasks;
using UnityEditor;
using System.Linq;
using Choi.MyProj.Interface.Editor.Build.Rom;
using Choi.MyProj.Interface.Editor.Build.AssetBundle;

namespace Choi.MyProj.API.Editor.Build
{
#if UNITY_EDITOR
    /// <summary>
    /// Unity外側から実行するビルドスクリプトのクラス
    /// </summary>
    public static class BuildAPI
    {
        /// <summary>
        /// The success key massage.
        /// </summary>
        private const string SuccessKeyMassage = "Result:Succeeded";

        /// <summary>
        /// The success key massage.
        /// </summary>
        private const string FailKeyMassage = "Result:Failed";

        /// <summary>
        /// The build tag key.
        /// </summary>
        private const string BuildTagKey = "/build_tag";

        /// <summary>
        /// The use AAB key.
        /// </summary>
        private const string UseAabSuffix = "/use_aab";

    #region 外向け　本体のみ
        /// <summary>
        /// Builds the only rom dev android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomDevAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var args = System.Environment.GetCommandLineArgs();
            var buildTag = args.SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildOnlyRomDev(buildTagVal);
        }

        /// <summary>
        /// Builds the only rom dev ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomDevIos()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var args = System.Environment.GetCommandLineArgs();
            var buildTag = args.SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildOnlyRomDev(buildTagVal);
        }

        /// <summary>
        /// Builds the only rom release(APK) android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomPreReleaseAndroidByMenu(bool isUseAAB)
        {
            Debug.Log($"[CHOI] BuildOnlyRomPreReleaseAndroidByMenu({isUseAAB})");
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            return await BuildOnlyRomPreRelease(string.Empty, true, isUseAAB);
        }

        /// <summary>
        /// Builds the only rom release(APK) android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomPreReleaseAndroid()
        {
            Debug.Log($"[CHOI] BuildOnlyRomPreReleaseAndroid()");
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var args = System.Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                Debug.Log($" arg = {arg}");
            }
            var buildTag = args.SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            var useAab = args.SkipWhile(element => !element.Equals(UseAabSuffix)).Skip(1).FirstOrDefault();
            var _isUseAab = string.IsNullOrEmpty(useAab) ? false : (useAab.Equals("yes") || useAab.Equals("true")) ? true : false;
            Debug.Log($"[CHOI] buildTag = {buildTag}, useAab = {useAab}({_isUseAab})");
            return await BuildOnlyRomPreRelease(buildTagVal, true, _isUseAab);
        }

        /// <summary>
        /// Builds the only rom release android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomReleaseAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var args = System.Environment.GetCommandLineArgs();
            var buildTag = args.SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildOnlyRomRelease(buildTagVal);
        }

        /// <summary>
        /// Builds the only rom release ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyRomReleaseIos()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var args = System.Environment.GetCommandLineArgs();
            var buildTag = args.SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildOnlyRomRelease(buildTagVal);
        }
    #endregion

    #region 外向け AssetBundleのみ
        /// <summary>
        /// Builds the only AssetBundle dev android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyAssetBundleAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            return await BuildOnlyAssetBundle();
        }
        /// <summary>
        /// Builds the only rom dev ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildOnlyAssetBundleIos()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            return await BuildOnlyAssetBundle();
        }
    #endregion

    #region 外向け　AssetBundleと本体両方
        /// <summary>
        /// Builds the asset bundle and rom dev android.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildAllDevAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;

            Debug.Log($"[Choi] BuildAllDevAndroid() buildTag : {buildTagVal}, isUseDebugMode : {true}");
            return await BuildAllDev(buildTagVal, true);
        }

        /// <summary>
        /// Builds the asset bundle and rom dev ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildAllDevIos()
        {
            Debug.Log($"BuildAllDevIos()");
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildAllDev(buildTagVal, true);
        }

        /// <summary>
        /// Builds asset bundle and rom release android.(Shell/Jenkinsから)
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildAllPreReleaseAndroid(bool isUseAAB)
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            var useAab = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(UseAabSuffix)).Skip(1).FirstOrDefault();
            var _isUseAab = string.IsNullOrEmpty(useAab) ? false : (useAab.Equals("yes") || useAab.Equals("true")) ? true : false;
            return await BuildAllPreRelease(buildTagVal, true, isUseAAB || _isUseAab);
        }

        /// <summary>
        /// Builds the asset bundle and rom release ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildAllPreReleaseIos()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildAllPreRelease(buildTagVal, true, false);
        }

        /// <summary>
        /// Builds asset bundle and rom release android.(Editor or　Default)
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        /// <param name="isUseAabSuffix">AAB拡張子を使う/使わない</param>
        public static async Task<bool> BuildAllReleaseAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildAllRelease(buildTagVal);
        }

        /// <summary>
        /// Builds the asset bundle and rom release ios.
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        public static async Task<bool> BuildAllReleaseIos()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError("Invalid Platform");
                return false;
            }
            var buildTag = System.Environment.GetCommandLineArgs().SkipWhile(element => !element.Equals(BuildTagKey)).Skip(1).FirstOrDefault();
            var buildTagVal = string.IsNullOrEmpty(buildTag) ? string.Empty : buildTag;
            return await BuildAllRelease(buildTagVal);
        }
    #endregion

    #region 内部メソッド
        /// <summary>
        /// dev用本体だけビルド
        /// </summary>
        /// <returns>全体手順の最終的な成功/失敗結果</returns>
        private static async Task<bool> BuildOnlyRomDev(string buildTag)
        {
            var romResult = await RomBuildAdapter.ExecuteRomBuiildByDevMode(buildTag);
            if (!romResult)
            {
                Debug.Log($"[Choi] Rom Dev Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] Rom Dev Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// release用本体だけビルド
        /// </summary>
        /// <returns>The only rom release.</returns>
        private static async Task<bool> BuildOnlyRomPreRelease(string buildTag, bool isUseDebug, bool isUseAabSuffix)
        {
            var romResult = await RomBuildAdapter.ExecuteRomBuildByPreReleaseMode(buildTag, isUseDebug, isUseAabSuffix);
            if (!romResult)
            {
                Debug.Log($"[Choi] Rom Release Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] Rom PreRelease Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// release用本体だけビルド
        /// </summary>
        /// <returns>The only rom release.</returns>
        private static async Task<bool> BuildOnlyRomRelease(string buildTag)
        {
            var romResult = await RomBuildAdapter.ExecuteRomBuildByReleaseMode(buildTag);
            if (!romResult)
            {
                Debug.Log($"[Choi] Rom Release Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] Rom Release Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// AssetBundleだけビルド
        /// </summary>
        /// <returns>The only asset bundle dev.</returns>
        private static async Task<bool> BuildOnlyAssetBundle()
        {
            var bundleResult = await AssetBundleBuildAdapter.ExecuteAssetBundleBuild();
            if (!bundleResult)
            {
                Debug.LogError($"[Choi] AssetBundle Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] AssetBundle Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// AssetBundleと本体をDevモードとしてビルド
        /// </summary>
        /// <returns>The all dev.</returns>
        private static async Task<bool> BuildAllDev(string buildTag, bool isUseDebug)
        {
            var bundleResult = await AssetBundleBuildAdapter.ExecuteAssetBundleBuild();
            if (!bundleResult)
            {
                Debug.LogError($"[Choi] AssetBundle Build {FailKeyMassage}");
                return false;
            }
            Debug.Log("[Choi] AssetBundle Build Finished");
            var romResult = await RomBuildAdapter.ExecuteRomBuiildByDevMode(buildTag);
            if (!romResult)
            {
                Debug.LogError($"[Choi] Rom Dev Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] AssetBundle And Rom For Dev Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// AssetBundleと本体をリリースモードとしてビルド
        /// </summary>
        /// <returns>The all dev.</returns>
        private static async Task<bool> BuildAllPreRelease(string buildTag, bool isUseDebug, bool isUseAabSuffix)
        {
            var bundleResult = await AssetBundleBuildAdapter.ExecuteAssetBundleBuild();
            if (!bundleResult)
            {
                Debug.LogError($"[Choi] AssetBundle Build {FailKeyMassage}");
                return false;
            }
            var romResult = await RomBuildAdapter.ExecuteRomBuildByPreReleaseMode(buildTag, isUseDebug, isUseAabSuffix);
            if (!romResult)
            {
                Debug.LogError($"[Choi] Rom Pre Release Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] AssetBundle And Rom For Pre Release Build {SuccessKeyMassage}");
            return true;
        }

        /// <summary>
        /// AssetBundleと本体をリリースモードとしてビルド
        /// </summary>
        /// <returns>The all dev.</returns>
        private static async Task<bool> BuildAllRelease(string buildTag)
        {
            var bundleResult = await AssetBundleBuildAdapter.ExecuteAssetBundleBuild();
            if (!bundleResult)
            {
                Debug.LogError($"[Choi] AssetBundle Build {FailKeyMassage}");
                return false;
            }
            var romResult = await RomBuildAdapter.ExecuteRomBuildByReleaseMode(buildTag);
            if (!romResult)
            {
                Debug.LogError($"[Choi] Rom Release Build {FailKeyMassage}");
                return false;
            }
            Debug.Log($"[Choi] AssetBundle And Rom For Release Build {SuccessKeyMassage}");
            return true;
        }
    #endregion
    }
#endif
}