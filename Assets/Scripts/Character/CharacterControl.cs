using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public abstract class CharacterControl : MonoBehaviour
    {
        protected int m_maxHP = default;
        [SerializeField]
        protected Slider m_hpGauge = default;
        [SerializeField]
        protected Animator m_animation = default;
        public int CurrentHP { get; protected set; }
        public abstract void CharacterUpdate();
        public virtual void Damage(int damage)
        {
            CurrentHP -= damage;
            Debug.Log($"{gameObject.name}は{damage}のダメージ");
            var view = Instantiate(EffectManager.Instance.Text);
            view.transform.position = this.transform.position + Vector3.up;
            view.View("-" + damage.ToString(), Color.red);
            EffectManager.Instance.PlayEffect(EffectType.Damage1, transform.position);
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                Dead();
            }
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
        }
        protected abstract void Dead();
    }
}
