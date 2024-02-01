using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media;
using System.IO;

namespace Game.View;

public partial class MainWindow : Window
{
    private InGameWindow? inGameWindow;
    private MediaPlayer mediaPlayer;

    public MainWindow()
    {
        InitializeComponent();
        InitializeMediaPlayer().Wait();
        PlayAudio();
    }

    private async Task InitializeMediaPlayer()
    {
        mediaPlayer = new MediaPlayer();

        string workingDirectory = Environment.CurrentDirectory;
        var audioFilePath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\music\\menu.mp3";

        mediaPlayer.Open(new Uri(audioFilePath, UriKind.RelativeOrAbsolute));

        mediaPlayer.Volume = 0.5;

        mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
    }

    private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
    {
        MessageBox.Show($"Échec du chargement du média : {e.ErrorException.Message}");
    }

    private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
    {
        backgroundVideo.Position = TimeSpan.Zero;
        backgroundVideo.Play();
    }

    private void PlayAudio()
    {
        mediaPlayer.Play();
    }

    private void JouerButton_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Stop();
        inGameWindow = new();
        inGameWindow.Show();
        Close();
        ((App)Application.Current).StartGame();
    }

    private void QuitterButton_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Stop();
        Close();
    }
}
