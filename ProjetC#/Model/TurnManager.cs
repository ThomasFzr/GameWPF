using System.Windows;

namespace Game.Model;

public class TurnManager
{

    public PlayerState PlayerState { get; set; }
    private StateMachine stateMachine;

    public TurnManager(Player Player, Monster Monster)
    {
        PlayerState = new(Player, Monster);

        stateMachine = ((App)Application.Current).stateMachine;
        //stateMachine.m_currentState = PlayerState;

        stateMachine.HandleRequestStateChangement(PlayerState);
    }



    public void ProcessPlayerTurn()
    {
        stateMachine.ProcessUpdate();
    }

    public void ProcessMonsterTurn()
    {
        stateMachine.ProcessUpdate();
    }

}
