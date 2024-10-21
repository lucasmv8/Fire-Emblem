using Fire_Emblem_View;
using Fire_Emblem.Round;
using Fire_Emblem.TeamFolder;

namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Logs _logs;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _logs = new Logs();
    }
    
    public void Play()
    {
        TeamLoader teamLoader = new TeamLoader(_teamsFolder, _view);
        List<Team> teams = teamLoader.LoadTeams();

        TeamValidator teamValidator = new TeamValidator();
        if (teamValidator.AreTeamsValids(teams[0], teams[1]))
        {
            StartRound(teams[0], teams[1]);
        }
        else
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
    }

    private void StartRound(Team teamOne, Team teamTwo)
    {
        CombatExecutor combatExecutor = new CombatExecutor(_view, _logs);
        combatExecutor.StartGame(teamOne, teamTwo);
    }
}