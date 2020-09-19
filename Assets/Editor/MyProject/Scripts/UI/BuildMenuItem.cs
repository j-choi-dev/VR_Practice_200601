using UnityEditor;
using System.Threading.Tasks;
using Choi.MyProj.API.Editor.Build;

namespace Choi.MyProj.UI.Editor.Build
{
#if UNITY_EDITOR
    /// <summary>
    /// ビルド実行メニューのスクリプト
    /// </summary>
    /// Auth : @Choi Jaeyeon
    public static class BuildMenuItem
    {
        /// <summary>
        /// The priority dev rom build(1~10)
        /// </summary>
        private const int PriorityDevRomBuild = 1;

        /// <summary>
        /// The priority Pre release rom build(1~10)
        /// </summary>
        private const int PriorityPreReleaseRomBuild = 2;

        /// <summary>
        /// The priority release rom build(1~10)
        /// </summary>
        private const int PriorityReleaseRomBuild = 3;

        /// <summary>
        /// The priority of AssetBundle build(1~10)
        /// </summary>
        private const int PriorityAssetBundleBuild = 4;

        /// <summary>
        /// The priority of Dev All build(1~10)
        /// </summary>
        private const int PriorityDevAllBuild = 5;

        /// <summary>
        /// The priority of Pre Release All build(1~10)
        /// </summary>
        private const int PriorityPreReleaseAllBuild = 6;

        /// <summary>
        /// The priority of Pre Release All build(1~10)
        /// </summary>
        private const int PriorityReleaseAllBuild = 7;

#if UNITY_EDITOR && UNITY_ANDROID
        /// <summary>
        /// Only Rom Build menu (Dev mode)
        /// </summary>
        private const string BuildOnlyRomByDevModeMenuName = "ChoiTool/Build/Android Rom Only(Dev)";

        /// <summary>
        /// Only Rom Build menu (PreRelease mode)
        /// </summary>
        private const string BuildOnlyRomByPreReleaseApkModeMenuName = "ChoiTool/Build/Android Rom Only(PreRelease:APK)";

        /// <summary>
        /// Only Rom Build menu (PreRelease mode : AAB)
        /// </summary>
        private const string BuildOnlyRomByPreReleaseAabModeMenuName = "ChoiTool/Build/Android Rom Only(PreRelease:AAB)";

        /// <summary>
        /// Only Rom Build menu (Release mode)
        /// </summary>
        private const string BuildOnlyRomByReleaseModeMenuName = "ChoiTool/Build/Android Rom Only(Release:AAB)";

        /// <summary>
        /// The build only asset bundle menu name (android)
        /// </summary>
        private const string BuildAssetBundleMenuName = "ChoiTool/Build/Android AssetBundle Only";

        /// <summary>
        /// All Build menu (Dev mode)
        /// </summary>
        private const string BuildAllByDevModeMenuName = "ChoiTool/Build/Android All(Dev)";

        /// <summary>
        /// All Build menu (Release mode:APK)
        /// </summary>
        private const string BuildAllByPreReleaseModeApkMenuName = "ChoiTool/Build/Android All(PreRelease:APK)";

        /// <summary>
        /// All Build menu (Release mode:AAB)
        /// </summary>
        private const string BuildAllByPreReleaseModeAabMenuName = "ChoiTool/Build/Android Rom Only(PreRelease:AAB)";

        /// <summary>
        /// All Build menu (Release mode:AAB)
        /// </summary>
        private const string BuildAllByReleaseModeMenuName = "ChoiTool/Build/Android All(Release:AAB)";
        

        /// <summary>
        /// UnityEditorから本体ビルドだけ実行 (Release)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildOnlyRomByPreReleaseApkModeMenuName, priority = PriorityPreReleaseRomBuild)]
        private static async Task<bool> BuildOnlyRomPreReleaseAPK()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildOnlyRomPreReleaseAndroid();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorから本体ビルドだけ実行 (Release)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildOnlyRomByPreReleaseAabModeMenuName, priority = PriorityPreReleaseRomBuild)]
        private static async Task<bool> BuildOnlyRomPreReleaseAAB()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildOnlyRomPreReleaseAndroid(true);
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorから本体とAssetBundleビルド両方実行 (Release:APK)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildAllByPreReleaseModeApkMenuName, priority = PriorityPreReleaseAllBuild)]
        private static async Task<bool> BuildAllPreReleaseApk()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildAllPreReleaseAndroid(false);
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorから本体とAssetBundleビルド両方実行 (Release:APK)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildAllByPreReleaseModeAabMenuName, priority = PriorityPreReleaseAllBuild)]
        private static async Task<bool> BuildAllPreReleaseAab()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildAllPreReleaseAndroid(true);
            }
            Debug.LogError("Invalid Platform");
            return false;
        }
#elif UNITY_EDITOR && UNITY_IOS
        /// <summary>
        /// Only Rom Build menu (Dev mode)
        /// </summary>
        private const string BuildOnlyRomByDevModeMenuName = "ChoiTool/Build/iOS Rom Only(Dev)";

        /// <summary>
        /// Only Rom Build menu (Release mode)
        /// </summary>
        private const string BuildOnlyRomByPreReleaseModeMenuName = "";

        /// <summary>
        /// Only Rom Build menu (Release mode)
        /// </summary>
        private const string BuildOnlyRomByReleaseModeMenuName = "ChoiTool/Build/iOS Rom Only(Release)";

        /// <summary>
        /// The build only asset bundle menu name
        /// </summary>
        private const string BuildAssetBundleMenuName = "ChoiTool/Build/iOS AssetBundle Only";

        /// <summary>
        /// All Build menu (Dev mode)
        /// </summary>
        private const string BuildAllByDevModeMenuName = "ChoiTool/Build/iOS All(Dev)";

        /// <summary>
        /// All Build menu (Release mode)
        /// </summary>
        private const string BuildAllByPreReleaseModeMenuName = "";

        /// <summary>
        /// All Build menu (Release mode)
        /// </summary>
        private const string BuildAllByReleaseModeMenuName = "ChoiTool/Build/iOS All(Release)";
#else
        /// <summary>
        /// 無効なプラットフォーム
        /// </summary>
        private const string UnvalidPlatformMenuName =  "現在、モバイル向けプラットフォームではありません";
#endif


        #region Build Rom Only (Priority 割当 = 1~10)
        /// <summary>
        /// UnityEditorから本体ビルドだけ実行 (Dev)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildOnlyRomByDevModeMenuName, priority = PriorityDevRomBuild)]
        private static async Task<bool> BuildOnlyRomDev()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildOnlyRomDevAndroid();
                case BuildTarget.iOS:
                    return await BuildAPI.BuildOnlyRomDevIos();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }


        /// <summary>
        /// UnityEditorから本体ビルドだけ実行 (Release)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildOnlyRomByReleaseModeMenuName, priority = PriorityReleaseRomBuild)]
        private static async Task<bool> BuildOnlyRomRelease()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildOnlyRomReleaseAndroid();
                case BuildTarget.iOS:
                    return await BuildAPI.BuildOnlyRomReleaseIos();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorからAssetBundleビルドだけ実行
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildAssetBundleMenuName, priority = PriorityAssetBundleBuild)]
        private static async Task<bool> BuildOnlyAssetBundle()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildOnlyAssetBundleAndroid();
                case BuildTarget.iOS:
                    return await BuildAPI.BuildOnlyAssetBundleIos();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorから本体とAssetBundleビルド両方実行 (Dev)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildAllByDevModeMenuName, priority = PriorityDevAllBuild)]
        private static async Task<bool> BuildAllDev()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildAllDevAndroid();
                case BuildTarget.iOS:
                    return await BuildAPI.BuildAllDevIos();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }

        /// <summary>
        /// UnityEditorから本体とAssetBundleビルド両方実行 (Release:AAB)
        /// </summary>
        /// <returns>ビルド結果</returns>
        [MenuItem(BuildAllByReleaseModeMenuName, priority = PriorityReleaseAllBuild)]
        private static async Task<bool> BuildAllRelease()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return await BuildAPI.BuildAllReleaseAndroid();
                case BuildTarget.iOS:
                    return await BuildAPI.BuildAllReleaseIos();
            }
            Debug.LogError("Invalid Platform");
            return false;
        }
        #endregion
    }
#endif
}