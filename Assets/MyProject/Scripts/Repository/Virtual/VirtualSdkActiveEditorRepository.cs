using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.Virtual;
using Choi.MyProj.UI;
using Choi.MyProj.UI.Scene;

namespace Choi.MyProj.Repository.Virtual
{
#if UNITY_EDITOR
    /// <summary>
    /// Virtual Camera Control Class In Editor
    /// </summary>
    public class VirtualCameraActiveInEditorRepository : MonoBehaviour, IVirtualCameraActive
    {
        /// <summary>
        /// 現在活性化状態なのかを取得
        /// </summary>
        public bool IsEnabled => Manager.Instance.IsVirtualCameraInEditorActive;

        /// <summary>
        /// Activeを設定する
        /// </summary>
        /// <param name="isToEnable">活性化にするのか</param>
        /// <returns>切り替え成功の場合True</returns>
        public async UniTask<bool> Set(bool isToEnable)
        {
            var mainCamControl = FindObjectOfType<SceneRootBase>();
            await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
            Manager.Instance.VirtualCameraInEditorActive(true);
            mainCamControl.Camera.gameObject.SetActive(false);
            await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
            return true;
        }
    }
#endif
}