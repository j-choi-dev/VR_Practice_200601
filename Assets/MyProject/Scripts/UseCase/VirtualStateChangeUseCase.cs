using System.Threading.Tasks;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Domain.Editor;
using IRepository = Choi.MyProj.Domain.Editor.IVirtualStateInEditor;

namespace Choi.MyProj.UseCase.System
{
#if UNITY_EDITOR
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