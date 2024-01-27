namespace Game.Model;

public class GameManager
{
    private Player player;
    private Monster monster;
    private TurnManager turnManager;

    public GameManager()
    {
        player = new();
        monster = new();
        turnManager = new();
    }
}
