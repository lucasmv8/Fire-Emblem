using Fire_Emblem.UnitFolder;

namespace Fire_Emblem;

public class Logs
{
    private Dictionary<Unit, BattleLog> battleLogs = new Dictionary<Unit, BattleLog>();
    public void RegisterBattleLog(Unit unit, BattleLog battleLog )
    {
        battleLogs[unit] = battleLog;
    }
    public bool HasAttacked(Unit unit)
    {
        return battleLogs.ContainsKey(unit) ? battleLogs[unit].IsAttacking : false;
    }
    public bool CouldDoFollowUp(Unit unit)
    {
        return battleLogs.ContainsKey(unit) ? battleLogs[unit].CanFollowUp : false;
    }
    public bool HasWeaponAdvantage(Unit unit)
    {
        if (battleLogs.TryGetValue(unit, out var battleLog))
        {
            return battleLog.HasWeaponAdvantage == 1.2;
        }
        return false;
    }

}