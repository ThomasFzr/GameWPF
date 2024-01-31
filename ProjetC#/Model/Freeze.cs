namespace Game.Model
{
    public class Freeze : ASpell
    {
        public Freeze()
        {
            SpellName = "Freeze";
            ManaNeeded = 20;
        }

        public override void Execute(Character sender, Character receiver, bool totem)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                receiver.State = Character.EnumState.freeze;
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
