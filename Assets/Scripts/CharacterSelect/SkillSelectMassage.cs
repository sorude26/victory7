using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SkillSelectMassage : MonoBehaviour
    {
        [SerializeField]
        SkillGuide _skill = default;
        bool m_cancel = default;
        [SerializeField]
        GameObject[] m_arrows = default;
        public SkillGuide Skill { get => _skill; }
        public void OpenMassage()
        {
            gameObject.SetActive(true);
            m_cancel = true;
            m_arrows[0].SetActive(false);
            m_arrows[1].SetActive(true);
        }
        public void CloseMassage()
        {
            gameObject.SetActive(false);
        }
        public void ChangeTarget()
        {
            if (m_cancel)
            {
                m_cancel = false;
                m_arrows[1].SetActive(false);
                m_arrows[0].SetActive(true);
            }
            else
            {
                m_cancel = true;
                m_arrows[0].SetActive(false);
                m_arrows[1].SetActive(true);
            }
        }
        public bool Decision()
        {
            if (m_cancel)
            {
                CloseMassage();
            }
            return m_cancel;
        }
    }
}