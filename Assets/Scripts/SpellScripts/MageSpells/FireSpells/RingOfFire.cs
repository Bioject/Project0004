using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : FireSpell {
    bool isActive = false;

    // Use this for initialization
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
        isActive = true;
        Character.CharacterStats.Mana -= SpellCost;
        Player.combatScript.SetAttacking(false);
        StartCoroutine(Nova());
    }

    IEnumerator Nova()
    {
        List<Node> nodes = new List<Node>();

        int count = 0;
        while (count < SpellRange)
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
                            nodes.Add(GameManager.NodeManager.allNodes[i, j]);
                            GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                            if (GameManager.NodeManager.allNodes[i, j].occupant is IDamageable)
                            {
                                //adding the ndoe to our potential nodes
                                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[i, j].occupant;
                                target.TakeFireDamage(Character, FireDamage);
                            }//end of if #3
                        }//end of if #2
                    }//end of if #1
                }//end of nested loop
            }//end of outer forloop
            yield return new WaitForSeconds(.1f);
            foreach (Node n in nodes)
            {
                n.gameObject.GetComponent<SpriteRenderer>().color = GameManager.NodeManager.allNodes[0, 0].gameObject.GetComponent<SpriteRenderer>().color;
            }
            nodes.Clear();
        }//end of whileloop
            Player.combatScript.SetAttacking(false);
            isActive = false;
    }//end of Nova()
}
