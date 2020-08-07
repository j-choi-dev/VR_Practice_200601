using System.Threading.Tasks;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.UseCase.System
{
    public class VirtualModeChangeUseCase : UseCase<bool, CameraState>
    { 
        protected override async Task<bool> ExecuteImpl(CameraState targetState)
        {
            var result = await CameraModeInfo.Set(targetState);
            return result == targetState;
        }
    }
}
