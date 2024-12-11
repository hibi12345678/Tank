using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	//変数の宣言
	[SerializeField]
	[Tooltip("弾の発射場所")]
	private GameObject firingPoint;

	[SerializeField]
	[Tooltip("弾")]
	private GameObject bullet;

	[SerializeField]
	[Tooltip("弾の速さ")]
	private float speed = 5f;

	public int count;

	AudioSource firstAudioSource;
	AudioSource secondAudioSource;

	private float reloadTime;

	bool flag;

	void Start()
    {
		//初期化
		flag = false;
		count = 0;
		reloadTime = 0.0f;
		AudioSource[] audioSource = GetComponents<AudioSource>();
		firstAudioSource = audioSource[0];
		secondAudioSource = audioSource[1];
		
		//移動音の再生
		firstAudioSource.Play();
		Invoke("StartTime", 0.3f);
		
	}

	void Update()
	{

		//弾の発射
		if (Input.GetMouseButtonDown(0) && flag == true && Time.timeScale != 0f)
		{
			    

			if (count < 5 && reloadTime <= 0.0f)
			{
				//AudioSource型が入っている変数を再生
				secondAudioSource.Play();
				// 弾を発射する場所を取得
				Vector3 bulletPosition = firingPoint.transform.position;
				// 上で取得した場所に、"bullet"のPrefabを出現させる。Bulletの向きはMuzzleのローカル値と同じにする（3つ目の引数）
				GameObject newBullet = Instantiate(bullet, bulletPosition, firingPoint.gameObject.transform.rotation);
				// 出現させた弾のup(Y軸方向)を取得（MuzzleのローカルY軸方向のこと）
				Vector3 direction = newBullet.transform.forward;
				// 弾の発射方向にnewBallのY方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
				newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
				// 出現させた弾の名前を"Bullet(Clone)"に変更
				newBullet.name = "Bullet(Clone)";
				// 出現させた弾のタグを変更
				newBullet.tag  = gameObject.tag;

				//弾数制限
				count++;
				//クールタイム
				reloadTime = 0.3f;

			}

		}

		reloadTime -= Time.deltaTime;

		if (Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") )
		{
			if (flag == true)
			{
				// 一時停止解除
				firstAudioSource.UnPause();
			}
		}
		else
		{
			// 一時停止
			firstAudioSource.Pause();

		}

	}

	private void StartTime()
    {
		flag = true;
	}

}
