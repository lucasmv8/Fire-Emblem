namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class LunaEffect : Effect
{
    public override void Apply(Unit unit)
    {
        Unit opponent = unit.CurrentOpponent;

        int defPenalty = opponent.Def / 2;
        int resPenalty = opponent.Res / 2;

        opponent.BonusAndPenaltiesInFirstAttack.AddPenalty(StatType.Def, defPenalty);
        opponent.BonusAndPenaltiesInFirstAttack.AddPenalty(StatType.Res, resPenalty);
    }
}