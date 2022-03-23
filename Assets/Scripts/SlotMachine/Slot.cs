using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        int m_id = default;
        [SerializeField]
        SkillType m_type = default;
        [SerializeField]
        int m_effect = default;
        [SerializeField]
        Image m_slotImage = default;
        [Header("レベルアップ先のスロット")]
        [SerializeField]
        Slot m_levelUpTarget = default;
        [SerializeField]
        RectTransform m_rect = default;
        [SerializeField]
        GameObject m_selectMark = default;

        public int ID { get => m_id; }
        public SkillType Type { get => m_type; }
        public int EffectID { get => m_effect; }
        public RectTransform SlotRect { get => m_rect; }
        public Slot LevelUpTarget { get => m_levelUpTarget; }
        public Sprite SlotSprite { get => m_slotImage.sprite; }
        public void PlayEffect()
        {
            //Debug.Log($"{Type}が発動！、効果：{m_testDebagEffect}");
            SkillController.Skill(m_type, m_effect);
        }
        public void DestroySlot()
        {
            Destroy(gameObject);
        }
        public void Select()
        {
            m_selectMark?.SetActive(true);
        }
        public void OutSelect()
        {
            m_selectMark?.SetActive(false);
        }
    }
}