using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class MapLine : ScriptableObject
    {
        [Header("この横列に存在する全てのマス")]
        [SerializeField]
        MapPoint[] m_points = default;
        [Header("設定したマスの数だけ用意する。この列からみて上ラインの何処に繋げるか。")]
        [SerializeField]
        int[] m_targetPointU = default;
        [Header("設定したマスの数だけ用意する。この列からみて下ラインの何処に繋げるか。")]
        [SerializeField]
        int[] m_targetPointD = default;
        public MapPoint[] Points { get => m_points; }
        public int[] TargetPointU { get => m_targetPointU; }
        public int[] TargetPointD { get => m_targetPointD; }
    }
}