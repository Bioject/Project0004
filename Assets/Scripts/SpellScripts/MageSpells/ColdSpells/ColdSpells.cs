using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColdSpells : BaseSpells {

    [SerializeField]
    private int coldDamage;
    public int ColdDamage { get { return coldDamage; } set { coldDamage = value; } }

    public override void CastSpell()
    {
    }
}
