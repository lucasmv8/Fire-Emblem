namespace Fire_Emblem.RoundParticipants;

public class CombatTeams
{
    public Team AttackingTeam { get; }
    public Team DefendingTeam { get; }

    public CombatTeams(Team attackingTeam, Team defendingTeam)
    {
        AttackingTeam = attackingTeam;
        DefendingTeam = defendingTeam;
    }
}