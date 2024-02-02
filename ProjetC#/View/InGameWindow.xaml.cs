﻿using Game.Model;
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
    public InGameWindow()
    {
        InitializeComponent();
        InitializeTimer();
        DataContext = GameManager.Instance;
        shopWindow = new ShopWindow(GameManager.Instance.Player);
        PlayerState.Instance.OnArrowToShow += PlayerTurn;
        PlayerState.Instance.OnBtnToShow += SwitchBtn;
        shopWindow.ShopViewModel.Shop.OnBuyAttack += ShowBtnWhenBuy;
        shopWindow.ShopViewModel.Shop.OnBuyDamageBooster += ShowPochitaDmgBooster;
        GameManager.Instance.Player.HealthController.OnHealthChanged += ChangeHpColorPlayer;
        GameManager.Instance.Monster.HealthController.OnHealthChanged += ChangeHpColorMonster;
        GameManager.Instance.Player.MoneyController.OnMoneyChanged += ChangeMoneyColorPlayer;
        MonsterState.Instance.OnMonsterAttack += AdvanceMonster;

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
        timer.Start();

        Closing += InGameWindow_Closing;

    }

    public void AdvanceMonster()
    {
        attackSoundMonster.Play();

        Dispatcher.Invoke(() =>
        {
            monsterMoveAnimation.Begin();
        });
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
            attackSoundPlayer.Play();


            currentImageIndex = 0;
            timer.Start();
            BubbleDenji.Visibility = Visibility.Visible;
            AttackDenji.Text = GameManager.Instance.Player.AttacksEquipped[attackNumber].AttackName;
            Task.Delay(1000).ContinueWith(t =>
            {
                PlayerState.Instance.OnClickedAttack?.Invoke(attackNumber);
                App.Current.Dispatcher.Invoke(() =>
                {
                    BubbleDenji.Visibility = Visibility.Collapsed;
                    if (!GameManager.Instance.Monster.IsDead)
                    {
                        StateMachine.Instance.HandleRequestStateChangement(MonsterState.Instance);
                    }
                    else
                    {
                        StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
                        GameManager.Instance.Monster.IsDead = false;
                    }
                });
            });
            this.SwitchBtn();
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
        btn1.IsEnabled = btn1.IsEnabled == true ? false : true;
        btn2.IsEnabled = btn2.IsEnabled == true ? false : true;
        btn3.IsEnabled = btn3.IsEnabled == true ? false : true;
        btn4.IsEnabled = btn4.IsEnabled == true ? false : true;
        if (btn1.Visibility == Visibility.Visible)
        {
            if (GameManager.Instance.Player.BloodController.Blood < GameManager.Instance.Player.AttacksEquipped[0].BloodNeeded)
            {
                btn1.IsEnabled = false;
            }
        }
        if (btn2.Visibility == Visibility.Visible)
        {
            if (GameManager.Instance.Player.BloodController.Blood < GameManager.Instance.Player.AttacksEquipped[1]?.BloodNeeded)
            {
                btn2.IsEnabled = false;
            }
        }
        if (btn3.Visibility == Visibility.Visible)
        {
            if (GameManager.Instance.Player.BloodController.Blood < GameManager.Instance.Player.AttacksEquipped[2]?.BloodNeeded)
            {
                btn3.IsEnabled = false;
            }
        }
        if (btn4.Visibility == Visibility.Visible)
        {
            if (GameManager.Instance.Player.BloodController.Blood < GameManager.Instance.Player.AttacksEquipped[3]?.BloodNeeded)
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
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        InputBox.Visibility = Visibility.Collapsed;

        RadioButton checkedRadioButton = null;
        foreach (UIElement element in listChoice.Children)
        {
            if (element is RadioButton radioButton && radioButton.IsChecked == true)
            {
                checkedRadioButton = radioButton;
                break;
            }
        }

        if (checkedRadioButton != null)
        {
            string selectedAttackName = checkedRadioButton.Content.ToString();
            int index = GameManager.Instance.Player.Attacks.FindIndex(attack => attack.AttackName == selectedAttackName);
            GameManager.Instance.Player.Attacks.Add(GameManager.Instance.Player.AttacksEquipped[index]);
            GameManager.Instance.Player.OnPropertyChanged("Attacks");

            //GameManager.Instance.Player.AttacksEquipped[index] = GameManager.Instance.attackToAdd;
            GameManager.Instance.Player.OnPropertyChanged("AttacksEquipped");
        }
    }
}
