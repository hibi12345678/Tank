using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    public float pauseTime =4f; // ��~���鎞�ԁi�b�j

    void Start()
    {
        StartCoroutine(PauseForSeconds());
    }

    IEnumerator PauseForSeconds()
    {
        Time.timeScale = 0f; // ���Ԃ��~

        // �w�肵�����Ԃ����ҋ@
        yield return new WaitForSecondsRealtime(pauseTime);

        Time.timeScale = 1f; // ���Ԃ�ʏ푬�x�ɖ߂�

    }
}