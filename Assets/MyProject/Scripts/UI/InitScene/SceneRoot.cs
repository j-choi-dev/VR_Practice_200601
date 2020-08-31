using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Interface.API.System;

namespace Choi.MyProj.UI.Scene.Init
{
    /// <summary>
    /// Init Scene Root Class
    /// </summary>
    public class SceneRoot : SceneRootBase
    {
        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            Debug.Log("[CHOI] Init Scene");
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("01_Switch");
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns></returns>
        public override async UniTask<bool> Init()
        {
            if (VirtualControlAPI.Instance.NowCameraState == CameraState.Virtual && Application.isEditor)
            {
                m_camera.gameObject.SetActive(false);
            }
            return true;
        }
    }
}