using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class ManaController : INotifyPropertyChanged
{
    public Action? OnManaChanged;

    private int mana;
    public int Mana
    {
        get { return mana; }
        set
        {
            mana = value;
            OnPropertyChanged();
        }
    }

    public ManaController(int mana)
    {
        Mana = mana;
    }

    public void ManaLoss(int amount)
    {
        OnManaChanged?.Invoke();
        Mana -= amount;
    }

    public bool IsManaEmpty()
    {
        return Mana <= 0;
    }

    public void ManaGain(int amount)
    {
        OnManaChanged?.Invoke();
        Mana += amount;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
