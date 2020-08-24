using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Interface.API.System;

namespace Choi.MyProj.UI.Scene.Test
{
    /// <summary>
    /// Test Scene Root Class
    /// </summary>
    public class SceneRoot : SceneRootBase
    {
        /// <summary>
        /// シーンで使われる Default カメラ
        /// </summary>
        [SerializeField] private Camera m_camera;

        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            Debug.Log("[CHOI] Test Scene");
            await Init();
        }

        /// <summary>
        /// シーンで使われる Default カメラを外からアクセス
        /// </summary>
        /// <returns>シーンで使われる Default カメラ</returns>
        public override Camera GetSceneDefaultCamera()
        {
            return m_camera;
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("01_Init");
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
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