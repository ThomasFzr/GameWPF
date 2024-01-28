namespace Game.Model;

public class GameManager
{
    private static GameManager instance;

    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public TurnManager TurnManager { get; set; }

    private bool isGameRunning;
    public bool IsGameRunning => isGameRunning;

    private GameManager()
    {
        Player = new();
        Monster = new();
        TurnManager = new(Player, Monster);
        Monster.MonsterIsDead += AddMoneytoPlayer;
        isGameRunning = false;
    }

    // Property to access the singleton instance
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
        isGameRunning = true;
    }

    public void EndGame()
    {
        isGameRunning = false;
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
