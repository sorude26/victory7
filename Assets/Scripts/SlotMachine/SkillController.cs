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
                    BattleManager.Instance.BattleActions.Push(() =>
                    BattleManager.Instance?.AttackEnemy(effect));
                    break;
                case SkillType.Guard:
                    BattleManager.Instance.BattleActions.Push(() =>
                    BattleManager.Instance.Player?.AddGuard(effect));
                    break;
                case SkillType.Heel:
                    BattleManager.Instance.BattleActions.Push(() =>
                    BattleManager.Instance.Player?.HeelPlayer(effect));
                    break;
                case SkillType.Charge:
                    BattleManager.Instance.BattleActions.Push(() =>
                    BattleManager.Instance.Player?.Charge(effect));
                    break;
                default:
                    break;
            }
        }
        public static void UseSkill(PlayerSkill skillType, int effect)
        {
            switch (skillType)
            {
                case PlayerSkill.PercentageAttack:
                    BattleManager.Instance.AttackEnemyPercentage(30);
                    break;
                case PlayerSkill.InstantDeathAttack:
                    BattleManager.Instance.AttackEnemyCritical(100);
                    break;
                case PlayerSkill.Barrier:
                    BattleManager.Instance.Player.Barrier();
                    break;
                default:
                    break;
            }
        }
    }
}
