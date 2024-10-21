namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class TheUnitUseAnSpecificWeaponCondition : Condition
{
    private WeaponType _weaponType;

    public TheUnitUseAnSpecificWeaponCondition(WeaponType weaponType)
    {
        _weaponType = weaponType;
    }

    public override bool DoesHold(Unit unit)
    {
        return IsWeaponTypeMatching(unit);
    }
    private bool IsWeaponTypeMatching(Unit unit)
    {
        if (Enum.TryParse(unit.Weapon, true, out WeaponType unitWeaponType))
        {
            return unitWeaponType == _weaponType;
        }
        return false;
    }
}