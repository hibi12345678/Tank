using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public float avoidDistance = 5.0f;// プレイヤーから離れる距離
    
    public float randomMoveRadius = 10.0f; // ランダム移動の半径
    public float checkInterval = 1.0f; // 行動を切り替える間隔
    GameObject obj;             // プレイヤーオブジェクト
    Transform player;
    private NavMeshAgent agent;
    private float nextActionTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextActionTime = Time.time;
        obj = GameObject.Find("Player");
        player = obj.transform;
    }

    void Update()
    {
        
        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + checkInterval;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            


            if (distanceToPlayer < avoidDistance)
            {
                Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
                Vector3 newGoal = transform.position + directionAwayFromPlayer * avoidDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(newGoal, out hit, avoidDistance, 1))
                {
                    agent.SetDestination(hit.position);
                }
            }
            else
            {
                Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, randomMoveRadius, 1))
                {
                    agent.SetDestination(hit.position);
                }
            }


        }
    }
}

