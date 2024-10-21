namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation
{
    public class PercentageDamageReductionEffect : Effect
    {
        private readonly float _reductionPercentage;
        private readonly AttackType _attackType;
        private readonly TargetType _targetType;

        public PercentageDamageReductionEffect(float reductionPercentage, AttackType attackType, TargetType targetType)
        {
            _reductionPercentage = reductionPercentage;
            _attackType = attackType;
            _targetType = targetType;
        }

        public override void Apply(Unit unit)
        {
            Unit targetUnit = GetTargetUnit(unit);

            switch (_attackType)
            {
                case AttackType.Normal:
                    targetUnit.ExtraDamage.PercentageNormalAttack -= _reductionPercentage;
                    break;
                case AttackType.FirstAttack:
                    targetUnit.ExtraDamage.PercentageFirstAttack -= _reductionPercentage;
                    break;
                case AttackType.FollowUp:
                    targetUnit.ExtraDamage.PercentageFollowUpAttack -= _reductionPercentage;
                    break;
                default:
                    throw new InvalidOperationException("Tipo de ataque no soportado.");
            }
        }

        private Unit GetTargetUnit(Unit unit)
        {
            return _targetType == TargetType.Self ? unit : unit.CurrentOpponent;
        }
    }
}