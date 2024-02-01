using System;
using System.Collections.Generic;

namespace Game.Model;

public class Monster : Character
{
    private int monsterLevel = 1;
    public int monsterAttackLevel = 1;
    public Action<int> MonsterIsDead;
    public bool IsDead { get; set; }

    private int maxHp = new();
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
            OnPropertyChanged();
        }
    }

    public Monster()
    {
        HealthController = new(100);
        MaxHp = (int)HealthController.Hp;
        BloodController = new(100);
        State = Character.EnumState.nothing;
        Attacks.Add(new Fireball());
        HealthController.IsDead += DeathManager;
        IsDead = false;
    }

    public void DeathManager()
    {
        IsDead = true;
        MonsterIsDead.Invoke(monsterLevel);
        monsterLevel++;
        HealthController.Hp = (100 + monsterLevel * 10);
        MaxHp = (int)HealthController.Hp;
        BloodController.Blood = (100 + monsterLevel * 10);
        switch (monsterLevel)
        {
            case 2:
                Attacks.Add(new Heal());
                monsterAttackLevel++;
                break;

            case 5:
                Attacks.Add(new ChainsawHurricane());
                monsterAttackLevel++;
                break;
        }
    }

}
