using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Interface.API.System;

namespace Choi.MyProj.UI.System
{
    /// <summary>
    /// カメラモードチェンジシーンの制御スクリプト
    /// </summary>
    /// @j_choi 2020.08.06
    // TODO  ボケてるうちに書いたスクリプト
    public class ChangeModeImageControl : MonoBehaviour
    {
        /// <summary>
        /// 背景イメージ
        /// </summary>
        [SerializeField] private Image m_BackGround;

        /// <summary>
        /// LandScape Guide Sprites
        /// </summary>
        [SerializeField] private Sprite[] m_guideImage;

        /// <summary>
        /// LandScape Guide Sprite を表示するイメージ
        /// </summary>
        [SerializeField] private SimpleImage m_image;

        /// <summary>
        /// Text単純代入スクリプト for Count Down
        /// </summary>
        [SerializeField] private SimpleText m_text;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            // TODO NULL Check Here
        }

        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            m_image.gameObject.SetActive(false);
            m_text.gameObject.SetActive(false);
            await ChangeModeToVirtual();
        }

        /// <summary>
        /// Change To Virtual Process
        /// </summary>
        /// <returns></returns>
        public async UniTask ChangeModeToVirtual()
        {
            m_image.gameObject.SetActive(true);
            if (VirtualControlAPI.Instance.NowCameraState != CameraState.NONE
                && VirtualControlAPI.Instance.NowCameraState != CameraState.Normal)
            {
                Debug.Log($"Already CameraMode Virtual : {VirtualControlAPI.Instance.NowCameraState}");
                return ;
            }
            if(!await DoCheckLandScape())
            {
                Debug.LogError("Change Device Orientation Error!");
                return;
            }
            m_image.gameObject.SetActive(false);
            await VirtualControlAPI.Instance.SetCameraState(CameraState.ChangeToVirtual);
            Debug.Log($"Changed CameraMode : {VirtualControlAPI.Instance.NowCameraState}");

            m_text.gameObject.SetActive(true);
            if (!await DoCountDown())
            {
                Debug.LogError("Count Down Error!");
                return;
            }
            m_text.gameObject.SetActive(false);
            m_BackGround.gameObject.SetActive(false);
            await VirtualControlAPI.Instance.SetCameraState(CameraState.Virtual);
            Debug.Log($"Changed CameraMode : {VirtualControlAPI.Instance.NowCameraState}");
            await VirtualControlAPI.Instance.SetVirtualSdkActive(true);
            Debug.Log($"Virtual Sdk Activate : {VirtualControlAPI.Instance.NowSdkActiveState}");
        }

        /// <summary>
        /// Change To Landscape Process
        /// </summary>
        /// <returns></returns>
        private async UniTask <bool> DoCheckLandScape()
        {
            var timer = 0f;
            Debug.Log($"nowOrientation DoCheckLandScape : {VirtualControlAPI.Instance.NowDeviceOrientation}");
            while (VirtualControlAPI.Instance.NowDeviceOrientation != DeviceOrientation.LandscapeLeft)
            {
                if (VirtualControlAPI.Instance.NowDeviceOrientation != Input.deviceOrientation)
                {
                    await VirtualControlAPI.Instance.SetDeviceOrientation(DeviceOrientation.LandscapeLeft);
                }
                timer = (int)timer < m_guideImage.Length - 1 ? timer += Time.deltaTime * 3 : 0;
                await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
                m_image.Set(m_guideImage[(int)timer]);
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
                await UniTask.Delay(100);
#endif
                timer -= 1f;
            }
            return true;
        }
    }
}