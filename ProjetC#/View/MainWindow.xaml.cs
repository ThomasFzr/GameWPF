using Game.ViewModel;
using System;
using System.Windows;

namespace Game.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
        backgroundVideo.Position = TimeSpan.Zero;
        backgroundVideo.Play();
    }

    private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
    {
        backgroundVideo.Position = TimeSpan.Zero;
        backgroundVideo.Play();
    }
}
