using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseKasoru: MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Cursor.visible = false;

    }

    void Update()
    {
        mouse = Input.mousePosition;
        target = new Vector3(mouse.x, mouse.y, 10);
        this.transform.position = target;

        if (currentScene.name == "StartScene" )
        {
            Cursor.visible = true;
        }
    }
}