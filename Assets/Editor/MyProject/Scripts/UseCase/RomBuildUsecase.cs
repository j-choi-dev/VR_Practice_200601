using System.Threading.Tasks;
using Choi.MyProj.Domain.Editor.Build.Rom;
using IRepository = Choi.MyProj.Domain.Editor.Build.Rom.IRomBuildRepository;

namespace Choi.MyProj.UseCase.Editor.Build.Rom
{
#if UNITY_EDITOR
    /// <summary>
    /// Rom build の手順実装クラス
    /// </summary>
    /// Auth : @Choi
    public sealed class RomBuildUseCase : UseCase<bool, RomBuildUseCaseProtocol>
    {
        /// <summary>
        /// 手順が適宜されているクラス
        /// </summary>
        private IRepository m_repository;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="repository">Repository.</param>
        public RomBuildUseCase(IRepository repository)
        {
            m_repository = repository;
        }

        /// <summary>
        /// 外部向け、手順通り実行するメソッド
        /// </summary>
        /// <returns>The impl.</returns>
        /// <param name="buildMode">ビルドモード.</param>
        protected override async Task<bool> ExecuteImpl(RomBuildUseCaseProtocol protocol)
        {
            var preResult = await m_repository.BuildPreProcess(protocol);
            if (!preResult.Success)
            {
                Debug.LogError("PreProcess Failed");
                return false;
            }
            Debug.Log("[Choi] PostProcess Finished");
            var mainResult = await m_repository.BuildMainProcess(preResult);
            if (!mainResult.Success)
            {
                Debug.LogError("MainProcess Failed");
                return false;
            }
            Debug.Log("[Choi] MainProcess Finished");
            var postResult = await m_repository.BuildPostProcess(mainResult, protocol);
            if (!postResult.Success)
            {
                Debug.LogError("PostProcess Failed");
                return false;
            }
            Debug.Log("[Choi] PostProcess Finished");
            return true;
        }
    }
#endif
}