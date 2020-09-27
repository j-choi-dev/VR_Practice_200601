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

        /// <summary>
        /// Note Object Init
        /// </summary>
        /// <param name="side"Note の位置></param>
        public void Init(NoteSide side)
        {
            Side = side;
        }
    }
}