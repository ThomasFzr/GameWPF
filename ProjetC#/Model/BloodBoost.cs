namespace Game.Model
{
    public class BloodBoost : AAttack
    {
        public BloodBoost()
        {
            AttackName = "BLOODBOOST";
            BloodNeeded = 0;

        }
        public override void Execute(Character sender, Character receiver, bool totem)
        {
            sender.BloodController.BloodGain(25);
        }
    }
}
