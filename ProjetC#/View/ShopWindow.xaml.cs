using Game.Model;
using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class ShopWindow : Window
{
    private ShopViewModel shopViewModel;
    private Player player;


    public ShopWindow(Player _player)
    {
        InitializeComponent();
        shopViewModel = new();
        DataContext = shopViewModel;
        player = _player;
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
                shopViewModel.Shop.Buy(player, spellToBuy);
                (sender as Button).Visibility = Visibility.Collapsed;
            }
        }
    }
}
