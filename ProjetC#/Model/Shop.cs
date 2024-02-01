﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Model;

public class Shop : INotifyPropertyChanged
{
    private Action<AAttack>? OnAttackAdded;
    public Action? OnBuyAttack;
    public Action? OnBuyDamageBooster;

    private List<AAttack> attacksOnSale = new();
    public List<AAttack> AttacksOnSale
    {
        get { return attacksOnSale; }
        set
        {
            attacksOnSale = value;
            OnPropertyChanged();
        }
    }

    public bool Buy(Player buyer, AAttack attack) {
        if(buyer.MoneyController.Money >= 1000)
        {
            buyer.Attacks.Add(attack);
            OnAttackAdded -= buyer.EquipNewAttack;
            OnAttackAdded += buyer.EquipNewAttack;
            OnAttackAdded.Invoke(attack);
            AttacksOnSale.Remove(attack);
            buyer.MoneyController.Money-=1000;
            OnBuyAttack?.Invoke();
            return true;
        }
        return false;
    }

    public Shop()
    {
        AttacksOnSale.Add(new Heal());
        AttacksOnSale.Add(new ChainsawHurricane());
        AttacksOnSale.Add(new ChainsawBlaster());
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
