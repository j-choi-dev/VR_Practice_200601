using UnityEngine;

namespace Choi.MyProj.UI.System
{
    /// <summary>
    /// Editorでの Virtual Camera 制御スクリプト
    /// </summary>
    /// @j_choi 2020.08.06
    public class VirtualCameraInEditor : MonoBehaviour
    {
        /// <summary>
        /// Rotation Speed
        /// </summary>
        private const float turnSpeed = 270;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// 活性化される旅の挙動
        /// </summary>
        private void OnEnable()
        {
            Debug.Log("OnEnable");
            transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Fixed Update
        /// </summary>
        private void FixedUpdate()
        {
            var h = Input.GetAxis("Horizontal");
            //Turn
            transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        }
    }
}