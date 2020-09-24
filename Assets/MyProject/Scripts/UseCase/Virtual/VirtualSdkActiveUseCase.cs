using System.Threading.Tasks;
using IRepository = Choi.MyProj.Domain.Virtual.IVirtualCameraActive;

namespace Choi.MyProj.UseCase.Virtual
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