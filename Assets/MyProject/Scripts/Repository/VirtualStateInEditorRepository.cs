using System;
using UnityEngine;
using Choi.MyProj.Domain.Editor;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.Repository.Editor
{
#if UNITY_EDITOR
    /// <summary>
    /// Editor での Virtual State 変更、取得の Repository 
    /// </summary>
    public class VirtualStateInEditorRepository : IVirtualStateInEditor
    {
        /// <summary>
        /// 取得
        /// </summary>
        /// <returns><Editorで指定されているモード/returns>
        public VirtualState Get()
        {
            var intVal = PlayerPrefs.GetInt(KeyValue.VirtualStateKeyValue, (int)VirtualState.Normal);
            return (VirtualState)Enum.ToObject(typeof(VirtualState), intVal);
        }

        /// <summary>
        /// 変更
        /// </summary>
        /// <param name="value">Editorで指定するモード</param>
        /// <returns>成功/失敗</returns>
        public bool Set(VirtualState value)
        {
            PlayerPrefs.SetInt(KeyValue.VirtualStateKeyValue, (int)value);
            return PlayerPrefs.GetInt(KeyValue.VirtualStateKeyValue, (int)VirtualState.Normal) == (int)value;
        }
    }
#endif
}