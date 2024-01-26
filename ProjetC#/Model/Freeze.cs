namespace Game.Model
{
    public class Freeze : ASpell
    {
        public Freeze()
        {
            SpellName = "Freeze";
            SpellNumber = 2;
            ManaNeeded = 20;
            Description = "Freeze l'adversaire pendant un tour";
        }

        public override void Execute(Character sender, Character receiver)
        {
            if (sender.ManaController.Mana >= ManaNeeded)
            {
                receiver.State = Character.EnumState.freeze;
                sender.ManaController.ManaLoss(ManaNeeded);
            }
        }
    }
}
