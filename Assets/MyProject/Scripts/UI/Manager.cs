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
        public Camera NowCamera { get; private set; }
        public bool IsActive => gameObject.activeSelf;

#if UNITY_EDITOR
        [SerializeField] private VirtualCameraInEditor m_virtualCamera;

        public VirtualCameraInEditor VirtualCameraInEditor => m_virtualCamera;

        public bool IsVirtualCameraInEditorActive => m_virtualCamera.gameObject.activeSelf;
#endif

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

        public void SetNowCamera(Camera camera)
        {
            NowCamera = camera;
            Debug.Log(NowCamera.name);
        }

#if UNITY_EDITOR
        public bool VirtualCameraInEditorActive(bool isOn)
        {
            m_virtualCamera.gameObject.SetActive(isOn);
            return m_virtualCamera.gameObject.activeSelf == isOn;
        }
#endif
    }
}