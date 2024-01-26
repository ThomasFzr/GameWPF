namespace Game.Model;

public abstract class ASpell
{
    public string? SpellName { get; set; }
    public int SpellNumber { get; set; }
    public int ManaNeeded { get; set; }
    public string? Description { get; set; }

    public virtual void Execute(Character sender, Character receiver) { }

}