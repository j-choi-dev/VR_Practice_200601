using System.Threading.Tasks;
using Choi.MyProj.Domain;

namespace Choi.MyProj.UseCase
{
    /// <summary>
    /// 作業手順を定義するUseCaseの基本クラス
    /// </summary>
    /// Auth : @Choi
    public abstract class UseCase<ResultType, ProtocolType>
    {
        /// <summary>
        /// ユースケースの具象実装メソッド
        /// </summary>
        /// <returns>The use case.</returns>
        /// <param name="protocol">Protocol.</param>
        protected abstract Task<ResultType> ExecuteImpl(ProtocolType protocol);

        /// <summary>
        /// 外部向け、定義された手順を実行するメソッド
        /// </summary>
        /// <returns>The execute.</returns>
        /// <param name="protocol">Protocol.</param>
        public async Task<ResultType> Execute(ProtocolType protocol)
        {
            return await ExecuteImpl(protocol);
        }
    }

    /// <summary>
    /// 作業手順を定義するUseCaseの基本クラス
    /// </summary>
    /// Auth : @Choi
    public abstract class UseCase<ResultType>
    {
        /// <summary>
        /// ユースケースの具象実装メソッド
        /// </summary>
        /// <returns>The use case.</returns>
        /// <param name="protocol">Protocol.</param>
        protected abstract Task<ResultType> ExecuteImpl();

        /// <summary>
        /// 外部向け、定義された手順を実行するメソッド
        /// </summary>
        /// <returns>The execute.</returns>
        /// <param name="protocol">Protocol.</param>
        public async Task<ResultType> Execute()
        {
            return await ExecuteImpl();
        }
    }
}