namespace Game.Model
{
    public class Heal : AAttack
    {
        public Heal()
        {
            AttackName = "Heal";
            BloodNeeded = 15;
        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            if (sender.BloodController.Blood >= BloodNeeded)
            {
                sender.HealthController.HealthGain(25);
                sender.BloodController.BloodLoss(BloodNeeded);
            }
        }
    }
}
