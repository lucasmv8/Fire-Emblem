using Fire_Emblem_View;

namespace Fire_Emblem.Round;

public class RoundPrints
{
    private View _view;
    private CombatExecutor _combatExecutor;
    
    public RoundPrints(View view, CombatExecutor combatExecutor)
    {
        _view = view;
        _combatExecutor = combatExecutor;
    }
    
    public void PrintRoundStart(Unit attacker)
    {
        string player = _combatExecutor._isPlayerOneTurn ? "Player 1" : "Player 2";
        _view.WriteLine($"Round {_combatExecutor._round}: {attacker.Name} ({player}) comienza");
    }
    
    private string PrintWeaponAdvantageBetween(Unit unitOne, Unit unitTwo)
    {
        CombatDetails combatDetails = new CombatDetails();
        if (combatDetails.CalculateWeaponAdvantage(unitOne, unitTwo) == 1.2)
        {
            return $"{unitOne.Name} ({unitOne.Weapon}) tiene ventaja con respecto a {unitTwo.Name} ({unitTwo.Weapon})";
        }
        if (combatDetails.CalculateWeaponAdvantage(unitTwo, unitOne) == 1.2)
        {
            return $"{unitTwo.Name} ({unitTwo.Weapon}) tiene ventaja con respecto a {unitOne.Name} ({unitOne.Weapon})";
        }
        return "Ninguna unidad tiene ventaja con respecto a la otra";
    }
    
    public void PrintWeaponAdvantage(Unit attacker, Unit defender)
    {
        string advantage = PrintWeaponAdvantageBetween(attacker, defender);
        _view.WriteLine(advantage);
    }
    
    public void PrintUnitNames(Team team)
    {
        int unitIndex = 0;
        foreach (Unit unit in team.Unit)
        {
            _view.WriteLine($"{unitIndex}: {unit.Name}");

            unitIndex++;
        }
    }
    
    public void PrintCombatResult(Unit attacker, Unit defender)
    {
        _view.WriteLine($"{attacker.Name} ({attacker.HP}) : {defender.Name} ({defender.HP})");
    }
}