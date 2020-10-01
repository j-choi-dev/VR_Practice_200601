using System;
using UnityEngine;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    /// <summary>
    /// Note Object
    /// </summary>
    public class NoteObject : MonoBehaviour
    {
        /// <summary>
        /// Collider
        /// </summary>
        [SerializeField] protected Collider m_collider;

        /// <summary>
        /// Renderer
        /// </summary>
        [SerializeField] protected Renderer m_renderer;

        /// <summary>
        /// Note ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Note の位置
        /// </summary>
        public NoteType Type { get; private set; }

        /// <summary>
        /// 目標 Trasnform 
        /// </summary>
        public Transform m_destTr { get; private set; }

        /// <summary>
        /// 進行方向
        /// </summary>
        private Vector3 m_dirVector;

        /// <summary>
        /// Pool に戻す(For Over)
        /// </summary>
        private Action<NoteObject, Judgement> m_judgementAction;

        /// <summary>
        /// Note Object Init
        /// </summary>
        /// <param name="side"Note の位置></param>
        public void Init(int id, NoteType type, Material material, Transform startTr, Transform destTr, Action<NoteObject, Judgement> judgementAction)
        {
            ID = id;
            Type = type;
            m_renderer.material = material;
            m_destTr = destTr;
            m_judgementAction = judgementAction;
            transform.localPosition = startTr.position;
            m_dirVector = (m_destTr.position - transform.localPosition).normalized;
        }

        /// <summary>
        /// Fixed Update
        /// </summary>
        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;
            if (Type == NoteType.Left || Type == NoteType.Right)
            {
                MusicNoteMoveProcess();
            }
            else
            {
                FlagNoteMoveProcess();
            }
        }

        public void MusicNoteMoveProcess()
        {
            var noowDist = Vector3.Distance(m_destTr.localPosition, transform.localPosition);
            m_collider.enabled = noowDist < Value.ColliderActiveDistance ? true : false;
            if (noowDist > Value.OverJudgementDistance && transform.localPosition.z - m_destTr.position.z < Value.OverJudgementDelta)
            {
                //Debug.Log($"TODO Poolに返す(Over)_{name}");
                m_judgementAction(this, Judgement.Miss);
                return;
            }
            transform.Translate(m_dirVector * 2f * Time.deltaTime, Space.World);
        }

        public void FlagNoteMoveProcess()
        {
            var noowDist = Vector3.Distance(m_destTr.localPosition, transform.localPosition);
            m_collider.enabled = noowDist < Value.ColliderActiveDistance ? true : false;
            if (noowDist < Value.OverJudgementDistance)
            {
                Debug.Log($"TODO 音楽再生イベント実行{name}:{Type}");
                m_judgementAction(this, Judgement.None);
            }
            transform.Translate(m_dirVector * 2f * Time.deltaTime, Space.World);
        }
    }
}