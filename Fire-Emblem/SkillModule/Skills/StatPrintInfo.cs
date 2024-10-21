using Fire_Emblem_View;

namespace Fire_Emblem.SkillModule.Skills;

public class StatPrintInfo
{
    public Unit Unit { get; set; }
    public View View { get; set; }
    public Dictionary<StatType, int> Stats { get; set; }
    public string Message { get; set; }
    public string Sign { get; set; }
    public EffectType EffectType { get; set; }

    public StatPrintInfo(Unit unit, View view, Dictionary<StatType, int> stats, string message, string sign, EffectType effectType)
    {
        Unit = unit;
        View = view;
        Stats = stats;
        Message = message;
        Sign = sign;
        EffectType = effectType;
    }
}
