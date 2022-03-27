using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class MapPlayerDataPanel : MapEventControlBase
    {
        [SerializeField]
        Image m_skillGuide = default;
        public override void StartSet()
        {
            base.StartSet();
            m_skillGuide.sprite = PlayerData.CurrentSkill.SkillGuideSprite;
        }
        public override void SelectAction()
        {
            return;
        }
        public override void CancelAction()
        {
            OutSelectEvent();
        }
    }
}