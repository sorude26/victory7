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
        protected Animator m_animator = default;
        [SerializeField]
        protected Transform m_centerPos = default;
        [SerializeField]
        protected Transform m_topPos = default;
        [SerializeField]
        protected Transform m_bottomPos = default;
        [SerializeField]
        protected float m_actionTime = 1f;
        [SerializeField]
        protected Vector3 m_targetPos = default;
        protected Vector3 m_startPos = default;
        protected Vector3 m_moveTarget = default;
        public int CurrentHP { get; protected set; }
        public float ActionTime { get => m_actionTime; }
        public Transform CenterPos
        {
            get
            {
                if (m_centerPos == null)
                {
                    m_centerPos = transform;
                }
                return m_centerPos;
            }
        }
        public Transform TopPos
        {
            get
            {
                if (m_topPos == null)
                {
                    m_topPos = transform;
                }
                return m_topPos;
            }
        }
        public Transform BottomPos
        {
            get
            {
                if (m_bottomPos == null)
                {
                    m_bottomPos = transform;
                }
                return m_bottomPos;
            }
        }
        public abstract void CharacterUpdate();
        public virtual void Damage(int damage)
        {
            CurrentHP -= damage;
            //Debug.Log($"{gameObject.name}は{damage}のダメージ");
            EffectManager.Instance.PlayEffect(EffectType.Damage1, CenterPos.position);
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                Dead();
            }
            m_hpGauge.value = CurrentHP / (float)m_maxHP;
        }
        public virtual void PercentageDamage(int percentage)
        {
            Damage(percentage * m_maxHP / 100);
        }
        public virtual void CheckPercentageDamage(int percentage)
        {
            if (AvoidanceCheck())
            {
                //Debug.Log("miss!");
                var view = Instantiate(EffectManager.Instance.Text);
                view.transform.position = TopPos.position + Vector3.up;
                view.View("miss!", Color.white);
                return;
            }
            PercentageDamage(percentage);
        }
        public virtual void AttackAction(CharacterControl target) { }
        public virtual bool AvoidanceCheck() { return false; }
        protected abstract void Dead();
        protected IEnumerator CharacterMove(Vector3 target, float moveSpeed = 1f)
        {
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
