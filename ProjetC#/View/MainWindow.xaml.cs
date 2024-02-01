using System;
using System.Windows;

namespace Game.View;

public partial class MainWindow : Window
{
    private InGameWindow? inGameWindow;

    public MainWindow()
    {
        InitializeComponent();
        backgroundVideo.Position = TimeSpan.Zero;
        backgroundVideo.Play();
    }

    private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
    {
        backgroundVideo.Position = TimeSpan.Zero;
        backgroundVideo.Play();
    }

    private void JouerButton_Click(object sender, RoutedEventArgs e)
    {
        inGameWindow = new();
        inGameWindow.Show();
        Close();
        ((App)Application.Current).StartGame();
    }

    private void QuitterButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
