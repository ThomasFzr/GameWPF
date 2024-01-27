using System;

namespace Game.Model;

public class Monster : Character
{
    private int monsterLevel = 1;
    public int monsterSpellLevel = 1;
    public Action<int> MonsterIsDead;

    public Monster()
    {
        HealthController = new(100);
        ManaController = new(100);
        State = Character.EnumState.nothing;
        Spells.Add(new Fireball());
        HealthController.IsDead += DeathManager;
    }

    public void DeathManager()
    {

        MonsterIsDead.Invoke(monsterLevel);
        monsterLevel++;
        HealthController.Hp = (100 + monsterLevel * 10);
        ManaController.Mana = (100 + monsterLevel * 10);
        switch (monsterLevel)
        {
            case 2:
                Spells.Add(new Heal());
                monsterSpellLevel++;
                break;

            case 5:
                Spells.Add(new Kamehameha());
                monsterSpellLevel++;
                break;
        }
    }

}
