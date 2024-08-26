using UnityEngine;
using UnityEngine.UI;

public class SelectableText : MonoBehaviour
{
    // Text�R���|�[�l���g�ւ̎Q��
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnButtonClick()
    {
        // �e�L�X�g���I�����ꂽ�Ƃ��̏���
        Debug.Log("Text selected: " + textComponent.text);
    }
}