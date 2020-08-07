using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Choi.MyProj.UI
{
    /// <summary>
    /// イメージ単純代入スクリプト
    /// </summary>
    /// @j_choi 2020.08.06
    public class SimpleImage : MonoBehaviour
    {
        /// <summary>
        /// Image Component
        /// </summary>
        [SerializeField] private Image m_image;

        /// <summary>
        /// 画像をセット
        /// </summary>
        /// <param name="sprite">Sprite</param>
        public void Set(Sprite sprite)
        {
            m_image.sprite = sprite;
        }
    }
}