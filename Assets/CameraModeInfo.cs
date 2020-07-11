using UnityEngine;
using UnityEngine.XR;

namespace Choi.VR2020.Domain.System
{
    public class DeviceOrientationInfo
    {
        public DeviceOrientation Value { get; private set; }

        public DeviceOrientation SetValue(DeviceOrientation value)
        {
            Value = value;
            return value;
        }
    }
    public class CameraModeInfo
    {
        [SerializeField]
        private bool m_isVirtualMode;

        [SerializeField]
        public CameraState State { get; private set; } = CameraState.NONE;

        public CameraModeInfo()
        {
            XRSettings.enabled = false;
            State = CameraState.Normal;
            SetVRSettings();
        }

        /// <summary>
        /// Set Normal
        /// </summary>
        /// <returns>ステート</returns>
        public CameraState SetState(CameraState state)
        {
            State = state;
            SetVRSettings();
            return State;
        }

        private bool SetVRSettings()
        {
            XRSettings.enabled = State != CameraState.NONE
                        || State != CameraState.Normal;
            return true;
        }
    }

    public enum CameraState
    {
        NONE,
        Normal,
        ChangeToVirtual,
        Virtual,
        ChangeToNormal,
    }
}