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

        [SerializeField] private Material m_leftNoteMaterial;
        [SerializeField] private Material m_rightNoteMaterial;
        [SerializeField] private Material m_flagNoteMaterial;

        private NoteInfoControl m_noteInfoControl;

        private IList<NoteInfo> m_noteList;
        private IList<NoteResult> m_resultList;

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

            m_noteList = await m_noteInfoControl.Init();

            if (!await m_musicControl.Init())
            {
                Debug.LogError("m_musicControl.Init() FAILED");
                return false;
            }
            return true;
        }

        public async UniTask<bool> Run()
        {
            Transform startTr;
            Transform destTr;
            Material material;

            foreach (var note in m_noteList)
            {
                var noteObject = m_notePool.GetObject();
                noteObject.name = $"{note.ID}_{note.Type}";
                if (note.Type == NoteType.Start || note.Type == NoteType.Finish)
                {
                    startTr = note.Type == NoteType.Start ? m_startLeft : m_startRight;
                    destTr = note.Type == NoteType.Start ? m_destLeft : m_destRight;
                    material = m_flagNoteMaterial;
                }
                else
                {
                    startTr = note.Type == NoteType.Left ? m_startLeft : m_startRight;
                    destTr = note.Type == NoteType.Left ? m_destLeft : m_destRight;
                    material = note.Type == NoteType.Left ? m_leftNoteMaterial : m_rightNoteMaterial;
                }
                noteObject.Init(note.ID, note.Type, material, startTr, destTr, NoteJudgement);
                await UniTask.Delay(note.DeltaTime > 0 ? note.DeltaTime : 1, ignoreTimeScale: true, delayTiming: PlayerLoopTiming.FixedUpdate);
                noteObject.gameObject.SetActive(true);
            }
            while (m_resultList.Count < m_noteList.Count)
            {
                await UniTask.WaitForEndOfFrame();
            }
            foreach (var result in m_resultList)
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
                    NoteJudgement(note);
                }
            }
#endif
        }

        private void NoteJudgement(NoteObject note, Judgement defaultScore = Judgement.None)
        {
            var destTr = note.Type == NoteType.Left ? m_destLeft : m_destRight;
            var judge = defaultScore;
            if(defaultScore != Judgement.None)
            {
                var noowDist =(int) (Mathf.Abs(Vector3.Distance(destTr.localPosition, note.transform.localPosition)) * 100f);
                if(noowDist < Value.ScoreJedge.Bad && noowDist >= Value.ScoreJedge.Good)
                {
                    judge = Judgement.Bad;
                }
                else if (noowDist < Value.ScoreJedge.Good && noowDist >= Value.ScoreJedge.Greate)
                {
                    judge = Judgement.Good;
                }
                else if (noowDist < Value.ScoreJedge.Greate && noowDist >= Value.ScoreJedge.Perfect)
                {
                    judge = Judgement.Greate;
                }
                else if (noowDist < Value.ScoreJedge.Perfect)
                {
                    judge = Judgement.Perfect;
                }
                else
                {
                    judge = Judgement.Miss;
                }
            }
            else
            {
                if(note.Type == NoteType.Start && !m_musicControl.IsPlayingNow)
                {
                    Debug.Log("Music Start");
                    m_musicControl.PlayMusic();
                }
                else if (note.Type == NoteType.Finish && m_musicControl.IsPlayingNow)
                {
                    m_musicControl.StopMusic();
                }
            }
            var result = new NoteResult(note.ID, judge);
            m_resultList.Add(result);
            m_notePool.ReturnObject(note);
        }
    }
}