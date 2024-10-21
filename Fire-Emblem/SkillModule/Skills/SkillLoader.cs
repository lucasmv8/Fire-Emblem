using System.Text.Json;

namespace Fire_Emblem.SkillModule.Skills;

public class SkillLoader
{
    public static Skill GetSkillByName(string skillName)
    {
        string skillsJsonFilePath = "skills.json";
        string skillsJsonFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, skillsJsonFilePath);

        string skillsJsonContent = File.ReadAllText(skillsJsonFullPath);
        var deserializedSkills = JsonSerializer.Deserialize<List<Skill>>(skillsJsonContent);

        return deserializedSkills?.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
    }
}
