using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class HealthController : INotifyPropertyChanged
{
    public Action? OnHealthChanged;

    private int hp;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            OnPropertyChanged();
        }
    }

    public HealthController(int hp)
    {
        Hp = hp;
    }
    public bool IsDead() 
    {
        return hp <= 0;
    }

    public void HealthLoss(int amount)
    {
        OnHealthChanged?.Invoke();
        Hp -= amount;
    }

    public void HealthGain(int amount)
    {
        OnHealthChanged?.Invoke();
        Hp += amount;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
