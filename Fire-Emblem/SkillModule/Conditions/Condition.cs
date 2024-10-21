namespace Fire_Emblem.SkillModule.Conditions;

public abstract class Condition
{
    public abstract bool DoesHold(Unit unit);
}