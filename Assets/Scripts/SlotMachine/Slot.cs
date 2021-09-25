using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum SlotType
    {
        Seven,
        Attack,
        Guard,
        Heel,
        Charge,
    }
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        SlotType m_type = default;
        [SerializeField]
        int m_testDebagEffect = default;
        [SerializeField]
        RectTransform m_rect = default;
        public SlotType Type { get => m_type; }
        public int TestDebagEffect { get => m_testDebagEffect; }
        public RectTransform SlotRect { get => m_rect; }
        public void PlayEffect()
        {
            Debug.Log($"{Type}が発動！、効果：{m_testDebagEffect}");
            switch (m_type)
            {
                case SlotType.Seven:
                    break;
                case SlotType.Attack:
                    BattleManager.Instance?.AttackEnemy(m_testDebagEffect);
                    break;
                case SlotType.Guard:
                    BattleManager.Instance.Player?.AddGuard(m_testDebagEffect);
                    break;
                case SlotType.Heel:
                    BattleManager.Instance.Player?.HeelPlayer(m_testDebagEffect);
                    break;
                case SlotType.Charge:
                    BattleManager.Instance.Player?.Charge(m_testDebagEffect);
                    break;
                default:
                    break;
            }
        }
    }
}