using System;
using System.Threading.Tasks;

namespace Game.Model;

public class MonsterState : AState
{
    private static MonsterState? _instance;
    private int _randomNumber;
    private Random _random = new();
    public Action<int> OnMonsterAttack;

    private MonsterState()
    {
    }

    public static MonsterState Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }

    public override void OnEnter()
    {
        Task.Delay(1250).ContinueWith(t =>
        {


            _randomNumber = _random.Next(0, GameManager.Instance.Monster.monsterAttackLevel);
            OnMonsterAttack?.Invoke(_randomNumber);
            GameManager.Instance.Monster.Attacks[_randomNumber]?.Execute(GameManager.Instance.Monster, GameManager.Instance.Player);

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
