namespace Game.Model
{
    public class Fireball : ASpell
    {
        public Fireball()
        {
            SpellName = "Fireball";
            SpellNumber = 3;
            ManaNeeded = 20;
            Description = "Inflige - 35 HP à l'adversaire";

        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                receiver.HealthController.HealthLoss(35);
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
