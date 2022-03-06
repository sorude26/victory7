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
            Placement(m_leftData);
            Placement(m_centerData);
            Placement(m_rightData);
        }

        void Placement(List<Slot> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list.ForEach(x => Instantiate(x,m_base[i]).transform.position = m_base[i].position);
            }
        }
    }
}
