using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Choi.MyProj.UI.Scene.ModeChangeScene
{
    /// <summary>
    /// Guide Image Control
    /// </summary>
    public class GuideImageControl : MonoBehaviour
    {
        /// <summary>
        /// LandScape Guide Sprites
        /// </summary>
        [SerializeField] private Sprite[] m_guideImage;

        /// <summary>
        /// Sprite Count
        /// </summary>
        public int Length => m_guideImage.Length;

        /// <summary>
        /// Sprite 取得
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Sprite</returns>
        public Sprite GetSprite(int index) => m_guideImage[index];
    }
}