using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.UnitFolder;

public class UnitBonusPenaltyResetter
{
    private void ResetUnitBonusesAndPenalties(Unit unit)
    {
        unit.BonusPenalties.Reset();
    }
    
    private void ResetUnitBonusesAndPenaltiesNeutralizers(Unit unit)
    {
        unit.BonusPenaltiesNeutralizer.Reset();
    }

    public void ResetUnitBonusesAndPenaltiesForFollowUp(Unit unit)
    {
        unit.BonusAndPenaltiesForFollowUp.Reset();
    }

    public void ResetExtraDamage(Unit unit)
    {
        unit.ExtraDamage.Reset();
    }
    
    public void ResetBonusesAndPenalties(CombatParticipants combatParticipants)
    {
        ResetUnitBonusesAndPenalties(combatParticipants.Attacker);
        ResetUnitBonusesAndPenalties(combatParticipants.Defender);
    
        ResetUnitBonusesAndPenaltiesNeutralizers(combatParticipants.Attacker);
        ResetUnitBonusesAndPenaltiesNeutralizers(combatParticipants.Defender);

        ResetExtraDamage(combatParticipants.Attacker);
        ResetExtraDamage(combatParticipants.Defender);
    }
}