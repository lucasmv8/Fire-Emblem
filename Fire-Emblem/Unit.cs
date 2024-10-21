using System.Text.Json.Serialization;
using Fire_Emblem.SkillModule;
using Fire_Emblem.UnitFolder;

namespace Fire_Emblem;

public class Unit
{
    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    [JsonConverter(typeof(JsonStringToIntConverter))]
    public int HP { get; set; }
    public int MaxHP { get; set; }

    [JsonConverter(typeof(JsonStringToIntConverter))]
    public int Atk { get; set; }

    [JsonConverter(typeof(JsonStringToIntConverter))]
    public int Spd { get; set; }

    [JsonConverter(typeof(JsonStringToIntConverter))]
    public int Def { get; set; }

    [JsonConverter(typeof(JsonStringToIntConverter))]
    public int Res { get; set; }

    public List<SkillModule.Skills.Skill> Skills { get; set; }
    public BonusAndPenalties BonusPenalties { get; private set; }
    public BonusAndPenaltiesNeutralizer BonusPenaltiesNeutralizer { get; private set; }
    public BonusAndPenaltiesForFollowUp BonusAndPenaltiesForFollowUp { get; private set; }
    public BonusAndPenaltiesInFirstAttack BonusAndPenaltiesInFirstAttack { get; private set; }
    public Unit CurrentOpponent { get; set; }
    public Unit LastOpponent { get; set; }
    public bool IsFirstAttack { get; set; }
    public bool BaseStatsAltered { get; set; }
    public ExtraDamage ExtraDamage { get; set; }

    public Unit(string name, string weapon, string gender, string deathQuote,
        int hp, int atk, int spd, int def, int res, List<SkillModule.Skills.Skill> skills)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
        HP = hp;
        MaxHP = hp;
        Atk = atk;
        Spd = spd;
        Def = def;
        Res = res;
        Skills = skills ?? new List<SkillModule.Skills.Skill>();
        BonusPenalties = new BonusAndPenalties();
        BonusPenaltiesNeutralizer = new BonusAndPenaltiesNeutralizer();
        BonusAndPenaltiesInFirstAttack = new BonusAndPenaltiesInFirstAttack();
        BonusAndPenaltiesForFollowUp = new BonusAndPenaltiesForFollowUp();
        BaseStatsAltered = false;
        ExtraDamage = new ExtraDamage();
    }
}