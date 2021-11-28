using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class LevelUpControl : MapEventControlBase
    {
        public override void SelectAction()
        {
            if (!m_select)
            {
                return;
            }
            if (LevelUp())
            {
                OutSelectEvent();
            }
        }
        public bool LevelUp()
        {
            bool levelUp = SlotData.LevelUpSlot(m_lineData[m_selectNum].SelectNum, m_selectNum);
            if (!levelUp)
            {
                return false;
            }
            m_lineData[0].SetLine(SlotData.LeftSlotData);
            m_lineData[1].SetLine(SlotData.CenterSlotData);
            m_lineData[2].SetLine(SlotData.RightSlotData);
            Select();
            return true;
        }
    }
}
