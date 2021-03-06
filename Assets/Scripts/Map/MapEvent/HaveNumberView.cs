using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7 {
    public class HaveNumberView : MonoBehaviour
    {
        [SerializeField]
        Text[] m_haveNumber = new Text[3];
        [SerializeField]
        SkillType m_type = default;
        (int Level1, int Level2, int Level3) m_haveCount = default;
        public SkillType Type { get => m_type; }
        public void SetNumber(int level1, int level2, int level3)
        {
            m_haveNumber[0].text = "Level1 ×" + level1;
            m_haveNumber[1].text = "Level2 ×" + level2;
            m_haveNumber[2].text = "Level3 ×" + level3;
        }
        public void AddNumber(int level1, int level2, int level3)
        {
            m_haveCount.Level1 += level1;
            m_haveCount.Level2 += level2;
            m_haveCount.Level3 += level3;
            SetNumber(m_haveCount.Level1, m_haveCount.Level2, m_haveCount.Level3);
        }
    }
}