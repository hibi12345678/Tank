using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Quit : MonoBehaviour
{

    public GameObject escapeUI;
    public void UIClose()
    {

        EventSystem.current.SetSelectedGameObject(null);  // �t�H�[�J�X����x����
        escapeUI.SetActive(false);
    }

    public void LeaveGame()
    {
        // ���[������ޏo����
        SceneManager.LoadScene("StartScene");

    }
    public void QuitGame()
    {   
        // �A�v���P�[�V�������I������
        Application.Quit();

        // Unity�G�f�B�^���ł̓���m�F�p�i�r���h��͕s�v�j
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
