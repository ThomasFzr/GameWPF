namespace Game.Model
{
    public class ManaBooster : ASpell
    {
        public ManaBooster()
        {
            SpellName = "ManaBooster";
            ManaNeeded = 0;

        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            sender.ManaController.ManaGain(25);
        }
    }
}
