using System;
using UnityEngine;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.Domain.Editor
{
#if UNITY_EDITOR
    /// <summary>
    /// Editor での Virtual State 変更、取得の Interface 
    /// </summary>
    public interface IVirtualStateInEditor
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <returns><Editorで指定されているモード/returns>
        VirtualState Get();

        /// <summary>
        /// 変更
        /// </summary>
        /// <param name="value">Editorで指定するモード</param>
        /// <returns>成功/失敗</returns>
        bool Set(VirtualState value);
    }
#endif
}