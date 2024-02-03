namespace Game.Model
{
    public class ChainsawBlaster : AAttack
    {
        public ChainsawBlaster()
        {
            AttackName = "BLASTER";
            Damage = 50;
            BloodNeeded = 30;

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
}
