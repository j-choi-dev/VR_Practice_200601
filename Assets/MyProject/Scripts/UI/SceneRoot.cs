using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Choi.MyProj.Interface.API.System;

namespace Choi.MyProj.UI.Scene.Test
{
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] private Camera m_camera;

        private async void Start()
        {
            Debug.Log("[CHOI] Test Scene");
            await Init();
        }

        public override Camera GetSceneDefaultCamera()
        {
            return m_camera;
        }

        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("01_Init");
        }

        public override async UniTask<bool> Init()
        {
            if(VirtualControlAPI.Instance.NowCameraState == Domain.System.CameraState.Virtual)
            {
                // m_camera.gameObject.SetActive(false);
            }
            return true;
        }
    }
}