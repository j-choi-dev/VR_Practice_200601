using System.Threading.Tasks;
using IRepository = Choi.MyProj.Domain.Build.AssetBundle.IAssetBundleBuildRepository;
using Choi.MyProj.Repository.Editor.Build.AssetBundle;
using Choi.MyProj.UseCase.Editor.Build.AssetBundle;
using UnityEditor;

namespace Choi.MyProj.Interface.Editor.Build.AssetBundle
{
#if UNITY_EDITOR
    /// <summary>
    /// 外側で呼び出すAssetBundleビルドスクリプト
    /// </summary>
    public static class AssetBundleBuildAdapter
    {
        /// <summary>
        /// ビルドの具象が定義されているレポジトリー
        /// </summary>
        private static IRepository m_repository;

        /// <summary>
        /// Executes the asset bundle build.
        /// </summary>
        /// <returns>The asset bundle build.</returns>
        public static async Task<bool> ExecuteAssetBundleBuild()
        {
            m_repository = new AssetBundleBuildRepository();
            var usecase = new AssetBundleBuildUseCase(m_repository);
            var result = await usecase.Execute(EditorUserBuildSettings.activeBuildTarget);
            return result;
        }
    }
#endif
}