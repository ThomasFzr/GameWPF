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
        GameManager.Instance.TurnManager.PlayerState.OnPlayerToPlay += PlayerTurn;

    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            GameManager.Instance.TurnManager.PlayerState.OnClickedSpell?.Invoke(spellNumber);
            StateMachine.Instance.HandleRequestStateChangement(new MonsterState(GameManager.Instance.Player, GameManager.Instance.Monster));

        }

    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        shopWindow.ShowDialog();
    }

    private void PlayerTurn()
    {
        if (playerArrow.Visibility == Visibility.Collapsed)
        {
            playerArrow.Visibility = Visibility.Visible;
            monsterArrow.Visibility = Visibility.Collapsed;
        }
        else if (playerArrow.Visibility == Visibility.Visible)
        {
            playerArrow.Visibility = Visibility.Collapsed;
            monsterArrow.Visibility = Visibility.Visible;
        }
    }
}
