using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum SlotType
    {
        Seven,
        Attack,
        Guard,
        Heel,
        Charge,
    }
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        SlotType m_type = default;
        [SerializeField]
        int m_testDebagEffect = default;
        [SerializeField]
        RectTransform m_rect = default;
        public SlotType Type { get => m_type; }
        public int TestDebagEffect { get => m_testDebagEffect; }
        public RectTransform SlotRect { get => m_rect; }
    }
}