﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.Virtual;
using Choi.MyProj.Interface.API.Virtual;

namespace Choi.MyProj.UI.Scene.ModeChangeScene
{
    /// <summary>
    /// Mode Change Scene Riit Class
    /// </summary>
    public class SceneRoot : SceneRootBase
    {
        /// <summary>
        /// 背景イメージ
        /// </summary>
        [SerializeField] private Image m_BackGround;

        /// <summary>
        /// Guide Image Control
        /// </summary>
        [SerializeField] private GuideImageControl m_guideImageControl;

        /// <summary>
        /// LandScape Guide Sprite を表示するイメージ
        /// </summary>
        [SerializeField] private SimpleImage m_image;

        /// <summary>
        /// Text単純代入スクリプト for Count Down
        /// </summary>
        [SerializeField] private SimpleText m_text;

        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            Debug.Log("[CHOI] Change Scene");
            m_image.gameObject.SetActive(false);
            m_text.gameObject.SetActive(false);
            await ChangeModeToVirtual();
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("11_Main");
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
        public override async UniTask<bool> Init()
        {
            return true;
        }


        /// <summary>
        /// Change To Virtual Process
        /// </summary>
        /// <returns></returns>
        public async UniTask ChangeModeToVirtual()
        {
            m_image.gameObject.SetActive(true);
            if (VirtualControlAPI.Instance.NowCameraState != VirtualState.NONE
                && VirtualControlAPI.Instance.NowCameraState != VirtualState.Normal)
            {
                Debug.Log($"Already CameraMode Virtual : {VirtualControlAPI.Instance.NowCameraState}");
                return;
            }
            if (!await DoCheckLandScape())
            {
                Debug.LogError("Change Device Orientation Error!");
                return;
            }
            m_image.gameObject.SetActive(false);
            await VirtualControlAPI.Instance.SetCameraState(VirtualState.ChangeToVirtual);
            m_BackGround.gameObject.SetActive(false);
            await VirtualControlAPI.Instance.SetVirtualSdkActive(true);
            Manager.Instance.SetNowCamera(Manager.Instance.VirtualCameraInEditor.GetCamera());
            Debug.Log($"Virtual Sdk Activate : {VirtualControlAPI.Instance.NowSdkActiveState}");
            await VirtualControlAPI.Instance.SetCameraState(VirtualState.Virtual);
            Debug.Log($"Changed CameraMode : {VirtualControlAPI.Instance.NowCameraState}");
            SetVirtualCanvas();
            await UniTask.DelayFrame(1);

            m_text.gameObject.SetActive(true);
            if (!await DoCountDown())
            {
                Debug.LogError("Count Down Error!");
                return;
            }
            m_text.gameObject.SetActive(false);
            SceneChangeToNext();
        }


        /// <summary>
        /// Change To Landscape Process
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> DoCheckLandScape()
        {
            var timer = 0f;
            Debug.Log($"nowOrientation DoCheckLandScape : {VirtualControlAPI.Instance.NowDeviceOrientation}");
            while (VirtualControlAPI.Instance.NowDeviceOrientation != DeviceOrientation.LandscapeLeft)
            {
                if (VirtualControlAPI.Instance.NowDeviceOrientation != Input.deviceOrientation && Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
                {
                    await VirtualControlAPI.Instance.SetDeviceOrientation(DeviceOrientation.LandscapeLeft);
                }
                timer = (int)timer < m_guideImageControl.Length - 1 ? timer += Time.deltaTime * 3 : 0;
                await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
                m_image.Set(m_guideImageControl.GetSprite((int)timer));
            }
            return true;
        }

        /// <summary>
        /// Count Down Process
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> DoCountDown()
        {
            var timer = 10f;
            m_text.Set((int)timer);
            while (timer > 0f)
            {
                m_text.Set((int)timer);
#if !UNITY_EDITOR
                await UniTask.Delay(1000);
#else
                await UniTask.Delay(300);
#endif
                timer -= 1f;
            }
            return true;
        }
    }
}