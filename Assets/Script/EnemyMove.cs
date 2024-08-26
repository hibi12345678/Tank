using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
	[SerializeField]
	[Tooltip("�e�̔��ˏꏊ")]
	private GameObject firingPoint;

	[SerializeField]
	[Tooltip("�e")]
	private GameObject bullet;

	[SerializeField]
	[Tooltip("�e�̑���")]
	private float speed = 5f;

	[SerializeField]
	[Tooltip("��")]
	private GameObject effect;

	GameObject sensya;

	[SerializeField]
	[Tooltip("�G���")]
	private GameObject enemy;

	// ���g��Transform
	[SerializeField] private Transform _self;

	Vector3  myPos, pos;

    public int count;

	public int countLimit;

	private int b = 0;

	public float reloadTime; // �����[�h�ɂ����鎞��

	EnemyMove enemyMove;
	BoxCollider boxCollider;

	AudioSource fireSound;
	void Start()
    {
		count = 0;
		Invoke("StartTime", 0.5f);
		reloadTime = 0.0f;
		enemyMove = this.gameObject.GetComponent<EnemyMove>();
		boxCollider = this.gameObject.GetComponent<BoxCollider>();
		sensya = GameObject.Find("Player");
		AudioSource[] audioSource = GetComponents<AudioSource>();
		fireSound = audioSource[1];
	}

    void Update()
    {

		pos = sensya.transform.position;
		myPos = _self.position;
		_self.LookAt(pos);
		// Ray�𐶐�
		Ray ray = new Ray(myPos, (pos - myPos).normalized); // �x�N�g���̍��𐳋K�����ĕ��������߂�
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
		{
			string name = hit.collider.gameObject.name; // �Փ˂�������I�u�W�F�N�g�̖��O���擾


			if (count < countLimit && b == 1 && name == "Player" && reloadTime <= 0.0f)
            {

			  // �e,���𔭎˂���ꏊ���擾
			  Vector3 bulletPosition = firingPoint.transform.position;
	          // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������BBullet�̌�����Muzzle�̃��[�J���l�Ɠ����ɂ���i3�ڂ̈����j
	          GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);
	          GameObject newEffect = Instantiate(effect, bulletPosition, this.gameObject.transform.rotation);

			  // �o���������e��up(Y������)���擾�iMuzzle�̃��[�J��Y�������̂��Ɓj
			  Vector3 direction = new Vector3(sensya.transform.position.x -_self.position.x, sensya.transform.position.y - _self.position.y,  sensya.transform.position.z - _self.position.z);
			  Vector3 unitDirection = direction.normalized;

			  // �e�̔��˕�����newBall��Y����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
			  newBullet.GetComponent<Rigidbody>().AddForce(unitDirection * speed, ForceMode.Impulse);
			  newBullet.name = "Bullet(Clone)";
			  newBullet.tag = gameObject.tag;

			  count++;

			  reloadTime = 2.0f;

			  fireSound.Play();
			}

			
		}

		reloadTime -= Time.deltaTime;
		
	}

	private void StartTime()
	{
		b = 1;
	}

	private void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.name == "Bullet(Clone)")
		{

			enemy.SetActive(false);			
			enemyMove.enabled = false;
				
			boxCollider.enabled = false;
		}

	}
}
