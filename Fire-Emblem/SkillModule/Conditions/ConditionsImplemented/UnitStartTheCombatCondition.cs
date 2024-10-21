namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class UnitStartTheCombatCondition : Condition
{
    private Logs _logs;

    public UnitStartTheCombatCondition(Logs logs)
    {
        _logs = logs;
    }

    public override bool DoesHold(Unit unit)
    {
        bool? isAttacking = _logs.HasAttacked(unit);
        return isAttacking.Value;
    }
}