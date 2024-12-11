using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MyGameNamespace; 

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    GameObject clearUI;  
    
    [SerializeField]
    GameObject allClearUI; 
    
    [SerializeField]
    GameObject button;

    [SerializeField]
    GameObject sensya;

    AudioSource bgm;
    AudioSource ending;
    AudioSource source;
    AudioSource clearBGM;

    bool isDestroyed = false;

    public GameObject[] enemyPrefabs; // 配列の宣言と初期化
    public Text[] textComponents;
    public Text totalScore;
    int randomElement;
    string obstacleName;
    int[] row;
    int randomElementIndex;
    int k;
    Vector3 spawnPosition;
    int enemyNum;
    bool clearflag;
    bool endflag;

    public  int[] destCount;
    public static int[] desCou = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    int total;
    private bool[] hasProcessed;
    int b;

    void Start()
    {
        destCount = new int[] { 0, 0, 0, 0, 0, 0, 0 ,0};
        
        enemyNum = 0;
        
        GlobalVariables.stageNumber++;

        b = 0;
        k = 0;
        AudioSource[] audioSource = GetComponents<AudioSource>();
        bgm = audioSource[0];
        ending = audioSource[1];
        source = audioSource[2];
        

        clearflag = false;
        endflag = false;
 
        Invoke("BGMPlay", 0.1f);

        // フラグ配列を初期化 (敵の数と同じサイズ)
        int[][] enemyNumber = new int[][]
        {
             new int[] { 1, 0, 0, 0, 0, 0, 0, 0 },
             new int[] { 1, 2, 0, 0, 0, 0, 0, 0 },
             new int[] { 0, 3, 0, 0, 0, 0, 0, 0 },
             new int[] { 0, 2, 2, 0, 0, 0, 0, 0 },
             new int[] { 0, 0, 4, 0, 0, 0, 0, 0 },
             new int[] { 0, 0, 2, 3, 0, 0, 0, 0 },
             new int[] { 0, 2, 2, 2, 0, 0, 0, 0 },
             new int[] { 0, 0, 3, 3, 0, 0, 0, 0 },
             new int[] { 0, 2, 0, 0, 0, 3, 0, 0 },
             new int[] { 0, 0, 2, 0, 0, 4, 0, 0 },
             new int[] { 0, 0, 0, 0, 4, 0, 0, 0 },
             new int[] { 0, 0, 0, 2, 4, 0, 0, 0 },
             new int[] { 0, 0, 2, 0, 4, 0, 0, 0 },
             new int[] { 0, 0, 0, 0, 3, 4, 0, 0 },
             new int[] { 0, 2, 0, 2, 0, 0, 3, 0 },
             new int[] { 0, 0, 3, 0, 0, 0, 4, 0 },
             new int[] { 0, 0, 0, 0, 3, 0, 4, 0 },
             new int[] { 0, 0, 0, 2, 0, 2, 0, 3 },
             new int[] { 0, 0, 2, 0, 2, 0, 0, 4 },
             new int[] { 0, 0, 0, 0, 0, 0, 4, 4 }
        };

        DecideStageNumber();
            
        // 指定された数の Enemy を生成する
        for (int  j= 0; j<=7; j++ )
        {
            for (int i = 0; i < enemyNumber[GlobalVariables.stageNumber - 1][j]; i++)
            {
                
                DecideMap();
                
                k++;
                // Enemy を生成する
                GameObject newEnemy = Instantiate(enemyPrefabs[j], spawnPosition, Quaternion.identity);

                // Enemy の名前を設定する
                newEnemy.name = "Enemy" + k;
                newEnemy.tag = "Enemy" + k;
                
            }
        }
        hasProcessed = new bool[k + 1]; // k+1 とすることで 1 から k までのインデックスに対応
    }

    void Update()
    {
        
        

        for (int i = 1; i <= k; i++)
        {
            GameObject obj = GameObject.Find("Enemy" + i);
            if (obj != null)
            {
                EnemyMove enemyMoveComponent = obj.GetComponent<EnemyMove>();

                if (enemyMoveComponent != null && !enemyMoveComponent.enabled && !hasProcessed[i-1])
                {
                    enemyNum++;
                    source.Play();
                    hasProcessed[i-1] = true; // 処理済みフラグを立てる
                }
            }
        }

        if (enemyNum == k  && endflag == false)
        {
            clearflag = true;
            if (GlobalVariables.stageNumber == 20)
            {
                Invoke("Finish", 0.5f);
                
            }
            else
            {
                Invoke("Clear",0.5f);
            }
            
        }

        if (!isDestroyed && sensya.activeSelf == false && clearflag ==false )
        {

            if (b == 0)
            {
                for (int i = 0; i <= 7; i++)
                {
                    desCou[i] += destCount[i];
                    b = 1;
                }
            }
            endflag = true;
            isDestroyed = true;
            ending.Play();
            bgm.Pause();
            Invoke("ShowGameOverUI", 3f);


        }

    }

    public void BGMPlay()
    {
        bgm.Play();
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        button.SetActive(true);
        
        for (int i = 0; i <= 7; i++)
        {
            textComponents[i].text = desCou[i] + "台";
            
        }
        total = desCou[0] + desCou[1] + desCou[2] + desCou[3] + desCou[4] + desCou[5] + desCou[6] + desCou[7];
        totalScore.text = "総撃破数 : " + total;
        
    }

    void DecideStageNumber()
    {
        int[][] mapArray = new int[][]
        {
           new int[] { 1, 2},
           new int[] { 1, 2 },
           new int[] { 3 },
           new int[] { 4, 5, 6 },
           new int[] { 4, 5, 6 },
           new int[] { 4, 5, 6 },
           new int[] { 7, 8, 9 },
           new int[] { 7, 8, 9 },
           new int[] { 7, 8, 9 },
           new int[] { 7, 8, 9 },
           new int[] { 1, 2 },
           new int[] { 1, 2 },
           new int[] { 1, 2 },
           new int[] { 5, 6, 7, 8, 9 },
           new int[] { 5, 6, 7, 8, 9 },
           new int[] { 4 },
           new int[] { 3, 4, 5, 6, 7, 8, 9 },
           new int[] { 3, 4, 5, 6, 7, 8, 9 },
           new int[] { 4 },
           new int[] { 3, 4, 5, 6, 7, 8, 9 }

        };



        if (GlobalVariables.stageNumber >= 1 && GlobalVariables.stageNumber <= 20)
        {

            row = mapArray[GlobalVariables.stageNumber - 1];
            randomElementIndex = Random.Range(0, row.Length);
            randomElement = row[randomElementIndex];
            
            for(int i = 1; i < randomElement; i++ )
            {
                obstacleName = "Obstacle" + i.ToString();
                GameObject obstacle = GameObject.Find(obstacleName);
                obstacle.SetActive(false);
            }

            for (int i = randomElement + 1; i <= 9; i++)
            {
                obstacleName = "Obstacle" + i.ToString();
                GameObject obstacle = GameObject.Find(obstacleName);
                obstacle.SetActive(false);
            }

        }
    }

    public void Clear()
    {
        bgm.Pause();
          
        clearUI.SetActive(true);
        if (b == 0)
        {
            for (int i = 0; i <= 7; i++)
            {
                desCou[i] += destCount[i];
                b = 1;
            }
        }
        
        Invoke("SceneChange", 3.5f);
    }
    public void Finish()
    {
        bgm.Pause();
        allClearUI.SetActive(true);
        
        
        Invoke("UnShowAllClearUI", 3.0f);
        if (b == 0)
        {
            for (int i = 0; i <= 7; i++)
            {
                desCou[i] += destCount[i];
                b = 1;
            }
        }
        
        Invoke("ShowGameOverUI", 3.1f);
        
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("BetWeenScene");
    }
    public void UnShowAllClearUI()
    {
        allClearUI.SetActive(false);
    }
    public void DecideMap()
    {
        
        if (randomElement == 1)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = -1.5f;
            float minZ1 = -5.5f;
            float maxZ1 = -1.5f;
            
            // 範囲2の座標範囲
            float minX2 = 1.5f;
            float maxX2 = 8.5f;
            float minZ2 = -5.5f;
            float maxZ2 = -1.5f;
            
            // 範囲3の座標範囲
            float minX3 = 1.5f;
            float maxX3 = 8.5f;
            float minZ3 = 1.5f;
            float maxZ3 = 5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 4);
            
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);


            }
            else if (range == 1)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
                
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
                
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 2)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = 0.0f;
            float minZ1 = 4.8f;
            float maxZ1 = 5.5f;

            // 範囲2の座標範囲
            float minX2 = 0.0f;
            float maxX2 = 8.5f;
            float minZ2 = -5.5f;
            float maxZ2 = -4.8f;

            // 範囲3の座標範囲
            float minX3 = 1.5f;
            float maxX3 = 8.5f;
            float minZ3 = -2.0f;
            float maxZ3 = 5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 6);
            
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 2)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 3)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = -6.5f;
            float minZ1 = - 5.6f;
            float maxZ1 = 0.0f;

            // 範囲2の座標範囲
            float minX2 = -4.0f;
            float maxX2 =  0.0f;
            float minZ2 = -5.5f;
            float maxZ2 = -4.0f;

            // 範囲3の座標範囲
            float minX3 = 0.0f;
            float maxX3 = 8.5f;
            float minZ3 = 5.6f;
            float maxZ3 = 4.4f;

            // 範囲4の座標範囲
            float minX4 = 6.0f;
            float maxX4 = 8.8f;
            float minZ4 = 0.0f;
            float maxZ4 = 3.0f;

            // 範囲5の座標範囲
            float minX5 = 1.4f;
            float maxX5 = 8.8f;
            float minZ5 = - 1.0f;
            float maxZ5 = - 5.6f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 10);
            if (range == 1 || range == 2)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 3)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else if (range == 4)
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }
            else if (range ==5 || range == 6)
            {
                randomX = Random.Range(minX4, maxX4);
                randomZ = Random.Range(minZ4, maxZ4);
            }
            else
            {
                randomX = Random.Range(minX5, maxX5);
                randomZ = Random.Range(minZ5, maxZ5);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 4)
        {
            // 範囲1の座標範囲
            float minX1 = 0.0f;
            float maxX1 = 8.5f;
            float minZ1 = -1.7f;
            float maxZ1 = 1.7f;

            // 範囲2の座標範囲
            float minX2 = -8.5f;
            float maxX2 = 8.5f;
            float minZ2 = -5.6f;
            float maxZ2 = -4.3f;

            // 範囲3の座標範囲
            float minX3 = -8.5f;
            float maxX3 = 8.5f;
            float minZ3 = 4.3f;
            float maxZ3 = 5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 8);
            if (range == 1 || range == 2)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 3|| range == 4)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 5)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 =  -1.5f;
            float minZ1 =  4.0f;
            float maxZ1 =  5.6f;

            // 範囲2の座標範囲
            float minX2 = -8.5f;
            float maxX2 =  -1.5f;
            float minZ2 = -1.2f;
            float maxZ2 =  1.2f;

            // 範囲3の座標範囲
            float minX3 = 1.5f;
            float maxX3 = 8.5f;
            float minZ3 = -1.5f;
            float maxZ3 = -5.5f;

            // 範囲4の座標範囲
            float minX4 = 1.5f;
            float maxX4 = 8.5f;
            float minZ4 = 1.5f;
            float maxZ4 = 5.5f;

            // 範囲5の座標範囲
 

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 5);
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 2)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else if (range == 3)
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }
            else
            {
                randomX = Random.Range(minX4, maxX4);
                randomZ = Random.Range(minZ4, maxZ4);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if( randomElement == 6)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = -1.5f;
            float minZ1 = -5.5f;
            float maxZ1 = -1.5f;

            // 範囲2の座標範囲
            float minX2 = 1.5f;
            float maxX2 = 8.5f;
            float minZ2 = -5.5f;
            float maxZ2 = -1.5f;

            // 範囲3の座標範囲
            float minX3 = 1.5f;
            float maxX3 = 8.5f;
            float minZ3 = 1.5f;
            float maxZ3 = 5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 4);
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 2)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 7)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = 0.5f;
            float minZ1 = -0.5f;
            float maxZ1 = 5.5f;

            // 範囲2の座標範囲
            float minX2 = -0.5f;
            float maxX2 = 8.5f;
            float minZ2 = 5.5f;
            float maxZ2 = -0.5f;

            // 範囲3の座標範囲
            float minX3 = 3.5f;
            float maxX3 = 8.5f;
            float minZ3 = 4.0f;
            float maxZ3 = 5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 6);
            if (range == 1 || range == 2)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 3 || range == 4)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else if (randomElement == 8)
        {
            // 範囲1の座標範囲
            float minX1 = -8.5f;
            float maxX1 = -2.8f;
            float minZ1 = -1.0f;
            float maxZ1 = 1.0f;

            // 範囲2の座標範囲
            float minX2 = -8.5f;
            float maxX2 = -2.8f;
            float minZ2 = 4.0f;
            float maxZ2 = 5.5f;

            // 範囲3の座標範囲
            float minX3 = 2.8f;
            float maxX3 = 8.5f;
            float minZ3 = -1.0f;
            float maxZ3 = 1.0f;

            // 範囲4の座標範囲
            float minX4 = 2.8f;
            float maxX4 = 8.5f;
            float minZ4 = 4.0f;
            float maxZ4 = 5.5f;

            // 範囲5の座標範囲
            float minX5 = -2.0f;
            float maxX5 = 2.0f;
            float minZ5 = 1.5f;
            float maxZ5 = 5.5f;

            // 範囲6の座標範囲
            float minX6 = 2.8f;
            float maxX6 = 8.5f;
            float minZ6 = -4.0f;
            float maxZ6 = -5.5f;

            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 6);
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 2)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else if (range == 3)
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }
            else if (range == 4)
            {
                randomX = Random.Range(minX4, maxX4);
                randomZ = Random.Range(minZ4, maxZ4);
            }
            else if( range == 5)
            {
                randomX = Random.Range(minX5, maxX5);
                randomZ = Random.Range(minZ5, maxZ5);
            }

            else
            {
                randomX = Random.Range(minX6, maxX6);
                randomZ = Random.Range(minZ6, maxZ6);
            }

            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }

        else
        {
            // 範囲1の座標範囲
            float minX1 = -4.0f;
            float maxX1 = 4.0f;
            float minZ1 = -1.7f;
            float maxZ1 = 1.7f;

            // 範囲2の座標範囲
            float minX2 = 7.5f;
            float maxX2 = 8.5f;
            float minZ2 = -5.5f;
            float maxZ2 = 5.5f;

            // 範囲3の座標範囲
            float minX3 = -8.8f;
            float maxX3 = 7.5f;
            float minZ3 = 4.4f;
            float maxZ3 = 5.5f;


            // ランダムな座標を生成
            float randomX;
            float randomZ;

            // 3つの範囲からランダムな座標を選択
            int range = Random.Range(1, 6);
            if (range == 1)
            {
                randomX = Random.Range(minX1, maxX1);
                randomZ = Random.Range(minZ1, maxZ1);
            }
            else if (range == 2)
            {
                randomX = Random.Range(minX2, maxX2);
                randomZ = Random.Range(minZ2, maxZ2);
            }
            else
            {
                randomX = Random.Range(minX3, maxX3);
                randomZ = Random.Range(minZ3, maxZ3);
            }
  
            // EnemyObject を生成
            spawnPosition = new Vector3(randomX, 0f, randomZ);
            
        }
    }

}
