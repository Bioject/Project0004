using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MeleeSkills {

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
        Player.combatScript.SetAttacking(true);
        isActive = true;
        Character.CharacterStats.Modifiers.DamageModifier = (int)((Character.CharacterStats.MaxAttackDamage/10)*(SpellLevel*SpellDamage));
        print(Character.CharacterStats.Modifiers.DamageModifier);
        Character.CharacterStats.Modifiers.AttackRatingModifier = SpellLevel * 30;
        FindTarget();
    }

    void FindTarget()
    {
        for(int i = Player.GetColumn() - SpellRange; i < Player.GetColumn() + SpellRange; i++)
        {
            for (int j = Player.GetRow() - SpellRange; j < Player.GetRow() + SpellRange; j++)
            {
                if (i <= GameManager.NodeManager.rows - 1 && i >= 0
                            && j <= GameManager.NodeManager.columns - 1 && j >= 0)
                {
                    //nodes.Add(GameManager.NodeManager.allNodes[i, j]);
                    //GameManager.NodeManager.allNodes[i, j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    if (GameManager.NodeManager.allNodes[i, j].occupant is BaseEnemy)
                    {
                        Node node = AdjecentNode(GameManager.NodeManager.allNodes[i, j]);
                        if (node != null)
                        {
                            Character.CharacterStats.Mana -= SpellCost;
                            Player.SetGrid(Player.GetColumn(), Player.GetRow());
                            IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[i, j].occupant;
                            GameManager.NodeManager.ClearNode(Player.GetColumn(), Player.GetRow());
                            Player.SetColumn(node.column);
                            Player.SetRow(node.row);
                            GameManager.NodeManager.SetPosition(Player, Player.GetColumn(), Player.GetRow());
                            StartCoroutine(LerpMovement(node.gameObject.transform.position, target));
                            return;      
                        } 
                        //find the nearest adjecent node to the enemy
                    }//end of if #3
                }//end of if #2
            }//end of inner loop
        }//end of outer loop
        Player.combatScript.SetAttacking(false);
        isActive = false;
        Character.CharacterStats.Modifiers.DamageModifier = 0;
        Character.CharacterStats.Modifiers.AttackRatingModifier = 0;

    }//dash

    Node AdjecentNode(Node node)
    {
        //checking right
        if (node.column + 1 <= GameManager.NodeManager.columns - 1)
        {
            if (!GameManager.NodeManager.allNodes[node.column + 1, node.row].occupied)
            {
                Player.ChangeDirection("Left");
                return GameManager.NodeManager.allNodes[node.column + 1, node.row];
            }            
        }

        //checking left
        if (node.column - 1 >= 0)
        {
            if (!GameManager.NodeManager.allNodes[node.column - 1, node.row].occupied)
            {
                Player.ChangeDirection("Right");
                return GameManager.NodeManager.allNodes[node.column - 1, node.row];
            }
        }

        //checking up
        if (node.row + 1 <= GameManager.NodeManager.rows - 1)
        {
            if (!GameManager.NodeManager.allNodes[node.column, node.row + 1].occupied)
            {
                Player.ChangeDirection("Down");
                return GameManager.NodeManager.allNodes[node.column, node.row + 1];
            }
        }

        //checking down
        if (node.row - 1 >= 0)
        {
            if (!GameManager.NodeManager.allNodes[node.column, node.row - 1].occupied)
            {
                //adding the ndoe to our potential nodes
                Player.ChangeDirection("Up");
                return GameManager.NodeManager.allNodes[node.column, node.row - 1];
            }
        }
        return null;
    }//end of AjdecentNode()

    IEnumerator LerpMovement(Vector3 pos, IDamageable target)
    {
        float elapsedTime = 0;
        float time = .1f;
        Vector3 startPos = Player.characterTransform.localPosition;
        while (elapsedTime < time)
        {
            Player.characterTransform.localPosition = Vector3.Lerp(startPos, pos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Player.characterTransform.localPosition = pos;
        target.TakeDamage(Character);
        AdjecentFoe(Player.GetColumn(), Player.GetRow());
        yield return new WaitForSeconds(.4f);
        Player.combatScript.SetAttacking(false);
        isActive = false;
        Character.CharacterStats.Modifiers.DamageModifier = 0;
        Character.CharacterStats.Modifiers.AttackRatingModifier = 0;
    }//end of lerp movment

    public void AdjecentFoe(int column, int row)
    {
        //checking right
        if (column + 1 <= GameManager.NodeManager.columns - 1)
        {
            if (GameManager.NodeManager.allNodes[column + 1, row].occupant is IDamageable)
            {
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column + 1, row].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking left
        if (column - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[column - 1, row].occupant is IDamageable)
            {
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column - 1, row].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking Up
        if (row + 1 <= GameManager.NodeManager.rows - 1)
        {
            if (GameManager.NodeManager.allNodes[column, row + 1].occupant is IDamageable)
            {
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column, row + 1].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking down
        if (row - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[column, row - 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column, row - 1].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking top left
        if (column - 1 >= 0 && row + 1 <= GameManager.NodeManager.rows - 1)
        {
            if (GameManager.NodeManager.allNodes[column - 1, row + 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column - 1, row + 1].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking top right
        if (column + 1 <= GameManager.NodeManager.columns - 1 && row + 1 <= GameManager.NodeManager.rows - 1)
        {
            if (GameManager.NodeManager.allNodes[column + 1, row + 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column + 1, row + 1].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking bottom right
        if (column +1 <-GameManager.NodeManager.columns - 1 && row - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[column + 1, row - 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column + 1, row - 1].occupant;
                target.TakeDamage(Character);
            }
        }

        //checking bottom right
        if (column - 1 >= 0 && row - 1 >= 0)
        {
            if (GameManager.NodeManager.allNodes[column - 1, row - 1].occupant is IDamageable)
            {
                //adding the ndoe to our potential nodes
                IDamageable target = (IDamageable)GameManager.NodeManager.allNodes[column - 1, row - 1].occupant;
                target.TakeDamage(Character);
            }
        }
    }

}//end of class
