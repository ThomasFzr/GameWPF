using Game.Model;
using Game.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class ShopWindow : Window
{
    public ShopViewModel ShopViewModel { get; private set; }
    private Player Player { get; set; }
    private ASpell spellToAdd;

    public ShopWindow(Player player)
    {
        InitializeComponent();
        ShopViewModel = new();
        DataContext = ShopViewModel;
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
            ASpell spellToBuy = ShopViewModel.Shop.SpellOnSale.Find(s => s.SpellName == spellName);

            if (spellToBuy != null)
            {
                if (ShopViewModel.Shop.Buy(Player, spellToBuy))
                {
                    (sender as Button).Visibility = Visibility.Collapsed;
                }
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

    private void BuyTotemButton_Click(object sender, RoutedEventArgs e)
    {
        if (Player.MoneyController.Money >= 5000)
        {
            Player.IsTotemActivated = true;
            (sender as Button).Visibility = Visibility.Collapsed;
            ShopViewModel.Shop.OnBuyDamageBooster.Invoke();
            Player.MoneyController.Money -= 5000;
        }

    }
}
