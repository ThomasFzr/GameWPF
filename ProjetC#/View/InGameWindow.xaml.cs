using Game.Model;
using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class InGameWindow : Window
{

    private GameManager gameManager;
    private ShopWindow? shopWindow;

    public InGameWindow()
    {
        InitializeComponent();

        gameManager = ((App)Application.Current).gameManager;
        DataContext = gameManager;
        shopWindow = new ShopWindow(gameManager.Player);
        gameManager.TurnManager.PlayerState.OnPlayerToPlay += PlayerTurn;

    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            gameManager.TurnManager.PlayerState.OnClickedSpell?.Invoke(spellNumber);
        }

    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        shopWindow.Show();
    }

    private void PlayerTurn()
    {
        if(playerArrow.Visibility == Visibility.Collapsed)
        {
            playerArrow.Visibility = Visibility.Visible;
        }
        else if (playerArrow.Visibility == Visibility.Visible)
        {
            playerArrow.Visibility = Visibility.Collapsed;
        }
    }
}
