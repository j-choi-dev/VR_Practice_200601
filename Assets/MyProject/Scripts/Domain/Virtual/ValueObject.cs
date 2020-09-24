namespace Choi.MyProj.Domain.Virtual
{
    public struct Value
    {
        public const string VirtualStateKeyValue = "VirtualState";
        public const int ObjectUILayer = 1 << 10;
    }

    public enum VirtualState
    {
        NONE,
        Normal,
        ChangeToVirtual,
        Virtual,
        ChangeToNormal,
    }
}