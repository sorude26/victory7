using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class LevelUpControl : MapEventControlBase
    {
        [SerializeField]
        EventDialog m_dialog = default;
        [SerializeField]
        GameObject m_ngDialog = default;
        [SerializeField]
        string m_message = "この絵柄をレベルアップしますか？";
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
                    if (LevelUp())
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
        public bool LevelUp()
        {
            bool levelUp = SlotData.LevelUpSlot(m_lineData[m_selectNum].SelectNum, m_selectNum);
            if (!levelUp)
            {
                return false;
            }
            SoundManager.Play(SEType.LevelUPSquares);
            m_lineData[0].SetLine(SlotData.LeftSlotData);
            m_lineData[1].SetLine(SlotData.CenterSlotData);
            m_lineData[2].SetLine(SlotData.RightSlotData);
            Select();
            return true;
        }
    }
}
