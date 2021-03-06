﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Choi.MyProj.Domain.Virtual;
using Choi.MyProj.Interface.API.Virtual;

namespace Choi.MyProj.UI.Scene.Init
{
    /// <summary>
    /// Init Scene Root Class
    /// </summary>
    public class SceneRoot : SceneRootBase
    {
        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            Debug.Log("[CHOI] Init Scene");
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("01_Switch");
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns></returns>
        public override async UniTask<bool> Init()
        {
            return true;
        }
    }
}