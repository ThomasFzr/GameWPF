namespace Game.Model;

public class GameManager
{
    private static GameManager? _instance;

    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public bool IsGameRunning { get; private set; }

    public enum EnumTurn
    {
        PlayerTurn,
        MonsterTurn,
        InventoryTurn
    }

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
            _instance ??= new GameManager();
            return _instance;
        }
    }

    private void AddMoneytoPlayer(int monsterLevel)
    {
        Player.MoneyController?.MoneyGain(monsterLevel * 1000);
        Player.BloodController?.BloodGain(monsterLevel * 10);
        Player.HealthController?.HealthGain(monsterLevel * 10);
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
