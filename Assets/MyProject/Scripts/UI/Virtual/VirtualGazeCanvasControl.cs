using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using Choi.MyProj.Interface.API.Virtual;
using Choi.MyProj.Domain.Virtual;
using Choi.MyProj.Domain.Common;

namespace Choi.MyProj.UI.Virtual
{
    /// <summary>
    /// 凝視によってボタンの役割を担当する Object をコントロールするスクリプト
    /// </summary>
	public sealed class VirtualGazeCanvasControl : MonoBehaviour
	{
		/// <summary>
		/// 現在見ている Object(Canvas)
		/// </summary>
		private GameObject m_currentLookAtCanvasObject = null;

		/// <summary>
		/// 現在見ているボタン
		/// </summary>
		private IButtonImpl m_currentLookAtButton;

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
			if (virtualState.Get() != VirtualState.Virtual
                || VirtualControlAPI.Instance.NowCameraState != VirtualState.Virtual) return;
#else
			if (VirtualControlAPI.Instance.NowCameraState != VirtualState.Virtual) return;
#endif
            var ray = Manager.Instance.NowCamera.ScreenPointToRay((Vector2)Manager.Instance.NowCamera.transform.forward);
			if (Application.isEditor) Debug.DrawLine(ray.origin, ray.direction * 100f, Color.red);
			var hit = Physics2D.Raycast(ray.origin, Manager.Instance.NowCamera.transform.forward, Mathf.Infinity);

			if (hit.collider == null)
			{
				if (m_currentLookAtButton == null) return;
				m_currentLookAtButton.OnButtonRelease(); 
				m_currentLookAtButton = null;
				m_currentLookAtCanvasObject = null;
				return;
			}
			if (m_currentLookAtCanvasObject != hit.collider.gameObject)
			{
				m_currentLookAtCanvasObject = hit.collider.gameObject;
				m_currentLookAtHandlerClickTime = Time.realtimeSinceStartup + 2f;
				m_currentLookAtButton = m_currentLookAtCanvasObject.GetComponent<ButtonForCanvas>();
				if (m_currentLookAtButton == null) return;
			}
			if (m_currentLookAtCanvasObject != null && Time.realtimeSinceStartup > m_currentLookAtHandlerClickTime)
			{
				m_currentLookAtHandlerClickTime = float.MaxValue;
				m_currentLookAtButton.OnButtonClick();
			}
		}
	}
}