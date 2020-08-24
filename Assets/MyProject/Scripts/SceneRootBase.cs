using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Choi.MyProj.UI.Scene
{
    /// <summary>
    /// シーン管理スクリプトの Base
    /// </summary>
    public abstract class SceneRootBase : MonoBehaviour
    {
        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            var isActive = Manager.Instance.IsActive;
            Debug.Log("SceneRootBase Awake");
        }

        /// <summary>
        /// シーンで使われる Default カメラを外からアクセス
        /// </summary>
        /// <returns>シーンで使われる Default カメラ</returns>
        public abstract Camera GetSceneDefaultCamera();

        /// <summary>
        /// シーンチェンジメソッド
        /// </summary>
        public abstract void SceneChangeToNext();

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Initialize Result</returns>
        public abstract UniTask<bool> Init();
    }
}