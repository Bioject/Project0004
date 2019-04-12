using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasterScript : MonoBehaviour {

    Player player;
    Character character;
    public List<BaseSpells> spellBook;
    int spellIndex = 0;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        character = player.character;

        spellBook = character.Spells;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (spellIndex <= 0)
                spellIndex = spellBook.Count-1;
            else
                spellIndex--;
            print(spellIndex);
        }
        if (Input.GetKeyDown("2"))
        {
            if (spellIndex >= spellBook.Count - 1)
                spellIndex = 0;
            else
                spellIndex++;
            print(spellIndex);
        }

        if (Input.GetButtonDown("Fire2"))
            spellBook[spellIndex].CastSpell();
    }
}
