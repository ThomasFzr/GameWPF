namespace Game.Model;

public abstract class AAttack
{
    public string AttackName { get; protected set; } = "";
    public int BloodNeeded { get; protected set; }
    public float Damage { get; protected set; }
    public virtual void Execute(Character sender, Character receiver, bool Totem=false) { }

}