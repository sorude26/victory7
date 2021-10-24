using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class MapData
    {
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
    }
}