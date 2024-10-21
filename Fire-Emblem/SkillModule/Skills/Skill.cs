using Fire_Emblem.SkillModule.Conditions;
using Fire_Emblem.SkillModule.Effects;

namespace Fire_Emblem.SkillModule.Skills;

public class Skill
{
    public string Name { get; set; }
    private Condition condition;
    private Effect effect;

    public Skill() { }

    public Skill(string name, Condition condition, Effect effect)
    {
        Name = name;
        this.condition = condition;
        this.effect = effect;
    }

    public void SetCondition(Condition condition)
    {
        this.condition = condition;
    }

    public void SetEffect(Effect effect)
    {
        this.effect = effect;
    }

    public Condition GetCondition()
    {
        return condition;
    }

    public Effect GetEffect()
    {
        return effect;
    }
}