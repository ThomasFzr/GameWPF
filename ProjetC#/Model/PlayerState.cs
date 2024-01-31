using System;

namespace Game.Model;

public class PlayerState : AState
{
    private static PlayerState instance;

    public Action<int>? OnClickedSpell;
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
        OnClickedSpell += ExecuteSpell;
        OnArrowToShow?.Invoke();
        OnBtnToShow?.Invoke();
    }

    public void ExecuteSpell(int spellNbr)
    {
        GameManager.Instance.Player.SpellsEquipped[spellNbr]?.Execute(GameManager.Instance.Player, GameManager.Instance.Monster, GameManager.Instance.Player.IsTotemActivated);
    }
    public override void OnLeave()
    {
        OnClickedSpell -= ExecuteSpell;
        OnArrowToShow?.Invoke();
    }

    public override void OnProcess()
    {

    }
}
