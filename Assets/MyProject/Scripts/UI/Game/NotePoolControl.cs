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
        [SerializeField] private NoteObject m_prefab;

        /// <summary>
        /// Left Pool
        /// </summary>
        [SerializeField] private Queue<NoteObject> m_notePool;

        private void Awake()
        {
            m_notePool = new Queue<NoteObject>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <returns><True/returns>
        public async Task<bool> Init()
        {
            Debug.Log($"NotePoolControl.Init()");
            for (int i = 0; i<Value.MockCount; i++)
            {
                m_notePool.Enqueue(CreateNewObject());
            }
            return true;
        }

        /// <summary>
        /// Note Object を新規生成
        /// </summary>
        /// <param name="side"Note の座標(これに基づいて Pool を選ぶ)></param>
        /// <returns>Note Object</returns>
        private NoteObject CreateNewObject()
        {
            var newObj = Instantiate(m_prefab).GetComponent<NoteObject>();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }

        /// <summary>
        /// Note Object を取得
        /// </summary>
        /// <param name="side"Note の座標(これに基づいて Pool を選ぶ)></param>
        /// <returns>Note Object</returns>
        public NoteObject GetObject()
        {
            if (m_notePool.Count > 0)
            {
                var obj = m_notePool.Dequeue();
                obj.transform.SetParent(null);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
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
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            m_notePool.Enqueue(obj);
            return true;
        }
    }
}
