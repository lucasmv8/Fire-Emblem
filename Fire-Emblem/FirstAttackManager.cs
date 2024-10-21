using Fire_Emblem.UnitFolder;
using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem;

public class FirstAttackManager
{
    private readonly UnitBonusPenaltyManager _bonusPenaltyManager;

    public FirstAttackManager(UnitBonusPenaltyManager bonusPenaltyManager)
    {
        _bonusPenaltyManager = bonusPenaltyManager;
    }

    private void ApplyFirstAttackBonusOrPenalty(Unit unit)
    {
        if (!unit.IsFirstAttack) return;

        Dictionary<StatType, int> unitFirstAttackBonuses = unit.BonusAndPenaltiesInFirstAttack.Bonuses;
        
        foreach (var stat in unitFirstAttackBonuses.Keys.ToList())
        {
            int bonus = unit.BonusAndPenaltiesInFirstAttack.Bonuses[stat];
            int penalty = unit.BonusAndPenaltiesInFirstAttack.Penalties[stat];

            if (bonus != 0 || penalty != 0)
            {
                UnitStatModifier unitStatModifier = new UnitStatModifier(stat, bonus, penalty);
                _bonusPenaltyManager.ApplyBonusOrPenalty(unit, unitStatModifier);
            }
        }
    }

    public void ResetFirstAttackBonuses(Unit attacker, Unit defender, UnitStatRestorer statRestorer)
    {
        statRestorer.RestablishUnitAtributesForFirstAttack(attacker);
        statRestorer.RestablishUnitAtributesForFirstAttack(defender);
        
        attacker.IsFirstAttack = false;
        defender.IsFirstAttack = false;
        
        attacker.BonusAndPenaltiesInFirstAttack.Reset();
        defender.BonusAndPenaltiesInFirstAttack.Reset();
    }
    
    public void ApplyFirstAttackBonuses(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        
        attacker.IsFirstAttack = true;
        defender.IsFirstAttack = true;
        
        ApplyFirstAttackBonusOrPenalty(attacker);
        ApplyFirstAttackBonusOrPenalty(defender);
    }
}