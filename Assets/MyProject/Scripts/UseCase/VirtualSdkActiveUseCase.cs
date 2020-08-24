using System.Threading.Tasks;
using IRepository = Choi.MyProj.Domain.System.IVirtualCameraActive;

namespace Choi.MyProj.UseCase.System
{
    public class VirtualSdkActiveUseCase : UseCase<bool, bool>
    {
        private IRepository m_repository;

        public VirtualSdkActiveUseCase(IRepository repository)
        {
            m_repository = repository;
        }

        protected override async Task<bool> ExecuteImpl(bool isActive)
        {
            var result = await m_repository.Set(isActive);
            return result;
        }
    }
}