using Game.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Game.View;

public partial class InGameWindow : Window
{

    private InGameViewModel inGameViewModel;
    private ShopWindow? ShopWindow;
    public Action<int>? OnClickedSpell;

    public InGameWindow()
    {
        InitializeComponent();

        inGameViewModel = new(this);
        inGameViewModel.StartGame();
        DataContext = inGameViewModel;
    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            OnClickedSpell?.Invoke(spellNumber);
        }
        
    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        ShopWindow = new();
        ShopWindow.Show();
        Close();
    }
}
