namespace Game.Model;

public abstract class ASpell
{
    public string? SpellName { get; set; }
    public int SpellNumber { get; set; }
    public int ManaNeeded { get; set; }
    public string? Description { get; set; }

    public virtual void Execute(Character sender, Character receiver) { }

}

class Heal : ASpell
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
        if (sender.Mana >= ManaNeeded)
        {
            sender.Hp += 25;
            sender.Mana -= ManaNeeded;
        }
    }
}

class Freeze : ASpell
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
        if (sender.Mana >= ManaNeeded)
        {
            receiver.State = Character.EnumState.freeze;
            sender.Mana -= ManaNeeded;
        }
    }
}

class Fireball : ASpell
{
    public Fireball()
    {
        SpellName = "Fireball";
        SpellNumber = 3;
        ManaNeeded = 20;
        Description = "Inflige - 35 HP à l'adversaire";

    }
    public override void Execute(Character sender, Character receiver)
    {
        if (sender.Mana >= ManaNeeded)
        {
            receiver.Hp -= 35;
            sender.Mana -= ManaNeeded;
        }
    }
}

class ManaBooster : ASpell
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
        sender.Mana += 25;
    }
}

class Kamehameha : ASpell
{
    public Kamehameha()
    {
        SpellName = "Kamehameha ";
        SpellNumber = 5;
        ManaNeeded = 30;
        Description = "Inflige - 50 HP à l'adversaire";

    }
    public override void Execute(Character sender, Character receiver)
    {
        if (sender.Mana >= ManaNeeded)
        {
            receiver.Hp -= 50;
            sender.Mana -= ManaNeeded;
        }
    }
}

class GumGumPistol : ASpell
{
    public GumGumPistol()
    {
        SpellName = "GumGumPistol";
        SpellNumber = 6;
        ManaNeeded = 50;
        Description = "Inflige - 50 HP à l'adversaire";

    }
    public override void Execute(Character sender, Character receiver)
    {
        if (sender.Mana >= ManaNeeded)
        {
            receiver.Hp -= 50;
            sender.Mana -= ManaNeeded;
        }
    }
}