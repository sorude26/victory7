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
        int[] m_power = new int[3];
        [SerializeField]
        int[] m_guard = new int[3];
        [SerializeField]
        int[] m_charge = new int[3];
        [SerializeField]
        int[] m_heel = new int[3];
        [SerializeField]
        Slot[] m_startSlotL = new Slot[4];
        [SerializeField]
        Slot[] m_startSlotC = new Slot[4];
        [SerializeField]
        Slot[] m_startSlotR = new Slot[4];
        [SerializeField]
        SkillType[] m_haveSkill = default;
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
    }
}