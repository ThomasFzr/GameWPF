namespace Game.Model
{
    public class BloodBooster : AAttack
    {
        public BloodBooster()
        {
            AttackName = "BloodBooster";
            BloodNeeded = 0;

        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            sender.BloodController.BloodGain(25);
        }
    }
}
