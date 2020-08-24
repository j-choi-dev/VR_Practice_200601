using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Choi.MyProj.UI.Scene.Init
{
    public class SceneRoot : SceneRootBase
    {
        [SerializeField] private Camera m_camera;

        public override Camera GetSceneDefaultCamera()
        {
            return m_camera;
        }

        private void Start()
        {
            Debug.Log("[CHOI] Init Scene");
        }

        public override void SceneChangeToNext()
        {
            SceneManager.LoadScene("01_Switch");
        }

        public override async UniTask<bool> Init()
        {
            return true;
        }
    }
}