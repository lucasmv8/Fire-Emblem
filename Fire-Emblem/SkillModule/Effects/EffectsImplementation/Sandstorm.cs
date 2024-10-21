namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation;

public class SandstormEffect : Effect
{
    // Multiplicador de Def para el efecto de Sandstorm (150%)
    private const double DefMultiplier = 1.5;

    // Calcula el 150% de la defensa base
    private int GetDefBasedAtkValue(int baseDef)
    {
        return (int)Math.Floor(baseDef * DefMultiplier);
    }

    // Calcula la diferencia entre el 150% de la defensa y el ataque base
    private (int atkDifference, bool isBonus) CalculateAtkDifferenceFromDef(Unit unit)
    {
        int defBasedAtk = GetDefBasedAtkValue(unit.Def);
        int atkDifference = defBasedAtk - unit.Atk;

        // Es un bonus si la diferencia es positiva o cero, de lo contrario es una penalidad
        bool isBonus = atkDifference >= 0;
        
        return (Math.Abs(atkDifference), isBonus); // Retorna el valor absoluto y si es bonus
    }

    public override void Apply(Unit unit)
    {
        ApplyAtkEffectInFollowUp(unit);
    }

    private void ApplyAtkEffectInFollowUp(Unit unit)
    {
        (int atkDifference, bool isBonus) = CalculateAtkDifferenceFromDef(unit);

        if (isBonus)
        {
            ApplyBonusToFollowUp(unit, atkDifference);
        }
        else
        {
            ApplyPenaltyToFollowUp(unit, atkDifference);
        }
    }

    // Aplica el bono al Follow-Up
    private void ApplyBonusToFollowUp(Unit unit, int bonusValue)
    {
        unit.BonusAndPenaltiesForFollowUp.AddBonus(StatType.Atk, bonusValue);
        Console.WriteLine($"{unit.Name} recibe un bono de Atk+{bonusValue} en el Follow-Up.");
    }

    // Aplica la penalidad al Follow-Up
    private void ApplyPenaltyToFollowUp(Unit unit, int penaltyValue)
    {
        unit.BonusAndPenaltiesForFollowUp.AddPenalty(StatType.Atk, penaltyValue);
        Console.WriteLine($"{unit.Name} recibe una penalidad de Atk-{penaltyValue} en el Follow-Up.");
    }
}
