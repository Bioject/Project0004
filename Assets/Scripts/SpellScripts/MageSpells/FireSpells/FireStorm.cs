using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : FireSpell {
    bool isActive = false;

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
        StartCoroutine(Storm());
    }
    IEnumerator Storm()
    {
        float time = Time.time + SpellDuration;
        List<Node> nodes = new List<Node>();
        while (Time.time < time)
        {
            for (int i = Player.GetColumn() - SpellRange; i <= Player.GetColumn() + SpellRange; i++)
            {
                for (int j = Player.GetRow() - SpellRange; j <= Player.GetRow() + SpellRange; j++)
                {
                    if (i <= GameManager.NodeManager.rows - 1 && i >= 0
                        && j <= GameManager.NodeManager.columns - 1 && j >= 0)
                    {
                        //adding the ndoe to our potential nodes
                         nodes.Add(GameManager.NodeManager.allNodes[i, j]);
                         //print(i+", "+ j);
                    }
                }//end of nested loop
            }//end of outer forloop
        int location = Random.Range(0, nodes.Count);
        nodes[location].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        AdjecentNode(nodes[location]);
        if (nodes[location].occupant is IDamageable)
        {
            //adding the ndoe to our potential nodes
            IDamageable target = (IDamageable)nodes[location].occupant;
            target.TakeFireDamage(Character, FireDamage);
        }
        nodes.Clear();
        yield return new WaitForSeconds(Random.Range(.05f, .3f));
        }//end of while
        isActive = false;
    }//end of storm

    void AdjecentNode(Node node)
    {
        //checking up
        if (node.column + 1 <= GameManager.NodeManager.columns - 1)
        {
            if (GameManager.NodeManager.allNodes[node.column + 1, node.row].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[node.column + 1, node.row].occupant;
                target.TakeFireDamage(Character, FireDamage);
            }
            GameManager.NodeManager.allNodes[node.column + 1, node.row].gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        //checking down
        if (node.column - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[node.column - 1, node.row].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[node.column - 1, node.row].occupant;
                target.TakeFireDamage(Character, FireDamage);
            }
            GameManager.NodeManager.allNodes[node.column - 1, node.row].gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        //checking right
        if (node.row + 1 <= GameManager.NodeManager.rows - 1)
        {
            if (GameManager.NodeManager.allNodes[node.column, node.row + 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[node.column, node.row + 1].occupant;
                target.TakeFireDamage(Character, FireDamage);
            }
            GameManager.NodeManager.allNodes[node.column, node.row + 1].gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        //checking left
        if (node.row - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[node.column, node.row - 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[node.column, node.row - 1].occupant;
                target.TakeFireDamage(Character, (int)(FireDamage / 2));
                print((int)(FireDamage / 2));
            }
            GameManager.NodeManager.allNodes[node.column, node.row - 1].gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }//end of AjdecentNode()
}//end of class
