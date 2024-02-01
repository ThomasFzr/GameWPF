using System;

namespace Game.Model;

public class PlayerState : AState
{
    private static PlayerState instance;

    public Action<int>? OnClickedAttack;
    public Action? OnArrowToShow;
    public Action? OnBtnToShow;

    private PlayerState()
    {
    }

    public static PlayerState Instance
    {
        get
        {
            instance ??= new ();
            return instance;
        }
    }

    public override void OnEnter()
    {
        OnClickedAttack += ExecuteAttack;
        OnArrowToShow?.Invoke();
        OnBtnToShow?.Invoke();
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
