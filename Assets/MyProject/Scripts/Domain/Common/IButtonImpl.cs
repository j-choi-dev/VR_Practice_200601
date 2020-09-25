using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Choi.MyProj.Domain.Common
{
    /// <summary>
    /// このプロジェクトで使う、ボタンの Component Interface
    /// </summary>
    public interface IButtonImpl
    {
        UnityAction OnClick { get; }
        UnityAction OnRelease { get; }

        void OnButtonClick();

        void OnButtonRelease();
    }
}
