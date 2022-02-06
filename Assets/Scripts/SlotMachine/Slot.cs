using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        int m_id = default;
        [SerializeField]
        SkillType m_type = default;
        [SerializeField]
        int m_testDebagEffect = default;
        [Header("レベルアップ先のスロット")]
        [SerializeField]
        Slot m_levelUpTarget = default;
        [SerializeField]
        RectTransform m_rect = default;
        [SerializeField]
        GameObject m_selectMark = default;

        public int ID { get => m_id; }
        public SkillType Type { get => m_type; }
        public int EffectID { get => m_testDebagEffect; }
        public RectTransform SlotRect { get => m_rect; }
        public Slot LevelUpTarget { get => m_levelUpTarget; }
        public void PlayEffect()
        {
            //Debug.Log($"{Type}が発動！、効果：{m_testDebagEffect}");
            if (m_type == SkillType.Seven)
            {
                var message = Instantiate(EffectManager.Instance.Text);
                message.transform.position = new Vector2(0, -4f);
                message.View("Fever！！", new Color(1f, 0, 0.7f), 200, 3f);
            }
            SkillController.Skill(m_type, m_testDebagEffect);
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