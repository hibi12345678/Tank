using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGameNamespace; // 名前空間をインポート

public class ScoreManager : MonoBehaviour
{
    public Text textComponent1; // テキストコンポーネントへの参照
    public Text textComponent2;

    // クラスレベルで配列を定義
    int[] array = { 1, 3, 3, 4, 4, 5, 6, 6, 5, 6, 4, 6, 6, 7, 7, 7, 7, 7, 8, 8 };

    void Start()
    {
        int currentScore = GlobalVariables.stageNumber +1;

        // テキストコンポーネントに値を設定
        textComponent1.text = "ステージ : " + currentScore;
        textComponent2.text = "敵戦車" + array[currentScore - 1] + "台";
    }
}