using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //�ړ��p�ϐ�
    float x,z;

    //�X�s�[�h�����p
    float speed = 0.035f;

    // �ő�̉�]�p���x[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // �i�s�����Ɍ����̂ɂ����邨���悻�̎���[s]
    [SerializeField] private float _smoothTime = 0.03f;

    private Transform _transform;

    // �O�t���[���̃��[���h�ʒu
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

        // ���݃t���[���̃��[���h�ʒu
        var position = _transform.position;

        // �ړ��ʂ��v�Z
        var delta = position - _prevPosition;

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        _prevPosition = position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if (delta == Vector3.zero)
            return;

            
        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        var targetRot = Quaternion.LookRotation(delta, Vector3.up);

        // ���݂̌����Ɛi�s�����Ƃ̊p�x�����v�Z
        var diffAngle = Vector3.Angle(_transform.forward, delta);
        // ���݃t���[���ŉ�]����p�x�̌v�Z
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );
        // ���݃t���[���ɂ������]���v�Z
        var nextRot = Quaternion.RotateTowards(
            _transform.rotation,
            targetRot,
            rotAngle
        );

        // �I�u�W�F�N�g�̉�]�ɔ��f
        _transform.rotation = nextRot;
    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") ;
        z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0, z);

        // �x�N�g����0�łȂ��ꍇ�ɐ��K��
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


