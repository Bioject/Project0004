using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseSpells : MonoBehaviour { 

    private Character character;//determiens whether or not the skill is locked and can be used
    public Character Character { get { return character; } set { character = value; } }

    private Player player;//determiens whether or not the skill is locked and can be used
    public Player Player { get { return player; } set { player = value; } }

    [SerializeField]
    private string spellName;
    public string SpellName { get { return spellName; } set { spellName = value; } }

    [SerializeField]
    private int levelRequired;
    public int LevelRequired { get { return levelRequired; } set { levelRequired = value; } }

    [SerializeField]
    private int spellLevel;
    public int SpellLevel { get { return spellLevel; } set { spellLevel = value; } }

    [SerializeField]
    private int spellCost;
    public int SpellCost { get { return spellCost; } set { spellCost = value; } }

    [SerializeField]
    private int coolDown;
    public int CoolDown { get { return coolDown; } set { coolDown = value; } }

    [SerializeField]
    private int spellDuration;
    public int SpellDuration { get { return spellDuration; } set { spellDuration = value; } }

    [SerializeField]
    private int spellRange;
    public int SpellRange { get { return spellRange; } set { spellRange = value; } }

    [SerializeField]
    private bool locked;//determiens whether or not the skill is locked and can be used
    public bool Locked { get { return locked; } set { locked = value; } }

    public abstract void CastSpell();
}
