namespace Game.Model
{
    class Player : Character
    {
        public int money;
        public int totemEquiped;
        public int damageBooster;
        public Player() {
            hp = 100;
            mana = 200;
            money = 0;
            state = Character.enumState.nothing;
            spells.Add(new Heal());
            spells.Add(new Fireball());
        }
    }
}
