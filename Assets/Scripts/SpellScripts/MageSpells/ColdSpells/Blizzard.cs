using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : ColdSpells {
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
        StartCoroutine(Nova());
    }

    IEnumerator Nova()
    {
        int count = 0;
        List<Node> tempLst = new List<Node>();
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
                            //nodes.Add(GameManager.NodeManager.allNodes[i, j]);
                            GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                            if (GameManager.NodeManager.allNodes[i, j].occupant is IDamageable)
                            {
                                //adding the ndoe to our potential nodes
                                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[i, j].occupant;
                                target.TakeFireDamage(Character, ColdDamage);
                            }//end of if #3
                            yield return new WaitForSeconds(.2f);
                            GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = GameManager.NodeManager.allNodes[0, 0].gameObject.GetComponent<SpriteRenderer>().color;
                        }//end of if #2
                    }//end of if #1
                }//end of nested loop
            }//end of outer forloop
        }//end of whileloop
        isActive = false;
    }//end of Nova()
}
