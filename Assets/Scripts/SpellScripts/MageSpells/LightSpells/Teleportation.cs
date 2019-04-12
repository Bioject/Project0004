using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : LightSpells {

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

        switch (Player.direction)
        {
            case Player.Direction.Up:
                TeleUp();
                break;
            case Player.Direction.Down:
                TeleDown();
                break;
            case Player.Direction.Left:
                TeleLeft();
                break;
            case Player.Direction.Right:
                TeleRight();
                break;
        }    
    }

    private void TeleUp()
    {
        for (int i = SpellRange; i > 0; i--)
        {
            if (Player.GetRow() + i <= GameManager.NodeManager.rows - 1)
            {//checking to see if the node we are going to teleport to is occupied
                if (!GameManager.NodeManager.Node(Player.GetColumn(), Player.GetRow() + i).occupied)
                {
                    Player.SetGrid(Player.GetColumn(), Player.GetRow() + i);
                    Character.CharacterStats.Mana -= SpellCost;
                    return;
                }//end of nested if
            }//end of if
        }//end of forloop
    }//end of up

    private void TeleDown()
    {
        for (int i = SpellRange; i > 0; i--)
        {
            if (Player.GetRow() - i >= 0)
            {//checking to see if the node we are going to teleport to is occupied
                if (!GameManager.NodeManager.Node(Player.GetColumn(), Player.GetRow() - i).occupied)
                {
                    Player.SetGrid(Player.GetColumn(), Player.GetRow() - i);
                    Character.CharacterStats.Mana -= SpellCost;
                    return;
                }//end of nested if
            }//end of if
        }//end of forloop
    }//end of up
    private void TeleLeft()
    {
        for (int i = SpellRange; i > 0; i--)
        {
            if (Player.GetColumn() - i >= 0)
            {//checking to see if the node we are going to teleport to is occupied
                if (!GameManager.NodeManager.Node(Player.GetColumn() - i, Player.GetRow()).occupied)
                {
                    Player.SetGrid(Player.GetColumn() - i, Player.GetRow());
                    Character.CharacterStats.Mana -= SpellCost;
                    return;
                }//end of nested if
            }//end of if
        }//end of forloop
    }//end of up
    private void TeleRight()
    {
        for (int i = SpellRange; i > 0; i--)
        {
            if (Player.GetColumn() + i <= GameManager.NodeManager.columns - 1)
            {//checking to see if the node we are going to teleport to is occupied
                if (!GameManager.NodeManager.Node(Player.GetColumn() + i, Player.GetRow()).occupied)
                {
                    Player.SetGrid(Player.GetColumn() + i, Player.GetRow());
                    Character.CharacterStats.Mana -= SpellCost;
                    return;
                }//end of nested if
            }//end of if
        }//end of forloop
    }//end of up
}