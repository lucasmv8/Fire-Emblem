namespace Fire_Emblem.UnitFolder;

public class ExtraDamage
{
    public int BonusNormalAttack { get; set; }
    public int BonusFollowUpAttack { get; set; }
    public int BonusFirstAttack { get; set; }
    public int PenaltyNormalAttack { get; set; }
    public int PenaltyFollowUpAttack { get; set; }
    public int PenaltyFirstAttack { get; set; }
    public float PercentagePenaltyNormalAttack { get; set; }
    public float PercentagePenaltyFollowUpAttack { get; set; }
    public float PercentagePenaltyFirstAttack { get; set; }
    public float PercentageBonusNormalAttack { get; set; }
    public float PercentageBonusFollowUpAttack { get; set; }
    public float PercentageBonusFirstAttack { get; set; }

    public ExtraDamage(int normalAttack = 0, int followUpAttack = 0, int firstAttack = 0, int percentagePenalty = 1)
    {
        BonusNormalAttack = normalAttack;
        BonusFollowUpAttack = followUpAttack;
        BonusFirstAttack = firstAttack;
        PenaltyNormalAttack = normalAttack;
        PenaltyFollowUpAttack = followUpAttack;
        PenaltyFirstAttack = firstAttack;
        PercentagePenaltyNormalAttack = percentagePenalty;
        PercentagePenaltyFollowUpAttack = percentagePenalty;
        PercentagePenaltyFirstAttack = percentagePenalty;
        PercentageBonusNormalAttack = percentagePenalty;
        PercentageBonusFollowUpAttack = percentagePenalty;
        PercentageBonusFirstAttack = percentagePenalty;
    }
    
    public void Reset()
    {
        BonusNormalAttack = 0;
        BonusFollowUpAttack = 0;
        BonusFirstAttack = 0;
        PenaltyNormalAttack = 0;
        PenaltyFollowUpAttack = 0;
        PenaltyFirstAttack = 0;
        PercentagePenaltyNormalAttack = 1;
        PercentagePenaltyFollowUpAttack = 1;
        PercentagePenaltyFirstAttack = 1;
        PercentageBonusNormalAttack = 1;
        PercentageBonusFollowUpAttack = 1;
        PercentageBonusFirstAttack = 1;
    }
}