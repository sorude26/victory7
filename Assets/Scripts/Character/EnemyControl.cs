using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class EnemyControl : CharacterControl
    {
        [SerializeField]
        protected EnemyParameter m_parameter = default;
        protected float m_rbtGauge;
        public void StartSet()
        {
            m_maxHP = m_parameter.MaxHP;
            CurrentHP = m_parameter.MaxHP;
        }
        public override void CharacterUpdate()
        {
            
        }
    } 
}
