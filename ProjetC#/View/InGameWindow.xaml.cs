using Game.Model;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Game.View;

public partial class InGameWindow : Window
{
    private ShopWindow? shopWindow;
    private string[] imagePaths = { "/photos/1.png", "/photos/2.png", "/photos/3.png", "/photos/3.png" };
    private int currentImageIndex = 0;
    private DispatcherTimer timer;
    private MediaElement backgroundMusic;
    private SoundPlayer attackSoundPlayer;

    public InGameWindow()
    {
        InitializeComponent();
        InitializeTimer();
        DataContext = GameManager.Instance;
        shopWindow = new ShopWindow(GameManager.Instance.Player);
        PlayerState.Instance.OnPlayerToPlay += PlayerTurn;


        Storyboard monsterAnimation = (Storyboard)this.Resources["monsterAnimation"];
        monsterAnimation.Begin();

        attackSoundPlayer = new SoundPlayer("C:\\Users\\thoma\\source\\repos\\ProjetC#\\ProjetC#\\music\\metal.wav");
        attackSoundPlayer.Load();

        backgroundMusic = new MediaElement();
        backgroundMusic.Source = new Uri("D:\\jeu\\ProjetC#\\music\\background.mp3");
        backgroundMusic.MediaEnded += BackgroundMusic_MediaEnded;
        backgroundMusic.LoadedBehavior = MediaState.Manual;
        backgroundMusic.UnloadedBehavior = MediaState.Manual;
        backgroundMusic.Play();
        currentImageIndex = 0;
        timer.Start();


    }


    private void BackgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
    {
        backgroundMusic.Position = TimeSpan.Zero;
        backgroundMusic.Play();
    }

    private void InitializeTimer()
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(0.2);
        timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        animatedImage.Source = new BitmapImage(new Uri(imagePaths[currentImageIndex], UriKind.RelativeOrAbsolute));

        currentImageIndex = (currentImageIndex + 1) % imagePaths.Length;

        if (currentImageIndex == 0)
        {
            timer.Stop();
        }
    }

    private void SpellButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int spellNumber))
        {
            PlayerState.Instance.OnClickedSpell?.Invoke(spellNumber);
            StateMachine.Instance.HandleRequestStateChangement(MonsterState.Instance);

        }

    }

    private void ShopButton_Click(object sender, RoutedEventArgs e)
    {
        shopWindow.ShowDialog();
    }

    private void PlayerTurn()
    {
        playerArrow.Visibility = playerArrow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        monsterArrow.Visibility = monsterArrow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
    }
}
