using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Character : INotifyPropertyChanged
{
    public HealthController? HealthController { get; protected set; }
    public BloodController? BloodController { get; protected set; }

    private ObservableCollection<AAttack> _attacksEquipped = new();
    public ObservableCollection<AAttack> AttacksEquipped
    {
        get { return _attacksEquipped; }
        set
        {
            _attacksEquipped = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
