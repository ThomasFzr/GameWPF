using Game.ViewModel;
using System.Windows;

namespace Game.View;

public partial class ShopWindow : Window
{
    public ShopViewModel ShopViewModel { get; private set; }

    public ShopWindow()
    {
        InitializeComponent();
        ShopViewModel = new ShopViewModel(this);
        DataContext = ShopViewModel;
        Closing += InGameWindow_Closing;
    }

    private void InGameWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}
