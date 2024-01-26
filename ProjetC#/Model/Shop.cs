using System.Collections.Generic;

namespace Game.Model;

public class Shop
{
    int price;
    public List<ASpell> spellOnSale = new();
    public void Buy(Character buyer, ASpell spell) { }

    public Shop()
    {
        spellOnSale.Add(new Heal());
    }
}
