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
        // �ő�̉�]�p���x[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // �i�s�����Ɍ����̂ɂ����邨���悻�̎���[s]
    [SerializeField] private float _smoothTime = 0.0f;

    private Transform _transform;

    // �O�t���[���̃��[���h�ʒu
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
        // �|�C���g
        // Update�̒��Œl����Ɏ擾���邱�ƁB
        direction = rb.velocity;
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

    //�Փ˔���
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") && a<reflectCount)
        {
            reflectSound.Play();
            normal = collision.contacts[0].normal;

            Vector3 result = Vector3.Reflect(direction, normal);

            rb.velocity = result;

            // direction�̍X�V
            direction = rb.velocity;
            self.LookAt(result);
            a++;
        }

        else 
        {
            //�j��
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
