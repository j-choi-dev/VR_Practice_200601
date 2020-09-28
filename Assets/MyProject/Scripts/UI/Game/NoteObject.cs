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
        /// Note の位置
        /// </summary>
        public NoteSide Side { get; private set; }

        private Transform m_destTr;

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
            m_dirVec = (m_destTr.localPosition -transform.localPosition).normalized;
        }

        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;
            transform.Translate(m_dirVec * Time.deltaTime, Space.World);
        }
    }
}