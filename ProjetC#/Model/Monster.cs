namespace Game.Model;

public class Monster : Character
{
    private int monsterLevel = 1;

    public Monster()
    {
        HealthController = new(100);
        ManaController = new(100);
        State = Character.EnumState.nothing;
        Spells.Add(new Heal());
        Spells.Add(new Fireball());
        //HealthController.OnHealthChanged += DeathManager;
    }

    //public void DeathManager()
    //{
    //    if (HealthController.IsDead())
    //    {
    //        monsterLevel++;
    //        HealthController.Hp = (100 + monsterLevel * 10);
    //        ManaController.Mana = (100 + monsterLevel * 10);
    //    }
    //}

}
