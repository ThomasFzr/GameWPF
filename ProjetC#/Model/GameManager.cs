using Game.View;

namespace Game.Model;

public class GameManager
{
    private static GameManager instance;

    private static InGameWindow inGameWindowInstance;

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

    public static InGameWindow InGameWindowInstance
    {
        get { return inGameWindowInstance; }
        set { inGameWindowInstance = value; }

    }

    private void AddMoneytoPlayer(int monsterLevel)
    {
        Player.MoneyController.MoneyGain(monsterLevel * 1000);
        Player.BloodController.BloodGain(monsterLevel * 10);
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
