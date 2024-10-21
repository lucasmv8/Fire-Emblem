namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class FirstAttackEffect : Effect
{
    private readonly StatType _targetStat;
    private readonly int _value;
    private readonly EffectType _effectType;
    private readonly TargetType _targetType;

    public FirstAttackEffect(StatType targetStat, int value, EffectType effectType, TargetType targetType)
    {
        _targetStat = targetStat;
        _value = value;
        _effectType = effectType;
        _targetType = targetType;
    }

    public override void Apply(Unit unit)
    {
        Unit target = _targetType == TargetType.Opponent ? unit.CurrentOpponent : unit;

        if (_effectType == EffectType.Bonus)
        {
            target.BonusAndPenaltiesInFirstAttack.AddBonus(_targetStat, _value);
        }
        else if (_effectType == EffectType.Penalty)
        {
            target.BonusAndPenaltiesInFirstAttack.AddPenalty(_targetStat, _value);
        }
    }
}