using System;

namespace Game.Model;

public class PlayerState : AState
{
    private static PlayerState instance;

    public Action<int>? OnClickedSpell;
    public Action? OnPlayerToPlay;

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
        OnPlayerToPlay?.Invoke();
    }

    public void ExecuteSpell(int spellNbr)
    {
        GameManager.Instance.Player.SpellsEquipped[spellNbr]?.Execute(GameManager.Instance.Player, GameManager.Instance.Monster);
    }
    public override void OnLeave()
    {
        OnClickedSpell -= ExecuteSpell;
        OnPlayerToPlay?.Invoke();
    }

    public override void OnProcess()
    {

    }
}
