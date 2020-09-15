using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using Choi.MyProj.Interface.API.System;

namespace Choi.MyProj.UI
{
	public class VirtualGazeControl : MonoBehaviour
	{
		/// <summary>
		/// 現在見ている Object
		/// </summary>
		private GameObject m_currentLookAtObject;

		/// <summary>
		/// 現在見ているボタン
		/// </summary>
		private ButtonImpl m_currentLookAtButton;

		/// <summary>
		/// 累積 Geza 時間
		/// </summary>
		private float m_currentLookAtHandlerClickTime;


		void FixedUpdate()
		{
			GazeProcess();
		}

		/// <summary>
		/// Gaze Process
		/// </summary>
		private void GazeProcess()
		{
#if UNITY_EDITOR
			var virtualState = new Repository.Editor.VirtualStateInEditorRepository();
			if (virtualState.Get() != Domain.System.VirtualState.Virtual) return;
#else
			if (VirtualControlAPI.Instance.NowCameraState != Domain.System.VirtualState.Virtual) return;
#endif
            var ray = Manager.Instance.NowCamera.ScreenPointToRay((Vector2)Manager.Instance.NowCamera.transform.forward);
			if (Application.isEditor) Debug.DrawLine(ray.origin, ray.direction * 100f, Color.red);
			// Rayの長さ
			float maxDistance = 10;
			var hit = Physics2D.Raycast(ray.origin, Manager.Instance.NowCamera.transform.forward, maxDistance);

			if (hit.collider == null)
			{
				if (m_currentLookAtButton == null) return;
				m_currentLookAtButton.OnButtonRelease(); 
				m_currentLookAtButton = null;
				m_currentLookAtObject = null;
				return;
			}
			if (m_currentLookAtObject != hit.collider.gameObject)
			{
				m_currentLookAtObject = hit.collider.gameObject;
				m_currentLookAtHandlerClickTime = Time.realtimeSinceStartup + 2f;
				m_currentLookAtButton = m_currentLookAtObject.GetComponent<ButtonImpl>();
				if (m_currentLookAtButton == null) return;
			}
			if (m_currentLookAtObject != null && Time.realtimeSinceStartup > m_currentLookAtHandlerClickTime)
			{
				m_currentLookAtHandlerClickTime = float.MaxValue;
				m_currentLookAtButton.OnButtonClick();
			}
		}
	}
}