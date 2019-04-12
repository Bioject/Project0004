using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    [SerializeField]
    private int maxHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private int health;
    public int Health { get { return health; } set { health = value; } }

    [SerializeField]
    private int defense;
    public int Defense { get { return defense; } set { defense = value; } }

    [SerializeField]
    private int attackRating;
    public int AttackRating { get { return attackRating; } set { attackRating = value; } }

    [SerializeField]
    private int armor;
    public int Armor { get { return armor; } set { armor = value; } }

    [SerializeField]
    private int minAttackDamage;
    public int MinAttackDamage { get { return minAttackDamage; } set { minAttackDamage = value; } }

    [SerializeField]
    private int maxAttackDamage;
    public int MaxAttackDamage { get { return maxAttackDamage; } set { maxAttackDamage = value; } }

    [SerializeField]
    Resistance resistance;
    public Resistance Resistance { get { return resistance; } set { resistance = value; } }

    [SerializeField]
    private int column;
    public int Column { get { return column; } set { column = value; } }

    [SerializeField]
    private int row;
    public int Row { get { return row; } set { row = value; } }
}
