using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    public sealed class InGameManager : MonoBehaviour
    {
        [SerializeField] private Transform m_startLeft;
        [SerializeField] private Transform m_startRight;

        [SerializeField] private Transform m_destLeft;
        [SerializeField] private Transform m_destRight;

        [SerializeField] private NotePoolControl m_notePool;
        [SerializeField] private MusicControl m_musicControl;

        // Start is called before the first frame update
        public async UniTask<bool> Init()
        {
            Debug.Log($"InGameManager.Init()");
            if(!await m_notePool.Init())
            {
                Debug.LogError("m_notePool.Init() FAILED");
                return false;
            }
            if (!await m_musicControl.Init())
            {
                Debug.LogError("m_musicControl.Init() FAILED");
                return false;
            }
            return true;
        }

        public async UniTask<bool> Run()
        {
            var count = 0;
            var mockDelay = 500;

            while (count < 10)
            {
                var add = Random.Range(1, 5) * 100;
                NoteSide noteSide = count % 2 == 0 ? NoteSide.Left : NoteSide.Right;
                var startTr = noteSide == NoteSide.Left ? m_startLeft : m_startRight;
                var destTr = noteSide == NoteSide.Left ? m_destLeft : m_destRight;
                var note = m_notePool.GetObject(noteSide);
                note.Init(noteSide, startTr, destTr);
                note.gameObject.SetActive(true);
                await UniTask.Delay(mockDelay + add, ignoreTimeScale: true, delayTiming: PlayerLoopTiming.FixedUpdate);
                count++;
            }
            return true;
        }
    }
}