using System.Threading.Tasks;
using Choi.MyProj.Domain.Virtual;
using UnityEngine;

namespace Choi.MyProj.UseCase.Virtual
{
    public class OrientationInfoUseCase : UseCase<bool, DeviceOrientation>
    {
        protected override async Task<bool> ExecuteImpl(DeviceOrientation value)
        {
            var result = await DeviceOrientationInfo.Set(value);
            return result == value;
        }
    }
}