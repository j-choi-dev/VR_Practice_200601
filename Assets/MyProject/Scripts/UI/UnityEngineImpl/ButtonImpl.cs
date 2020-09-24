using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Domain;

namespace UnityEngine.UI
{
    public sealed class ButtonImpl : Button, IButtonImpl
    {
        [SerializeField] string m_text;
        public UnityAction OnClick { get; private set; }
        public UnityAction OnRelease { get; private set; }

        public void Init(UnityAction actionClick, UnityAction actionExit)
        {
            OnClick = onClick != null ? onClick.Invoke :  actionClick;
            OnRelease = actionExit;
        }

        public void OnButtonClick()
        {
            if (OnClick == null) return;
            OnClick();
        }

        public void OnButtonRelease()
        {
            if (OnRelease == null) return;
            OnRelease();
        }
    }
}
