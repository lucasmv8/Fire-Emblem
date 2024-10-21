namespace Fire_Emblem.UnitFolder;

public class ExtraDamage
{
    public int BonusNormalAttack { get; set; }
    public int BonusFollowUpAttack { get; set; }
    public int BonusFirstAttack { get; set; }
    public int PenaltyNormalAttack { get; set; }
    public int PenaltyFollowUpAttack { get; set; }
    public int PenaltyFirstAttack { get; set; }
    public float PercentageNormalAttack { get; set; }
    public float PercentageFollowUpAttack { get; set; }
    public float PercentageFirstAttack { get; set; }

    public ExtraDamage(int normalAttack = 0, int followUpAttack = 0, int firstAttack = 0, int percentage = 1)
    {
        BonusNormalAttack = normalAttack;
        BonusFollowUpAttack = followUpAttack;
        BonusFirstAttack = firstAttack;
        PenaltyNormalAttack = normalAttack;
        PenaltyFollowUpAttack = followUpAttack;
        PenaltyFirstAttack = firstAttack;
        PercentageNormalAttack = percentage;
        PercentageFollowUpAttack = percentage;
        PercentageFirstAttack = percentage;
    }
    
    public void Reset()
    {
        BonusNormalAttack = 0;
        BonusFollowUpAttack = 0;
        BonusFirstAttack = 0;
        PenaltyNormalAttack = 0;
        PenaltyFollowUpAttack = 0;
        PenaltyFirstAttack = 0;
        PercentageNormalAttack = 1;
        PercentageFollowUpAttack = 1;
        PercentageFirstAttack = 1;
    }
}