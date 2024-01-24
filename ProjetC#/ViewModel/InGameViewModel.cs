using Game.Model;
using Game.View;

namespace Game.ViewModel;

public class InGameViewModel
{

    public Player Player { get; set; } = new();
    public Monster Monster { get; set; } = new();

    public InGameViewModel(InGameWindow inGameWindow)
    {
        inGameWindow.OnClickedSpell += ExecuteSpell;

    }

    public void StartGame()
    {
        int monsterLevel = 1;

        Monster.State = Character.EnumState.nothing;
        if (Player.State == Character.EnumState.freeze)
        {
            //Console.WriteLine("Vous êtes freeze, vous ne pouvez pas jouer!");
        }



        //player.spells.Find(spell => spell.spellNumber == spellNbr).Execute(player, monster);



        if (Monster.State == Character.EnumState.freeze)
        {
            //Console.WriteLine("Le monstre est freeze, il ne peut pas jouer!");
        }
        else if (Monster.Mana > 0)
        {
            //player.spells.Find(spell => spell.spellNumber == rdmNbr).Execute(monster, player);
            //Console.WriteLine("Le monstre utilise " + player.spells.Find(spell => spell.spellNumber == rdmNbr).spellName + "!\n");
        }

        //System.Threading.Thread.Sleep(2000);
        //Console.Clear();

        if (Monster.Hp < 0)
        {
            //Console.Clear();
            //Console.WriteLine("Le monstre niveau " + monsterLevel + " est mort! \n");
            monsterLevel++;
            //Console.WriteLine("Le monstre niveau " + monsterLevel + " apparait! \n");
            Monster.Hp = 100 + 10 * (monsterLevel - 1);
            Monster.Mana = 50 + 10 * (monsterLevel - 1);
            //System.Threading.Thread.Sleep(2000);
            //Console.Clear();
        }

        if (Player.Hp <= 0)
        {
            //Console.WriteLine("Vous êtes mort :( \n");
        }
    }
    void ExecuteSpell(int spellNbr)
    {
        Player.Spells[spellNbr]?.Execute(Player, Monster);
    }
}


