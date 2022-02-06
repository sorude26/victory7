using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SkillPanel : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_guideBase = default;
        [SerializeField]
        SkillGuide m_skillGuide = default;
        [SerializeField]
        SkillDataLibrary m_dataLibrary = default;
        SkillGuide[] m_skillPos = default;
        public SkillGuide[] SkillPos { get => m_skillPos; }

        int m_current = 0;
        public PlayerSkill CurrentSkill { get => m_skillPos[m_current].SkillType; }
        public void SetData(PlayerSkill[] skill)
        {
            m_skillPos = new SkillGuide[skill.Length];
            for (int i = 0; i < skill.Length; i++)
            {
                var guide = Instantiate(m_skillGuide, m_guideBase);
                guide.SetData(m_dataLibrary.GetData(skill[i]));
                m_skillPos[i] = guide;
            }
        }
        public void SelectGuide()
        {
            foreach (var skill in m_skillPos)
            {
                skill.CloseGuide();
            }
            m_skillPos[m_current].OpenGuide();
        }
        public void Next()
        {
            m_current++;
            if (m_current >= m_skillPos.Length)
            {
                m_current = 0;
            }
            SelectGuide();
        }
        public void Back()
        {
            m_current--;
            if (m_current < 0)
            {
                m_current = m_skillPos.Length - 1;
            }
            SelectGuide();
        }
        public void Left()
        {

        }
        public void Right()
        {

        }
        public void Up()
        {

        }
        public void Down()
        {

        }
    }
}