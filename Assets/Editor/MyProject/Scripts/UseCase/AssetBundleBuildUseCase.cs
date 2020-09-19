using System.Threading.Tasks;
using IRepository = Choi.MyProj.Domain.Build.AssetBundle.IAssetBundleBuildRepository;
using UnityEditor;

namespace Choi.MyProj.UseCase.Editor.Build.AssetBundle
{
#if UNITY_EDITOR
    public class AssetBundleBuildUseCase : UseCase<bool, BuildTarget>
    {
        /// <summary>
        /// 手順が適宜されているクラス
        /// </summary>
        private IRepository m_repository;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="repository">Repository.</param>
        public AssetBundleBuildUseCase(IRepository repository)
        {
            m_repository = repository;
        }

        /// <summary>
        /// AssetBundleビルド実行
        /// </summary>
        /// <returns>The impl.</returns>
        /// <param name="protocol">Protocol.</param>
        protected override async Task<bool> ExecuteImpl(BuildTarget protocol)
        {
            return await m_repository.BuildAssetBundle(protocol);
        }
    }
#endif
}