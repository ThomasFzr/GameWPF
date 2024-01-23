using Game.Model;
using Game.View;

namespace Game.ViewModel;

class InGameViewModel
{

    Player player = new();
    Monster monster = new();

    public int MyProperty { get; set; } = 12;

    public InGameViewModel(InGameWindow inGameWindow)
    {
        inGameWindow.OnClickedSpell += ExecuteSpell;
        
    }

    public void StartGame()
    {
        int monsterLevel = 1;

        monster.state = Character.enumState.nothing;
        if (player.state == Character.enumState.freeze)
        {
            //Console.WriteLine("Vous êtes freeze, vous ne pouvez pas jouer!");
        }



        //player.spells.Find(spell => spell.spellNumber == spellNbr).Execute(player, monster);



        if (monster.state == Character.enumState.freeze)
        {
            //Console.WriteLine("Le monstre est freeze, il ne peut pas jouer!");
        }
        else if (monster.mana > 0)
        {
            //player.spells.Find(spell => spell.spellNumber == rdmNbr).Execute(monster, player);
            //Console.WriteLine("Le monstre utilise " + player.spells.Find(spell => spell.spellNumber == rdmNbr).spellName + "!\n");
        }

        //System.Threading.Thread.Sleep(2000);
        //Console.Clear();

        if (monster.hp < 0)
        {
            //Console.Clear();
            //Console.WriteLine("Le monstre niveau " + monsterLevel + " est mort! \n");
            monsterLevel++;
            //Console.WriteLine("Le monstre niveau " + monsterLevel + " apparait! \n");
            monster.hp = 100 + 10 * (monsterLevel - 1);
            monster.mana = 50 + 10 * (monsterLevel - 1);
            //System.Threading.Thread.Sleep(2000);
            //Console.Clear();
        }

        if (player.hp <= 0)
        {
            //Console.WriteLine("Vous êtes mort :( \n");
        }
    }
    void ExecuteSpell(int spellNbr)
    {
        player.spells.Find(spell => spell.spellNumber == spellNbr)?.Execute(player, monster);
    }
}


