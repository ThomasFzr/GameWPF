namespace Game.Model;

abstract class Totem
{
    public string? totemName;
    public int totemPrice;
    public string? description;
    public abstract void Activated(Player player);
}

class DamageBooster : Totem
{
    public DamageBooster() {
        totemName = "Booster de dégats";
        totemPrice = 500;
        description = "Boost les dégats de 15%";
    }

    public override void Activated(Player player)
    {
        //player.damageBooster = 15;
    }
}
