using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Choi.MyProj.UI;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Interface.API.Virtual;
using Choi.MyProj.Domain.System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

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
        protected async Task Awake()
        {
            var isActive = Manager.Instance.IsActive;
#if UNITY_EDITOR
            var virtualState = new Repository.Editor.VirtualStateInEditorRepository();
            if (virtualState.Get() == VirtualState.Virtual && VirtualControlAPI.Instance.NowCameraState == VirtualState.Virtual)
            {
                Manager.Instance.VirtualCameraInEditorActive(true);
                m_camera.gameObject.SetActive(false);
                Manager.Instance.SetNowCamera(Manager.Instance.VirtualCameraInEditor.GetCamera());
                await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
                SetVirtualCanvas();
                return;
            }

            Manager.Instance.VirtualCameraInEditorActive(false);
            m_camera.gameObject.SetActive(true);
            SetNormalCanvas();
            return;
#endif
            if (VirtualControlAPI.Instance.NowCameraState == VirtualState.Virtual)
            {
                Manager.Instance.SetNowCamera(m_camera);
                SetVirtualCanvas();
                return;
            }
            Manager.Instance.SetNowCamera(m_camera);
            SetNormalCanvas();
            return;
        }

        public bool SetNormalCanvas()
        {
            m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            m_canvas.GetComponent<GraphicRaycaster>().enabled = true;
            m_canvas.transform.position = Vector3.zero;
            m_canvas.transform.localScale = Vector3.one;
            return true;
        }

        public bool SetVirtualCanvas()
        {
            m_canvas.renderMode = RenderMode.WorldSpace;
            m_canvas.planeDistance = 2f;
            m_canvas.worldCamera = Manager.Instance.NowCamera;
            m_canvas.transform.position = new Vector3(0, 0, 3f);
            m_canvas.transform.localScale = Vector3.one * 0.01f;
            Debug.Log(m_canvas.worldCamera.name);
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