using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class EnemyControl : CharacterControl
    {
        [SerializeField]
        protected EnemyParameter m_parameter = default;
        [SerializeField]
        protected Slider m_rtgGauge = default;
        protected float m_rbtTimer = default;
        public void StartSet()
        {
            if (!m_parameter)
            {
                Debug.Log("パラメータがセットされていません");
                return;
            }
            m_maxHP = m_parameter.MaxHP;
            CurrentHP = m_parameter.MaxHP;
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
        }
        public override void CharacterUpdate()
        {
            m_rbtTimer += Time.deltaTime;
            if (m_rbtTimer >= m_parameter.RtbGaugeTime)
            {
                m_rbtTimer = 0;
                Debug.Log("攻撃");
                BattleManager.Instance.AttackPlayer(m_parameter.AttackPower);
                if (m_animation)
                {
                    m_animation.Play("attack");
                }
            }
            m_rtgGauge.value = m_rbtTimer / m_parameter.RtbGaugeTime;
        }
    } 
}
