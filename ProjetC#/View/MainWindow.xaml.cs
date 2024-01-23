using Game.ViewModel;
using System.Windows;

namespace Game.View;

public partial class MainWindow : Window
{
    private InGameWindow inGameWindow;
    private InGameViewModel inGameViewModel;

    public MainWindow()
    {
        InitializeComponent();
        inGameWindow = new ();
        inGameViewModel = new (inGameWindow);
    }

    private void JouerButton_Click(object sender, RoutedEventArgs e)
    {
        inGameWindow.Show();
        Close();
        inGameViewModel.StartGame();
    }

    private void QuitterButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
