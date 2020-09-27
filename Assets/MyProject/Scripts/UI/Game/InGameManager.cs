using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Choi.MyProj.UI.InGame
{
    public sealed class InGameManager : MonoBehaviour
    {
        [SerializeField] private Transform m_startLeft;
        [SerializeField] private Transform m_startRight;

        [SerializeField] private Transform m_destLeft;
        [SerializeField] private Transform m_destRight;

        [SerializeField] private NotePoolControl m_notePool;

        // Start is called before the first frame update
        public async Task Init()
        {
            Debug.Log($"InGameManager.Init()");
            await m_notePool.Init();
        }
    }
}