using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Shop : INotifyPropertyChanged
{
    int price;
    private Action<ASpell>? OnSpellAdded;

    private List<ASpell> spellOnSale = new();
    public List<ASpell> SpellOnSale
    {
        get { return spellOnSale; }
        set
        {
            spellOnSale = value;
            OnPropertyChanged();
        }
    }

    public void Buy(Player buyer, ASpell spell) {
        buyer.Spells.Add(spell);
        OnSpellAdded -= buyer.EquipNewSpell;
        OnSpellAdded += buyer.EquipNewSpell;
        OnSpellAdded.Invoke(spell);
        SpellOnSale.Remove(spell);
    }

    public Shop()
    {
        SpellOnSale.Add(new ManaBooster());
        SpellOnSale.Add(new Kamehameha());
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
