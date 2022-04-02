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
        GameObject[] m_panel;

        List<Slot> m_leftData;
        List<Slot> m_centerData;
        List<Slot> m_rightData;

        void Start()
        {
            m_leftData = SlotData.LeftSlotData;
            m_centerData = SlotData.CenterSlotData;
            m_rightData = SlotData.RightSlotData;
            m_panel[0].SetActive(false);
            m_panel[1].SetActive(false);
            m_panel[2].SetActive(false);
            SetResultSlot();
            m_panel[0].SetActive(true);
        }

        void Update()
        {
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
                SoundManager.Play(SEType.Choice);
            }
            if(Input.GetButtonDown("Vertical"))
            {
                m_panel[1].SetActive(true);
                m_panel[0].SetActive(false);
                m_panel[2].SetActive(false);
                SoundManager.Play(SEType.Choice);
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
