using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour {

    Player player;
    [SerializeField]
    NodeManager nodeManager;
    Character character;

    float lastAttack;

    UnityEngine.Object target = null;


    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        character = player.character;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && lastAttack <= Time.time)
            Attack();
    }

    private void Attack()
    {

        lastAttack = Time.time + player.character.CharacterStats.AttackSpeed;

            switch (player.direction)
            {
                case Player.Direction.Right:
                    target = nodeManager.AttackNode(player.GetColumn() + 1, player.GetRow());
                    break;
                case Player.Direction.Left:
                    target = nodeManager.AttackNode(player.GetColumn() - 1, player.GetRow());
                    break;
                case Player.Direction.Up:
                    target = nodeManager.AttackNode(player.GetColumn(), player.GetRow() + 1);
                    break;
                case Player.Direction.Down:
                    target = nodeManager.AttackNode(player.GetColumn(), player.GetRow() - 1);
                    break;
            }
        if (target is IDamageable)
        {
            IDamageable t = (IDamageable)target;
            t.TakeDamage(player.character);
        }    
     }

    public bool IsAttacking()
    {
        if (lastAttack + .2f > Time.time)
            return true;
        else
            return false;
    }

    public void SetAttacking(bool state)
    {
        if (state)
            lastAttack = Time.time + 100000000000;
        else
            lastAttack = 0;
    }
}
