namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class WithoutCondition : Condition
{
    public override bool DoesHold(Unit unit)
        => true;
}