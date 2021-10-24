using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum PointType
    {
        Enemy,
        Heel,
    }
    public class MapPoint : MonoBehaviour
    {
        [SerializeField]
        PointType m_type = PointType.Enemy;
        [SerializeField]
        LineRenderer m_renderer = default;
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