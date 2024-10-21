using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.UnitFolder;

public class UnitStatRestorer
{
    private void RestablishUnitAtributes(Unit unit)
    {
        unit.Atk -= unit.BonusPenalties.GetFinalStat(StatType.Atk);
        unit.Def -= unit.BonusPenalties.GetFinalStat(StatType.Def);
        unit.Spd -= unit.BonusPenalties.GetFinalStat(StatType.Spd);
        unit.Res -= unit.BonusPenalties.GetFinalStat(StatType.Res);
    }

    public void RestablishUnitAtributesForFirstAttack(Unit unit)
    {
        unit.Atk -= unit.BonusAndPenaltiesInFirstAttack.GetFinalStat(StatType.Atk);
        unit.Def -= unit.BonusAndPenaltiesInFirstAttack.GetFinalStat(StatType.Def);
        unit.Spd -= unit.BonusAndPenaltiesInFirstAttack.GetFinalStat(StatType.Spd);
        unit.Res -= unit.BonusAndPenaltiesInFirstAttack.GetFinalStat(StatType.Res);
    }

    public void RestablishUnitAtributesForFollowUp(Unit unit)
    {
        unit.Atk -= unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Atk);
        unit.Spd -= unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Spd);
        unit.Def -= unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Def);
        unit.Res -= unit.BonusAndPenaltiesForFollowUp.GetFinalStat(StatType.Res);
    }
    
    public void ManageUnitAttributes(CombatParticipants combatParticipants)
    {
        RestablishUnitAtributes(combatParticipants.Attacker);
        RestablishUnitAtributes(combatParticipants.Defender);
    }
}