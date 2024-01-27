using Game.Model;
using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class InGameWindow : Window
{

    private TurnManager turnManager;
    private ShopWindow? ShopWindow;
    private PlayerState playerState;

    public InGameWindow()
    {
        InitializeComponent();

        turnManager = new();
        DataContext = turnManager;
        ShopWindow = new(turnManager.Player);

    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            playerState.OnClickedSpell.Invoke(spellNumber);
        }

    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        ShopWindow.Show();
    }
}
