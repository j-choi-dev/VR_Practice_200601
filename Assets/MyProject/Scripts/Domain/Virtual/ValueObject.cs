namespace Choi.MyProj.Domain.Virtual
{
    public struct Value
    {
        public const string VirtualStateKeyValue = "VirtualState";
        public const int CanvasUILayer = 1 << 5;
        public const int ObjectUILayer = 1 << 11;
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