using System;
using UnityEngine;
using UnityEngine.XR;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.Repository.Virtual
{
    /// <summary>
    /// Virtual Camera Control Class
    /// </summary>
    public class VirtualCameraActiveInMobileRepository : IVirtualCameraActive
    {
        /// <summary>
        /// 現在活性化状態なのかを取得
        /// </summary>
        public bool IsEnabled => XRSettings.enabled;

        /// <summary>
        /// Activeを設定する
        /// </summary>
        /// <param name="isToEnable">活性化にするのか</param>
        /// <returns>切り替え成功の場合True</returns>
        public async UniTask<bool> Set(bool isToEnable)
        {
            var desiredDevice = "cardboard"; // Or "cardboard".

            Debug.Log($"XRSettings.loadedDeviceName Before = {XRSettings.loadedDeviceName}");
            // Some VR Devices do not support reloading when already active, see
            // https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
            if (String.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0)
            {
                XRSettings.LoadDeviceByName(desiredDevice);
                await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
            }
            XRSettings.enabled = isToEnable;
            Debug.Log($"XRSettings.loadedDeviceName After = {XRSettings.loadedDeviceName}");
            return true;
        }
    }
}