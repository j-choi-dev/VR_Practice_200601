using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.System;
using Choi.MyProj.UseCase.System;
using Choi.MyProj.Repository.System;

namespace Choi.MyProj.Interface.API.System
{
    /// <summary>
    ///  Virtual Mode Control API Class
    /// </summary>
    public class VirtualControlAPI
    {
        /// <summary>
        /// Lazy Instance
        /// </summary>
        private static readonly Lazy<VirtualControlAPI> m_instance = new Lazy<VirtualControlAPI>(() => new VirtualControlAPI());

        /// <summary>
        /// 外からアクセスできる Lazy Instance
        /// </summary>
        public static VirtualControlAPI Instance => m_instance.Value;

        /// <summary>
        /// 現時点での Device Orientation
        /// </summary>
        public DeviceOrientation NowDeviceOrientation => DeviceOrientationInfo.Value;

        /// <summary>
        /// 現時点での Camera State
        /// </summary>
        public VirtualState NowCameraState => CameraModeInfo.State;

        /// <summary>
        /// 現時点での Virtual Reality SDK State
        /// </summary>
        public bool NowSdkActiveState => m_virtualSdkActive.IsEnabled;

        /// <summary>
        /// Virtual Camera 制御スクリプト Interface
        /// </summary>
        private IVirtualCameraActive m_virtualSdkActive;

        /// <summary>
        /// Device Orientation をセットするメソッド
        /// </summary>
        /// <param name="state">更新するステータス</param>
        /// <returns>更新結果</returns>
        public async UniTask<bool> SetDeviceOrientation(DeviceOrientation state)
        {
            // LandScape 変換 UseCase実行
            var usecase = new OrientationInfoUseCase();
            var result = await usecase.Execute(state);
            if (!result)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Camera　状態をセットするメソッド
        /// </summary>
        /// <param name="state">更新するステータス</param>
        /// <returns>更新結果</returns>
        public async UniTask<bool> SetCameraState(VirtualState state)
        {
            // Virtual 変換 UseCase実行
            var usecase = new VirtualModeChangeUseCase();
            var result = await usecase.Execute(state);
            if(!result)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Virtual Reality SDK をセットするメソッド
        /// </summary>
        /// <param name="isActive">On/Off</param>
        /// <returns></returns>
        public async UniTask<bool> SetVirtualSdkActive(bool isActive)
        {
            if(m_virtualSdkActive == null)
            {
#if UNITY_EDITOR
                Debug.Log("[CHOI] Create  VirtualCameraActiveInEditorRepository");
                m_virtualSdkActive = new VirtualCameraActiveInEditorRepository();
#elif UNITY_ANDROID || UNITY_IPHONE
                Debug.Log("[CHOI] Create  VirtualCameraActiveInMobileRepository");
                m_virtualSdkActive = new VirtualCameraActiveInMobileRepository();
#endif
                //m_virtualSdkActive = new VirtualCameraActiveInMobileRepository();
            }
            var usecase = new VirtualSdkActiveUseCase(m_virtualSdkActive);
            var result = await usecase.Execute(isActive);
            if (!result)
            {
                return false;
            }
            return true;
        }
    }
}