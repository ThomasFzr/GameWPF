using Game.View;
using System.Windows;
using System.Windows.Input;

namespace Game.ViewModel;

public class MainViewModel
{
    private InGameWindow? inGameWindow;
    public ICommand JouerCommand { get; }
    public ICommand QuitterCommand { get; }

    public MainViewModel()
    {
        JouerCommand = new RelayCommand(OnJouer);
        QuitterCommand = new RelayCommand(OnQuitter);
    }

    private void OnJouer(object? parameter)
    {
        inGameWindow = new();
        inGameWindow.Show();
        Application.Current.MainWindow?.Close();
        ((App)Application.Current).StartGame();
    }

    private void OnQuitter(object? parameter)
    {
        Application.Current.MainWindow?.Close();
    }
}
