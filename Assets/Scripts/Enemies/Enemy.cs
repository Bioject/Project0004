using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy, IDamageable {

    public NodeManager nodeManager;
    [SerializeField]
    int experience = 15;
    int gold = 3;

    [SerializeField]
    UnityEngine.UI.Image healthBar;

    private void Start()
    {
        Health = MaxHealth;
        healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);
        transform.position = nodeManager.SetPosition(gameObject.GetComponent<Enemy>(), Column, Row);
    }


    public void TakeDamage(Object inCharacter)
    {
        Character character = (Character)inCharacter;

        int damage = Random.Range(character.CharacterStats.MinAttackDamage, character.CharacterStats.MaxAttackDamage + 1);

        damage -= Armor;
        if (damage <= 2)
            damage = 2;

        //calculating the chance of landing a hit
        float chanceToMiss = (float)Defense / ((float)Defense + (float)character.CharacterStats.AttackRating);
        if (chanceToMiss >= .95f)
            chanceToMiss = .95f;
        else if (chanceToMiss <= .05f)
            chanceToMiss = .05f;

        float value = Random.Range(0.0f, 1.0f);
        if (value >= chanceToMiss)
        {
            //cacluating critical chance
            if (character.CharacterStats.CriticalChance >= 15)
                character.CharacterStats.CriticalChance = 15;

            int criticalValue = Random.Range(0, 101);
            if (criticalValue >= character.CharacterStats.CriticalChance)
                damage *= 2;

            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                character.CharacterStats.Gold += gold;
                character.CharacterStats.AddExperience(experience);
                nodeManager.ClearNode(Column, Row);
                Destroy(gameObject);
            }
            healthBar.transform.localScale = new Vector3((float)Health / (float)MaxHealth, 1, 1);
            healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);

        }
        else
            print("miss");
    }

    public void TakePoisonDamage(Object inCharacter, int damage)
    {
        Character character = (Character)inCharacter;
    }

    public void TakeColdDamage(Object inCharacter, int damage)
    {
        Character character = (Character)inCharacter;

        damage -= Resistance.FireResistance;
        if (damage <= 2)
            damage = 2;
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            character.CharacterStats.Gold += gold;
            character.CharacterStats.AddExperience(experience);
            nodeManager.ClearNode(Column, Row);
            Destroy(gameObject);
        }
        healthBar.transform.localScale = new Vector3((float)Health / (float)MaxHealth, 1, 1);
        healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);
    }

    public void TakeLightDamage(Object inCharacter, int damage)
    {
        Character character = (Character)inCharacter;

        damage -= Resistance.LightningResistance;
        if (damage <= 2)
            damage = 2;
        Health -= damage;


        if (Health <= 0)
        {
            Health = 0;
            character.CharacterStats.Gold += gold;
            character.CharacterStats.AddExperience(experience);
            nodeManager.ClearNode(Column, Row);
            Destroy(gameObject);
        }
        healthBar.transform.localScale = new Vector3((float)Health / (float)MaxHealth, 1, 1);
        healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);
    }

    public void TakeFireDamage(Object inCharacter, int damage)
    {
        Character character = (Character)inCharacter;

        damage -= Resistance.FireResistance;
        if (damage <= 2)
            damage = 2;
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            character.CharacterStats.Gold += gold;
            character.CharacterStats.AddExperience(experience);
            nodeManager.ClearNode(Column, Row);
            Destroy(gameObject);
        }
        healthBar.transform.localScale = new Vector3((float)Health / (float)MaxHealth, 1, 1);
        healthBar.GetComponent<UnityEngine.UI.Image>().color = new Color(Mathf.Clamp((1 - (float)Health / (float)MaxHealth), 0, 1), Mathf.Clamp(((float)Health / (float)MaxHealth), 0, 1), 0, 0.5f);
    } 
}
