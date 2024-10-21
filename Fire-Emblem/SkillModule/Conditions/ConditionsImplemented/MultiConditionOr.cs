namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;
    
public class MultiConditionOr : Condition
{
    private readonly List<Condition> _conditions;

    public MultiConditionOr(List<Condition> conditions)
    {
        _conditions = conditions;
    }

    public override bool DoesHold(Unit unit)
    {
        return _conditions.Any(condition => condition.DoesHold(unit));
    }

}