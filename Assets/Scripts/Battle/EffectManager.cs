using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum EffectType
    {
        Heel,
        Chage,
        Guard,
        Damage1,
        Damage2,
        Damage3,
        Fever,
        Start,
        AttackPlayer,
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Attack5,
        Attack6,
        Attack7,
        Attack8,
        Attack9,
        Attack10,
        Attack11,
    }
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }
        [SerializeField]
        ViewText m_viewText = default;
        [SerializeField]
        GameObject[] m_effectPrefabs = default;
        public ViewText Text { get => m_viewText; }
        private void Awake()
        {
            Instance = this;
        }
        public void PlayEffect(EffectType type, Vector2 pos)
        {
            Instantiate(m_effectPrefabs[(int)type], transform).transform.position = pos;
        }
    }
}
