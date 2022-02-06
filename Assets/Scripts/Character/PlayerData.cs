using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class PlayerData
    {
        protected static int m_maxHP = default;
        protected static int m_additionHP = default;
        public static int MaxHP { get => m_maxHP + m_additionHP; }
        public static int MaxSP { get; protected set; }
        public static int MaxGP { get; protected set; }
        public static int CurrentHP { get; protected set; }
        public static int CurrentSP { get; protected set; }
        public static int CurrentGP { get; protected set; }
        public static PlayerSkill SkillType { get; protected set; }
        public static int ID { get; protected set; }
        public static void StartSet(PlayerParameter parameter)
        {
            m_maxHP = parameter.MaxHP;
            MaxSP = parameter.MaxSp;
            MaxGP = parameter.MaxGp;
            CurrentHP = parameter.MaxHP;
            m_additionHP = 0;
            CurrentSP = 0;
            CurrentGP = 0;
            ID = parameter.ID;
        }
        public static void SetSkill(PlayerSkill skill)
        {
            SkillType = skill;
        }
        public static void SetData(int hp,int sp)
        {
            CurrentHP = hp;
            CurrentSP = sp;
            CurrentGP = 0;
        }
        public static void AddMaxHP(int addHP)
        {
            m_additionHP += addHP;
        }
        public static void HeelHP(int heel)
        {
            CurrentHP += heel;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }
    }
}
