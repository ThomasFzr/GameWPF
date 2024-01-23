namespace Game.Model
{
    abstract class ASpell
    {
        public string? spellName;
        public int spellNumber;
        public int manaNeeded;
        public string? description;

        public abstract void Execute(Character sender, Character receiver);

    }

    class Heal : ASpell
    {
        public Heal()
        {
            spellName = "Heal";
            spellNumber = 1;
            manaNeeded = 15;
            description = "Heal 15 HP";
        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.mana >= manaNeeded)
            {
                sender.hp += 25;
                sender.mana -= manaNeeded;
            }
        }
    }

    class Freeze : ASpell
    {
        public Freeze()
        {
            spellName = "Freeze";
            spellNumber = 2;
            manaNeeded = 20;
            description = "Freeze l'adversaire pendant un tour";
        }

        public override void Execute(Character sender, Character receiver)
        {
            if (sender.mana >= manaNeeded)
            {
                receiver.state = Character.enumState.freeze;
                sender.mana -= manaNeeded;
            }
        }
    }

    class Fireball : ASpell
    {
        public Fireball()
        {
            spellName = "Fireball";
            spellNumber = 3;
            manaNeeded = 20;
            description = "Inflige - 35 HP à l'adversaire";

        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.mana >= manaNeeded)
            {
                receiver.hp -= 35;
                sender.mana -= manaNeeded;
            }
        }
    }

    class ManaBooster : ASpell
    {
        public ManaBooster()
        {
            spellName = "ManaBooster";
            spellNumber = 4;
            manaNeeded = 0;
            description = "Donne 25 mana";

        }
        public override void Execute(Character sender, Character receiver)
        {
            sender.mana += 25;
        }
    }

    class Kamehameha : ASpell
    {
        public Kamehameha()
        {
            spellName = "Kamehameha ";
            spellNumber = 5;
            manaNeeded = 30;
            description = "Inflige - 50 HP à l'adversaire";

        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.mana >= manaNeeded)
            {
                receiver.hp -= 50;
                sender.mana -= manaNeeded;
            }
        }
    }

    class GumGumPistol : ASpell
    {
        public GumGumPistol()
        {
            spellName = "GumGumPistol";
            spellNumber = 6;
            manaNeeded = 50;
            description = "Inflige - 50 HP à l'adversaire";

        }
        public override void Execute(Character sender, Character receiver)
        {
            if (sender.mana >= manaNeeded)
            {
                receiver.hp -= 50;
                sender.mana -= manaNeeded;
            }
        }
    }
}
