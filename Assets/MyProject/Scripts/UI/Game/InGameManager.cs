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

        private NoteInfoControl m_noteInfoControl;

        private IList<NoteInfo> m_notetList;
        private IList<NoteResult> m_resultList;

        private int MockValue = 10;

        private void Awake()
        {
            m_resultList = new List<NoteResult>();
            m_noteInfoControl = new NoteInfoControl();
        }

        // Start is called before the first frame update
        public async UniTask<bool> Init()
        {
            Debug.Log($"InGameManager.Init()");
            if(!await m_notePool.Init())
            {
                Debug.LogError("m_notePool.Init() FAILED");
                return false;
            }

            m_notetList = await m_noteInfoControl.Init();

            if (!await m_musicControl.Init())
            {
                Debug.LogError("m_musicControl.Init() FAILED");
                return false;
            }
            return true;
        }

        public async UniTask<bool> Run()
        {
            var mockId = 0;
            var mockDelay = 500;
            NoteType mockType;

            while (mockId < MockValue)
            {
                var add = Random.Range(1, 5) * 100;
                mockType = mockId % 2 == 0 ? NoteType.Left : NoteType.Right;
                var startTr = mockType == NoteType.Left ? m_startLeft : m_startRight;
                var destTr = mockType == NoteType.Left ? m_destLeft : m_destRight;
                var note = m_notePool.GetObject(mockType);
                note.name = $"{mockId}_{mockType}";
                note.Init(mockId, mockType, startTr, destTr, Judgement);
                note.gameObject.SetActive(true);
                await UniTask.Delay(mockDelay + add, ignoreTimeScale: true, delayTiming: PlayerLoopTiming.FixedUpdate);
                mockId++;
                Debug.Log(mockId);
                if (mockId >= 10) break;
            }
            while(m_resultList.Count < MockValue)
            {
                await UniTask.WaitForEndOfFrame();
            }
            foreach( var result in m_resultList)
            {
                Debug.Log($"RESULT : {result.ID}, {result.Judgement}");
            }
            return true;
        }

        private void FixedUpdate()
        {
            Ray ray;
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)

#elif UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawLine(Manager.Instance.NowCamera.transform.position, ray.direction * 100f);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Value.NoteObjectLayer))
                {
                    Debug.Log($"TODO Poolに返す(判定) : { hitInfo.point},{ hitInfo.collider.name}");
                    var note = hitInfo.collider.GetComponent<NoteObject>();
                    if(note == null)
                    {
                        Debug.LogError("NoteObject Component is NULL");
                        return;
                    }
                    Judgement(note);
                }
            }
#endif
        }

        private void Judgement(NoteObject note, Judgement defaultScore = Domain.InGame.Judgement.None)
        {
            var destTr = note.Type == NoteType.Left ? m_destLeft : m_destRight;
            var judge = defaultScore;
            if(defaultScore != Domain.InGame.Judgement.Miss)
            {
                var noowDist =(int) (Mathf.Abs(Vector3.Distance(destTr.localPosition, note.transform.localPosition)) * 100f);
                if(noowDist < Value.ScoreJedge.Bad && noowDist >= Value.ScoreJedge.Good)
                {
                    judge = Domain.InGame.Judgement.Bad;
                }
                else if (noowDist < Value.ScoreJedge.Good && noowDist >= Value.ScoreJedge.Greate)
                {
                    judge = Domain.InGame.Judgement.Good;
                }
                else if (noowDist < Value.ScoreJedge.Greate && noowDist >= Value.ScoreJedge.Perfect)
                {
                    judge = Domain.InGame.Judgement.Greate;
                }
                else if (noowDist < Value.ScoreJedge.Perfect)
                {
                    judge = Domain.InGame.Judgement.Perfect;
                }
                else
                {
                    judge = Domain.InGame.Judgement.Miss;
                }
            }
            var result = new NoteResult(note.ID, judge);
            Debug.Log($"Add({result.ID},{result.Judgement})");
            m_resultList.Add(result);
            m_notePool.ReturnObject(note);
        }
    }
}