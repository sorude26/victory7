using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class MapData
    {
        public static MapPatternData CurrentMap { get; private set; }
        public static int ClearStageCount { get; private set; }
        public static int BattleCount { get; private set; }
        public static bool Create { get; private set; }
        public static Vector2Int PlayerPos { get; private set; }
        public static void SetData(Vector2Int playerPos)
        {
            Create = true;
            PlayerPos = playerPos;
        }
        public static void PositionReset()
        {
            Create = false;
            PlayerPos = Vector2Int.zero;
        }
        public static void ClearReset()
        {
            PositionReset();
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
        public static void SetStartMap(MapPatternData data)
        {
            CurrentMap = data;
        }
        public static void SetNextMap()
        {
            CurrentMap = CurrentMap.GetNextMap();
        }
    }
}