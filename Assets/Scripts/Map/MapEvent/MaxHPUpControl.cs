using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class MaxHPUpControl : MapEventControlBase
    {
        [Header("最大値増加量")]
        [SerializeField]
        int _upHP = 10;
        public override void StartSet()
        {
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            m_rect.position = m_hidePos;
        }
        public override void SelectAction()
        {
            PlayerData.AddMaxHP(_upHP);
            OutSelectEvent();
        }
        public override void MoveLine(int dir)
        {
            return;
        }
        public override void MoveSlot(int dir)
        {
            return;
        }
    }
}