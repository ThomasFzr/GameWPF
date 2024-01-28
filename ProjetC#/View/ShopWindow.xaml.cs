using Game.Model;
using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Game.View;

public partial class ShopWindow : Window
{
    private ShopViewModel shopViewModel;
    private Player Player { get;set; }
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
        int input;
        //if (int.TryParse(InputTextBox.Text, out input))
        //{
        //    Player.SpellsEquipped.RemoveAt(input-1);
        //}
        //InputTextBox.Text = String.Empty;
        Player.SpellsEquipped.Add(spellToAdd);
        Player.OnPropertyChanged("SpellsEquipped");
    }

    private void PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!int.TryParse(e.Text, out int result))
        {
            e.Handled = true;
            return;
        }

        // Validate that the input is 1, 2, 3, or 4
        if (result < 1 || result > 4 || ((TextBox)sender).Text.Length > 0)
        {
            e.Handled = true;
        }
    }
}
