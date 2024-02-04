using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Game.Model;

public class Player : Character
{
    const int HP = 200;
    const int BLOOD = 200;
    const int MONEY = 0;
    public MoneyController MoneyController { get; set; }
    public bool IsTotemActivated { get; set; }
    public Action<AAttack>? OnYesClickedAction;

    private ObservableCollection<AAttack> _attacksUnequipped = new();
    public ObservableCollection<AAttack> AttacksUnequipped
    {
        get { return _attacksUnequipped; }
        set
        {
            _attacksUnequipped = value;
        }
    }

    public Player()
    {
        HealthController = new(HP);
        BloodController = new(BLOOD);
        MoneyController = new(MONEY);

        IsTotemActivated = false;
        AttacksEquipped.Add(new ChainsawSlash());
    }

    public void EquipNewAttack(AAttack newAttack)
    {
        if (AttacksEquipped.Count < 4)
        {
            AttacksEquipped.Add(newAttack);
        }
        else if (MessageBox.Show("Vous avez déjà le nombre maximum d'attaque équipée, voulez-vous quand même l'équiper? ",
        "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            OnYesClickedAction?.Invoke(newAttack);
        }
        else
        {
            AttacksUnequipped.Add(newAttack);
            MessageBox.Show("Attaque ajoutée à votre inventaire mais non équipé.");
        }
    }
}
