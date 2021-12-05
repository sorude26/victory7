using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class PlayerData
    {
        protected static int _maxHP = default;
        protected static int _additionHP = default;
        public static int MaxHP { get => _maxHP + _additionHP; }
        public static int CurrentHP { get; protected set; }
        public static int CurrentSP { get; protected set; }
        public static int CurrentGP { get; protected set; }
        public static void StartSet(PlayerParameter parameter)
        {
            _maxHP = parameter.MaxHP;
            CurrentHP = parameter.MaxHP;
            _additionHP = 0;
            CurrentSP = 0;
            CurrentGP = 0;
        }
        public static void SetData(int hp,int sp)
        {
            CurrentHP = hp;
            CurrentSP = sp;
            CurrentGP = 0;
        }
        public static void AddMaxHP(int addHP)
        {
            _additionHP += addHP;
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
