using System.Linq;
using UnityEditor;
using System.Threading.Tasks;
using Choi.MyProj.Domain.Editor.Build.Rom;
using Choi.MyProj.Domain.App;

namespace Choi.MyProj.Repository.Editor.Build.Rom
{
#if UNITY_EDITOR
    /// <summary>
    /// Rom build 具象クラス
    /// </summary>
    /// Auth : @Choi
    public abstract class RomBuildRepository : IRomBuildRepository
    {
        //TODO 今後リファクターして、YAMLなどに管理されるようにする予定
        #region 共通ビルド情報
        /// <summary>
        /// 本体についての圧縮オプション
        /// </summary>
        private const BuildOptions BuildOption = BuildOptions.None;
        #endregion

        /// <summary>
        /// Gets the rom file suffix.
        /// </summary>
        /// <value>The rom file suffix.</value>
        protected abstract string RomFileSuffix { get; }

        /// <summary>
        /// Builds the pre process.
        /// </summary>
        /// <returns>The pre process Result.</returns>
        public async Task<IBuildInfo> BuildPreProcess(RomBuildUseCaseProtocol buildProtocol)
        {
            Debug.Log("Start BuildPreProcess");
            var startAt = System.DateTime.Now.ToString("yyMMdd_HHmmss");
            var fileNameByVersion = buildProtocol.BuildTagKey;
            if (string.IsNullOrEmpty(fileNameByVersion))
            {
                Debug.LogError($"Invalid Version");
                //return buildInfo.Fail;
                fileNameByVersion = $"{buildProtocol.BuildMode}_{AppVersion.Value}_{startAt}";
            }

            var buildInfo = BuildInfo.Create(startAt, buildProtocol.BuildMode, fileNameByVersion);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android && EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                Debug.LogError($"Invalid Platform : now is {EditorUserBuildSettings.activeBuildTarget}");
                return buildInfo.Fail;
            }
            EditorUserBuildSettings.development = buildProtocol.IsUseDebugMode;

            // アプリの共通情報を設定
            PlayerSettings.companyName = TempValue.CompanyName;
            PlayerSettings.applicationIdentifier = TempValue.BundleID;
            var productName = string.Empty;
            switch (buildProtocol.BuildMode)
            {
                case BuildMode.Dev:
                    productName = $"{TempValue.ProductName}{TempValue.ProductNameInfo.Dev}";
                    break;
                case BuildMode.PreRelease:
                case BuildMode.PreReleaseAAB:
                    productName = $"{TempValue.ProductName}{TempValue.ProductNameInfo.PreRelease}";
                    break;
                case BuildMode.Release:
                    productName = $"{TempValue.ProductName}{TempValue.ProductNameInfo.Release}";
                    break;
            }
            PlayerSettings.productName = productName;
            PlayerSettings.bundleVersion = TempValue.VersionName;
            PlayerSettings.statusBarHidden = true;

            // 全プラットフォームにIL2CPPに適用
            PlayerSettings.SetScriptingBackend(BuildTargetGroup, ScriptingImplementation.IL2CPP);

            // 画面向きを強制的に縦に設定
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

            // TODO :: Set Define Symbol in Here
            Debug.Log("// TODO :: Set Define Symbol in Here");
            var definedSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup).Split(';');
            if (buildProtocol.IsUseDebugMode && !definedSymbols.Contains(ValueObject.UseDebugDefine))
            {
                Debug.Log($"[Choi] Attach USE_DEBUG");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup, string.Join(";", definedSymbols.Append(ValueObject.UseDebugDefine)));
            }
            if (!buildProtocol.IsUseDebugMode && !definedSymbols.Contains(ValueObject.UseReleaseDefine))
            {
                Debug.Log($"[Choi] Attach USE_RELEASE");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup, string.Join(";", definedSymbols.Append(ValueObject.UseReleaseDefine)));
            }

            var buildNumber = 0;
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    buildNumber = TempValue.VersionInfo.Android;
                    break;
                case BuildTarget.iOS:
                    buildNumber = TempValue.VersionInfo.IOS;
                    break;
            }
            var success = await PlatformPreProcess(buildNumber, buildProtocol.BuildMode);
            return success ? buildInfo : buildInfo.Fail;
        }

        /// <summary>
        /// Builds the main process.
        /// </summary>
        /// <returns>The main process Result.</returns>
        public async Task<IBuildInfo> BuildMainProcess(IBuildInfo buildInfo)
        {
            Debug.Log("Start BuildMainProcess");
            var path = GetDistPath(buildInfo);
            var scenes = EditorBuildSettings.scenes
                            .Where(scene => scene.enabled)
                            .Select(scene => scene.path)
                            .ToArray();
            var buildReport = BuildPipeline.BuildPlayer(scenes, path, BuildTarget, BuildOption);
            Debug.Log($"Build Result({buildReport.summary.result}) : {buildReport.summary.totalSize} byte");
            return buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded ? buildInfo : buildInfo.Fail;

        }

        /// <summary>
        /// Builds the post process.
        /// </summary>
        /// <returns>The post process Result.</returns>
        public async Task<IBuildInfo> BuildPostProcess(IBuildInfo buildInfo, RomBuildUseCaseProtocol buildProtocol)
        {
            Debug.Log("Start BuildPostProcess");
            EditorUserBuildSettings.development = false;

            var definedSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup).Split(';');
            if (buildProtocol.IsUseDebugMode && definedSymbols.Contains(ValueObject.UseDebugDefine))
            {
                definedSymbols.ToList().Remove(ValueObject.UseDebugDefine);
            }
            if (!buildProtocol.IsUseDebugMode && definedSymbols.Contains(ValueObject.UseReleaseDefine))
            {
                definedSymbols.ToList().Remove(ValueObject.UseReleaseDefine);
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup, string.Join(";", definedSymbols));
            var success = await PlatformPostProcess(buildInfo);
            return success ? buildInfo : buildInfo.Fail;
        }

        /// <summary>
        /// Platform 毎 pre process.
        /// </summary>
        /// <returns>The pre process Result.</returns>
        /// <param name="buildNumber">Build number.</param>
        protected abstract Task<bool> PlatformPreProcess(int buildNumber, BuildMode buildMode);

        /// <summary>
        /// Platform 毎 post process.
        /// </summary>
        /// <returns>The post process Result.</returns>
        protected abstract Task<bool> PlatformPostProcess(IBuildInfo buildInfo);

        /// <summary>
        /// RomFileを収納するパスを取得
        /// </summary>
        /// <returns>The dist path.</returns>
        /// <param name="buildInfo">Info.</param>
        protected abstract string GetDistPath(IBuildInfo buildInfo);

        /// <summary>
        /// ビルドターゲット
        /// </summary>
        protected abstract BuildTarget BuildTarget { get; }

        /// <summary>
        /// ビルドターゲットグループ
        /// </summary>
        protected abstract BuildTargetGroup BuildTargetGroup { get; }
    }
#endif
}