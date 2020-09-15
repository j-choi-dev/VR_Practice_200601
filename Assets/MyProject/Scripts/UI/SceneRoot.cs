using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Interface.API.System;
using UnityEngine.UI;

namespace Choi.MyProj.UI.Scene.Test
{
    /// <summary>
    /// Test Scene Root Class
    /// </summary>
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] GameObject aaa;
        [SerializeField] private ButtonImpl m_button;
        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            Debug.Log("[CHOI] Test Scene");
            aaa.SetActive(false);
            await Init();
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
            if (VirtualControlAPI.Instance.NowCameraState == VirtualState.Virtual && Application.isEditor)
            {
                m_camera.gameObject.SetActive(false);
            }
            if(VirtualControlAPI.Instance.NowCameraState == VirtualState.Virtual)
            {
                m_canvas.renderMode = RenderMode.WorldSpace;
                //m_canvas.worldCamera = Application.isEditor ? Manager.Instance.VirtualCameraInEditor.GetCamera() : m_camera;
                m_canvas.worldCamera = Manager.Instance.NowCamera;
                m_canvas.planeDistance = 2f;
            }
            m_button.Init(OnButtonClick, OnButtonRelease);
            return true;
        }

        public void OnButtonClick()
        {
            aaa.SetActive(true);
            Debug.Log($"Clicked : {aaa.activeSelf} ()");
        }
        public void OnButtonRelease()
        {
            aaa.SetActive(false);
            Debug.Log($"Released : {aaa.activeSelf} ()");
        }
    }
}