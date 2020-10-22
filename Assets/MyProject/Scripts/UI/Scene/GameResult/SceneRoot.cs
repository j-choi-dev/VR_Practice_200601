using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.UI.InGame;

namespace Choi.MyProj.UI.Scene.GameResult
{
    public sealed class SceneRoot : SceneRootBase
    {

        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            await Init();
        }

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public override void SceneChangeToNext()
        {
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
        public override async UniTask<bool> Init()
        {
            return true;
        }
    }
}