using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 normal;
    private Rigidbody rb;
    private Vector3 target;
    private int a;
    public int reflectCount;

    AudioSource destroySound;
    AudioSource reflectSound;

    GameObject Sensya;
    BulletController bulletController;

    private GameObject effect;

    [SerializeField] private Transform self;
        // 最大の回転角速度[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // 進行方向に向くのにかかるおおよその時間[s]
    [SerializeField] private float _smoothTime = 0.0f;

    private Transform _transform;

    // 前フレームのワールド位置
    private Vector3 _prevPosition;

    private float _currentAngularVelocity;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _transform = transform;

        _prevPosition = _transform.position;
        a = 0;

        AudioSource[] audioSource = GetComponents<AudioSource>();
        destroySound = audioSource[0];
        reflectSound = audioSource[1];

    }

    void Update()
    {
        // ポイント
        // Updateの中で値を常に取得すること。
        direction = rb.velocity;
        // 現在フレームのワールド位置
        var position = _transform.position;

        // 移動量を計算
        var delta = position - _prevPosition;

        // 次のUpdateで使うための前フレーム位置更新
        _prevPosition = position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if (delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var targetRot = Quaternion.LookRotation(delta, Vector3.up);

        // 現在の向きと進行方向との角度差を計算
        var diffAngle = Vector3.Angle(_transform.forward, delta);
        // 現在フレームで回転する角度の計算
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );
        // 現在フレームにおける回転を計算
        var nextRot = Quaternion.RotateTowards(
            _transform.rotation,
            targetRot,
            rotAngle
        );

        // オブジェクトの回転に反映
        _transform.rotation = nextRot;


        
    }

    //衝突判定
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") && a<reflectCount)
        {
            reflectSound.Play();
            normal = collision.contacts[0].normal;

            Vector3 result = Vector3.Reflect(direction, normal);

            rb.velocity = result;

            // directionの更新
            direction = rb.velocity;
            self.LookAt(result);
            a++;
        }

        else 
        {
            //破壊
            destroySound.Play();
            Destroy(gameObject);
            GameObject playerObject = GameObject.Find(gameObject.tag);
            
            if(gameObject.tag== "Player")
            {              
                BulletController bulletController = playerObject.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.count--;
                }       
            }

            else
            {   
                EnemyMove enemyMove = playerObject.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.count--;
                }
                
            }

          
        }




    }



}
