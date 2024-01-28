namespace Game.Model;

public class GameManager
{
    private static GameManager instance;

    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public TurnManager TurnManager { get; set; }
    public bool IsGameRunning { get; private set; }

    private GameManager()
    {
        Player = new();
        Monster = new();
        TurnManager = new(Player, Monster);
        Monster.MonsterIsDead += AddMoneytoPlayer;
        IsGameRunning = false;
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
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

    public void ProcessPlayerTurn()
    {
        TurnManager.ProcessPlayerTurn();
    }

    public void ProcessMonsterTurn()
    {
        TurnManager.ProcessMonsterTurn();
    }
}
