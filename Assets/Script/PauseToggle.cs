using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject gameObject;
    void Update()
    {
        // ESC�L�[�������ꂽ�����m�F
        // ESC�L�[�������ꂽ�����m�F
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

            // �ĊJ
            Cursor.visible =false;
            Time.timeScale = 1f;
            isPaused = false;
        }

        else if (!isPaused && Time.timeScale == 1.0f)
        {
            Cursor.visible = true;
            gameObject.SetActive(true);
            // �ꎞ��~
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}