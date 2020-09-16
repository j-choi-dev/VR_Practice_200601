using System.Threading.Tasks;
using Choi.MyProj.Domain.System;

namespace Choi.MyProj.UseCase.System
{
    public class VirtualModeChangeUseCase : UseCase<bool, VirtualState>
    { 
        protected override async Task<bool> ExecuteImpl(VirtualState targetState)
        {
            var result = await CameraModeInfo.Set(targetState);
            return result == targetState;
        }
    }
}
