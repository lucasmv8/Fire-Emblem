namespace Fire_Emblem.SkillModule;

public class StatResetter
{
    public void ResetNeutralizedBonuses(Unit unit)
    {
        foreach (var statType in unit.BonusPenalties.Bonuses.Keys.ToList())
        {
            if (unit.BonusPenaltiesNeutralizer.IsBonusNeutralizedFor(statType))
            {
                ResetBonusForStat(unit, statType);
            }
        }
    }

    public void ResetNeutralizedPenalties(Unit unit)
    {
        foreach (var statType in unit.BonusPenalties.Penalties.Keys.ToList())
        {
            if (unit.BonusPenaltiesNeutralizer.IsPenaltyNeutralizedFor(statType))
            {
                ResetPenaltyForStat(unit, statType);
            }
        }
    }

    private void ResetBonusForStat(Unit unit, StatType statType)
    {
        unit.BonusPenalties.Bonuses[statType] = 0;
        unit.BonusAndPenaltiesInFirstAttack.Bonuses[statType] = 0;
    }

    private void ResetPenaltyForStat(Unit unit, StatType statType)
    {
        unit.BonusPenalties.Penalties[statType] = 0;
        unit.BonusAndPenaltiesInFirstAttack.Penalties[statType] = 0;
    }
}
