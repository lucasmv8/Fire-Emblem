using Fire_Emblem.RoundParticipants;

namespace Fire_Emblem.SkillModule.Skills;

public class SkillManager
{
    private void ApplyInitialSkillsToUnit(Unit unit, Logs logs)
    {
        SkillFactory skillFactory = new SkillFactory(logs);
    
        foreach (Skill skill in unit.Skills)
        {
            skillFactory.AddConditionAndEffectToSkill(skill, unit);
        }
    }

    private void UpdateConditions(Unit unit, Logs logs)
    {
        foreach (Skill skill in unit.Skills)
        {
            SkillFactory skillFactory = new SkillFactory(logs);
            skillFactory.AddConditionAndEffectToSkill(skill, unit);
        }
    }
    
    public static void AddSkillsToAUnit(Unit unit, string skills)
    {
        string[] skillNames = skills.Split(',', StringSplitOptions.RemoveEmptyEntries);
        unit.Skills = skillNames.Select(skillName => SkillLoader.GetSkillByName(skillName.Trim())).ToList();
    }
    
    public void UpdateRoundConditionsForBothTeams(CombatParticipants combatParticipants, CombatExecutor combatExecutor)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;
        SkillManager skillManager = new SkillManager();
        
        if (combatExecutor._round > 1)
        {
            skillManager.UpdateConditions(attacker, combatExecutor._logs);
            skillManager.UpdateConditions(defender, combatExecutor._logs);
        }

        if (combatExecutor._round == 1)
        {
            skillManager.ApplyInitialSkillsToUnit(attacker, combatExecutor._logs);
            skillManager.ApplyInitialSkillsToUnit(defender, combatExecutor._logs);
        }
    }
}