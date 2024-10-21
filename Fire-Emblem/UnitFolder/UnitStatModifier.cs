namespace Fire_Emblem.UnitFolder;

public class UnitStatModifier
{
    public StatType StatType { get; }
    public int Bonus { get; }
    public int Penalty { get; }

    public UnitStatModifier(StatType statType, int bonus, int penalty)
    {
        StatType = statType;
        Bonus = bonus;
        Penalty = penalty;
    }
}
