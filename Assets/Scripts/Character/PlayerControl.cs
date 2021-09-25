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
        protected int m_sp = default;
        protected int m_gp = default;

        public void StartSet()
        {
            if (!m_parameter)
            {
                Debug.Log("パラメータがセットされていません");
                return;
            }
            m_maxHP = m_parameter.MaxHP;
            CurrentHP = m_parameter.MaxHP;
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
                return;
            }
            base.Damage(damage);
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
            CharacterUpdate();
        }
        public void HeelPlayer(int slotPower)
        {
            CurrentHP += m_parameter.Heel[slotPower];
            if (CurrentHP > m_parameter.MaxHP)
            {
                CurrentHP = m_parameter.MaxHP;
            }
            CharacterUpdate();
        }
        public void Charge(int slotPower)
        {
            m_sp += m_parameter.Charge[slotPower];
            if (m_sp > m_parameter.MaxSp)
            {
                m_sp = m_parameter.MaxSp;
            }
            CharacterUpdate();
        }
    }
}