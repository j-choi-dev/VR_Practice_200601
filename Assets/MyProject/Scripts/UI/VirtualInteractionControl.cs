using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

namespace Choi.MyProj.UI
{
	public class VirtualInteractionControl : PointerInputModule
	{
		/// <summary>
		/// RayCast結果
		/// </summary>
		public RaycastResult CurrentRaycast { get; private set; }

		/// <summary>
		/// PointerEventData
		/// </summary>
		private PointerEventData m_pointerEventData;

        /// <summary>
        /// 現在見ている　Object
        /// </summary>
		private GameObject m_currentLookAtHandler;

        /// <summary>
        /// 累積 Geza 時間
        /// </summary>
        private float m_currentLookAtHandlerClickTime;

		/// <summary>
		/// PointerInputModule.Process()　景勝
		/// </summary>
        /// <remarks>Updateタイムで呼ばれる</remarks>
		public override void Process()
		{
			CanvasObjectLook();
			HandleSelection();
		}

        /// <summary>
        /// Canvas Object を見ているのか確認するメソッド
        /// </summary>
		void CanvasObjectLook()
		{
			if (m_pointerEventData == null)
			{
				m_pointerEventData = new PointerEventData(eventSystem);
			}
			// fake a pointer always being at the center of the screen
			m_pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);
			m_pointerEventData.delta = Vector2.zero;

			var raycastResults = new List<RaycastResult>();
			eventSystem.RaycastAll(m_pointerEventData, raycastResults);
			CurrentRaycast = m_pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
			ProcessMove(m_pointerEventData);
		}

        /// <summary>
        /// Handler Event がある場合、実行する
        /// </summary>
		void HandleSelection()
		{
			if (m_pointerEventData.pointerEnter == null)
			{
				m_pointerEventData = null;
				return;
			}
			// if the ui receiver has changed, reset the gaze delay timer
			var handler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_pointerEventData.pointerEnter);
			if (m_currentLookAtHandler != handler)
			{
				m_currentLookAtHandler = handler;
				m_currentLookAtHandlerClickTime = Time.realtimeSinceStartup + 2f;
			}

			// if we have a handler and it's time to click, do it now
			if (m_currentLookAtHandler != null && Time.realtimeSinceStartup > m_currentLookAtHandlerClickTime)
			{
				EventSystem.current.SetSelectedGameObject(m_currentLookAtHandler);
				ExecuteEvents.ExecuteHierarchy(m_currentLookAtHandler, m_pointerEventData, ExecuteEvents.pointerClickHandler);
				m_currentLookAtHandlerClickTime = float.MaxValue;
				ExecuteEvents.ExecuteHierarchy(EventSystem.current.currentSelectedGameObject, m_pointerEventData, ExecuteEvents.deselectHandler);
			}
		}
	}
}