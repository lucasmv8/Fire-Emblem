namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class MultiEffect : Effect
{
    private readonly List<Effect> _effects;
    public MultiEffect(List<Effect> effects)
    {
        _effects = effects;
    }
    public override void Apply(Unit unit)
    {
        foreach (var effect in _effects)
        {
            effect.Apply(unit);
        }
    }
}