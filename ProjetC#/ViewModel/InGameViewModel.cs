using Game.Model;
using Game.View;
using System;
using System.Threading.Tasks;

namespace Game.ViewModel;

public class InGameViewModel
{
    private int monsterLevel = 1;
    private int randomNumber;
    public Player Player { get; set; } = new();
    public Monster Monster { get; set; } = new();
    Random random = new();

    public InGameViewModel(InGameWindow inGameWindow)
    {
        inGameWindow.OnClickedSpell += ExecuteSpellPlayer;

    }

    public void StartGame()
    {

    }

    void ExecuteSpellPlayer(int spellNbr)
    {
        Player.Spells[spellNbr]?.Execute(Player, Monster);
        Task.Delay(1000).ContinueWith(t => ExecuteSpellMonster());
    }

    void ExecuteSpellMonster()
    {
        randomNumber = random.Next(0, monsterLevel + 1);
        Monster.Spells[randomNumber]?.Execute(Monster, Player);
    }
}


