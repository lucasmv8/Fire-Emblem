namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class AlterBaseStatsEffect : Effect
{
    public override void Apply(Unit unit)
    {
        if (!unit.BaseStatsAltered)
        {
            unit.MaxHP = unit.MaxHP + 15;
            unit.HP = unit.HP + 15;
            unit.BaseStatsAltered = true;
        }
    }
}