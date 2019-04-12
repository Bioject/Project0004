using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Transform xpBar;
    int curXp = 0;
    public int CurXP { get { return curXp; } set { curXp = value;  } }

    public void AddXPBar(int value, int maxXp)
    {
        CurXP += value;
        xpBar.localScale = new Vector3((float)CurXP / (float)maxXp,1, 1);
    }
}
