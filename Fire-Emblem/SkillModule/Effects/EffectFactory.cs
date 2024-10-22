using Fire_Emblem.SkillModule.Effects.EffectsImplementation;

namespace Fire_Emblem.SkillModule.Effects;

public class EffectFactory
{
    public Effect CreateEffect(string effectCase, params object[] args)
    {
        switch (effectCase)
        {
            case "BonusEffect":
            {
                if (args.Length == 3 && args[0] is StatType targetStat && args[1] is int bonus 
                    && args[2] is TargetType targetType)
                {
                    return new BonusEffect(targetStat, bonus, targetType);
                }
                throw new ArgumentException("Invalid arguments for BonusEffect");
            }
            case "PenaltyEffect":
            {
                if (args.Length == 3 && args[0] is StatType targetStat && args[1] is int penalty 
                    && args[2] is TargetType targetType)
                {
                    return new PenaltyEffect(targetStat, penalty, targetType);
                }
                throw new ArgumentException("Invalid arguments for PenaltyEffect");
            }
            case "FirstAttackEffect":
            {
                if (args.Length == 4 && args[0] is StatType targetStat && args[1] is int value 
                    && args[2] is EffectType effectType && args[3] is TargetType targetType)
                {
                    return new FirstAttackEffect(targetStat, value, effectType, targetType);
                }
                throw new ArgumentException("Invalid arguments for FirstAttackEffect");
            }
            case "NeutralizationEffect":
            {
                if (args.Length >= 1 && args[0] is NeutralizationType neutralizationType)
                {
                    StatType[] statsToNeutralize = args.Skip(1).Cast<StatType>().ToArray();
                    return new NeutralizationEffect(neutralizationType, statsToNeutralize);
                }

                throw new ArgumentException("Invalid arguments for NeutralizationEffect");
            }
            case "AlterBaseStatsEffect":
            {
                return new AlterBaseStatsEffect();
            }
            case "MultiEffect":
            {
                if (args.Length == 1 && args[0] is List<Effect> effects)
                {
                    return new MultiEffect(effects);
                }
                throw new ArgumentException("Invalid arguments for MultiEffect");
            }
            case "SoulbladeEffect":
            {
                return new SoulbladeEffect();
            }
            case "SandstormEffect":
            {
                return new SandstormEffect();
            }
            case "LunaEffect":
            {
                return new LunaEffect();
            }
            case "ExtraDamageEffect":
            {
                if (args.Length == 3 && args[0] is int extraDamage && args[1] is AttackType attackType 
                    && args[2] is TargetType targetType)
                {
                    return new ExtraDamageEffect(extraDamage, attackType, targetType);
                }
                throw new ArgumentException("Invalid arguments for ExtraDamageEffect");
            }
            case "PercentageDamageEffect":
            {
                if (args.Length == 4 && args[0] is float reductionPercentage && args[1] is AttackType attackType
                    && args[2] is TargetType targetType && args[3] is EffectType effectType)
                {
                    return new PercentageDamageEffect(reductionPercentage, attackType, targetType, effectType);
                }
                throw new ArgumentException("Invalid arguments for PercentageDamageEffect");
            }
            default:
                throw new ArgumentException($"Unknown effect type: {effectCase}");
        }
    }
}
