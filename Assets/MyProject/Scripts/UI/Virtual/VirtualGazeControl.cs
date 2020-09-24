using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using Choi.MyProj.Interface.API.Virtual;
using UnityEngine.Domain;
using Choi.MyProj.Domain.Virtual;

namespace Choi.MyProj.UI.Virtual
{
    /// <summary>
    /// 凝視によってボタンの役割を担当する Object をコントロールするスクリプト
    /// </summary>
	public sealed class VirtualGazeControl : MonoBehaviour
	{
		/// <summary>
		/// 現在見ている Object
		/// </summary>
		private GameObject m_currentLookAtObject;

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
			GazeProcess2D();
			GazeProcess3D();
		}

		/// <summary>
		/// Gaze Process
		/// </summary>
		private void GazeProcess2D()
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

		private void GazeProcess3D()
		{
			RaycastHit hit;
#if UNITY_EDITOR
			var virtualState = new Repository.Editor.VirtualStateInEditorRepository();
			if (virtualState.Get() != VirtualState.Virtual || VirtualControlAPI.Instance.NowCameraState != VirtualState.Virtual) return;
#else
			if (VirtualControlAPI.Instance.NowCameraState != VirtualState.Virtual) return;
#endif
			var ray = Manager.Instance.NowCamera.ScreenPointToRay((Vector2)Manager.Instance.NowCamera.transform.forward);
			if (Application.isEditor) Debug.DrawLine(ray.origin, ray.direction * 100f, Color.red);

			var hisHitit = Physics.Raycast(ray.origin, Manager.Instance.NowCamera.transform.forward, out hit, Mathf.Infinity, 1);

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
				m_currentLookAtButton = m_currentLookAtObject.GetComponent<ButtonForObject>();
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