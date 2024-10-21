using Fire_Emblem_View;

namespace Fire_Emblem.TeamFolder;

public class TeamLoader
{
    private string _teamsFolder;
    private View _view;

    public TeamLoader(string teamsFolder, View view)
    {
        _teamsFolder = teamsFolder;
        _view = view;
    }
    
    public List<Team> LoadTeams()
    {
        TeamLoader teamLoader = new TeamLoader(_teamsFolder, _view);
        string selectedFile = teamLoader.GetSelectedFileFromPlayer();
        TeamCreator teamCreator = new TeamCreator();
        return teamCreator.CreateTeamsFromFile(File.ReadAllLines(selectedFile));
    }
    
    public string GetSelectedFileFromPlayer()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        string[] files = Directory.GetFiles(_teamsFolder);

        PrintFileNames(files);
        
        int selectedFileIndex = Convert.ToInt32(_view.ReadLine());
        return files[selectedFileIndex];
    }
    
    private void PrintFileNames(string[] fileContent)
    {
        for (int i = 0; i < fileContent.Length; i++)
        {
            string fileName = Path.GetFileName(fileContent[i]);
            _view.WriteLine($"{i}: {fileName}");
        }
    }
}