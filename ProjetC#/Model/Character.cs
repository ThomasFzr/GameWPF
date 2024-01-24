using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Character : INotifyPropertyChanged
{
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

    private EnumState state;
    public EnumState State
    {
        get { return state; }
        set
        {
            state = value;
            OnPropertyChanged();
        }
    }

    public enum EnumState
    {
        freeze,
        fire,
        nothing
    };

    private List<ASpell> spells = new();
    public List<ASpell> Spells
    {
        get { return spells; }
        set
        {
            spells = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
