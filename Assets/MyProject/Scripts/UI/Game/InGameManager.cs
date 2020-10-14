using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    public sealed class InGameManager : MonoBehaviour
    {
        /// <summary>
        /// Note Object 生成 Transform 左
        /// </summary>
        [SerializeField] private Transform m_startLeft;

        /// <summary>
        /// Note Object 生成 Transform 右
        /// </summary>
        [SerializeField] private Transform m_startRight;

        /// <summary>
        /// Note Object 移動目標 Transform 左
        /// </summary>
        [SerializeField] private Transform m_destLeft;

        /// <summary>
        /// Note Object 移動目標 Transform 右
        /// </summary>
        [SerializeField] private Transform m_destRight;

        /// <summary>
        /// Note Object Pool Controller
        /// </summary>
        [SerializeField] private NotePoolControl m_notePool;

        /// <summary>
        /// Music Controller
        /// </summary>
        [SerializeField] private MusicControl m_musicControl;

        /// <summary>
        /// Note Object Material 左
        /// </summary>
        [SerializeField] private Material m_leftNoteMaterial;

        /// <summary>
        /// Note Object Material 右
        /// </summary>
        [SerializeField] private Material m_rightNoteMaterial;

        /// <summary>
        /// Start/Finish など Flag Object Material 右
        /// </summary>
        [SerializeField] private Material m_flagNoteMaterial;

        /// <summary>
        /// Note　生成情報をインポートするクラス
        /// </summary>
        private NoteInfoControl m_noteInfoControl;

        /// <summary>
        /// 生成される Note のリスト
        /// </summary>
        private IList<NoteInfo> m_noteList;

        /// <summary>
        /// 判定結果を帆zんするリスト
        /// </summary>
        private IList<NoteResult> m_resultList;

        /// <summary>
        /// 一時停止
        /// </summary>
        private bool m_isPause;

        /// <summary>
        /// Npte Object の移動を一時停止するデリゲート
        /// </summary>
        /// <param name="isPause"></param>
        private delegate void SetNotePause(bool isPause);

        /// <summary>
        /// Npte Object の移動を一時停止するデリゲート
        /// </summary>
        private SetNotePause m_setNotePause;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            m_resultList = new List<NoteResult>();
            m_noteInfoControl = new NoteInfoControl();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <returns>Init 結果</returns>
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

        /// <summary>
        /// InGame　実行
        /// </summary>
        /// <returns>Init 結果</returns>
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
                    // 開始/終了などのフラグNote Objet
                    startTr = note.Type == NoteType.Start ? m_startLeft : m_startRight;
                    destTr = note.Type == NoteType.Start ? m_destLeft : m_destRight;
                    material = m_flagNoteMaterial;
                }
                else
                {
                    // 一般的なNote Objet
                    startTr = note.Type == NoteType.Left ? m_startLeft : m_startRight;
                    destTr = note.Type == NoteType.Left ? m_destLeft : m_destRight;
                    material = note.Type == NoteType.Left ? m_leftNoteMaterial : m_rightNoteMaterial;
                }

                // Note Object Init in Here
                noteObject.Init(note.ID, note.Type, material, startTr, destTr, NoteJudgement);
                if(m_setNotePause == null)
                {
                    m_setNotePause = new SetNotePause(noteObject.SetIsPause);
                }
                else
                {
                    m_setNotePause += noteObject.SetIsPause;
                }

                while (m_isPause)
                {
                    await UniTask.Delay(1);
                }
                await UniTask.Delay(note.DeltaTime > 0 ? note.DeltaTime : 1,
                    ignoreTimeScale: true,
                    delayTiming: PlayerLoopTiming.FixedUpdate);

                noteObject.gameObject.SetActive(true);
            }

            // InGameの終了条件まで待機
            while (m_resultList.Count < m_noteList.Count)
            {
                await UniTask.WaitForEndOfFrame();
            }
            return true;
        }

        /// <summary>
        /// Fixed Upadte
        /// </summary>
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

        /// <summary>
        /// Note 判定メソッド
        /// </summary>
        /// <param name="note">判定する Note Object</param>
        /// <param name="defaultScore">明らかなMissではない場合の基本指定</param>
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
            m_setNotePause -= note.SetIsPause;
            m_notePool.ReturnObject(note);
        }

        /// <summary>
        /// ゲーム全体の動作を一時停止/Continue
        /// </summary>
        public void PauseAllGameProcess()
        {
            m_isPause = !m_isPause;
            m_setNotePause(m_isPause);
            if (m_isPause)
            {
                m_musicControl.Pause();
            }
            else
            {
                m_musicControl.Continue();
            }
        }
    }
}