using Game.ViewModel;
using System.Windows;

namespace Game.View;

public partial class MainWindow : Window
{
    private InGameWindow? inGameWindow;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void JouerButton_Click(object sender, RoutedEventArgs e)
    {
        inGameWindow = new();
        inGameWindow.Show();
        Close();
    }

    private void QuitterButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
