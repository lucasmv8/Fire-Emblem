namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class TheOpponentStartTheCombatCondition : Condition
{
    private Logs _logs;

    public TheOpponentStartTheCombatCondition(Logs logs)
    {
        _logs = logs;
    }

    public override bool DoesHold(Unit unit)
    {
        bool? isAttacking = _logs.HasAttacked(unit.CurrentOpponent);
        return isAttacking.Value;
    }
}