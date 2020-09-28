using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.UI.InGame;

namespace Choi.MyProj.UI.Scene.InGame
{
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] private InGameManager m_manager;
        /// <summary>
        /// Start
        /// </summary>
        private async void Start()
        {
            await Init();
            await m_manager.Run();
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
            await m_manager.Init();
            return true;
        }
    }
}