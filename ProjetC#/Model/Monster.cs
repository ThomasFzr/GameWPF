namespace Game.Model;

public class Monster : Character
{
    public Monster()
    {
        Hp = 100;
        Mana = 100;
        State = Character.EnumState.nothing;
    }
}
