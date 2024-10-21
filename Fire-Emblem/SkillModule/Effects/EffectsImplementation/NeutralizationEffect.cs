namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class NeutralizationEffect : Effect
{
    private readonly List<StatType> _statsToNeutralize;
    private readonly NeutralizationType _neutralizationType;

    public NeutralizationEffect(NeutralizationType neutralizationType, params StatType[] statsToNeutralize)
    {
        _neutralizationType = neutralizationType;
        _statsToNeutralize = statsToNeutralize?.ToList() ?? new List<StatType>();
    }

    public override void Apply(Unit unit)
    {
        Unit target = GetTargetUnit(unit);
        List<StatType> stats = GetStatsToNeutralize(target);
        foreach (var stat in stats)
        {
            NeutralizeStatIfNeeded(target, stat);
        }
    }

    private Unit GetTargetUnit(Unit unit)
    {
        return _neutralizationType == NeutralizationType.Bonus ? unit.CurrentOpponent : unit;
    }

    private List<StatType> GetStatsToNeutralize(Unit target)
    {
        return _statsToNeutralize.Any() ? _statsToNeutralize : GetDefaultStats(target);
    }

    private List<StatType> GetDefaultStats(Unit target)
    {
        return _neutralizationType == NeutralizationType.Bonus
            ? target.BonusPenalties.Bonuses.Keys.ToList()
            : target.BonusPenalties.Penalties.Keys.ToList();
    }

    private void NeutralizeStatIfNeeded(Unit target, StatType stat)
    {
        if (!ShouldNeutralize(target, stat)) return;
        ApplyNeutralization(target, stat);
    }

    private bool ShouldNeutralize(Unit target, StatType stat)
    {
        return (_neutralizationType == NeutralizationType.Bonus && target.BonusPenalties.Bonuses.ContainsKey(stat)) ||
               (_neutralizationType == NeutralizationType.Penalty && target.BonusPenalties.Penalties.ContainsKey(stat));
    }

    private void ApplyNeutralization(Unit target, StatType stat)
    {
        if (_neutralizationType == NeutralizationType.Bonus)
        {
            target.BonusPenaltiesNeutralizer.NeutralizeBonus(stat);
        }
        else
        {
            target.BonusPenaltiesNeutralizer.NeutralizePenalty(stat);
        }
    }
}
