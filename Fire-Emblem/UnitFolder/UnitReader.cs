using System.Text.Json;

namespace Fire_Emblem.UnitFolder;

public class UnitReader
{
    public static Unit FindUnitInJsonFile(string characterName)
    {
        string jsonFilePath  = "characters.json";
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFilePath );

        string charactersJson = File.ReadAllText(fullPath);
        
        var jsonOptions  = new JsonSerializerOptions
        {
            Converters = { new JsonStringToIntConverter() }
        };

        var characters = JsonSerializer.Deserialize<List<Unit>>(charactersJson, jsonOptions );
        return characters?.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
    }
}
