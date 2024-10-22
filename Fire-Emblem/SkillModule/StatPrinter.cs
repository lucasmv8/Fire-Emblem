using Fire_Emblem_View;
using Fire_Emblem.SkillModule.Skills;
using Fire_Emblem.SkillModule.Effects;

namespace Fire_Emblem.SkillModule;

public class StatPrinter
{
    private View _view;
    private EffectTypeBonusPenaltyMapper _effectTypeBonusPenaltyMapper = new EffectTypeBonusPenaltyMapper();

    public StatPrinter(View view)
    {
        _view = view;
    }
    
    public void PrintBonusesByEffectType(Unit unit, EffectType effectType)
    {
        var bonuses = _effectTypeBonusPenaltyMapper.GetBonusesByEffectType(unit, effectType);
        var printInfo = new StatPrintInfo(unit, _view, bonuses, "obtiene", "+", effectType);
        PrintNonZeroStats(printInfo);
    }

    public void PrintPenaltiesByEffectType(Unit unit, EffectType effectType)
    {
        var penalties = _effectTypeBonusPenaltyMapper.GetPenaltiesByEffectType(unit, effectType);
        var printInfo = new StatPrintInfo(unit, _view, penalties, "obtiene", "-", effectType);
        PrintNonZeroStats(printInfo);
    }

    private void PrintNonZeroStats(StatPrintInfo printInfo)
    {
        foreach (var stat in printInfo.Stats)
        {
            PrintStatIfNonZero(printInfo, stat);
        }
    }

    private void PrintStatIfNonZero(StatPrintInfo printInfo, KeyValuePair<StatType, int> stat)
    {
        if (stat.Value == 0) return;

        string phaseMessage = GetPhaseMessage(printInfo.EffectType);
        printInfo.View.WriteLine($"{printInfo.Unit.Name} {printInfo.Message} {stat.Key}{printInfo.Sign}{stat.Value}{phaseMessage}");
    }

    private string GetPhaseMessage(EffectType effectType)
    {
        return effectType switch
        {
            EffectType.FirstAttack => " en su primer ataque",
            EffectType.FollowUp => " en su Follow-Up",
            _ => string.Empty
        };
    }

    public void PrintNeutralizedBonuses(Unit unit)
    {
        foreach (var statType in unit.BonusPenalties.Bonuses.Keys.ToList())
        {
            if (unit.BonusPenaltiesNeutralizer.IsBonusNeutralizedFor(statType))
            {
                _view.WriteLine($"Los bonus de {statType} de {unit.Name} fueron neutralizados");
            }
        }
    }

    public void PrintNeutralizedPenalties(Unit unit)
    {
        foreach (var statType in unit.BonusPenalties.Penalties.Keys.ToList())
        {
            if (unit.BonusPenaltiesNeutralizer.IsPenaltyNeutralizedFor(statType))
            {
                _view.WriteLine($"Los penalty de {statType} de {unit.Name} fueron neutralizados");
            }
        }
    }

    public void PrintBonusExtraDamage(Unit unit)
    {
        PrintAttackDamage(unit, unit.ExtraDamage.BonusNormalAttack, "cada ataque", isBonus: true);
        PrintAttackDamage(unit, unit.ExtraDamage.BonusFirstAttack, "su primer ataque", isBonus: true);
        PrintAttackDamage(unit, unit.ExtraDamage.BonusFollowUpAttack, "su Follow-Up", isBonus: true);
    }
    
    public void PrintPenaltyExtraDamage(Unit unit)
    {
        PrintAttackDamage(unit, unit.ExtraDamage.PenaltyNormalAttack, "cada ataque", isBonus: false);
        PrintAttackDamage(unit, unit.ExtraDamage.PenaltyFirstAttack, "su primer ataque", isBonus: false);
        PrintAttackDamage(unit, unit.ExtraDamage.PenaltyFollowUpAttack, "su Follow-Up", isBonus: false);
    }

    private void PrintAttackDamage(Unit unit, int damageValue, string attackDescription, bool isBonus)
    {
        if (damageValue == 0) return;

        string damageType = isBonus ? "+" : "-";
        string action = isBonus ? "realizará" : "recibirá";
        string extraText = isBonus ? "extra " : "";

        string message = $"{unit.Name} {action} {damageType}{damageValue} daño {extraText}en {attackDescription}";
        _view.WriteLine(message);
    }
    
    public void PrintDamageReduction(Unit unit)
    {
        Unit opponent = unit.CurrentOpponent;
        PrintReductionForAttackType(unit, opponent.ExtraDamage.PercentagePenaltyNormalAttack, "de los ataques del rival");
        PrintReductionForAttackType(unit, opponent.ExtraDamage.PercentagePenaltyFirstAttack, "del primer ataque del rival");
        PrintReductionForAttackType(unit, opponent.ExtraDamage.PercentagePenaltyFollowUpAttack, "del Follow-Up del rival");
    }
    private void PrintReductionForAttackType(Unit unit, float reductionPercentage, string attackDescription)
    {
        if (reductionPercentage is > 0 and < 1)
        {
            string formattedPercentage = ((1 - reductionPercentage) * 100).ToString("0.##");
            string message = $"{unit.Name} reducirá el daño {attackDescription} en un {formattedPercentage}%";
            _view.WriteLine(message);
        }
    }
}
