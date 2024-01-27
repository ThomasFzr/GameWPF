namespace Game.Model;

public class StateMachine
{
    public AState m_currentState;



    public void HandleRequestStateChangement(AState a_newState)
    {
        if (m_currentState != null)
        {
            m_currentState.OnRequestChangeState -= HandleRequestStateChangement;
        }

        m_currentState?.OnLeave();
        m_currentState = a_newState;

        m_currentState?.OnEnter();
        m_currentState.OnRequestChangeState += HandleRequestStateChangement;

    }


    public void ProcessUpdate()
    {
        m_currentState?.OnProcess();
    }
}