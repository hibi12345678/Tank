using System.Collections;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float moveSpeed = 2f;         // �ړ����x
    public float moveTime = 1f;          // �ړ�����
    public float waitTime = 1f;          // �ҋ@����
    public Vector3 areaSize = new Vector3(10, 10, 10);  // �ړ��͈�
    GameObject obj;             // �v���C���[�I�u�W�F�N�g
    public float avoidDistance = 5f;     // �v���C���[���痣��鋗��
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
                // �����_���ȃ^�[�Q�b�g�ʒu��ݒ�
                targetPosition = new Vector3(
                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                    Random.Range(-areaSize.y / 2, areaSize.y / 2),
                    Random.Range(-areaSize.z / 2, areaSize.z / 2)
                );

                // �v���C���[�����苗���ȓ��Ȃ痣���ʒu�ɐݒ�
                if (Vector3.Distance(transform.position, player.position) < avoidDistance)
                {
                    Vector3 directionToPlayer = transform.position - player.position;
                    targetPosition = transform.position + directionToPlayer.normalized * avoidDistance;
                }

                // �^�[�Q�b�g�ʒu�Ɉړ�����
                isMoving = true;
                yield return StartCoroutine(MoveToPosition(targetPosition));

                // �ҋ@
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

    // �f�o�b�O�p�ɔ͈͂�\��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, areaSize);
    }
}