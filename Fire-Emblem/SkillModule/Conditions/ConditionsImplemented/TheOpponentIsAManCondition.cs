namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class TheOpponentIsAManCondition : Condition
{
    public override bool DoesHold(Unit unit)
    {
        Unit opponent = unit.CurrentOpponent;
        return opponent.Gender == "Male";
    }
}