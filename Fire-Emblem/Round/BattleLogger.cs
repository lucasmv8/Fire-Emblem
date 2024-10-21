using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.Round;

public class BattleLogger
{
    private Logs _logs;
    
    public BattleLogger(Logs logs)
    {
        _logs = logs;
    }
    
    public void RegisterBattleLogs(CombatParticipants combatParticipants, CombatExecutor combatExecutor)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        CombatDetails combatDetails = new CombatDetails();

        BattleLog attackerBattleLog = new BattleLog(true, combatDetails.CanUnitDoFollowUp(attacker, defender), 
            combatDetails.IsPhysicalWeapon(attacker), combatDetails.CalculateWeaponAdvantage(attacker, defender));
        BattleLog defenderBattleLog = new BattleLog(false, combatDetails.CanUnitDoFollowUp(defender, attacker), 
            combatDetails.IsPhysicalWeapon(defender), combatDetails.CalculateWeaponAdvantage(defender, attacker));
        
        _logs.RegisterBattleLog(attacker, attackerBattleLog);
        _logs.RegisterBattleLog(defender, defenderBattleLog);
    }
}