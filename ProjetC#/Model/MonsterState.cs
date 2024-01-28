using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Model;

public class MonsterState : AState
{
    private static MonsterState instance;

    private int randomNumber;

    Random random = new();

    private MonsterState()
    {
    }
    public static MonsterState Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    public override void OnEnter()
    {
        Task.Delay(2000).ContinueWith(t =>
        {
            randomNumber = random.Next(0, GameManager.Instance.Monster.monsterSpellLevel);
            GameManager.Instance.Monster.Spells[randomNumber]?.Execute(GameManager.Instance.Monster, GameManager.Instance.Player);
            App.Current.Dispatcher.Invoke(() =>
            {
                StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
            });
        });
    }

    public override void OnLeave()
    {
    }

    public override void OnProcess()
    {
    }
}
