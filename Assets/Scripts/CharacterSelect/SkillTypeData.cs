using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7 
{
    [CreateAssetMenu]
    public class SkillTypeData : ScriptableObject
    {
        [SerializeField]
        PlayerSkill m_skillType = default;
        [SerializeField]
        string m_skillName = default;
        [SerializeField]
        string m_skillGuide = default;
        public PlayerSkill SkillType { get => m_skillType; }
        public string SkillName { get => m_skillName; }
        public string SkillGuide { get => m_skillGuide; }
    }
}