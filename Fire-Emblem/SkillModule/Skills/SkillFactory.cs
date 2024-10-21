using Fire_Emblem.SkillModule.Effects;
using Fire_Emblem.SkillModule.Conditions;

namespace Fire_Emblem.SkillModule.Skills;

public class SkillFactory
{
    private SkillApplier _skillApplier = new SkillApplier();
    private EffectFactory _effectFactory = new EffectFactory();
    private ConditionFactory _conditionFactory = new ConditionFactory();
    private Logs _logs;

    public SkillFactory(Logs logs)
    {
        _logs = logs;
    }

    public void AddConditionAndEffectToSkill(Skill skill, Unit unit)
    {
        if (_logs == null)
        {
            throw new InvalidOperationException("Logs no está inicializado.");
        }
        string skillName = skill.Name;
        //Console.WriteLine($"SkillName: {skillName}");
        switch (skillName)
        {
            case "HP +15":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("AlterBaseStatsEffect"));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Fair Fight":
            {
                var unitStartCondition = _conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs);
                var unitAtkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var rivalAtkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Opponent);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { unitAtkBonusEffect, rivalAtkBonusEffect });
                skill.SetCondition(unitStartCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Will to Win":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 50f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Atk, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Single-Minded":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("RecentOpponentCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Atk, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Ignis":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("FirstAttackEffect", StatType.Atk, (int)Math.Floor(unit.Atk * 0.5), EffectType.Bonus, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Perceptive":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 12, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, unit.Spd / 4, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                var startCombatCondition = _conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs);
                skill.SetCondition(startCombatCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Tome Precision":
            {
                var magicCondition = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Magic);
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                skill.SetCondition(magicCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Attack +6":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Speed +5":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Spd, 5, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Defense +5":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Def, 5, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Wrath":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, Math.Min(unit.MaxHP - unit.HP, 30), TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, Math.Min(unit.MaxHP - unit.HP, 30), TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Resolve":
            {
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 7, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 7, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { defBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 75f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Resistance +5":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Res, 5, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Atk/Def +5":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 5, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, defBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Atk/Res +5":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 5, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Spd/Res +5":
            {
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 5, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { spdBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Deadly Blade":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 8, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 8, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                var swordCondition = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword);
                var startCondition = _conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs);
                var startWithSwordCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit, new List<Condition> { swordCondition, startCondition });
                skill.SetCondition(startWithSwordCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Death Blow":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Atk, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Armored Blow":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Def, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Darting Blow":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Spd, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Warding Blow":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Res, 8, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Swift Sparrow":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sturdy Blow":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, defBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Mirror Strike":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Steady Blow":
            {
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { spdBonusEffect, defBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Swift Strike":
            {
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { spdBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Bracing Blow":
            {
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { defBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Atk/Spd":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, spdBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Atk/Def":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, defBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Atk/Res":
            {
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Spd/Def":
            {
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 10, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { spdBonusEffect, defBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Spd/Res":
            {
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 10, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { spdBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Brazen Def/Res":
            {
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 10, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { defBonusEffect, resBonusEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("HPinARangeCondition", unit, 80f, ComparisonType.LessThanOrEqual, TargetType.Self));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Fire Boost": 
            {
                skill.SetCondition(_conditionFactory.CreateCondition("StatComparationCondition", unit, StatType.HP, StatType.HP, 3, ComparisonType.GreaterThanOrEqual));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Wind Boost":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("StatComparationCondition", unit, StatType.HP, StatType.HP, 3,
                    ComparisonType.GreaterThanOrEqual));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Earth Boost":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("StatComparationCondition", unit, StatType.HP, StatType.HP, 3,
                    ComparisonType.GreaterThanOrEqual));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Water Boost":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("StatComparationCondition", unit, StatType.HP, StatType.HP, 3,
                    ComparisonType.GreaterThanOrEqual));
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Chaos Style":
            {
                var startCombatCondition = _conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs);
                var unitUseSword = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword);
                var unitUseBow = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Bow);
                var unitUseAxe = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Axe);
                var unitUseLance = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Lance);
                
                var unitUseMagic = _conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Magic);
                
                var opponentUseMagicCondition = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Magic);
                
                var opponentUseSword = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Sword);
                var opponentUseBow = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Bow);
                var opponentUseAxe = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Axe);
                var opponentUseLance = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Lance);
                
                var opponentUsePhysicalWeaponCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit,
                    new List<Condition> { opponentUseSword, opponentUseBow, opponentUseAxe, opponentUseLance });
                
                var unitUsePhysicalWeaponCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit,
                    new List<Condition> { unitUseSword, unitUseBow, unitUseAxe, unitUseLance });
                
                var physicalVsMagicCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit,
                    new List<Condition> { unitUsePhysicalWeaponCondition, opponentUseMagicCondition });
                
                var magicVsPhysicalCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit,
                    new List<Condition> { unitUseMagic, opponentUsePhysicalWeaponCondition });
                
                var chaosStyleCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit,
                    new List<Condition> { physicalVsMagicCondition, magicVsPhysicalCondition });
                
                var finalCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit,
                    new List<Condition> { startCombatCondition, chaosStyleCondition });
                skill.SetCondition(finalCondition);
                skill.SetEffect(_effectFactory.CreateEffect("BonusEffect", StatType.Spd, 3, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Blinding Flash":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("UnitStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 4, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Not *Quite*":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 4, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Stunning Smile":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("TheOpponentIsAManCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 8, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Disarming Sigh":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("TheOpponentIsAManCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 8, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Charmer":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("RecentOpponentCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 3, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                
                skill.SetCondition(_conditionFactory.CreateCondition("RecentOpponentCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 3, TargetType.Opponent));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Luna":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("LunaEffect"));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Belief in Love":
            {
                var opponentStartCondition = _conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs);
                var hp100Condition = _conditionFactory.CreateCondition("HPinARangeCondition", unit, 100f, ComparisonType.Equal, TargetType.Opponent);
                var multiCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit, new List<Condition> { opponentStartCondition, hp100Condition });
                var atkPenaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 5, TargetType.Opponent);
                var defPenaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 5, TargetType.Opponent);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { atkPenaltyEffect, defPenaltyEffect });
                skill.SetCondition(multiCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Beorc's Blessing":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Agnea's Arrow":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Penalty,StatType.Atk, StatType.Def, StatType.Res, StatType.Spd));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Soulblade":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword));
                skill.SetEffect(_effectFactory.CreateEffect("SoulbladeEffect"));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sandstorm":  //Implementarla
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("SandstormEffect"));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sword Agility":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 12, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lance Power":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Lance));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sword Power":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Bow Focus":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Bow));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lance Agility":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 12, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Lance));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Axe Power":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Axe));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Bow Agility":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 12, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 6, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Bow));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sword Focus":
            {
                var bonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 10, TargetType.Self);
                var penaltyEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 10, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusEffect, penaltyEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("TheUnitUseAnSpecificWeaponCondition", unit, WeaponType.Sword));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Close Def":
            {
                var opponentStartCondition = _conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs);
                var weaponConditionSword = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Sword);
                var weaponConditionLance = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Lance);
                var weaponConditionAxe = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Axe);
                var weaponConditionOr = _conditionFactory.CreateCondition("MultiConditionOr", unit, new List<Condition> { weaponConditionSword, weaponConditionLance, weaponConditionAxe });
                var finalCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit, new List<Condition> { opponentStartCondition, weaponConditionOr });
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 8, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 8, TargetType.Self);
                var neutralizeBonusEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { defBonusEffect, resBonusEffect, neutralizeBonusEffect });
                skill.SetCondition(finalCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Distant Def":
            {
                var weaponConditionMagic = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Magic);
                var weaponConditionBow = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Bow);
                var opponentStartCondition = _conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs);
                var weaponMultiCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit, new List<Condition> { weaponConditionMagic, weaponConditionBow });
                var finalCondition = _conditionFactory.CreateCondition("MultiConditionAnd", unit, new List<Condition> { opponentStartCondition, weaponMultiCondition });
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 8, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 8, TargetType.Self);
                var neutralizeBonusEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { defBonusEffect, resBonusEffect, neutralizeBonusEffect });
                skill.SetCondition(finalCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Atk/Spd":
            {
                var penaltyAtkEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 3, TargetType.Opponent);
                var penaltySpdEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Spd);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltyAtkEffect, penaltySpdEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Atk/Def":
            {
                var penaltyAtkEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 3, TargetType.Opponent);
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltyAtkEffect, penaltyDefEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Atk/Res":
            {
                var penaltyAtkEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 3, TargetType.Opponent);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Res);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltyAtkEffect, penaltyResEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Spd/Def":
            {
                var penaltySpdEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 3, TargetType.Opponent);
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Spd, StatType.Def);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltySpdEffect, penaltyDefEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Spd/Res":
            {
                var penaltySpdEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 3, TargetType.Opponent);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Spd, StatType.Res);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltySpdEffect, penaltyResEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lull Def/Res":
            {
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 3, TargetType.Opponent);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 3, TargetType.Opponent);
                var neutralizationEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Def, StatType.Res);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { penaltyDefEffect, penaltyResEffect, neutralizationEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }

            case "Fort. Def/Res":
            {
                var bonusDefEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var bonusResEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var penaltyAtkEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 2, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusDefEffect, bonusResEffect, penaltyAtkEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Life and Death":
            {
                var bonusAtkEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var bonusSpdEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 5, TargetType.Self);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusAtkEffect, bonusSpdEffect, penaltyDefEffect, penaltyResEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Solid Ground":
            {
                var bonusAtkEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var bonusDefEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusAtkEffect, bonusDefEffect, penaltyResEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Still Water":
            {
                var bonusAtkEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var bonusResEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 5, TargetType.Self);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect> { bonusAtkEffect, bonusResEffect, penaltyDefEffect });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Dragonskin":
            {
                var opponentStartsCombatCondition = _conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs);
                var opponentHpCondition = _conditionFactory.CreateCondition("HPinARangeCondition", unit, 75f, ComparisonType.GreaterThanOrEqual, TargetType.Opponent);
                var multiCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit, new List<Condition> { opponentStartsCombatCondition, opponentHpCondition });
                var atkBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Atk, 6, TargetType.Self);
                var spdBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Spd, 6, TargetType.Self);
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 6, TargetType.Self);
                var neutralizeBonusEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect>
                {
                    atkBonusEffect, spdBonusEffect, defBonusEffect, 
                    resBonusEffect, neutralizeBonusEffect
                });
                skill.SetCondition(multiCondition);
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Light and Dark":
            {
                var penaltyAtkEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Atk, 5, TargetType.Opponent);
                var penaltySpdEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Spd, 5, TargetType.Opponent);
                var penaltyDefEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Def, 5, TargetType.Opponent);
                var penaltyResEffect = _effectFactory.CreateEffect("PenaltyEffect", StatType.Res, 5, TargetType.Opponent);
                var neutralizeBonusEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Bonus, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd);
                var neutralizePenaltyEffect = _effectFactory.CreateEffect("NeutralizationEffect", NeutralizationType.Penalty, StatType.Atk, StatType.Def, StatType.Res, StatType.Spd);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect>
                {
                    penaltyAtkEffect, penaltySpdEffect, penaltyDefEffect, penaltyResEffect,
                    neutralizeBonusEffect, neutralizePenaltyEffect
                });
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Golden Lotus":
            {
                var opponentUseSword = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Sword);
                var opponentUseBow = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Bow);
                var opponentUseAxe = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Axe);
                var opponentUseLance = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Lance);
                var opponentUsePhysicalWeaponCondition = _conditionFactory.CreateCondition("MultiConditionOr", unit,
                    new List<Condition> { opponentUseSword, opponentUseBow, opponentUseAxe, opponentUseLance });
                skill.SetCondition(opponentUsePhysicalWeaponCondition);
                var percentageDamageReductionEffect = _effectFactory.CreateEffect("PercentageDamageReductionEffect", 0.5f, AttackType.FirstAttack, TargetType.Opponent);
                skill.SetEffect((percentageDamageReductionEffect));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Gentility":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Bow Guard":
            {
                var opponentUseBow = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Bow);
                skill.SetCondition(opponentUseBow);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Arms Shield":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WeaponAdvantage", unit, _logs));
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 7, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Axe Guard":
            {
                
                var opponentUseAxe = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Axe);
                skill.SetCondition(opponentUseAxe);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Magic Guard":
            {
                var weaponConditionMagic = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Magic);
                skill.SetCondition(weaponConditionMagic);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Lance Guard":
            {
                var weaponConditionLance = _conditionFactory.CreateCondition("TheOpponentUseAnSpecificWeaponCondition", unit, WeaponType.Lance);
                skill.SetCondition(weaponConditionLance);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Sympathetic":
            {
                var opponentStartCombat = _conditionFactory.CreateCondition("TheOpponentStartTheCombatCondition", unit, _logs);
                var hpCondition = _conditionFactory.CreateCondition("HPinARangeCondition", unit, 50f,
                    ComparisonType.LessThanOrEqual, TargetType.Self);
                var multiConditionAnd = _conditionFactory.CreateCondition("MultiConditionAnd", unit, new List<Condition> { opponentStartCombat, hpCondition });
                skill.SetCondition(multiConditionAnd);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                skill.SetEffect(opponentExtraDamage);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Bravery":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                skill.SetEffect(_effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal, TargetType.Self));
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Blue Skies":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                var unitExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Self);
                var opponentExtraDamage = _effectFactory.CreateEffect("ExtraDamageEffect", 5, AttackType.Normal,
                    TargetType.Opponent);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect>
                {
                    unitExtraDamage, opponentExtraDamage
                });
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
            case "Aegis Shield":
            {
                skill.SetCondition(_conditionFactory.CreateCondition("WithoutCondition", unit));
                var defBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Def, 6, TargetType.Self);
                var resBonusEffect = _effectFactory.CreateEffect("BonusEffect", StatType.Res, 3, TargetType.Self);
                var percentageDamageReductionEffect = _effectFactory.CreateEffect("PercentageDamageReductionEffect", 0.5f, AttackType.FirstAttack, TargetType.Opponent);
                var multiEffect = _effectFactory.CreateEffect("MultiEffect", new List<Effect>
                {
                    defBonusEffect, resBonusEffect, percentageDamageReductionEffect
                });
                skill.SetEffect(multiEffect);
                _skillApplier.ApplySkillToUnit(skill, unit);
                break;
            }
        }
    }
}