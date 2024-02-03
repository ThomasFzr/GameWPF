using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Shop : INotifyPropertyChanged
{
    private Action<AAttack>? _OnAttackAdded;
    public Action? OnBuyAttack;
    public Action? OnBuyDamageBooster;

    private List<AAttack> _attacksOnSale = new();
    public List<AAttack> AttacksOnSale
    {
        get { return _attacksOnSale; }
        set
        {
            _attacksOnSale = value;
            OnPropertyChanged();
        }
    }

    public Shop()
    {
        AttacksOnSale.Add(new Heal());
        AttacksOnSale.Add(new BloodBoost());
        AttacksOnSale.Add(new ChainsawBlaster());
        AttacksOnSale.Add(new ChainsawHurricane());
    }

    public bool Buy(Player buyer, AAttack attack) {
        if(buyer.MoneyController.Money >= 1000)
        {
            _OnAttackAdded -= buyer.EquipNewAttack;
            _OnAttackAdded += buyer.EquipNewAttack;
            _OnAttackAdded.Invoke(attack);
            AttacksOnSale.Remove(attack);
            buyer.MoneyController.MoneyLoss(1000);
            OnBuyAttack?.Invoke();
            return true;
        }
        return false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
