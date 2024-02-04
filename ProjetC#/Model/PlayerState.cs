using System;

namespace Game.Model;

public class PlayerState : AState
{
    private static PlayerState? _instance;

    public Action<int>? OnClickedAttack;
    public Action? OnArrowToShow;
    public Action? OnPlayerTurn;
    public Action<GameManager.EnumTurn>? OnBtnToShow;

    private PlayerState()
    {
    }

    public static PlayerState Instance
    {
        get
        {
            _instance ??= new ();
            return _instance;
        }
    }

    public override void OnEnter()
    {
        OnClickedAttack += ExecuteAttack;
        OnArrowToShow?.Invoke();
        OnPlayerTurn?.Invoke();
        OnBtnToShow?.Invoke(GameManager.EnumTurn.PlayerTurn);
    }

    public void ExecuteAttack(int attackNbr)
    {
        GameManager.Instance.Player.AttacksEquipped[attackNbr]?.Execute(GameManager.Instance.Player, GameManager.Instance.Monster, GameManager.Instance.Player.IsTotemActivated);
    }
    public override void OnLeave()
    {
        OnClickedAttack -= ExecuteAttack;
        OnArrowToShow?.Invoke();
    }

    public override void OnProcess()
    {
    }
}
