using System;
using UnityEngine;
using UnityEngine.XR;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Choi.MyProj.Domain.System
{
    public class CameraModeInfo
    {
        public static CameraState State { get; private set; } = CameraState.NONE;

        public CameraModeInfo()
        {
            XRSettings.enabled = false;
            State = CameraState.Normal;
        }

        /// <summary>
        /// Set Normal
        /// </summary>
        /// <returns>ステート</returns>
        public static Task<CameraState> Set(CameraState state)
        {
            State = state;
            return Task.Run(() => State);
        }
    }
}