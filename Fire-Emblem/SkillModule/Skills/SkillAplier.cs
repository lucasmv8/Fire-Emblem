namespace Fire_Emblem.SkillModule.Skills;

public class SkillApplier
{
    public void ApplySkillToUnit(Skill skill, Unit unit)
    {
        if (IsSkillValid(skill) && skill.GetCondition().DoesHold(unit))
        {
            skill.GetEffect().Apply(unit);
        }
    }

    private bool IsSkillValid(Skill skill)
    {
        return skill?.GetCondition() != null && skill?.GetEffect() != null;
    }
}
