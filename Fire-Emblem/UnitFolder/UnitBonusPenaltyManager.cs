using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.UnitFolder;

public class UnitBonusPenaltyManager
{
    private void ApplyBonusesAndPenalties(Unit unit)
    {
        unit.Atk += unit.BonusPenalties.GetFinalStat(StatType.Atk);
        unit.Def += unit.BonusPenalties.GetFinalStat(StatType.Def);
        unit.Spd += unit.BonusPenalties.GetFinalStat(StatType.Spd);
        unit.Res += unit.BonusPenalties.GetFinalStat(StatType.Res);
    }

    public void ApplyFollowUpSkill(Unit unit)
    {
        unit.Atk += unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Atk);
        unit.Spd += unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Spd);
        unit.Def += unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Def);
        unit.Res += unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Res);
    }
    
    public void ApplyBonusOrPenalty(Unit unit, UnitStatModifier unitStatModifier)
    {
        if (unitStatModifier.Bonus != 0)
        {
            ApplyBonus(unit, unitStatModifier.StatType, unitStatModifier.Bonus);
        }

        if (unitStatModifier.Penalty != 0)
        {
            ApplyPenalty(unit, unitStatModifier.StatType, unitStatModifier.Penalty);
        }
    }

    private void ApplyBonus(Unit unit, StatType statType, int bonus)
    {
        switch (statType)
        {
            case StatType.Atk:
                unit.Atk += bonus;
                break;
            case StatType.Def:
                unit.Def += bonus;
                break;
            case StatType.Spd:
                unit.Spd += bonus;
                break;
            case StatType.Res:
                unit.Res += bonus;
                break;
        }
    }

    private void ApplyPenalty(Unit unit, StatType statType, int penalty)
    {
        switch (statType)
        {
            case StatType.Atk:
                unit.Atk -= penalty;
                break;
            case StatType.Def:
                unit.Def -= penalty;
                break;
            case StatType.Spd:
                unit.Spd -= penalty;
                break;
            case StatType.Res:
                unit.Res -= penalty;
                break;
        }
    }
    
    public void ApplyBonusesAndPenalties(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        ApplyBonusesAndPenalties(attacker);
        ApplyBonusesAndPenalties(defender);
    }
}
