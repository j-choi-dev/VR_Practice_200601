using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Choi.MyProj.Domain.App
{
    /// <summary>
    /// チーム内やサーバーとの通信に使う App version.
    /// </summary>
    /// <remarks>
    /// チーム内orユーザー配信目標日付の YYMMDD 6桁 + ビルド回数 2桁
    /// アプリビルドする前に必ずこの値を更新し、関係者全員が確認するように運用すればいいかなと思います。
    /// TODO バージョン値のフォーマットはサーバーサイドエンジニアとの相談によって変わる可能性もあります。
    /// </remarks>
    /// Auth : @Choi
    public sealed class AppVersion
    {
        /// <summary>
        /// App Version の値
        /// </summary>
        private const int m_value = 20091700;

        /// <summary>
        /// App Version の値取得
        /// </summary>
        /// <value>The value.</value>
        public static int Value => m_value;
    }
}