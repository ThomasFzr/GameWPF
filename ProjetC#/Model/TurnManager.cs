namespace Game.Model;

public class TurnManager
{

    public PlayerState PlayerState { get; set; }

    public TurnManager(Player Player, Monster Monster)
    {
        PlayerState = new(Player, Monster);
        StateMachine.Instance.HandleRequestStateChangement(PlayerState);
    }



    public void ProcessPlayerTurn()
    {
        StateMachine.Instance.ProcessUpdate();
    }

    public void ProcessMonsterTurn()
    {
        StateMachine.Instance.ProcessUpdate();
    }

}
