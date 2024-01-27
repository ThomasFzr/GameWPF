using System;

namespace Game.Model;

public class PlayerState : AState
{
    public Action<int>? OnClickedSpell;
    public Action? OnPlayerToPlay;

    public Player Player { get; set; }
    public Monster Monster { get; set; }


    public PlayerState(Player _Player, Monster _Monster)
    {
        Player = _Player;
        Monster = _Monster;
    }

    public override void OnEnter()
    {
        OnClickedSpell += ExecuteSpell;
        OnPlayerToPlay?.Invoke();
    }

    public void ExecuteSpell(int spellNbr)
    {
        Player.SpellsEquipped[spellNbr]?.Execute(Player, Monster);

    }
    public override void OnLeave()
    {
        OnClickedSpell -= ExecuteSpell;
        OnPlayerToPlay?.Invoke();
        OnRequestChangeState?.Invoke(new MonsterState(Player, Monster));
    }

    public override void OnProcess()
    {
    }
}
