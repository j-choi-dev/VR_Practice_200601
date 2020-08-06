using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Choi.MyProj.UI
{
    /// <summary>
    /// テキスト単純代入スクリプト
    /// </summary>
    /// @j_choi 2020.08.06
    public class SimpleText : MonoBehaviour
    {
        /// <summary>
        /// Text Component
        /// </summary>
        [SerializeField] private Text m_text;

        /// <summary>
        /// 文字列をセット
        /// </summary>
        /// <param name="text">String Value</param>
        public void Set(Object text)
        {
            m_text.text = text.ToString();
        }
    }
}