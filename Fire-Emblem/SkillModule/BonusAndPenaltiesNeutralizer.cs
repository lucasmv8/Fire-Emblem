namespace Fire_Emblem.SkillModule;

public class BonusAndPenaltiesNeutralizer
{
    public Dictionary<StatType, bool> IsBonusNeutralized { get;}
    public Dictionary<StatType, bool> IsPenaltyNeutralized { get;}

    public BonusAndPenaltiesNeutralizer()
    {
        IsBonusNeutralized = new Dictionary<StatType, bool>
        {
            { StatType.Atk, false },
            { StatType.Spd, false },
            { StatType.Def, false },
            { StatType.Res, false }
        };

        IsPenaltyNeutralized = new Dictionary<StatType, bool>
        {
            { StatType.Atk, false },
            { StatType.Spd, false },
            { StatType.Def, false },
            { StatType.Res, false }
        };
    }

    public void NeutralizeBonus(StatType statType)
    {
        IsBonusNeutralized[statType] = true;
    }

    public void NeutralizePenalty(StatType statType)
    {
        IsPenaltyNeutralized[statType] = true;
    }

    public bool IsBonusNeutralizedFor(StatType statType)
    {
        return IsBonusNeutralized[statType];
    }

    public bool IsPenaltyNeutralizedFor(StatType statType)
    {
        return IsPenaltyNeutralized[statType];
    }

    public void Reset()
    {
        foreach (var statType in IsBonusNeutralized.Keys.ToList())
        {
            IsBonusNeutralized[statType] = false;
            IsPenaltyNeutralized[statType] = false;
        }
    }
}