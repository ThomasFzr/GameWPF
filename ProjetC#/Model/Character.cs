using System.Collections.Generic;

namespace Game.Model
{
    class Character
    {
        public int hp;
        public int mana;
        public enumState state;
        public enum enumState
        {
            freeze,
            fire,
            nothing,
        };

        public List<ASpell> spells = new ();

    }
}
