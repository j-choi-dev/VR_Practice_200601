namespace Choi.MyProj.Domain.System
{
    public struct KeyValue
    {
        public const string VirtualStateKeyValue = "VirtualState";
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