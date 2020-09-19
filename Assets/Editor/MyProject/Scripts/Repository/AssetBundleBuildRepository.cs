using System.Threading.Tasks;
using UnityEditor;
using Choi.MyProj.Domain.Build.AssetBundle;

namespace Choi.MyProj.Repository.Editor.Build.AssetBundle
{
#if UNITY_EDITOR
    /// <summary>
    /// Asset bundle build 具象
    /// </summary>
    public sealed class AssetBundleBuildRepository : IAssetBundleBuildRepository
    {
        /// <summary>
        /// AssetBundleビルド実行
        /// </summary>
        /// <returns>The asset bundle.</returns>
        /// <param name="buildTarget">Build target.</param>
        public async Task<bool> BuildAssetBundle(BuildTarget buildTarget)
        {
            // TODO 今後差分管理のため、今回ビルドしたAssetBundleをターゲットバージョンを基準として作った別のパスにコピーするロジック追加する必要がある。 // @Choi
            Debug.Log("TODO BuildAssetBundle() 未実装");

            return true;
        }
    }
#endif
}