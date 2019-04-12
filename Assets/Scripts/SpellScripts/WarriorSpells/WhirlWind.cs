using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : MeleeSkills {

    bool isActive;

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
        Player.combatScript.SetAttacking(true);
        isActive = true;
        Character.CharacterStats.Modifiers.DamageModifier = +((Character.CharacterStats.MinAttackDamage - Character.CharacterStats.MaxAttackDamage)+SpellDamage);
        Character.CharacterStats.Modifiers.AttackRatingModifier = -((int)(Character.CharacterStats.AttackRating / 4) +SpellLevel * 10);
        for (int i = 0; i <= SpellDuration; i++)
        {
            StartCoroutine(Whirl(i));
        }
    }

    IEnumerator Whirl(int wait)
    {
        yield return new WaitForSeconds((float)wait * .1f);
        int count = 0;
        int r = Random.Range(0, 100);
        int sr = SpellRange; //this is the spell range
        if (r >= 80)
            sr = SpellRange + 1;

        while (count < sr)
        {
            count++;
            for (int i = Player.GetColumn() - count; i <= Player.GetColumn() + count; i++)
            {
                for (int j = Player.GetRow() - count; j <= Player.GetRow() + count; j++)
                {
                    if (i == Player.GetColumn() - count || i == Player.GetColumn() + count
                        || j == Player.GetRow() - count || j == Player.GetRow() + count)
                    {
                        if (i <= GameManager.NodeManager.rows - 1 && i >= 0
                            && j <= GameManager.NodeManager.columns - 1 && j >= 0)
                        {
                            //nodes.Add(GameManager.NodeManager.allNodes[i, j]);
                            GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                            if (GameManager.NodeManager.allNodes[i, j].occupant is IDamageable)
                            {
                                //adding the ndoe to our potential nodes
                                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[i, j].occupant;
                                target.TakeDamage(Character);
                            }//end of if #3
                            yield return null;
                            GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = GameManager.NodeManager.allNodes[0, 0].gameObject.GetComponent<SpriteRenderer>().color;
                        }//end of if #2
                    }//end of if #1
                }//end of nested loop
            }//end of outer forloop
        }//end of whileloop
        if (wait == SpellDuration)
        {
            Character.CharacterStats.Modifiers.DamageModifier = 0;
            Character.CharacterStats.Modifiers.AttackRatingModifier = 0;
            Player.combatScript.SetAttacking(false);
            isActive = false;
        } 
    }//end of Nova()
}
