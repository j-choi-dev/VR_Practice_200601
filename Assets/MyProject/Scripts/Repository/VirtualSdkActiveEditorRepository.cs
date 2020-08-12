using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.System;
using Choi.MyProj.UI;

namespace Choi.MyProj.Repository.System
{
#if UNITY_EDITOR
    /// <summary>
    /// Virtual Camera Control Class
    /// </summary>
    public class VirtualCameraActiveInEditorRepository : MonoBehaviour, IVirtualCameraActive
    {
        /// <summary>
        /// 現在活性化状態なのかを取得
        /// </summary>
        public bool IsEnabled => Manager.Instance.VirtualCameraInEditor.gameObject.activeSelf;

        /// <summary>
        /// Activeを設定する
        /// </summary>
        /// <param name="isToEnable">活性化にするのか</param>
        /// <returns>切り替え成功の場合True</returns>
        public async UniTask<bool> Set(bool isToEnable)
        {
            var mainCam = GameObject.FindWithTag("MainCamera");
            await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
            Manager.Instance.VirtualCameraInEditor.gameObject.SetActive(true);
            mainCam.SetActive(false);
            await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
            return true;
        }
    }
#endif
}