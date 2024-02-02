namespace Game.Model;

public class Dash : AAttack
{
    public Dash()
    {
        AttackName = "Dash";
        Damage = 30;
        BloodNeeded = 0;
    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        receiver.HealthController.HealthLoss(Damage);
    }
}
