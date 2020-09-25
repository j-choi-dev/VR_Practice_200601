using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Choi.MyProj.Domain.Common;

namespace Choi.MyProj.UI
{
    public sealed class ButtonForCanvas : Button, IButtonImpl
    {
        [SerializeField] string m_text;
        public UnityAction OnClick { get; private set; }
        public UnityAction OnRelease { get; private set; }

        public void Init(UnityAction actionClick, UnityAction actionExit)
        {
            Debug.Log($"{actionClick}, {actionExit}");
            OnClick = onClick != null ? onClick.Invoke :  actionClick;
            OnRelease = actionExit;
        }

        public void OnButtonClick()
        {
            if (OnClick == null) return;
            Debug.Log($"OnClick Run");
            OnClick();
        }

        public void OnButtonRelease()
        {
            if (OnRelease == null) return;
            OnRelease();
        }
    }
}
