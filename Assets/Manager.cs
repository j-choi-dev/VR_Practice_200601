using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Choi.VR2020.Domain.System;

namespace Choi.VR2020.UI.System
{
    public class Manager : SingletonMonoBehaviour<Manager>
    {
        public CameraModeInfo CameraMode { get; private set; }

        public DeviceOrientationInfo Orientation { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (CameraMode == null)
            {
                CameraMode = new CameraModeInfo();
            }
            if (Orientation == null)
            {
                Orientation = new DeviceOrientationInfo();
            }
            CameraMode.SetState(CameraState.Normal);
            Orientation.SetValue(DeviceOrientation.Unknown);
        }

        private void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Orientation.SetValue(Orientation.Value == DeviceOrientation.Portrait ? DeviceOrientation.LandscapeLeft : DeviceOrientation.Portrait);
            }
        }
    }
}