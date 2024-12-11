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

        EventSystem.current.SetSelectedGameObject(null);  // フォーカスを一度解除
        escapeUI.SetActive(false);
    }

    public void LeaveGame()
    {
        // ルームから退出する
        SceneManager.LoadScene("StartScene");

    }
    public void QuitGame()
    {   
        // アプリケーションを終了する
        Application.Quit();

        // Unityエディタ内での動作確認用（ビルド後は不要）
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
