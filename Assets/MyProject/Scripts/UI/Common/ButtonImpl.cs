using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public interface IButtonImpl
    {
        UnityAction OnClick { get; }
        UnityAction OnRelease { get; }

        void OnButtonClick();

        void OnButtonRelease();
    }

    public class ButtonImpl : Button, IButtonImpl
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
            Debug.Log("OnClick()");
            if (OnClick == null) return;
            OnClick();
        }

        public void OnButtonRelease()
        {
            Debug.Log("OnRelease()");
            if (OnRelease == null) return;
            OnRelease();
        }
    }
}
