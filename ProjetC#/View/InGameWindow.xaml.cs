using Game.ViewModel;
using System.Windows;
using System.Windows.Media.Animation;


namespace Game.View;

public partial class InGameWindow : Window
{
    public InGameWindow()
    {
        InitializeComponent();
        DataContext = new InGameViewModel((Storyboard)FindResource("monsterMoveAnimation"));

        Closing += InGameWindow_Closing;
    }

    private void InGameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
