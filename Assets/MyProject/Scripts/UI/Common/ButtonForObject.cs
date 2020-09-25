using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using Choi.MyProj.Domain.Common;

namespace Choi.MyProj.UI
{
    public sealed class ButtonForObject : MonoBehaviour, IButtonImpl
    {
        public UnityAction OnClick { get; private set; }
        public UnityAction OnRelease { get; private set; }

        public void Init(UnityAction actionClick, UnityAction actionExit)
        {
            OnClick = actionClick;
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
