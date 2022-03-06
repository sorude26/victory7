using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class BattleData
    {
        public static bool Next { get; set; }
        public static EnemyControl[] Enemys { get; private set; }
        public static Slot[] PopSlot { get; private set; }
        public static void SetData(MapPointData data)
        {
            Enemys = data.Enemys;
            PopSlot = data.PopSlot;
        }
    }
}
