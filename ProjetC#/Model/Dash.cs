namespace Game.Model;

public class Dash : AAttack
{
    public Dash()
    {
        AttackName = "DASH";
        Damage = 30;
        BloodNeeded = 15;
    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (sender.BloodController?.Blood >= BloodNeeded)
        {
            receiver.HealthController?.HealthLoss(Damage);
            sender.BloodController?.BloodLoss(BloodNeeded);
        }
    }
}
