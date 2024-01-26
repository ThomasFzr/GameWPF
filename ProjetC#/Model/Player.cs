namespace Game.Model;

public class Player : Character
{
    public MoneyController MoneyController { get; set; }

    int totemEquiped;
    int damageBooster;

    public Player()
    {
        HealthController = new (100);
        ManaController = new (200);
        MoneyController = new(10);

        State = Character.EnumState.nothing;
        Spells.Add(new Heal());
        Spells.Add(new Fireball());     
        Spells.Add(new ManaBooster());
        Spells.Add(new ManaBooster());
    }
}
