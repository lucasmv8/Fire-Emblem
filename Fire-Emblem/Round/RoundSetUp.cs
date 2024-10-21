using Fire_Emblem_View;
using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.Round;

public class RoundSetUp
{
    private View _view;
    private CombatExecutor _combatExecutor;
    
    public RoundSetUp(View view, CombatExecutor combatExecutor)
    {
        _view = view;
        _combatExecutor = combatExecutor;
    }
    
    public (Unit, Unit, Team, Team) SetUpRound(Team teamOne, Team teamTwo)
    {
        Team attackingTeam = _combatExecutor._isPlayerOneTurn ? teamOne : teamTwo;
        Team defendingTeam = _combatExecutor._isPlayerOneTurn ? teamTwo : teamOne;

        Unit attacker = SelectAttacker(attackingTeam);
        Unit defender = SelectDefender(defendingTeam);

        RoundPrints roundPrints = new RoundPrints(_view, _combatExecutor);
        
        roundPrints.PrintRoundStart(attacker);
        roundPrints.PrintWeaponAdvantage(attacker, defender);

        return (attacker, defender, attackingTeam, defendingTeam);
    }
    
    private Unit SelectAttacker(Team attackingTeam)
    {
        string player = _combatExecutor._isPlayerOneTurn ? "Player 1" : "Player 2";
        _view.WriteLine($"{player} selecciona una opción");
        return ChooseUnit(attackingTeam);
    }

    private Unit SelectDefender(Team defendingTeam)
    {
        string opponent = _combatExecutor._isPlayerOneTurn ? "Player 2" : "Player 1";
        _view.WriteLine($"{opponent} selecciona una opción");
        return ChooseUnit(defendingTeam);
    }
    
    private Unit GetChosenUnit(Team team)
    {
        int election = Convert.ToInt32(_view.ReadLine());
        return team.Unit[election];
    }
    
    public Unit ChooseUnit(Team team)
    {
        RoundPrints roundPrints = new RoundPrints(_view, _combatExecutor);
        roundPrints.PrintUnitNames(team);
        return GetChosenUnit(team);
    }
    
    public void SetOpponents(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        
        attacker.CurrentOpponent = defender;
        defender.CurrentOpponent = attacker;
    }
    
    public void SetLastOpponent(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        
        attacker.LastOpponent = defender;
        defender.LastOpponent = attacker;
    }
}