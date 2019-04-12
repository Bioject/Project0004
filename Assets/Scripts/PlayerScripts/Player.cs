using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //List<Character> characters = new List<Character>();
    //int characterIndex = 0; //keeps track of what player is currently selected
    [SerializeField]
    public Transform characterTransform;
    public Character character;
    public bool isMoving;
    public CombatScript combatScript;
    public enum Direction { Up, Down, Left, Right };
    public Direction direction;

    //IEnumerator lerp;

    private void Start()
    {
        combatScript = gameObject.GetComponent<CombatScript>();
    }

    [SerializeField]
    NodeManager nodeManager; 

    int column = 0;
    int row = 0;

    public int GetColumn()
    {
        return column;
    }
    public void SetColumn(int value)
    {
        column = value;
    }

    public int GetRow()
    {
        return row;
    }
    public void SetRow(int value)
    {
        row = value;
    }

	// Use this for initialization
	void Update() {
        if (isMoving || combatScript.IsAttacking())
            return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0.00f || y != 0.00f)
        {
            SetDirection(x,y);
            Vector2 move = nodeManager.GoTo(new Vector2(x, y), this, column, row);
            StartCoroutine(LerpPos(move));
        }
    }

    private void SetDirection(float x, float y)
    {
        if (x < 0)
            direction = Direction.Left;
        else if (x > 0)
            direction = Direction.Right;
        else if (y < 0)
            direction = Direction.Down;
        else if (y > 0)
            direction = Direction.Up;
        //print(direction);
    }

    public void ChangeDirection(string dir)
    {
        switch (dir)
        {
            default:
                Debug.LogError(dir+ "is invalid. Be sure to use capitalization for the direction in which you wish you to have the playe face (i.e. Up, Down, Left, and Right)");
                break;
            case "Up":
                direction = Direction.Up;
                break;
            case "Right":
                direction = Direction.Right;
                break;
            case "Left":
                direction = Direction.Left;
                break;
            case "Down":
                direction = Direction.Down;
                break;
        }
        print(direction);
    }

    IEnumerator LerpPos(Vector3 pos)
    {
        isMoving = true;
        if (characterTransform.localPosition.x < pos.x)
        {
            direction = Direction.Right;
            while (characterTransform.localPosition.x < pos.x)
            {
                characterTransform.transform.Translate(Vector3.right * character.CharacterStats.MovementSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else if (characterTransform.localPosition.x > pos.x)
        {
            direction = Direction.Left;
            while (characterTransform.localPosition.x > pos.x)
            {
                characterTransform.transform.Translate(Vector3.left * character.CharacterStats.MovementSpeed * Time.deltaTime);
                yield return null;
            };
        }
        else if (characterTransform.localPosition.y > pos.y)
        {
            direction = Direction.Down;
            while (characterTransform.localPosition.y > pos.y)
            {
                characterTransform.transform.Translate(Vector3.down * character.CharacterStats.MovementSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else if (characterTransform.localPosition.y < pos.y)
        {
            direction = Direction.Up;
            while (characterTransform.localPosition.y < pos.y)
            {
                characterTransform.transform.Translate(Vector3.up * character.CharacterStats.MovementSpeed * Time.deltaTime);
                yield return null;
            }
        }
            characterTransform.transform.localPosition = pos;
            isMoving = false;     
    }

    public void SetGrid(int columns, int rows)
    {
        StopAllCoroutines();
        isMoving = false;
        GameManager.NodeManager.ClearNode(column, row);
        column = columns;
        row = rows;
        characterTransform.localPosition = GameManager.NodeManager.SetPosition(this, column, row);
    }
}
