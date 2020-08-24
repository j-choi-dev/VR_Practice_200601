using UnityEngine;
using Choi.MyProj.Domain.System;
using Choi.MyProj.Interface.API.System;
using Choi.MyProj.UI.System;

namespace Choi.MyProj.UI
{
    /// <summary>
    /// ゲーム全体のマネージャー
    /// </summary>
    /// @j_choi 2020.08.06
    public class Manager : SingletonMonoBehaviour<Manager>
    {
        [SerializeField] private VirtualCameraInEditor m_virtualCamera;
        public VirtualCameraInEditor VirtualCameraInEditor => m_virtualCamera;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            this.transform.position = Vector3.zero;
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            m_virtualCamera.gameObject.SetActive(false);
        }

        /// <summary>
        /// Fixed Updateでの挙動
        /// </summary>
        private async void FixedUpdate()
        {
            if(Application.isEditor && Input.GetKeyDown(KeyCode.L))
            {
                await VirtualControlAPI.Instance.SetDeviceOrientation(DeviceOrientationInfo.Value == DeviceOrientation.Portrait ? DeviceOrientation.LandscapeLeft : DeviceOrientation.Portrait);
            }
        }
    }
}