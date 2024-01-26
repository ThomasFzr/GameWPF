using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Character : INotifyPropertyChanged
{
   public  HealthController? HealthController { get;protected set; }
   public ManaController? ManaController { get; protected set; }



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
