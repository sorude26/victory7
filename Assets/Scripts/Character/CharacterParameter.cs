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
        [Header("即死攻撃の回避率")]
        [Range(0, 100)]
        [SerializeField]
        protected int m_avoidance = 70;
        public string Name { get => m_name; }
        public int MaxHP { get => m_maxHP; }
        public int Avoidance { get => m_avoidance; }
    }
}