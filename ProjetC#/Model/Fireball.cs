namespace Game.Model;

public class Fireball : ASpell
{
    public Fireball()
    {
        SpellName = "Fireball";
        Damage = 35;
        ManaNeeded = 20;

    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (sender.ManaController.Mana >= ManaNeeded)
        {
            if (totem)
            {
                receiver.HealthController.HealthLoss(Damage * (float)1.5);
            }
            else
            {
                receiver.HealthController.HealthLoss(Damage);
            }
            sender.ManaController.ManaLoss(ManaNeeded);
        }
    }
}
