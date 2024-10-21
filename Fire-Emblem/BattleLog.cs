namespace Fire_Emblem;

public class BattleLog
{
    public bool IsAttacking { get; set; }
    public bool CanFollowUp { get; set; }
    public bool IsPhysicalWeapon { get; set; }
    public double HasWeaponAdvantage { get; set; }

    public BattleLog(bool isAttacking, bool canFollowUp, bool isPhysicalWeapon, double hasWeaponAdvantage)
    {
        IsAttacking = isAttacking;
        CanFollowUp = canFollowUp;
        IsPhysicalWeapon = isPhysicalWeapon;
        HasWeaponAdvantage = hasWeaponAdvantage;
    }
}