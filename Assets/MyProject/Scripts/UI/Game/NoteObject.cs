using System;
using UnityEngine;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    /// <summary>
    /// Note Object
    /// </summary>
    public sealed class NoteObject : MonoBehaviour
    {
        /// <summary>
        /// Collider
        /// </summary>
        [SerializeField] private Collider m_collider;

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
        private Transform m_destTr;

        /// <summary>
        /// 進行方向
        /// </summary>
        private Vector3 m_dirVec;

        /// <summary>
        /// Pool に戻す(For Over)
        /// </summary>
        private Action<NoteObject, Judgement> m_judgementAction;

        /// <summary>
        /// Note Object Init
        /// </summary>
        /// <param name="side"Note の位置></param>
        public void Init(int id, NoteType type, Transform startTr, Transform destTr, Action<NoteObject, Judgement> judgementAction)
        {
            ID = id;
            Type = type;
            m_destTr = destTr;
            m_judgementAction = judgementAction;
            transform.localPosition = startTr.position;
            m_dirVec = (m_destTr.position - transform.localPosition).normalized;
        }

        /// <summary>
        /// Fixed Update
        /// </summary>
        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;
            var noowDist = Vector3.Distance(m_destTr.localPosition, transform.localPosition);
            m_collider.enabled = noowDist < Value.ColliderActiveDistance ? true : false;
            if (noowDist > Value.OverJudgementDistance && transform.localPosition.z - m_destTr.position.z < Value.OverJudgementDelta)
            {
                Debug.Log($"TODO Poolに返す(Over)_{name}");
                m_judgementAction(this, Judgement.Miss);
                return;
            }
            transform.Translate(m_dirVec * Time.deltaTime, Space.World);
        }
    }
}