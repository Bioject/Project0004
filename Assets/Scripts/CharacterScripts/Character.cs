using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private string characterName;
    public string CharacterName { get { return characterName; } set { characterName = value;  } }

    [SerializeField]
    public enum CharacterClass { Mage, Wizard, Witch, Cleric, Knight, Warrior, Archer}
    public CharacterClass characterClass;

    [SerializeField]
    private List<BaseSpells> spells = new List<BaseSpells>();
    [SerializeField]
    public List<BaseSpells> Spells { get { return spells; } set { spells = value; } }

    [SerializeField]
    private CharacterStats characterStats;
    public CharacterStats CharacterStats { get { return characterStats; } set { characterStats = value; } }

    [SerializeField]
    private Attributes attributes;
    public Attributes Attributes { get { return attributes; } set { attributes = value; } }

    [SerializeField]
    private Resistance resistance;
    public Resistance Resistance { get { return resistance; } set { resistance = value; } }

    [SerializeField]
    private Equipment equipment;
    public Equipment Equipment { get { return equipment; } set { equipment = value; } }


    //methods
    public abstract void CastSpell();
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
    public abstract void UseItem();
    public abstract void UseMana(int value);
    public abstract void UseHealth(int value);
    public abstract void UseAntidote();
    public abstract void LevelUp();
}

//resistance
[System.Serializable]
public class Resistance
{
    [SerializeField]
    private int fireResistance;
    public int FireResistance { get { return fireResistance; } set { fireResistance = value; } }

    [SerializeField]
    private int lightningResistance;
    public int LightningResistance { get { return lightningResistance; } set { lightningResistance = value; } }

    [SerializeField]
    private int coldResistance;
    public int ColdResistance { get { return coldResistance; } set { coldResistance = value; } }

    [SerializeField]
    private int poisonResistance;
    public int PoisonResistance { get { return PoisonResistance; } set { PoisonResistance = value; } }
}
[System.Serializable]
public class Attributes
{
    //attributes
    [SerializeField]
    private int strength;
    public int Strength { get { return strength; } set { strength = value; } }

    [SerializeField]
    private int constitution;
    public int Constitution { get { return constitution; } set { constitution = value; } }

    [SerializeField]
    private int intelligence;
    public int Intelligence { get { return intelligence; } set { intelligence = value; } }

    [SerializeField]
    private int agility;
    public int Agility { get { return agility; } set { agility = value; } }

    [SerializeField]
    private int dexterity;
    public int Dexterity { get { return dexterity; } set { dexterity = value; } }
}
[System.Serializable]
public class CharacterStats
{
    //properties
    [SerializeField]
    private int characterLevel;//the maximum experience until next level
    public int CharacterLevel { get { return characterLevel; } set { characterLevel = value; } }
    [SerializeField]
    private int maxExperience;//the maximum experience until next level
    public int MaxExperience { get { return maxExperience; } set { maxExperience = value; } }
    private int experience;
    public int Experience { get { return maxExperience; } set { maxExperience = value; } }
    [SerializeField]
    private int gold;//the maximum experience until next level
    public int Gold { get { return gold; } set { gold = value; } }
    [SerializeField]
    private int maxMana;
    public int MaxMana { get { return maxMana; } set { maxMana = value; } }
    private int mana;
    public int Mana { get { return mana; } set { mana = value; } }
    [SerializeField]
    private int maxHealth;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    private int health;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField]
    private int defense;//the chance to defense
    public int Defense { get { return defense; } set { defense = value; } }
    [SerializeField]
    private int damageAbsorber;//the chance to defense
    public int DamageAbsorber { get { return damageAbsorber; ; } set { damageAbsorber = value; } }
    [SerializeField]
    private int armor;//reduces damage
    public int Armor { get { return armor; } set { armor = value; } }
    [SerializeField]
    private int attackRating;//the chance to hit
    public int AttackRating { get { return (attackRating + modifiers.AttackRatingModifier)-adversities.CursedAttackRating; } set { attackRating = value; } }

    [Header("Damage")]
    [Space(3)]
    [SerializeField]
    private int minAttackDamage;
    [SerializeField]
    private int maxAttackDamage;
    public int MaxAttackDamage { get { return (maxAttackDamage + modifiers.DamageModifier) - adversities.CursedDamage; } set { maxAttackDamage = value; } }
    public int MinAttackDamage { get { return (minAttackDamage + modifiers.DamageModifier) - adversities.CursedDamage; } set { minAttackDamage = value; } }
    [Space(3)]
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return (attackSpeed - modifiers.AttackSpeedModifier)+adversities.CursedAttackSpeed; } set { attackSpeed = value; } }
    [SerializeField]
    private int attackRange;
    public int AttackRange { get { return (attackRange + modifiers.RangeModifier)-adversities.CursedRange; } set { attackRange = value; } }
    [SerializeField]
    private int criticalChance;
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    [SerializeField]
    private float healthRegen;
    public float HealthRegen { get { return healthRegen; } set { healthRegen = value; } }

    [SerializeField]
    private float manaRegen;
    public float ManaRegen { get { return manaRegen; } set { manaRegen = value; } }

    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get { return (movementSpeed+modifiers.MoveSpeedModifier)-adversities.CursedMovement; } set { movementSpeed = value; } }

    private Modifiers modifiers = new Modifiers();
    public Modifiers Modifiers { get { return modifiers; } set { modifiers = value; } }

    private Adversities adversities = new Adversities();
    public Adversities Adversities { get { return adversities; } set { adversities = value; } }

    public void AddExperience(int value)
    {
        experience += value;
        if (experience >= maxExperience)
        {
            GameManager.UIManager.CurXP = experience-maxExperience;
            characterLevel += 1;
            MaxExperience += 50;
            GameManager.UIManager.AddXPBar(0, maxExperience);
            Debug.Log("Gained a level!");
        }
        else
            GameManager.UIManager.AddXPBar(value, maxExperience);

    }
}
[System.Serializable]
public class Equipment
{
    [SerializeField]
    private BaseArmor armor;
    public BaseArmor Armor {  get { return armor; } set { armor = value; } }

    [SerializeField]
    private BaseWeapon weapon;
    public BaseWeapon Wepon { get { return weapon; } set { weapon = value; } }

    [SerializeField]
    private BaseShield shield;
    public BaseShield Shield { get { return shield; } set { shield= value; } }
    //public void change gear
}

[System.Serializable]
public class Adversities
{
    [SerializeField]
    private int cursedDefense;
    public int CursedDefense { get { return cursedDefense; } set { cursedDefense = value; } }

    [SerializeField]
    private int cursedAttackRating;
    public int CursedAttackRating { get { return cursedAttackRating; } set { cursedAttackRating = value; } }

    [SerializeField]
    private int cursedRange;
    public int CursedRange { get { return cursedRange; } set { cursedRange = value; } }

    [SerializeField]
    private int cursedDamage;
    public int CursedDamage { get { return cursedDamage; } set { cursedDamage = value; } }

    [SerializeField]
    private float cursedMovement;
    public float CursedMovement { get { return cursedMovement; } set { cursedMovement = value; } }

    [SerializeField]
    private float cursedAttackSpeed;
    public float CursedAttackSpeed { get { return cursedAttackSpeed; } set { cursedAttackSpeed = value; } }

    [SerializeField]
    private int poisonDamage;
    public int PoisonDamage { get { return poisonDamage; } set { poisonDamage = value; } }

    public void CureAll()
    {
        CursedAttackRating = 0;
        CursedAttackSpeed = 0;
        CursedDamage = 0;
        CursedDefense = 0;
        CursedMovement = 0;
        CursedRange = 0;
        PoisonDamage = 0;
    }

}

[System.Serializable]
public class Modifiers
{
    [SerializeField]
    private int damageModifier;
    public int DamageModifier { get { return damageModifier; } set { damageModifier = value; } }
    [SerializeField]
    private int rangeModifier;
    public int RangeModifier { get { return rangeModifier; } set { rangeModifier = value; } }
    [SerializeField]
    private int attackRatingModifier;//the chance to hit
    public int AttackRatingModifier { get { return attackRatingModifier; } set { attackRatingModifier = value; } }
    [SerializeField]
    private float attackSpeedModifier;
    public float AttackSpeedModifier { get { return attackSpeedModifier; } set { attackSpeedModifier = value; } }
    [SerializeField]
    private float moveSpeedModifier;
    public float MoveSpeedModifier { get { return moveSpeedModifier; } set { moveSpeedModifier = value; } }

} 
