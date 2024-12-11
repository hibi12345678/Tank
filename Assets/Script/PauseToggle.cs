using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject gameObject;
    void Update()
    {
        // ESCキーが押されたかを確認
        // ESCキーが押されたかを確認
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (isPaused && Time.timeScale == 0f)
        {
            gameObject.SetActive(false);

            // 再開
            Cursor.visible =false;
            Time.timeScale = 1f;
            isPaused = false;
        }

        else if (!isPaused && Time.timeScale == 1.0f)
        {
            Cursor.visible = true;
            gameObject.SetActive(true);
            // 一時停止
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}