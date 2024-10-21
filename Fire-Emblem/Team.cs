using Fire_Emblem.SkillModule.Skills;
using Fire_Emblem.UnitFolder;

namespace Fire_Emblem;

public class Team
{
    public string TeamName { get; set; }
    public List<Unit> Unit { get; set; }

    public Team(string teamName)
    {
        TeamName = teamName;
        Unit = new List<Unit>();
    }

    public void AddInitializedUnit(string unitName, string skills)
    {
        Unit foundUnit = UnitReader.FindUnitInJsonFile(unitName);

        if (!string.IsNullOrEmpty(skills))
        {
            SkillManager.AddSkillsToAUnit(foundUnit, skills);
        }
        Unit.Add(foundUnit);
    }
}