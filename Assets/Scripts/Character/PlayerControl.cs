using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class PlayerControl : CharacterControl
    {
        [SerializeField]
        protected PlayerParameter m_parameter = default;
        [SerializeField]
        protected Slider m_spGauge = default;
        [SerializeField]
        protected Slider m_guardGauge = default;
        [SerializeField]
        protected SkillType m_skillType = default;
        protected int m_sp = default;
        public int CurrentSP { get => m_sp; }
        protected int m_gp = default;
        public int CurrentGP { get => m_gp; }

        public void StartSet()
        {
            if (!m_parameter)
            {
                Debug.Log("パラメータがセットされていません");
                return;
            }
            m_maxHP = m_parameter.MaxHP + PlayerData.AdditionHP;
            CurrentHP = PlayerData.CurrentHP;
            m_sp = PlayerData.CurrentSP;
            m_gp = PlayerData.CurrentGP;
            CharacterUpdate();
        }
        public override void CharacterUpdate()
        {
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
            m_spGauge.value = m_sp / (float)m_parameter.MaxSp;
            m_guardGauge.value = m_gp / (float)m_parameter.MaxGp;
        }
        public override void Damage(int damage)
        {
            if (m_gp > 0)
            {
                m_gp -= damage;
                if (m_gp < 0)
                {
                    base.Damage(-m_gp);
                    m_gp = 0;
                }
                EffectManager.Instance.PlayEffect(EffectType.Damage2, transform.position);
                CharacterUpdate();
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
            if (m_sp >= m_parameter.MaxSp)
            {
                m_sp = 0;
                SkillController.UseSkill(m_skillType, 3);
                CharacterUpdate();
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
            CharacterUpdate();
            SoundManager.Play(SEType.PaylineGuard);
        }
        public void HeelPlayer(int slotPower)
        {
            CurrentHP += m_parameter.Heel[slotPower];
            if (CurrentHP > m_parameter.MaxHP + PlayerData.AdditionHP)
            {
                CurrentHP = m_parameter.MaxHP + PlayerData.AdditionHP;
            }
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("+" + m_parameter.Heel[slotPower].ToString(), Color.green);
            EffectManager.Instance.PlayEffect(EffectType.Heel, transform.position);
            CharacterUpdate();
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
            CharacterUpdate();
            SoundManager.Play(SEType.PaylineCharge);
        }
    }
}