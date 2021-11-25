using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public abstract class MapEventControlBase : MonoBehaviour
    {
        [SerializeField]
        protected LineDataView[] m_lineData = default;
        [SerializeField]
        protected RectTransform m_selectMark = default;
        [SerializeField]
        protected Vector2 m_hidePos = new Vector2(0, 2000);
        protected int m_selectNum = 0;
        protected RectTransform m_rect = default;
        protected Vector2 m_startPos = default;
        protected bool m_select = false;

        public event Action OnEventEnd = default;

        private void Start()
        {
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            foreach (var line in m_lineData)
            {
                line.StartSet();
            }
            m_lineData[0].SetLine(SlotData.LeftSlotData);
            m_lineData[1].SetLine(SlotData.CenterSlotData);
            m_lineData[2].SetLine(SlotData.RightSlotData);
            Select();
            m_rect.position = m_hidePos;
        }
        public virtual void SelectEvent()
        {
            m_select = true;
            m_rect.position = m_startPos;
        }
        public virtual void OutSelectEvent()
        {
            m_select = false;
            m_rect.position = m_hidePos;
            OnEventEnd?.Invoke();
        }
        public virtual void MoveLine(int dir)
        {
            if (!m_select)
            {
                return;
            }
            m_selectNum += dir;
            if (m_selectNum >= m_lineData.Length)
            {
                m_selectNum -= m_lineData.Length;
            }
            if (m_selectNum < 0)
            {
                m_selectNum += m_lineData.Length;
            }
            Select();
        }
        public virtual void MoveSlot(int dir)
        {
            if (!m_select)
            {
                return;
            }
            m_lineData[m_selectNum].Move(dir);
        }

        protected virtual void Select()
        {
            foreach (var line in m_lineData)
            {
                line.Hide();
            }
            m_selectMark.position = m_lineData[m_selectNum].Center.position;
            m_lineData[m_selectNum].Select();
        }

        public virtual LineDataView Target()
        {
            return m_lineData[m_selectNum];
        }

        public abstract void SelectAction();
    }
}
