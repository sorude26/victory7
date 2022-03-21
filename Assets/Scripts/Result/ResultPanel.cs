using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using victory7;

namespace victory7
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_base = default;

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
            Placement(m_leftData, m_base[0]);
            Placement(m_centerData, m_base[1]);
            Placement(m_rightData, m_base[2]);
        }

        void Placement(List<Slot> list, RectTransform rect)
        {
            list.ForEach(x => Instantiate(x,rect).transform.position = rect.position);
        }
    }
}
