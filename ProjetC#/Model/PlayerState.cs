using System;

namespace Game.Model;

public class PlayerState : AState
{
    public Action<int>? OnClickedSpell;

    public Player Player { get; set; }
    public Monster Monster { get; set; }
    public override void OnEnter()
    {
        OnClickedSpell += ExecuteSpell;
    }

    public void ExecuteSpell(int spellNbr)
    {
        Player.Spells[spellNbr]?.Execute(Player, Monster);

    }
    public override void OnLeave()
    {
        OnRequestChangeState?.Invoke(new MonsterState());
    }

    public override void OnProcess()
    {
        OnRequestChangeState?.Invoke(new PlayerState());
    }
}
