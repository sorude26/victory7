using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class StartSlotDataView : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] m_slotBases = default;
        [SerializeField]
        HaveNumberView[] m_haveViews = default;
        [SerializeField]
        GameObject[] m_uiObjects = default;
        List<Slot> m_lineSlot = default;
        public void StartSet(PlayerParameter player)
        {
            m_lineSlot = new List<Slot>();
            for (int i = 0; i < System.Enum.GetNames(typeof(LineType)).Length; i++)
            {
                foreach (var slot in player.GetStartSlot(i))
                {
                    m_lineSlot.Add(Instantiate(slot, m_slotBases[i]));
                }
                SetView();
            }
        }
        public void CloseUI()
        {
            foreach(var ui in m_uiObjects)
            {
                ui.SetActive(false);
            }
        }
        public void OpenUI()
        {
            foreach (var ui in m_uiObjects)
            {
                ui.SetActive(true);
            }
        }
        void SetView()
        {
            foreach (var view in m_haveViews)
            {
                HaveSlot(view);
            }
            m_lineSlot.Clear();
        }
        void HaveSlot(HaveNumberView view)
        {
            view.AddNumber(CountSlot(view.Type, 0), CountSlot(view.Type, 1), CountSlot(view.Type, 2));
        }
        int CountSlot(SkillType type, int effect)
        {
            return m_lineSlot.Count(s => s.Type == type && s.EffectID == effect);
        }
    }
}