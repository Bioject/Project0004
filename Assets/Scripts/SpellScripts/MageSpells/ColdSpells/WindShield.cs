using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShield : ColdSpells {
    bool isActive = false;

    public override void CastSpell()
    {
        if (isActive)
            return;
        if (Character.CharacterStats.Mana < SpellCost)
        {
            print("No Mana");
            return;
        }
        isActive = true;
        Character.CharacterStats.Mana -= SpellCost;
        Character.CharacterStats.DamageAbsorber = ColdDamage;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(SpellDuration);
        isActive = false;
        Character.CharacterStats.DamageAbsorber = 0;
    }
}
