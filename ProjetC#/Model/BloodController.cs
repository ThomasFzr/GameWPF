using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class BloodController : INotifyPropertyChanged
{
    public Action? OnBloodChanged;

    private int _blood;
    public int Blood
    {
        get { return _blood; }
        set
        {
            _blood = value;
            OnPropertyChanged();
        }
    }

    public BloodController(int blood)
    {
        Blood = blood;
    }

    public void BloodLoss(int amount)
    {
        OnBloodChanged?.Invoke();
        Blood -= amount;
    }

    public bool IsBloodEmpty()
    {
        return Blood <= 0;
    }

    public void BloodGain(int amount)
    {
        OnBloodChanged?.Invoke();
        Blood += amount;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
