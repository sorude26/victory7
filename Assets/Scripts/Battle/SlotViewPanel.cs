using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SlotViewPanel : MonoBehaviour
    {
        [SerializeField]
        LineDataView[] m_lineData = default;
        [SerializeField]
        RectTransform m_selectMark = default;
        int m_selectNum = 0;
        RectTransform m_rect = default;
        Vector2 m_startPos = default;
        [SerializeField]
        Vector2 m_hidePos = default;
        public void StartSet()
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
        public void OpenPanel()
        {
            m_rect.position = m_startPos; 
            Select();
        }
        public void ClosePanel()
        {
            m_rect.position = m_hidePos;
        }
        public void Select(int number)
        {
            m_selectNum = number;
            Select();
        }
        private void Select()
        {
            foreach (var line in m_lineData)
            {
                line.Hide();
            }
            m_selectMark.position = m_lineData[m_selectNum].Center.position;
            m_lineData[m_selectNum].Select();
        }
    }
}