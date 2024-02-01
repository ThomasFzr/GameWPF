namespace Game.Model
{
    public class ManaBooster : AAttack
    {
        public ManaBooster()
        {
            AttackName = "ManaBooster";
            BloodNeeded = 0;

        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            sender.BloodController.BloodGain(25);
        }
    }
}
