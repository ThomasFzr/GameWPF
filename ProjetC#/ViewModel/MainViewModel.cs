using Game.View;
using System.Windows;
using System.Windows.Input;

namespace Game.ViewModel;

public class MainViewModel
{
    private InGameWindow? _inGameWindow;
    public ICommand JouerCommand { get; }
    public ICommand QuitterCommand { get; }

    public MainViewModel()
    {
        JouerCommand = new RelayCommand(OnJouer);
        QuitterCommand = new RelayCommand(OnQuitter);
    }

    private void OnJouer(object? parameter)
    {
        _inGameWindow = new();
        _inGameWindow.Show();
        Application.Current.MainWindow?.Close();
        ((App)Application.Current).StartGame();
    }

    private void OnQuitter(object? parameter)
    {
        Application.Current.MainWindow?.Close();
    }
}
