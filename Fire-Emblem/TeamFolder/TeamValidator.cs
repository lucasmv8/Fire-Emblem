using Fire_Emblem.SkillModule.Skills;

namespace Fire_Emblem.TeamFolder;
public class TeamValidator
{
    private bool TheTeamHasUniqueUnits(Team team)
    {
        var uniqueUnitNames = new HashSet<string>();
        return AreUnitsUnique(team.Unit, uniqueUnitNames);
    }
    
    private bool AreUnitsUnique(List<Unit> units, HashSet<string> uniqueUnitNames)
    {
        foreach (Unit unit in units)
        {
            if (IsUnitDuplicate(unit, uniqueUnitNames))
            {
                return false;
            }
        }
        return true;
    }
    
    private bool IsUnitDuplicate(Unit unit, HashSet<string> uniqueUnitNames)
    {
        return !uniqueUnitNames.Add(unit.Name);
    }
    
    private bool HasMaxTwoSkillsPerUnit(Team team)
    {
        return team.Unit.All(unit => unit.Skills.Count <= 2);
    }
    
    private bool HasUniqueSkills(Team team)
    {
        foreach (var unit in team.Unit)
        {
            if (!AreSkillsUnique(unit))
            {
                return false;
            }
        }
        return true;
    }
    
    private bool AreSkillsUnique(Unit unit)
    {
        HashSet<string> skillNames = new HashSet<string>();
        foreach (var skill in unit.Skills)
        {
            if (!IsSkillUnique(skill, skillNames))
            {
                return false;
            }
        }
        return true;
    }
    
    private bool IsSkillUnique(Skill skill, HashSet<string> skillNames)
    {
        return skillNames.Add(skill.Name);
    }
    
    private bool HasValidUnitCount(Team team)
    {
        return team.Unit.Count >= 1 && team.Unit.Count <= 3;
    }
    
    public bool IsTeamValid(Team team)
    {
        return HasValidUnitCount(team) && TheTeamHasUniqueUnits(team) &&
               HasMaxTwoSkillsPerUnit(team) && HasUniqueSkills(team);
    }
    
    public bool AreTeamsValids(Team teamOne, Team teamTwo)
    {
        return IsTeamValid(teamOne) && IsTeamValid(teamTwo);
    }
}