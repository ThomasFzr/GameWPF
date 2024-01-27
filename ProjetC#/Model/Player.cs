using System.Collections.Generic;

namespace Game.Model;

public class Player : Character
{
    public MoneyController MoneyController { get; set; }

    int totemEquiped;
    int damageBooster;

    private List<ASpell> spellsEquipped = new();
    public List<ASpell> SpellsEquipped
    {
        get { return spellsEquipped; }
        set
        {
            spellsEquipped = value;
        }
    }


    public Player()
    {
        HealthController = new (100);
        ManaController = new (200);
        MoneyController = new(10);

        State = Character.EnumState.nothing;
        Spells.Add(new Heal());
        Spells.Add(new Fireball());

        SpellsEquipped.Add(new Heal());
        SpellsEquipped.Add(new Fireball());
    }

    public void EquipNewSpell(ASpell newSpell) {
        if(SpellsEquipped.Count < 4)
        {
            SpellsEquipped.Add(newSpell);
            OnPropertyChanged("SpellsEquipped");

        }
    }
}
