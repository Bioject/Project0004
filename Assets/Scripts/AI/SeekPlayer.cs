using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeekPlayer : MonoBehaviour {

    AIManager ai;

    public SeekPlayer(AIManager inAI)
    {
        ai = inAI;
    }

    public Vector2 GetPlayerLocation()
    {
        return new Vector2(ai.player.GetColumn(), ai.player.GetRow());
    }

    public void GetPathToPlayer(int column, int row)
    {
        int cost = 0; //this keeps track of how many nodes were searched
        Vector2 target = GetPlayerLocation();
        List<Node> openNodes = new List<Node>();//keeping track of all the destinations
        List<Node> closedNodes = new List<Node>();//keeping track of all the destinations
        int i = 0;//use for incremetation

        do
        {
            i++;
            closedNodes.Add(ai.nodeManager.allNodes[column, row]);
            openNodes.Remove(ai.nodeManager.allNodes[column, row]);

            if (closedNodes[i].occupant is Player)
                break;

            foreach (Node n in ai.nodeManager.AdjecentNodes(column, row))
            {
                if (closedNodes.Contains(n))
                    continue;
                if (openNodes.Contains(n))
                    openNodes.Add(n);
                else
                {

                }
            }

        }
        while (!openNodes.Any());
    }


    //gets the distance from the player
    public Vector2 GetDistance(Vector2 pos)
    {
        //making sure the distance is always a positive number
        //for examlpe 7-5 is +3 and 5-7 is still +3. Both are three values away from their destination
        int x = (int)pos.x - ai.player.GetColumn();
        x = ~x + 1;
        int y = (int)pos.y - ai.player.GetRow();
        y = ~y + 1;

        return new Vector2(x,y);
    }
}
