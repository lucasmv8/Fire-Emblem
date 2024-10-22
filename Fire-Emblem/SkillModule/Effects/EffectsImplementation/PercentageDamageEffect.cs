namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation
{
    public class PercentageDamageEffect : Effect
    {
        private readonly float _percentageChange;
        private readonly AttackType _attackType;
        private readonly TargetType _targetType;
        private readonly EffectType _effectType;

        public PercentageDamageEffect(float percentageChange, AttackType attackType, TargetType targetType, EffectType effectType)
        {
            _percentageChange = percentageChange;
            _attackType = attackType;
            _targetType = targetType;
            _effectType = effectType;
        }

        public override void Apply(Unit unit)
        {
            Unit targetUnit = GetTargetUnit(unit);

            switch (_attackType)
            {
                case AttackType.Normal:
                    ApplyPercentageChange(targetUnit, AttackType.Normal);
                    break;
                case AttackType.FirstAttack:
                    ApplyPercentageChange(targetUnit, AttackType.FirstAttack);
                    break;
                case AttackType.FollowUp:
                    ApplyPercentageChange(targetUnit, AttackType.FollowUp);
                    break;
                default:
                    throw new InvalidOperationException("Tipo de ataque no soportado.");
            }
        }

        private Unit GetTargetUnit(Unit unit)
        {
            return _targetType == TargetType.Self ? unit : unit.CurrentOpponent;
        }

        private void ApplyPercentageChange(Unit targetUnit, AttackType attackType)
        {
            switch (_effectType)
            {
                case EffectType.Penalty:
                    ApplyPenaltyPercentageChange(targetUnit, attackType);
                    break;
                case EffectType.Bonus:
                    Console.WriteLine("De tipo Bonus");
                    ApplyBonusPercentageChange(targetUnit, attackType);
                    break;
                default:
                    throw new InvalidOperationException("Tipo de efecto no soportado.");
            }
        }

        private void ApplyPenaltyPercentageChange(Unit targetUnit, AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Normal:
                    targetUnit.ExtraDamage.PercentagePenaltyNormalAttack -= _percentageChange;
                    break;
                case AttackType.FirstAttack:
                    targetUnit.ExtraDamage.PercentagePenaltyFirstAttack -= _percentageChange;
                    break;
                case AttackType.FollowUp:
                    targetUnit.ExtraDamage.PercentagePenaltyFollowUpAttack -= _percentageChange;
                    break;
            }
        }

        private void ApplyBonusPercentageChange(Unit targetUnit, AttackType attackType)
        {
            Console.WriteLine($"attackType {attackType}");
            switch (attackType)
            {
                case AttackType.Normal:
                    Console.WriteLine("De tipo Normal");
                    targetUnit.ExtraDamage.PercentageBonusNormalAttack += _percentageChange;
                    break;
                case AttackType.FirstAttack:
                    targetUnit.ExtraDamage.PercentageBonusFirstAttack += _percentageChange;
                    break;
                case AttackType.FollowUp:
                    targetUnit.ExtraDamage.PercentageBonusFollowUpAttack += _percentageChange;
                    break;
            }
        }
    }
}
