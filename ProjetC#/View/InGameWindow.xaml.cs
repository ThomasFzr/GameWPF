using Game.Model;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class InGameWindow : Window
{
    private ShopWindow? shopWindow;

    public InGameWindow()
    {
        InitializeComponent();
        DataContext = GameManager.Instance;
        shopWindow = new ShopWindow(GameManager.Instance.Player);
        PlayerState.Instance.OnPlayerToPlay += PlayerTurn;

    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            PlayerState.Instance.OnClickedSpell?.Invoke(spellNumber);
            StateMachine.Instance.HandleRequestStateChangement(MonsterState.Instance);

        }

    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        shopWindow.ShowDialog();
    }

    private void PlayerTurn()
    {
        playerArrow.Visibility = playerArrow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        monsterArrow.Visibility = monsterArrow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
    }
}
