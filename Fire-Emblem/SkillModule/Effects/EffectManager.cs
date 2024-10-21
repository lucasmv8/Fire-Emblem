using Fire_Emblem.RoundParticipants;
using Fire_Emblem_View;

namespace Fire_Emblem.SkillModule.Effects;

public class EffectManager
{
    private EffectTypeBonusPenaltyMapper _effectTypeBonusPenaltyMapper;
    private StatPrinter _statPrinter;
    private StatResetter _statResetter;
    private View _view;

    public EffectManager(View view)
    {
        _view = view;
        _effectTypeBonusPenaltyMapper = new EffectTypeBonusPenaltyMapper();
        _statPrinter = new StatPrinter(_view);
        _statResetter = new StatResetter();
    }

    public void ManageAllEffects(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        ManageUnitEffects(attacker);
        ManageUnitEffects(defender);
    }

    private void ManageUnitEffects(Unit unit)
    {
        ManageUnitBonuses(unit, new string[] { "Normal", "FirstAttack", "FollowUp" });
        ManageUnitPenalties(unit, new string[] { "Normal", "FirstAttack", "FollowUp" });

        ManageUnitNeutralizedBonuses(unit);
        ManageUnitNeutralizedPenalties(unit);
        _statPrinter.PrintBonusExtraDamage(unit);
        _statPrinter.PrintDamageReduction(unit);
        _statPrinter.PrintPenaltyExtraDamage(unit);
    }
    
    private void ManageUnitBonuses(Unit unit, string[] effectTypesArray)
    {
        foreach (string effectTypeString in effectTypesArray)
        {
            if (Enum.TryParse<EffectType>(effectTypeString, out var effectType))
            {
                _statPrinter.PrintBonusesByEffectType(unit, effectType);
            }
            else
            {
                _view.WriteLine($"El tipo de efecto '{effectTypeString}' no es válido.");
            }
        }
    }

    private void ManageUnitPenalties(Unit unit, string[] effectTypesArray)
    {
        foreach (string effectTypeString in effectTypesArray)
        {
            if (Enum.TryParse<EffectType>(effectTypeString, out var effectType))
            {
                _statPrinter.PrintPenaltiesByEffectType(unit, effectType);
            }
            else
            {
                _view.WriteLine($"El tipo de efecto '{effectTypeString}' no es válido.");
            }
        }
    }


    private void ManageUnitNeutralizedBonuses(Unit unit)
    {
        _statPrinter.PrintNeutralizedBonuses(unit);
        _statResetter.ResetNeutralizedBonuses(unit);
    }

    private void ManageUnitNeutralizedPenalties(Unit unit)
    {
        _statPrinter.PrintNeutralizedPenalties(unit);
        _statResetter.ResetNeutralizedPenalties(unit);
    }
}