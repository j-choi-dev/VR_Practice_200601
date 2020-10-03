using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Choi.MyProj.Interface.API
{
    public sealed class AssetLoaderAPI
    {
        /// <summary>
        /// Lazy Instance
        /// </summary>
        private static readonly Lazy<AssetLoaderAPI> m_instance = new Lazy<AssetLoaderAPI>(() => new AssetLoaderAPI());

        /// <summary>
        /// 外からアクセスできる Lazy Instance
        /// </summary>
        public static AssetLoaderAPI Instance => m_instance.Value;

        /// <summary>
        /// Audio Clip Load
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>Audio Clip</returns>
        public async UniTask<AudioClip> LoadAudioClip(string fileName)
        {
            var path = $"AudioSource/{fileName}";
            var asset = await LoadAsset(path);
            Debug.Log($"<AudioClip> {asset.name}");
            return asset as AudioClip;
        }

        /// <summary>
        /// TextAsset Load
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>TextAsset</returns>
        public async UniTask<TextAsset> LoadTextData(string fileName)
        {
            var path =$"TextAssets/{fileName}";
            var asset = await LoadAsset(path);
            Debug.Log($"<TextAsset> {asset.name}");
            return asset as TextAsset;
        }

        /// <summary>
        /// Asset Load
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private async UniTask<Object> LoadAsset(string path)
        {
            var request = Resources.LoadAsync<Object>($"{path}");
            if (request.asset == null)
            {
                Debug.Log($"Couldn't Find '{request.asset.name}'");
                return null;
            }
            while (!request.isDone)
            {
                await UniTask.WaitForFixedUpdate();
                Debug.Log($"{request.asset.name} : {request.progress * 100f} %");
            }
            return request.asset;
        }
    }
}