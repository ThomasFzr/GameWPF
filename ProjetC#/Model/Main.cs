namespace Game.Model;
using System;

class MainClass : InGameWindow
{
    public bool isPlaying = false;
    private int spellNbr;

    public MainClass() {
        
    }  
    public void Start(bool isPlaying)
    {      
        Player player = new();
        Monster monster = new();

        int monsterLevel = 1;
        int nbToPlay = 1;

        Random random = new Random();
        int rdmNbr = 0;
       

        while (isPlaying && player.hp > 0)
        {

            //Console.WriteLine("HP: " + player.hp + "                        HP monstre: " + monster.hp + "\n");
            //Console.WriteLine("Mana: " + player.mana + "                        Mana monstre: " + monster.mana + "\n");

            if (nbToPlay == 1)
            {
                monster.state = Character.enumState.nothing;
                if (player.state == Character.enumState.freeze)
                {
                    //Console.WriteLine("Vous êtes freeze, vous ne pouvez pas jouer!");
                }
                else
                {
                    //Console.WriteLine("Spell dispo: " + "\n");

                    foreach (ASpell spell in player.spells)
                    {
                        if (player.mana >= spell.manaNeeded)
                        {
                            //Console.WriteLine("(" + spell.spellNumber + ") " + spell.spellName + " : Coûte " + spell.manaNeeded + " mana, " + spell.description);
                        }
                    }

                    //Console.WriteLine("\n Choisir un spell: " + "\n");

                    //while (!int.TryParse(Console.ReadLine(), out spellNbr)) { }

                    player.spells.Find(spell => spell.spellNumber == spellNbr).Execute(player, monster);
                    //Console.WriteLine("Vous utilisez " + player.spells.Find(spell => spell.spellNumber == spellNbr).spellName + "! \n");
                }
                nbToPlay = 2;
            }
            else if (nbToPlay == 2 && monster.hp > 0)
            {
                player.state = Character.enumState.nothing;

                if (monster.state == Character.enumState.freeze)
                {
                    //Console.WriteLine("Le monstre est freeze, il ne peut pas jouer!");
                }
                else if (monster.mana > 0)
                {
                    rdmNbr = random.Next(1, 5);
                    player.spells.Find(spell => spell.spellNumber == rdmNbr).Execute(monster, player);
                    //Console.WriteLine("Le monstre utilise " + player.spells.Find(spell => spell.spellNumber == rdmNbr).spellName + "!\n");
                }
                nbToPlay = 1;
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
                nbToPlay = 1;
                //System.Threading.Thread.Sleep(2000);
                //Console.Clear();
            }

        }
        if (player.hp <= 0)
        {
            //Console.WriteLine("Vous êtes mort :( \n");
        }
    }
}