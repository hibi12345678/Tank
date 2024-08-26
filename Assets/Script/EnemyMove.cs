using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
	[SerializeField]
	[Tooltip("弾の発射場所")]
	private GameObject firingPoint;

	[SerializeField]
	[Tooltip("弾")]
	private GameObject bullet;

	[SerializeField]
	[Tooltip("弾の速さ")]
	private float speed = 5f;

	[SerializeField]
	[Tooltip("煙")]
	private GameObject effect;

	GameObject sensya;

	[SerializeField]
	[Tooltip("敵戦車")]
	private GameObject enemy;

	// 自身のTransform
	[SerializeField] private Transform _self;

	Vector3  myPos, pos;

    public int count;

	public int countLimit;

	private int b = 0;

	public float reloadTime; // リロードにかかる時間

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
		// Rayを生成
		Ray ray = new Ray(myPos, (pos - myPos).normalized); // ベクトルの差を正規化して方向を求める
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)) // もしRayを投射して何らかのコライダーに衝突したら
		{
			string name = hit.collider.gameObject.name; // 衝突した相手オブジェクトの名前を取得


			if (count < countLimit && b == 1 && name == "Player" && reloadTime <= 0.0f)
            {

			  // 弾,煙を発射する場所を取得
			  Vector3 bulletPosition = firingPoint.transform.position;
	          // 上で取得した場所に、"bullet"のPrefabを出現させる。Bulletの向きはMuzzleのローカル値と同じにする（3つ目の引数）
	          GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);
	          GameObject newEffect = Instantiate(effect, bulletPosition, this.gameObject.transform.rotation);

			  // 出現させた弾のup(Y軸方向)を取得（MuzzleのローカルY軸方向のこと）
			  Vector3 direction = new Vector3(sensya.transform.position.x -_self.position.x, sensya.transform.position.y - _self.position.y,  sensya.transform.position.z - _self.position.z);
			  Vector3 unitDirection = direction.normalized;

			  // 弾の発射方向にnewBallのY方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
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
