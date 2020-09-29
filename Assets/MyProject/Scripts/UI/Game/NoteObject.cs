using System.Collections;
using System.Collections.Generic;
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
        /// Note の位置
        /// </summary>
        public NoteSide Side { get; private set; }

        /// <summary>
        /// 目標 Trasnform 
        /// </summary>
        private Transform m_destTr;

        /// <summary>
        /// 進行方向
        /// </summary>
        private Vector3 m_dirVec;

        /// <summary>
        /// Note Object Init
        /// </summary>
        /// <param name="side"Note の位置></param>
        public void Init(NoteSide side, Transform startTr, Transform destTr)
        {
            Side = side;
            m_destTr = destTr;
            transform.localPosition = startTr.position;
            m_dirVec = (m_destTr.position - transform.localPosition).normalized;
        }

        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;
            var noowDist = Vector3.Distance(m_destTr.localPosition, transform.localPosition);
            m_collider.enabled = noowDist < 1.5f ? true : false;
            if (noowDist > 1 && transform.localPosition.z - m_destTr.position.z < -0.5f)
            {
                Debug.Log($"TODO Poolに返す(Over)_{name}");
            }
            transform.Translate(m_dirVec * Time.deltaTime, Space.World);
        }
    }
}