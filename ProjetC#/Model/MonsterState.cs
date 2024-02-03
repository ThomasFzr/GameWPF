using System;
using System.Threading.Tasks;

namespace Game.Model;

public class MonsterState : AState
{
    private static MonsterState? instance;
    private int randomNumber;
    private Random random = new();
    public Action<int> OnMonsterAttack;

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
        Task.Delay(1250).ContinueWith(t =>
        {


            randomNumber = random.Next(0, GameManager.Instance.Monster.monsterAttackLevel);
            OnMonsterAttack?.Invoke(randomNumber);
            GameManager.Instance.Monster.Attacks[randomNumber]?.Execute(GameManager.Instance.Monster, GameManager.Instance.Player);

            Task.Delay(550).ContinueWith(t =>
            {
                App.Current.Dispatcher.Invoke(() =>
            {
                StateMachine.Instance.HandleRequestStateChangement(PlayerState.Instance);
            });
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
