using System;
using System.Collections.Generic;
using System.Windows;

namespace Game.Model;

public class Player : Character
{
    public MoneyController MoneyController { get; set; }
    public bool IsTotemActivated { get; set; }
    public Action<AAttack>? OnYesClickedAction;

    private List<AAttack> attacksEquipped = new();
    public List<AAttack> AttacksEquipped
    {
        get { return attacksEquipped; }
        set
        {
            attacksEquipped = value;
        }
    }

    public Player()
    {
        HealthController = new(200);
        BloodController = new(200);
        MoneyController = new(0);

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
            Attacks.Add(newAttack);
            MessageBox.Show("Attaque ajoutée à votre inventaire mais non équipé.");
            OnPropertyChanged("Attacks");

        }
        OnPropertyChanged("AttacksEquipped");
    }
}
