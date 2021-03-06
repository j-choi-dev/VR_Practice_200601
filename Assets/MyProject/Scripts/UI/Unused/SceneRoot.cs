﻿using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Choi.MyProj.UI.Scene.Test
{
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] private ButtonForObject m_button;
        [SerializeField] private ButtonForCanvas aaa;

        private async void Start()
        {
            Debug.Log("[CHOI] Test Scene");
            await Init();
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("21_Game");
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
        public override async UniTask<bool> Init()
        {
            m_button.Init(OnButtonClick, OnButtonRelease);
            aaa.Init(aaaa, bbbb);
            return true;
        }

        public void OnButtonClick()
        {
            SceneChangeToNext();
        }

        public void OnButtonRelease()
        {
        }

        public void aaaa()
        {
            Debug.Log("Click");
        }

        public void bbbb()
        {
            Debug.Log("Released");
        }
    }
}