using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class MoneyController : INotifyPropertyChanged
{
    private readonly SoundPlayer _moneySound;

    public Action<bool>? OnMoneyChanged;

    private int _money;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            OnPropertyChanged();
        }
    }

    public MoneyController(int money)
    {
        Money = money;

        string workingDirectory = Environment.CurrentDirectory;
        var _moneySoundPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "coin.wav");
        _moneySound = new SoundPlayer(_moneySoundPath);
    }

    public void MoneyLoss(int amount)
    {
        OnMoneyChanged?.Invoke(false);
        Money -= amount;
        _moneySound.Play();
    }

    public void MoneyGain(int amount)
    {
        OnMoneyChanged?.Invoke(true);
        Money += amount;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
