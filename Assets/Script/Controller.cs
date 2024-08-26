using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //移動用変数
    float x,z;

    //スピード調整用
    float speed = 0.035f;

    // 最大の回転角速度[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // 進行方向に向くのにかかるおおよその時間[s]
    [SerializeField] private float _smoothTime = 0.03f;

    private Transform _transform;

    // 前フレームのワールド位置
    private Vector3 _prevPosition;

    private float _currentAngularVelocity;

    int a;

    private void Start()
    {
        _transform = transform;

        _prevPosition = _transform.position;

        for (int i = 1; i <= 9; i++)
        {
            string obstacleName = "Obstacle" + i.ToString();
            GameObject obstacle = GameObject.Find(obstacleName);
            if (obstacle != null)
            {
                a = i;
                DecidePosition();
            }
        }

    }

    private void Update()
    {

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

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") ;
        z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0, z);

        // ベクトルが0でない場合に正規化
        if (move.magnitude > 1)
        {
            move = move.normalized;
        }
        transform.position += move*speed;
    }


    private void DecidePosition()
    {
        if (a==1)
        {
            this.gameObject.transform.position = new Vector3(-5, 0, 3);
        }
        else if( a == 2)
        {
            this.gameObject.transform.position = new Vector3(-5, 0, 0);
        }
        else if (a == 3)
        {
            this.gameObject.transform.position = new Vector3(-7, 0, 4);
        }
        else if (a == 4)
        {
            this.gameObject.transform.position = new Vector3(-8, 0, 0);
        }
        else if (a == 5)
        {
            this.gameObject.transform.position = new Vector3(-7, 0, -4.5f);
        }
        else if (a == 6)
        {
            this.gameObject.transform.position = new Vector3(-5, 0, 3);
        }
        else if (a == 7)
        {
            this.gameObject.transform.position = new Vector3(-4.5f, 0, -4);
        }
        else if (a == 8)
        {
            this.gameObject.transform.position = new Vector3(-6, 0, -5);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(-7.5f, 0, -4.5f);
        }
    }
}


