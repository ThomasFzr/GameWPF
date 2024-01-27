using System.Collections.Generic;

namespace Game.Model;

public class TurnManager
{

    public Player Player { get; set; } = new();
    public Monster Monster { get; set; } = new();
    //public List<Character> characterList { get; set; } = new();
    //private Character CharacterToPlay { get; set; } = new();

    private PlayerState PlayerState { get; set; }
    private StateMachine StateMachine { get; set; } = new();

    public TurnManager()
    {
        //characterList.Add(Player);
        //characterList.Add(Monster);
        StateMachine.m_currentState = PlayerState;
    }


    //public void ExecuteSpellPlayer(int spellNbr)
    //{


    //    StateMachine.HandleRequestStateChangement(MonsterState);
    //    this.ExecuteSpellMonster();

    //}

    //void ExecuteSpellMonster()
    //{

      

    //    StateMachine.HandleRequestStateChangement(PlayerState);

    //}

}
