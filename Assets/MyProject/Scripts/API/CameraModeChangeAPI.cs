using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.System;
using Choi.MyProj.UseCase.System;
using Choi.MyProj.Repository.System;

namespace Choi.MyProj.Interface.API.System
{
    public class VirtualControlAPI
    {
        private static readonly Lazy<VirtualControlAPI> m_instance = new Lazy<VirtualControlAPI>(() => new VirtualControlAPI());
        public static VirtualControlAPI Instance => m_instance.Value;

        private IVirtualCameraActive m_virtualSdkActive;

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

        public async UniTask<bool> SetCameraState(CameraState state)
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

        public async UniTask<bool> SetVirtualSdkActive(bool isActive)
        {
            if(m_virtualSdkActive == null)
            {
#if UNITY_EDITOR
                m_virtualSdkActive = new VirtualCameraActiveInEditorRepository();
#elif UNITY_ANDROID || UNITY_IPHONE
                m_virtualSdkActive = new VirtualCameraActiveInMobileRepository();
#endif
            }
            var usecase = new VirtualSdkActiveUseCase(m_virtualSdkActive);
            var result = await usecase.Execute(isActive);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public DeviceOrientation NowDeviceOrientation => DeviceOrientationInfo.Value;

        public CameraState NowCameraState => CameraModeInfo.State;

        public bool NowSdkActiveState => m_virtualSdkActive.IsEnabled;
    }
}