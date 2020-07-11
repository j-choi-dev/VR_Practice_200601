using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Choi.VR2020.Domain.System;

namespace Choi.VR2020.UI.System
{
    public class ChangeModeImageControl : MonoBehaviour
    {
        [SerializeField] private Sprite[] m_guideImage;
        [SerializeField] private SimpleImage m_image;
        [SerializeField] private SimpleText m_text;

        private void Awake()
        {
        }

        private async void Start()
        {
            m_image.gameObject.SetActive(false);
            m_text.gameObject.SetActive(false);
            await ChangeModeToVirtual();
        }

        public async UniTask ChangeModeToVirtual()
        {
            m_image.gameObject.SetActive(true);
            if (Manager.Instance.CameraMode.State != CameraState.NONE
                && Manager.Instance.CameraMode.State != CameraState.Normal)
            {
                Debug.Log($"Already CameraMode : {Manager.Instance.CameraMode.State}");
                return ;
            }
            var orientResult = await DoCheckLandScape();
            m_image.gameObject.SetActive(false);
            Manager.Instance.CameraMode.SetState(orientResult);

            m_text.gameObject.SetActive(true);
            var countDownResult = await DoCountDown(true);
            m_text.gameObject.SetActive(false);
            Manager.Instance.CameraMode.SetState(countDownResult);
        }

        private async UniTask<CameraState> DoCheckLandScape()
        {
            var timer = 0f;
            Debug.Log($"nowOrientation DoCheckLandScape : {Manager.Instance.Orientation.Value }");
            while (Manager.Instance.Orientation.Value != DeviceOrientation.LandscapeLeft)
            {
                if (Manager.Instance.Orientation.Value != Input.deviceOrientation)
                {
                    Manager.Instance.Orientation.SetValue(DeviceOrientation.LandscapeLeft);
                }
                timer = (int)timer < m_guideImage.Length - 1 ? timer += Time.deltaTime * 3 : 0;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
                m_image.Set(m_guideImage[(int)timer]);
            }
            return CameraState.ChangeToVirtual;
        }

        private async UniTask<CameraState> DoCountDown(bool isToVirtual)
        {
            var timer = 10f;
            m_text.Set((int)timer);
            while (timer > 0f)
            {
                m_text.Set((int)timer);
                Debug.Log($"countTimer : {timer}");
                // await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
                await UniTask.Delay(1000);
                timer -= 1f;
            }
            return isToVirtual ? CameraState.Virtual : CameraState.Normal;
        }
    }
}