using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_line = default;
        Slot[] m_allSlot = default;
        List<Slot> m_slot = default;
        public void StartSet()
        {
            m_allSlot = GameManager.Instance.AllSlot;
            m_slot = new List<Slot>();
        }
        public void SetLine(int top, int middle, int bottom)
        {
            if (m_slot.Count > 0)
            {
                foreach (var item in m_slot)
                {
                    item.DestroySlot();
                }
                m_slot.Clear();
            }
            var slot = Instantiate(m_allSlot[top], m_line[0]);
            slot.transform.position = m_line[0].position;
            m_slot.Add(slot);
            slot = Instantiate(m_allSlot[middle], m_line[1]);
            slot.transform.position = m_line[1].position;
            m_slot.Add(slot);
            slot = Instantiate(m_allSlot[bottom], m_line[2]);
            slot.transform.position = m_line[2].position;
            m_slot.Add(slot);
        }        
    }
}