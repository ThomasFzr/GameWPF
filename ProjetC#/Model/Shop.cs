using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Shop : INotifyPropertyChanged
{
    private Action<ASpell>? OnSpellAdded;
    public Action? OnBuySpell;
    public Action? OnBuyDamageBooster;

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

    public bool Buy(Player buyer, ASpell spell) {
        if(buyer.MoneyController.Money >= 1000)
        {
            buyer.Spells.Add(spell);
            OnSpellAdded -= buyer.EquipNewSpell;
            OnSpellAdded += buyer.EquipNewSpell;
            OnSpellAdded.Invoke(spell);
            SpellOnSale.Remove(spell);
            buyer.MoneyController.Money-=1000;
            OnBuySpell?.Invoke();
            return true;
        }
        return false;
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
