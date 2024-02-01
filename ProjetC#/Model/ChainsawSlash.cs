namespace Game.Model;

public class ChainsawSlash : AAttack
{
    public ChainsawSlash()
    {
        AttackName = "Slash";
        Damage = 30;
        BloodNeeded = 0;
    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (totem)
        {
            receiver.HealthController.HealthLoss(Damage * (float)1.5);
        }
        else
        {
            receiver.HealthController.HealthLoss(Damage);
        }
    }
}
