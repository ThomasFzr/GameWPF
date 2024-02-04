namespace Game.Model;

public class Heal : AAttack
{
    public Heal()
    {
        AttackName = "HEAL";
        BloodNeeded = 15;
        Damage = 0;
    }
    public override void Execute(Character sender, Character receiver, bool totem)
    {
        if (sender.BloodController?.Blood >= BloodNeeded)
        {
            sender.HealthController?.HealthGain(25);
            sender.BloodController?.BloodLoss(BloodNeeded);
        }
    }
}
