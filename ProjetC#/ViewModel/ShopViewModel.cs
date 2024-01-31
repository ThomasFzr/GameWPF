using Game.Model;

namespace Game.ViewModel;

public class ShopViewModel
{
    public Shop Shop { get; set; } = new();
    public Player Player { get;private set; }
    public ShopViewModel()
    {
        Player = GameManager.Instance.Player;
    }
}
