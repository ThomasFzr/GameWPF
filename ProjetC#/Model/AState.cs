using System;

namespace Game.Model;

abstract public class AState
{
    public Action<AState>? OnRequestChangeState;
    abstract public void OnProcess();

    abstract public void OnEnter();

    abstract public void OnLeave();

}
