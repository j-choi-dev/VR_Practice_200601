using System.Threading.Tasks;

namespace Choi.MyProj.Domain.Editor.Build.Rom
{
    /// <summary>
    /// Rom build のInterface
    /// </summary>
    /// Auth : @Choi
    public interface IRomBuildRepository
    {
        /// <summary>
        /// Builds the pre process.
        /// </summary>
        /// <returns>The pre process.</returns>
        Task<IBuildInfo> BuildPreProcess(RomBuildUseCaseProtocol buildProtocol);

        /// <summary>
        /// Builds the main process.
        /// </summary>
        /// <returns>The main process.</returns>
        Task<IBuildInfo> BuildMainProcess(IBuildInfo buildInfo);

        /// <summary>
        /// Builds the post process.
        /// </summary>
        /// <returns>The post process.</returns>
        Task<IBuildInfo> BuildPostProcess(IBuildInfo buildInfo, RomBuildUseCaseProtocol buildProtocol);
    }
}
