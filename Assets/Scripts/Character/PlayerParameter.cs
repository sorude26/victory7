using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class PlayerParameter : CharacterParameter
    {
        [SerializeField]
        GameObject m_character = default;
        [SerializeField]
        int m_id = default;
        [SerializeField]
        int m_maxSp = 50;
        [SerializeField]
        int m_maxGp = 50;
        [SerializeField]
        int[] m_power = { 10, 20, 30 };
        [SerializeField]
        int[] m_guard = { 10, 20, 30 };
        [SerializeField]
        int[] m_charge = { 10, 20, 30 };
        [SerializeField]
        int[] m_heel = { 10, 20, 30 };
        [SerializeField]
        Slot[] m_startSlotL = new Slot[4];
        [SerializeField]
        Slot[] m_startSlotC = new Slot[4];
        [SerializeField]
        Slot[] m_startSlotR = new Slot[4];
        [SerializeField]
        PlayerSkill[] m_haveSkills = default;
        public GameObject Character { get => m_character; }
        public int ID { get => m_id; }
        public int MaxSp { get => m_maxSp; }
        public int MaxGp { get => m_maxGp; }
        public int[] Power { get => m_power; }
        public int[] Guard { get => m_guard; }
        public int[] Charge { get => m_charge; }
        public int[] Heel { get => m_heel; }
        public Slot[] StartSlotL { get => m_startSlotL; }
        public Slot[] StartSlotC { get => m_startSlotC; }
        public Slot[] StartSlotR { get => m_startSlotR; }
        public PlayerSkill[] HaveSkills { get => m_haveSkills; }
        public void SetMaxSp(int max)
        {
            m_maxSp = max;
        }
        public Slot[] GetStartSlot(int lineNumder)
        {
            if (lineNumder < 0 || lineNumder >= System.Enum.GetNames(typeof(LineType)).Length)
            {
                return null;
            }
            LineType line = (LineType)lineNumder;
            switch (line)
            {
                case LineType.Left:
                    return m_startSlotL;
                case LineType.Center:
                    return m_startSlotC;
                case LineType.Right:
                    return m_startSlotR;
                default:
                    return null;
            }
        }
    }
}