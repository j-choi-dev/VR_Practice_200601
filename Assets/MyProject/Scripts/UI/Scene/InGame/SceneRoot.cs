using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.UI.InGame;

namespace Choi.MyProj.UI.Scene.InGame
{
    public sealed class SceneRoot : SceneRootBase
    {
        [SerializeField] private InGameManager m_manager;

        [SerializeField] private ButtonForCanvas m_pause;
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

        public void OnButtonClick()
        {
            Debug.Log("Pause");
            
        }

        public void OnButtonRelease()
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