using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class MapData
    {
        public static int ClearStageCount { get; private set; }
        public static int BattleCount { get; private set; }
        public static bool Create { get; private set; }
        public static Vector2Int PlayerPos { get; private set; }
        public static void SetData(Vector2Int playerPos)
        {
            Create = true;
            PlayerPos = playerPos;
        }
        public static void Reset()
        {
            Create = false;
            PlayerPos = Vector2Int.zero;
        }
        public static void ClearReset()
        {
            Reset();
            ClearStageCount = 0;
            BattleCount = 0;
        }
        public static void AddClearCount()
        {
            ClearStageCount++;
        }
        public static void AddBattleCount()
        {
            BattleCount++;
        }
    }
}