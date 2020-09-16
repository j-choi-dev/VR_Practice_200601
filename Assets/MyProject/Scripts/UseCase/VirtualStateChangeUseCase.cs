using System.Threading.Tasks;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.UseCase.System
{
#if UNITY_EDITOR
    using IRepository = Domain.Editor.IVirtualStateInEditor;

    /// <summary>
    /// Editor での Virtual State 変更する UseCase
    /// </summary>
    public class VirtualModeChangeInEditorUseCase : UseCase<bool, VirtualState>
    {
        /// <summary>
        /// Repository
        /// </summary>
        private IRepository m_repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Repository</param>
        public VirtualModeChangeInEditorUseCase(IRepository repository)
        {
            m_repository = repository;
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="state">VirtualState</param>
        /// <returns>Result</returns>
        protected override async Task<bool> ExecuteImpl(VirtualState state)
        {
            var result = m_repository.Set(state);
            return result;
        }
    }
#endif
}