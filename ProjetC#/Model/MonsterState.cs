using System;
using System.Threading;

namespace Game.Model;

public class MonsterState : AState
{
    private int randomNumber;

    Random random = new();
    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public override void OnEnter()
    {
        Thread.Sleep(1000);
        randomNumber = random.Next(0, 2);
        Monster.Spells[randomNumber]?.Execute(Monster, Player);
    }

    public override void OnLeave()
    {
        OnRequestChangeState?.Invoke(new PlayerState());
    }

    public override void OnProcess()
    {
        OnRequestChangeState?.Invoke(new MonsterState());
    }
}
