using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Choi.MyProj.Interface.API;
using Cysharp.Threading.Tasks;


namespace Choi.MyProj.UI.InGame
{
    /// <summary>
    /// 音楽再生コトロール
    /// </summary>
    public class MusicControl : MonoBehaviour
    {
        /// <summary>
        /// Audio Source
        /// </summary>
        [SerializeField] private AudioSource m_audioSource;

        /// <summary>
        /// Audio Play
        /// </summary>
        public void PlayMusic() => m_audioSource.Play();

        /// <summary>
        /// Audio Stop
        /// </summary>
        public void StopMusic() => m_audioSource.Stop();

        /// <summary>
        /// Init
        /// </summary>
        /// <returns>Init Result</returns>
        public async UniTask<bool> Init()
        {
            if (m_audioSource == null) return false;
            m_audioSource.playOnAwake = false;
            m_audioSource.loop = false;

            var mock = "Song_001";
            var clip = await AssetLoaderAPI.Instance.LoadAudioClip(mock);
            m_audioSource.clip = clip;
            return true;
        }
    }
}