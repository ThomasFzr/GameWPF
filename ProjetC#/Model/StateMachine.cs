namespace Game.Model;

public class StateMachine
{
    private static StateMachine? _instance;
    public AState? m_currentState;

    private StateMachine()
    { 
    }

    public static StateMachine Instance
    {
        get
        {
            _instance ??= new StateMachine();
            return _instance;
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
