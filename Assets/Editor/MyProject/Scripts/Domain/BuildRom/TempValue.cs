namespace Choi.MyProj.Domain.Editor.Build.Rom
{
#if UNITY_EDITOR
    public struct TempValue
    {
        /// <value>
        /// 会社名
        /// </value>
        public const string CompanyName = "ChoiJY";

        public const string BundleID = "com.jy.choi.vitrual";

        public const string ProductName = "Choi";

        /// <value>
        /// Version(x.x.x型)
        /// </value>
        public const string VersionName = "0.0.1";

        /// <summary>
        /// Product name info.
        /// </summary>
        public struct ProductNameInfo
        {
            public const string Release = "";
            public const string PreRelease = ".pre";
            public const string Dev = ".dev";
    }

        /// <summary>
        /// Version code.
        /// </summary>
        public struct VersionInfo
        {
            public const int Android = 1;
            public const int IOS = 1;
        }
    }
#endif
}