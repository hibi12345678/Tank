using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyGameNamespace;

public class SceneChange : MonoBehaviour
{
    string currentSceneName;
    public void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "StartScene")
        {
            GlobalVariables.stageNumber = 0;
            
        }

    }

    void Update()
    {
        
        
        
    }

    public void LordScene()
    {

        
        if (currentSceneName == "StartScene")
        {
            Invoke("ChangeScene", 1.5f);

        }
        else
        {
            ChangeScene();
            
        }

    }

    public void ChangeScene()
    {

            
        if (currentSceneName == "StartScene")
        {
            SceneManager.LoadScene("BetweenScene");
    
        }
        else
        {
            SceneManager.LoadScene("StartScene");

        }
    }
  
}