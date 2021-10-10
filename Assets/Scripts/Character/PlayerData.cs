using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class PlayerData
    {
        public static int CurrentHP { get; protected set; }
        public static int CurrentSP { get; protected set; }
        public static int CurrentGP { get; protected set; }
        public static void StartSet(PlayerParameter parameter)
        {
            CurrentHP = parameter.MaxHP;
            CurrentSP = 0;
            CurrentGP = 0;
        }
        public static void SetData(int hp,int sp,int gp)
        {
            CurrentHP = hp;
            CurrentSP = sp;
            CurrentGP = gp;
        }
    }
}
