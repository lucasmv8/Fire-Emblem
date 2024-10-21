namespace Fire_Emblem.SkillModule
{
    public class BonusAndPenaltiesForFollowUp
    {
        public Dictionary<StatType, int> Bonuses { get;}
        public Dictionary<StatType, int> Penalties { get;}

        public BonusAndPenaltiesForFollowUp()
        {
            Bonuses = new Dictionary<StatType, int>
            {
                { StatType.Atk, 0 },
                { StatType.Spd, 0 },
                { StatType.Def, 0 },
                { StatType.Res, 0 }
            };

            Penalties = new Dictionary<StatType, int>
            {
                { StatType.Atk, 0 },
                { StatType.Spd, 0 },
                { StatType.Def, 0 },
                { StatType.Res, 0 }
            };
        }

        public void AddBonus(StatType statType, int amount)
        {
            Bonuses[statType] += amount;
        }

        public void AddPenalty(StatType statType, int amount)
        {
            Penalties[statType] += amount;
        }

        public int GetFinalStat(StatType statType)
        {
            return Bonuses[statType] - Penalties[statType];
        }

        public void Reset()
        {
            foreach (var statType in Bonuses.Keys.ToList())
            {
                Bonuses[statType] = 0;
                Penalties[statType] = 0;
            }
        }
    }
}