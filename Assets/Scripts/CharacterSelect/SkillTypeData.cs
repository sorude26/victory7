using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 
{
    [CreateAssetMenu]
    public class SkillTypeData : ScriptableObject
    {
        [Header("スキル種類")]
        [SerializeField]
        PlayerSkill m_skillType = default;
        [Header("スキル名称")]
        [SerializeField]
        string m_skillName = default;
        [Header("スキル解説テキスト")]
        [SerializeField]
        string m_skillGuide = default;
        [Header("消費スキルポイント")]
        [SerializeField]
        protected int m_needSp = 50;
        [Header("スキル持続回数(持続タイプのみ反映)")]
        [SerializeField]
        protected int m_maxCount = 3;
        [Header("スキル効果量(効果量があるもののみ反映)")]
        [SerializeField]
        protected float m_effect = 0.5f;
        [Header("スキルダメージ割合量(ダメージタイプのみ反映)")]
        [SerializeField]
        protected int m_damage = 30;
        public PlayerSkill SkillType { get => m_skillType; }
        public string SkillName { get => m_skillName; }
        public string SkillGuide { get => m_skillGuide; }
        public int NeedSp { get => m_needSp; }
        public int MaxCount { get => m_maxCount; }
        public float Effect { get => m_effect; }
        public int Damage { get => m_damage; }
    }
}