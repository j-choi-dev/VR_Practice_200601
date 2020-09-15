using System.Collections;
using System.Collections.Generic;
using System;
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
        /// シーンで使われる Default カメラを取得する
        /// </summary>
        public Camera Camera => m_camera;

        /// <summary>
        /// シーンのCanvasを取得
        /// </summary>
        public Canvas Canvas => m_canvas;

        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            var isActive = Manager.Instance.IsActive;
            Debug.Log("SceneRootBase Awake");
            if (VirtualControlAPI.Instance.NowCameraState == VirtualState.Virtual)
            {
#if UNITY_EDITOR
                var virtualState = new Repository.Editor.VirtualStateInEditorRepository();
                if (virtualState.Get() != VirtualState.Virtual)
                {
                    SetNormalCanvas();
                    return;
                }
#endif
                SetVirtualCanvas();
                return;
            }
            SetNormalCanvas();
            return;
        }

        private bool SetNormalCanvas()
        {
            Manager.Instance.SetNowCamera(m_camera);
            m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            m_canvas.GetComponent<GraphicRaycaster>().enabled = true;
            return true;
        }

        private bool SetVirtualCanvas()
        {
            m_canvas.renderMode = RenderMode.WorldSpace;
            m_canvas.planeDistance = 2f;
            Manager.Instance.SetNowCamera(Application.isEditor ? Manager.Instance.VirtualCameraInEditor.GetCamera() : m_camera);
            return true;
        }

        private void OnDestroy()
        {
            GC.Collect();
        }

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