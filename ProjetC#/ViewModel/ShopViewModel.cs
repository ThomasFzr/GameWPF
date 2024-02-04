using Game.Model;
using System.Printing;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Game.View;
using System.Windows.Input;
using System;

namespace Game.ViewModel;

public class ShopViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public ICommand ReplaceAttackCommand { get; }
    public ICommand HomeCommand { get; }
    public ICommand OnBuyAttackCommand { get; }
    public ICommand OnBuyTotemCommand { get; }
    public Action<GameManager.EnumTurn>? OnShopClosed;

    public Shop Shop { get; set; } = new();
    private ShopWindow _shopWindow;
    public Player Player { get; private set; }
    private AAttack? _attackToAdd;

    private Visibility _pochitaVisibility = Visibility.Visible;
    public Visibility PochitaVisibility
    {
        get { return _pochitaVisibility; }
        private set
        {
            if (_pochitaVisibility != value)
            {
                _pochitaVisibility = value;
                OnPropertyChanged();
            }
        }
    }
    private Visibility _attacksEquipedListVisibility = Visibility.Collapsed;
    public Visibility AttacksEquipedListVisibility
    {
        get { return _attacksEquipedListVisibility; }
        private set
        {
            if (_attacksEquipedListVisibility != value)
            {
                _attacksEquipedListVisibility = value;
                OnPropertyChanged();
            }
        }
    }

    private Visibility _shopItem1Visibility = Visibility.Visible;
    public Visibility ShopItem1Visibility
    {
        get { return _shopItem1Visibility; }
        private set
        {
            if (_shopItem1Visibility != value)
            {
                _shopItem1Visibility = value;
                OnPropertyChanged();
            }
        }
    }

    private Visibility _shopItem2Visibility = Visibility.Visible;
    public Visibility ShopItem2Visibility
    {
        get { return _shopItem2Visibility; }
        private set
        {
            if (_shopItem2Visibility != value)
            {
                _shopItem2Visibility = value;
                OnPropertyChanged();
            }
        }
    }

    private Visibility _shopItem3Visibility = Visibility.Visible;
    public Visibility ShopItem3Visibility
    {
        get { return _shopItem3Visibility; }
        private set
        {
            if (_shopItem3Visibility != value)
            {
                _shopItem3Visibility = value;
                OnPropertyChanged();
            }
        }
    }

    private Visibility _shopItem4Visibility = Visibility.Visible;
    public Visibility ShopItem4Visibility
    {
        get { return _shopItem4Visibility; }
        private set
        {
            if (_shopItem4Visibility != value)
            {
                _shopItem4Visibility = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _attackIsChecked1 = false;
    public bool AttackIsChecked1
    {
        get { return _attackIsChecked1; }
        set
        {
            if (_attackIsChecked1 != value)
            {
                _attackIsChecked1 = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _attackIsChecked2 = false;
    public bool AttackIsChecked2
    {
        get { return _attackIsChecked2; }
        set
        {
            if (_attackIsChecked2 != value)
            {
                _attackIsChecked2 = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _attackIsChecked3 = false;
    public bool AttackIsChecked3
    {
        get { return _attackIsChecked3; }
        set
        {
            if (_attackIsChecked3 != value)
            {
                _attackIsChecked3 = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _attackIsChecked4 = false;
    public bool AttackIsChecked4
    {
        get { return _attackIsChecked4; }
        set
        {
            if (_attackIsChecked4 != value)
            {
                _attackIsChecked4 = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
    }
    public ShopViewModel(ShopWindow shopWindow)
    {
        Player = GameManager.Instance.Player;
        _shopWindow = shopWindow;

        Player.OnYesClickedAction += EnableDialogBox;
        GameManager.Instance.Player.MoneyController.OnMoneyChanged += ChangeMoneyColorPlayer;

        ReplaceAttackCommand = new RelayCommand(OnReplaceAttack);
        HomeCommand = new RelayCommand(OnHomeClicked);
        OnBuyAttackCommand = new RelayCommand(OnBuyAttack);
        OnBuyTotemCommand = new RelayCommand(OnBuyTotemButton);
    }

    private void OnHomeClicked(object? sender)
    {
        _shopWindow.Hide();
        OnShopClosed?.Invoke(GameManager.EnumTurn.PlayerTurn);
    }

    private void OnBuyAttack(object? sender)
    {
        string? attackName = (string)sender;

        AAttack attackToBuy = Shop.AttacksOnSale.Find(s => s.AttackName == attackName);

        if (attackToBuy != null)
        {
            if (Shop.Buy(Player, attackToBuy))
            {
                switch (attackName)
                {
                    case "HEAL":
                        ShopItem1Visibility = Visibility.Collapsed; break;
                    case "SANGBOOST":
                        ShopItem2Visibility = Visibility.Collapsed; break;
                    case "BLASTER":
                        ShopItem3Visibility = Visibility.Collapsed; break;
                    case "HURRICANE":
                        ShopItem4Visibility = Visibility.Collapsed; break;
                }
            }
        }

    }

    private void EnableDialogBox(AAttack attack)
    {
        AttacksEquipedListVisibility = Visibility.Visible;
        _attackToAdd = attack;
    }

    private void OnReplaceAttack(object? sender)
    {
        AttacksEquipedListVisibility = Visibility.Collapsed;
        const int nothinSelected = 666;
        int attackIndexToReplace = nothinSelected;
        if (AttackIsChecked1)
        {
            attackIndexToReplace = 0;
        }
        else if (AttackIsChecked2)
        {
            attackIndexToReplace = 1;
        }
        else if (AttackIsChecked3)
        {
            attackIndexToReplace = 2;
        }
        else if (AttackIsChecked4)
        {
            attackIndexToReplace = 3;
        }
        if (attackIndexToReplace != nothinSelected)
        {
            Player.Attacks.Add(Player.AttacksEquipped[attackIndexToReplace]);
            Player.AttacksEquipped[attackIndexToReplace] = _attackToAdd;
        }

    }

    private void OnBuyTotemButton(object? sender)
    {
        const int priceTotem = 5000;
        if (Player.MoneyController.Money >= priceTotem)
        {
            Player.IsTotemActivated = true;
            PochitaVisibility = Visibility.Collapsed;
            Shop.OnBuyDamageBooster?.Invoke();
            Player.MoneyController.MoneyLoss(priceTotem);
        }

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
}
