namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class SoulbladeEffect : Effect
{
    private (int, EffectType) CalculateDifference(Unit unit, StatType statType)
    {
        Unit opponent = unit.CurrentOpponent;
        int averageDefRes = (opponent.Def + opponent.Res) / 2;
        int statValue = statType == StatType.Def ? opponent.Def : opponent.Res;
        int difference = averageDefRes - statValue;
        EffectType effectType = difference >= 0 ? EffectType.Bonus : EffectType.Penalty;
        return (Math.Abs(difference), effectType);
    }

    public override void Apply(Unit unit)
    {
        Unit opponent = unit.CurrentOpponent;
        ApplyEffectToStat(opponent, StatType.Def, CalculateDifference(unit, StatType.Def));
        ApplyEffectToStat(opponent, StatType.Res, CalculateDifference(unit, StatType.Res));
    }
    private void ApplyEffectToStat(Unit opponent, StatType statType, (int difference, EffectType effectType) statDifference)
    {
        if (statDifference.effectType == EffectType.Bonus)
        {
            opponent.BonusPenalties.AddBonus(statType, statDifference.difference);
        }
        else
        {
            opponent.BonusPenalties.AddPenalty(statType, statDifference.difference);
        }
    }
}