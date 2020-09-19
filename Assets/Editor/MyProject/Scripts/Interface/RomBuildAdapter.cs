using UnityEditor;
using System.Threading.Tasks;
using Choi.MyProj.Domain.Editor.Build.Rom;
using Choi.MyProj.Repository.Editor.Build.Rom;
using Choi.MyProj.UseCase.Editor.Build.Rom;

namespace Choi.MyProj.Interface.Editor.Build.Rom
{
#if UNITY_EDITOR
    /// <summary>
    /// 本体ビルドに必要な情報や設定を備えて手順通り実行するクラスに引き渡すクラス
    /// </summary>
    /// <remarks>
    /// Editorボタン or その他の手段(Batch Shellなど)ではこのスクリプトを使う
    /// </remarks>
    /// Auth : @Choi
    public static class RomBuildAdapter
    {
        /// <summary>
        /// Devモードビルドを実行する
        /// </summary>
        /// <returns>The dev mode rom buiild.</returns>
        /// <remarks>
        /// Editorボタン or その他の手段(Batch Shellなど)ではこのメソッドを呼ぶ
        /// </remarks>
        public static async Task<bool> ExecuteRomBuiildByDevMode(string buildTag)
        {
            // 手順を定義した手順クラスにDev/Release情報を渡す
            var usecase = CreateUseCase();
            var protocol = RomBuildUseCaseProtocol.CreateDebugMode(buildTag);
            Debug.Log($"ExecuteRomBuiildByDevMode() :: protocol.buildTag = {protocol.BuildTagKey}, protocol.isUseDebug = {protocol.IsUseDebugMode}, protocol.BuildMode = {protocol.BuildMode}");
            var result = await usecase.Execute(protocol);
            return result;
        }

        /// <summary>
        /// Release(APK)モードビルドを実行する
        /// </summary>
        /// <returns>The rom build by release mode.</returns>
        /// <remarks>
        /// Editorボタン or その他の手段(Batch Shellなど)ではこのメソッドを呼ぶ
        /// </remarks>
        public static async Task<bool> ExecuteRomBuildByPreReleaseMode(string buildTag, bool isDebug, bool isUseAabSuffix)
        {
            // 手順を定義した手順クラスにDev/Release情報を渡す
            var usecase = CreateUseCase();
            var protocol = isUseAabSuffix ?
                    RomBuildUseCaseProtocol.Create(BuildMode.PreReleaseAAB, buildTag, isDebug) :
                    RomBuildUseCaseProtocol.Create(BuildMode.PreRelease, buildTag, isDebug);
            Debug.Log($"ExecuteRomBuildByPreReleaseMode() :: protocol.buildTag = {protocol.BuildTagKey}, protocol.isUseDebug = {protocol.IsUseDebugMode}, protocol.BuildMode = {protocol.BuildMode}");
            var result = await usecase.Execute(protocol);
            return result;
        }

        /// <summary>
        /// Releaseモードビルドを実行する
        /// </summary>
        /// <returns>The rom build by release mode.</returns>
        /// <remarks>
        /// Editorボタン or その他の手段(Batch Shellなど)ではこのメソッドを呼ぶ
        /// </remarks>
        public static async Task<bool> ExecuteRomBuildByReleaseMode(string buildTag)
        {
            // 手順を定義した手順クラスにDev/Release情報を渡す
            var usecase = CreateUseCase();
            var protocol = RomBuildUseCaseProtocol.Create(BuildMode.Release, buildTag, false);
            Debug.Log($"ExecuteRomBuildByReleaseMode() :: protocol.buildTag = {protocol.BuildTagKey}, protocol.isUseDebug = {protocol.IsUseDebugMode}, protocol.BuildMode = {protocol.BuildMode}");
            var result = await usecase.Execute(protocol);
            return result;
        }

        /// <summary>
        /// OSによって必要なUseCaseを作成して返す
        /// </summary>
        /// <returns>The use case.</returns>
        private static RomBuildUseCase CreateUseCase()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
#if UNITY_EDITOR && UNITY_ANDROID
                case BuildTarget.Android:
                    return new RomBuildUseCase(new RomBuildAndroidRepository());
#elif UNITY_EDITOR && UNITY_IOS
                case BuildTarget.iOS:
                    return new RomBuildUseCase(new RomBuildIosRepository());
#endif
                default:
                    throw new System.Exception("本体ビルド失敗：無効なプラットフォームです。");
            }
        }
    }
#endif
}
