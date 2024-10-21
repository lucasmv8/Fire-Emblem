namespace Fire_Emblem.RoundParticipants;

public class CombatParticipants
{
    public Unit Attacker { get; }
    public Unit Defender { get; }

    public CombatParticipants(Unit attacker, Unit defender)
    {
        Attacker = attacker;
        Defender = defender;
    }
}