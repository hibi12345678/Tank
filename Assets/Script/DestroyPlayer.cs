using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("êÌé‘")]
    private GameObject sensya;

    BulletController bulletController;
    int b = 0;
    AudioSource source;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        bulletController = this.gameObject.GetComponent<BulletController>();
        AudioSource[] audioSource = GetComponents<AudioSource>();
        source = audioSource[2];
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Bullet(Clone)" && b == 0)
        {
            b = 1;
            sensya.SetActive(false);
            source.Play();
            boxCollider.enabled = false;
            this.gameObject.GetComponent<Controller>().enabled = false;
            bulletController.enabled = false;                       
        }

    }
}
