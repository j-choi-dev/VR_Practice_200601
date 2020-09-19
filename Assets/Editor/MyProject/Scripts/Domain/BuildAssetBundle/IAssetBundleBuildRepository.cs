using System.Threading.Tasks;
using UnityEditor;

namespace Choi.MyProj.Domain.Build.AssetBundle
{
#if UNITY_EDITOR
    /// <summary>
    /// Asset bundle build Interface
    /// </summary>
    public interface IAssetBundleBuildRepository
    {
        /// <summary>
        /// AssetBundleビルド実行
        /// </summary>
        /// <returns>The asset bundle.</returns>
        /// <param name="buildTarget">Build target.</param>
        Task<bool> BuildAssetBundle(BuildTarget buildTarget);
    }
#endif
}