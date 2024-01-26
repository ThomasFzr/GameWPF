using Game.ViewModel;
using System.Windows;

namespace Game.View;

public partial class ShopWindow : Window
{
    private ShopViewModel? shopViewModel;

    public ShopWindow()
    {
        InitializeComponent();
        shopViewModel = new ();
        DataContext = shopViewModel;
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
