﻿using System.Collections;
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
    }
    public class PlayerControl : CharacterControl
    {
        [SerializeField]
        protected PlayerParameter m_parameter = default;
        [SerializeField]
        protected Slider m_spGauge = default;
        [SerializeField]
        protected Slider m_guardGauge = default;
        [Header("使用スキル")]
        [SerializeField]
        protected PlayerSkill m_skillType = default;
        [Header("消費スキルポイント")]
        [SerializeField]
        protected int m_needSp = 50;
        [Header("ガード持続回数")]
        [SerializeField]
        protected int m_maxGuardCount = 3;
        [SerializeField]
        protected Text m_count = default;
        protected int m_guardCount = default;
        protected int m_sp = default;
        protected int m_gp = default;
        public int CurrentSP { get => m_sp; }
        public int CurrentGP { get => m_gp; }

        public void StartSet()
        {
            if (!m_parameter)
            {
                Debug.Log("パラメータがセットされていません");
                return;
            }
            m_maxHP = PlayerData.MaxHP;
            CurrentHP = PlayerData.CurrentHP;
            m_sp = PlayerData.CurrentSP;
            m_gp = PlayerData.CurrentGP;
            ParameterUpdate();
        }
        public override void CharacterUpdate()
        {
            if (m_guardCount > 0)
            {
                m_guardCount--;
            }
            if (m_count)
            {
                m_count.text = "";
                if (m_guardCount > 0)
                {
                    m_count.text = m_guardCount.ToString();
                }
            }
            ParameterUpdate();
        }
        protected void ParameterUpdate()
        {
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
            m_spGauge.value = m_sp / (float)m_parameter.MaxSp;
            m_guardGauge.value = m_gp / (float)m_parameter.MaxGp;
        }
        public override void Damage(int damage)
        {
            if (m_guardCount > 0)
            {
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
            if (m_sp >= m_needSp)
            {
                Debug.Log("u");
                m_sp -= m_needSp;
                SkillController.UseSkill(m_skillType, 3);
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
            if (m_gp > m_parameter.MaxGp)
            {
                m_gp = m_parameter.MaxGp;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Guard[slotPower].ToString(), Color.blue);
            EffectManager.Instance.PlayEffect(EffectType.Guard, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineGuard);
        }
        public void HeelPlayer(int slotPower)
        {
            CurrentHP += m_parameter.Heel[slotPower];
            if (CurrentHP > PlayerData.MaxHP)
            {
                CurrentHP = PlayerData.MaxHP;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Heel[slotPower].ToString(), Color.green);
            EffectManager.Instance.PlayEffect(EffectType.Heel, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineHeel);
        }
        public void Charge(int slotPower)
        {
            m_sp += m_parameter.Charge[slotPower];
            if (m_sp > m_parameter.MaxSp)
            {
                m_sp = m_parameter.MaxSp;
            }

            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Charge[slotPower].ToString(), Color.yellow);
            EffectManager.Instance.PlayEffect(EffectType.Chage, transform.position);
            ParameterUpdate();
            SoundManager.Play(SEType.PaylineCharge);
        }
        public void Barrier()
        {
            m_guardCount = m_maxGuardCount;
            if (m_count)
            {
                m_count.text = "";
                if (m_guardCount > 0)
                {
                    m_count.text = m_guardCount.ToString();
                }
            }
            EffectManager.Instance.PlayEffect(EffectType.Guard, transform.position);
            SoundManager.Play(SEType.PaylineCharge);
        }
    }
}