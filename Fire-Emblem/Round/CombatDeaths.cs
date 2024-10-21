using Fire_Emblem_View;
using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.Round;

public class CombatDeaths
{
    private void RemoveDeadUnitFromTeam(Team team, Unit unit)
    {
        team.Unit.Remove(unit);
    }
    
    public void HandleUnitDeath(CombatTeams combatTeams, CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        
        if (attacker.HP == 0)
        {
            RemoveDeadUnitFromTeam(combatTeams.AttackingTeam, attacker);
        }
        else if (defender.HP == 0)
        {
            RemoveDeadUnitFromTeam(combatTeams.DefendingTeam, defender);
        }
    }
    
    public bool CheckIfUnitIsDead(CombatParticipants combatParticipants, View view, CombatExecutor combatExecutor)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        
        if (defender.HP == 0 || attacker.HP == 0)
        {
            RoundPrints roundPrints = new RoundPrints(view, combatExecutor);
            roundPrints.PrintCombatResult(attacker, defender);
            return true;
        }
        return false;
    }
}