namespace Game.Model;

public class Fireball : AAttack
{
    public Fireball()
    {
        AttackName = "Fireball";
        Damage = 35;
        BloodNeeded = 20;

    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (sender.BloodController.Blood >= BloodNeeded)
        {
            if (totem)
            {
                receiver.HealthController.HealthLoss(Damage * (float)1.5);
            }
            else
            {
                receiver.HealthController.HealthLoss(Damage);
            }
            sender.BloodController.BloodLoss(BloodNeeded);
        }
    }
}
