namespace Game.Model
{
    public class Kamehameha : ASpell
    {
        public Kamehameha()
        {
            SpellName = "Kamehameha ";
            SpellNumber = 5;
            ManaNeeded = 30;
            Description = "Inflige - 50 HP à l'adversaire";

        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                receiver.HealthController.HealthLoss(50);
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
