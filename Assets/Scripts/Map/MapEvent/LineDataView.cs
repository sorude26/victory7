using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 {
    public class LineDataView : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_center = default;
        [SerializeField]
        RectTransform m_base = default;
        [SerializeField]
        HaveNumberView[] m_haveGuidView = default;
        [SerializeField]
        SlotView m_slot = default;
        [SerializeField]
        Vector2 m_hidePos = new Vector2(2500, 1500);

        List<Slot> m_lineSlot = default;
        int m_selectNum = 0;
        RectTransform m_rect = default;
        Vector2 m_startPos =default;
        public RectTransform Center { get => m_center; }
        public int SlotNum { get => m_lineSlot.Count; }
        public int SelectNum
        { 
            get 
            {
                int num = m_selectNum + 1;
                if (num >= m_lineSlot.Count)
                {
                    num -= m_lineSlot.Count;
                }
                return num; 
            } 
        }
        public void StartSet()
        {
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            m_lineSlot = new List<Slot>();
            m_slot.StartSet();
        }

        public void SetLine(List<Slot> slots)
        {
            if(m_lineSlot.Count > 0)
            {
                foreach (var item in m_lineSlot)
                {
                    item.DestroySlot();
                }
                m_lineSlot.Clear();
            }
            foreach (var item in slots)
            {
                var slot = Instantiate(item, m_base);
                slot.SlotRect.localScale = Vector2.one;
                m_lineSlot.Add(slot);
            }
            foreach (var guid in m_haveGuidView)
            {
                HaveSlot(guid);
            }
            Move(0);
        }
        public void Move(int dir)
        {
            foreach (var slot in m_lineSlot)
            {
                slot.OutSelect();
            }
            m_selectNum += dir;
            if (m_selectNum >= m_lineSlot.Count)
            {
                m_selectNum -= m_lineSlot.Count;
            }
            if (m_selectNum < 0)
            {
                m_selectNum += m_lineSlot.Count;
            }
            int[] num = new int[3];
            for (int i = 0; i < 3; i++)
            {
                num[i] = i + m_selectNum;
                if (num[i] >= m_lineSlot.Count)
                {
                    num[i] -= m_lineSlot.Count;
                }
                m_lineSlot[num[i]].Select();
            }
            m_slot.SetLine(m_lineSlot[num[0]].ID, m_lineSlot[num[1]].ID, m_lineSlot[num[2]].ID);
        }

        public void Select()
        {
            m_rect.position = m_startPos;
        }
        public void Hide()
        {
            m_rect.position = m_hidePos;
        }
        void HaveSlot(HaveNumberView view)
        {
            view.SetNumber(CountSlot(view.Type, 0), CountSlot(view.Type, 1), CountSlot(view.Type, 2));
        }
        int CountSlot(SkillType type,int effect)
        {
            return m_lineSlot.Count(s => s.Type == type && s.EffectID == effect);
        }
    } 
}
