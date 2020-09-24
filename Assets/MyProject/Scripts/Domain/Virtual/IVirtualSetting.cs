using Cysharp.Threading.Tasks;

namespace Choi.MyProj.Domain.Virtual
{
    /// <summary>
    /// Virtual Camera 制御スクリプト Interface
    /// </summary>
    public interface IVirtualCameraActive
    {
        /// <summary>
        /// Is Active?
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Active情報をセット
        /// </summary>
        /// <param name="isToEnabled">To Enable or Diable</param>
        /// <returns>切り替えの結果</returns>
        UniTask<bool> Set(bool isToEnabled);
    }
}