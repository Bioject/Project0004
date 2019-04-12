using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormShield : LightSpells {
    bool isActive = false;

    private void Start()
    {
        Character = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>().character;
        Player = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>();
    }

    public override void CastSpell()
    {
        if (isActive)
            return;
        if (Character.CharacterStats.Mana < SpellCost)
        {
            print("No Mana");
            return;
        }
        Character.CharacterStats.Mana -= SpellCost;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        isActive = true;
        //storing it into a tmep variabale incase the lightdamage changes due to increase in level
        int ld = LightDamage;
        Character.CharacterStats.Armor += ld;
        yield return new WaitForSeconds(SpellDuration);
        Character.CharacterStats.Armor -= ld;
        isActive = false;
    }
}
