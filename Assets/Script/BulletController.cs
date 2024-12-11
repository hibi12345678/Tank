using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	//�ϐ��̐錾
	[SerializeField]
	[Tooltip("�e�̔��ˏꏊ")]
	private GameObject firingPoint;

	[SerializeField]
	[Tooltip("�e")]
	private GameObject bullet;

	[SerializeField]
	[Tooltip("�e�̑���")]
	private float speed = 5f;

	public int count;

	AudioSource firstAudioSource;
	AudioSource secondAudioSource;

	private float reloadTime;

	bool flag;

	void Start()
    {
		//������
		flag = false;
		count = 0;
		reloadTime = 0.0f;
		AudioSource[] audioSource = GetComponents<AudioSource>();
		firstAudioSource = audioSource[0];
		secondAudioSource = audioSource[1];
		
		//�ړ����̍Đ�
		firstAudioSource.Play();
		Invoke("StartTime", 0.3f);
		
	}

	void Update()
	{

		//�e�̔���
		if (Input.GetMouseButtonDown(0) && flag == true && Time.timeScale != 0f)
		{
			    

			if (count < 5 && reloadTime <= 0.0f)
			{
				//AudioSource�^�������Ă���ϐ����Đ�
				secondAudioSource.Play();
				// �e�𔭎˂���ꏊ���擾
				Vector3 bulletPosition = firingPoint.transform.position;
				// ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������BBullet�̌�����Muzzle�̃��[�J���l�Ɠ����ɂ���i3�ڂ̈����j
				GameObject newBullet = Instantiate(bullet, bulletPosition, firingPoint.gameObject.transform.rotation);
				// �o���������e��up(Y������)���擾�iMuzzle�̃��[�J��Y�������̂��Ɓj
				Vector3 direction = newBullet.transform.forward;
				// �e�̔��˕�����newBall��Y����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
				newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
				// �o���������e�̖��O��"Bullet(Clone)"�ɕύX
				newBullet.name = "Bullet(Clone)";
				// �o���������e�̃^�O��ύX
				newBullet.tag  = gameObject.tag;

				//�e������
				count++;
				//�N�[���^�C��
				reloadTime = 0.3f;

			}

		}

		reloadTime -= Time.deltaTime;

		if (Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") )
		{
			if (flag == true)
			{
				// �ꎞ��~����
				firstAudioSource.UnPause();
			}
		}
		else
		{
			// �ꎞ��~
			firstAudioSource.Pause();

		}

	}

	private void StartTime()
    {
		flag = true;
	}

}
