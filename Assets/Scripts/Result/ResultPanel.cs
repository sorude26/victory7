using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using victory7;

namespace victory7
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_line = default;
        [SerializeField]
        float m_slotScale = 1.5f;

        List<Slot> m_leftData;
        List<Slot> m_centerData;
        List<Slot> m_rightData;

        bool m_jump = false;

        void Start()
        {
            m_leftData = SlotData.LeftSlotData;
            m_centerData = SlotData.CenterSlotData;
            m_rightData = SlotData.RightSlotData;
        }

        void Update()
        {
            if(Input.GetButtonDown("Jump") && !m_jump)
            {
                m_jump = true;
                Debug.Log("jump");
                SetResultSlot();
            }
        }
        void SetResultSlot()
        {
            foreach (var slot in m_leftData)
            {
                var data = Instantiate(slot, m_line[0]);
                data.transform.position = m_line[0].position;
            }
            foreach (var slot in m_centerData)
            {
                var data = Instantiate(slot, m_line[1]);
                data.transform.position = m_line[1].position;

            }
            foreach (var slot in m_rightData)
            {
                var data = Instantiate(slot, m_line[2]);
                data.transform.position = m_line[2].position;

            }
        }
    }
}
