using System;
using System.Collections.Generic;
using System.Windows;

namespace Game.Model;

public class Player : Character
{
    public MoneyController MoneyController { get; set; }
    public bool IsTotemActivated { get; set; }
    public Action<ASpell>? OnYesClickedAction;

    private List<ASpell> spellsEquipped = new();
    public List<ASpell> SpellsEquipped
    {
        get { return spellsEquipped; }
        set
        {
            spellsEquipped = value;
        }
    }

    public Player()
    {
        HealthController = new(200);
        ManaController = new(200);
        MoneyController = new(0);

        State = Character.EnumState.nothing;
        IsTotemActivated = false;

        Spells.Add(new Punch());
        SpellsEquipped.Add(new Punch());
    }

    public void EquipNewSpell(ASpell newSpell)
    {
        if (SpellsEquipped.Count < 4)
        {
            SpellsEquipped.Add(newSpell);
        }
        else if (MessageBox.Show("Vous avez déjà le nombre maximum de sort équipé, voulez-vous quand même l'équiper? ",
        "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            OnYesClickedAction.Invoke(newSpell);
        }
        else
        {
            MessageBox.Show("Sort ajouté à votre liste mais non équipé.");
        }
        OnPropertyChanged("SpellsEquipped");
    }
}
