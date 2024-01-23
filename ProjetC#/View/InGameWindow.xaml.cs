using System;
using System.Windows;
using System.Windows.Controls;

//TODO CODE BEHIND => VIEWMODEL
namespace Game.View;

public partial class InGameWindow : Window
{
    public Action<int>? OnClickedSpell;

    public InGameWindow()
    {
        InitializeComponent();
    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            OnClickedSpell?.Invoke(spellNumber);
        }
        
    }
}
