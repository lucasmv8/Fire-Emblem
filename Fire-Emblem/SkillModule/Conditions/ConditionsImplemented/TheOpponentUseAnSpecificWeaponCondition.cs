namespace Fire_Emblem.SkillModule.Conditions.ConditionsImplemented;

public class TheOpponentUseAnSpecificWeaponCondition : Condition
{
    private WeaponType _weaponType;

    public TheOpponentUseAnSpecificWeaponCondition(WeaponType weaponType)
    {
        _weaponType = weaponType;
    }

    public override bool DoesHold(Unit unit)
    {
        Unit opponent = unit.CurrentOpponent;
        return IsWeaponTypeMatching(opponent);
    }

    private bool IsWeaponTypeMatching(Unit opponent)
    {
        if (Enum.TryParse(opponent.Weapon, true, out WeaponType opponentWeaponType))
        {
            return opponentWeaponType == _weaponType;
        }
        return false;
    }
}