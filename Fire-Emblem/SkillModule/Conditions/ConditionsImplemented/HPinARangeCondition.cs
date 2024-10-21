namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class HPinARangeCondition : Condition
{
    private readonly float _percentage;
    private readonly ComparisonType _comparisonType;
    private readonly TargetType _targetType;

    public HPinARangeCondition(float percentage, ComparisonType comparisonType, TargetType targetType)
    {
        _percentage = percentage;
        _comparisonType = comparisonType;
        _targetType = targetType;
    }

    public override bool DoesHold(Unit unit)
    {
        Unit target = _targetType == TargetType.Opponent ? unit.CurrentOpponent : unit;
        if (target == null)
        {
            return false;
        }
        float currentHpPercentage = (float)target.HP / target.MaxHP * 100;
        return CompareHp(currentHpPercentage, _percentage);
    }

    private bool CompareHp(float currentHpPercentage, float targetPercentage)
    {
        return _comparisonType switch
        {
            ComparisonType.GreaterThanOrEqual => currentHpPercentage >= targetPercentage,
            ComparisonType.LessThanOrEqual => currentHpPercentage <= targetPercentage,
            ComparisonType.Equal => Math.Abs(currentHpPercentage - targetPercentage) < 0.01f,
            _ => false
        };
    }
}