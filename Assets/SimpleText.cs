using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleText : MonoBehaviour
{
    [SerializeField] private Text m_text;

    public void Set(System.Object text)
    {
        m_text.text = text.ToString();
    }
}
