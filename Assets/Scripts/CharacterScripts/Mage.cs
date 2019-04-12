using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mage : Character, IDamageable{

    private void Start()
    {
        CharacterStats.Mana = CharacterStats.MaxMana;
    }

    public override void CastSpell()
    {
    }

    public override void Attack()
    {
    }

    public override void TakeDamage(int damage)
    {
        damage -= CharacterStats.Armor;
        if (damage <= 2)
            damage = 2;

        CharacterStats.Health -= damage;


        if (CharacterStats.Health <= 0)
        {
            CharacterStats.Health = 0;
            //Destroy(gameObject);
        }

    }

    public override void UseItem()
    {
    }


    public override void LevelUp()
    {



        CharacterStats.MaxMana += 5;
        CharacterStats.Mana = CharacterStats.MaxMana;
        CharacterStats.MaxHealth += 5;
        CharacterStats.Health = CharacterStats.MaxHealth;

    }

    public override void UseMana(int value)
    {
        CharacterStats.Mana += value;
        if (CharacterStats.Mana >= CharacterStats.MaxMana)
            CharacterStats.Mana = CharacterStats.MaxMana;

    }

    public override void UseHealth(int value)
    {
        CharacterStats.Health += value;
        if (CharacterStats.Health >= CharacterStats.MaxHealth)
            CharacterStats.Health = CharacterStats.MaxHealth;
    }

    public override void UseAntidote()
    {
        CharacterStats.Adversities.CureAll();
    }

    public void TakeDamage(Object inCharacter)
    {
        BaseEnemy character = (BaseEnemy)inCharacter;

        int damage = Random.Range(character.MinAttackDamage, character.MaxAttackDamage + 1);
        int temp = CharacterStats.DamageAbsorber;
        CharacterStats.DamageAbsorber -= damage;
        if (CharacterStats.DamageAbsorber <= 0)
            CharacterStats.DamageAbsorber = 0;
        damage -= CharacterStats.DamageAbsorber;
        if (damage <= 0)
            return;

        damage -= CharacterStats.Armor;
        if (damage <= 2)
            damage = 2;

        //calculating the chance of landing a hit
        float chanceToMiss = (float)CharacterStats.Defense / ((float)CharacterStats.Defense + (float)character.AttackRating);
        if (chanceToMiss >= .95f)
            chanceToMiss = .95f;
        else if (chanceToMiss <= .05f)
            chanceToMiss = .05f;

        float value = Random.Range(0.0f, 1.0f);
        if (value >= chanceToMiss)
        {
            //cacluating critical chance
            //if (character.CharacterStats.CriticalChance >= 15)
                //character.CharacterStats.CriticalChance = 15;

            //int criticalValue = Random.Range(0, 101);
            //if (criticalValue >= character.CharacterStats.CriticalChance)
                //damage *= 2;

            CharacterStats.Health -= damage;
            if (CharacterStats.Health <= 0)
            {
                CharacterStats.Health = 0;
                print("Player is dead");
            }
            //healthBar.transform.localScale = new Vector3((float)Health / (float)MaxHealth, 1, 1);
            //healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);

        }
        else
            print("miss");
    }

    public void TakePoisonDamage(Object character, int value)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeColdDamage(Object character, int value)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeFireDamage(Object character, int value)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeLightDamage(Object character, int value)
    {
        //throw new System.NotImplementedException();
    }
}
