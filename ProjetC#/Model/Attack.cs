namespace Game.Model;

public abstract class AAttack
{
    public string? AttackName { get; set; }
    public int BloodNeeded { get; set; }
    public float Damage { get; set; }
    public virtual void Execute(Character sender, Character receiver, bool Totem=false) { }

}