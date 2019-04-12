using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightSpells : BaseSpells {

    [SerializeField]
    private int lightDamage;
    public int LightDamage { get { return lightDamage; } set { lightDamage = value; } }

    public override void CastSpell()
    {

    }
}
