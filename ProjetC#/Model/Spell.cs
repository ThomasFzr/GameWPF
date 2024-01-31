namespace Game.Model;

public abstract class ASpell
{
    public string? SpellName { get; set; }
    public int ManaNeeded { get; set; }
    public float Damage { get; set; }
    public virtual void Execute(Character sender, Character receiver, bool Totem=false) { }

}