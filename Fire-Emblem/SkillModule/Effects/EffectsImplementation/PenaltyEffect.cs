namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class PenaltyEffect : Effect
{
    private readonly StatType _targetStat;
    private readonly int _penalty;
    private readonly TargetType _targetType;

    public PenaltyEffect(StatType targetStat, int penalty, TargetType targetType)
    {
        _targetStat = targetStat;
        _penalty = penalty;
        _targetType = targetType;
    }

    public override void Apply(Unit unit)
    {
        Unit target = _targetType == TargetType.Opponent ? unit.CurrentOpponent : unit;
        target.BonusPenalties.AddPenalty(_targetStat, _penalty);
    }
}