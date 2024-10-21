using Fire_Emblem_View;
using Fire_Emblem.Round;
using Fire_Emblem.RoundParticipants;
using Fire_Emblem.SkillModule;
using Fire_Emblem.SkillModule.Effects;
using Fire_Emblem.SkillModule.Skills;
using Fire_Emblem.UnitFolder;

namespace Fire_Emblem;

public class CombatExecutor
{
    private View _view;
    public Logs _logs;
    private bool _isGameInCourse = true;
    public bool _isPlayerOneTurn = true;
    public int _round = 1;

    public CombatExecutor(View view, Logs logs)
    {
        _view = view;
        _logs = logs;
    }

    public void StartGame(Team teamOne, Team teamTwo)
    {
        while (_isGameInCourse)
        {
            TurnManager turnManager = new TurnManager(_view);
            turnManager.PlayTurn(teamOne, teamTwo, this);
            _isGameInCourse = VerifyEndOfTheGame(teamOne, teamTwo);
            _isPlayerOneTurn = !_isPlayerOneTurn;
            _round++;
        }
    }
    
    public void ManageEndOfTheRound(CombatParticipants combatParticipants, CombatTeams teams, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        UnitStatRestorer unitStatRestorer = new UnitStatRestorer();
        unitStatRestorer.ManageUnitAttributes(combatParticipants);
        unitBonusPenaltyResetter.ResetBonusesAndPenalties(combatParticipants);

        CombatDeaths combatDeaths = new CombatDeaths();
        combatDeaths.HandleUnitDeath(teams, combatParticipants);
    }
    
    public void ExecuteRound(CombatParticipants combatParticipants, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        RoundSetUp roundSetUp = new RoundSetUp(_view, this);
        roundSetUp.SetOpponents(combatParticipants);

        BattleLogger battleLogger = new BattleLogger(_logs);
        battleLogger.RegisterBattleLogs(combatParticipants, this);

        SkillManager skillManager = new SkillManager();
        skillManager.UpdateRoundConditionsForBothTeams(combatParticipants, this);

        EffectManager effectManager = new EffectManager(_view);
        effectManager.ManageAllEffects(combatParticipants);

        UnitBonusPenaltyManager unitBonusPenaltyManager = new UnitBonusPenaltyManager();
        unitBonusPenaltyManager.ApplyBonusesAndPenalties(combatParticipants);

        ExecuteCombat(combatParticipants, unitBonusPenaltyResetter);
        roundSetUp.SetLastOpponent(combatParticipants);
    }
    
    private void ExecuteCombat(CombatParticipants combatParticipants, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        UnitBonusPenaltyManager unitBonusPenaltyManager = new UnitBonusPenaltyManager();
        FirstAttackManager firstAttackManager = new FirstAttackManager(unitBonusPenaltyManager);
        firstAttackManager.ApplyFirstAttackBonuses(combatParticipants);
        
        if (!ExecuteInitialAttacks(combatParticipants))
        {
            return;
        }
        
        if (!ExecuteFollowUpIfNeeded(combatParticipants, unitBonusPenaltyResetter))
        {
            return;
        }

        RoundPrints roundPrints = new RoundPrints(_view, this);
        roundPrints.PrintCombatResult(attacker, defender);
    }
    
    private bool ExecuteInitialAttacks(CombatParticipants combatParticipants)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        UnitBonusPenaltyManager unitBonusPenaltyManager = new UnitBonusPenaltyManager();
        UnitStatRestorer unitStatRestorer = new UnitStatRestorer();
        FirstAttackManager firstAttackManager = new FirstAttackManager(unitBonusPenaltyManager);
        
        ExecuteAttack(attacker, defender, AttackType.FirstAttack);
        CombatDeaths combatDeaths = new CombatDeaths();
        if (combatDeaths.CheckIfUnitIsDead(combatParticipants, _view, this))
        {
            firstAttackManager.ResetFirstAttackBonuses(attacker, defender, unitStatRestorer);
            return false;
        }

        ExecuteAttack(defender, attacker, AttackType.Normal);
        if (combatDeaths.CheckIfUnitIsDead(combatParticipants, _view, this))
        {
            firstAttackManager.ResetFirstAttackBonuses(attacker, defender, unitStatRestorer);
            return false;
        }

        firstAttackManager.ResetFirstAttackBonuses(attacker, defender, unitStatRestorer);
        return true;
    }

    private bool ExecuteFollowUpIfNeeded(CombatParticipants combatParticipants, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        Unit attacker = combatParticipants.Attacker;
        Unit defender = combatParticipants.Defender;

        CombatDeaths combatDeaths = new CombatDeaths();
        CombatDetails combatDetails = new CombatDetails();
        if (combatDetails.CanUnitDoFollowUp(attacker, defender))
        {
            ApplyFollowUpAttack(attacker, defender, unitBonusPenaltyResetter);
            return !combatDeaths.CheckIfUnitIsDead(combatParticipants, _view, this);
        }
        if (combatDetails.CanUnitDoFollowUp(defender, attacker))
        {
            ApplyFollowUpAttack(defender, attacker, unitBonusPenaltyResetter);
            return !combatDeaths.CheckIfUnitIsDead(combatParticipants, _view, this);
        }
        _view.WriteLine("Ninguna unidad puede hacer un follow up");
        return true;
    }
    
    private void ApplyFollowUpAttack(Unit attacker, Unit defender, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        UnitBonusPenaltyManager unitBonusPenaltyManager = new UnitBonusPenaltyManager();
        unitBonusPenaltyManager.ApplyFollowUpSkill(attacker);
        
        ExecuteAttack(attacker, defender, AttackType.FollowUp);
        HandleFollowUpBonusesAndPenalties(attacker, unitBonusPenaltyResetter);
    }
    
    private void HandleFollowUpBonusesAndPenalties(Unit attacker, UnitBonusPenaltyResetter unitBonusPenaltyResetter)
    {
        UnitStatRestorer unitStatRestorer = new UnitStatRestorer();
        
        unitStatRestorer.RestablishUnitAtributesForFollowUp(attacker);
        unitBonusPenaltyResetter.ResetUnitBonusesAndPenaltiesForFollowUp(attacker);
    }
    
    private void ExecuteAttack(Unit attacker, Unit defender, AttackType attackType)
    {
        CombatDetails combatDetails = new CombatDetails();
        int damage = combatDetails.CalculateDamageDependingOnWeapon(attacker, defender, attackType);
        defender.HP = Math.Max(0, defender.HP - damage);
        _view.WriteLine($"{attacker.Name} ataca a {defender.Name} con {damage} de daño");
    }

    private bool VerifyEndOfTheGame(Team teamOne, Team teamTwo)
    {
        if (teamOne.Unit.Count == 0)
        {
            _view.WriteLine("Player 2 ganó");
            return false;
        }
        if (teamTwo.Unit.Count == 0)
        {
            _view.WriteLine("Player 1 ganó");
            return false;
        }
        return true;
    }
}