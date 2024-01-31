namespace Game.Model
{
    public class Heal : ASpell
    {
        public Heal()
        {
            SpellName = "Heal";
            ManaNeeded = 15;
        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                sender.HealthController.HealthGain(25);
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
