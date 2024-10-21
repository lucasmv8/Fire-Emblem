namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class BonusEffect : Effect
{
    private readonly StatType _targetStat;
    private readonly int _bonus;
    private readonly TargetType _targetType;

    public BonusEffect(StatType targetStat, int bonus, TargetType targetType)
    {
        _targetStat = targetStat;
        _bonus = bonus;
        _targetType = targetType;
    }

    public override void Apply(Unit unit)
    {
        Unit target = _targetType == TargetType.Opponent ? unit.CurrentOpponent : unit;
        target.BonusPenalties.AddBonus(_targetStat, _bonus);
    }
}