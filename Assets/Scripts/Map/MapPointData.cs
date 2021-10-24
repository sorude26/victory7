using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class MapPointData : ScriptableObject
    {
        [Header("出現する敵")]
        [SerializeField]
        EnemyControl[] m_enemys = default;
        [Header("ドロップするスロット")]
        [SerializeField]
        Slot[] m_popSlot = default;
        public EnemyControl[] Enemys { get => m_enemys; }
        public Slot[] PopSlot { get => m_popSlot; }
    }
}