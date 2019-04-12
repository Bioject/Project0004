using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeSkills : BaseSpells
{
    [SerializeField]
    private int spellDamage;
    public int SpellDamage { get { return spellDamage; } set { spellDamage = value; } }
}
