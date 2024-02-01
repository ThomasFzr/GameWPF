using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Character : INotifyPropertyChanged
{
   public  HealthController? HealthController { get;protected set; }
   public BloodController? BloodController { get; protected set; }



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

    private List<AAttack> attacks = new();
    public List<AAttack> Attacks
    {
        get { return attacks; }
        set
        {
            attacks = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
