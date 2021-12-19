using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class EnemyParameter : CharacterParameter
    {
        //[Header("リアルタイムバトルゲージ上昇時間")]
        //[SerializeField]
        protected float m_rtbGaugeTime = default;
        //[Header("基本攻撃力")]
        //[SerializeField]
        protected int m_attackPower = default;
        [Header("攻撃データ：x = 攻撃力、y = 攻撃までのカウント数")]
        [SerializeField]
        protected Vector2Int[] m_attackData = default;
        [Header("割合ダメージ軽減率")]
        [Range(0, 99)]
        [SerializeField]
        protected int m_reduction = 0;
        public float RtbGaugeTime { get => m_rtbGaugeTime; }
        public int AttackPower { get => m_attackPower; }
        public Vector2Int[] AttackData { get => m_attackData; }
        public int Reduction { get => m_reduction; }
    }
}