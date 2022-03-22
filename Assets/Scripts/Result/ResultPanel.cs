using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_base = default;

        [SerializeField]
        int m_overCount;

        [SerializeField]
        GameObject[] m_panel;

        List<Slot> m_leftData;
        List<Slot> m_centerData;
        List<Slot> m_rightData;

        bool m_over = false;

        void Start()
        {
            m_leftData = SlotData.LeftSlotData;
            m_centerData = SlotData.CenterSlotData;
            m_rightData = SlotData.RightSlotData;
            m_panel[0].SetActive(false);
            m_panel[1].SetActive(false);
            m_panel[2].SetActive(false);
            SetResultSlot();
        }

        void Update()
        {
            if (!m_over) return;
            if(Input.GetButtonDown("Horizontal"))
            {
                var i = Input.GetAxisRaw("Horizontal");
                if (i < 0)
                {
                    m_panel[0].SetActive(true);
                    m_panel[2].SetActive(false);
                }
                else
                {
                    m_panel[2].SetActive(true);
                    m_panel[0].SetActive(false);
                }
                m_panel[1].SetActive(false);
            }
            if(Input.GetButtonDown("Vertical"))
            {
                m_panel[1].SetActive(true);
                m_panel[0].SetActive(false);
                m_panel[2].SetActive(false);
            }
        }
        void SetResultSlot()
        {
            if(m_leftData.Count > m_overCount || m_centerData.Count > m_overCount || m_rightData.Count > m_overCount)
            {
                m_over = true;
                m_panel[0].SetActive(true);
            }
            Placement(m_leftData, m_base[0], m_base[3]);
            Placement(m_centerData, m_base[1], m_base[4]);
            Placement(m_rightData, m_base[2], m_base[5]);
        }

        void Placement(List<Slot> list, RectTransform rect, RectTransform overRect)
        {
            if(!m_over)
            {
                list.ForEach(x => Instantiate(x, rect).transform.position = rect.position);//通常時
            }
            else
            {
                list.ForEach(x => Instantiate(x,overRect).transform.position = rect.position);//Over時

            }
        }
    }
}
