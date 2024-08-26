using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCount : MonoBehaviour
{
    GameObject gameManager;
    GameManager gamemanager;
    public int typeNum;

    [SerializeField]
    [Tooltip("ìGêÌé‘")]
    private GameObject enemy;

    int b;

    // Start is called before the first frame update
    void Start()
    {
        b = 0;
        GameObject gameObj = GameObject.Find("GameManager");
        if (gameObj != null)
        {
            gamemanager = gameObj.GetComponent<GameManager>();
        }
 
    }

    // Update is called once per frame
    void Update()
    {

        // Check if enemy is null or inactive
        if ((enemy == null || !enemy.activeSelf) && gamemanager != null && b == 0)
        {       
            gamemanager.destCount[typeNum - 1]++;
            b = 1;
        }
    }
}