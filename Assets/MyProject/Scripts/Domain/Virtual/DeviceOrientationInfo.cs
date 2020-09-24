using UnityEngine;
using System.Threading.Tasks;

namespace Choi.MyProj.Domain.Virtual
{
    public class DeviceOrientationInfo
    {
        public static DeviceOrientation Value { get; private set; }

        public static Task<DeviceOrientation> Set(DeviceOrientation value)
        {
            Value = value;
            return Task.Run(() => Value);
        }
    }
}