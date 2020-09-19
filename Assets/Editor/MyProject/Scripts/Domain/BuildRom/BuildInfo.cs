using UnityEditor;

namespace Choi.MyProj.Domain.Editor.Build.Rom
{
    /// <summary>
    /// ビルドモード定義
    /// </summary>
    /// Auth : @Choi
    public enum BuildMode
    {
        /// <value>Developerモード</value>
        Dev,
        /// <value>リリース想定ビルドモード</value>
        PreRelease,
        /// <value>リリース想定ビルドモード(AAB)</value>
        PreReleaseAAB,
        /// <value>リリースモード</value>
        Release,
    }

    /// <summary>
    /// 本体ビルドの基本情報や結果を管理する Interface.
    /// </summary>
    /// Auth : @Choi
    public interface IBuildInfo
    {
        /// <summary>
        /// 開始時間
        /// </summary>
        /// <value>The start at.</value>
        string StartAt { get; }

        /// <summary>
        /// Gets the sub version.
        /// </summary>
        /// <value>The sub version.</value>
        string FileNameByVersion { get; }

        /// <summary>
        /// ビルドモード
        /// </summary>
        /// <value>The build mode.</value>
        BuildMode BuildMode { get; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        bool Success { get; }

        /// <summary>
        /// 失敗時返す
        /// </summary>
        /// <value>The fail.</value>
        IBuildInfo Fail { get; }
    }

    /// <summary>
    /// 本体ビルドの基本情報や結果を管理する具象クラス
    /// </summary>
    /// Auth : @Choi
    public static class BuildInfo
    {
        /// <summary>
        /// 基本型のBuildInfoを作る
        /// </summary>
        /// <returns>基本型のBuildInfo</returns>
        /// <param name="startAt">Start at.</param>
        /// <param name="mode">Mode.</param>
        public static IBuildInfo Create(string startAt, BuildMode mode, string fileNameByVersion) => new Impl(startAt, mode, fileNameByVersion);

        /// <summary>
        /// Build info 具象
        /// </summary>
        public sealed class Impl : IBuildInfo
        {
            /// <summary>
            /// 開始時間
            /// </summary>
            /// <value>The start at.</value>
            public string StartAt { get; private set; }

            /// <summary>
            /// ビルドモード
            /// </summary>
            /// <value>The build mode.</value>
            public BuildMode BuildMode { get; private set; }

            /// <summary>
            /// Gets the sub version.
            /// </summary>
            /// <value>The sub version.</value>
            public string FileNameByVersion { get; }

            /// <summary>
            /// 成功/失敗を返す
            /// </summary>
            /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
            public bool Success { get; private set; }

            /// <summary>
            /// Gets the fail.
            /// </summary>
            /// <value>The fail.</value>
            public IBuildInfo Fail => new Impl(StartAt, BuildMode, FileNameByVersion, false);

            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="startAt">Start Time</param>
            /// <param name="buildMode">Build mode.</param>
            public Impl(string startAt, BuildMode buildMode, string fileNameByVersion)
            {
                StartAt = startAt;
                BuildMode = buildMode;
                FileNameByVersion = fileNameByVersion;
                Success = true;
            }

            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="startAt">Start Time</param>
            /// <param name="buildMode">Build mode.</param>
            /// <param name="success">成功/失敗</param>
            private Impl(string startAt, BuildMode buildMode, string fileNameByVersion, bool success)
            {
                StartAt = startAt;
                BuildMode = buildMode;
                FileNameByVersion = fileNameByVersion;
                Success = success;
            }
        }
    }
}