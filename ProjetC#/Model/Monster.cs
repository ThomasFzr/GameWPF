namespace Game.Model;

public class Monster : Character
{
    public Monster()
    {
        HealthController = new(100);
        ManaController = new(100);
        State = Character.EnumState.nothing;
        Spells.Add(new Heal());
        Spells.Add(new Fireball());
    }

   
}
