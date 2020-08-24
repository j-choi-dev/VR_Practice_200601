using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Choi.MyProj.UI.Scene
{
    public abstract class SceneRootBase : MonoBehaviour
    {
        public abstract Camera GetSceneDefaultCamera();

        public abstract void SceneChangeToNext();

        public abstract UniTask<bool> Init();
    }
}