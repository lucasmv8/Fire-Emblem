namespace Fire_Emblem.TeamFolder;

public class TeamCreator
{
    private TeamType DetermineTeam(string line, TeamType currentTeam)
    {
        if (line.Contains("Player 1 Team"))
        {
            return TeamType.TeamOne;
        }
        if (line.Contains("Player 2 Team"))
        {
            return TeamType.TeamTwo;
        }
        return currentTeam;
    }
    
    private void AddUnitToTeam(string line, TeamType teamType, Teams teams)
    {
        if (!string.IsNullOrWhiteSpace(line) && !line.Contains("Team"))
        {
            string unitName = ExtractUnitName(line);
            string skills = ExtractSkills(line);

            if (teamType == TeamType.TeamOne)
            {
                teams.TeamOne.AddInitializedUnit(unitName, skills);
            }
            else
            {
                teams.TeamTwo.AddInitializedUnit(unitName, skills);
            }
        }
    }
    
    private string ExtractUnitName(string line)
    {
        return line.Split('(')[0].Trim();
    }

    private string ExtractSkills(string line)
    {
        return line.Contains('(') ? line.Split('(', ')')[1].Trim() : null;
    }
    
    public List<Team> CreateTeamsFromFile(string[] fileContent)
    {
        Team teamOne = new Team("Player 1 Team");
        Team teamTwo = new Team("Player 2 Team");

        TeamType currentTeam = TeamType.TeamOne;

        foreach (string line in fileContent)
        {
            currentTeam = DetermineTeam(line, currentTeam);
            Teams teams = new Teams(teamOne, teamTwo);
            AddUnitToTeam(line, currentTeam, teams);
        }

        return new List<Team> { teamOne, teamTwo };
    }
}