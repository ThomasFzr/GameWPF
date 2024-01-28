namespace Game.Model;

public class GameManager
{
    private static GameManager instance;

    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public bool IsGameRunning { get; private set; }

    private GameManager()
    {
        Player = new();
        Monster = new();
        Monster.MonsterIsDead += AddMoneytoPlayer;
        IsGameRunning = false;
        StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
    }

    public static GameManager Instance
    {
        get
        {
            instance ??= new GameManager();
            return instance;
        }
    }

    private void AddMoneytoPlayer(int monsterLevel)
    {
        Player.MoneyController.Money += monsterLevel * 1000;
    }

    public void StartGame()
    {
        IsGameRunning = true;
    }

    public void EndGame()
    {
        IsGameRunning = false;
    }
}
