namespace Game.Model;

public class StateMachine
{
    private static StateMachine? instance;
    public AState? m_currentState;

    private StateMachine()
    { 
    }

    public static StateMachine Instance
    {
        get
        {
            instance ??= new StateMachine();
            return instance;
        }
    }

    public void HandleRequestStateChangement(AState a_newState)
    {
        m_currentState?.OnLeave();
        m_currentState = a_newState;
        m_currentState?.OnEnter();
    }

    public void ProcessUpdate()
    {
        m_currentState?.OnProcess();
    }
}
