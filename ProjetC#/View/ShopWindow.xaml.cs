﻿using Game.Model;
using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Game.View;

public partial class ShopWindow : Window
{
    private ShopViewModel shopViewModel;
    private Player Player { get; set; }
    private ASpell spellToAdd;

    public ShopWindow(Player player)
    {
        InitializeComponent();
        shopViewModel = new();
        DataContext = shopViewModel;
        Player = player;
        Player.OnYesClickedAction += EnableDialogBox;

    }

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void BuySpellButton_Click(object sender, RoutedEventArgs e)
    {
        string spellName = (sender as Button)?.Tag?.ToString();

        if (!string.IsNullOrEmpty(spellName))
        {
            ASpell spellToBuy = shopViewModel.Shop.SpellOnSale.Find(s => s.SpellName == spellName);

            if (spellToBuy != null)
            {
                shopViewModel.Shop.Buy(Player, spellToBuy);
                (sender as Button).Visibility = Visibility.Collapsed;
            }
        }
    }

    private void EnableDialogBox(ASpell spell)
    {
        InputBox.Visibility = Visibility.Visible;
        spellToAdd = spell;
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Visibility = Visibility.Collapsed;

        RadioButton checkedRadioButton = null;
        foreach (UIElement element in listChoice.Children)
        {
            if (element is RadioButton radioButton && radioButton.IsChecked == true)
            {
                checkedRadioButton = radioButton;
                break;
            }
        }

        if (checkedRadioButton != null)
        {
            string selectedSpellName = checkedRadioButton.Content.ToString();
            int index = Player.SpellsEquipped.FindIndex(spell => spell.SpellName == selectedSpellName);
            Player.SpellsEquipped[index] = spellToAdd;
            Player.OnPropertyChanged("SpellsEquipped");
        }

    }
}
