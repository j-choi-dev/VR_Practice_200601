using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Choi.MyProj.UI.Scene.Main
{
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] private ButtonForObject m_button;

        private async void Start()
        {
            Debug.Log("[CHOI] Main Scene");
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
            return true;
        }

        public void OnButtonClick()
        {
            SceneChangeToNext();
        }

        public void OnButtonRelease()
        {
        }
    }
}