namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class MultiConditionAnd : Condition
{
    private List<Condition> _conditions;

    public MultiConditionAnd(List<Condition> conditions)
    {
        _conditions = conditions;
    }

    public override bool DoesHold(Unit unit)
    {
        return _conditions.All(condition => condition.DoesHold(unit));
    }

}