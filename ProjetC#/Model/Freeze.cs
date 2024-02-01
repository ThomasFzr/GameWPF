namespace Game.Model
{
    public class Freeze : AAttack
    {
        public Freeze()
        {
            AttackName = "Freeze";
            BloodNeeded = 20;
        }

        public override void Execute(Character sender, Character receiver, bool totem)
        {
            if (sender.BloodController.Blood >= BloodNeeded)
            {
                receiver.State = Character.EnumState.freeze;
                sender.BloodController.BloodLoss(BloodNeeded);
            }
        }
    }
}
