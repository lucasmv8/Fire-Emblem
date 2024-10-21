namespace Fire_Emblem.SkillModule.Effects;
public class EffectTypeBonusPenaltyMapper
{
    public Dictionary<StatType, int> GetBonusesByEffectType(Unit unit, EffectType effectType)
    {
        return effectType switch
        {
            EffectType.FirstAttack => unit.BonusAndPenaltiesInFirstAttack.Bonuses,
            EffectType.FollowUp => unit.BonusAndPenaltiesForFollowUp.Bonuses,
            _ => unit.BonusPenalties.Bonuses
        };
    }
    public Dictionary<StatType, int> GetPenaltiesByEffectType(Unit unit, EffectType effectType)
    {
        return effectType switch
        {
            EffectType.FirstAttack => unit.BonusAndPenaltiesInFirstAttack.Penalties,
            EffectType.FollowUp => unit.BonusAndPenaltiesForFollowUp.Penalties,
            _ => unit.BonusPenalties.Penalties
        };
    }
}
