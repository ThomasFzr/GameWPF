namespace Game.Model
{
    public class Heal : ASpell
    {
        public Heal()
        {
            SpellName = "Heal";
            SpellNumber = 1;
            ManaNeeded = 15;
            Description = "Heal 15 HP";
        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                sender.HealthController.HealthGain(25);
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
