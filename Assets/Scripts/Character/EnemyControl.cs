using System;
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
        protected int[] m_attackCounts = default;
        public bool IsDead { get; protected set; }
        private Stack<Action> m_actionStack = default;
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
            m_actionStack = new Stack<Action>(); ;
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
                        m_actionStack.Push(() => BattleManager.Instance.AttackPlayer(a));
                        BattleManager.Instance.SetActionTime(this);
                        if (m_animator)
                        {
                            m_animator.Play("attack");
                            SEType audio = default;
                            switch (m_parameter.Name)
                            {
                                case "通常 1-1"://ゴブリン
                                    if(a == 15)
                                    {
                                        audio = SEType.StrongAttackGoblin;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackGoblin;
                                    }
                                    break;
                                case "通常 2-1"://ゴブリン
                                    if (a == 15)
                                    {
                                        audio = SEType.StrongAttackGoblin;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackGoblin;
                                    }
                                    break;
                                case "通常 3-1"://ゴブリン
                                    if (a == 15)
                                    {
                                        audio = SEType.StrongAttackGoblin;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackGoblin;
                                    }
                                    break;
                                case "通常 4-1"://ゴブリン
                                    if (a == 10)
                                    {
                                        audio = SEType.StrongAttackGoblin;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackGoblin;
                                    }
                                    break;
                                case "通常 5-1"://ゴブリン
                                    if (a == 10)
                                    {
                                        audio = SEType.StrongAttackGoblin;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackGoblin;
                                    }
                                    break;
                                case "通常 1-2"://ゴーレム
                                    audio = SEType.WeakAttackGolem;
                                    break;
                                case "通常 2-2"://ゴーレム
                                    audio = SEType.WeakAttackGolem;
                                    break;
                                case "通常 3-2"://ゴーレム
                                    audio = SEType.WeakAttackGolem;
                                    break;
                                case "通常 4-2"://ゴーレム
                                    audio = SEType.WeakAttackGolem;
                                    break;
                                case "通常 5-2"://ゴーレム
                                    audio = SEType.WeakAttackGolem;
                                    break;
                                case "通常　1-3"://スライム
                                    if (a == 5)
                                    {
                                        audio = SEType.StrongAttackSlime;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSlime;
                                    }
                                    break;
                                case "通常　2-3"://スライム
                                    if (a == 5)
                                    {
                                        audio = SEType.StrongAttackSlime;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSlime;
                                    }
                                    break;
                                case "通常　3-3"://スライム
                                    if (a == 5)
                                    {
                                        audio = SEType.StrongAttackSlime;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSlime;
                                    }
                                    break;
                                case "通常　4-3"://スライム
                                    if (a == 7)
                                    {
                                        audio = SEType.StrongAttackSlime;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSlime;
                                    }
                                    break;
                                case "通常　5-3"://スライム
                                    if (a == 7)
                                    {
                                        audio = SEType.StrongAttackSlime;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSlime;
                                    }
                                    break;
                                case "Seven chan"://イレギュラーバトル
                                    if(a == 77)
                                    {
                                        audio = SEType.StrongAttackSpecial;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackSpecial;
                                    }
                                    break;
                                case "Boss 1"://ドラゴン
                                    if(a == 20)
                                    {
                                        audio = SEType.StrongAttackDragon;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackDragon;
                                    }
                                    break;
                                case "Boss 2"://ドラゴン
                                    if (a == 30)
                                    {
                                        audio = SEType.StrongAttackDragon;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackDragon;
                                    }
                                    break;
                                case "Boss 3"://ドラゴン
                                    if (a == 50)
                                    {
                                        audio = SEType.StrongAttackDragon;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackDragon;
                                    }
                                    break;
                                case "Boss 4"://ドラゴン
                                    if (a == 50)
                                    {
                                        audio = SEType.StrongAttackDragon;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackDragon;
                                    }
                                    break;
                                case "Boss 5"://ドラゴン
                                    if (a == 77)
                                    {
                                        audio = SEType.StrongAttackDragon;
                                    }
                                    else
                                    {
                                        audio = SEType.WeakAttackDragon;
                                    }
                                    break;
                            }
                                    SoundManager.Play(audio);
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
        public override void Damage(int damage)
        {
            if (m_animator)
            {
                m_animator.Play("damage");
            }
            base.Damage(damage);
        }
        protected override void Dead()
        {
            if (m_animator)
            {
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
            int r = UnityEngine.Random.Range(0, 100);
            if (m_parameter.Avoidance > r)
            {
                return true;
            }
            return false;
        }
        public void AddAttackCount(int addCount = 1)
        {
            for (int i = 0; i < m_attackCounts.Length; i++)
            {
                m_attackCounts[i] += addCount;
            }
            if (m_count)
            {
                m_count.text = "";
                foreach (var attackCount in m_attackCounts)
                {
                    m_count.text += attackCount + ",";
                }
            }
            EffectManager.Instance.PlayEffect(EffectType.Damage2, transform.position);
        }
        void GameOut()
        {
            EffectManager.Instance.PlayEffect(EffectType.Damage3, transform.position);
            gameObject.SetActive(false);
        }
        void PlayAction()
        {
            m_actionStack.Pop()?.Invoke();
        }
    } 
}
