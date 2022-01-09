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
        [SerializeField]
        protected Text m_count = default;
        public bool IsDead { get; protected set; }
        protected int[] m_attackCounts = default;
        public void StartSet()
        {
            if (!m_parameter)
            {
                Debug.Log("パラメータがセットされていません");
                return;
            }
            gameObject.SetActive(true);
            m_maxHP = m_parameter.MaxHP;
            CurrentHP = m_parameter.MaxHP;
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
            m_attackCounts = new int[m_parameter.AttackData.Length];
            for (int i = 0; i < m_attackCounts.Length; i++)
            {
                m_attackCounts[i] = m_parameter.AttackData[i].y;
            }
            if (m_count)
            {
                m_count.text = "";
                foreach (var attackCount in m_attackCounts)
                {
                    m_count.text += attackCount + ",";
                }
            }
        }
        public override void CharacterUpdate()
        {
            if (IsDead)
            {
                return;
            }
            for (int i = 0; i < m_attackCounts.Length; i++)
            {
                m_attackCounts[i]--;
                if (m_attackCounts[i] <= 0)
                {
                    m_attackCounts[i] = m_parameter.AttackData[i].y;
                    int a = m_parameter.AttackData[i].x;
                    BattleManager.Instance.BattleActions.Push(() =>
                    {
                        BattleManager.Instance.AttackPlayer(a);
                        if (m_animator)
                        {
                            m_animator.Play("attack");
                        }
                    });
                }
            }
            if (m_count)
            {
                m_count.text = "";
                foreach (var attackCount in m_attackCounts)
                {
                    m_count.text += attackCount + ",";
                }
            }
        }
        protected override void Dead()
        {
            if (m_animator)
            {
                Debug.Log("Dead");
                m_animator.Play("dead");
            }
            IsDead = true;
            BattleManager.Instance.CheckBattle();
        }
        public override void PercentageDamage(int percentage)
        {
            Damage(percentage * m_maxHP / 100);
        }
        public override bool AvoidanceCheck()
        {
            int r = Random.Range(0, 100);
            if (m_parameter.Avoidance > r)
            {
                return true;
            }
            return false;
        }
        void GameOut()
        {
            EffectManager.Instance.PlayEffect(EffectType.Damage3, transform.position);
            gameObject.SetActive(false);
        }
    } 
}
