using System.Threading.Tasks;
using UnityEditor;
using Choi.MyProj.Domain.Editor.Build.Rom;
using Choi.MyProj.Repository.Editor.Build.Rom;
using System;
using System.IO;
using System.Linq;
#if UNITY_EDITOR && UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Choi.MyProj.Repository.Editor.Build.Rom
{
#if UNITY_EDITOR && UNITY_IOS
    /// <summary>
    /// iOSでの Rom Build Repository 具象クラス
    /// </summary>
    /// Auth : @Choi
    public sealed class RomBuildIosRepository : RomBuildRepository
    {
        /// <summary>
        /// The rom file suffix.
        /// </summary>
        protected override string RomFileSuffix => string.Empty;

        /// <summary>
        /// ビルドターゲット
        /// </summary>
        protected override BuildTarget BuildTarget => BuildTarget.iOS;

        /// <summary>
        /// ビルドターゲットグループ
        /// </summary>
        protected override BuildTargetGroup BuildTargetGroup => BuildTargetGroup.iOS;

        /// <summary>
        /// The xcode project suffix.
        /// </summary>
        private const string XcodeProjectSuffix = "/Unity-iPhone.xcodeproj/project.pbxproj";

        /// <summary>
        /// The apple developer team identifier.
        /// </summary>
        private const string AppleDeveloperTeamID = "";

        /// <summary>
        /// The minimum sdk version.
        /// </summary>
        private const string MinimumSdkVersion = "9.0";

        /// <summary>
        /// iOS Rom Build pre process.
        /// </summary>
        /// <returns>The pre process Result.</returns>
        /// <param name="buildNumber">Build number.</param>
        protected override async Task<bool> PlatformPreProcess(int buildNumber, BuildMode buildMode)
        {
            Debug.Log("Start iOS PlatformPreProcess");
            PlayerSettings.iOS.buildNumber = buildNumber.ToString();
            // Unity > Preference > External Tools > Automatically Signにチェック
            PlayerSettings.iOS.appleEnableAutomaticSigning = true;
            // Unity > Preference > External Tools > Automatic Signing Team Idに自身のチームIDを入力
            PlayerSettings.iOS.appleDeveloperTeamID = AppleDeveloperTeamID;
            PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
            PlayerSettings.iOS.targetOSVersionString = MinimumSdkVersion;
            return true;
        }

        /// <summary>
        /// iOS Rom Build post process.
        /// </summary>
        /// <returns>The post process Result.</returns>
        protected override async Task<bool> PlatformPostProcess(IBuildInfo buildInfo)
        {
            Debug.Log("Start iOS PlatformPostProcess");

            // PList ファイル編集
            var projectPath = PBXProject.GetPBXProjectPath(GetDistPath(buildInfo));
            if (!File.Exists(projectPath))
            {
                Debug.LogError($"[Choi] Not Exist projectPath :: {projectPath}");
                return false;
            }
            var pbxProject = new PBXProject();
            pbxProject.ReadFromString(File.ReadAllText(projectPath));

#if UNITY_2019_3_OR_NEWER
            // Ref) https://support.unity3d.com/hc/en-us/articles/207942813-How-can-I-disable-Bitcode-support-
            var targetGuids = new string[2] { pbxProject.GetUnityMainTargetGuid(), pbxProject.GetUnityFrameworkTargetGuid() };
            // PList設定 : iOSアプリの交換性のため、BITCODEをOFFにする。
            pbxProject.SetBuildProperty(targetGuids, "ENABLE_BITCODE", "NO");
#else
            var targetGUID = pbxProject.TargetGuidByName(PBXProject.GetUnityTargetName());
            // PList設定 : iOSアプリの交換性のため、BITCODEをOFFにする。
            project.SetBuildProperty(targetGUID, " ENABLE_BITCODE ", " NO ");
#endif

            File.WriteAllText(projectPath, pbxProject.WriteToString());
            return true;
        }

        /// <summary>
        /// iOSのRomFileを収納するパスを取得
        /// </summary>
        /// <returns>The dist path.</returns>
        /// <param name="buildInfo">Info.</param>
        protected override string GetDistPath(IBuildInfo buildInfo)
        {
            return $"Builds/Rom/iOS/{buildInfo.FileNameByVersion}{RomFileSuffix}";
        }
    }
#endif
}