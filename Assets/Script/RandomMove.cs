using System.Collections;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float moveSpeed = 2f;         // 移動速度
    public float moveTime = 1f;          // 移動時間
    public float waitTime = 1f;          // 待機時間
    public Vector3 areaSize = new Vector3(10, 10, 10);  // 移動範囲
    GameObject obj;             // プレイヤーオブジェクト
    public float avoidDistance = 5f;     // プレイヤーから離れる距離
    Transform player;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(MoveRandomly());
        obj  = GameObject.Find("Player");
        player= obj.transform;
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (!isMoving)
            {
                // ランダムなターゲット位置を設定
                targetPosition = new Vector3(
                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                    Random.Range(-areaSize.y / 2, areaSize.y / 2),
                    Random.Range(-areaSize.z / 2, areaSize.z / 2)
                );

                // プレイヤーから一定距離以内なら離れる位置に設定
                if (Vector3.Distance(transform.position, player.position) < avoidDistance)
                {
                    Vector3 directionToPlayer = transform.position - player.position;
                    targetPosition = transform.position + directionToPlayer.normalized * avoidDistance;
                }

                // ターゲット位置に移動する
                isMoving = true;
                yield return StartCoroutine(MoveToPosition(targetPosition));

                // 待機
                yield return new WaitForSeconds(waitTime);
                isMoving = false;
            }
            yield return null;
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }

    // デバッグ用に範囲を表示
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, areaSize);
    }
}