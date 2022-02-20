using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class SkillDataLibrary : ScriptableObject
    {
        [SerializeField]
        SkillTypeData[] skillTypeDatas = default;
        public SkillTypeData GetData(PlayerSkill skill)
        {
            return skillTypeDatas.Where(s => s.SkillType == skill).FirstOrDefault();
        }
        public SkillTypeData GetData(string skillName)
        {
            return skillTypeDatas.Where(s => s.SkillName == skillName).FirstOrDefault();
        }
    }
}