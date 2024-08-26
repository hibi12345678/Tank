using UnityEngine;
using UnityEngine.UI;

public class SelectableText : MonoBehaviour
{
    // Textコンポーネントへの参照
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // ボタンがクリックされたときに呼ばれるメソッド
    public void OnButtonClick()
    {
        // テキストが選択されたときの処理
        Debug.Log("Text selected: " + textComponent.text);
    }
}