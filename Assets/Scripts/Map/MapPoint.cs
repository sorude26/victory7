using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum MapPointType
    {
        Enemy,
        Remove,
        Heel,
    }
    public class MapPoint : MonoBehaviour
    {
        [SerializeField]
        MapPointType m_type = MapPointType.Enemy;
        [SerializeField]
        int m_typeNumber = 0;
        [SerializeField]
        LineRenderer m_renderer = default;
        public MapPointType Type { get => m_type; }
        public int TypeNumber { get => m_typeNumber; }
        public int LineNumber { get; private set; }
        public int PosNumber { get; private set; }
        public int UpTarget { get; private set; }
        public int DownTarget { get; private set; }
        int m_lineCount = 0;
        public void SetData(int line,int pos)
        {
            LineNumber = line;
            PosNumber = pos;
        }
        public void SetUpTarget(int target)
        {
            if (target > 0)
            {
                UpTarget = target;
            }
        }
        public void SetDownTarget(int target)
        {
            if (target > 0)
            {
                DownTarget = target;
            }
        }
        public void DrawLine(Vector2 start, Vector2 end)
        {
            var renderer = Instantiate(m_renderer);
            renderer.transform.SetParent(transform);
            renderer.SetPosition(m_lineCount, start);
            renderer.SetPosition(m_lineCount + 1, end);
        }
    }
}