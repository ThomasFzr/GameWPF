using Game.Model;
using Game.ViewModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game.View;

public partial class ShopWindow : Window
{
    public ShopViewModel ShopViewModel { get; private set; }
    private Player Player { get; set; }
    private AAttack attackToAdd;

    public ShopWindow(Player player)
    {
        InitializeComponent();
        ShopViewModel = new();
        DataContext = ShopViewModel;
        Player = player;
        Player.OnYesClickedAction += EnableDialogBox;
        GameManager.Instance.Player.MoneyController.OnMoneyChanged += ChangeMoneyColorPlayer;
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void BuyAttackButton_Click(object sender, RoutedEventArgs e)
    {
        string attackName = (sender as Button)?.Tag?.ToString();

        if (!string.IsNullOrEmpty(attackName))
        {
            AAttack attackToBuy = ShopViewModel.Shop.AttacksOnSale.Find(s => s.AttackName == attackName);

            if (attackToBuy != null)
            {
                if (ShopViewModel.Shop.Buy(Player, attackToBuy))
                {
                    (sender as Button).Visibility = Visibility.Collapsed;
                }
            }
        }
    }

    private void EnableDialogBox(AAttack attack)
    {
        InputBox.Visibility = Visibility.Visible;
        attackToAdd = attack;
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
            string selectedAttackName = checkedRadioButton.Content.ToString();
            int index = Player.AttacksEquipped.FindIndex(attack => attack.AttackName == selectedAttackName);
            Player.AttacksEquipped[index] = attackToAdd;
            Player.OnPropertyChanged("AttacksEquipped");
        }

    }

    private void BuyTotemButton_Click(object sender, RoutedEventArgs e)
    {
        const int priceTotem = 5000;
        if (Player.MoneyController.Money >= priceTotem)
        {
            Player.IsTotemActivated = true;
            (sender as Button).Visibility = Visibility.Collapsed;
            ShopViewModel.Shop.OnBuyDamageBooster.Invoke();
            Player.MoneyController.MoneyLoss(priceTotem);
        }

    }

    private void ChangeMoneyColorPlayer(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {

            MoneyPlayer.Foreground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                MoneyPlayer.Foreground = Brushes.White;
            });
        });
    }
    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
    }
}
