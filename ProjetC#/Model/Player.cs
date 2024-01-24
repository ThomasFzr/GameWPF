namespace Game.Model;

public class Player : Character
{
    private int money;
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            OnPropertyChanged();
        }
    }

    int totemEquiped;
    int damageBooster;


    public Player()
    {
        Hp = 100;
        Mana = 200;
        Money = 10000000;
        State = Character.EnumState.nothing;
        Spells.Add(new Heal());
        Spells.Add(new Fireball());
        Spells.Add(new ManaBooster());
    }
}
