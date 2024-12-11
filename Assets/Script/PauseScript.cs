using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    public float pauseTime =4f; // 停止する時間（秒）

    void Start()
    {
        StartCoroutine(PauseForSeconds());
    }

    IEnumerator PauseForSeconds()
    {
        Time.timeScale = 0f; // 時間を停止

        // 指定した時間だけ待機
        yield return new WaitForSecondsRealtime(pauseTime);

        Time.timeScale = 1f; // 時間を通常速度に戻す

    }
}