using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class MaxHPUpControl : MapEventControlBase
    {
        [Header("最大値増加量")]
        [SerializeField]
        int m_upHP = 10;
        [SerializeField]
        Text m_message = default;
        public override void StartSet()
        {
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            m_rect.position = m_hidePos;            
        }
        public override void SelectEvent()
        {
            if (m_message)
            {
                m_message.text = PlayerData.MaxHP + " => " + (PlayerData.MaxHP + m_upHP);
            }
            base.SelectEvent();
        }
        public override void SelectAction()
        {
            if (!m_select)
            {
                return;
            }
            PlayerData.AddMaxHP(m_upHP);
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