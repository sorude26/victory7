using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class CharacterParameter : ScriptableObject
    {
        [Header("キャラクター名")]
        [SerializeField]
        protected string m_name = default;
        [Header("最大耐久値")]
        [SerializeField]
        protected int m_maxHP = default;
        public string Name { get => m_name; }
        public int MaxHP { get => m_maxHP; }
    }
}