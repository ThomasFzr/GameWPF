namespace Game.Model;

public class ChainsawHurricane : AAttack
{
    public ChainsawHurricane()
    {
        AttackName = "HURRICANE";
        Damage = 70;
        BloodNeeded = 40;

    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (sender.BloodController?.Blood >= BloodNeeded)
        {
            if (totem)
            {
                receiver.HealthController?.HealthLoss(Damage * (float)1.5);
            }
            else
            {
                receiver.HealthController?.HealthLoss(Damage);
            }
            sender.BloodController?.BloodLoss(BloodNeeded);
        }
    }
}
