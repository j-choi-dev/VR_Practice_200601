using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        /// Audioc Clip
        /// </summary>
        private AudioClip m_nowClip;

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

            var mock = "AudioSource/SampleMusic";
            var request = Resources.LoadAsync<AudioClip>(mock);
            if (request.asset == null)
            {
                Debug.Log($"Couldn't Find '{request.asset.name}'");
                return false;
            }
            while (!request.isDone)
            {
                await UniTask.WaitForFixedUpdate();
                Debug.Log($"{request.asset.name} : {request.progress * 100f} %");
            }
            m_nowClip = request.asset as AudioClip;

            m_audioSource.clip = m_nowClip;
            return true;
        }
    }
}