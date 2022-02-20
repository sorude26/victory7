using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public enum PlayerSkill
    {
        PercentageAttack,
        InstantDeathAttack,
        Barrier,
        DelayEnemy,
        DelaySlot,
        Heel,
        Random,
    }
    public class PlayerControl : CharacterControl
    {
        [Header("SPゲージ")]
        [SerializeField]
        protected Slider m_spGauge = default;
        [Header("GPゲージ")]
        [SerializeField]
        protected Slider m_guardGauge = default;
        [Header("スキルカウントテキスト")]
        [SerializeField]
        protected Text m_count = default;

        protected PlayerParameter m_parameter = default;
        protected SkillTypeData m_skill = default;
        protected int m_needSp = default;
        protected int m_skillCount = default;
        protected int m_sp = default;
        protected int m_gp = default;

        public int CurrentSP { get => m_sp; }
        public int CurrentGP { get => m_gp; }

        public void StartSet()
        {
            m_parameter = PlayerData.DefaultParameter;
            m_maxHP = PlayerData.MaxHP;
            CurrentHP = PlayerData.CurrentHP;
            m_sp = PlayerData.CurrentSP;
            m_gp = PlayerData.CurrentGP;
            m_skill = PlayerData.CurrentSkill;

            ParameterUpdate();
        }
        public override void CharacterUpdate()
        {
            if (m_skill.SkillType == PlayerSkill.DelaySlot)
            {
                SkillCheck();
            }
            ParameterUpdate();
        }
        protected void ParameterUpdate()
        {
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
            m_spGauge.value = m_sp / (float)PlayerData.MaxSP;
            m_guardGauge.value = m_gp / (float)PlayerData.MaxGP;
        }
        public override void Damage(int damage)
        {
            if (m_skill.SkillType == PlayerSkill.Barrier && m_skillCount > 0)
            {
                SkillCheck();
                CharacterUpdate();
                EffectManager.Instance.PlayEffect(EffectType.Damage2, transform.position);
                return;
            }
            if (m_gp > 0)
            {
                m_gp -= damage;
                if (m_gp < 0)
                {
                    base.Damage(-m_gp);
                    m_gp = 0;
                }
                EffectManager.Instance.PlayEffect(EffectType.Damage2, transform.position);
                ParameterUpdate();
                return;
            }
            base.Damage(damage);
        }
        protected override void Dead()
        {
            EffectManager.Instance.PlayEffect(EffectType.Damage3, transform.position);
            gameObject.SetActive(false);
            BattleManager.Instance.CheckBattle();
        }
        public void UseSkill()
        {
            if (m_sp >= m_skill.NeedSp)
            {
                m_sp -= m_skill.NeedSp; 
                PlaySkill();
                ParameterUpdate();
            }
        }
        public int GetPower(int slotPower)
        {
            return m_parameter.Power[slotPower];
        }
        public void AddGuard(int slotPower)
        {
            m_gp += m_parameter.Guard[slotPower];
            if (m_gp > PlayerData.MaxGP)
            {
                m_gp = PlayerData.MaxGP;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Guard[slotPower].ToString(), Color.blue);
            EffectManager.Instance.PlayEffect(EffectType.Guard, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineGuard);
        }
        public void HeelPlayer(int heelPower)
        {
            CurrentHP += heelPower;
            if (CurrentHP > PlayerData.MaxHP)
            {
                CurrentHP = PlayerData.MaxHP;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + heelPower.ToString(), Color.green);
            EffectManager.Instance.PlayEffect(EffectType.Heel, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineHeel);
        }
        public void HeelSlot(int slotPower)
        {
            HeelPlayer(m_parameter.Heel[slotPower]);
        }
        public void Charge(int slotPower)
        {
            m_sp += m_parameter.Charge[slotPower];
            if (m_sp > PlayerData.MaxSP)
            {
                m_sp = PlayerData.MaxSP;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Charge[slotPower].ToString(), Color.yellow);
            EffectManager.Instance.PlayEffect(EffectType.Chage, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineCharge);
        }
        public void PlaySkill()
        {
            m_skillCount += m_skill.MaxCount;
            if (m_count)
            {
                m_count.text = "";
                if (m_skillCount > 0)
                {
                    m_count.text = m_skillCount.ToString();
                }
            }
            switch (m_skill.SkillType)
            {
                case PlayerSkill.PercentageAttack:
                    BattleManager.Instance.AttackEnemyPercentage(m_skill.Damage);
                    break;
                case PlayerSkill.InstantDeathAttack:
                    BattleManager.Instance.AttackEnemyCritical(m_skill.Damage);
                    break;
                case PlayerSkill.Barrier:
                    EffectManager.Instance.PlayEffect(EffectType.Guard, transform.position);
                    SoundManager.Play(SEType.PaylineCharge);
                    break;
                case PlayerSkill.DelayEnemy:
                    BattleManager.Instance.AddEnemyActionCount();
                    break;
                case PlayerSkill.DelaySlot:
                    GameManager.Instance.SlotSpeedChange(m_skill.Effect);
                    EffectManager.Instance.PlayEffect(EffectType.Guard, transform.position);
                    SoundManager.Play(SEType.PaylineCharge);
                    break;
                case PlayerSkill.Heel:
                    HeelPlayer((int)(PlayerData.MaxHP * m_skill.Effect));
                    break;
                case PlayerSkill.Random:
                    int r = Random.Range(0, 4);
                    if (r == 0)
                    {
                        BattleManager.Instance.AttackEnemy(2);
                    }
                    else if (r == 1)
                    {
                        BattleManager.Instance.Player?.AddGuard(2);
                    }
                    else if (r == 2)
                    {
                        BattleManager.Instance.Player?.HeelSlot(2);
                    }
                    else
                    {
                        BattleManager.Instance.AttackEnemyCritical(m_skill.Damage);
                    }
                    break;
                default:
                    break;
            }
        }
        public void SkillCheck()
        {
            if (m_skillCount > 0)
            {
                m_skillCount--;
                if (m_count)
                {
                    m_count.text = "";
                    if (m_skillCount > 0)
                    {
                        m_count.text = m_skillCount.ToString();
                    }
                    else
                    {
                        switch (m_skill.SkillType)
                        {
                            case PlayerSkill.DelaySlot:
                                GameManager.Instance.SlotSpeedChange();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}