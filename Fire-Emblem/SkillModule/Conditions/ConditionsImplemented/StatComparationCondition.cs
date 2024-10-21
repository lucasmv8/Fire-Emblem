namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class StatComparationCondition : Condition
{
    private readonly StatType _statToCompare;
    private readonly StatType _opponentStat;
    private readonly int _modifier;
    private readonly ComparisonType _comparisonType;

    public StatComparationCondition(StatType statToCompare, StatType opponentStat, int modifier, ComparisonType comparisonType)
    {
        _statToCompare = statToCompare;
        _opponentStat = opponentStat;
        _modifier = modifier;
        _comparisonType = comparisonType;
    }
    public override bool DoesHold(Unit unit)
    {
        int unitStatValue = GetStatValue(unit, _statToCompare);
        int opponentStatValue = GetStatValue(unit.CurrentOpponent, _opponentStat) + _modifier;
        return CompareStats(unitStatValue, opponentStatValue);
    }
    private int GetStatValue(Unit unit, StatType statType)
    {
        return statType switch
        {
            StatType.HP => unit.HP,
            StatType.Spd => unit.Spd,
            StatType.Res => unit.Res,
            StatType.Def => unit.Def,
            StatType.Atk => unit.Atk,
            _ => 0
        };
    }
    private bool CompareStats(int unitStat, int opponentStat)
    {
        return _comparisonType switch
        {
            ComparisonType.GreaterThan => unitStat > opponentStat,
            ComparisonType.LessThan => unitStat < opponentStat,
            ComparisonType.GreaterThanOrEqual => unitStat >= opponentStat,
            ComparisonType.LessThanOrEqual => unitStat <= opponentStat,
            _ => false
        };
    }
}
