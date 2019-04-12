using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager instance;

    [SerializeField]
    NodeManager nodeManager;
    public static NodeManager NodeManager { get { return instance.nodeManager; } set { instance.nodeManager = value; } }

    [SerializeField]
    UIManager uiManager;
    public static UIManager UIManager { get { return instance.uiManager;  } set { instance.uiManager = value;  } }

    private void Awake()
    {
        instance = gameObject.GetComponent<GameManager>();
    }
}
