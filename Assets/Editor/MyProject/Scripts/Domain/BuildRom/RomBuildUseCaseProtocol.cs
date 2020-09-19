namespace Choi.MyProj.Domain.Editor.Build.Rom
{
    /// <summary>
    /// 本体ビルド時に必要なプロトコル
    /// </summary>
    public class RomBuildUseCaseProtocol
    {
        /// <summary>
        /// Gets the build mode.
        /// </summary>
        /// <value>The build mode.</value>
        public BuildMode BuildMode { get; private set; }

        /// <summary>
        /// Gets the build tag key.
        /// </summary>
        /// <value>The build tag key.</value>
        public string BuildTagKey { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this
        /// <see cref="T:CCS.Editor.Domain.BuildScript.Rom.RomBuildUseCaseProtocol"/> is use debug mode.
        /// </summary>
        /// <value><c>true</c> if is use debug mode; otherwise, <c>false</c>.</value>
        public bool IsUseDebugMode { get; private set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="buildMode">Build mode.</param>
        /// <param name="buildTagKey">Build tag key.</param>
        /// <param name="isUserDebugMode">If set to <c>true</c> is user debug mode.</param>
        private RomBuildUseCaseProtocol(BuildMode buildMode, string buildTagKey, bool isUserDebugMode)
        {
            BuildMode = buildMode;
            BuildTagKey = buildTagKey;
            IsUseDebugMode = isUserDebugMode;
        }

        /// <summary>
        /// Creates the default.
        /// </summary>
        /// <returns>The default.</returns>
        /// <param name="buildMode">Build mode.</param>
        /// <param name="buildTagKey">Build tag key.</param>
        public static RomBuildUseCaseProtocol Create(BuildMode buildMode, string buildTagKey, bool isDebug)
        {
            return new RomBuildUseCaseProtocol(buildMode, buildTagKey, isDebug);
        }

        /// <summary>
        /// Creates the debug mode.
        /// </summary>
        /// <returns>The debug mode.</returns>
        /// <param name="buildTagKey">Build tag key.</param>
        public static RomBuildUseCaseProtocol CreateDebugMode(string buildTagKey)
        {
            return new RomBuildUseCaseProtocol(BuildMode.Dev, buildTagKey, true);
        }
    }
}