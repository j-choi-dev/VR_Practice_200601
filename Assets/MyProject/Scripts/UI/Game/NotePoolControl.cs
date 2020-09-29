using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Choi.MyProj.Domain.InGame;

namespace Choi.MyProj.UI.InGame
{
    /// <summary>
    /// Note Object Pool
    /// </summary>
    public sealed class NotePoolControl : MonoBehaviour
    {
        /// <summary>
        /// Left Prefab
        /// </summary>
        [SerializeField] private NoteObject m_prefabLeft;

        /// <summary>
        /// Right Prefab
        /// </summary>
        [SerializeField] private NoteObject m_prefabRight;

        /// <summary>
        /// Left Pool
        /// </summary>
        [SerializeField] private Queue<NoteObject> m_notePoolLeft;

        /// <summary>
        /// Right Pool
        /// </summary>
        [SerializeField] private Queue<NoteObject> m_notePoolRight;

        private void Awake()
        {
            m_notePoolLeft = new Queue<NoteObject>();
            m_notePoolRight = new Queue<NoteObject>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns><True/returns>
        public async Task<bool> Init()
        {
            Debug.Log($"NotePoolControl.Init()");
            for (int i = 0; i<Value.NoteCountMax; i++)
            {
                m_notePoolLeft.Enqueue(CreateNewObject(NoteSide.Left));
                m_notePoolRight.Enqueue(CreateNewObject(NoteSide.Right));
            }
            return true;
        }

        /// <summary>
        /// Note Object を新規生成
        /// </summary>
        /// <param name="side"Note の座標(これに基づいて Pool を選ぶ)></param>
        /// <returns>Note Object</returns>
        private NoteObject CreateNewObject(NoteSide side)
        {
            var newObj = Instantiate(side == NoteSide.Left ? m_prefabLeft : m_prefabRight).GetComponent<NoteObject>();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }

        /// <summary>
        /// Note Object を取得
        /// </summary>
        /// <param name="side"Note の座標(これに基づいて Pool を選ぶ)></param>
        /// <returns>Note Object</returns>
        public NoteObject GetObject(NoteSide side)
        {
            var poolingQueue = side == NoteSide.Left ? m_notePoolLeft : m_notePoolRight;
            if (poolingQueue.Count > 0)
            {
                var obj = poolingQueue.Dequeue();
                obj.transform.SetParent(null);
                //obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject(side);
                //newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        /// <summary>
        /// Note Object を返還
        /// </summary>
        /// <param name="obj">返還する Object</param>
        /// <returns>返還に成功したら True</returns>
        public bool ReturnObject(NoteObject obj)
        {
            var poolingQueue = obj.Side == NoteSide.Left ? m_notePoolLeft : m_notePoolRight;
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            poolingQueue.Enqueue(obj);
            return true;
        }
    }
}
