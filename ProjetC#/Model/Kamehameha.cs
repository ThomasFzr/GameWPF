namespace Game.Model
{
    public class Kamehameha : ASpell
    {
        public Kamehameha()
        {
            SpellName = "Kamehameha ";
            Damage = 50;
            ManaNeeded = 30;

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
}
