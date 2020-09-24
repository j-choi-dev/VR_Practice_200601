using System;
using UnityEngine;
using UnityEngine.XR;
using System.Threading.Tasks;

namespace Choi.MyProj.Domain.Virtual
{
    public class CameraModeInfo
    {
        public static VirtualState State { get; private set; } = VirtualState.NONE;

        public CameraModeInfo()
        {
            XRSettings.enabled = false;
            State = VirtualState.Normal;
        }

        /// <summary>
        /// Set Normal
        /// </summary>
        /// <returns>ステート</returns>
        public static Task<VirtualState> Set(VirtualState state)
        {
            State = state;
            return Task.Run(() => State);
        }
    }
}