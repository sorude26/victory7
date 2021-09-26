using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public enum SkillType
    {
        Seven,
        Attack,
        Guard,
        Heel,
        Charge,
    }
    public class SkillController
    {
        public static void Skill(SkillType skillType, int effect)
        {
            switch (skillType)
            {
                case SkillType.Seven:
                    BattleManager.Instance?.ChargeFeverTime();
                    break;
                case SkillType.Attack:
                    BattleManager.Instance?.AttackEnemy(effect);
                    break;
                case SkillType.Guard:
                    BattleManager.Instance.Player?.AddGuard(effect);
                    break;
                case SkillType.Heel:
                    BattleManager.Instance.Player?.HeelPlayer(effect);
                    break;
                case SkillType.Charge:
                    BattleManager.Instance.Player?.Charge(effect);
                    break;
                default:
                    break;
            }
        }
    }
}
