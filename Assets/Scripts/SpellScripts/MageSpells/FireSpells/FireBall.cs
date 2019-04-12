using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : FireSpell {

    private void Start()
    {
        Character = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>().character;
        Player = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>();
    }

    public override void CastSpell()
    {
    }
}
