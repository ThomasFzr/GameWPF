using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Character : INotifyPropertyChanged
{
    public HealthController? HealthController { get; protected set; }
    public BloodController? BloodController { get; protected set; }

    private ObservableCollection<AAttack> _attacks = new();
    public ObservableCollection<AAttack> Attacks
    {
        get { return _attacks; }
        set
        {
            _attacks = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
