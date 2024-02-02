using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class MoneyController :INotifyPropertyChanged
{
    public Action<bool>? OnMoneyChanged;

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

    public MoneyController(int money)
    {
        Money = money;
    }

    public void MoneyLoss(int amount)
    {
        OnMoneyChanged?.Invoke(false);
        Money -= amount;
    }

    public void MoneyGain(int amount)
    {
        OnMoneyChanged?.Invoke(true);
        Money += amount;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
