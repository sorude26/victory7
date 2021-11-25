using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 
{
    public class RemoveSlotControl : MapEventControlBase
    {
        public override void SelectAction()
        {
            if (!m_select)
            {
                return;
            }
            if (Remove())
            {
                OutSelectEvent();
            }
        }
        public bool Remove()
        {
            if (m_lineData[m_selectNum].SlotNum < 5)
            {
                return false;
            }
            SlotData.RemoveSlot(m_lineData[m_selectNum].SelectNum, m_selectNum);
            m_lineData[0].SetLine(SlotData.LeftSlotData);
            m_lineData[1].SetLine(SlotData.CenterSlotData);
            m_lineData[2].SetLine(SlotData.RightSlotData);
            Select();
            return true;
        }        
    }
}