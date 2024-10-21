using Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;
using Fire_Emblem.SkillModule;

namespace Fire_Emblem.SkillModule.Conditions;

public class ConditionFactory
{
    public Condition CreateCondition(String conditionType, Unit unit, params object[] args)
    {
        switch (conditionType)
        {
            case "HPinARangeCondition":
            {
                if (args.Length == 3 && args[0] is float percentage && args[1] is ComparisonType comparisonType
                    && args[2] is TargetType targetType)
                {
                    return new HPinARangeCondition(percentage, comparisonType, targetType);
                }
                throw new ArgumentException("Invalid arguments for HPinARangeCondition");
            }
            
            case "WithoutCondition":
                return new WithoutCondition();
            
            case "RecentOpponentCondition":
                return new RecentOpponentCondition();
            
            case "UnitStartTheCombatCondition":
            {
                if ((args.Length == 1 && args[0] is Logs logs))
                {
                    return new UnitStartTheCombatCondition(logs);
                }
                throw new ArgumentException("Invalid arguments for UnitStartTheCombatCondition");
            }
            
            case "TheOpponentUseAnSpecificWeaponCondition":
            {
                if (args.Length == 1 && args[0] is WeaponType weaponType)
                {
                    return new TheOpponentUseAnSpecificWeaponCondition(weaponType);
                }

                throw new ArgumentException("Invalid arguments for TheOpponentUseAnSpecificWeaponCondition");
            }
            case "TheUnitUseAnSpecificWeaponCondition":
            {
                if (args.Length == 1 && args[0] is WeaponType weaponType)
                {
                    return new TheUnitUseAnSpecificWeaponCondition(weaponType);
                }

                throw new ArgumentException("Invalid arguments for TheUnitUseAnSpecificWeaponCondition");
            }

            case "TheOpponentStartTheCombatCondition":
            {
                if ((args.Length == 1 && args[0] is Logs logs))
                {
                    return new TheOpponentStartTheCombatCondition(logs); 
                }
                throw new ArgumentException("Invalid arguments for TheOpponentStartTheCombatCondition");
            }
            
            case "StatComparationCondition": //Perfeccionarlo
            {
                if (args.Length == 4 &&
                    args[0] is StatType statToCompare &&
                    args[1] is StatType opponentStat &&
                    args[2] is int modifier &&
                    args[3] is ComparisonType comparisonType)
                {
                    return new StatComparationCondition(statToCompare, opponentStat, modifier, comparisonType);
                }
                throw new ArgumentException("Invalid arguments for StatComparationCondition");
            }
            case "MultiConditionAnd":
            {
                if (args.Length == 1 && args[0] is List<Condition> conditions)
                {
                    return new MultiConditionAnd(conditions);
                }
                throw new ArgumentException("Invalid arguments for MultiConditionAnd");
            }

            case "MultiConditionOr":
            {
                if (args.Length == 1 && args[0] is List<Condition> conditions)
                {
                    return new MultiConditionOr(conditions);
                }
                throw new ArgumentException("Invalid arguments for MultiConditionOr");
            }
            case "TheOpponentIsAManCondition":
                return new TheOpponentIsAManCondition();
            case "WeaponAdvantage":
            {
                if (args.Length == 1 && args[0] is Logs logs)
                {
                    return new WeaponAdvantage(logs);
                }
                throw new ArgumentException("Invalid arguments for WeaponAdvantage");
            }
            default:
                throw new ArgumentException($"Unknown condition type: {conditionType}");
        }
    }
}
