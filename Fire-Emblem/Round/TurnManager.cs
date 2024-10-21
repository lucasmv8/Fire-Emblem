using Fire_Emblem_View;
using Fire_Emblem.RoundParticipants;
using Fire_Emblem.SkillModule.Effects;
using Fire_Emblem.SkillModule.Skills;
using Fire_Emblem.UnitFolder;

namespace Fire_Emblem.Round;

public class TurnManager
{
    private View _view;
    
    public TurnManager(View view)
    {
        _view = view;
    }
    public void PlayTurn(Team teamOne, Team teamTwo, CombatExecutor combatExecutor)
    {
        RoundSetUp roundSetUp = new RoundSetUp(_view, combatExecutor);
        var (attacker, defender, attackingTeam, defendingTeam) = roundSetUp.SetUpRound(teamOne, teamTwo);
        
        CombatParticipants combatParticipants = new CombatParticipants(attacker, defender);
        CombatTeams combatTeams = new CombatTeams(attackingTeam, defendingTeam);
        UnitBonusPenaltyResetter unitBonusPenaltyResetter = new UnitBonusPenaltyResetter();
        
        combatExecutor.ExecuteRound(combatParticipants, unitBonusPenaltyResetter);
        combatExecutor.ManageEndOfTheRound(combatParticipants, combatTeams, unitBonusPenaltyResetter);
    }
}
