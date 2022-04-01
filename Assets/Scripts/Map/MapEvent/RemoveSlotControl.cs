using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 
{
    public class RemoveSlotControl : MapEventControlBase
    {
        [SerializeField]
        EventDialog m_dialog = default;
        [SerializeField]
        GameObject m_ngDialog = default;
        [SerializeField]
        string m_message = "この絵柄を排除しますか？";
        public override void SelectAction()
        {
            if (!m_select)
            {
                return;
            }
            if (m_dialog.IsOpen)
            {
                if (m_dialog.SelectDecision())
                {
                    if (Remove())
                    {
                        SoundManager.Play(SEType.Decision);
                        OutSelectEvent();
                    }
                    else
                    {
                        SoundManager.Play(SEType.Cancel);
                        m_ngDialog.SetActive(true);
                    }
                }
                else
                {
                    SoundManager.Play(SEType.Cancel);
                }
            }
            else
            {
                m_dialog.SetMessage = m_message;
                m_dialog.SetImage(SlotData.GetSlot(m_lineData[m_selectNum].SelectNum, m_selectNum).SlotSprite);
                m_dialog.OpenDialog();
            }
        }
        public override void MoveLine(int dir)
        {
            if (m_dialog.IsOpen)
            {
                m_dialog.ChangeSelect(dir);
                return;
            }
            base.MoveLine(dir);
        }
        public bool Remove()
        {
            if (m_lineData[m_selectNum].SlotNum < 5)
            {
                return false;
            }
            SoundManager.Play(SEType.Decision);
            SlotData.RemoveSlot(m_lineData[m_selectNum].SelectNum, m_selectNum);
            m_lineData[0].SetLine(SlotData.LeftSlotData);
            m_lineData[1].SetLine(SlotData.CenterSlotData);
            m_lineData[2].SetLine(SlotData.RightSlotData);
            Select();
            return true;
        }        
    }
}