using Game.Model;
using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    private SoundPlayer attackSoundMonster;
    private Storyboard monsterMoveAnimation;
    private bool isInventoryOpen;
    private Player Player { get; set; }
    private Monster Monster { get; set; }

    public InGameWindow()
    {
        InitializeComponent();
        InitializeTimer();

        DataContext = GameManager.Instance;
        Player = GameManager.Instance.Player;
        Monster = GameManager.Instance.Monster;
        shopWindow = new ShopWindow(Player);

        PlayerState.Instance.OnArrowToShow += PlayerTurn;
        PlayerState.Instance.OnBtnToShow += SwitchBtn;
        MonsterState.Instance.OnMonsterAttack += AdvanceMonster;

        shopWindow.ShopViewModel.Shop.OnBuyAttack += ShowBtnWhenBuy;
        shopWindow.ShopViewModel.Shop.OnBuyDamageBooster += ShowPochitaDmgBooster;

        Player.HealthController.OnHealthChanged += ChangeHpColorPlayer;
        Player.HealthController.IsDead += ShowDeath;
        Monster.HealthController.OnHealthChanged += ChangeHpColorMonster;
        Player.MoneyController.OnMoneyChanged += ChangeMoneyColorPlayer;

        string workingDirectory = Environment.CurrentDirectory;
        var attackSoundPlayerPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\music\\metal.wav";
        attackSoundPlayer = new SoundPlayer(attackSoundPlayerPath);
        var attackSoundMonsterPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\music\\monsterattack.wav";
        attackSoundMonster = new SoundPlayer(attackSoundMonsterPath);
        monsterMoveAnimation = (Storyboard)FindResource("monsterMoveAnimation");

        backgroundMusic = new MediaElement();
        var inGameSoundPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\music\\background.mp3";
        backgroundMusic.Source = new Uri(inGameSoundPath);
        backgroundMusic.MediaEnded += BackgroundMusic_MediaEnded;
        backgroundMusic.LoadedBehavior = MediaState.Manual;
        backgroundMusic.UnloadedBehavior = MediaState.Manual;
        backgroundMusic.Play();
        currentImageIndex = 0;
        isInventoryOpen = false;
        timer?.Start();

        Closing += InGameWindow_Closing;

    }

    public void AdvanceMonster(int attackNumber)
    {
        Dispatcher.Invoke(() =>
        {
            BubbleMonster.Visibility = Visibility.Visible;
            AttackMonster.Text = Monster.Attacks[attackNumber].AttackName;
            attackSoundMonster.Play();
            monsterMoveAnimation.Begin();
            Task.Delay(1000).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    BubbleMonster.Visibility = Visibility.Collapsed;
                });
            });
        });
    }

    private void BackgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
    {
        backgroundMusic.Position = TimeSpan.Zero;
        backgroundMusic.Play();
    }

    private void InitializeTimer()
    {
        timer = new()
        {
            Interval = TimeSpan.FromSeconds(0.2)
        };
        timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            animatedImage.Source = new BitmapImage(new Uri(imagePaths[currentImageIndex], UriKind.RelativeOrAbsolute));

            currentImageIndex = (currentImageIndex + 1) % imagePaths.Length;

            if (currentImageIndex == 0)
            {
                timer.Stop();
            }
        });
    }

    private void AttackButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse((sender as Button)?.Tag?.ToString(), out int attackNumber))
        {
            if (isInventoryOpen && Player.Attacks.Count != 0)
            {
                SwapAttackInventory(attackNumber);
            }
            else
            {
                attackSoundPlayer.Play();

                currentImageIndex = 0;
                timer.Start();
                BubbleDenji.Visibility = Visibility.Visible;
                AttackDenji.Text = Player.AttacksEquipped[attackNumber].AttackName;
                Task.Delay(1000).ContinueWith(t =>
                {
                    PlayerState.Instance.OnClickedAttack?.Invoke(attackNumber);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BubbleDenji.Visibility = Visibility.Collapsed;
                        if (!Monster.IsDead && Monster.BloodController?.Blood > 0)
                        {
                            StateMachine.Instance.HandleRequestStateChangement(MonsterState.Instance);
                        }
                        else
                        {
                            StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
                            Monster.IsDead = false;
                        }
                    });
                });
                this.SwitchBtn();
            }

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

    private void SwitchBtn()
    {
        btn1.IsEnabled = btn1.IsEnabled != true;
        btn2.IsEnabled = btn2.IsEnabled != true;
        btn3.IsEnabled = btn3.IsEnabled != true;
        btn4.IsEnabled = btn4.IsEnabled != true;
        if (btn1.Visibility == Visibility.Visible)
        {
            if (Player.BloodController?.Blood < Player.AttacksEquipped[0].BloodNeeded)
            {
                btn1.IsEnabled = false;
            }
        }
        if (btn2.Visibility == Visibility.Visible)
        {
            if (Player.BloodController?.Blood < Player.AttacksEquipped[1]?.BloodNeeded)
            {
                btn2.IsEnabled = false;
            }
        }
        if (btn3.Visibility == Visibility.Visible)
        {
            if (Player.BloodController?.Blood < Player.AttacksEquipped[2]?.BloodNeeded)
            {
                btn3.IsEnabled = false;
            }
        }
        if (btn4.Visibility == Visibility.Visible)
        {
            if (Player.BloodController?.Blood < Player.AttacksEquipped[3]?.BloodNeeded)
            {
                btn4.IsEnabled = false;
            }
        }
    }

    private void ShowBtnWhenBuy()
    {
        btn2.Visibility = btn2.Content == null ? Visibility.Collapsed : Visibility.Visible;
        btn3.Visibility = btn3.Content == null ? Visibility.Collapsed : Visibility.Visible;
        btn4.Visibility = btn4.Content == null ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ShowPochitaDmgBooster()
    {
        pochitaDmgBooster.Visibility = Visibility.Visible;
    }

    private void ChangeHpColorPlayer(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            HpPlayer.Foreground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                HpPlayer.Foreground = Brushes.White;
            });
        });

    }
    private void ChangeMoneyColorPlayer(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {

            MoneyPlayer.Foreground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                MoneyPlayer.Foreground = Brushes.White;
            });
        });
    }

    private void ChangeHpColorMonster(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            HpMonster.Foreground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    HpMonster.Foreground = Brushes.White;
                });
            });
    }

    private void InGameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Inventory_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Visibility = InputBox.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        isInventoryOpen = isInventoryOpen == false;
    }

    private void SwapAttackInventory(int attackNbr)
    {
        AAttack temp = Player.Attacks[0];
        Player.Attacks[0] = Player.AttacksEquipped[attackNbr];
        Player.OnPropertyChanged("Attacks");
        Player.AttacksEquipped[attackNbr] = temp;
        Player.OnPropertyChanged("AttacksEquipped");
    }

    private void ShowDeath()
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            deathView.Visibility = Visibility.Visible;
        });
    }

    private void QuitterBtn_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
