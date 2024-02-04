using System;
using System.IO;
using System.Media;

namespace Game.Model;

public class Monster : Character
{
    private readonly SoundPlayer _monsterLevelUpSound;

    private int _monsterLevel = 1;
    public int MonsterLevel {
        get
        {
            return _monsterLevel;
        }
        set
        {
            _monsterLevel = value;
            OnPropertyChanged();
        }
    }
    public int monsterAttackLevel = 1;
    public Action<int>? MonsterIsDead;
    public bool IsDead { get; set; }

    public Monster()
    {
        HealthController = new(100);
        BloodController = new(100);
        Attacks.Add(new Dash());
        HealthController.OnDie += DeathManager;
        IsDead = false;

        string workingDirectory = Environment.CurrentDirectory;
        var _monsterLevelUpSoundPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "level-up.wav");
        _monsterLevelUpSound = new SoundPlayer(_monsterLevelUpSoundPath);
    }

    public void DeathManager()
    {
        IsDead = true;
        MonsterIsDead?.Invoke(MonsterLevel);
        _monsterLevelUpSound.Play();
        MonsterLevel++;
        HealthController.Hp = (100 + MonsterLevel * 10);
        BloodController.Blood = (100 + MonsterLevel * 10);
        switch (MonsterLevel)
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
