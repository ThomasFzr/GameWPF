using Game.Model;
using Game.View;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Game.ViewModel;

public class InGameViewModel : INotifyPropertyChanged
{
    public ICommand AttackCommand { get; }
    public ICommand OpenShopCommand { get; }
    public ICommand OpenInventoryCommand { get; }
    public ICommand QuitCommand { get; }


    private readonly ShopWindow? _shopWindow;
    private readonly string[] _imagePaths = { "/photos/1.png", "/photos/2.png", "/photos/3.png", "/photos/3.png" };
    private int _currentImageIndex = 0;
    private DispatcherTimer _timer;
    private readonly MediaElement _backgroundMusic;
    private readonly SoundPlayer _attackSoundPlayer;
    private readonly SoundPlayer _attackSoundMonster;
    private readonly SoundPlayer _gameOverSound;
    private readonly Storyboard _monsterMoveAnimation;
    private bool IsInventoryOpen
    {
        get
        {
            return InventoryVisibility == Visibility.Visible;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public Player Player { get; private set; }
    public Monster Monster { get; private set; }

    private Visibility _bubbleMonsterVisibility = Visibility.Collapsed;
    public Visibility BubbleMonsterVisibility
    {
        get { return _bubbleMonsterVisibility; }
        private set
        {
            if (_bubbleMonsterVisibility != value)
            {
                _bubbleMonsterVisibility = value;
                OnPropertyChanged(nameof(BubbleMonsterVisibility));
            }
        }
    }
    private Visibility _bubbleDenjiVisibility = Visibility.Collapsed;
    public Visibility BubbleDenjiVisibility
    {
        get { return _bubbleDenjiVisibility; }
        private set
        {
            if (_bubbleDenjiVisibility != value)
            {
                _bubbleDenjiVisibility = value;
                OnPropertyChanged(nameof(BubbleDenjiVisibility));
            }
        }
    }

    private Visibility _playerArrowVisibility = Visibility.Visible;
    public Visibility PlayerArrowVisibility
    {
        get { return _playerArrowVisibility; }
        private set
        {
            if (_playerArrowVisibility != value)
            {
                _playerArrowVisibility = value;
                OnPropertyChanged(nameof(PlayerArrowVisibility));
            }
        }
    }

    private Visibility _monsterArrowVisibility = Visibility.Collapsed;
    public Visibility MonsterArrowVisibility
    {
        get { return _monsterArrowVisibility; }
        private set
        {
            if (_monsterArrowVisibility != value)
            {
                _monsterArrowVisibility = value;
                OnPropertyChanged(nameof(MonsterArrowVisibility));
            }
        }
    }
    private Visibility _pochitaDmgBoosterVisibility = Visibility.Collapsed;
    public Visibility PochitaDmgBoosterVisibility
    {
        get { return _pochitaDmgBoosterVisibility; }
        private set
        {
            if (_pochitaDmgBoosterVisibility != value)
            {
                _pochitaDmgBoosterVisibility = value;
                OnPropertyChanged(nameof(PochitaDmgBoosterVisibility));
            }
        }
    }

    private Visibility _attack1BtnVisibility = Visibility.Visible;
    public Visibility Attack1BtnVisibility
    {
        get { return _attack1BtnVisibility; }
        private set
        {
            if (_attack1BtnVisibility != value)
            {
                _attack1BtnVisibility = value;
                OnPropertyChanged(nameof(Attack1BtnVisibility));
            }
        }
    }

    private bool _attack1IsEnabled = true;
    public bool Attack1IsEnabled
    {
        get { return _attack1IsEnabled; }
        private set
        {
            if (_attack1IsEnabled != value)
            {
                _attack1IsEnabled = value;
                OnPropertyChanged(nameof(Attack1IsEnabled));
            }
        }
    }

    private Visibility _attack2BtnVisibility = Visibility.Collapsed;
    public Visibility Attack2BtnVisibility
    {
        get { return _attack2BtnVisibility; }
        private set
        {
            if (_attack2BtnVisibility != value)
            {
                _attack2BtnVisibility = value;
                OnPropertyChanged(nameof(Attack2BtnVisibility));
            }
        }
    }
    private bool _attack2IsEnabled = true;
    public bool Attack2IsEnabled
    {
        get { return _attack2IsEnabled; }
        private set
        {
            if (_attack2IsEnabled != value)
            {
                _attack2IsEnabled = value;
                OnPropertyChanged(nameof(Attack2IsEnabled));
            }
        }
    }

    private Visibility _attack3BtnVisibility = Visibility.Collapsed;
    public Visibility Attack3BtnVisibility
    {
        get { return _attack3BtnVisibility; }
        private set
        {
            if (_attack3BtnVisibility != value)
            {
                _attack3BtnVisibility = value;
                OnPropertyChanged(nameof(Attack3BtnVisibility));
            }
        }
    }

    private bool _attack3IsEnabled = true;
    public bool Attack3IsEnabled
    {
        get { return _attack3IsEnabled; }
        private set
        {
            if (_attack3IsEnabled != value)
            {
                _attack3IsEnabled = value;
                OnPropertyChanged(nameof(Attack3IsEnabled));
            }
        }
    }

    private Visibility _attack4BtnVisibility = Visibility.Collapsed;
    public Visibility Attack4BtnVisibility
    {
        get { return _attack4BtnVisibility; }
        private set
        {
            if (_attack4BtnVisibility != value)
            {
                _attack4BtnVisibility = value;
                OnPropertyChanged(nameof(Attack4BtnVisibility));
            }
        }
    }

    private bool _attack4IsEnabled = true;
    public bool Attack4IsEnabled
    {
        get { return _attack4IsEnabled; }
        private set
        {
            if (_attack4IsEnabled != value)
            {
                _attack4IsEnabled = value;
                OnPropertyChanged(nameof(Attack4IsEnabled));
            }
        }
    }

    private Visibility _deathScreenVisibility = Visibility.Collapsed;
    public Visibility DeathScreenVisibility
    {
        get { return _deathScreenVisibility; }
        private set
        {
            if (_deathScreenVisibility != value)
            {
                _deathScreenVisibility = value;
                OnPropertyChanged(nameof(DeathScreenVisibility));
            }
        }
    }

    private Visibility _inventoryVisibility = Visibility.Collapsed;
    public Visibility InventoryVisibility
    {
        get { return _inventoryVisibility; }
        private set
        {
            if (_inventoryVisibility != value)
            {
                _inventoryVisibility = value;
                OnPropertyChanged(nameof(InventoryVisibility));
            }
        }
    }

    private Brush _hpMonsterForeground = Brushes.White;
    public Brush HpMonsterForeground
    {
        get { return _hpMonsterForeground; }
        private set
        {
            if (_hpMonsterForeground != value)
            {
                _hpMonsterForeground = value;
                OnPropertyChanged(nameof(HpMonsterForeground));
            }
        }
    }

    private Brush _hpPlayerForeground = Brushes.White;
    public Brush HpPlayerForeground
    {
        get { return _hpPlayerForeground; }
        private set
        {
            if (_hpPlayerForeground != value)
            {
                _hpPlayerForeground = value;
                OnPropertyChanged(nameof(HpPlayerForeground));
            }
        }
    }

    private Brush _moneyPlayerForeground = Brushes.White;
    public Brush MoneyPlayerForeground
    {
        get { return _moneyPlayerForeground; }
        private set
        {
            if (_moneyPlayerForeground != value)
            {
                _moneyPlayerForeground = value;
                OnPropertyChanged(nameof(MoneyPlayerForeground));
            }
        }
    }

    private string _attackMonsterText;
    public string AttackMonsterText
    {
        get { return _attackMonsterText; }
        private set
        {
            if (_attackMonsterText != value)
            {
                _attackMonsterText = value;
                OnPropertyChanged(nameof(AttackMonsterText));
            }
        }
    }

    private string _attackDenjiText;
    public string AttackDenjiText
    {
        get { return _attackDenjiText; }
        private set
        {
            if (_attackDenjiText != value)
            {
                _attackDenjiText = value;
                OnPropertyChanged(nameof(AttackDenjiText));
            }
        }
    }

    private ImageSource _animatedImageSource;
    public ImageSource AnimatedImageSource
    {
        get { return _animatedImageSource; }
        private set
        {
            if (_animatedImageSource != value)
            {
                _animatedImageSource = value;
                OnPropertyChanged(nameof(AnimatedImageSource));
            }
        }
    }

    private bool _isShopAllowed = true;
    public bool IsShopAllowed
    {
        get
        {
            return _isShopAllowed;
        }
        private set
        {
            _isShopAllowed = value;
            OnPropertyChanged();

        }
    }
    private bool _isInventoryAllowed = true;
    public bool IsInventoryAllowed
    {
        get
        {
            return _isInventoryAllowed;
        }
        private set
        {
            _isInventoryAllowed = value;
            OnPropertyChanged();

        }
    }

    public InGameViewModel(Storyboard monsterStoryboard)
    {
        InitializeTimer();

        Player = GameManager.Instance.Player;
        Monster = GameManager.Instance.Monster;
        _shopWindow = new ShopWindow();

        PlayerState.Instance.OnArrowToShow += PlayerTurn;
        PlayerState.Instance.OnBtnToShow += SwitchBtn;
        PlayerState.Instance.OnPlayerTurn += AllowInventory;
        MonsterState.Instance.OnMonsterAttack += AdvanceMonster;

        _shopWindow.ShopViewModel.Shop.OnBuyAttack += ShowBtnWhenBuy;
        _shopWindow.ShopViewModel.Shop.OnBuyDamageBooster += ShowPochitaDmgBooster;
        _shopWindow.ShopViewModel.OnShopClosed += SwitchBtn;

        Player.HealthController.OnHealthChanged += ChangeHpColorPlayer;
        Player.HealthController.OnDie += ShowDeath;
        Monster.HealthController.OnHealthChanged += ChangeHpColorMonster;
        Player.MoneyController.OnMoneyChanged += ChangeMoneyColorPlayer;

        string workingDirectory = Environment.CurrentDirectory;
        var attackSoundPlayerPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "metal.wav");
        _attackSoundPlayer = new SoundPlayer(attackSoundPlayerPath);
        var attackSoundMonsterPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "monsterattack.wav");
        _attackSoundMonster = new SoundPlayer(attackSoundMonsterPath);
        var gameOverSoundPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "fail.wav");
        _gameOverSound = new SoundPlayer(gameOverSoundPath);
        _monsterMoveAnimation = monsterStoryboard;

        _backgroundMusic = new MediaElement();
        var inGameSoundPath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "music", "background.mp3");
        _backgroundMusic.Source = new Uri(inGameSoundPath);
        _backgroundMusic.MediaEnded += BackgroundMusic_MediaEnded;
        _backgroundMusic.LoadedBehavior = MediaState.Manual;
        _backgroundMusic.UnloadedBehavior = MediaState.Manual;
        _backgroundMusic.Play();
        _timer?.Start();

        AttackCommand = new RelayCommand(OnAttackButton);
        OpenShopCommand = new RelayCommand(OnOpenShop);
        OpenInventoryCommand = new RelayCommand(OnInventory);
        QuitCommand = new RelayCommand(OnQuitGame);
    }

    private void AllowInventory()
    {
        IsInventoryAllowed = true;
    }

    public void AdvanceMonster(int attackNumber)
    {
        BubbleMonsterVisibility = Visibility.Visible;
        AttackMonsterText = Monster.Attacks[attackNumber].AttackName;
        _attackSoundMonster.Play();
        App.Current.Dispatcher.Invoke(() =>
        {
            _monsterMoveAnimation?.Begin();
            Task.Delay(1000).ContinueWith(t =>
            {
                BubbleMonsterVisibility = Visibility.Collapsed;
            });
        });
    }

    private void BackgroundMusic_MediaEnded(object sender, RoutedEventArgs e)
    {
        _backgroundMusic.Position = TimeSpan.Zero;
        _backgroundMusic.Play();
    }

    private void InitializeTimer()
    {
        _timer = new()
        {
            Interval = TimeSpan.FromSeconds(0.2)
        };
        _timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        AnimatedImageSource = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.RelativeOrAbsolute));

        _currentImageIndex = (_currentImageIndex + 1) % _imagePaths.Length;

        if (_currentImageIndex == 0)
        {
            _timer.Stop();
        }
    }

    private void OnAttackButton(object? parameter)
    {
        int attackNumber = int.Parse((string)parameter);
        if (IsInventoryOpen)
        {
            if (Player.Attacks.Count != 0)
            {
                SwapAttackInventory(attackNumber);
            }
        }
        else
        {
            IsInventoryAllowed = false;
            _attackSoundPlayer.Play();

            _currentImageIndex = 0;
            _timer.Start();
            BubbleDenjiVisibility = Visibility.Visible;
            AttackDenjiText = Player.AttacksEquipped[attackNumber].AttackName;
            Task.Delay(1000).ContinueWith(t =>
            {
                PlayerState.Instance.OnClickedAttack?.Invoke(attackNumber);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    BubbleDenjiVisibility = Visibility.Collapsed;
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
            this.SwitchBtn(GameManager.EnumTurn.MonsterTurn);
        }
    }

    private void OnOpenShop(object? parameter)
    {
        _shopWindow.ShowDialog();
    }

    private void PlayerTurn()
    {
        PlayerArrowVisibility = PlayerArrowVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        MonsterArrowVisibility = MonsterArrowVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
    }

    private void SwitchBtn(GameManager.EnumTurn turn)
    {
        switch (turn)
        {
            case GameManager.EnumTurn.InventoryTurn:
                Attack1IsEnabled = true;
                Attack2IsEnabled = true;
                Attack3IsEnabled = true;
                Attack4IsEnabled = true;
                break;

            case GameManager.EnumTurn.MonsterTurn:
                Attack1IsEnabled = false;
                Attack2IsEnabled = false;
                Attack3IsEnabled = false;
                Attack4IsEnabled = false;
                break;

            case GameManager.EnumTurn.PlayerTurn:
                Attack1IsEnabled = (Attack1BtnVisibility == Visibility.Visible && Player.BloodController?.Blood >= Player.AttacksEquipped[0]?.BloodNeeded && StateMachine.Instance.m_currentState == PlayerState.Instance);
                Attack2IsEnabled = (Attack2BtnVisibility == Visibility.Visible && Player.BloodController?.Blood >= Player.AttacksEquipped[1]?.BloodNeeded && StateMachine.Instance.m_currentState == PlayerState.Instance);
                Attack3IsEnabled = (Attack3BtnVisibility == Visibility.Visible && Player.BloodController?.Blood >= Player.AttacksEquipped[2]?.BloodNeeded && StateMachine.Instance.m_currentState == PlayerState.Instance);
                Attack4IsEnabled = (Attack4BtnVisibility == Visibility.Visible && Player.BloodController?.Blood >= Player.AttacksEquipped[3]?.BloodNeeded && StateMachine.Instance.m_currentState == PlayerState.Instance);
                break;
        }
    }

    private void ShowBtnWhenBuy()
    {
        Attack2BtnVisibility = Player.AttacksEquipped.Count < 2 ? Visibility.Collapsed : Visibility.Visible;
        Attack3BtnVisibility = Player.AttacksEquipped.Count < 3 ? Visibility.Collapsed : Visibility.Visible;
        Attack4BtnVisibility = Player.AttacksEquipped.Count < 4 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ShowPochitaDmgBooster()
    {
        PochitaDmgBoosterVisibility = Visibility.Visible;
    }

    private void ChangeHpColorPlayer(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            HpPlayerForeground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                HpPlayerForeground = Brushes.White;
            });
        });

    }
    private void ChangeMoneyColorPlayer(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {

            MoneyPlayerForeground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                MoneyPlayerForeground = Brushes.White;
            });
        });
    }

    private void ChangeHpColorMonster(bool isGained)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            HpMonsterForeground = isGained ? Brushes.Green : Brushes.Red;
        });
        Task.Delay(250).ContinueWith(t =>
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                HpMonsterForeground = Brushes.White;
            });
        });
    }

    private void OnInventory(object? parameter)
    {
        if (StateMachine.Instance.m_currentState == PlayerState.Instance)
        {
            InventoryVisibility = InventoryVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            if (IsInventoryOpen)
            {
                SwitchBtn(GameManager.EnumTurn.InventoryTurn);
                IsShopAllowed = false;
            }
            else
            {
                SwitchBtn(GameManager.EnumTurn.PlayerTurn);
                IsShopAllowed = true;
            }
        }
    }

    private void SwapAttackInventory(int attackNbr)
    {
        (Player.AttacksEquipped[attackNbr], Player.Attacks[0]) = (Player.Attacks[0], Player.AttacksEquipped[attackNbr]);
    }

    private void ShowDeath()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            _backgroundMusic.Stop();

            DeathScreenVisibility = Visibility.Visible;
        });
        _gameOverSound.Play();

    }

    private void OnQuitGame(object? parameter)
    {
        Application.Current.Shutdown();
    }
}