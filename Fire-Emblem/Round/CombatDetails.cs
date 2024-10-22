using Fire_Emblem.SkillModule;

namespace Fire_Emblem.Round;

public class CombatDetails
{
    public double CalculateWeaponAdvantage(Unit attacker, Unit defender)
    {
        double weaponTriangleBonus = 1;
        if ((attacker.Weapon == "Sword" && defender.Weapon == "Axe") ||
            (attacker.Weapon == "Lance" && defender.Weapon == "Sword") ||
            (attacker.Weapon == "Axe" && defender.Weapon == "Lance"))
        {
            weaponTriangleBonus += 0.2;
        }
        else if ((attacker.Weapon == "Sword" && defender.Weapon == "Lance") ||
                 (attacker.Weapon == "Lance" && defender.Weapon == "Axe") ||
                 (attacker.Weapon == "Axe" && defender.Weapon == "Sword"))
        {
            weaponTriangleBonus -= 0.2;
        }
        return weaponTriangleBonus;
    }
    
    public bool CanUnitDoFollowUp(Unit unitOne, Unit unitTwo)
    {
        return unitOne.Spd >= unitTwo.Spd + 5;
    }

    public bool IsPhysicalWeapon(Unit attacker)
    {
        if (attacker.Weapon == "Magic")
        {
            return false;
        }
        return true;
    }

    private double CalculateBaseDamage(Unit attacker, Unit defender)
    {
        double weaponTriangleBonus = CalculateWeaponAdvantage(attacker, defender);
        double defensiveStat = IsPhysicalWeapon(attacker) ? defender.Def : defender.Res;
        return Math.Max(0, (attacker.Atk * weaponTriangleBonus) - defensiveStat);
    }

    private double CalculateExtraDamage(Unit attacker, Unit defender, AttackType attackType)
    {
        double attackerBonus = GetAttackerBonus(attacker, attackType);
        double defenderPenalty = GetDefenderPenalty(defender, attackType);
    
        return attackerBonus - defenderPenalty;
    }
    

    private double GetAttackerBonus(Unit attacker, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => attacker.ExtraDamage.BonusNormalAttack,
            AttackType.FirstAttack => attacker.ExtraDamage.BonusFirstAttack + attacker.ExtraDamage.BonusNormalAttack,
            AttackType.FollowUp => attacker.ExtraDamage.BonusFollowUpAttack + attacker.ExtraDamage.BonusNormalAttack,
            _ => 0
        };
    }

    private double GetDefenderPenalty(Unit defender, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => defender.ExtraDamage.PenaltyNormalAttack,
            AttackType.FirstAttack => defender.ExtraDamage.PenaltyFirstAttack + defender.ExtraDamage.PenaltyNormalAttack,
            AttackType.FollowUp => defender.ExtraDamage.PenaltyFollowUpAttack + defender.ExtraDamage.PenaltyNormalAttack,
            _ => 0
        };
    }
    
    private double GetPercentageModification(Unit attacker, AttackType attackType)
    {
        double bonusPercentage = GetBonusPercentage(attacker, attackType);
        double penaltyPercentage = GetPenaltyPercentage(attacker, attackType);

        if (penaltyPercentage < 1.0)
        {
            Console.WriteLine("Se aplica penalty");
            return penaltyPercentage;
        }

        if (bonusPercentage > 1.0)
        {
            Console.WriteLine("Se aplica bonus");
            return bonusPercentage;
        }

        return 1.0;
    }

    private double GetBonusPercentage(Unit attacker, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => Math.Max(attacker.ExtraDamage.PercentageBonusNormalAttack, attacker.ExtraDamage.PercentageBonusFirstAttack),
            AttackType.FirstAttack => Math.Max(attacker.ExtraDamage.PercentageBonusFirstAttack, attacker.ExtraDamage.PercentageBonusNormalAttack),
            AttackType.FollowUp => attacker.ExtraDamage.PercentageBonusFollowUpAttack,
            _ => 1.0
        };
    }

    private double GetPenaltyPercentage(Unit attacker, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => Math.Min(attacker.ExtraDamage.PercentagePenaltyNormalAttack, attacker.ExtraDamage.PercentagePenaltyFirstAttack),
            AttackType.FirstAttack => Math.Min(attacker.ExtraDamage.PercentagePenaltyFirstAttack, attacker.ExtraDamage.PercentagePenaltyNormalAttack),
            AttackType.FollowUp => attacker.ExtraDamage.PercentagePenaltyFollowUpAttack,
            _ => 1.0
        };
    }
    
    public int CalculateDamageDependingOnWeapon(Unit attacker, Unit defender, AttackType attackType)
    {
        double baseDamage = CalculateBaseDamage(attacker, defender);
        Console.WriteLine($"baseDamage: {baseDamage}");
        double extraDamage = CalculateExtraDamage(attacker, defender, attackType);
        Console.WriteLine($"extraDamage: {extraDamage}");

        double totalDamage = baseDamage + extraDamage;
        Console.WriteLine($"totalDamage: {totalDamage}");

        double percentageModification = GetPercentageModification(attacker, attackType);

        double finalDamage = ApplyPercentageModification(totalDamage, percentageModification);

        return Math.Max(Convert.ToInt32(Math.Floor(finalDamage)), 0);
    }

    private double ApplyPercentageModification(double totalDamage, double percentageModification)
    {
        double modifiedDamage = totalDamage * percentageModification;
        return Math.Round(modifiedDamage, 9);
    }
}