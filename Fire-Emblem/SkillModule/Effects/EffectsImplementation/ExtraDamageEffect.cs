namespace Fire_Emblem.SkillModule.Effects.EffectsImplementation
{
    public class ExtraDamageEffect : Effect
    {
        private readonly int _extraDamage;
        private readonly AttackType _attackType;
        private readonly TargetType _targetType;

        public ExtraDamageEffect(int extraDamage, AttackType attackType, TargetType targetType)
        {
            _extraDamage = extraDamage;
            _attackType = attackType;
            _targetType = targetType;
        }

        public override void Apply(Unit unit)
        {
            ApplyExtraDamage(unit, _targetType);
        }

        private void ApplyExtraDamage(Unit unit, TargetType targetType)
        {
            if (targetType == TargetType.Self)
            {
                ApplyBonusDamage(unit);
            }
            else if (targetType == TargetType.Opponent)
            {
                ApplyPenaltyDamage(unit);
            }
        }

        private void ApplyBonusDamage(Unit unit)
        {
            switch (_attackType)
            {
                case AttackType.Normal:
                    unit.ExtraDamage.BonusNormalAttack += _extraDamage;
                    break;
                case AttackType.FirstAttack:
                    unit.ExtraDamage.BonusFirstAttack += _extraDamage;
                    break;
                case AttackType.FollowUp:
                    unit.ExtraDamage.BonusFollowUpAttack += _extraDamage;
                    break;
                default:
                    throw new InvalidOperationException("Tipo de ataque desconocido");
            }
        }

        private void ApplyPenaltyDamage(Unit unit)
        {
            switch (_attackType)
            {
                case AttackType.Normal:
                    unit.ExtraDamage.PenaltyNormalAttack += _extraDamage;
                    break;
                case AttackType.FirstAttack:
                    unit.ExtraDamage.PenaltyFirstAttack += _extraDamage;
                    break;
                case AttackType.FollowUp:
                    unit.ExtraDamage.PenaltyFollowUpAttack += _extraDamage;
                    break;
                default:
                    throw new InvalidOperationException("Tipo de ataque desconocido");
            }
        }
    }
}
