using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public abstract class CharacterControl : MonoBehaviour
    {
        protected int m_maxHP = default;
        public int CurrentHP { get; protected set; }
        public abstract void CharacterUpdate();
        public virtual void Damage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
            }
        }
    }
}
