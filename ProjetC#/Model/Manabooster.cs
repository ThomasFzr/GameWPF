namespace Game.Model
{
    public class ManaBooster : ASpell
    {
        public ManaBooster()
        {
            SpellName = "ManaBooster";
            SpellNumber = 4;
            ManaNeeded = 0;
            Description = "Donne 25 mana";

        }
        public override void Execute(Character sender, Character receiver)
        {
            sender.ManaController.ManaGain(25);
        }
    }
}
