using System.Collections.Generic;

namespace Game.Model
{
    abstract class Shop
    {
        int price;
        public List<ASpell> spellOnSale = new();
        public abstract void Buy(Character buyer);

    }
}
