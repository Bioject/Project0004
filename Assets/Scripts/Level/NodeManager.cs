using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

    [SerializeField]
    float offSet;
    [SerializeField]
    public int rows;
    [SerializeField]
    public int columns;

    [SerializeField]
    GameObject node;

    [SerializeField]
    Player player;

    public Node[,] allNodes;

    private void Awake()
    {
        allNodes = new Node[columns, rows];

        for (int c = 0; c < columns; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                GameObject clone;
                Vector3 pos = new Vector3(c * offSet, r * offSet, 0);
                clone = Instantiate(node, pos, Quaternion.identity);

                clone.transform.SetParent(transform);
                clone.name = "[" + c + "]" + ", " + "[" + r + "]";
                Node nodes = clone.GetComponent<Node>();
                allNodes[c, r] = nodes;
                nodes.column = c;
                nodes.row = r;
            }
        }
    }

    public Vector3 GoTo(Vector2 dir, Object occupant, int column, int row)
    {
        //going up
        if (dir.y > 0)
        {
            if (row < rows - 1)
            {
                if (!allNodes[column, row + 1].occupied)
                {
                    allNodes[column, row].occupied = false;
                    allNodes[column, row + 1].occupied = true;
                    allNodes[column, row + 1].occupant = occupant;
                    if (occupant is Player)
                    {
                        Player obj = (Player)occupant;
                        obj.SetColumn(column);
                        obj.SetRow(row + 1);
                    }
                    return allNodes[column, row + 1] .transform.position;
                }
                else
                    return allNodes[column, row].transform.position;
            }
            else
                return allNodes[column, row].transform.position;
        }

        //going down
        if (dir.y < 0)
        {
            if (row > 0)
            {
                if (!allNodes[column, row - 1].occupied)
                {
                    allNodes[column, row].occupied = false;
                    allNodes[column, row - 1].occupied = true;
                    allNodes[column, row - 1].occupant = occupant;
                    if (occupant is Player)
                    {
                        Player obj = (Player)occupant;
                        obj.SetColumn(column);
                        obj.SetRow(row - 1);
                    }
                    return allNodes[column, row - 1].transform.position;
                }
                else
                    return allNodes[column, row].transform.position;
            }
            else
                return allNodes[column, row].transform.position;
        }

        //going left
        if (dir.x < 0)
        {
            if (column > 0)
            {
                if (!allNodes[column - 1, row].occupied)
                {
                    allNodes[column, row].occupied = false;
                    allNodes[column - 1, row].occupied = true;
                    allNodes[column - 1, row].occupant = occupant;
                    if (occupant is Player)
                    {
                        Player obj = (Player)occupant;
                        obj.SetColumn(column - 1);
                        obj.SetRow(row);
                    }
                    return allNodes[column - 1, row].transform.position;
                }
                else
                    return allNodes[column, row].transform.position;
            }
            else
                return allNodes[column, row].transform.position;
        }

        //going right
        if (dir.x > 0)
        {
            if (column < columns - 1)
            {
                if (!allNodes[column + 1, row].occupied)
                {
                    allNodes[column, row].occupied = false;
                    allNodes[column + 1, row].occupied = true;
                    allNodes[column + 1, row].occupant = occupant;
                    if (occupant is Player)
                    {
                        Player obj = (Player)occupant;
                        obj.SetColumn(column + 1);
                        obj.SetRow(row);
                    }
                    return allNodes[column + 1, row].transform.position;
                }
                else
                    return allNodes[column, row].transform.position;
            }
            else
                return allNodes[column, row].transform.position;
        }
        return allNodes[column, row].transform.position;
    }

    public Object AttackNode(int column, int row)
    {
        if ((column >= 0 && column < columns -1) && (row >= 0 && row < rows - 1))
        {
            return allNodes[column, row].occupant;
        }
        else
            return null;
    }

    public Node Node(int column, int row)
    {
        return allNodes[column, row];
    }

    public Vector3 SetPosition(UnityEngine.Object go, int column, int row)
    {
        allNodes[column, row].occupied = true;
        allNodes[column, row].occupant = go;
        return allNodes[column, row].transform.position;
    }

    public void ClearNode(int column, int row)
    {
        allNodes[column, row].occupied = false;
        allNodes[column, row].occupant = null;
    }

    public List<Node> AdjecentNodes(int column, int row)
    {
        List<Node> list = new List<Node>();

        //get up
        if ((column + 1 >= 0 && column + 1 < columns - 1) && (row >= 0 && row < rows - 1))
        {
                list.Add(allNodes[column + 1, rows]);
        }

        //get down
        if ((column - 1 >= 0 && column - 1 < columns - 1) && (row >= 0 && row < rows - 1))
        {
                list.Add(allNodes[column - 1, rows]);
        }

        //get right
        if ((column >= 0 && column < columns - 1) && (row + 1>= 0 && row + 1 < rows - 1))
        {
                list.Add(allNodes[column, rows + 1]);
        }
        //get left
        if ((column >= 0 && column < columns - 1) && (row - 1 >= 0 && row - 1 < rows - 1))
        {
                list.Add(allNodes[column, rows - 1]);
        }
        return list;
    }
}
