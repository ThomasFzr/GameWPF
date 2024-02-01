namespace Game.Model;

public class Punch : ASpell
{
    public Punch()
    {
        SpellName = "Punch";
        Damage = 30;
        ManaNeeded = 0;
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
