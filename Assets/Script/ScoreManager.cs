using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameNamespace; // ���O��Ԃ��C���|�[�g

public class ScoreManager : MonoBehaviour
{
    public Text textComponent1; // �e�L�X�g�R���|�[�l���g�ւ̎Q��
    public Text textComponent2;

    // �N���X���x���Ŕz����`
    int[] array = { 1, 3, 3, 4, 4, 5, 6, 6, 5, 6, 4, 6, 6, 7, 7, 7, 7, 7, 8, 8 };

    void Start()
    {
        int currentScore = GlobalVariables.stageNumber +1;

        // �e�L�X�g�R���|�[�l���g�ɒl��ݒ�
        textComponent1.text = "�X�e�[�W : " + currentScore;
        textComponent2.text = "�G���" + array[currentScore - 1] + "��";
    }
}