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
        double defense = IsPhysicalWeapon(attacker) ? defender.Def : defender.Res;
        return Math.Max(0, (attacker.Atk * weaponTriangleBonus) - defense);
    }
    
    private double CalculateExtraDamage(Unit attacker, Unit defender, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => attacker.ExtraDamage.BonusNormalAttack - defender.ExtraDamage.PenaltyNormalAttack,
            AttackType.FirstAttack => attacker.ExtraDamage.BonusFirstAttack + attacker.ExtraDamage.BonusNormalAttack 
                                      - defender.ExtraDamage.PenaltyFirstAttack - defender.ExtraDamage.PenaltyNormalAttack,
            AttackType.FollowUp => attacker.ExtraDamage.BonusFollowUpAttack + attacker.ExtraDamage.BonusNormalAttack 
                                   - defender.ExtraDamage.PenaltyFollowUpAttack - defender.ExtraDamage.PenaltyNormalAttack,
            _ => 0
        };
    }
    
    private double GetPercentageReduction(Unit attacker, AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Normal => double.Min(attacker.ExtraDamage.PercentageNormalAttack, attacker.ExtraDamage.PercentageFirstAttack),
            AttackType.FirstAttack => double.Min(attacker.ExtraDamage.PercentageNormalAttack, attacker.ExtraDamage.PercentageFirstAttack),
            AttackType.FollowUp => attacker.ExtraDamage.PercentageFollowUpAttack,
            _ => 1.0 // Si no se define el ataque, no hay reducción
        };
    }
    
    public int CalculateDamageDependingOnWeapon(Unit attacker, Unit defender, AttackType attackType)
    {
        double baseDamage = CalculateBaseDamage(attacker, defender);
    
        double extraDamage = CalculateExtraDamage(attacker, defender, attackType);
    
        double totalDamage = baseDamage + extraDamage;
    
        double percentageReduction = GetPercentageReduction(attacker, attackType);
        totalDamage *= percentageReduction;

        return Math.Max(0, (int)Math.Floor(totalDamage));
    }
}