using System;
using System.Threading.Tasks;

namespace Game.Model;

public class MonsterState : AState
{
    private static MonsterState instance;
    private int randomNumber;
    private Random random = new();
    public Action OnMonsterAttack;

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

            OnMonsterAttack?.Invoke();
            randomNumber = random.Next(0, GameManager.Instance.Monster.monsterAttackLevel);
            GameManager.Instance.Monster.Attacks[randomNumber]?.Execute(GameManager.Instance.Monster, GameManager.Instance.Player);

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
