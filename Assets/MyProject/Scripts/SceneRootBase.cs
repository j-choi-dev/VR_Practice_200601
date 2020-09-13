using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Choi.MyProj.UI;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Interface.API.System;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.UI.Scene
{
    /// <summary>
    /// シーン管理スクリプトの Base
    /// </summary>
    public abstract class SceneRootBase : MonoBehaviour
    {
        [SerializeField] protected Camera m_camera;
        [SerializeField] protected Canvas m_canvas;
        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            var isActive = Manager.Instance.IsActive;
            Debug.Log("SceneRootBase Awake");
            if (VirtualControlAPI.Instance.NowCameraState == CameraState.Virtual)
            {
                Manager.Instance.SetNowCamera(Application.isEditor ? Manager.Instance.VirtualCameraInEditor.GetCamera() : m_camera);
                return;
            }
            Manager.Instance.SetNowCamera(m_camera);
        }

        /// <summary>
        /// シーンで使われる Default カメラを取得する
        /// </summary>
        public Camera Camera => m_camera;

        /// <summary>
        /// シーンのCanvasを取得
        /// </summary>
        public Canvas Canvas => m_canvas;

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public abstract void SceneChangeToNext();

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
        public abstract UniTask<bool> Init();
    }
}