using System;
using System.Threading;

namespace Game.Model;

public class MonsterState : AState
{
    private int randomNumber;

    Random random = new();
    public Player Player { get; set; }
    public Monster Monster { get; set; }

    public MonsterState(Player _Player, Monster _Monster)
    {
        Player = _Player;
        Monster = _Monster;
    }

    public override void OnEnter()
    {
        //Thread.Sleep(2000);
        randomNumber = random.Next(0, Monster.monsterSpellLevel);
        Monster.Spells[randomNumber]?.Execute(Monster, Player);
        StateMachine.Instance.HandleRequestStateChangement(new PlayerState(GameManager.Instance.Player, GameManager.Instance.Monster));

    }

    public override void OnLeave()
    {
        
        //OnRequestChangeState?.Invoke(new PlayerState(Player, Monster));
    }

    public override void OnProcess()
    {
        //StateMachine.Instance.HandleRequestStateChangement(new PlayerState(Player, Monster));
        //OnRequestChangeState?.Invoke(new PlayerState(Player, Monster));
    }
}
