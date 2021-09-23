using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class EnemyParameter : CharacterParameter
    {
        [Header("リアルタイムバトルゲージ上昇時間")]
        [SerializeField]
        protected float m_rtbGaugeTime = default;
        [Header("基本攻撃力")]
        [SerializeField]
        protected int m_attackPower = default;
        public float RtbGaugeTime { get => m_rtbGaugeTime; }
        public int AttackPower { get => m_attackPower; }
    }
}