using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    [SerializeField]
    public Player player;
    [SerializeField]
    public NodeManager nodeManager;

    AIManager instance;

    SeekPlayer seekPlayer;

    private void Start()
    {
        instance = gameObject.GetComponent<AIManager>();
        seekPlayer = new SeekPlayer(this.instance);
    }
}
