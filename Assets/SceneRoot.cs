using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Choi.MyProj.UI.Scene
{
    public class SceneRoot : MonoBehaviour
    {
        public void OnButtonClick()
        {
            SceneManager.LoadScene("01_Switch");
        }
    }
}