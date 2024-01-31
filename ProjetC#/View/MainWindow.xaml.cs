using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

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

        Storyboard monsterAnimation = (Storyboard)this.Resources["monsterAnimation"];
        monsterAnimation.Begin();

        Storyboard monsterAnimationn = (Storyboard)this.Resources["monsterAnimationn"];
        monsterAnimationn.Begin();
    }

    private async Task InitializeMediaPlayer()
    {
        mediaPlayer = new MediaPlayer();

        var audioFilePath = "C:\\Users\\thoma\\source\\repos\\ProjetC#\\ProjetC#\\music\\menu.mp3";

        mediaPlayer.Open(new Uri(audioFilePath, UriKind.RelativeOrAbsolute));

        mediaPlayer.Volume = 0.5;

        mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
    }

    private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
    {
        MessageBox.Show($"Échec du chargement du média : {e.ErrorException.Message}");
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
