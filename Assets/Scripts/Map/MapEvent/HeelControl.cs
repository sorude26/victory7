using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class HeelControl : MapEventControlBase
    {
        [Header("回復率")]
        [SerializeField]
        [Range(1f, 100f)]
        float _recoveryRate = 100f;
        public override void StartSet()
        {
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            m_rect.position = m_hidePos;
        }
        public override void SelectAction()
        {
            if (!m_select)
            {
                return;
            }
            PlayerData.HeelHP((int)(PlayerData.MaxHP * _recoveryRate / 100));
            OutSelectEvent();
        }
        public override void MoveLine(int dir)
        {
            return;
        }
        public override void MoveSlot(int dir)
        {
            return;
        }
    }
}
