using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMain : MonoBehaviour
{
    void Start()
    {
        LordScene();
    }

    public void LordScene()
    {
        Invoke("ChangeScene", 4.0f);
    }
    
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}
