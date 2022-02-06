using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class SkillGuide : MonoBehaviour
    {
        [SerializeField]
        Text m_skillName = default;
        [SerializeField]
        GameObject m_guideBase = default;
        [SerializeField]
        Text m_guideText = default;
        public PlayerSkill SkillType { get; private set; }
        public void SetData(SkillTypeData data)
        {
            m_skillName.text = data.SkillName;
            m_guideText.text = data.SkillGuide;
            SkillType = data.SkillType;
            m_guideBase.SetActive(false);
        }
        public void OpenGuide()
        {
            m_guideBase.SetActive(true);
        }
        public void CloseGuide()
        {
            m_guideBase.SetActive(false);
        }
    }
}