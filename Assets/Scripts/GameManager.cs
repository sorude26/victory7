using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField]
        PlayerParameter m_startParameter = default;
        [SerializeField]
        Slot[] m_slotStart = default;
        [SerializeField]
        Slot[] m_slotSeven = default;
        [SerializeField]
        Slot[] m_allSlot = default;
        [SerializeField]
        bool m_test = default;

        public Slot[] AllSlot { get => m_allSlot; }
        private void Awake()
        {
            if (Instance)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (m_test)
            {
                StartSet();
            }
        }
        public void StartSet()
        {
            SlotData.StartSet(m_slotStart, m_slotStart, m_slotStart, m_slotSeven);
            for (int i = 0; i < 3; i++)
            {
                SlotData.ShuffleSlot(i);
            }
            PlayerData.StartSet(m_startParameter);
        }
        public void StartSet(PlayerParameter parameter,SkillTypeData skill)
        {
            SlotData.StartSet(parameter.StartSlotL,parameter.StartSlotC,parameter.StartSlotR, m_slotSeven);
            for (int i = 0; i < 3; i++)
            {
                SlotData.ShuffleSlot(i);
            }
            parameter.SetMaxSp(skill.NeedSp);
            PlayerData.StartSet(parameter);
            PlayerData.SetSkill(skill);
        }
    }
}