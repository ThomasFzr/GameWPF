using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class HealthController : INotifyPropertyChanged
{
    public Action? OnHealthChanged;
    public Action? IsDead;

    private float hp;
    public float Hp
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

    public void HealthLoss(float amount)
    {
        Hp -= amount;
        OnHealthChanged?.Invoke();

        if (Hp <= 0)
        {
            IsDead?.Invoke();
        }
    }

    public void HealthGain(int amount)
    {
        Hp += amount;
        OnHealthChanged?.Invoke();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
