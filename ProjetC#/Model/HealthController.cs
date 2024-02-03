using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class HealthController : INotifyPropertyChanged
{
    public Action<bool>? OnHealthChanged;
    public Action? OnDie;

    private float _hp;
    public float Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
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
        OnHealthChanged?.Invoke(false);

        if (Hp <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void HealthGain(int amount)
    {
        Hp += amount;
        OnHealthChanged?.Invoke(true);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
