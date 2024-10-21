namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class RecentOpponentCondition : Condition
{
    public override bool DoesHold(Unit unit)
    {
        if (unit.LastOpponent == null)
        {
            return false;
        }
        return unit.CurrentOpponent == unit.LastOpponent;
    }
}