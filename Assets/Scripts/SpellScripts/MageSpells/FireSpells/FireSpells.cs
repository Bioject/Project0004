using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireSpell : BaseSpells {

    [SerializeField]
    private int fireDamage;
    public int FireDamage { get { return fireDamage; } set { fireDamage = value; } }

    [SerializeField]
    private int fireRadius;
    public int FireRadius { get { return fireRadius; } set { fireRadius = value; } }
}
