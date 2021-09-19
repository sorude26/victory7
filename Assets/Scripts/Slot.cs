using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    int m_testDebagEffect = default;
    [SerializeField]
    RectTransform m_rect = default;
    public int TestDebagEffect { get => m_testDebagEffect; }
    public RectTransform SlotRect { get => m_rect; }
}
