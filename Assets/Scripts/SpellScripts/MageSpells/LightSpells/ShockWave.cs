using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : LightSpells {

    private void Start()
    {
        Character = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>().character;
        Player = gameObject.transform.parent.parent.parent.parent.GetComponent<Player>();
    }

    public override void CastSpell()
    {
        if (Character.CharacterStats.Mana < SpellCost)
        {
            print("No Mana");
            return;
        }
        Character.CharacterStats.Mana -= SpellCost;
        Nova();
    }

    //checking the radious around the player
    private void Nova()
    {
        //List<IDamageable> targets = new List<IDamageable>();
        int damage = LightDamage;

        for (int i = Player.GetColumn() - SpellRange; i <= Player.GetColumn() +SpellRange; i++)
        {
            for (int j = Player.GetRow() - SpellRange; j <= Player.GetRow() + SpellRange; j++)
            {
                if (i <= GameManager.NodeManager.rows - 1 && i >= 0
                    && j <= GameManager.NodeManager.columns - 1 && j >= 0){
                    if (GameManager.NodeManager.allNodes[i, j].occupant is IDamageable)
                    {
                        //getting the distance and reducing the damage accordingly
                        damage = LightDamage;
 
                        int x = Mathf.Abs((i - Player.GetColumn()));
                        int y = Mathf.Abs((j - Player.GetRow()));

                        damage -= (x+y)+SpellLevel;

                        if (damage <= 1)
                            damage = 1;

                        IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[i, j].occupant;
                        target.TakeLightDamage(Character, damage);
                    }
                }
            }//end of nested loop
        }//end of outer forloop
    }//end of nova
}
