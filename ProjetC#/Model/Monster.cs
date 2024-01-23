namespace Game.Model;

class Monster : Character
{
    public Monster()
    {
        hp = 100;
        mana = 100;
        state = Character.enumState.nothing;
    }
}
