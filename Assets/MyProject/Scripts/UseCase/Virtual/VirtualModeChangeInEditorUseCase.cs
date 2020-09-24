using System.Threading.Tasks;
using Choi.MyProj.Domain.Virtual;

namespace Choi.MyProj.UseCase.Virtual
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
