namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class WeaponAdvantage : Condition
{
    private Logs _logs;

    public WeaponAdvantage(Logs logs)
    {
        _logs = logs;
    }

    public override bool DoesHold(Unit unit)
    {
        return _logs.HasWeaponAdvantage(unit);
    }
}