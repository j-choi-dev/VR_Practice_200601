using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleImage : MonoBehaviour
{
    [SerializeField] private Image m_image;

    public void Set(Sprite sprite)
    {
        m_image.sprite = sprite;
    }
}