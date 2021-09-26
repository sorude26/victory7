using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        SkillType m_type = default;
        [SerializeField]
        int m_testDebagEffect = default;
        [SerializeField]
        RectTransform m_rect = default;
        public SkillType Type { get => m_type; }
        public int TestDebagEffect { get => m_testDebagEffect; }
        public RectTransform SlotRect { get => m_rect; }
        public void PlayEffect()
        {
            Debug.Log($"{Type}が発動！、効果：{m_testDebagEffect}");
            if (m_type == SkillType.Seven)
            {
                var message = Instantiate(EffectManager.Instance.Text);
                message.transform.position = new Vector2(0, -4f);
                message.View("Fever！！", new Color(1f, 0, 0.7f), 200, 3f);
            }
            SkillController.Skill(m_type, m_testDebagEffect);
        }
    }
}