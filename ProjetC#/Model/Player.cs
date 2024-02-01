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

        State = Character.EnumState.nothing;
        IsTotemActivated = false;

        Attacks.Add(new ChainsawSlash());
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
            OnYesClickedAction.Invoke(newAttack);
        }
        else
        {
            MessageBox.Show("Attaque ajouté à votre liste mais non équipé.");
        }
        OnPropertyChanged("AttacksEquipped");
    }
}
